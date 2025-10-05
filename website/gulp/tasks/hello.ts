import type { TaskFunction } from 'gulp';

/**
 * Hello World task - demonstrates basic Gulp task with TypeScript
 */
export const hello: TaskFunction = (cb) => {
  console.log('Hello World from Gulp with TypeScript!');
  console.log('Task location: gulp/tasks/hello.ts');
  cb();
};

// Task metadata for better documentation
hello.description = 'Prints Hello World message';
