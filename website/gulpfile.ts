// @ts-check
// TypeScript config: tsconfig.gulp.json
import gulp from 'gulp';
import type { TaskFunction } from 'gulp';
import type { Post } from '@/types/post';

// Hello World task (properly typed)
export const hello: TaskFunction = (cb) => {
  console.log('Hello World from Gulp with TypeScript!');
  cb();
};

// Example task demonstrating @/ alias usage
export const testAlias: TaskFunction = (cb) => {
  // Demonstrate that we can use the Post type from @/types/post
  const examplePost: Post = {
    url: '/example',
    title: 'Example Post',
    date: new Date(),
    description: 'Testing @/ alias in Gulp',
    category: 'test',
    tags: ['gulp', 'typescript'],
    year: 2025,
    displayDate: 'Oct 05'
  };
  
  console.log('✅ @/ alias works in Gulp!');
  console.log('Example post:', examplePost.title);
  cb();
};

// Default task
export default hello;
