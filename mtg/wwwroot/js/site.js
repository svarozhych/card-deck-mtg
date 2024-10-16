// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code

document.addEventListener("DOMContentLoaded", init);

function init(){
  retrieveFromStorage();

  handleDragging();

  document.querySelector("#selected").addEventListener("click", removeFromDeck);



  document.querySelectorAll(".pagination").forEach(a => { 
    a.addEventListener("click", putToStorage);
  });
}

function retrieveFromStorage() {
  const storedData = localStorage.getItem('DeckData');
  const deck = document.querySelector("#deck #selected");

  const parsedData = JSON.parse(storedData);
  if(parsedData != null)
  {
    document.querySelector("#name").value = parsedData.name;
    document.querySelector("#description").value = parsedData.description;
    let count = 0;
    parsedData.cardsId.forEach(card => {
      deck.insertAdjacentHTML("beforeend",`<div style="border: solid 0.15rem">
        <input type="hidden" readonly name="SelectedCardIds" value=${card} data-card-name=${card}></input>
        <span>${parsedData.cardNames[count]}</span>
      </div>`)
      count ++;
    });
  }
}

function handleDragging() {
  const draggableElements = document.querySelectorAll(".draggable");

const droppableElement = document.querySelector(".droppable");


draggableElements.forEach((element) => {
  element.addEventListener("dragstart", dragStart);
  element.addEventListener("dragend", dragEnd);
});


droppableElement.addEventListener("dragover", dragOver);
droppableElement.addEventListener("dragenter", dragEnter);
droppableElement.addEventListener("dragleave", dragLeave);
droppableElement.addEventListener("drop", drop);


function dragStart(event) {
  
  console.log("dragStart");
  event.dataTransfer.setData("text/plain", event.target.alt);
  event.dataTransfer.setData("url", event.target.src);
}


function dragEnd(event) {
  
  console.log("dragEnd");
  event.target.classList.remove("dragging");
}


function dragOver(event) {
  event.preventDefault();
}


function dragEnter(event) {
  event.target.classList.add("dragover");
}


function dragLeave(event) {
  event.target.classList.remove("dragover");
}


function drop(event) {
  event.preventDefault();
  event.target.classList.remove("dragover");

  
  const draggableElementId = event.dataTransfer.getData("text/plain");

  const url = event.dataTransfer.getData("url");
  
  
  const draggableElement = document.querySelector(`img[src="${url}"][alt="${draggableElementId}"]`);
  
  
  const cardName = draggableElement.getAttribute("alt");
  const id = draggableElement.parentElement.querySelector("input").value;

  
  const cardIdElement = document.createElement("input");
  cardIdElement.setAttribute("type", "hidden")
  cardIdElement.setAttribute("readonly", "");
  cardIdElement.setAttribute("name", "SelectedCardIds");
  cardIdElement.setAttribute("value", id);
  cardIdElement.setAttribute("data-card-name", id);

  const cardNameElement = document.createElement("span");
  cardNameElement.textContent = cardName

  const div = document.createElement("div");
  div.setAttribute("style", "border: solid 0.15rem")
  div.appendChild(cardIdElement);
  div.appendChild(cardNameElement);


  const selected = document.querySelector("#selected");

  
  selected.appendChild(div);
  
  putToStorage();
}

const deckElement = document.getElementById("selected");
const counterElement = document.querySelector(".count");


function updateCounter() {
  const count = deckElement.querySelectorAll("div").length;
  counterElement.textContent = "Card Count:" + count.toString() + "/60";
}


updateCounter();


deckElement.addEventListener("DOMSubtreeModified", updateCounter);
}

function removeFromDeck(event) {
  if (event.target.matches("span")) {
    const clickedDiv = event.target.parentElement;
    clickedDiv.remove(); 
  }
  putToStorage();
}

function putToStorage() {
  const deckDivs = document.querySelectorAll("#selected div");
    let values = [];
    let names = [];

    deckDivs.forEach(div => {
      const input = div.querySelector("input");
      if (input) {
        const value = input.value;
        values.push(value);
      }
      const span = div.querySelector("span");
      if(span) {
        const name = span.textContent;
        names.push(name);
      }
    });

    const data = {
      name: document.querySelector("#name").value,
      description: document.querySelector("#description").value,
      cardsId: values,
      cardNames: names
    };

    const jsonString = JSON.stringify(data);

    localStorage.setItem('DeckData', jsonString);
}
