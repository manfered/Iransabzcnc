$(function () {

    //add bootstrap's scrollspy
    $('body').scrollspy({
        target: '.navbar',
        offset: 60
    });

    // smooth scrolling
    $('nav a, .sidenav a').bind('click', function () {
        $('html, body').stop().animate({
            scrollTop: $($(this).attr('href')).offset().top
        }, 1500, 'easeInOutExpo');
        event.preventDefault();
    });

    // initialize WOW for element animation
    new WOW({ offset: 0 }).init()

});


function openNav() {
    document.getElementById("mySidenav").style.width = "50vw";
    document.getElementById("mySidenavContainer").style.zIndex = "1000000";
    document.getElementById("mySidenavContainer").style.backgroundColor = "rgba(0,0,0, 0.5)";

}

function closeNav() {
    document.getElementById("mySidenavContainer").style.zIndex = "0";
    document.getElementById("mySidenavContainer").style.backgroundColor = "rgba(255,255,255, 0.0)";
    document.getElementById("mySidenav").style.width = "0";
}
