// Settings
const hideWidth = 645;
const navbarTextShrinksPercent = 0.8;

const navbarLogo = document.getElementById("navbar-logo");
const navbarLogoFontSize = parseFloat(getComputedStyle(navbarLogo).fontSize);
const navbarLogoShrinkedFontSize = navbarTextShrinksPercent * navbarLogoFontSize;
const newBtnText = document.getElementById("navbar-newentry-txt");
const logoutBtnText = document.getElementById("navbar-logout-txt");

function onNavbarResize()
{
	let width = window.innerWidth;
	if(width <= hideWidth)
	{
		navbarLogo.style.fontSize = `${navbarLogoShrinkedFontSize}px`;
		newBtnText.style.display = "none";
		logoutBtnText.style.display = "none";
	}
	else
	{
		navbarLogo.removeAttribute("style");
		newBtnText.removeAttribute("style");
		logoutBtnText.removeAttribute("style");
	}
};

window.addEventListener("load", onNavbarResize);
window.addEventListener("resize", onNavbarResize);