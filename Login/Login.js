API.onResourceStart.connect(function () {
    var res = API.getScreenResolution();
    mainBrowser = API.createCefBrowser(res.Width - 600, res.Height - 400);
    API.setCefBrowserPosition(mainBrowser, 150, 150);
  //  API.setCefBrowserHeadless(mainBrowser, true);
    API.waitUntilCefBrowserInit(mainBrowser);
    API.loadPageCefBrowser(mainBrowser, "Login/index.html");
   // API.showCursor(true);
});

API.onResourceStop.connect(function () {
    if (mainBrowser != null) {
        API.destroyCefBrowser(mainBrowser);
    }
});

API.onServerEventTrigger.connect(function (name, args) {
    if (name == "showLogin") {
        API.setCefBrowserHeadless(mainBrowser, false);
       // API.showCursor(true);
    }
    });

function sendInput(password) {
    API.triggerServerEvent("CefPassword", password);
    API.setCefBrowserHeadless(mainBrowser, true);
    API.showCursor(false);
}
