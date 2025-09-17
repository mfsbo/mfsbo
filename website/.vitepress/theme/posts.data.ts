import { createContentLoader } from 'vitepress'

export interface Post {
  url: string;
  title: string;
  date: Date;              
  description: string;
  category: string;
  tags: string[];
  year: number;            
  displayDate: string;     
}

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
          displayDate: displayDateFormatter.format(dateObj)
        };
      })
      .sort((a, b) => b.date.getTime() - a.date.getTime());
  }
})