API.onKeyDown.connect(function (sender, args) {
    if (!API.isChatOpen() && args.KeyCode == Keys.B) {
        API.triggerServerEvent("try_to_buy");
        justEnteredCar = false;
    }
});