# mfsbo Personal Portfolio & Blog

Personal portfolio and blog built with Jekyll 4.2.2, deployed to GitHub Pages. Contains development articles, coding stats integration via WakaTime, and professional background information.

**Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.**

## Working Effectively

### Bootstrap and Build
**NEVER CANCEL any command - all operations complete quickly. Use 5+ minute timeouts as safety margin.**

- Install dependencies: `sudo gem install bundler` (requires sudo due to system gem permissions)
- Navigate to website directory: `cd website/`
- Install Jekyll dependencies: `sudo bundle install` - takes ~0.3 seconds. NEVER CANCEL. Set timeout to 5+ minutes.
- Build the site: `bundle exec jekyll build` - takes ~0.3 seconds. NEVER CANCEL. Set timeout to 2+ minutes.
- Check site health: `bundle exec jekyll doctor` - takes ~0.3 seconds. Validates all content and configuration.

### Development Server
- Serve locally: `bundle exec jekyll serve --host 0.0.0.0 --port 4000` - starts in ~0.3 seconds. NEVER CANCEL.
- Site accessible at: `http://localhost:4000/`
- Auto-regeneration enabled - changes rebuild automatically
- Stop server with Ctrl+C

### Content Creation and Validation
- Blog posts: Create in `website/_posts/` with format: `YYYY-MM-DD-title.md`
- **CRITICAL**: Use past dates only - Jekyll skips future-dated posts by default
- Required frontmatter: `title`, `date`, `layout: default`
- Categories create URL paths: `categories: [validation]` → `/validation/YYYY/MM/DD/title.html`

### Manual Validation Scenarios
**ALWAYS perform these validation steps after making content changes:**

1. **Content Validation Workflow**:
   - Run `bundle exec jekyll build` and verify no errors
   - Run `bundle exec jekyll doctor` and verify "Everything looks fine"
   - Start server with `bundle exec jekyll serve --host 0.0.0.0 --port 4000`
   - Test homepage: `curl -s -I http://localhost:4000/ | head -5`
   - For new posts: Access URL and verify title appears in HTML
   - Stop server when done

2. **Blog Post Testing**:
   - After creating new post in `_posts/`, always build and serve
   - Check generated URL follows pattern: `/category/YYYY/MM/DD/post-title.html`
   - Verify frontmatter renders correctly in browser/curl output
   - Confirm post appears on homepage listing

## Repository Structure

### Key Directories
```
/home/runner/work/mfsbo/mfsbo/
├── .github/workflows/          # GitHub Actions for deployment and stats
├── website/                    # Main Jekyll site
│   ├── _posts/                # Blog posts (7 current posts)
│   ├── _config.yml           # Jekyll configuration
│   ├── Gemfile               # Ruby dependencies
│   └── _site/                # Generated site (after build)
├── charts/                    # GitHub stats bar chart
└── README.md                 # Profile README with stats integration
```

### Important Files
- `website/_config.yml`: Site configuration, URL: "https://mfsbo.github.io/mfsbo"
- `website/Gemfile`: Dependencies - Jekyll 4.2.2, Minima theme, plugins
- `website/about.md`: Personal/professional background
- `.github/workflows/jekyll-gh-pages.yml`: GitHub Pages deployment
- `.github/workflows/main_readme_stats.yml`: WakaTime stats automation

## Technology Stack
- **Jekyll 4.2.2** - Static site generator
- **Ruby 3.2.3** with Bundler 2.3.24+ for dependency management  
- **Minima theme** - Clean, responsive Jekyll theme
- **GitHub Pages** - Hosting via Actions workflow
- **WakaTime** - Coding time tracking integration

## Build Process Details
- **Bundle install**: ~0.3 seconds, requires sudo permissions
- **Jekyll build**: ~0.3 seconds, generates static files to `_site/`
- **Jekyll serve**: ~0.3 seconds to start, serves on port 4000
- **Jekyll doctor**: ~0.3 seconds, validates content and config

## CI/CD and Deployment
- **GitHub Actions**: Auto-deploys to `https://mfsbo.github.io/mfsbo/` on push to main
- **WakaTime**: Daily stats update at midnight IST via GitHub Actions
- **No traditional tests**: Jekyll doctor and successful build serve as validation

## Common Tasks

### Adding a Blog Post
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

### Troubleshooting
- **Permission errors**: Use `sudo` for gem operations due to system gem directory permissions
- **Bundler version warnings**: Safe to ignore - functionality works correctly
- **Future date posts**: Jekyll skips posts with future dates - use past dates only
- **Server not accessible**: Ensure `--host 0.0.0.0` flag is used for external access

### Frequently Used Commands Output
```bash
# Repository root listing
$ ls -la
.frontmatter  .github  .jekyll-cache  README.md  _site  charts  website

# Website directory contents  
$ ls -la website/
.gitignore  404.html  Gemfile  _config.yml  _posts  about.md  assets  index.markdown

# Blog posts count
$ find website/_posts -name "*.md" | wc -l
7

# Ruby environment
$ ruby --version
ruby 3.2.3 (2024-01-18 revision 52bb2ac0a6) [x86_64-linux-gnu]

# Jekyll version check
$ bundle exec jekyll --version  
jekyll 4.2.2
```

## Critical Reminders
- **NEVER CANCEL builds or serves** - all operations complete in seconds
- **Always use sudo** for initial gem installation and bundle operations
- **Test all content changes** by building and serving locally
- **Use past dates only** for blog posts to avoid Jekyll skipping them
- **Always run jekyll doctor** after content changes to validate site health