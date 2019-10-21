using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.UI;

public class StarCollector : MonoBehaviour {
    private GameObject[] selectedStars;
    private int index;
    private Animator anim;

    // Use this for initialization
    void Start(){
        index = 0;
        selectedStars = new GameObject[30];
        anim = GameObject.Find("ConstellationName").GetComponent<Animator>();
    }

    public void addStar(GameObject star) {
        if (isSelected(star)) {
            return;
        }
        if (selectedStars[0] == null){
            selectedStars[0] = star;
            index++;
        }
        else if (selectedStars[0].transform.parent == star.transform.parent){
            selectedStars[index] = star;
            index++;
        }
        else if (selectedStars[0].transform.parent != star.transform.parent){
            ClearSelectedStars(star);
        }
        if (size() == selectedStars[0].transform.parent.childCount) {
            GameObject.Find("ConstellationName").GetComponent<Text>().text = selectedStars[0].transform.parent.name;
            anim.CrossFade("NameShow", 1f, 0);
        }
    }

    public GameObject getStar(int i) {
        if (selectedStars[i] == null) {
            return null;
        }
        return selectedStars[i];
    }

    public int size() {
        return index;
    }


    public GameObject[] getStars() {
        return selectedStars;
    }

    public bool isSelected(GameObject star) {
        for (int i = 0; i < selectedStars.Length; i++) {
            if (star == selectedStars[i] && selectedStars[i] != null) {
                return true;
            }
        }
        return false;
    }

    private void ClearSelectedStars(GameObject star) {
        anim.CrossFade("NameHide", 1f, 0);
        for (int i = 0; i < selectedStars.Length; i++){
            if (selectedStars[i] != null){
                Destroy(selectedStars[i].GetComponent<LineRenderer>());
                selectedStars[i].GetComponent<StarTaps>().Reconnect();
            }
        }
        selectedStars = new GameObject[30];
        selectedStars[0] = star;
        index = 1;
    }
}
