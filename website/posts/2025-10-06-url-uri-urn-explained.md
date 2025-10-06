---
title: URL vs URI vs URN — Modern Web Anatomy (with SPA Context)
description: Practical guide to URI structure and how URL & URN map parts (scheme, authority, path, query, fragment) to modern SPA routing and state.
date: 2025-10-06T02:17:00.000Z
draft: false
preview: "Scheme, authority, path, query, fragment — what they actually mean today (and why URL & URN are both URIs)."
tags:
  - web-platform
  - urls
  - architecture
  - spa
  - http
  - standards
categories:
  - web
  - explainer
hero: https://res.cloudinary.com/dfph3xsla/image/upload/f_auto%2Cq_auto%2Cw_1600%2Cdpr_auto/github/mfsbo/url_uri_urn_mvbgv0.png
type: default
---

## URL vs URI vs URN — Modern Web Anatomy (with SPA Context)

**TL;DR:** **URI** is the superset concept. **URL** (locator) and **URN** (name) are *both* kinds of URIs. A URL says *where/how* to fetch. A URN says *what it is named* inside a formal namespace (location-agnostic). Most day‑to‑day dev work is with URLs; URNs appear in niche namespaces (ISBN, RFC, UUID, package metadata, some XML/semantic vocabularies). Understanding the generic URI grammar unlocks consistent parsing, state encoding in SPAs, and cleaner routing.

<!-- markdownlint-disable MD033 -->
<picture>
  <source media="(min-width: 1024px)" srcset="https://res.cloudinary.com/dfph3xsla/image/upload/f_auto%2Cq_auto%2Cw_1400%2Cdpr_auto/github/mfsbo/url_uri_urn_mvbgv0.png">
  <img src="https://res.cloudinary.com/dfph3xsla/image/upload/f_auto%2Cq_auto%2Cw_900%2Cdpr_auto/github/mfsbo/url_uri_urn_mvbgv0.png" alt="Diagram illustrating comparison of URL, URI and URN components" loading="lazy" decoding="async">
</picture>

## 1. The Core Hierarchy

```text
URI
├── URL (Uniform Resource Locator)
└── URN (Uniform Resource Name)
```

Think: *All squares are rectangles*. A URL *is a* URI that contains (or implies) a network access mechanism (scheme + authority + optional path etc.). A URN *is a* URI that names a resource in a persistent, location‑independent namespace: `urn:isbn:9780141036144`.

| Term | Stands for | Core Purpose | Example |
|------|------------|--------------|---------|
| URI | Uniform Resource Identifier | Generic identifier string form | `https://example.com/docs?page=2#intro` |
| URL | Locator (a URI subtype) | Locate & access resource (typically via protocol) | `https://api.example.dev/v1/users?id=42` |
| URN | Name (a URI subtype) | Persistent, location‑agnostic name | `urn:ietf:rfc:7230` |

## 2. Generic URI Syntax (RFC 3986 Pattern)

```text
<scheme>://<authority><path>?<query>#<fragment>
```

Only *scheme* is mandatory in a minimal absolute form; other pieces appear when meaningful. Authority may embed userinfo and port.

| Component | Subparts | Separator(s) | Purpose | Example Values |
|-----------|----------|--------------|---------|----------------|
| scheme | — | `:` (then often `//`) | Defines interpretation & initial protocol rules | `http`, `https`, `mailto`, `urn`, `blob`, `data`, `ws`, `wss` |
| authority | userinfo@ host : port | `//` prefix; `@`, `:` | Names network location & optional credentials | `api.example.com:8443`, `user:pass@host` |
| userinfo | username[:password] | `@` after credentials | Legacy/basic access (rare in public web) | `admin:secret@` |
| host | domain / IPv4 / [IPv6] | — | Network address label | `example.com`, `127.0.0.1`, `[2001:db8::1]` |
| port | digits | `:` after host | Overrides default port for scheme | `443`, `3000` |
| path | segments separated by `/` | `/` | Hierarchical resource location | `/v1/users/42` |
| query | key=value pairs (URL-encoded) | `?` then `&` | Non-hierarchical parameters / filters / state | `page=2&sort=desc` |
| fragment | opaque interpreter-specific token | `#` | Client-side intra-resource reference or state anchor | `#section-3`, `#/?modal=login` |

*Note:* Some schemes omit certain pieces (e.g., `mailto:`, `urn:` have no authority; `data:` encodes media inline).

## 3. URL: Practical Anatomy

Using the Web API `URL` interface (MDN reference), once parsed you get ergonomic properties:

