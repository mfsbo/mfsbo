<template>
  <div>
    <h2>Recent Posts</h2>
    <ul>
      <li v-for="post in recentPosts" :key="post.url" class="post-item">
        <a :href="withBasePath(post.url)">{{ post.title }}</a>
        <span class="post-date">- {{ post.displayDate }}, {{ post.year }}</span>
      </li>
    </ul>
  </div>
</template>

<script setup lang="ts">
import { data as posts } from '../posts.data';
import type { Post } from '@/types/post';
import { useData, withBase } from 'vitepress';
import { computed, ComputedRef } from 'vue';

// Site base (already includes trailing slash in your config)
const { site } = useData();
const base = site.value.base || '/';

const props = defineProps<{ count?: number }>();
const count = props.count ?? 5;
const withBasePath = (url: string) => withBase ? withBase(url) : base + url.replace(/^\//, '');
const recentPosts: ComputedRef<Post[]> = computed(() => posts.slice(0, count));
</script>

<style scoped>
.post-item {
  margin-bottom: 0.5em;
}
.post-date {
  color: var(--vp-c-text-secondary);
  font-size: 0.95em;
}
</style>

