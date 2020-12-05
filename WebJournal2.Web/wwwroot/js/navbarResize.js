// Settings
const hideWidth = 645;
const navbarTextShrinksPercent = 0.8;

let navbarLogo, navbarLogoFontSize, navbarLogoShrinkedFontSize, newBtnText, logoutBtnText;

function navbarResizeInit()
{
	navbarLogo = document.getElementsByClassName("navbar-logo")[0];
	navbarLogoFontSize = parseFloat(getComputedStyle(navbarLogo).fontSize);
	navbarLogoShrinkedFontSize = navbarTextShrinksPercent * navbarLogoFontSize;
	newBtnText = document.getElementById("navbar-newentry-txt");
	logoutBtnText = document.getElementById("navbar-logout-txt");
}

function onNavbarResize()
{
	if (navbarLogo == null)
		return;

	let width = window.innerWidth;
	if (width <= hideWidth)
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

window.addEventListener("resize", onNavbarResize);