```js
const u = new URL("https://user:pw@sub.example.dev:8443/reports/q1/summary?region=emea&sort=asc#section-2");
{
  protocol: 'https:', // includes ':'
  username: 'user',
  password: 'pw',
  host: 'sub.example.dev:8443',
  hostname: 'sub.example.dev',
  port: '8443',
  origin: 'https://sub.example.dev:8443',
  pathname: '/reports/q1/summary',
  search: '?region=emea&sort=asc',
  hash: '#section-2'
}
```

`searchParams` lets us treat the query as a map:

```js
u.searchParams.get('region'); // 'emea'
u.searchParams.set('page', '2');
```

### Frequent URL Misunderstandings

| Myth | Reality |
|------|---------|
| "URL and URI are different things" | A URL is *one kind* of URI. |
| "Fragment is sent to server" | It is *not* (unless turned into part of path/query by client routing). |
| "Everything after ? is query param pairs" | Some apps embed JSON or base64 — allowed, but readability trade-off. |
| "Ports must always be specified" | Default ports (80, 443, etc.) are implied. |
| "Username/password are common" | Largely discouraged; use headers/OAuth not `user:pass@` in production. |

## 4. URN: Pure Naming

A **URN** identifies by *name only*. Pattern is:

```
urn:<NID>:<NSS>
```

Where **NID** is namespace identifier (case-insensitive) and **NSS** is namespace-specific string, guaranteed unique *within* that namespace.

Examples:

| Namespace | URN Example | Meaning |
|-----------|-------------|---------|
| ISBN | `urn:isbn:9780141036144` | Book by ISBN code |
| IETF RFC | `urn:ietf:rfc:7230` | HTTP/1.1 RFC 7230 specification |
| UUID | `urn:uuid:123e4567-e89b-12d3-a456-426614174000` | Specific universally unique identifier |
| OASIS | `urn:oasis:names:specification:docbook:dtd:xml:4.5` | DocBook DTD name |

No authority, no path/query/fragment in the typical web sense. Resolution (turning the name into a retrievable representation) is external and namespace-dependent.

## 5. Relative References & Base URLs

Not every identifier you write is absolute. In HTML & modules:

```html
<a href="../images/logo.svg">Logo</a>
<script type="module" src="./main.js"></script>
```

The browser synthesises the absolute URL using the document base (`<base href>` or the document location). The `URL()` constructor mirrors this when you pass a second argument.

## 6. How SPAs Use The Pieces

Modern Single Page Applications repurpose normal URL parts for *client state*:

| Part | SPA Usage Pattern | Pros | Cautions |
|------|-------------------|------|----------|
| Path | Logical route hierarchy (`/dashboard/accounts/42`) | Bookmarkable, SEO, shareable | Deep nesting can bloat bundle splits |
| Query | Filters, pagination, sort, feature flags (`?page=2&sort=price`) | Reflects user intent; easily diffable | Overloading can create opaque long strings |
| Fragment (hash) | Legacy routers (`#/inbox/5`), scroll targets, ephemeral UI (`#modal=signup`) | Doesn't hit server; fast local transitions | Not indexed the same way; can complicate analytics |
| Subdomain | Multi-tenant or environment scoping (`eu.app.example.com`) | CDN separation; tenancy isolation | TLS & wildcard cert complexity |
| Port | Local dev (`:5173`, `:3000`) | Parallel micro-frontend dev | Avoid leaking non-standard ports in prod links |
| Scheme | Security + protocol upgrade (`http` → `https`, `ws` → `wss`) | Integrity & modern APIs require `https` | Mixed content/CDN edge cases |
| Query (encoded state) | Persisting UI layout (`layout=grid&cols=4`) | Restores state on refresh | Keep size < ~2KB for shareability |
| Fragment (in-app router) | Offline-first static hosting hacks | Works with simple static hosts | Prefer History API now |

### Query vs Fragment for State

| Use Case | Prefer Query | Prefer Fragment |
|----------|--------------|-----------------|
| Affects server-side caching / SSR result | ✅ | — |
| Purely client visual mode toggle | — | ✅ |
| Should be shareable & crawlable | ✅ | (Maybe) |
| Sensitive to ordering & diffing for analytics | ✅ | — |
| Must not trigger server 404 on static host | — | ✅ |

### History API Harmony

Modern routers use `pushState` to keep *semantic* paths without reloading. Under the hood they still form proper URLs so deep links function with server rewrites (e.g., catch-all to `index.html`).

## 7. Designing Clean API & App URLs

Principles:

1. **Stability over cleverness** — Changing `/v1/users` to `/u` saves 3 chars and burns muscle memory.
2. **Hierarchy mirrors resource nesting** — `/accounts/42/transactions/2024` reads like a breadcrumb.
3. **Short plural nouns** for collections; singular for specific resource (`/users` vs `/users/42`).
4. **Consistent separators** — prefer hyphens over underscores in path segments.
5. **Avoid encoding entire state blobs** — Resist base64 mega-query strings; consider server sessions or localStorage for large, non-shareable view state.
6. **Version in path or subdomain, not query** — `/v2/` or `v2.api.example.com`.
7. **Lowercase paths** — Case-insensitive mental model; avoid 404 surprises on case-sensitive servers.
8. **Human-first, machine-also** — Slugs: `/posts/how-url-parsing-works` > `/posts/9f2a01` (maybe both: ID + slug).

