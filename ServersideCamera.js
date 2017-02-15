var cameras = [];
var interpolating = false;
API.onServerEventTrigger.connect(function (name, args) {
    if (name == "createCameraActive") {
            var pos = args[0];
            var rot = args[1];
            var cam = API.createCamera(pos, rot);
            cameras.push(cam);
            API.setActiveCamera(cam);
    }

    else if (name == "createCameraAtGamecam") {
        var pos = API.getGameplayCamPos();
        var rot = API.getGameplayCamRot();
        var cam = API.createCamera(pos, rot);
        cameras.push(cam);
        API.setActiveCamera(cam);
    }

    else if (name == "createCameraInactive") {
        var pos = args[0];
        var rot = args[1];
        var cam = API.createCamera(pos, rot);
        cameras.push(cam);
    }

    else if (name == "setCameraFov") {
        API.setCameraFov(cameras[args[0]], args[1]);
    }

    else if (name == "pointCameraAtLocalPlayer") {
        API.pointCameraAtEntity(cameras[args[0]], API.getLocalPlayer(), args[1]);
    }

    else if (name == "setActiveCamera"){
        API.setActiveCamera(cameras[args[0]]);
    }

    else if (name == "interpolateCamera") {
        var cam = cameras[args[0]];
        var duration = args[1];
        var easepos = args[2];
        var easerot = args[3];
        if (API.getActiveCamera() != cam) {
            interpolating = true;
            API.interpolateCameras(API.getActiveCamera(), cam, duration, easepos, easerot);
            API.sleep(args[1] + 100);
            interpolating = false;
            API.setActiveCamera(cam);
        }
    }

    else if (name == "clearCameras") {
            API.setActiveCamera(null);
            cameras = [];
    }

    else if (name == "setActiveCamera") {
        API.setActiveCamera(args[0]);
    }

    else if (name == "backToGamecam") {
        API.setActiveCamera(null);
        }
    else if (name == "setCameraHeading") {
        var cam = args[0];
        var rot = args[1];
        API.setCameraRotation(cam, new Vector3(0, 0, rot));
    }
   
});