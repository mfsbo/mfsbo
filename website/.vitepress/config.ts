import { defineConfig } from 'vitepress'
import path from 'path'
import { fileURLToPath } from 'url'
import { siteMetadata } from './theme/metadata'

const __dirname = path.dirname(fileURLToPath(import.meta.url))

export default defineConfig({
  title: 'mfsbo',
  description: 'Random thoughts on Tech',
  base: '/mfsbo/',
  
  vite: {
    resolve: {
      alias: {
        '@': path.resolve(__dirname, '..')
      }
    }
  },
  
  head: [
    ['link', { rel: 'icon', type: 'image/x-icon', href: '/mfsbo/favicon.ico' }],
    ['link', { rel: 'icon', type: 'image/png', sizes: '32x32', href: '/mfsbo/favicon-32x32.png' }],
    ['link', { rel: 'icon', type: 'image/png', sizes: '16x16', href: '/mfsbo/favicon-16x16.png' }],
    ['link', { rel: 'apple-touch-icon', sizes: '180x180', href: '/mfsbo/apple-touch-icon.png' }],
    ['link', { rel: 'manifest', href: '/mfsbo/site.webmanifest' }],
    ['meta', { name: 'theme-color', content: '#ffffff' }],
    ['script', {},`(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],
j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);
})(window,document,'script','dataLayer','GTM-KZVX2HFD');`],
  ],

  themeConfig: {
    nav: [
      { text: 'Home', link: '/' },
      { text: 'Posts', link: '/posts/' },
      { text: 'About', link: '/about' },
      { text: 'CV', link: '/cv' }
    ],

    socialLinks: [
      { icon: 'github', link: 'https://github.com/mfsbo' }
    ],

    footer: {
      message: 'Random thoughts on Tech',
      copyright: 'Copyright © ' + siteMetadata.app_info_name + ' v' + siteMetadata.app_info_version+" (" + siteMetadata.app_info_commitShortHash + ") " + siteMetadata.app_info_released_on.toUTCString()
    },

    // Improve mobile menu accessibility
    sidebarMenuLabel: 'Menu',
    returnToTopLabel: 'Return to top',
    darkModeSwitchLabel: 'Appearance',
    lightModeSwitchTitle: 'Switch to light theme',
    darkModeSwitchTitle: 'Switch to dark theme'
  },

  sitemap: {
    hostname: 'https://mfsbo.github.io/mfsbo/'
  }
})