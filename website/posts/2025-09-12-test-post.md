---
title: Basics of AI in Modern Software Development
description: "Developer AI essentials: LLM basics, capability heuristics, prompt patterns, safety, Markdown steering, agent modes, MCP tools, and planning mindset."
date: 2025-09-12T19:17:41.199Z
preview: "A practical foundation for developers adopting AI: model mechanics, capability assessment, prompt design, safety, agent modes, MCP tools, and planning mental models."
draft: false
tags:
- ai
- llm
- developer-productivity
- prompting
- agents
- mcp
- tooling
- markdown
categories:
- engineering-practices
- ai
- developer-enablement

---

This article distills the foundational, *practical* knowledge every software engineer should have when working with modern Large Language Models (LLMs), AI-assisted coding, and emerging agent/tool ecosystems (like MCP). It is written to accelerate new adopters while giving teams a shared vocabulary. Each section is intentionally actionable and opinionated.

## 1. What an LLM Is / Isn’t & Next‑Token Prediction

An LLM (Large Language Model) is a probabilistic sequence model trained to predict the next token (a sub‑word fragment) given prior context. That’s it. Everything that feels like “reasoning,” “creativity,” or “intent” is an emergent property of statistical pattern completion over vast corpora. An LLM is *not* a database, a deterministic rules engine, an oracle of truth, or a secure sandbox. It cannot “know” facts in a grounded sense; it encodes statistical associations. When answering “What is X?”, it’s continuing a plausible explanation path, not performing a symbolic lookup—unless augmented with retrieval or tools.

Key mechanic: **Next-token prediction.** At each step the model outputs a distribution; sampling (temperature > 0) introduces variation. With `temperature = 0` (some vendors use nucleus/other sampling) responses become more deterministic but can still vary across versions or providers due to backend changes. Higher temperature increases diversity AND risk of incoherence.

Fallibility sources include: (1) training data gaps or bias, (2) misaligned prompt framing, (3) hallucination under pressure to be confidently specific, (4) truncation effects when context windows overflow, and (5) user misinterpretation of its capabilities. Treat outputs as *draft artifacts* requiring validation, especially for code that touches security, compliance, or performance‑critical paths.

Think of an LLM as: (a) a fast collaborator for first drafts, (b) a synthesizer across mixed modalities (when enabled), and (c) a pattern expansion engine that can produce structure from ambiguous intent. Do **not** treat it as: (a) a source of guaranteed truth, (b) a license to skip architecture thinking, or (c) a substitute for testing. Your leverage grows when you view it as a *latency reducer for iteration cycles* rather than a correctness oracle.

Practical rules: anchor prompts in *role + goal + constraints*. Re-run with lowered temperature when you need reproducibility (e.g., generating config JSON). Escalate to tool or retrieval augmentation when the model speculates beyond verifiable context.

## 2. Fast Heuristics for Assessing Model Capability

You rarely need a full benchmark suite to decide “Can this model handle my task?” A quick structured probe covers: **Breadth, Depth, Tooling, Context Window, Latency/Cost.**

1. Breadth: Ask it to process a mixed input (e.g., a README snippet plus a TypeScript interface plus an error log). If it conflates modalities or ignores one, breadth is limited. Multimodal variants (code+text+image) allow richer debugging (e.g., screenshot plus stack trace).
2. Depth: Prompt for a multi‑step reasoning chain: “List steps, then solve, then restate constraints, then produce tests.” Weak depth models skip steps or collapse structure. Enforce explicit phases (“Do not output final answer until you output a numbered plan.”) to probe compliance.
3. Tools: If you’re in an agent environment, ask it to call an available tool (e.g., run tests) and observe if it decides *when not to call* tools. Over‑eager tool invocation implies poor cost and safety discipline. Absence of tool awareness means you must manually paste artifacts.
4. Context Window: Feed a synthetic long input with sentinel markers near boundaries (“[START SECTION X] … [END]”). Ask it to reference the marker nearest the start and the one near the end. Any paraphrasing errors or omissions hint at compression or loss. Don’t just trust vendor stated token limits—empirically test.
5. Latency/Cost: Time interactive completions (cold vs warm). If latency exceeds your dev loop tolerance (>5–7s), consider local smaller models for drafting plus a larger remote model for validation. Track token in/out counts—optimize by pruning verbose logs and collapsing redundant instructions.

Capability triage pattern:

