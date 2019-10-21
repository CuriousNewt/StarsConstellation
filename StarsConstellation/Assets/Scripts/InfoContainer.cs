using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InfoContainer : MonoBehaviour {
    string[] names;
    string[] distance;
    string[] description;
    int[] multiplier;
	// Use this for initialization
	void Start () {
        names = new String[100];
        distance = new String[100];
        description = new String[100];
        multiplier = new int[100];
	}
}
