import type { TaskFunction } from 'gulp';
import type { Post } from '@/types/post';

/**
 * Test Alias task - demonstrates @/ path alias usage in Gulp tasks
 */
export const testAlias: TaskFunction = (cb) => {
  // Demonstrate that we can use the Post type from @/types/post
  const examplePost: Post = {
    url: '/example',
    title: 'Example Post from Modular Gulp Task',
    date: new Date(),
    description: 'Testing @/ alias in modular Gulp tasks',
    category: 'test',
    tags: ['gulp', 'typescript', 'alias', 'modular'],
    year: 2025,
    displayDate: 'Oct 05'
  };
  
  console.log('✅ @/ alias works in modular Gulp tasks!');
  console.log(`Example post: "${examplePost.title}"`);
  console.log(`Tags: ${examplePost.tags.join(', ')}`);
  console.log('Task location: gulp/tasks/testAlias.ts');
  cb();
};

testAlias.description = 'Demonstrates @/ path alias usage with Post type';
