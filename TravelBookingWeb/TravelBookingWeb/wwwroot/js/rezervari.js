document.addEventListener("DOMContentLoaded", () => {
    const bookingForm = document.getElementById('bookingForm');

    if (bookingForm) {
        bookingForm.addEventListener('submit', function (e) {
            e.preventDefault();

            // Simulăm o procesare
            const btn = this.querySelector('button[type="submit"]');
            btn.innerHTML = '<i class="fa-solid fa-circle-notch fa-spin"></i> Se procesează...';
            btn.disabled = true;

            setTimeout(() => {
                alert("Rezervarea a fost confirmată cu succes! Vei primi detaliile pe e-mail.");
                window.location.href = "/";
            }, 2000);
        });
    }
});

function confirmBooking(itemName) {
    return confirm(`Ești sigur că vrei să rezervi ${itemName}?`);
}