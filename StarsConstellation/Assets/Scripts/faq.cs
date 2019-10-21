using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.UI;

public class faq : MonoBehaviour
{
    private Button btn;
    private GameObject pan;
    private RectTransform btnTransform;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        pan = GameObject.Find("FAQ");
        btn = gameObject.GetComponent<Button>();
        btnTransform = btn.GetComponent<RectTransform>();
        anim = pan.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GetComponent<TapGesture>().Tapped += Slidefaq;
    }

    private void OnDisable()
    {
        GetComponent<TapGesture>().Tapped -= Slidefaq;
    }

    void Slidefaq(object s, EventArgs arg0)
    {
        anim.CrossFade("FAQ_in", 1f, 0);
        GameObject.Find("Roll Button").GetComponent<UIRoll>().Hide();
        GameObject.Find("UIPanel").SetActive(false);
        GetComponent<AudioSource>().Play();
    }
}
