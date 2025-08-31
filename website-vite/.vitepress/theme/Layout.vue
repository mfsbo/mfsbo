<template>
  <div class="layout">
    <header class="header">
      <div class="header-content">
        <div class="site-title">
          <a href="/" class="site-title-link">mfsbo</a>
        </div>
        <nav class="nav">
          <a href="/" class="nav-link">Home</a>
          <a href="/about" class="nav-link">About</a>
          <a href="https://github.com/mfsbo" target="_blank" class="nav-link">GitHub</a>
        </nav>
      </div>
    </header>
    
    <main class="main">
      <Content />
    </main>
    
    <footer class="footer">
      <div class="footer-content">
        <p>Random thoughts on Tech</p>
        <p>© 2024 mfsbo</p>
      </div>
    </footer>
  </div>
</template>

<style scoped>
.layout {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.header {
  background: #ffffff;
  border-bottom: 1px solid #e2e8f0;
  padding: 1rem 0;
}

.header-content {
  max-width: 1200px;
  margin: 0 auto;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 2rem;
}

.site-title-link {
  font-size: 1.5rem;
  font-weight: bold;
  color: #2563eb;
  text-decoration: none;
}

.site-title-link:hover {
  color: #1d4ed8;
}

.nav {
  display: flex;
  gap: 2rem;
}

.nav-link {
  color: #64748b;
  text-decoration: none;
  font-weight: 500;
}

.nav-link:hover {
  color: #2563eb;
}

.main {
  flex: 1;
  max-width: 1200px;
  margin: 0 auto;
  padding: 2rem;
  width: 100%;
  box-sizing: border-box;
}

.footer {
  background: #f8fafc;
  border-top: 1px solid #e2e8f0;
  padding: 2rem 0;
  margin-top: 4rem;
}

.footer-content {
  max-width: 1200px;
  margin: 0 auto;
  text-align: center;
  color: #64748b;
  padding: 0 2rem;
}

.footer-content p {
  margin: 0.5rem 0;
}

@media (max-width: 768px) {
  .header-content {
    flex-direction: column;
    gap: 1rem;
    padding: 0 1rem;
  }
  
  .nav {
    gap: 1rem;
  }
  
  .main {
    padding: 1rem;
  }
}
</style>