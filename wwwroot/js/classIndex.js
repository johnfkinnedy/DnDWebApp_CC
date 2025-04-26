"use strict";
import {gsap} from "../lib/gsap/all.js"
const mainContainer = document.querySelector(".main-container"),
    imagePreview = mainContainer.querySelectorAll(".image-preview"),
    images = mainContainer.querySelectorAll(".image-preview img"),
    coverImgs = mainContainer.querySelector(".coverImg");

window.onload = () => {
    mainContainer.onMouseEnter = () => {
        images.forEach((image) => {
            image.style.opacity = 0.5;

        })
    }
    mainContainer.onMouseLeave = () => {
        images.forEach((image) => {
            image.style.opacity = 1;
        })
    }

    const tl = gsap.timeline();
    tl.to(imagePreview, {
        duration: 1,
        clipPath: "polygon(0 0, 100% 0%, 100% 100%, 0% 100%)",
        stagger: 0.1
    })
    
}

const swapButtonOne = document.getElementById("classBtnOne");
const swapButtonTwo = document.getElementById("classBtnTwo");

swapButtonOne.addEventListener("click", changeToSetTwo);
swapButtonTwo.addEventListener("click", changeToSetOne);

function changeToSetOne() {
    window.location.href = "https://localhost:7130/characterclass/index";
}
function changeToSetTwo() {
    window.location.href = "https://localhost:7130/characterclass/index2";
}
/*
const mainDivOne = document.getElementById("classSetOne");
const mainDivTwo = document.getElementById("classSetTwo");

const imagesSetOne = document.querySelectorAll(".classImgSetOne");
const imagesSetTwo = document.querySelectorAll(".classImgSetTwo");



function changeToSetTwo() {
    console.log("switching to set two");
    mainDivOne.hidden = true;
    mainDivTwo.hidden = false;
    mainDivOne.style.display = 'none';
    mainDivTwo.style.display = 'flex';
    mainDivOne.style.visibility = 'hidden';
    mainDivTwo.style.visibility = 'visible';

}
function changeToSetOne() {
    console.log("switching to set one");
    mainDivOne.hidden = false;
    mainDivTwo.hidden = true;
    mainDivOne.style.display = 'flex';
    mainDivTwo.style.display = 'none';
    mainDivOne.style.visibility = 'visible';
    mainDivTwo.style.visibility = 'hidden';

}
*/