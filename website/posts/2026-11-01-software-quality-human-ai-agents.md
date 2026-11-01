---
title: "Software Quality in the Age of AI: Are Small Teams Building Faster or Just Creating Smaller Waterfalls?"
description: "A practical look at DORA research, living specifications, and measurable quality when humans and AI agents develop software together."
date: 2026-11-01T00:00:00.000Z
preview: "A holiday management feature shows why faster code generation is not the same as better software delivery."
draft: true
tags:
  - ai
  - software-quality
  - dora
  - agents
  - development-process
categories:
  - engineering-practices
  - ai
type: default
---

# Software Quality in the Age of AI: Are Small Teams Building Faster or Just Creating Smaller Waterfalls?

AI agents can implement features rapidly. But can a small development team demonstrate that it is delivering *better software* rather than simply generating code faster?

A familiar pattern is a stakeholder describing a feature, a developer translating it into tasks, an agent producing implementation and tests, and the customer reviewing the result at the end. Even if this takes days rather than months, it can become a **mini-waterfall**: assumptions are discovered too late.

The 2025 [DORA State of AI-assisted Software Development](https://dora.dev/research/2025/dora-report/) describes AI as an amplifier of organisational strengths and weaknesses. Its findings associate AI with higher throughput while also identifying delivery-instability challenges. The lesson is not that AI causes poor quality; it is that **the engineering system around AI matters**.

Let's explore that system through one ordinary feature.

## The feature: holiday management in an existing web application

A company asks: *“Employees should request holidays online, and managers should approve or reject them.”*

An agent could quickly generate a database table, forms, endpoints and tests. But what about half-days, public holidays, allowance limits, overlapping leave, manager absence, cancellations, and approval authority?

The initial sentence describes a desired capability, not all of its business rules. We should not freeze every imaginable requirement upfront. Instead, we should make decisions needed for the next usable increment, document assumptions and invite feedback early.

## Phase 1: agree on intent and the first increment

The business objective is to replace email-based requests with a traceable approval workflow. The actors are employees, managers and HR administrators.

For the first increment, suppose the business agrees that requests cover full working days; a designated manager approves them; employees cannot exceed their available allowance; pending requests can be withdrawn; and public holidays follow an agreed calendar.

These are **example decisions**, not universal HR policies. The important outcome is that stakeholders and implementers agree on what they mean.

**Evidence:** agreed objective, actors, scope, open questions and acceptance criteria.

**Measure:** clarification-related rework, defined as work repeated because an agreed requirement was misunderstood.

## Phase 2: preserve a living specification and domain language

A specification should be useful to the business, developers, testers and future agents. It needs consistent terms:

| Term | Meaning |
| --- | --- |
| Leave request | An employee's request for time off |
| Available balance | Entitlement remaining under the agreed policy |
| Approver | A person authorised to decide a request |
| Pending | Submitted but not yet decided |
| Approved | Accepted by an authorised approver |
| Rejected | Declined by an authorised approver |
| Withdrawn | Cancelled before a decision |

If the existing system calls the entity `LeaveRequest`, a new agent should not casually invent `HolidayBooking` for the same concept.

One acceptance scenario might be:

> Given an employee has sufficient available leave allowance and a pending request, when their authorised manager approves it, then the request becomes approved, the balance reflects the decision, and the audit history records who approved it.

Other scenarios cover insufficient balance, unauthorised approval, concurrent decisions and withdrawal.

Version the specification alongside the code where practical. Update it when the business confirms a new rule. An agent revisiting the feature months later needs to distinguish *intended behaviour* from whatever the current code happens to do.

**Evidence:** agreed vocabulary, examples, decisions, traceable acceptance criteria.

**Measure:** acceptance defects attributable to missing or misunderstood rules.

## Phase 3: implement through small, reviewable changes

Rather than asking an agent to build the entire module in one pass, break work into coherent changes:

| Change | Scope | Evidence |
| --- | --- | --- |
| PR 1 | Leave-request model and persistence | Schema and domain tests |
| PR 2 | Submission and allowance validation | API and boundary tests |
| PR 3 | Approval and rejection | Permission and workflow tests |
| PR 4 | Employee and manager UI | UI and integration tests |
| PR 5 | Audit history and notifications | Integration and failure-path tests |

These are illustrative PR boundaries, not a mandatory sequence. Integrate continuously; use feature flags or other controls where necessary. Demonstrate usable slices to stakeholders before all five changes are complete.

Each PR should link to the requirement, state assumptions, show what was verified, and identify residual risks. Agents can propose and implement changes, but generated explanations are not substitutes for actual test results.

**Evidence:** reviewed diffs, executable checks, traceability, integration results.

**Measures:** PR cycle time, review rework and integration delay.

**The crucial distinction:** five small PRs released together without customer feedback may still constitute a mini-waterfall.

## Phase 4: test behaviour and obtain customer acceptance

Automated checks should verify normal cases and important failures: sufficient balance, unauthorised approval, duplicate decisions, concurrent updates and notification failures.

Then a representative customer or business user exercises the workflow in a suitable environment. They may realise that managers need visibility of overlapping leave before approving a request.

That is feedback about product behaviour, not necessarily a coding defect. Record it, decide whether it changes the current increment, update the specification, and implement the next change.

**Evidence:** test results plus explicit acceptance of the demonstrated behaviour.

**Measures:** acceptance defects and feedback turnaround time.

## Phase 5: publish, observe and recover

A successful deployment is not proof of feature quality. The team needs to see whether employees can submit requests, managers can decide them, permission failures are handled, and support can investigate disputed decisions.

Use relevant telemetry, audit records and recovery procedures. The [DORA software delivery metrics](https://dora.dev/guides/dora-metrics-four-keys/) offer an established lens on delivery performance: deployment frequency, change lead time, change failure rate, failed-deployment recovery time and deployment rework rate. These are not direct measures of whether the holiday policy was implemented correctly.

**Evidence:** successful release, production diagnostics, recovery readiness.

**Measures:** DORA delivery metrics, escaped defects and relevant user outcomes.

## Phase 6: maintain and evolve without losing intent

Three months later, the company wants half-day holidays. A different developer or agent takes the task.

The maintained specification should explain balance calculations, request states, approval authority and the meaning of each term. The team can then examine whether the existing data model supports fractions, what happens to historical records, and which tests must change.

The agent may propose a refactor. The human reviewer checks that it preserves business invariants, not merely that the code compiles. Update the specification and regression tests when the new behaviour is accepted.

**Evidence:** preserved existing behaviour, approved new scenarios, updated domain documentation.

**Measures:** regression defects, maintenance rework and time to safely change an existing feature.

## What DORA adds—and what we must measure ourselves

DORA's 2025 [AI capabilities model](https://dora.dev/ai/capabilities-model/report/) highlights seven capabilities: small batches, user-centric focus, healthy data ecosystems, AI-accessible internal data, strong version control, quality internal platforms and a clear communicated AI stance.

For a small team, the most immediate applications are shorter feedback loops, reliable source-controlled context and a shared definition of successful delivery.

We should combine DORA's delivery measures with locally defined indicators:

| Quality question | Possible indicator |
| --- | --- |
| Are requirements understood? | Clarification-related rework |
| Does the customer accept the feature? | Acceptance defects and feedback time |
| Are changes safe to deliver? | DORA throughput and instability metrics |
| Can we maintain existing behaviour? | Regression defects and maintenance effort |
| Do agents help rather than add hidden work? | Substantive correction required after agent-assisted changes |

These supplementary indicators are **proposals, not official DORA metrics**. Establish definitions and a baseline before comparing trends. Avoid using them to rank individual developers or reward raw code volume.

If PRs become faster while customer rework and production incidents increase, we have not demonstrated better quality.

## Who owns quality when humans and agents collaborate?

Agents can research existing code, identify missing cases, propose designs, implement changes, run checks and document evidence. Humans remain accountable for business decisions, risk acceptance and the outcome delivered to users.

That is not a claim that every line must be manually written or inspected. It means the team must have a credible way to establish correctness, maintainability and operational confidence proportional to risk.

The specification is not a one-time handover document. It is shared, evolving knowledge connecting business intent, code, tests, deployment and future maintenance.

## Conclusion

Good software is not defined by who wrote the code.

It is software that solves the intended problem, behaves correctly, can be delivered and operated safely, and remains understandable when requirements change.

AI gives small teams an opportunity to shorten the distance between an idea and working software. But if we preserve late feedback and unclear requirements, we may only build smaller waterfalls more quickly.

**The real improvement is a continuous loop of shared intent, small changes, evidence, customer feedback and maintained knowledge.**

## Further reading

- [DORA — 2025 State of AI-assisted Software Development](https://dora.dev/research/2025/dora-report/)
- [DORA — AI Capabilities Model](https://dora.dev/ai/capabilities-model/report/)
- [DORA — Working in Small Batches](https://dora.dev/capabilities/working-in-small-batches/)
- [DORA — Software Delivery Metrics](https://dora.dev/guides/dora-metrics-four-keys/)
