var justEnteredCar = false;

API.onServerEventTrigger.connect(function (name, args) {
    if (name === "CarEnter") {
        justEnteredCar = true;
    }
});

API.onKeyDown.connect(function (sender, args) {
    if (!API.isChatOpen() && args.KeyCode == Keys.Enter) {
        if (!justEnteredCar) return;
        API.triggerServerEvent("Enter");
        justEnteredCar = false;
    }
    if (!API.isChatOpen() && args.KeyCode == Keys.F2 && API.isPlayerInAnyVehicle(API.getLocalPlayer())) {
        API.triggerServerEvent("VehicleMenuKeyPressed");
    }
});