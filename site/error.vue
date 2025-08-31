<template>
  <div class="min-h-screen flex items-center justify-center bg-gray-50">
    <div class="text-center">
      <h1 class="text-6xl font-bold text-gray-900">404</h1>
      <p class="mt-4 text-xl text-gray-600">Page not found</p>
      <p class="mt-2 text-gray-500">Sorry, we couldn't find the page you're looking for.</p>
      <div class="mt-6">
        <NuxtLink 
          to="/" 
          class="inline-flex items-center px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors"
        >
          Go back home
        </NuxtLink>
      </div>
    </div>
  </div>
</template>

<script setup>
useSeoMeta({
  title: '404 - Page Not Found | mfsbo',
  description: 'The page you are looking for could not be found.'
})
</script>