import { defineConfig } from 'vitepress'

export default defineConfig({
  title: 'mfsbo',
  description: 'Random thoughts on Tech',
  base: '/mfsbo/',
  
  head: [
    ['link', { rel: 'icon', type: 'image/x-icon', href: '/mfsbo/favicon.ico' }],
    ['link', { rel: 'icon', type: 'image/png', sizes: '32x32', href: '/mfsbo/favicon-32x32.png' }],
    ['link', { rel: 'icon', type: 'image/png', sizes: '16x16', href: '/mfsbo/favicon-16x16.png' }],
    ['link', { rel: 'apple-touch-icon', sizes: '180x180', href: '/mfsbo/apple-touch-icon.png' }],
    ['link', { rel: 'manifest', href: '/mfsbo/site.webmanifest' }],
    ['meta', { name: 'theme-color', content: '#ffffff' }]
  ],

  themeConfig: {
    nav: [
      { text: 'Home', link: '/' },
      { text: 'About', link: '/about' }
    ],

    socialLinks: [
      { icon: 'github', link: 'https://github.com/mfsbo' }
    ],

    footer: {
      message: 'Random thoughts on Tech',
      copyright: 'Copyright © 2024 mfsbo'
    }
  },

  sitemap: {
    hostname: 'https://mfsbo.github.io/mfsbo/'
  }
})