
//toggle

const ball = document.querySelector(".toggle-ball");
const items = document.querySelectorAll(
    ".menu-list,.container,.navbar-container,.second-navbar-list,.navbar-container,.logo,.toggle, .optionss, .dropbtn");

ball.addEventListener("click",()=>{
    items.forEach(item=>{
        item.classList.toggle("active")
    })
    ball.classList.toggle("active")
})

