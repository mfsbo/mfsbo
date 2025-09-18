import { h } from 'vue'
import DefaultTheme from 'vitepress/theme'
import './custom.css'
import ResponsiveImage from './components/ResponsiveImage.vue'

export default {
  extends: DefaultTheme,
  Layout: () => h(DefaultTheme.Layout, null, {}),
  enhanceApp(ctx: { app: any }) { // vitepress provides app (Vue App instance)
    ctx.app.component('ResponsiveImage', ResponsiveImage)
  }
}