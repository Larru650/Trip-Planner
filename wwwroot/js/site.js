//site.js

(function () {

    //var ele = $("#username");
    //ele.text("Albert");

    //var main = $("#main");

    //main.on("mouseenter", function () { //mouseenter in jquery is the name of the event
    //    $(this).css("background-color", "red")
    //});//here we are wiring up the mouse enter event

    //main.on("mouseleave", function () {
    //    $(this).css("background-color", "")
    //});
    //    //Collision: when same names are used for different variables, attached to the global scope
    //    //to prevent that we can call a variable INSIDE A FUNCTION A IT TURNS AUTOMATICALLY PRIVATE
   

    //var menuItems = $("ul.menu li a"); //we declare and assign a variable to the menu items(li) of the unordered(ul) list
    //menuItems.on("click", function () {
    //    var me = $(this) //this will be the object that jquery is looking at, one of the menu items that is being clicked
    //    alert(this.text()); //will show the text inside the anchortag
    // });

    
   

    var $icon = $("#sidebarToggle i.fa");//we store italic that is classed with the font awesome prop. inside sidebarToggle

    var $sidebarWrapper = $("#sidebar, #wrapper");

    $("#sidebarToggle").on("click", function () {
        $sidebarWrapper.toggleClass("hide-sidebar");


    if ($sidebarWrapper.hasClass("hide-sidebar")) {

        $icon.removeClass("fa-angle-left");
        $icon.addClass("fa-angle-right");

    }
    else {
        $icon.addClass("fa-angle-left");
        $icon.removeClass("fa-angle-right");
    }


    });
})(); //anonymous function or immediately executed function, wrapped in another big parenthesis, making a one big expression