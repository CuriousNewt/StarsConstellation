using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class quit : MonoBehaviour {

    private void OnEnable()
    {
        GetComponent<TapGesture>().Tapped += quitGame;
    }

    private void OnDisable()
    {
        GetComponent<TapGesture>().Tapped -= quitGame;
    }


    void quitGame(object s, EventArgs arg0)
    {
        Application.Quit();
    }
}
