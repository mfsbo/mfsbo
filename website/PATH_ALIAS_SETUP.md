# Path Alias Configuration (@/)

This document explains the `@/` path alias setup for both VitePress and Gulp in this project.

## Overview

The `@/` alias points to the **website root folder** and works in:
- ✅ VitePress content and components
- ✅ Gulp tasks (runtime with ts-node)
- ⚠️ Gulp tasks (VS Code IntelliSense - known limitation with NodeNext)

## Usage Examples

### In VitePress Files
```typescript
// .vitepress/theme/posts.data.ts
import type { Post } from '@/types/post';

// .vitepress/theme/components/PostList.vue
import type { Post } from '@/types/post';
```

### In Gulp Tasks
```typescript
// gulpfile.ts
import type { Post } from '@/types/post';

export const myTask: TaskFunction = (cb) => {
  const post: Post = { /* ... */ };
  // Works at runtime!
  cb();
};
```

## Configuration Files

### 1. TypeScript Config (VitePress)
**File**: `tsconfig.json`

```jsonc
{
  "compilerOptions": {
    "baseUrl": ".",
    "paths": {
      "@/*": ["./*"]
    }
    // ... other options
  }
}
```

### 2. TypeScript Config (Gulp)
**File**: `tsconfig.gulp.json`

```jsonc
{
  "compilerOptions": {
    "module": "NodeNext",
    "moduleResolution": "NodeNext",
    "baseUrl": ".",
    "paths": {
      "@/*": ["./*"]
    }
    // ... other options
  }
}
```

### 3. VitePress/Vite Config
**File**: `.vitepress/config.ts`

```typescript
import { defineConfig } from 'vitepress'
import path from 'path'
import { fileURLToPath } from 'url'

const __dirname = path.dirname(fileURLToPath(import.meta.url))

export default defineConfig({
  vite: {
    resolve: {
      alias: {
        '@': path.resolve(__dirname, '..')
      }
    }
  }
  // ... other config
})
```

### 4. Package.json (ts-node)
**File**: `package.json`

```json
{
  "ts-node": {
    "transpileOnly": true,
    "require": ["tsconfig-paths/register"],
    "compilerOptions": {
      "module": "CommonJS"
    }
  }
}
```

## How It Works

### VitePress (Build & Dev)
1. Vite's resolver uses the `alias` configuration in `.vitepress/config.ts`
2. TypeScript uses `paths` from `tsconfig.json` for type checking
3. Both resolve `@/` to the website root folder
4. ✅ Full IntelliSense and type checking support

### Gulp (ts-node Runtime)
1. Gulp loads `gulpfile.ts` via ts-node
2. ts-node reads `tsconfig.gulp.json` for compiler options
3. `tsconfig-paths/register` (required in package.json) resolves path aliases
4. TypeScript compiles the code with path mappings applied
5. ✅ Runtime execution works perfectly

### VS Code (Gulp Files)
- VS Code uses TypeScript language server with `tsconfig.gulp.json`
- `NodeNext` module resolution has limited support for path mappings in the language server
- ⚠️ You may see red squiggles for `@/` imports in `gulpfile.ts`
- ✅ The code still compiles and runs correctly via ts-node
- This is a known limitation, not an actual error

## Dependencies

Required packages:
```json
{
  "devDependencies": {
    "ts-node": "^10.9.2",
    "tsconfig-paths": "^4.2.0",
    "typescript": "^5.9.2"
  }
}
```

## File Structure

```
website/
├── types/
│   └── post.ts              # Shared Post interface
├── .vitepress/
│   ├── config.ts            # Vite alias config
│   └── theme/
│       ├── posts.data.ts    # Uses @/types/post ✅
│       └── components/
│           ├── PostList.vue      # Uses @/types/post ✅
│           └── RecentPosts.vue   # Uses @/types/post ✅
├── gulpfile.ts              # Uses @/types/post ✅ (runtime) ⚠️ (VS Code)
├── tsconfig.json            # VitePress TypeScript config with @/ alias
├── tsconfig.gulp.json       # Gulp TypeScript config with @/ alias
└── package.json             # ts-node config with tsconfig-paths
```

## Benefits

1. **Cleaner Imports**: No relative path hell (`../../../types/post`)
2. **Refactor-Friendly**: Moving files doesn't break imports
3. **Shared Types**: Both Gulp and VitePress can import from `@/types/`
4. **Consistent**: Same alias pattern across different tools
5. **Maintainable**: Central type definitions

## Troubleshooting

### VitePress: Module not found error
- ✅ Check `.vitepress/config.ts` has the `vite.resolve.alias` configuration
- ✅ Verify `tsconfig.json` has `baseUrl` and `paths` set correctly
- ✅ Restart dev server: `npm run dev`

### Gulp: Import error at runtime
- ✅ Verify `tsconfig-paths` is installed
- ✅ Check `package.json` includes `"require": ["tsconfig-paths/register"]` in ts-node config
- ✅ Confirm `tsconfig.gulp.json` has `paths` configured
- ✅ Run `npx gulp --tasks` to see if it loads without errors

### VS Code: Red squiggles on @/ in gulpfile.ts
- This is expected with `NodeNext` module resolution
- The code works at runtime via ts-node + tsconfig-paths
- You can safely ignore the VS Code error
- Alternative: Use relative imports in gulpfile if the squiggles bother you

### Type not found
- ✅ Ensure the file exists at `types/post.ts`
- ✅ Check the export: `export interface Post { ... }`
- ✅ Verify import syntax: `import type { Post } from '@/types/post'`
- ✅ Reload VS Code window if needed

## Testing

### Test VitePress
```bash
npm run build    # Should build successfully
npm run dev      # Should run without errors
```

### Test Gulp
```bash
npx gulp --tasks    # Should list tasks
npx gulp testAlias  # Should run and print success message
```

Both should work without runtime errors, confirming the `@/` alias is properly configured!
