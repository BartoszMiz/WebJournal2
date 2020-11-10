const hideWidth = 645;
const navbar = document.getElementById("navbar");
const newBtn = document.getElementById("navbar-newentry");
const newBtnText = document.getElementById("navbar-newentry-txt");
const logoutBtn = document.getElementById("navbar-logout");
const logoutBtnText = document.getElementById("navbar-logout-txt");

function onNavbarResize()
{
	let width = window.innerWidth;
	if(width <= hideWidth)
	{
		newBtnText.style = "display: none;";
		logoutBtnText.style = "display: none;";
	}
	else
	{
		newBtnText.style = null;
		logoutBtnText.style = null;
	}
};

window.addEventListener("load", onNavbarResize);
window.addEventListener("resize", onNavbarResize);