<template>
  <h1>Blog Posts</h1>
  <div v-for="year in years" :key="year" class="year-section">
    <h2>{{ year }}</h2>
    <ul>
    <li v-for="post in posts.filter(p => p.year === year)" :key="post.url" class="post-item">
        <a :href="post.url">{{ post.title }}</a>
    </li>
    </ul>
  </div>
</template>

<script setup lang="ts">
import { data as posts } from '../posts.data';
// Get unique Year from all posts in an array
const years = [...new Set(posts.map(post => post.year))];
years.sort((a, b) => b - a); // Sort years in descending order
</script>