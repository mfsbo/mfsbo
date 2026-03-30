---
title: "Aspire 13 and VoidZero / Vite+ in 2026 — Two Ecosystems Closing the Same Gap"
description: "March 2026 snapshot of Aspire 13 and VoidZero's Vite+ toolchain: current versions, key people, stack options, and how both ecosystems are reducing the distance between local development and production deployment."
date: 2026-03-30T00:00:00.000Z
preview: "Aspire 13 is now independently versioned, polyglot, and built on .NET 10 with .NET 11 already in preview. VoidZero has moved from separate tools to a public Vite+ and Void story on top of Vite, Rolldown, Oxc, Vitest, and Cloudflare. This post compares the two in their current 2026 form."
draft: false
tags:
  - aspire
  - dotnet
  - dotnet11
  - voidzero
  - vite-plus
  - vite
  - nodejs
  - cloudflare
  - vuejs
  - developer-experience
categories:
  - engineering-practices
  - frontend
  - architecture
  - developer-enablement
type: default
---

<!-- markdownlint-disable MD025 MD033 -->
# Aspire 13 and VoidZero / Vite+ in 2026 — Two Ecosystems Closing the Same Gap

This post needed a 2026 refresh.

In March 2026, both ecosystems have moved on from the 2025 framing:

- **Aspire is now at 13.1.x**, versioned independently, officially branded as **Aspire**, and documented on [aspire.dev](https://aspire.dev/). Microsoft's support policy currently lists **Aspire 13.1.2** as the supported release.
- **.NET 10 is the current LTS base** for Aspire 13, while **.NET 11 is already in preview** with Preview 2 available in March 2026.
- **VoidZero is no longer just a promise of better tooling**. It has publicly launched **Vite+** and is building a clearer product story around a unified toolchain and **Void**, a deployment platform built around Cloudflare.
- On the JavaScript side, **Node 24 is the Active LTS line** and **Node 25 is the Current release**, which matters because the Vite / Vite+ / Workers ecosystem moves quickly and benefits from staying close to the modern Node baseline.

That makes 2026 a much better moment to compare these stacks, because both are now more explicit about the same goal:

> **Reduce the distance between local development, team workflows, and production deployment without forcing developers to assemble everything themselves.**

---

## 2026 Snapshot

| Area | Aspire | VoidZero |
|---|---|---|
| Current 2026 position | **Aspire 13.1.x** supported | **Vite+** publicly launched by VoidZero |
| Platform base | **.NET 10 LTS** today, **.NET 11 Preview 2** already available | **Node 24 Active LTS** for production, **Node 25 Current** for latest features |
| Main strength | Cloud-native orchestration, service discovery, observability, polyglot app composition | Unified JS/TS toolchain, faster Rust core, integrated developer workflow, Cloudflare-aligned deployment |
| Core story | AppHost + service defaults + dashboard + integrations + pipelines | Vite + Rolldown + Oxc + Vitest + unified CLI + Void platform |
| Deployment direction | Azure-first, container-first, polyglot distributed apps | Edge/serverless-first, Cloudflare-based, client/server split with platform services |

---

## Aspire in March 2026: What Changed

The most important update is simple: **Aspire is no longer just “.NET Aspire 9.x.”**

According to Microsoft's official support policy, Aspire now has its **own release cadence and support lifecycle**. In practice, that means:

- **Aspire 13.1** is the supported train in March 2026.
- Older lines such as **13.0** and even the **9.x** series are already out of support.
- Microsoft now supports **only the latest Aspire release level**, so the upgrade cadence is intentionally fast.

That change matters because it signals that Aspire is becoming a product with its own identity rather than a sidecar to a single .NET release.

### It is now broader than “just .NET”

Aspire 13 is also important because Microsoft widened the scope of the platform:

- **JavaScript apps are now first-class** in the orchestration story.
- **Python apps are also first-class**.
- The docs and branding have shifted to **Aspire** rather than tying everything to the .NET name.

That makes the comparison to VoidZero much more interesting than it was a year ago. Aspire is still clearly strongest for .NET teams, but it is now also trying to solve the **polyglot app-platform problem** — the same broader workflow problem that VoidZero is attacking from the JavaScript side.

### What Aspire 13 offers right now

#### 1. App orchestration in code

The core Aspire value is still the same, but the platform is sharper now:

- **AppHost** remains the central composition point.
- Services, databases, caches, queues, and external dependencies are declared in code.
- Resources get **connection information, environment values, and service discovery** automatically.
- Teams spend less time stitching together YAML, local scripts, and ad-hoc environment setup.

#### 2. Better polyglot support

This is one of the biggest 2026 reasons to take Aspire seriously.

Aspire 13 adds or improves:

- **JavaScript app orchestration** with package-manager detection for `npm`, `pnpm`, and `yarn`
- better support for **Vite-style frontend projects** inside the Aspire host
- **Python application support**, including local workflows and container publishing
- improved handling of connection details across different languages and frameworks

That means a real-world stack can now look like this:

```text
Aspire AppHost
├── ASP.NET Core API
├── Vite / React or Vue frontend
├── PostgreSQL
├── Redis
└── Python worker or AI service
```

A year ago that comparison felt theoretical. In 2026 it is much more practical.

#### 3. Better publish / deploy pipelines

One of the biggest Aspire 13 additions is the new **pipeline-oriented workflow** around `aspire do`.

That is important because Aspire is not only trying to start your app locally anymore. It is trying to own more of the path from:

- build,
- validate,
- publish,
- deploy,
- and post-deploy verification.

That is very close to what frontend developers have been asking for from their tooling for years: **less glue, more default workflow**.

#### 4. Dashboard + MCP story

Aspire's local dashboard was already useful in 2025. In 2026, the more interesting step is that it is also part of the **AI-assisted diagnostics** story through an MCP server.

This is exactly the kind of thing that makes Aspire feel current instead of “just another orchestration layer”:

- live resources,
- logs,
- traces,
- operational metadata,
- and assistant-friendly access to that information.

That puts Aspire directly in the 2026 conversation about how development, operations, and AI tooling intersect.

---

## .NET 10 vs .NET 11 in this discussion

If you are looking at Aspire in March 2026, the practical version guidance is:

- **Use .NET 10** if you want the stable LTS foundation.
- **Watch .NET 11 Preview 2** if you want to understand where the platform is going next.

The reason .NET 11 matters to this post is not that Aspire 13 depends on it today. It matters because it shows what Microsoft is investing in for the next wave of app development:

- more runtime performance work,
- improved OpenTelemetry and ASP.NET Core capabilities,
- better cloud/container workflows,
- and continued work on reducing the friction around async, APIs, and distributed systems.

In other words, Aspire 13 is the current platform story, while .NET 11 is a signal about where the underlying runtime and SDK are heading.

---

## Why Aspire matters for backend and frontend teams

Aspire still lands most obviously with backend developers, but I think that undersells it in 2026.

### Backend value

For backend teams, Aspire gives you:

- opinionated service composition
- built-in service discovery
- local distributed observability
- healthier defaults for resilience and telemetry
- clearer deployment workflows for container-first apps

### Frontend value

For frontend teams, Aspire matters when your frontend is part of a bigger product system:

- your Vite app can live in the same local topology as your APIs, cache, queue, identity provider, and worker services
- local startup becomes **one host, one graph, one dashboard** rather than several README tabs
- you can model **frontend + backend + infra dependencies** together rather than pretending the frontend is separate from the system it depends on

That is one reason people like **David Fowler**, **Damian Edwards**, **Mady Montaquila**, and **Scott Hanselman** keep talking about it publicly. They are not only talking about .NET syntax or one framework. They are talking about reducing the systems-integration tax on modern teams.

---

## The People Driving Aspire

| Person | Contribution / perspective |
|---|---|
| **David Fowler** | The architectural depth behind ASP.NET Core, hosting, service discovery, and a lot of the “good default” design thinking that Aspire builds on. |
| **Damian Edwards** | A key voice in the developer loop, tooling quality, and practical “does this work on a real machine?” side of the platform. |
| **Mady Montaquila** | A visible product and communication bridge for Aspire: roadmap, demos, feedback loops, and helping developers understand where the platform is going. |
| **Scott Hanselman** | Helps connect Aspire to real-world product teams and real-world apps rather than just idealized demos. His streams and discussions make the Azure/.NET ecosystem side of the story much easier to follow. |

These are exactly the kinds of people who matter when a platform is trying to become a workflow, not just a library.

---

## VoidZero in March 2026: It Is About Vite+ Now

The old way of talking about VoidZero was: “Evan You started a company that might eventually unify JavaScript tooling.”

That is not the current situation anymore.

In 2026, the clearer framing is:

- **VoidZero has publicly launched Vite+** as the product layer.
- The open-source foundation remains **Vite, Rolldown, Oxc, and Vitest**.
- The platform/deployment direction is now much more explicit with **Void**, which is aligned with **Cloudflare** infrastructure.

So instead of talking about a loose ecosystem, it makes more sense to talk about a **toolchain strategy**.

### What Vite+ is trying to do

Vite+ is essentially an answer to the long-standing JavaScript problem that too many important workflows are spread across unrelated tools.

Instead of a team having to mentally stitch together:

- dev server
- bundler
- linter
- formatter
- test runner
- monorepo task runner
- library bundler
- debug UI

…the Vite+ pitch is that these should feel like **one system** rather than a bag of brands.

### The building blocks

#### Vite

Still the center of gravity for modern frontend development, but now more tightly aligned with the rest of the stack.

#### Rolldown

Rolldown is the Rust bundler story. This matters because production builds have historically been where frontend teams hit the worst gap between “fast local” and “slow real.”

#### Oxc

Oxc is probably the most strategically important piece of the whole story:

- parser
- linter
- formatter direction
- minification / transform foundation
- shared AST / shared infrastructure

That matters because once the parser and toolchain core are unified, everything else gets simpler and faster.

#### Vitest

Vitest remains one of the most practical wins in the ecosystem because it uses the same configuration and same mental model as Vite. It is exactly the kind of integration people wanted from Jest-era workflows but rarely got.

### The CLI direction

The big shift with Vite+ is not only raw speed. It is **workflow consolidation**.

The public Vite+ direction includes commands and capabilities around:

- create / scaffold
- dev
- build
- test
- lint
- fmt
- lib
- run
- ui

That makes the comparison to Aspire much stronger than it used to be. They are both trying to become the place where developers start their day.

---

## Void, Cloudflare, and the server/client split

The other important 2026 update is that VoidZero's deployment story is more explicit now.

The platform direction is not just “make builds faster.” It is:

- **build with the unified toolchain**,
- **deploy with Void**,
- and use a **Cloudflare-native runtime and services layer** for the server side.

That is a big deal because it lines up with how many teams actually want to build web products now:

1. **Client bundle** on the CDN
2. **Server or edge logic** close to the user
3. **Secrets and auth kept server-side**
4. **Platform services** like queues, storage, databases, and bindings handled by the deployment platform

This is the same broad problem space the issue description was pointing at.

Frameworks like **Next.js**, **Nuxt**, and now the broader Vite+/Void direction are all moving toward a world where:

- the client is not trusted with secrets,
- the server layer is not a separate afterthought,
- and deployment is part of the framework/toolchain story.

That is why Cloudflare matters so much in the VoidZero discussion. It is not just a hosting target. It is part of the product model.

---

## The People Driving VoidZero

| Person | Contribution / perspective |
|---|---|
| **Evan You** | The central technical figure. Creator of Vue and Vite, and now the person most strongly associated with turning JavaScript tooling into a more coherent platform. |
| **Wes Bos** | Important because he brings the story to a broad working-developer audience through Syntax and practical demos instead of only conference keynotes. |
| **Scott Tolinski (CJ)** | Similar role to Wes, but also useful as a bridge across ecosystems and real app-building workflows. Syntax has helped a lot of developers actually understand what Vite+, VoidZero, and related tools are trying to fix. |
| **Vue core team** | People around Vue, Vite, and adjacent tooling helped create the baseline of ergonomics that made this possible in the first place. |
| **Nuxt / UnJS team** | They matter because Nuxt is where a lot of the “full-stack but still frontend-friendly” execution has happened in practice, especially around server routes, rendering, deployment targets, and Cloudflare support. |

This mirrors the Aspire side more than people sometimes admit. In both cases, visible technical leaders plus strong public educators are moving an ecosystem from tools to workflow.

---

## Node in March 2026: Why It Matters Here

For the JavaScript side, the current Node baseline is part of the story.

As of March 2026:

- **Node 24 (“Krypton”)** is the **Active LTS** line.
- **Node 25** is the **Current** line.
- **Node 22** is in **Maintenance LTS**.

That matters because the fast-moving toolchain story is no longer just about what framework you pick. It is also about whether your runtime baseline is modern enough to benefit from the tooling work being shipped around ESM, bundling, testing, and platform APIs.

If I were choosing a production baseline for a Vite+/Void stack in March 2026, I would default to **Node 24 LTS** unless I had a very specific reason to target something else.

---

## Comparing Aspire and VoidZero in 2026

| Problem | Aspire 13 answer | VoidZero / Vite+ answer |
|---|---|---|
| **How do I start the whole system locally?** | AppHost composes the system graph and starts it with a dashboard | Unified CLI and framework-first tooling reduce the number of separate moving parts |
| **How do I keep dev and prod closer together?** | Container-first, pipeline-first, Azure-aligned orchestration | Same core toolchain for dev/build/test plus Cloudflare-aligned deployment |
| **How do I handle multiple services?** | First-class service composition with resources and integrations | Better app/toolchain composition; service composition depends more on framework + platform choices |
| **How do I handle observability?** | Aspire dashboard, traces, logs, telemetry | Better integrated tooling and UI; production insights lean on the hosting platform |
| **How do I handle secrets and auth?** | Env/config + Azure services + platform integrations | Workers/platform bindings keep secrets server-side and close to the runtime |
| **What kind of team benefits most?** | .NET-heavy or polyglot backend/platform teams | JS/TS-heavy frontend/full-stack teams moving fast on edge/serverless platforms |

The difference is mostly where they start:

- Aspire starts from **distributed application composition**.
- VoidZero starts from **toolchain unification**.

But both are moving toward the same place: a platform-shaped developer experience.

---

## Practical Stack Options in 2026

### Aspire stack options

### 1. API + frontend + data cache starter

```text
AppHost
├── Web API (.NET 10)
├── Vite frontend
├── PostgreSQL
└── Redis
```

Why this is a strong 2026 starting point:

- you get a clean API-first backend
- a modern frontend toolchain
- realistic local infra
- one orchestration entry point

Start with:

- [Aspire getting started docs](https://learn.microsoft.com/dotnet/aspire/get-started/aspire-overview)
- [dotnet/aspire-samples](https://github.com/dotnet/aspire-samples)

### 2. eShop-style multi-service reference

```text
AppHost
├── Catalog service
├── Basket / ordering services
├── frontend app
├── messaging
└── data stores
```

Start with:

- [dotnet/eShop](https://github.com/dotnet/eShop)

This remains one of the best “show me a real stack, not a toy” examples in the Microsoft ecosystem.

### 3. Polyglot team setup

```text
AppHost
├── ASP.NET Core API
├── JavaScript frontend
├── Python worker / AI app
├── PostgreSQL
└── queue or broker
```

This is the 2026 Aspire scenario that feels most different from earlier versions. If your team spans backend, frontend, and AI/service experimentation, Aspire 13 is much more relevant than Aspire 9 ever was.

---

### VoidZero / Vite+ stack options

### 1. Nuxt + Cloudflare Workers

```text
Nuxt app
├── pages/
├── server/api/
├── server/routes/
└── Cloudflare deployment target
```

Why it is strong:

- a familiar full-stack app model
- server routes and rendering built in
- strong Cloudflare path
- good fit for the client/server separation this post is discussing

Start with:

- [Nuxt on Cloudflare](https://nuxt.com/deploy/cloudflare)

### 2. Vite frontend + Hono API on Workers

```text
frontend/
└── Vite app
api/
└── Hono on Cloudflare Workers
```

Why it is strong:

- minimal and clear separation
- excellent for teams that want Vite without immediately adopting a larger meta-framework
- good typed-API story with TypeScript and Hono client helpers

Start with:

- [Hono on Cloudflare Workers](https://hono.dev/getting-started/cloudflare-workers)

### 3. Nuxt + typed server routes + Cloudflare storage/services

```text
Nuxt app
├── app UI
├── server API
├── auth
├── storage / db bindings
└── edge deployment
```

Why it is strong:

- one repo, one framework, one deployment path
- secrets stay server-side
- good fit for product teams that want frontend-first ownership with real backend capability

### 4. Vite+ oriented monorepo

```text
apps/
├── web
├── admin
└── docs
packages/
├── ui
├── api-client
└── config
```

Why it is strong:

- this is where Vite+ becomes more than “faster bundling”
- the dev / build / test / lint / run workflow becomes more coherent across the whole workspace

This is the kind of stack I would expect more teams to explore as Vite+ matures through 2026.

---

## So are Aspire and VoidZero solving the same problem?

I think the honest answer is **yes, but from opposite ends**.

Aspire starts from:

- distributed systems,
- backend reliability,
- resource composition,
- deployment pipelines,
- and observability.

VoidZero starts from:

- frontend performance,
- toolchain fragmentation,
- JavaScript ergonomics,
- build/test/lint cohesion,
- and edge deployment.

But both are trying to remove the same pain:

- too many tools,
- too many seams,
- too much config drift,
- too much difference between “my machine” and “production.”

That is why the comparison is interesting in 2026 in a way it was not in 2024.

---

## People worth following if you want to keep up

### Aspire

- [David Fowler](https://github.com/davidfowl)
- [Damian Edwards](https://github.com/DamianEdwards)
- [Mady Montaquila](https://github.com/maddymontaquila)
- [Scott Hanselman](https://www.hanselman.com/)
- [Aspire blog](https://devblogs.microsoft.com/aspire/)
- [Aspire docs](https://aspire.dev/)

### VoidZero / Vite+

- [Evan You](https://github.com/yyx990803)
- [VoidZero](https://voidzero.dev/)
- [Vite](https://vite.dev/)
- [Rolldown](https://rolldown.rs/)
- [Oxc](https://oxc.rs/)
- [Vitest](https://vitest.dev/)
- [Syntax.fm](https://syntax.fm/)
- [Nuxt](https://nuxt.com/)

---

## Final thought

If I had to summarize the 2026 state in one sentence:

- **Aspire 13** is the most interesting Microsoft move toward a real polyglot app platform.
- **VoidZero / Vite+** is the most interesting move in JavaScript toward a real unified toolchain and deployment experience.

They are not identical, and they are not competing for exactly the same team. But they are responding to the same reality:

> developers do not need more disconnected tools — they need fewer seams between building, running, testing, observing, and deploying software.

That is why both are worth paying attention to right now.

---

## Source notes

For the 2026 details in this refresh, the most useful references were:

- [Aspire support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/aspire)
- [What’s new in Aspire 13](https://aspire.dev/whats-new/aspire-13/)
- [.NET 11 Preview 2 announcement](https://devblogs.microsoft.com/dotnet/dotnet-11-preview-2/)
- [What’s new in .NET 11](https://learn.microsoft.com/dotnet/core/whats-new/dotnet-11/overview)
- [Node.js release schedule](https://github.com/nodejs/Release)
- [Node.js previous releases](https://nodejs.org/en/about/previous-releases)
- [VoidZero](https://voidzero.dev/)
- [Announcing Vite+](https://voidzero.dev/posts/announcing-vite-plus)
