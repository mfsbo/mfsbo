# TypeScript Configuration Structure

This project uses **separate TypeScript configurations** to avoid conflicts between different build tools and provide better IDE support.

## Configuration Files

### `tsconfig.json` (VitePress)

- **Purpose**: Main TypeScript configuration for VitePress website content
- **Module System**: ESNext modules
- **Target**: ESNext (modern browsers)
- **Includes**: Website content files (`website/**/*.ts`, `.vitepress/**/*.ts`)
- **Excludes**: `gulpfile.ts` and build artifacts

### `tsconfig.gulp.json` (Gulp Build Tasks)

- **Purpose**: Dedicated TypeScript configuration for Gulp build tasks
- **Module System**: NodeNext (Node.js ESM/CommonJS interop)
- **Target**: ES2022 (Node.js runtime)
- **Includes**: Only `gulpfile.ts`
- **Types**: `node` and `gulp` type definitions

## Why Separate Configs?

1. **Different Runtime Environments**
   - VitePress runs in browsers (needs ESNext, JSX, DOM types)
   - Gulp runs in Node.js (needs Node APIs, CommonJS interop)

2. **Different Module Systems**
   - VitePress uses ESNext modules for optimal bundling
   - Gulp uses NodeNext for ts-node compatibility with `"type": "module"`

3. **Better IDE Support**
   - VS Code can provide context-appropriate IntelliSense
   - Type checking is specific to each environment
   - No conflicting type definitions

4. **Cleaner Type Definitions**
   - VitePress gets `vitepress/client` types
   - Gulp gets `gulp` and `node` types
   - No type pollution between environments

## How It Works

### For Gulp (ts-node)

When you run `npx gulp`, the process is:

1. Gulp CLI detects `gulpfile.ts`
2. Loads `ts-node/register` to compile TypeScript
3. ts-node reads configuration from `package.json` (`ts-node` section)
4. TypeScript compiles using `tsconfig.gulp.json` settings
5. Compiled code runs in Node.js

### For VitePress

When you run `npm run dev/build`:

1. VitePress starts its build process
2. Reads `tsconfig.json` for TypeScript configuration
3. Processes `.vitepress` config and content files
4. Bundles for browser with Vite

### For VS Code

- The `.vscode/settings.json` configures the TypeScript language server
- VS Code automatically detects which `tsconfig.json` applies to each file
- `gulpfile.ts` uses `tsconfig.gulp.json` (via comment directive)
- Other files use `tsconfig.json`

## Running Gulp Tasks

```bash
# List all available tasks
npx gulp --tasks

# Run the hello task
npx gulp hello

# Run the default task
npx gulp
```

## Development Workflow

1. **VitePress Development**

   ```bash
   npm run dev      # Start dev server
   npm run build    # Build for production
   npm run preview  # Preview production build
   ```

2. **Gulp Tasks**

   ```bash
   npx gulp --tasks  # See available tasks
   npx gulp <task>   # Run specific task
   ```

## Adding New Gulp Tasks

Edit `gulpfile.ts` and export new task functions:

```typescript
export const myTask: TaskFunction = (cb) => {
  // Your task code here
  cb();
};

// Or use async/await
export async function asyncTask() {
  // Async task code
}
```

## Troubleshooting

### Gulp TypeScript Errors

- Check `tsconfig.gulp.json` includes `gulpfile.ts`
- Ensure `@types/node` and `@types/gulp` are installed
- Verify `ts-node` is in devDependencies

### VS Code IntelliSense Issues

- Reload VS Code window (Cmd/Ctrl + Shift + P → "Reload Window")
- Check `.vscode/settings.json` points to workspace TypeScript
- Ensure file is not excluded in both tsconfigs

### Module Resolution Errors

- Check `package.json` has `"type": "module"`
- Verify `tsconfig.gulp.json` uses `"module": "NodeNext"`
- Ensure imports use `.js` extension for local modules (if needed)
