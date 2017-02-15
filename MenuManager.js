var pool = null;
var text = null;


API.onServerEventTrigger.connect(function (name, args) {
    if (name == "menu_handler_create_menu") {
        pool = API.getMenuPool();
        var callbackId = args[0];
        var banner = args[1];
        var subtitle = args[2];
        var noExit = args[3];

        var menu = null;
        if (banner == null)
            menu = API.createMenu(subtitle, 0, 0, 6);
        else menu = API.createMenu(banner, subtitle, 0, 0, 6);

        if (noExit) {
            menu.ResetKey(menuControl.Back);
        }
        
        var itemsLen = args[4];

        for (var i = 0; i < itemsLen; i++) {
            var item = API.createMenuItem(args[5 + i], "");
            item.SetRightLabel(args[5 + i + itemsLen]);
            menu.AddItem(item);
        }

        menu.OnItemSelect.connect(function(sender, item, index) {
            API.triggerServerEvent("menu_handler_select_item", callbackId, index);
            pool = null;
        });

        menu.Visible = true;

        pool.Add(menu);
    }
    else if (name == "menu_handler_create_menu2") {
        pool = API.getMenuPool();
        var callbackId = args[0];
        var banner = args[1];
        var subtitle = args[2];
        var noExit = args[3];

        var menu2 = null;
        if (banner == null)
            menu2 = API.createMenu(subtitle, 0, 0, 6);
        else menu2 = API.createMenu(banner, subtitle, 0, 0, 6);

        if (noExit) {
            menu2.ResetKey(menuControl.Back);
        }
        
        var itemsLen = args[4];

        for (var i = 0; i < itemsLen; i++) {
            var item = API.createMenuItem(args[5 + i], "");
            item.SetRightLabel(args[5 + i + itemsLen]);
            menu2.AddItem(item);
        }

        menu2.OnItemSelect.connect(function(sender, item, index) {
            API.triggerServerEvent("menu_handler_select_item", callbackId, index);
        });

        menu2.Visible = true;

        pool.Add(menu2);
    }
    else if (name === "menu_handler_close_menu") {
        pool = null;
    }
    else if (name == "get_user_input") {
        text = API.getUserInput(args[1], args[2]);
        if (args[3] == null) {
            API.triggerServerEvent("menu_handler_user_input", args[0], text);
        }
        else
        {
            API.triggerServerEvent("menu_handler_user_input", args[0], text, args[3]);
        }
    }
});

API.onUpdate.connect(function() {
    if (pool != null) {
        pool.ProcessMenus();
    }
});