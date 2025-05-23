﻿"use strict";
const assignCharacterModalDOM = document.getElementById('assignCharacterModal');
const assignCharacterModal = new bootstrap.Modal(assignCharacterModalDOM);

const buttons = document.querySelectorAll("a.btn-warning");

//adding event listener to each button 
buttons.forEach(button => {
    button.addEventListener("click", function (event) {
        event.preventDefault();
        //does not navigate to page, but graps href and sets equipment ID
        let href = button.getAttribute("href");
        button.dataset.equipmentId = href.split("/Equipment/AssignCharacter/")[1];
        //when button is clicked, modal is shown
        assignCharacterModal.show();
        populateCharacters(allCharacters, button.dataset.equipmentId);
    })
})

//function to hide modal
let closeButton = document.getElementById("btnAssignCharacterClose");
closeButton.addEventListener("click", function () {
    assignCharacterModal.hide();
})

//gets all characters
async function getAllCharacters() {
    const url = "https://localhost:7130/api/character/all";
    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const json = await response.json();
        return json;
    } catch (error) {
        console.error(error.message);
    }
}
const allCharacters = await getAllCharacters();

//populates characters into modal
function populateCharacters(characters, equipmentId) {
    //getting table
    let characterTableBody = document.getElementById("characterTableBody");

    //looping to remove children, starting from the end
    while (characterTableBody.firstChild) {
        characterTableBody.removeChild(characterTableBody.lastChild);
    }

    characters.forEach(character => {
        let newTr = document.createElement("tr");

        let newTd = document.createElement("td");
        let text = document.createTextNode(`${character.name}`)
        newTd.appendChild(text);
        newTr.appendChild(newTd)

        newTd = document.createElement("td");
        text = document.createTextNode(`${character.alignment} ${character.class.name} ${character.level}`)
        newTd.appendChild(text);
        newTr.appendChild(newTd)

        newTd = document.createElement("td");
        let assignBtn = document.createElement("button");
        let btnText = document.createTextNode("Assign");
        assignBtn.appendChild(btnText);
        assignBtn.classList.add("btn-warning");
        assignBtn.dataset.characterId = character.id;
        assignBtn.dataset.equipmentId = equipmentId;
        newTd.appendChild(assignBtn);
        newTr.appendChild(newTd);

        //adds a button and an event listener to assign character to equipment when clicked
        assignBtn.addEventListener("click", async (event) => {
            event.preventDefault();
            console.log(assignBtn.dataset.characterId);
            console.log(assignBtn.dataset.equipmentId)
            let success = await assignCharacterToEquipment(assignBtn.dataset.equipmentId, assignBtn.dataset.characterId);
            console.log(success)
            if (!success) {
                alert("Character assignment failed. Please retry.")
            }
            else {
                alert("Equipment assigned to character!")
                location.reload();
            }

        })
        characterTableBody.appendChild(newTr);
    })

}
//assigning character to equipment
async function assignCharacterToEquipment(equipmentId, characterId) {
    let sendUrl = `https://localhost:7130/equipment/assigncharacter/${equipmentId}/${characterId}`;
    console.log(sendUrl);
    try {
        const response = await fetch(sendUrl);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const json = await response.json();
        console.log(json)
        return json;
    } catch (error) {
        console.error(error.message);
    }
}
