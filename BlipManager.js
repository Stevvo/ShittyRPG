blips = []
API.onServerEventTrigger.connect(function (name, args) {
    if (name == "showBlip") {
        API.setBlipTransparency(args[0], 255);
        }
    if (name == "deleteBlips") {
        for (var i = 0; i < blips.length; i++) {
            API.deleteEntity(blips[i]);
        }
    }
    if (name == "makeBlips") {
        for (var i = 1; i < args[0]; i++) {
           var blip = API.createBlip(args[i]);
            blips.push(blip);
        }
    }
});