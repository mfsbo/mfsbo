// @ts-check
// TypeScript config: tsconfig.gulp.json
/**
 * Main Gulpfile - Entry point for all Gulp tasks
 * 
 * This file imports and exports all tasks from the gulp/tasks/ directory.
 * Following Gulp best practices with modular task organization.
 * 
 * Directory structure:
 *   gulpfile.ts          - Main entry point (this file)
 *   gulp/
 *     tasks/
 *       hello.ts          - Hello world task
 *       testAlias.ts      - Demonstrates @/ alias usage
 */

// Import all tasks from gulp/tasks directory
import { hello } from './gulp/tasks/hello.js';
import { testAlias } from './gulp/tasks/testAlias.js';
import { generateMetadata } from './gulp/tasks/generateMetadata.js';

// Export tasks so they're available to Gulp CLI
export { hello, testAlias, generateMetadata };

// Set default task
export default hello;
