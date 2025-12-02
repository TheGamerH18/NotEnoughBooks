// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
import {InputSpinner} from "/js/InputSpinner.js"

const inputSpinnerElements = document.querySelectorAll("input[type='number']")
for (const inputSpinnerElement of inputSpinnerElements) {
    new InputSpinner(inputSpinnerElement)
}