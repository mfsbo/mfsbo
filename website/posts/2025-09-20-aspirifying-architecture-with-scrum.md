---
title: Aspirifying Architecture with Scrum – Scott Hanselman & Aspire Fridays
description: "How Scott Hanselman as Product Owner and three Aspirify dev teams applied Scrum to modernize a 20-year architecture using Aspire, tackling SDK mismatches, tooling gaps, and real-world impediments."
date: 2025-09-20T18:00:00.000Z
preview: "Scott Hanselman joined Aspire Fridays with three Aspirify dev teams to aspirify his podcast app. Acting as Product Owner, Scott exposed real challenges—SDK chaos, naming debt, expiring tokens, even a power outage—while the teams delivered incremental modernization using Scrum principles."
draft: false
tags:
- scrum
- .net
- aspire
- hanselman
- architecture
- developer-experience
categories:
- engineering-practices
- scrum
- architecture
- developer-enablement
type: default
---

# Aspire Fridays with Scott Hanselman: Modernizing Architecture with Scrum

## Introduction  

This session of *Aspire Fridays* brought together Scott Hanselman and three dev teams from the Aspirify group. Acting as a **Product Owner**, Scott opened up his long-running set of applications — blogs, podcasts, and admin sites — for a real-world modernization exercise. Using Scrum thinking, the team tackled architectural challenges and explored how Aspire can unify Scott’s diverse .NET apps.

---
The video can be watched here: [YouTube Link](https://www.youtube.com/watch?v=Z1EjpsOAZBU)

## The Product Owner: Scott Hanselman  

Scott manages several apps under what the team jokingly calls *Hanselman International*:  

- **Hanselman.com** – brochureware site on Razor Pages (.NET 8).  
- **Hanselman Blog** – running on DOSBlog, also in .NET 8.  
- **Hanselminutes Podcast** – separate app with caching and third-party APIs.  
- **Admin site** – newer site in TypeScript.  
- **Azure Friday site** – another .NET app in the mix.  

All of these live behind **Azure Front Door**, hosted on App Service for around $41/month. His role in this session resembled a product owner in Scrum — balancing business needs (cost, performance, reliability) while enabling developers to explore architectural improvements.

---

## The Dev Teams: Aspirify Engineers  

The Aspirify team (Maddie, Damian, David, and others) acted as three **Scrum dev teams**, pairing and swarming around Scott’s repo. Their Sprint Goal: **aspirify Scott’s architecture** by pulling one of his apps into Aspire for orchestration and better developer experience.

---

## Sprint Planning: Choosing a Slice  

The group applied Scrum’s “vertical slice” principle. Instead of aspirifying all apps at once, they selected the **Hanselminutes Podcast** app as their Product Backlog Item. It was complex enough to show value (caching, APIs, feeds), but isolated enough to deliver in a Sprint.

---

## Sprint Backlog: Key Tasks  

1. **Setup Aspire AppHost** – scaffold a host project.  
2. **Reference Podcast Project** – wire it into Aspire.  
3. **Update SDKs** – align Scott’s machine with .NET 10 RC1.  
4. **Fix Dev Loop** – ensure Aspire Run can orchestrate local services.  
5. **Plan for Multi-App Future** – discuss extending to blog, admin, and main site.  

---

## The Daily Scrum Moments  

Like a real Scrum team, blockers and surprises came quickly:  

- **SDK Mismatch** – Scott only had .NET 8 + preview 5, no .NET 9 runtime, causing Aspire templates to misbehave.  
- **Naming Chaos** – legacy folder naming (“Hanselman-Core”) created confusion when adding projects.  
- **Tooling Gaps** – Aspire CLI vs Visual Studio templates led to frustration; the CLI lacks smooth “add to existing solution” workflows.  
- **Power Outage!** – Scott’s entire neighborhood lost power mid-session, forcing him to tether his laptop to a phone hotspot while the team continued.  

These became classic Scrum impediments, handled collaboratively in real time.

---

## Sprint Review: What Was Delivered  

By the end, the team had:  

- An **Aspire AppHost** running Hanselminutes locally.  
- Project references wired through Aspire with resource naming clarified.  
- Confirmation that the Podcast’s caching and third-party API dependencies could be observed via Aspire’s dashboard.  
- A roadmap for scaling this to other apps under Scott’s ecosystem.  

---

## Retrospective: Lessons Learned  

1. **Start small** – Aspirify one app first; expand later.  
2. **Tooling gaps** – CLI experience needs to catch up with Visual Studio integration.  
3. **DevOps pain** – expiring tokens (GitHub, Azure DevOps) are ongoing impediments.  
4. **Resilience matters** – even a power outage didn’t stop progress; distributed Scrum teams can adapt quickly.  

---

## Conclusion  

This Aspire Friday highlighted the intersection of **Scrum practices** and **modern .NET tooling**. With Scott as Product Owner and the Aspirify teams collaborating, they demonstrated how real-world architecture can be modernized incrementally.  

The next Sprint? Expanding Aspire orchestration across Scott’s blog, admin site, and Azure Friday app — continuing the journey from legacy silos to a unified aspirified architecture.
