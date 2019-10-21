using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using System;
using TouchScript;

public class BackGroundRun : MonoBehaviour {
    private float x;
    private float y;
    private Camera cam;

	// Use this for initialization
	void Start (){
        cam = GetComponent<Camera>();
        Application.runInBackground = true;
    }

    private void OnEnable(){
        if (TouchManager.Instance != null){
            TouchManager.Instance.TouchesMoved += touchesMovedHandler;
        }
    }

    void OnDisable(){
        if (TouchManager.Instance != null){
            TouchManager.Instance.TouchesMoved -= touchesMovedHandler;
        }
    }

    private void touchesMovedHandler(object sender, TouchEventArgs e){
        int count = e.Touches.Count;
        for (int i = 0; i < count; i++){
            TouchPoint touch = e.Touches[i];
            if (touch.Tags.HasTag("horRot")){
                float f = (float)touch.Properties["Angle"];
                transform.rotation = Quaternion.Euler(x, (f / 6.25f) * 360f, 0);
                y = (f / 6.25f) * 360f;
            }

            if (touch.Tags.HasTag("verRot")){
                float j = (float)touch.Properties["Angle"];
                transform.rotation = Quaternion.Euler((j / 6.25f) * 360f, y, 0);
                x = (j / 6.25f) * 360f;
            }
            if (touch.Tags.HasTag("zoom")) {
                float k = (float)touch.Properties["RotationVelocity"];
                if (k > 0){
                    if (cam.fieldOfView >= 60){
                        return;
                    }
                    cam.fieldOfView += 3f;
                }
                else if (k == 0) {
                    return;
                }
                else{
                    if (cam.fieldOfView <= 3){
                        cam.fieldOfView = 1;
                        return;
                    }
                    cam.fieldOfView -= 3f;
                }
            }
        }
    }
}
