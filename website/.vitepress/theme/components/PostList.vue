<template>
  <h1>Posts</h1>
  <p>All blog posts from the mfsbo tech blog.</p>
  <div v-if="posts.length === 0" class="empty-state">
    <p>No posts published yet. Check back soon.</p>
  </div>
  <div v-else v-for="year in years" :key="year" class="year-section">
    <h2>{{ year }}</h2>
    <template v-if="shouldGroupByMonth(year)">
      <div v-for="month in monthsForYear(year)" :key="`${year}-${month}`" class="month-section">
        <h3>{{ getMonthName(month) }}</h3>
        <ul>
          <li
            v-for="post in postsByYearAndMonth(year, month)"
            :key="post.url"
            class="post-item"
          >
            <a :href="withBasePath(post.url)">{{ post.title }}</a>
            - <small class="post-date">{{ post.displayDate }}</small>
          </li>
        </ul>
      </div>
    </template>
    <template v-else>
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
    </template>
  </div>
</template>

<script setup lang="ts">
import { data as posts } from '../posts.data';
import type { Post } from '@/types/post';
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

// Check if a year has more than 6 posts
const shouldGroupByMonth = (year: number): boolean => postsByYear(year).length > 6;

// Get unique months for a year (descending order)
const monthsForYear = (year: number): number[] => {
  const yearPosts = postsByYear(year);
  return [...new Set(yearPosts.map(p => p.month))].sort((a, b) => b - a);
};

// Get posts for a specific year and month
const postsByYearAndMonth = (year: number, month: number): Post[] => 
  posts.filter(p => p.year === year && p.month === month);

// Get month name from month number
const getMonthName = (month: number): string => {
  const monthNames = [
    'January', 'February', 'March', 'April', 'May', 'June',
    'July', 'August', 'September', 'October', 'November', 'December'
  ];
  return monthNames[month];
};
</script>

<style scoped>
.year-section {
  margin-bottom: 2em;
}
.month-section {
  margin-bottom: 1em;
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