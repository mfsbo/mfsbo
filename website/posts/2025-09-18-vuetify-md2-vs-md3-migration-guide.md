---
title: Vuetify MD2 vs MD3 – Practical Migration Guide
description: "Key differences between Vuetify MD2 and MD3 plus a pragmatic, incremental migration strategy for teams."
date: 2025-09-18T10:15:00.000Z
preview: "Design tokens, typography, color system, density, elevation and component API changes between Vuetify MD2 and MD3 with a pragmatic migration checklist."
draft: false
tags:
- vue
- vuetify
- material-design
- design-system
- migration
- ui
categories:
- frontend
- design-systems
type: default
---

Material Design 3 (MD3) in Vuetify is not a superficial skin. It formalizes tokens, revisits elevation & color semantics, reduces default density, and introduces more flexible theming primitives. This post compresses what matters when you are maintaining an MD2 codebase and need to sequence an *incremental* (not big‑bang) migration.

## 1. Quick Mental Model

| Aspect | MD2 (Vuetify 2) | MD3 (Vuetify 3) | Why It Matters |
|--------|-----------------|-----------------|----------------|
| Color System | Static palette + variants | Source color → dynamic tonal palettes | Enables auto light/dark & accessibility tuning |
| Typography | Predefined classes (`display-4`, etc.) | Design token driven scales (`text-h1`, semantic roles) | Consistency + easier global adjustments |
| Density | Generally denser defaults | More spacious, touch-friendly spacing | Layout shifts; affects above-the-fold content |
| Elevation & Surfaces | Shadow centric | Surface color + shadow interplay, elevation tokens | Visual hierarchy depends more on color layers |
| Theming API | `$vuetify.theme.themes.light.primary` style overrides | Token-centric: `createTheme`, CSS vars (`--v-theme-*`) | Simpler dynamic runtime adjustments |
| Components | Some monolithic components | More composable primitives (e.g. layout, navigation) | Encourages smaller building blocks |
| Icons | Implicit default sets | Explicit icon aliasing & sets config | Need to register custom / migrated icons |

## 2. Color & Theming Deep Dive

MD3 uses a *source (seed) color* to algorithmically derive tonal ranges (0–100). Vuetify exposes these as CSS variables like `--v-theme-primary` plus tonal variants (e.g. `primary-darken-1` becomes more systematic).

Example MD3 theme creation (Vuetify 3):

```ts
// theme.ts
import { createVuetify } from 'vuetify';
import 'vuetify/styles';

const customLight = {
  dark: false,
  colors: {
    primary: '#4F46E5', // seed
    secondary: '#0EA5E9',
    error: '#DC2626',
    success: '#059669'
  },
  variables: { // optional extra design tokens
    'border-radius-sm': '4px'
  }
};

export const vuetify = createVuetify({
  theme: {
    defaultTheme: 'customLight',
    themes: { customLight },
  }
});
```

Legacy (MD2) override example for comparison:

```ts
// Vuetify 2 (MD2 style)
const vuetify = new Vuetify({
  theme: {
    themes: {
      light: {
        primary: '#4F46E5',
        secondary: '#0EA5E9'
      }
    }
  }
});
```

**Migration tip:** Start by introducing a *parallel theme module* while leaving existing SCSS overrides intact. Then progressively replace hard-coded palette references (e.g. `primary lighten-2`) with semantic usage via CSS vars.

## 3. Typography

MD2 relied on utility classes like `display-1`, `headline`, `subtitle-2`. MD3 encourages a semantic scale (e.g. `h1`, `title-large`, `body-medium`) which Vuetify maps to CSS variables.

Strategy:

1. Inventory current typography classes (grep for `display-`, `headline`, `subtitle`, `overline`).
2. Map each to new semantic target (e.g. `display-1` → `text-h3` depending on visual weight).
3. Create a *temporary mapping layer* (custom classes) to avoid massive template churn in one PR.

Example mapping SCSS:

```scss
/* transitional-typography.scss */
.display-1 { @extend .text-h3; }
.subtitle-2 { @extend .text-subtitle-2; }
```

Then gradually replace usages with native MD3 classes.

## 4. Density & Spacing

MD3 defaults may make app sections feel “looser.” Resist the urge to globally compress; validate real user tasks first. Where tighter density is truly needed (e.g. data grids), *apply local density modifiers* instead of global resets to preserve accessibility.

Checklist:

- Critical viewport benchmarks (above fold) after switch.
- Forms with many fields—review vertical rhythm.
- Icon buttons: confirm `size` and `density` props (Vuetify 3 exposes more explicit sizing props).

## 5. Elevation, Surfaces & Tone

MD3 uses color layering + subtle shadow. Hard-coded `elevation-24` rarely makes sense now.

Refactor approach:

1. Audit components using high elevation values.
2. Replace with lower tokens or rely on surface differentiation (`bg-surface-variant`).
3. Avoid mixing custom large box-shadows with MD3 surfaces—they'll look inconsistent.

## 6. Component API Shifts (Representative)

