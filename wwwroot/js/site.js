const modules = {
  "2024": [["IT Professional Practice",80],["Maths Principles for CS",77],["Network Engineering 1A",79],["Network Engineering 1B",76],["Programming Logic & Design",81],["Programming 1A",71],["Programming 1B",69],["Principles of Security",79]],
  "2025": [["Cloud Development A",72],["Cloud Development B",69],["Information Systems 1B",85],["Information Systems 2A",84],["Information Systems 2B",59],["Information Systems 2C",77],["Programming 2B",76],["Programming 2A",79]],
  "2026": [["Information Systems 3A",61],["Intro to Research for ICT",75],["Programming 3A",71],["Programming 3C",77]]
};
const host = document.getElementById('bars');
for (const [yr, rows] of Object.entries(modules)) {
  const h = document.createElement('div'); h.className = 'yr'; h.textContent = yr; host.appendChild(h);
  for (const [name, mark] of rows) {
    const r = document.createElement('div'); r.className = 'row';
    r.innerHTML = `<span class="name" title="${name}">${name}</span><span class="trackbar"><i class="${mark >= 75 ? 'd' : ''}" style="width:${mark}%"></i></span><span class="v">${mark}</span>`;
    host.appendChild(r);
  }
}
const btn = document.getElementById('copy-email');
btn.addEventListener('click', () => {
  const text = document.getElementById('email').textContent;
  const done = () => { btn.textContent = 'Copied'; setTimeout(() => btn.textContent = 'Copy', 1600); };
  if (navigator.clipboard) {
    navigator.clipboard.writeText(text).then(done).catch(selectIt);
  } else selectIt();
  function selectIt() { const s = getSelection(), rg = document.createRange(); rg.selectNodeContents(document.getElementById('email')); s.removeAllRanges(); s.addRange(rg); btn.textContent = 'Selected'; }
});