```text
Goal -> Minimal probe prompts -> Score (Pass / Partial / Fail) -> Mitigation (Add tools, retrieval, smaller model, transform pipeline)
```

If Depth fails: introduce structured intermediate supervision (“Reflect, then answer”). If Breadth fails: preprocess inputs into a summarized bundle. If Cost is excessive: compress context (embedding-based retrieval or summarization). Maintain a small internal rubric so teams converge on language: “Model X: Breadth=High, Depth=Medium (needs chain scaffold), Tools=Good, Context=Reliable to ~70%, Latency=Acceptable.” Shared rubrics reduce subjective debate.

## 3. Prompt Patterns That Work (Code & Non‑Code)

Effective prompts follow a composable template: **Role + Goal + Constraints + Examples + Output Contract + Self‑Check.** This scales across debugging, architecture brainstorming, refactoring, documentation drafting, and test generation.

Pattern anatomy:

1. Role: “You are a senior backend engineer optimizing memory usage.” Sets tone & abstraction level.
2. Goal: Clear, singular objective: “Refactor the function to eliminate O(n^2) hotspots.”
3. Constraints: Performance budgets, language versions, security rules, style conventions.
4. Examples: Positive (what ‘good’ looks like) and—when useful—negative (anti-patterns to avoid).
5. Output Contract: “Return valid JSON with keys: strategy, diff, tests.” or “Produce only a markdown table.” This reduces post‑processing friction.
6. Self‑Check: “Before final answer, verify against these acceptance criteria: … If any fail, revise.”

Use **progressive prompting**: first ask for a plan, then refine a selected plan, then request code. This reduces wasted tokens when early direction is wrong. For refactors, provide diff context (old code fenced) and ask for a minimal diff rather than full file output.

Structured output examples:

```text
Return valid JSON with keys: "plan" (string[]), "risks" (string[]), "next_step" (string).
```

Include a line: “If unsure, ask for clarification instead of fabricating.” to curb hallucination. In code generation, pin language + version + framework (e.g., “TypeScript 5.4, Node 20, ESM modules”).

Checklist embedding: supply a bullet list of acceptance criteria and instruct the model to append a table with columns Criterion | Pass | Notes | Fix Needed. This encourages deliberate verification. Encourage *idempotent formatting* by forbidding extraneous commentary: “Do not include greetings or prefaces.”

Refinement loop pattern:

```text
User: Draft architecture.
Model: Plan.
User: Revise step 3 to use event sourcing; keep others.
Model: Updated partial.
User: Generate code for only modules A & B.
```

This prevents broad regeneration and reduces merge complexity. Log prompts internally so teams can build a pattern library (share what works—avoid duplicated experimentation).

## 4. Hallucination & Safety Fundamentals

Hallucination is when the model outputs *syntactically plausible but ungrounded* content: non‑existent APIs, fabricated citations, invented performance numbers. Assume hallucination risk increases with: (a) pressure for specificity, (b) absence of grounding data, (c) long generation lengths, (d) ambiguous instructions, (e) misaligned temperature.

Primary mitigation: **“Cite or it didn’t happen.”** When asking for factual claims, require the model either (1) cite canonical sources (with URLs or doc section anchors you can verify) or (2) explicitly label uncertainty. Example: “List limitations; for each, cite official docs or respond UNKNOWN.” Reject answers that blend speculation with citation.

Never paste secrets (API keys, tokens, credentials, proprietary algorithms) into prompts. Threat model includes: logs retained by provider, model fine‑tuning leakage vectors, or unintentional sharing across team transcripts. Scrub with placeholder tokens (`<API_KEY>`). If you *must* analyze sensitive code, prefer on‑prem or local models with auditing.

Ask the model to self‑audit: “List any assumptions you made that were not stated in the input.” This surfaces silent invention. For security‑adjacent code (crypto, auth, serialization), treat AI output as a draft requiring manual review + tests + static analysis.

Safety checklist pattern:

```text
1. Any fabricated identifiers? 2. External calls introduced? 3. License conflicts? 4. Secrets exposed? 5. Missing validation paths?
```

Include that checklist in the prompt and require a pass/fail table. For compliance contexts, capture prompt+response hashes for traceability.

Escalate to retrieval augmentation when the model speculates: “Given the attached official spec, answer only from it; if answer not found, reply NOT_IN_SPEC.” This narrows output domain and reduces hallucination surface area. Remember: silence or refusal is sometimes the safest *correct* answer.

## 5. Using Markdown & Lightweight HTML to Steer Output

