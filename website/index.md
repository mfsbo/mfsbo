---
layout: home

hero:
  name: mfsbo
  text: Random thoughts on Tech
  tagline: A blog about web development, programming, and technology
  actions:
    - theme: brand
      text: View Posts
      link: /posts/
    - theme: alt
      text: About Me
      link: /about

features:
  - title: Web Development
    details: Insights on modern web technologies, frameworks, and best practices
  - title: Programming
    details: Code examples, tutorials, and thoughts on software development
  - title: Technology
    details: Discussion on tech trends, tools, and industry developments
---
<script setup>
import RecentPosts from './.vitepress/theme/components/RecentPosts.vue';
</script>
<RecentPosts :count="5" />

[View all posts →](/posts/)
