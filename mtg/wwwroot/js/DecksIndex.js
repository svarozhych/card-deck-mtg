document.addEventListener("DOMContentLoaded", init);

function init() {

    localStorage.removeItem('DeckData');


    const images = ['chivalry.png', 'classic.png', 'mage.png', 'maya.png', 'void.png', 'wild.png', 'beast.png'];

    function getRandomImage() {
    const randomIndex = Math.floor(Math.random() * images.length);
    return images[randomIndex];
    }

    console.log(images);

    const liElements = document.querySelectorAll('.deckItem');
    liElements.forEach(li => {
    const randomImage = getRandomImage();
    li.style.backgroundImage = `url(../assets/packs/${randomImage})`;
    li.style.backgroundRepeat = "no-repeat";
    li.style.backgroundSize = "contain";
    });

}