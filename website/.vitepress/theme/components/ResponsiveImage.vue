<script setup lang="ts">
interface Props {
  /** Cloudinary version segment e.g. v1718059398 */
  version: string
  /** Public ID path after version e.g. github/mfsbo/fogmykndenu9krtbg7oo.png */
  publicId: string
  /** Alt text for accessibility */
  alt: string
  /** Width breakpoints to generate */
  widths?: number[]
  /** Final rendered max width (used for sizes default) */
  maxDisplayWidth?: number
  /** Custom sizes attribute */
  sizes?: string
  /** Loading strategy */
  loading?: 'lazy' | 'eager'
  /** Decoding hint */
  decoding?: 'async' | 'sync' | 'auto'
  /** Explicit class */
  imgClass?: string
  /** Enable low-quality placeholder */
  lqip?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  widths: () => [360, 480, 688, 800],
  maxDisplayWidth: 688,
  sizes: '(max-width: 720px) 92vw, 688px',
  loading: 'lazy',
  decoding: 'async',
  imgClass: 'responsive-image',
  lqip: false
})

const base = 'https://res.cloudinary.com/dfph3xsla/image/upload/'

// Build srcset
const srcset = props.widths
  .sort((a,b)=>a-b)
  .map(w => `${base}f_auto,q_auto,dpr_auto,w_${w}/${props.version}/${props.publicId} ${w}w`)
  .join(', ')

// Chosen default src at largest constrained width
const src = `${base}f_auto,q_auto,dpr_auto,w_${props.maxDisplayWidth}/${props.version}/${props.publicId}`

// Optional LQIP placeholder
const lqipSrc = props.lqip
  ? `${base}f_auto,q_auto:low,dpr_auto,w_40,e_blur:200/${props.version}/${props.publicId}`
  : undefined
</script>

<template>
  <figure :class="['ri-wrapper', imgClass]" style="margin:1rem 0;">
    <img
      :src="lqipSrc || src"
      :data-src="lqipSrc ? src : undefined"
      :srcset="srcset"
      :sizes="sizes"
      :alt="alt"
      :loading="loading"
      :decoding="decoding"
      @load="onLoad"
    />
    <noscript>
      <img :src="src" :alt="alt" />
    </noscript>
  </figure>
</template>

<script lang="ts">
// Lightweight onload swap if LQIP used
function onLoad(e: Event) {
  const img = e.target as HTMLImageElement
  if (img.dataset && img.dataset.src && img.src !== img.dataset.src) {
    // Once the higher-res version is requested via srcset, browser will swap automatically.
    // If still showing placeholder, force swap.
    img.src = img.dataset.src
    img.removeAttribute('data-src')
  }
}
export default {}
</script>

<style scoped>
.ri-wrapper { text-align: center; }
.ri-wrapper img { max-width: 100%; height: auto; display: block; margin: 0 auto; }
</style>
