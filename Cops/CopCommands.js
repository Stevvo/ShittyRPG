var cuffed = null;

API.onKeyDown.connect(function (sender, args) {
    if (args.KeyCode == Keys.F3) {
        API.triggerServerEvent("GroupMenuKeyPressed");
    }
});

API.onServerEventTrigger.connect(function (name, args) {
    if (name == "CUFFED") {
        cuffed = args[0];
    }
});

API.onUpdate.connect(function(s, e) {
    var player = API.getLocalPlayer();
    if ((!API.isChatOpen()) && (API.isControlJustPressed(21)) && (cuffed == true)) {
        
        API.callNative("SET_PED_TO_RAGDOLL", player, 4000, 5000, 1, 1, 1, 0);
        API.callNative("SET_ENABLE_HANDCUFFS", player, true);
    //    API.callNative("CREATE_NM_MESSAGE", true, 0);
  //      API.callNative("GIVE_PED_NM_MESSAGE", player);
    }
    if ((!API.isChatOpen()) && (API.isControlJustPressed(32)) && (cuffed == true)) {
        API.triggerServerEvent("cuffUpdate", player);
    }
});