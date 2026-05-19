const hoteluriDinDB = window.allHotels || [];

document.addEventListener("DOMContentLoaded", function () {
    const priceRange = document.getElementById('priceRange');
    const priceInput = document.getElementById('priceInput');

    if (priceRange && priceInput) {
        priceRange.addEventListener('input', function () {
            priceInput.value = this.value;
        });

        priceInput.addEventListener('change', function () {
            let val = parseInt(this.value);
            if (val < 100) val = 100;
            if (val > 5000) val = 5000;
            val = Math.round(val / 100) * 100;
            this.value = val;
            priceRange.value = val;
        });
    }

    const params = new URLSearchParams(window.location.search);
    const tara = params.get('destinatie') || params.get('tara');
    if (tara) {
        document.querySelectorAll('.filter-dest').forEach(cb => {
            if (tara.toLowerCase().includes(cb.value.toLowerCase())) {
                cb.checked = true;
            }
        });
    }

    applyFilters();
});

function applyFilters() {
    const container = document.getElementById('hotels-container');
    const header = document.getElementById('results-count');

    if (!container || !header) return;

    const priceInput = document.getElementById('priceInput');
    const maxP = priceInput ? parseInt(priceInput.value) : 5000;

    const selDests = Array.from(document.querySelectorAll('.filter-dest:checked')).map(c => c.value.toLowerCase());
    const selStars = Array.from(document.querySelectorAll('.filter-stars:checked')).map(c => parseInt(c.value));
    const selFacs = Array.from(document.querySelectorAll('.filter-fac:checked')).map(c => c.value);

    const filtered = hoteluriDinDB.filter(h => {
        if (h.price > maxP) return false;

        let destinatieHotel = (h.dest || "").toLowerCase();
        if (selDests.length > 0 && !selDests.some(d => destinatieHotel.includes(d))) return false;

        if (selStars.length > 0 && !selStars.includes(h.stars)) return false;

        let facilitatiHotel = h.fac || [];
        if (selFacs.length > 0 && !selFacs.every(f => facilitatiHotel.includes(f))) return false;

        return true;
    });

    container.innerHTML = "";
    header.innerText = `Am găsit ${filtered.length} proprietăți`;

    if (filtered.length === 0) {
        container.innerHTML = "<p style='font-size: 18px; color: #666; margin-top: 20px;'>Niciun hotel nu corespunde acestor criterii.</p>";
        return;
    }

    filtered.forEach(h => {
        let starsHtml = "";
        for (let i = 0; i < h.stars; i++) starsHtml += '<i class="fa-solid fa-star"></i>';

        let badgeBg = parseFloat(h.rating) >= 9.0 ? '#0057ff' : '#008009';

        container.innerHTML += `
            <div class="hotel-card">
                <div class="hotel-img-container">
                    <div style="width: 100%; height: 100%; min-height: 220px; background: linear-gradient(135deg, #0057ff, #00cbff); display: flex; flex-direction: column; align-items: center; justify-content: center; color: white; text-align: center; padding: 20px; box-sizing: border-box;">
                        <span style="font-size: 24px; font-weight: 800; line-height: 1.2; margin-bottom: 8px;">${h.name}</span>
                        <span style="font-size: 16px; font-weight: 500; opacity: 0.9;"><i class="fa-solid fa-location-dot"></i> ${h.dest}</span>
                    </div>
                </div>
                <div class="hotel-info">
                    <div>
                        <div class="hotel-title-row">
                            <h3 class="hotel-title">${h.name} <span class="stars">${starsHtml}</span></h3>
                            <span class="badge-rating" style="background-color: ${badgeBg};">${h.rating} Scor</span>
                        </div>
                        <p class="hotel-desc">${h.desc}</p>
                        <div class="perks"><i class="fa-solid fa-check"></i> ${h.perk}</div>
                    </div>
                    <div class="hotel-bottom-row">
                        <div class="price-box">
                            <div class="price-value">${h.price} RON</div>
                            <div class="price-sub">pe noapte, 2 adulți</div>
                        </div>
                        <a href="/Hotels/DetaliiClient/${h.id}" class="btn-details">Vezi detalii</a>
                    </div>
                </div>
            </div>
        `;
    });
}