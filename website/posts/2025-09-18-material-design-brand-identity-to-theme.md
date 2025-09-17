---
title: Material Design Brand Identity → Theme Tokens Guide
description: "From brand colors to tonal palettes, semantic roles, design tokens, contrast and an accessible Material Design theme."
date: 2025-09-18T13:30:00.000Z
preview: "Practical path: capture brand inputs, generate tonal palettes, map roles, define tokens, enforce contrast, and implement a resilient Material theme."
draft: false
tags:
- material-design
- design-system
- design-tokens
- accessibility
- theming
- ux
categories:
- frontend
- design-systems
type: default
---

A strong Material Design theme is not “pick a primary & accent and ship.” It is a structured translation of brand identity → seed colors → tonal palettes → semantic roles → cross-platform design tokens → implementable, testable theme surfaces with guaranteed contrast.

## 1. Overview

| Step | Input | Output | Tooling / Artifact | Risk If Skipped |
|------|-------|--------|--------------------|-----------------|
| Brand Audit | Logo, marketing site, typography mood | Canonical brand primitives | Brand spec (Figma/Markdown) | Incoherent visual identity |
| Seed Color Selection | Primary + supporting neutrals | 1–3 seed hex values | Palette draft | Weak tonal harmony |
| Tonal Palette Generation | Seed(s) + MD algorithm | Tonal scales (0–100) | JSON palette export | Inconsistent light/dark states |
| Role Mapping | Tonals + product context | Semantic roles (primary, surface, error, etc.) | Role matrix | Arbitrary color reuse |
| Token Definition | Roles + platforms | Token set (global/alias/component) | tokens.json | Hard-to-scale overrides |
| Contrast Validation | Tokens vs backgrounds | WCAG AA/AAA results | Contrast report | Accessibility failures |
| Implementation | Tokens + framework (e.g. Vuetify) | Theme configuration + CSS vars | theme.ts / css | Drift between design & code |
| Regression Guard | Snapshot & linting | Automated checks | Tests (visual + contrast) | Silent regressions |

## 2. Brand Inputs (Ground Truth)

Gather:

- Brand narrative keywords (e.g. "trust", "momentum", "clarity").
- Existing marketing hex values (primary logo color, accent, neutral backgrounds).
- Usage frequencies (primary 60%, secondary 30%, accent 10% as a starting heuristic).
- Accessibility promises (e.g. minimum contrast AA for text, AAA for critical alerts).

Store this in a lightweight `brand-spec.md` so engineering can diff reasoning over time.

## 3. Seed & Tonal Palettes

Material Design 3 uses a *seed color* to derive a tonal palette (0 → 100). If you have multiple brand hues (e.g. Indigo + Teal) you may create separate tonal tracks or restrict one as accent.

Essential tonal stops commonly used:

| Tone | Usage Hint |
|------|------------|
| 0–10 | Highest contrast content on light surfaces; dark mode surfaces |
| 20–30 | Elevated surfaces / outlines in dark mode |
| 40–50 | Mid emphasis text on dark, accents on light |
| 60 | Primary default in many light themes |
| 70–80 | Secondary containers / hovered states |
| 90–95 | Surface variants / subtle backgrounds |
| 98–100 | Pure surfaces (light) or high-elevation overlays |

## 4. Mapping Tonals to Semantic Roles

Define a matrix mapping each role to a tonal value + reasoning.

| Role | Light (Tone) | Dark (Tone) | Rationale |
|------|--------------|-------------|-----------|
| primary | 60 | 70 | Sufficient contrast vs white & dark surfaces |
| on-primary | 100 | 10 | Legibility inside primary elements |
| primary-container | 90 | 30 | Container differentiation |
| on-primary-container | 10 | 90 | Max contrast in container |
| error | (seed: red tone 60) | 60 | Conventional alert visibility |
| on-error | 100 | 10 | Text inside error surfaces |

Use product screenshots to validate the matrix—visual judgment complements numeric contrast.

## 5. Designing Token Layers

Three-layer token strategy keeps scale manageable:

- Global Tokens (raw, brand-invariant semantic: `color.primary`, `color.surface`, `radius.sm`).
- Alias / Context Tokens (role-specific or stateful: `button.primary.background`, `card.outlined.border`).
- Component Tokens (lowest level: `v-btn-padding-x`, `nav-drawer-elevation`).

Example `tokens.json` fragment:

```json
{
  "color": {
    "primary": "{palette.indigo.60}",
    "on-primary": "{palette.indigo.99}",
 
    "surface": "{palette.neutral.98}",
    "surface-variant": "{palette.neutral.90}",
    "outline": "{palette.neutral.50}"
  },
  "radius": {
    "sm": "4px",
    "md": "8px"
  },
  "button": {
    "primary": {
      "bg": "{color.primary}",
      "fg": "{color.on-primary}",
      "radius": "{radius.md}"
    }
  }
}
```

## 6. Contrast & Accessibility

Run automated contrast checks across each foreground/background pair. Required minimums (WCAG 2.1):

| Text Type | Min Contrast (AA) | Target (Stretch) |
|-----------|-------------------|------------------|
| Focus ring vs adjacent | 3:1 | 4.5:1 |

If a chosen tone pair fails, adjust the role tone rather than injecting ad-hoc overrides—maintain matrix integrity.

## 7. Tooling Workflow

1. Start with seed color(s) in a palette generator (Material Theme Builder or CLI).
2. Export tonal palette JSON.
3. Feed into a token build step (Style Dictionary / custom script) that resolves `{palette.*}` references.
4. Emit platform artifacts: `tokens.css`, `tokens.d.ts`, `tokens.android.xml`, `tokens.ios.json` (as needed).
5. Run contrast test script (e.g. Node + `color-contrast` package) failing CI on regressions.
6. Publish versioned token package (npm) consumed by app(s).

## 8. Example Implementation (Vuetify 3 Theme)

```ts
// theme.ts
import { createVuetify } from 'vuetify';
import 'vuetify/styles';

const brandLight = {
  dark: false,
  colors: {
    primary: '#4F46E5',
    'on-primary': '#FFFFFF',
    surface: '#FAFAFE',
 
    'surface-variant': '#ECECF4',
    outline: '#9393A3',
    error: '#DC2626',
    'on-error': '#FFFFFF'
  }
};

const brandDark = {
  dark: true,
  colors: {
    primary: '#A5B4FF',
    'on-primary': '#1E1B2E',
    surface: '#121214',
 
    'surface-variant': '#2A2A32',
    outline: '#5A5A66',
    error: '#F87171',
    'on-error': '#1E1E1E'
  }
};

export const vuetify = createVuetify({
  theme: {
    defaultTheme: 'brandLight',
    themes: { brandLight, brandDark }
  }
});
```

## 9. Evolution & Governance

- Maintain a CHANGELOG for tokens; bump minor on additive, major on breaking semantic shifts.
- Sunset tokens via deprecation comments & CI warnings before removal.
- Quarterly contrast re-validation (designs & real usage screens can drift).
- Use *design linting* (Figma plugins) to catch off-token color usage.

## 10. Migration Strategy (If Coming From Ad-Hoc Colors)

| Phase | Action | Success Criteria |
|-------|--------|------------------|
| Audit | Extract all hard-coded `#` colors & frequency | Color inventory produced |
| Cluster | Group by visual intent (primary, info, border, subtle-bg) | >= 90% mappings decided |
| Seed | Select canonical seed(s) & neutrals | Signed off by design & brand |
| Generate | Produce tonal palettes | JSON artifacts stored in repo |
| Map | Build role matrix (light/dark) | Matrix passes initial contrast |
| Tokenize | Implement tokens + build pipeline | App builds with tokens only |
| Enforce | Add lint/CI checks (no raw hex) | New PRs blocked on violations |
| Iterate | Tweak tones for contrast / emotion | No manual inline overrides |

## 11. Quality Checklist

```text
[ ] Brand spec documented & versioned
[ ] Seed color(s) agreed & justified
[ ] Tonal palette JSON exported (light & dark)
[ ] Role matrix (light/dark) reviewed with design + engineering
[ ] All semantic tokens defined (global + alias + component)
[ ] Contrast report passes AA (body) & AAA (critical text)
[ ] Token build pipeline outputs multi-platform artifacts
[ ] Theme implemented in framework (Vuetify / etc.)
[ ] CI checks for raw hex & contrast regressions
[ ] Governance process (changelog + deprecation) in place
```

## 12. Summary

Translating brand identity into a robust Material Design theme is an *information architecture* exercise for color. When you formalize inputs → tonal math → semantic roles → tokens → implementation, you gain consistency, accessibility, and agility. Invest early; it pays down design debt continuously.