Markdown is a *control surface*. You can steer structure, readability, and downstream parsability. Headings (`##`) segment cognitive units. Fenced code blocks (```language) signal executable content; this matters when tools auto‑extract code for tests. Tables succinctly encode acceptance criteria, matrix comparisons, or risk registers. Blockquotes can isolate assumptions or decisions. Horizontal rules (`---`) mark phase transitions in longer sessions.

Hidden guidance: HTML comments (`<!-- internal: do not include in final answer -->`) can supply internal instructions without polluting rendered docs—many models respect them if referenced explicitly (“Follow hidden spec comments.”). Use sparingly to avoid confusion.

Structure encourages compliance: if you provide a skeleton:

```markdown
## Summary
## Constraints
## Plan
## Risks
## Output
```

…the model tends to fill it faithfully. For multi‑file scaffolds, ask for a table: File | Purpose | Key Functions. Then request each file individually to avoid context window bloat.

Markdown vs docx/pdf: Source‑controlled Markdown is diff‑friendly, mergeable, and pipeline‑friendly (convert with Pandoc, md-to-pdf, or static site generators like VitePress). Proprietary binaries (docx) impede code review. PDFs are final artifacts, not collaboration mediums. Encourage “Markdown as the single source of truth,” with automated conversion steps in CI.

Format conversion workflow example:

```bash
pandoc design.md -o design.pdf
```

Ask the model to emit stable anchors by using explicit heading slugs (“## API Design {#api-design}”) if your generator supports it, enabling deep linking.

When generating code + explanation, separate them: explanation first, then a fenced block with exact content. This minimizes accidental copy errors. Provide language tags for syntax highlighting and potential tooling extraction.

## 6. Agent vs Plain Chat (Interaction Modes)

Different mental models apply depending on how the AI is integrated:

1. Ask Mode (Plain Chat / Q&A): Ad‑hoc clarifications, conceptual explanations, lightweight transformation. Low setup cost, no tool orchestration. Best for ideation and quick lint‑level improvements. Risk: overuse for tasks that need verification (e.g., dependency upgrades) without tooling.
2. Edit Mode: The system applies diffs to existing code. Prompts emphasize minimal changes, invariants, and acceptance tests. Provide the *exact* focused context: “Here is file X (lines 40‑120). Add caching layer; keep function signatures stable.” The value is rapid iteration while preserving project style. Always request: “Return only a unified diff.” to reduce merge friction.
3. Agent Mode: Multi‑step autonomous or semi‑autonomous flows leveraging tools (tests, linters, issue trackers). Requires a **plan-first** pattern: Plan -> Execute tool calls -> Summarize intermediate results -> Produce output. You must constrain tool set to avoid scope creep and cost blowups. Insert explicit termination criteria (“Stop when tests green or after 3 attempts.”)
4. Custom Mode (Planning / Coding / Reporting phases): A structured wrapper that enforces lifecycle: (a) Planning: enumerate steps & risks; (b) Coding: generate scoped artifacts; (c) Verification: run tests/tools; (d) Reporting: outcome summary + next actions. This drives consistency across teams and yields auditability.

Switch intentionally: If a request crosses from Ask to Edit (e.g., “Can you also just change the file?”), pause to reframe prompt with file context + invariants. If an Agent's autonomy drifts (“exploring” irrelevant areas), tighten constraints or revert to manual decomposition.

Success signal per mode: Ask → clarity; Edit → minimal diff passes tests; Agent → closed loop with verifiable tool outputs; Custom Mode → reproducible runbook of transformation.

## 7. MCP Basics & Ecosystem (Servers, Tools, Markets)

The **Model Context Protocol (MCP)** defines a standard way for AI agents to discover and invoke external tools exposed by *servers*. A single server can export many tools (e.g., a GitHub server might expose: list issues, create issue, get PR diff, update project field). The agent negotiates capabilities at session start, after which tool calls become structured, auditable actions.

Why constrained tool sets matter: Limiting available tools reduces accidental side effects, improves determinism (fewer branching choices), and strengthens security posture (principle of least privilege). Tool inclusion/exclusion is a *governance decision*: only enable what the task domain genuinely needs. This also improves reasoning because the model narrows its strategy search space.

Server vs Tool: Think of a server as a namespace or capability bundle; tools are the atomic operations. Tools should be coarse enough to be meaningful (“create_issue”) but not so broad they become ambiguous (“manage_project_things”). Provide clear input/output schemas so the agent can compose them.

Playwright MCP: A server that can run end‑to‑end tests, generate new tests from recorded user flows, capture traces/screenshots, and return structured test diagnostics. When paired with a code editing agent, this enables a closed loop: generate fix → run tests → inspect failures → iterate.

MCP Markets (emerging): Registries/directories where vetted servers (GitHub, Filesystem, HTTP, Jira/Linear) can be discovered. For internal teams, maintain a curated allowlist. Audit logs should capture: timestamp, tool name, parameters, result status.

Common team servers:

- GitHub: read/write issues, fetch PR diffs, label triage, project field manipulation.
- Playwright: execute test suites, generate tests from manual steps, collect traces/videos.
- (Optional) Filesystem: read specific whitelisted paths, propose diffs.
- (Optional) Bash/Command: run safe, sandboxed scripts (high risk; restrict heavily).
- (Optional) HTTP: fetch API responses for grounding (enforce domain allowlist).

Adoption strategy: Start with read‑only tools, validate reasoning quality, then phase in write operations. Instrument cost & latency per tool call to refine autonomy budgets.

## 8. Mental Model of an Agent Plan

Robust agent workflows follow a loop: **Perception → Plan → Tool Calls → Verification → Output**. Skipping stages leads to brittle or unsafe automation.

1. Perception: Gather current state (files, test results, issue description). Avoid hallucinated assumptions by explicitly listing observed facts.
2. Plan: Produce a concise, ordered list of steps with success criteria. Enforce “Don’t execute yet” so you can intercept flawed strategies. Good plans decompose into idempotent, reversible actions.
3. Tool Calls: Execute minimal necessary tools. After each significant tool result, *re-evaluate plan deltas*: Did test output change the hypothesized root cause? Avoid plowing ahead with stale assumptions.
4. Verification: Confirm acceptance criteria (tests green, linter clean, performance threshold met). If failure, reflect (“What was wrong in prior assumption?”) before retrying. Limit retry loops (e.g., max 3) to prevent runaway cost.
5. Output: Produce a structured summary: Changes made, evidence (test summary), residual risks, recommended next steps. This becomes a durable artifact for code review.

In GitHub Copilot agent contexts (online or local dev session), you can observe this loop manifest as: the agent reading diffs or errors (Perception), drafting a TODO execution sequence (Plan), invoking test/lint servers (Tool Calls), adjusting code based on failures (Verification), then posting a summarized result (Output). Encouraging explicit state after each phase increases transparency and allows early human intervention.

Anti-patterns: (a) Monolithic “do everything” prompts with no checkpointing, (b) silent tool failures swallowed by the model, (c) redundant test re-runs without changed inputs, (d) large, unreviewable diffs. Counter by embedding: “After each tool call, restate: Observations | Plan Delta | Next Action.”

Design prompts for agents like designing APIs: clear preconditions, postconditions, error modes (e.g., “If dependency install fails, stop and summarize logs.”). Push for *deterministic scaffolding*—the creativity should live inside well-bounded steps, not in whether steps exist.

## 9. Quick Reference Tables

### Capability Heuristic Rubric

| Dimension | Probe Prompt Snippet | Red Flag | Mitigation |
|-----------|----------------------|----------|------------|
| Breadth | Combine code + log + prose | Ignores one modality | Pre-summarize missing modality |
| Depth | Ask for plan then solution | Skips plan | Enforce phased output |
| Tools | Request selective tool use | Calls everything | Narrow tool set |
| Context | Use boundary sentinels | Misquotes edges | Chunk & retrieve |
| Latency/Cost | Time cold vs warm | >10s avg | Smaller or hybrid model |

### Prompt Pattern Skeleton

```text
Role:
Goal:
Context:
Constraints:
Examples:
Output Contract:
Self-Check:
```

### Safety “Cite or Not” Rule

```text
If factual claim has no verifiable citation -> label UNKNOWN instead of guessing.
```

## 10. Final Takeaways

LLMs accelerate iteration, but *you* own correctness, security, and maintainability. Treat prompting as an engineering discipline: benchmark heuristically, structure outputs, constrain autonomy, and institutionalize safety patterns. Build internal playbooks so new developers ramp through shared artifacts—not tribal memory. Start simple (Ask & Edit), layer in tools responsibly, and evolve toward agentic workflows with auditable loops.

Adopt a mantra: *Plan explicitly, execute minimally, verify aggressively, summarize transparently.* That mindset compounds productivity while containing risk.
