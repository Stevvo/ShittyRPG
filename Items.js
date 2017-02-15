API.onKeyDown.connect(function (sender, args) {

if (args.KeyCode == Keys.F1) {
    API.triggerServerEvent("InventoryKeyPressed");
}
else if (args.KeyCode == Keys.E) {
    if (API.hasEntitySyncedData(API.getLocalPlayer(), "LastStep") == true) {
        API.triggerServerEvent("stopCreator", API.getLocalPlayer());
        API.setActiveCamera(null);
    }
}
});
