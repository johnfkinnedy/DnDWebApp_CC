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