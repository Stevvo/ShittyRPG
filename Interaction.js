API.onKeyDown.connect(function (sender, args) {
    var handle = null;
    if (!API.isChatOpen() && args.KeyCode == Keys.E) {
        var pos = API.getEntityPosition(API.getLocalPlayer());

        var handles = [-1631057904, -1317098115, -171943901, 1805980844, -403891623, 437354449, 291348133, 146905321, -741944541, 1329570871, -1126237515, -1364697528, 506770882, -628719744]
        for (i = 0; i < handles.length; i++) {

            var handle = API.returnNative("GET_CLOSEST_OBJECT_OF_TYPE", 9, pos.X, pos.Y, pos.Z, 10.5, handles[i], false, true, true);

            if (handle.IsNull) {
               // API.sendNotification("There is nothing to interact with here");
            }
            else {
                API.triggerServerEvent("Interact", API.getEntityModel(handle), API.getEntityPosition(handle), API.getEntityRotation(handle), handle);
                API.sendNotification("Press M to get up");
            }
            var handle = 0;
        }
    }
if (args.KeyCode == Keys.M && !API.isChatOpen()) {
        API.triggerServerEvent("CancelInteract", handle);
}
if (!API.isChatOpen() && args.KeyCode == Keys.Enter) {
    API.triggerServerEvent("KeysEnter")
}
});

API.onServerEventTrigger.connect(function (name, args) {
    if (name === "toggleblip") {
        API.setBlipTransparency(args[0], 0);
    }
});