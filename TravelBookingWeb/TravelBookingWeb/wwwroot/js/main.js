let currentIndex = 0;

function moveSlider(direction) {
    const track = document.getElementById('slider-track');
    const cards = document.querySelectorAll('.destination-card');
    const totalCards = cards.length;
    const cardsToShow = 2; 
    
    if (totalCards === 0) return;

    const maxIndex = totalCards - cardsToShow;

    currentIndex += direction;

    
    if (currentIndex < 0) {
        currentIndex = maxIndex;
    } else if (currentIndex > maxIndex) {
        currentIndex = 0;
    }

    
    const cardWidth = cards[0].offsetWidth; 
    const gap = 20; 
    
   
    const moveAmount = cardWidth + gap; 
    
    
    track.style.transform = `translateX(-${currentIndex * moveAmount}px)`;
}


window.addEventListener('resize', () => {
    moveSlider(0);
});