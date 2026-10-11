// Runs in <head> so the saved theme applies before the page paints.
(function () {
  try {
    var saved = localStorage.getItem('theme');
    if (saved === 'light' || saved === 'dark') document.documentElement.setAttribute('data-theme', saved);
  } catch (e) { /* storage blocked: follow the system theme */ }
})();
