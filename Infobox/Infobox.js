var mainBrowser = null;

API.onResourceStart.connect(function() {
	var res = API.getScreenResolution();
	mainBrowser = API.createCefBrowser(res.Width -600, res.Height -400);
	API.setCefBrowserPosition(mainBrowser, 160, 150);
	API.setCefBrowserHeadless(mainBrowser, true);
	API.waitUntilCefBrowserInit(mainBrowser);
	API.loadPageCefBrowser(mainBrowser, "Infobox/index.html");
});

API.onResourceStop.connect(function() {
	if (mainBrowser != null) {
		API.destroyCefBrowser(mainBrowser);
	}
});

API.onKeyDown.connect(function(sender, arg) {
    if (arg.KeyCode == Keys.Back && !API.isChatOpen()) {
		API.setCefBrowserHeadless(mainBrowser, true);
	}
});
API.onServerEventTrigger.connect(function (name, args) {
    if (name == "createInfobox") {
        API.setCefBrowserHeadless(mainBrowser, false);
    }
    else if (name == "hideInfobox") {
        API.setCefBrowserHeadless(mainBrowser, true);
    }
});