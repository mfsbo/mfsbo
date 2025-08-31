<template>
  <div class="container mx-auto px-4 py-8">
    <header class="mb-8">
      <h1 class="text-4xl font-bold text-gray-900 mb-2">mfsbo</h1>
      <p class="text-xl text-gray-600">Random thoughts on Tech</p>
    </header>

    <section>
      <h2 class="text-2xl font-semibold text-gray-900 mb-6">Recent Posts</h2>
      <div class="space-y-6">
        <article 
          v-for="post in blogPosts" 
          :key="post._path" 
          class="border-b border-gray-200 pb-6"
        >
          <h3 class="text-xl font-semibold mb-2">
            <NuxtLink 
              :to="post._path" 
              class="text-blue-600 hover:text-blue-800 transition-colors"
            >
              {{ post.title }}
            </NuxtLink>
          </h3>
          
          <div class="text-sm text-gray-500 mb-3">
            <time :datetime="post.date">
              {{ formatDate(post.date) }}
            </time>
            <span v-if="post.categories && post.categories.includes('personal')" class="ml-2 px-2 py-1 bg-blue-100 text-blue-800 rounded-full text-xs">
              Personal
            </span>
          </div>
          
          <p v-if="post.description" class="text-gray-700 mb-3">
            {{ post.description }}
          </p>
          
          <NuxtLink 
            :to="post._path" 
            class="text-blue-600 hover:text-blue-800 font-medium text-sm"
          >
            Read more →
          </NuxtLink>
        </article>
      </div>
    </section>
  </div>
</template>

<script setup>
const { data: posts } = await queryContent('/')
  .where({ _type: 'markdown' })
  .only(['_path', 'title', 'description', 'date', 'categories'])
  .sort({ date: -1 })
  .find()

// Filter out the about page and only get posts
const blogPosts = posts.filter(post => 
  post._path !== '/about' && 
  post._path.match(/\/\d{4}\/\d{2}\/\d{2}\//) // posts have date in path
)

function formatDate(dateString) {
  const date = new Date(dateString)
  return date.toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'long',
    day: 'numeric'
  })
}

// Set page meta
useSeoMeta({
  title: 'mfsbo - Random thoughts on Tech',
  description: 'Random thoughts on Tech - Blog by Farrukh Subhani covering development, programming, and technology insights.'
})
</script>