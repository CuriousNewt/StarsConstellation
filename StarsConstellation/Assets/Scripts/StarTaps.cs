using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.UI;

public class StarTaps : MonoBehaviour {
    private bool connect;
    private bool connect1;
    private int actualTo;
    private GameObject starCollector;
    private LineRenderer line;
    private Color c1 = Color.white;
    private Color c2 = new Color(1, 1, 1, 0);
    public static int size;
    public static int size1;
    public string distance;
    public string description;
    public float scaleToSun;
    public GameObject[] from = new GameObject[size];
    public GameObject[] to = new GameObject[size1];

    void Start () {
        if (from.Length == 0) {
            from = new GameObject[1];
        }
        if (to == null) {
            to = new GameObject[1];
        }
        actualTo = 0;
        starCollector = GameObject.Find("ClickedCollector");
        connect = false;
        connect1 = false;
    }

    private void OnEnable(){
        gameObject.GetComponent<TapGesture>().Tapped += OnTap;
        gameObject.GetComponent<LongPressGesture>().LongPressed += LongPress;
    }

    private void OnDisable(){
        gameObject.GetComponent<TapGesture>().Tapped -= OnTap;
        gameObject.GetComponent<LongPressGesture>().LongPressed -= LongPress;
    }

    void OnTap(object s, EventArgs arg0){
        starCollector.GetComponent<StarCollector>().addStar(gameObject);
        if (starCollector.GetComponent<StarCollector>().size() > 1) {
            ConnectTo();
            ConnectFrom();      
        }        
        StartCoroutine("WaitForNotify");
    }

    void LongPress(object s, EventArgs arg0) {
        GameObject holder = GameObject.Find("Star Holder");
        Destroy(holder.transform.GetChild(0).gameObject);
        GameObject newOne = Instantiate(gameObject, holder.transform);
        newOne.transform.localPosition = new Vector3(0,0,0);
        newOne.transform.localScale = new Vector3(scaleToSun, scaleToSun, scaleToSun);
        GameObject.Find("Star Name").GetComponent<Text>().text = gameObject.name;
        GameObject.Find("Star Distance").GetComponent<Text>().text = "Distance : " + distance + " light years";
        GameObject.Find("Star Temperature").GetComponent<Text>().text = "Temperature : " + GetComponent<Star>().temperatureKelvin + " K";
        GameObject.Find("Star Description").GetComponent<Text>().text = description;
        GameObject.Find("Roll Button").GetComponent<UIRoll>().Show();
    }

    private void SetUpLine(int positions) {
        if (line == null) {
            line = gameObject.AddComponent<LineRenderer>();
        }
        line.positionCount = positions;
        line.widthMultiplier = 25f;
        line.material = new Material(Shader.Find("Particles/Additive"));
        line.SetColors(c1, c2);
    }

    IEnumerator WaitForNotify() {
        while (!connect) {
            yield return new WaitForSeconds(.1f);
        }
        ConnectTo();
        if (to.Length == 1) goto end;
        while (!connect1){
            yield return new WaitForSeconds(.1f);
        }
        ConnectTo();
        end:
        yield return null;
    }

    public void Notify()
    {
        if (!connect){
            connect = true;
        }
        else {
            connect1 = true;
        }
    }

    public void Reconnect() {
        connect = false;
        connect1 = false;
    }

    private void ConnectTo(){
        for (int i = 0; i < starCollector.GetComponent<StarCollector>().size(); i++){
            for (int j = 0; j < to.Length; j++){
                if (to[j] != null){
                    if (to[j] == starCollector.GetComponent<StarCollector>().getStar(i)){
                        if (to.Length == 1){
                            SetUpLine(2);
                            line.SetPosition(0, gameObject.transform.position);
                            line.SetPosition(1, to[j].transform.position);
                        }
                        else if (to.Length == 2 && actualTo == 1){
                            SetUpLine(3);
                            line.SetPosition(0, to[0].transform.position);
                            line.SetPosition(1, gameObject.transform.position);
                            line.SetPosition(2, to[1].transform.position);
                        }
                        else if (to.Length == 2 && actualTo == 0) {
                            SetUpLine(2);
                            line.SetPosition(0, gameObject.transform.position);
                            line.SetPosition(1, to[j].transform.position);
                            actualTo++;
                        }
                    }
                }
            }
        }
    }

    private void ConnectFrom() {
        for (int i = 0; i < starCollector.GetComponent<StarCollector>().size(); i++){
            for (int j = 0; j < from.Length; j++){
                if (from[j] != null){
                    if (from[j] == starCollector.GetComponent<StarCollector>().getStar(i)){
                        from[j].GetComponent<StarTaps>().Notify();
                    }
                }
            }
        }
    }







}