## 8. Validation & Parsing Strategies

### Native `URL`

`new URL()` throws on invalid absolute URLs. For relative parsing supply base. Use `URL.canParse()` (newer) for preflight.

### Defensive Steps

- Normalise trailing slashes.
- Percent-encode unsafe characters (path vs query rules differ).
- Whitelist acceptable schemes (`https`, `wss`, maybe `blob`, `data` if controlled).
- Reject embedded credentials in publicly shared URLs.
- Limit query key count to prevent abuse.

### Example Utility (Lightweight)

```js
export function parseAppUrl(str, base = window.location.origin) {
  if (!URL.canParse(str, base)) throw new Error('Invalid URL');
  const u = new URL(str, base);
  return {
    origin: u.origin,
    path: u.pathname.replace(/\/+$/, ''),
    params: Object.fromEntries(u.searchParams.entries()),
    fragment: u.hash.startsWith('#') ? u.hash.slice(1) : u.hash
  };
}
```

## 9. Encoding Nuggets

| Layer | Example | Notes |
|-------|---------|-------|
| Percent-encoding (path) | space → `%20` | Avoid `+` (query forms may treat differently) |
| Query param encoding | `name=Jalal%20al-Din` | Use `URLSearchParams` to avoid manual mistakes |
| Reserved chars | `:/?#[]@!$&'()*+,;=` | Roles vary by component — escape when literal |
| International domains | `münich.example` → `xn--mnich-kva.example` | Punycode via `toASCII` (WHATWG URL) |
| Fragment encoding | `#section 2` → `#section%202` | Browser may auto-encode when setting `hash` |

## 10. URNs in Practice Today

While rarer in frontend application code, URNs surface in:

- Metadata (e.g., EPUB manifest using `urn:isbn:`)
- Standards referencing other specs (RFC refs)
- Namespace declarations (XML Schema, DocBook, OASIS)
- UUID-based names where persistence matters independent of host servers

If you expose URNs in an API, treat them as *opaque strings*; clients shouldn’t parse semantic structure beyond pattern validation.

## 11. Putting It Together — Example Dissection

```text
https://media.example.com:8443/assets/img/banner.png?theme=dark&v=5#hero
└─ scheme: https
   └─ authority: media.example.com:8443
      ├─ host: media.example.com
      └─ port: 8443
└─ path: /assets/img/banner.png
└─ query: theme=dark&v=5
   ├─ theme = dark
   └─ v = 5
└─ fragment: hero (client scroll target or state)
```

URN counterpart (naming same conceptual image in a registry):

```text
urn:example:asset:img:banner:5
```

## 12. Quick Decision Cheatsheet

| Need | Use | Why |
|------|-----|-----|
| Load a web resource | URL | Must locate & fetch |
| Provide persistent name across hosts | URN | Location can change |
| Link within your SPA & preserve filters | URL (query) | Share/reload state |
| Link to in-page section | URL (fragment) | Scroll / highlight |
| Name a spec / book / RFC | URN | Standards ecosystems |

## 13. Common Pitfalls & Anti-Patterns

| Pitfall | Better Approach |
|---------|-----------------|
| Packing entire view model JSON into query | Store large ephemeral state locally; keep shareable keys only |
| Using fragment for SEO-relevant content (`#!`) | Use real paths + SSR / prerender |
| Mixed-case path segments | Normalise to lowercase slugs |
| Credentials in URL | Use headers / OAuth flows |
| Unbounded query parameters from UI filters | Cap keys & paginate |
| Relying on implicit base without specifying `<base>` in complex docs | Add `<base>` or use absolute paths |

## 14. References & Attribution

1. MDN Web Docs — [URL interface](https://developer.mozilla.org/en-US/docs/Web/API/URL) (parsed property behaviors, constructor & searchParams) (paraphrased under CC BY-SA 2.5/3.0 +).
2. MDN Web Docs — [URIs overview](https://developer.mozilla.org/en-US/docs/Web/URI) (generic structure & components) (paraphrased).
3. MDN Web Docs — [URN scheme](https://developer.mozilla.org/en-US/docs/Web/URI/Reference/Schemes/urn) (syntax & examples) (paraphrased).
4. RFC 3986 (generic URI syntax) — authoritative grammar.
5. Personal implementation & SPA routing experience.

*All MDN-derived material paraphrased; consult original pages for verbatim definitions.*
