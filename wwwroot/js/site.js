(function () {
  'use strict';

  /* ---------- theme toggle ---------- */
  const root = document.documentElement;
  const toggle = document.getElementById('theme-toggle');
  const systemDark = () => window.matchMedia('(prefers-color-scheme: dark)').matches;
  const currentTheme = () => root.getAttribute('data-theme') || (systemDark() ? 'dark' : 'light');
  const syncToggle = () => {
    const t = currentTheme();
    root.setAttribute('data-theme', t);
    toggle.setAttribute('aria-pressed', String(t === 'dark'));
  };
  toggle.addEventListener('click', () => {
    const next = currentTheme() === 'dark' ? 'light' : 'dark';
    root.setAttribute('data-theme', next);
    try { localStorage.setItem('theme', next); } catch (e) { }
    syncToggle();
  });
  syncToggle();

  /* ---------- transcript chart ---------- */
  const modules = {
    '2024': [['IT Professional Practice', 80], ['Maths Principles for CS', 77], ['Network Engineering 1A', 79], ['Network Engineering 1B', 76], ['Programming Logic & Design', 81], ['Programming 1A', 71], ['Programming 1B', 69], ['Principles of Security', 79]],
    '2025': [['Cloud Development A', 72], ['Cloud Development B', 69], ['Information Systems 1B', 85], ['Information Systems 2A', 84], ['Information Systems 2B', 59], ['Information Systems 2C', 77], ['Programming 2B', 76], ['Programming 2A', 79]],
    '2026': [['Information Systems 3A', 61], ['Intro to Research for ICT', 75], ['Programming 3A', 71], ['Programming 3C', 77]]
  };
  const host = document.getElementById('bars');
  for (const [yr, rows] of Object.entries(modules)) {
    const h = document.createElement('div'); h.className = 'yr'; h.textContent = yr; host.appendChild(h);
    for (const [name, mark] of rows) {
      const r = document.createElement('div'); r.className = 'row';
      const n = document.createElement('span'); n.className = 'name'; n.title = name; n.textContent = name;
      const t = document.createElement('span'); t.className = 'trackbar';
      const fill = document.createElement('i'); if (mark >= 75) fill.className = 'd'; fill.style.width = mark + '%';
      t.appendChild(fill);
      const v = document.createElement('span'); v.className = 'v'; v.textContent = mark;
      r.append(n, t, v); host.appendChild(r);
    }
  }

  /* ---------- project filter ---------- */
  const buttons = document.querySelectorAll('.filter button');
  const projects = document.querySelectorAll('.proj');
  const note = document.getElementById('filter-note');
  const labels = { ci: 'CI/CD', container: 'containers', cloud: 'cloud', mobile: 'mobile' };
  buttons.forEach(btn => btn.addEventListener('click', () => {
    const f = btn.dataset.filter;
    buttons.forEach(b => b.setAttribute('aria-pressed', String(b === btn)));
    let shown = 0;
    projects.forEach(p => {
      const match = f === 'all' || p.dataset.tags.split(' ').includes(f);
      p.hidden = !match; if (match) shown++;
    });
    note.textContent = f === 'all' ? '' : `${shown} project${shown === 1 ? '' : 's'} involving ${labels[f]}.`;
  }));

  /* ---------- active section in nav ---------- */
  const navLinks = [...document.querySelectorAll('.bar nav a')];
  const sections = navLinks.map(a => document.querySelector(a.getAttribute('href'))).filter(Boolean);
  if ('IntersectionObserver' in window) {
    const io = new IntersectionObserver(entries => {
      entries.forEach(e => {
        if (!e.isIntersecting) return;
        navLinks.forEach(a => a.classList.toggle('active', a.getAttribute('href') === '#' + e.target.id));
      });
    }, { rootMargin: '-45% 0px -50% 0px' });
    sections.forEach(s => io.observe(s));
  }

  /* ---------- copy email ---------- */
  const copyBtn = document.getElementById('copy-email');
  const emailEl = document.getElementById('email');
  copyBtn.addEventListener('click', () => {
    const done = label => { copyBtn.textContent = label; setTimeout(() => (copyBtn.textContent = 'Copy'), 1600); };
    const selectIt = () => {
      const range = document.createRange(); range.selectNodeContents(emailEl);
      const sel = getSelection(); sel.removeAllRanges(); sel.addRange(range); done('Selected');
    };
    if (navigator.clipboard) navigator.clipboard.writeText(emailEl.textContent).then(() => done('Copied'), selectIt);
    else selectIt();
  });

  /* ---------- live deployment panel ---------- */
  const $ = id => document.getElementById(id);
  const fmtUptime = s => {
    const d = Math.floor(s / 86400), h = Math.floor(s % 86400 / 3600), m = Math.floor(s % 3600 / 60);
    return d ? `${d}d ${h}h ${m}m` : h ? `${h}h ${m}m` : `${m}m ${Math.floor(s % 60)}s`;
  };
  let uptimeBase = null, fetchedAt = 0;

  async function loadInfo() {
    const state = $('live-state');
    try {
      const t0 = performance.now();
      const [infoRes, healthRes] = await Promise.all([
        fetch('/api/info', { cache: 'no-store' }),
        fetch('/healthz', { cache: 'no-store' })
      ]);
      const ms = Math.round(performance.now() - t0);
      if (!infoRes.ok) throw new Error('HTTP ' + infoRes.status);
      const info = await infoRes.json();
      const health = (await healthRes.text()).trim();

      $('live-health').textContent = health || healthRes.status;
      if (info.commit) {
        const short = info.commit.slice(0, 7);
        const a = document.createElement('a');
        a.href = `https://github.com/Nyakatsari-Tinotenda/CV-Website/commit/${info.commit}`;
        a.target = '_blank'; a.rel = 'noopener'; a.textContent = short;
        $('live-commit').replaceChildren(a);
        const fb = $('footer-build');
        fb.textContent = 'Deployed from ';
        const fa = a.cloneNode(true); fb.appendChild(fa);
        fb.append(' via Docker on Render');
      } else {
        $('live-commit').textContent = 'local build';
      }
      $('live-branch').textContent = info.branch || '—';
      $('live-runtime').textContent = `.NET ${info.dotnet} · ${info.environment}`;
      $('live-latency').textContent = `${ms} ms`;
      uptimeBase = info.uptimeSeconds; fetchedAt = Date.now();
      tick();
      const ok = healthRes.ok && /healthy/i.test(health);
      state.textContent = ok ? '● healthy' : '● degraded';
      state.classList.toggle('warn', !ok);
    } catch (err) {
      state.textContent = '● unavailable';
      state.classList.add('warn');
      ['live-health', 'live-commit', 'live-branch', 'live-runtime', 'live-uptime', 'live-latency']
        .forEach(id => { if ($(id).textContent === '…') $(id).textContent = '—'; });
    }
  }
  function tick() {
    if (uptimeBase == null) return;
    $('live-uptime').textContent = fmtUptime(uptimeBase + (Date.now() - fetchedAt) / 1000);
  }
  loadInfo();
  setInterval(tick, 1000);
})();
