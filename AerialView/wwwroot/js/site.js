let arrow = document.querySelectorAll(".arrow");
let arrow1 = document.querySelectorAll(".arrow1");
let sidebar = document.querySelector(".sidebar");
let sidebarBtn = document.querySelector(".bx-menu");

//sidebar toggle
function sidebarToggleHandling() {

sidebarBtn.addEventListener("click", () => {
    var toggleState = sidebar.classList.toggle("close");

if (!toggleState) {
    sidebarBtn.style.transform = "rotate(180deg)";
} else {
    sidebarBtn.style.transform = "rotate(0deg)"; 
}
});
}

//menu toggle
function menuToggleHandling() {

for (var i = 0; i < arrow.length; i++) {
    arrow[i].addEventListener("click", (e) => {
        let arrowParent = e.target.parentElement.parentElement;
        arrowParent.classList.toggle("showMenu");
    });
}

for (var i = 0; i < arrow1.length; i++) {
    arrow1[i].addEventListener("click", (e) => {
        let arrowParent = e.target.parentElement.parentElement;
        let subMenu = arrowParent.querySelector(".childSub-menu");
        let subMenuItem = arrowParent.closest("li");
        console.log(subMenuItem);
        subMenuItem.classList.toggle("showSubMenu");
    });
}
}

sidebarToggleHandling();
menuToggleHandling();