//Home
const arrows = document.querySelectorAll(".arrow");
const artList = document.querySelectorAll(".art-list");

arrows.forEach((arrow, i) => {
    const itemNo = artList[i].querySelectorAll("img").length;
    let clickCounter = 0;
    arrow.addEventListener("click", ()=>{
        const ratio = Math.floor(window.innerWidth/270);
        clickCounter++;
        if (itemNo - (4 + clickCounter) + (4 - ratio) >= 0) {
            artList[i].style.transform = `translateX(${
              artList[i].computedStyleMap().get("transform")[0].x.value - 300
            }px)`;
        }else{
            artList[i].style.transform="translateX(0)";
            clickCounter = 0;
        }
    });
});

//Nav bar
const ball = document.querySelector(".toggle-ball");
const items = document.querySelectorAll(
    ".container-xxx,.art-list-title,.navbar-container,.logo,.toggle,.container-zzz,body");

ball.addEventListener("click",()=>{
    items.forEach(item=>{
        item.classList.toggle("active")
    })
    ball.classList.toggle("active")
})

