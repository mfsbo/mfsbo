<template>
  <h1>Blog Posts</h1>
  <div v-for="year in years" :key="year" class="year-section">
    <h2>{{ year }}</h2>
    <ul>
    <li v-for="post in posts.filter(p => p.year === year)" :key="post.url" class="post-item">
        <a :href="getUrlWithBase(post.url)">{{ post.title }}</a> - <small>{{ formatDate(post.date) }}</small>
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
const format= "DD, MMM"
const formatDate = (date: Date) => {
  return useDateFormat(date,format);
};
// Get unique Year from all posts in an array
const years = [...new Set(posts.map(post => post.year))];
years.sort((a, b) => b - a); // Sort years in descending order
</script>