| Component | MD2 Pattern | MD3 / Vuetify 3 Pattern | Migration Note |
|-----------|-------------|-------------------------|----------------|
| App Layout | `v-app-bar`, `v-navigation-drawer` tightly coupled | More composable layout wrappers + slots | Re-think nested nav patterns |
| Buttons | `v-btn color="primary" depressed` | `v-btn variant="flat\|tonal\|elevated\|outlined\|text"` | Map old style props → new `variant` |
| Icons | Implicit via `v-icon` | Register sets & aliases in Vuetify config | Consolidate icon strategy early |
| Forms | Legacy validation props | Composition API utilities & `v-form` events refined | Refactor progressively; wrap old forms |
| Data Tables | Monolithic | Virtualization & lab features separated | Evaluate feature parity before swapping |

## 7. Progressive Migration Strategy

Avoid a freeze & rewrite. Use *strangler* technique.

| Phase | Goal | Key Actions | Exit Criteria |
|-------|------|------------|---------------|
| 0 Baseline | Measurement | Capture CLS/LCP, bundle size, critical flows | Metrics dashboard exists |
| 1 Dual Theme | Introduce Vuetify 3 in parallel (if separate branch/app shell) | Create MD3 theme, mount a sandbox route | Sandbox route stable |
| 2 Token Layer | Abstract colors/typography behind tokens | Add CSS var fallbacks for MD2 | 80% hard-coded colors removed |
| 3 Component Islands | Migrate leaf components first (buttons, badges) | Provide adapter props layer | No visual regressions in migrated islands |
| 4 Layout & Nav | Rework navigation & app bars | Implement MD3 density/elevation defaults | Navigation UX parity |
| 5 Complex Widgets | Tables, forms, dialogs | Replace or wrap with adapters | Feature parity + a11y test pass |
| 6 Cleanup | Remove transitional shims | Delete mapping SCSS & alias utilities | No deprecated classes remain |

## 8. Code Example: Adapter Wrapper

Create wrappers so templates stay stable while you refactor internals.

```vue
<!-- components/AdaptiveButton.vue -->
<script setup lang="ts">
import { computed } from 'vue';
interface Props { legacyStyle?: 'depressed' | 'text' | 'outlined'; color?: string }
const props = defineProps<Props>();
const variant = computed(() => {
  switch (props.legacyStyle) {
    case 'depressed': return 'flat';
    case 'outlined': return 'outlined';
    case 'text': return 'text';
    default: return 'elevated';
  }
});
</script>
<template>
  <v-btn :color="props.color" :variant="variant"> <slot /> </v-btn>
</template>
```

Usage during migration:

```vue
<AdaptiveButton legacyStyle="depressed" color="primary">Save</AdaptiveButton>
```

Later you remove `legacyStyle` once all calls adopt `variant` directly.

## 9. Accessibility (A11y) Considerations

- Re-test color contrast after dynamic tonal palette adoption (light & dark modes).
- Validate focus rings: MD3 emphasizes visible focus—ensure custom overrides did not suppress them.
- Density reductions may help touch users but verify keyboard navigation order after layout changes.

## 10. Performance Notes

Vuetify 3 (Vite + tree-shaking) can reduce bundle size if you eliminate legacy plugin bloat. Track:

- Initial bundle diff (before/after enabling Vuetify 3 tree-shakable components)
- Hydration time (if SSR)
- Theme switch latency (should be near-instant with CSS vars)

## 11. Risk Radar & Mitigations

| Risk | Impact | Mitigation |
|------|--------|-----------|
| Design Drift | Visual inconsistency across mixed MD2/MD3 zones | Introduce a migration banner in internal environments; track pages migrated |
| Regressions | Subtle layout/breakpoints | Visual regression tests (Percy/Chromatic) on critical flows |
| Churn Fatigue | Developer slowdown | Keep phases <2 weeks; publish migration scoreboard |
| A11y Gaps | Missed contrast / focus | Add automated a11y scan (axe) to CI per PR |
| Scope Creep | Over-refactoring | Time-box: only refactor unrelated code if blocking migration |

## 12. Migration Checklist (Copy/Paste)

```text
[ ] Baseline performance + visual snapshots captured
[ ] MD3 theme scaffolded with source color + dark variant
[ ] Transitional typography mapping layer added
[ ] Color hard-codes replaced by semantic tokens (>=80%)
[ ] Adapter components for buttons & icons
[ ] First leaf component group migrated & validated
[ ] Navigation/layout shells refactored
[ ] Complex widgets (tables/forms) parity achieved
[ ] A11y audit (contrast, focus, landmarks)
[ ] Remove transitional shims / dead classes
[ ] Documentation updated for new tokens & variants
```

## 13. When NOT to Migrate (Yet)

Delay if:

- You depend on MD2-only community components not yet ported.
- Release cadence is mid critical feature cycle (avoid coupling risk).
- Design team has not re-issued updated spacing/typography tokens.

## 14. Summary

Treat MD3 migration as *design token adoption + component API modernization* rather than a visual repaint. Sequence your work so user value continues shipping while the foundation evolves. Start small, measure, iterate.

Happy building—ship incremental value, not migration debt.
