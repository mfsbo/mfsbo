// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2025-07-15',
  devtools: { enabled: true },
  
  modules: [
    '@nuxt/content',
    '@nuxtjs/tailwindcss',
    '@nuxt/image',
    '@nuxtjs/sitemap',
    '@nuxtjs/robots'
  ],

  app: {
    baseURL: process.env.BASE_URL || '/mfsbo/',
    head: {
      title: 'mfsbo',
      meta: [
        { charset: 'utf-8' },
        { name: 'viewport', content: 'width=device-width, initial-scale=1' },
        { name: 'description', content: 'Random thoughts on Tech' }
      ]
    }
  },

  site: {
    url: process.env.NUXT_PUBLIC_SITE_URL || 'https://mfsbo.github.io',
    name: 'mfsbo'
  },

  content: {
    documentDriven: true,
    markdown: {
      anchorLinks: false
    }
  },

  nitro: {
    prerender: {
      crawlLinks: true,
      routes: [
        '/sitemap.xml',
        '/about',
        '/2023/03/07/syntax-podcast-github-copilot-and-frontmatter',
        '/2024/04/05/syntax-podcast-talking-about-ui,-ai-and-fitness',
        '/2024/05/22/scott-hanselman\'s-vs-code-setup-from-recent-c#-dev-kit-video',
        '/2024/06/10/5-years-of-development-life',
        '/2024/11/16/electric-vehicle-charging-future-of-autonomous-travel',
        '/2024/12/23/development-in-year-2024',
        '/personal/2022/10/19/welcome-to-jekyll',
        '/personal/2023/01/13/Style-Guide-Meme'
      ]
    }
  },

  robots: {
    allow: ['/'],
    robotsEnabledValue: false  // Disable robots.txt generation since we have our own
  }
})
