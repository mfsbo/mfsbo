<template>
  <div class="min-h-screen bg-white">
    <nav class="bg-white border-b border-gray-200">
      <div class="container mx-auto px-4">
        <div class="flex justify-between items-center h-16">
          <NuxtLink to="/" class="text-xl font-bold text-gray-900">
            mfsbo
          </NuxtLink>
          <div class="space-x-4">
            <NuxtLink to="/" class="text-gray-700 hover:text-gray-900">
              Home
            </NuxtLink>
            <NuxtLink to="/about" class="text-gray-700 hover:text-gray-900">
              About
            </NuxtLink>
          </div>
        </div>
      </div>
    </nav>

    <main>
      <slot />
    </main>

    <footer class="bg-gray-50 border-t border-gray-200 mt-16">
      <div class="container mx-auto px-4 py-8">
        <div class="text-center text-gray-600">
          <p>&copy; {{ new Date().getFullYear() }} mfsbo. All rights reserved.</p>
          <div class="mt-4 space-x-4">
            <a href="https://github.com/mfsbo" target="_blank" rel="noopener" class="text-blue-600 hover:text-blue-800">
              GitHub
            </a>
            <a href="https://stackoverflow.com/users/923695/farrukh-subhani" target="_blank" rel="noopener" class="text-blue-600 hover:text-blue-800">
              Stack Overflow
            </a>
          </div>
        </div>
      </div>
    </footer>
  </div>
</template>

<script setup>
// Default layout for all pages
</script>