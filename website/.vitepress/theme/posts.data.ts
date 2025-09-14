import { createContentLoader} from 'vitepress'

// Define a TypeScript interface for your post data
export interface Post {
  url: string;
  title: string;
  date: Date;
  description: string;
  category: string;
  tags: string[];
  year: number;
}

declare const data: Post[];
export { data };


export default createContentLoader(["./posts/**/2*.md"], {
  // Add a transform function to restructure the data
  transform(raw) {
    return raw
      .map(({ url, frontmatter }): Post => ({
        url,
        title: frontmatter.title,
        date: new Date(frontmatter.date),
        description: frontmatter.description,
        category: frontmatter.category,
        tags: frontmatter.tags || [],
        year: new Date(frontmatter.date).getFullYear(),
      }))
      .sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime());
  }
})