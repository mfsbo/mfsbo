import { createContentLoader } from 'vitepress'
import type { Post } from '@/types/post'

declare const data: Post[];
export { data };

const displayDateFormatter = new Intl.DateTimeFormat('en-US', { month: 'short', day: '2-digit' });

export default createContentLoader(["./posts/**/2*.md"], {
  transform(raw): Post[] {
    return raw
      .map(({ url, frontmatter }): Post => {
        const dateObj = new Date(frontmatter.date);
        return {
          url,
          title: frontmatter.title,
          date: dateObj,
            // Provide fallback empty strings for optional frontmatter fields if they are undefined
          description: frontmatter.description ?? '',
          category: frontmatter.category ?? '',
          tags: frontmatter.tags || [],
          year: dateObj.getFullYear(),
          month: dateObj.getMonth(),
          displayDate: displayDateFormatter.format(dateObj)
        };
      })
      .sort((a, b) => b.date.getTime() - a.date.getTime());
  }
})