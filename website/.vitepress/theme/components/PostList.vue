<template>
  <h1>Posts</h1>
  <p>All blog posts from the mfsbo tech blog.</p>
  <div v-if="posts.length === 0" class="empty-state">
    <p>No posts published yet. Check back soon.</p>
  </div>
  <div v-else v-for="year in years" :key="year" class="year-section">
    <h2>{{ year }}</h2>
    <ul>
      <li
        v-for="post in postsByYear(year)"
        :key="post.url"
        class="post-item"
      >
        <a :href="withBasePath(post.url)">{{ post.title }}</a>
        - <small class="post-date">{{ post.displayDate }}</small>
      </li>
    </ul>
  </div>
</template>

<script setup lang="ts">
import { data as posts, Post } from '../posts.data';
import { useData, withBase } from 'vitepress';

// Site base (already includes trailing slash in your config)
const { site } = useData();
const base = site.value.base || '/';

// Utility for base-safe path building (fallback if withBase changes in API)
const withBasePath = (url: string) => withBase ? withBase(url) : base + url.replace(/^\//, '');

// Years list derived once (descending)
const years = [...new Set(posts.map(p => p.year))].sort((a, b) => b - a);

// Memoized filter helper (simple enough—inline function)
const postsByYear = (year: number): Post[] => posts.filter(p => p.year === year);
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
.empty-state {
  padding: 1em 0;
  font-style: italic;
  color: var(--vp-c-text-2);
}
</style>