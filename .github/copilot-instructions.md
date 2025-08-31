# mfsbo Personal Portfolio & Blog

Personal portfolio and blog built with Jekyll 4.2.2 and Nuxt 4, deployed to GitHub Pages. Contains development articles, coding stats integration via WakaTime, and professional background information.

**Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.**

## Working Effectively

### Jekyll Bootstrap and Build (website/)
**NEVER CANCEL any command - all operations complete quickly. Use 5+ minute timeouts as safety margin.**

- Install dependencies: `sudo gem install bundler` (requires sudo due to system gem permissions)
- Navigate to website directory: `cd website/`
- Install Jekyll dependencies: `sudo bundle install` - takes ~0.3 seconds. NEVER CANCEL. Set timeout to 5+ minutes.
- Build the site: `bundle exec jekyll build` - takes ~0.3 seconds. NEVER CANCEL. Set timeout to 2+ minutes.
- Check site health: `bundle exec jekyll doctor` - takes ~0.3 seconds. Validates all content and configuration.

### Jekyll Development Server
- Serve locally: `bundle exec jekyll serve --host 0.0.0.0 --port 4000` - starts in ~0.3 seconds. NEVER CANCEL.
- Site accessible at: `http://localhost:4000/`
- Auto-regeneration enabled - changes rebuild automatically
- Stop server with Ctrl+C

### Nuxt Bootstrap and Build (nuxt/)
**NEVER CANCEL any command - all operations complete quickly. Use 5+ minute timeouts as safety margin.**

- Navigate to nuxt directory: `cd nuxt/`
- Install dependencies: `npm install` - takes ~30 seconds. NEVER CANCEL. Set timeout to 5+ minutes.
- Build for development: `npm run dev` - starts in ~3 seconds. NEVER CANCEL. Set timeout to 2+ minutes.
- Generate static files: `npm run generate` - takes ~10 seconds. NEVER CANCEL. Set timeout to 2+ minutes.

### Nuxt Development Server
- Serve locally: `npm run dev` - starts in ~3 seconds on port 3000. NEVER CANCEL.
- Site accessible at: `http://localhost:3000/nuxt/`
- Hot reload enabled - changes rebuild automatically
- Stop server with Ctrl+C

### Content Creation and Validation
- Jekyll blog posts: Create in `website/_posts/` with format: `YYYY-MM-DD-title.md`
- Nuxt content: Create in `nuxt/content/` directory as `.md` files
- **CRITICAL**: Use past dates only for Jekyll posts - Jekyll skips future-dated posts by default
- Jekyll required frontmatter: `title`, `date`, `layout: default`
- Nuxt content supports frontmatter and automatic routing

### Manual Validation Scenarios
**ALWAYS perform these validation steps after making content changes:**

1. **Jekyll Content Validation Workflow**:
   - Run `bundle exec jekyll build` and verify no errors
   - Run `bundle exec jekyll doctor` and verify "Everything looks fine"
   - Start server with `bundle exec jekyll serve --host 0.0.0.0 --port 4000`
   - Test homepage: `curl -s -I http://localhost:4000/ | head -5`
   - For new posts: Access URL and verify title appears in HTML
   - Stop server when done

2. **Nuxt Content Validation Workflow**:
   - Run `npm run generate` and verify no errors
   - Start server with `npm run dev`
   - Test homepage: `curl -s -I http://localhost:3000/nuxt/ | head -5`
   - For new content: Access URL and verify content appears
   - Stop server when done

3. **Blog Post Testing (Jekyll)**:
   - After creating new post in `_posts/`, always build and serve
   - Check generated URL follows pattern: `/category/YYYY/MM/DD/post-title.html`
   - Verify frontmatter renders correctly in browser/curl output
   - Confirm post appears on homepage listing

## Repository Structure

### Key Directories
```
/home/runner/work/mfsbo/mfsbo/
├── .github/workflows/          # GitHub Actions for deployment and stats
│   ├── jekyll-gh-pages.yml    # Jekyll deployment (website/ changes)
│   ├── nuxt-gh-pages.yml      # Nuxt deployment (nuxt/ changes)
│   └── main_readme_stats.yml  # WakaTime stats automation
├── website/                    # Main Jekyll site
│   ├── _posts/                # Blog posts (7 current posts)
│   ├── _config.yml           # Jekyll configuration
│   ├── Gemfile               # Ruby dependencies
│   └── _site/                # Generated site (after build)
├── nuxt/                      # Nuxt TypeScript application
│   ├── app/                  # App directory
│   ├── content/              # Content files
│   ├── nuxt.config.ts        # Nuxt configuration
│   ├── package.json          # Node.js dependencies
│   └── .output/              # Generated site (after build)
├── charts/                    # GitHub stats bar chart
├── dev.md                    # Development documentation
└── README.md                 # Profile README with stats integration
```

### Important Files
- `website/_config.yml`: Jekyll configuration, URL: "https://mfsbo.github.io/mfsbo"
- `website/Gemfile`: Jekyll dependencies - Jekyll 4.2.2, Minima theme, plugins
- `website/about.md`: Personal/professional background
- `nuxt/nuxt.config.ts`: Nuxt configuration with base URL "/nuxt/" and static generation
- `nuxt/package.json`: Nuxt dependencies - Nuxt 4, Content module
- `.github/workflows/jekyll-gh-pages.yml`: Jekyll GitHub Pages deployment (website/ trigger)
- `.github/workflows/nuxt-gh-pages.yml`: Nuxt GitHub Pages deployment (nuxt/ trigger)
- `.github/workflows/main_readme_stats.yml`: WakaTime stats automation
- `dev.md`: Development documentation for both applications

