"use strict";

async function getSpell(id) {
    const url =`https://localhost:7130/Spell/GetOneJson/${id}`;
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
const id = window.location.href.split("https://localhost:7130/Spell/Roll/")[1];
const spell = await getSpell(id);
const dice = spell.diceDenomination.size.split("d")[1];
const amountToRoll = spell.diceToRoll;
const spellName = spell.name;

const mainDiv = document.getElementById("mainDiv");

const avgDiv = document.getElementById("avgDiv");
const avgRoll = calculateAverage(dice, amountToRoll);
const avgP = document.createElement('p');
avgP.appendChild(document.createTextNode(`The average roll for ${amountToRoll}d${dice} is ${avgRoll}`));
avgDiv.appendChild(avgP);



let rollButton = document.getElementById('btnRoll');
rollButton.addEventListener("click", function () {
    console.log("Roll button clicked!")
    while (mainDiv.firstChild) {
        mainDiv.removeChild(mainDiv.firstChild);
    }
    let sum = 0;
    let rolls = [];
    for (let i = 0; i < amountToRoll; i++) {
        const roll = rollDice(1, dice);
        sum += roll;
        rolls.push(roll);
    }
    const rollNumbers = rolls.join(", ");
    const p = document.createElement('p');
    p.appendChild(document.createTextNode(`The individual dice rolled: ${rollNumbers}`));
    const pSum = document.createElement('p');
    pSum.appendChild(document.createTextNode(`Your roll for ${spellName} resulted in a total roll of ${sum}!`));
    mainDiv.appendChild(p);
    mainDiv.appendChild(pSum);
})

//function from https://rocambille.github.io/en/2019/07/30/how-to-roll-a-dice-in-javascript/
function rollDice(min, max) {
    return min + Math.floor(Math.random() * (max - min + 1));
}

//calculating average roll for x dice of y size, formula taken from
// https://www.omnicalculator.com/statistics/dice-average (m = (h+1)/2)
function calculateAverage(size, diceAmt) {
    return (parseInt(size) + 1 )/2 * diceAmt
}