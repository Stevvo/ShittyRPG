var canshoot = true;

API.onUpdate.connect(function () {
    if (API.isControlPressed(24))
    {
        API.callNative("0x5E6CC07646BBEAB8", API.getLocalPlayer(), true);
    }
    
});
