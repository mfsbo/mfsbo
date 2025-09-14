<template>
  <h1>Posts</h1>
  <p>All blog posts from the mfsbo tech blog.</p>
  <div v-for="year in years" :key="year" class="year-section">
    <h2>{{ year }}</h2>
    <ul>
    <li v-for="post in posts.filter(p => p.year === year)" :key="post.url" class="post-item">
        <a :href="getUrlWithBase(post.url)">{{ post.title }}</a> - <small class="post-date">{{ formatDate(post.date) }}</small>
    </li>
    </ul>
  </div>
</template>

<script setup lang="ts">
import { data as posts } from '../posts.data';
import { useData } from 'vitepress';
import { useDateFormat} from '@vueuse/core';
const { site } = useData();
const base = site.value.base || '/mfsbo/';
const getUrlWithBase = (url: string) => base + url.replace(/^\//, '');
// function to format date to given format
const format= "MMM DD"
const formatDate = (date: Date) => {
  return useDateFormat(date,format);
};
// Get unique Year from all posts in an array
const years = [...new Set(posts.map(post => post.year))];
years.sort((a, b) => b - a); // Sort years in descending order
</script>

<style scoped>
.year-section {
  margin-bottom: 2em;
}
.post-item {
  margin-bottom: 0.5em;
}
/* Different colour of date on dark and light mode */
.post-date {
  color: var(--vp-c-text-secondary);
}
</style>