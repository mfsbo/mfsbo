# Development Documentation

This repository contains two separate applications:

1. **Jekyll Website** - Located in `website/` folder, deployed to `/mfsbo` path
2. **Nuxt Application** - Located in `nuxt/` folder, deployed to `/nuxt` path

## Jekyll Website (website/)

### Prerequisites
- Ruby 3.2.3+
- Bundler 2.3.24+

### Development Setup
```bash
# Navigate to website directory
cd website/

# Install dependencies (requires sudo due to system gem permissions)
sudo gem install bundler
sudo bundle install

# Build the site
bundle exec jekyll build

# Serve locally
bundle exec jekyll serve --host 0.0.0.0 --port 4000

# Check site health
bundle exec jekyll doctor
```

### Deployment
- Automatically deployed via GitHub Actions when changes are made to `website/` folder
- Workflow: `.github/workflows/jekyll-gh-pages.yml`
- Live site: `https://mfsbo.github.io/mfsbo/`

## Nuxt Application (nuxt/)

### Prerequisites
- Node.js 20+
- npm 10+

### Development Setup
```bash
# Navigate to nuxt directory
cd nuxt/

# Install dependencies
npm install

# Start development server
npm run dev

# Build for production
npm run build

# Generate static files
npm run generate
```

### Deployment
- Automatically deployed via GitHub Actions when changes are made to `nuxt/` folder
- Workflow: `.github/workflows/nuxt-gh-pages.yml`
- Live site: `https://mfsbo.github.io/mfsbo/nuxt/`

### Configuration
The Nuxt app is configured for static generation with:
- Base URL: `/nuxt/`
- SSR disabled for GitHub Pages compatibility
- Content module enabled for markdown processing

## GitHub Actions Workflows

### Jekyll Deployment
- **File**: `.github/workflows/jekyll-gh-pages.yml`
- **Triggers**: Changes to `website/` folder or workflow file
- **Output**: Deploys to root path `/mfsbo`

### Nuxt Deployment
- **File**: `.github/workflows/nuxt-gh-pages.yml`
- **Triggers**: Changes to `nuxt/` folder or workflow file
- **Output**: Deploys to `/nuxt` path

## Contributing

### Jekyll Website
1. Make changes in `website/` folder
2. Test locally with `bundle exec jekyll serve`
3. Commit and push - deployment is automatic

### Nuxt Application
1. Make changes in `nuxt/` folder
2. Test locally with `npm run dev`
3. Test build with `npm run generate`
4. Commit and push - deployment is automatic

## Project Structure

```
/
├── .github/workflows/
│   ├── jekyll-gh-pages.yml    # Jekyll deployment
│   ├── nuxt-gh-pages.yml      # Nuxt deployment
│   └── main_readme_stats.yml  # Stats automation
├── website/                   # Jekyll site
│   ├── _posts/               # Blog posts
│   ├── _config.yml          # Jekyll config
│   └── Gemfile              # Ruby dependencies
├── nuxt/                     # Nuxt app
│   ├── app/                 # Nuxt app directory
│   ├── content/             # Content files
│   ├── nuxt.config.ts       # Nuxt config
│   └── package.json         # Node dependencies
├── README.md                 # GitHub profile (do not modify)
└── dev.md                   # This documentation
```

## Notes

- README.md serves as the GitHub profile and should not be modified
- Both applications are deployed to the same GitHub Pages site but at different paths
- Each application has its own isolated CI/CD pipeline
- Path-based triggers ensure only relevant changes trigger deployments