const toggler = document.getElementById('toggler');
const nav = document.getElementById('nav');
const navContent = document.getElementById('navContent');

const hideClasses = ['toggler-menu--hide', 'nav-side-bar--hide', 'nav-content--hide']

toggler.addEventListener('click', function() {
    if(toggler.classList.contains('toggler-menu--hide') && nav.classList.contains('nav-side-bar--hide') && navContent.classList.contains('nav-content--hide')){
        toggler.classList.replace(hideClasses[0], 'toggler-menu');
        toggler.innerHTML = `<i class="fa-solid fa-x"></i>`
        nav.classList.replace(hideClasses[1], 'nav-side-bar');
        navContent.classList.replace(hideClasses[2], 'nav-content')
    }
    else{
        toggler.classList.replace('toggler-menu', hideClasses[0]);
        toggler.innerHTML = `<i class="fa-solid fa-bars"></i>`
        nav.classList.replace('nav-side-bar', hideClasses[1]);
        navContent.classList.replace('nav-content', hideClasses[2])
    }
})