## Technology Stack
- **Jekyll 4.2.2** - Static site generator (website/)
- **Ruby 3.2.3** with Bundler 2.3.24+ for dependency management  
- **Minima theme** - Clean, responsive Jekyll theme
- **Nuxt 4** - Vue.js framework with TypeScript support (nuxt/)
- **Node.js 20+** with npm 10+ for Nuxt development
- **@nuxt/content** - Content management for Nuxt
- **GitHub Pages** - Hosting via Actions workflows (both apps)
- **WakaTime** - Coding time tracking integration

## Build Process Details
### Jekyll (website/)
- **Bundle install**: ~0.3 seconds, requires sudo permissions
- **Jekyll build**: ~0.3 seconds, generates static files to `_site/`
- **Jekyll serve**: ~0.3 seconds to start, serves on port 4000
- **Jekyll doctor**: ~0.3 seconds, validates content and config

### Nuxt (nuxt/)
- **npm install**: ~30 seconds, installs Node.js dependencies
- **npm run dev**: ~3 seconds to start, serves on port 3000 at `/nuxt/`
- **npm run generate**: ~10 seconds, generates static files to `.output/public/`
- **Static generation**: Configured for GitHub Pages with base URL `/nuxt/`

## CI/CD and Deployment
- **GitHub Actions**: Auto-deploys both applications via separate workflows
  - Jekyll: Deploys to `https://mfsbo.github.io/mfsbo/` on `website/` changes
  - Nuxt: Deploys to `https://mfsbo.github.io/mfsbo/nuxt/` on `nuxt/` changes
- **Path-based triggers**: Workflows only run when relevant folders change
- **WakaTime**: Daily stats update at midnight IST via GitHub Actions
- **No traditional tests**: Jekyll doctor and successful builds serve as validation

## Common Tasks

### Adding a Jekyll Blog Post (website/)
1. Create file: `website/_posts/YYYY-MM-DD-title.md`
2. Add frontmatter:
   ```yaml
   ---
   title: Your Post Title
   description: "Brief description"
   date: YYYY-MM-DDTHH:MM:SS.000Z
   tags: [tag1, tag2]
   categories: [category]
   type: post
   layout: default
   ---
   ```
3. Write content in Markdown
4. Build: `bundle exec jekyll build`
5. Test: `bundle exec jekyll serve` and verify at `http://localhost:4000/`

### Adding Nuxt Content (nuxt/)
1. Create file: `nuxt/content/your-content.md`
2. Add frontmatter (optional):
   ```yaml
   ---
   title: Your Content Title
   description: "Brief description"
   ---
   ```
3. Write content in Markdown
4. Generate: `npm run generate`
5. Test: `npm run dev` and verify at `http://localhost:3000/nuxt/`

### Working with Nuxt Development
1. Navigate: `cd nuxt/`
2. Install: `npm install`
3. Develop: `npm run dev` (serves at http://localhost:3000/nuxt/)
4. Build: `npm run generate` (for production static files)
5. Preview: Check `.output/public/` for generated files

### Troubleshooting
#### Jekyll (website/)
- **Permission errors**: Use `sudo` for gem operations due to system gem directory permissions
- **Bundler version warnings**: Safe to ignore - functionality works correctly
- **Future date posts**: Jekyll skips posts with future dates - use past dates only
- **Server not accessible**: Ensure `--host 0.0.0.0` flag is used for external access

#### Nuxt (nuxt/)
- **Module installation**: If prompted for better-sqlite3 or other modules, select "Yes"
- **Build errors**: Run `npm install` to ensure all dependencies are installed
- **Base URL issues**: Ensure nuxt.config.ts has `app: { baseURL: '/nuxt/' }`
- **Static generation**: Use `npm run generate` for GitHub Pages deployment
- **Development server**: Runs on port 3000 with auto-reload enabled

### Frequently Used Commands Output
```bash
# Repository root listing
$ ls -la
.frontmatter  .github  .jekyll-cache  README.md  _site  charts  nuxt  website  dev.md

# Website directory contents  
$ ls -la website/
.gitignore  404.html  Gemfile  _config.yml  _posts  about.md  assets  index.markdown

# Nuxt directory contents
$ ls -la nuxt/
.gitignore  README.md  app  content  content.config.ts  node_modules  nuxt.config.ts  package.json  public  tsconfig.json

# Blog posts count
$ find website/_posts -name "*.md" | wc -l
7

# Ruby environment
$ ruby --version
ruby 3.2.3 (2024-01-18 revision 52bb2ac0a6) [x86_64-linux-gnu]

# Node.js environment  
$ node --version
v20.19.4

# Jekyll version check
$ bundle exec jekyll --version  
jekyll 4.2.2

# Nuxt info
$ cd nuxt && npx nuxi info
Nuxt 4.0.3 with Nitro 2.12.4
```

## Critical Reminders
- **NEVER CANCEL builds or serves** - all operations complete in seconds
- **Always use sudo** for initial gem installation and bundle operations (Jekyll)
- **Test all content changes** by building and serving locally (both apps)
- **Use past dates only** for Jekyll blog posts to avoid Jekyll skipping them
- **Always run jekyll doctor** after Jekyll content changes to validate site health
- **Use npm run generate** for Nuxt static site generation for GitHub Pages
- **Path-based deployments**: Only relevant workflows run based on folder changes
- **Two separate applications**: Jekyll at `/mfsbo` and Nuxt at `/nuxt` paths