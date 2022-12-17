// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function decrementValue(index) {
    var input = document.getElementById("numberInput-" + index);
    var value = parseFloat(input.value);
    if (!isNaN(value) && value > 0) {
        input.value = value - 1;
    }
}

function incrementValue(index) {
    var input = document.getElementById("numberInput-" + index);
    var value = parseFloat(input.value);
    if (!isNaN(value)) {
        input.value = value + 1;
    }
}



function decrementRating() {
    var input = document.getElementById("ratingInput");
    var value = parseInt(input.value);
    if (value > 0) {
        input.value = value - 1;
    }
}

function incrementRating() {
    var input = document.getElementById("ratingInput");
    var value = parseInt(input.value);
    if (value < 5) {
        input.value = value + 1;
    }
}