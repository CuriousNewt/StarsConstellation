using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.UI;

public class faqx : MonoBehaviour
{
    private Button btn;
    private GameObject pan;
    private RectTransform btnTransform;
    private Animator anim;
    private GameObject panelToShow;

    // Use this for initialization
    void Start()
    {
        pan = GameObject.Find("FAQ");
        btn = gameObject.GetComponent<Button>();
        btnTransform = btn.GetComponent<RectTransform>();
        anim = pan.GetComponent<Animator>();
        panelToShow = GameObject.Find("UIPanel");
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
        StartCoroutine("WaitForAnimation");
    }

    private IEnumerator WaitForAnimation()
    {
        anim.CrossFade("FAQ_out", 1f, 0);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);
        panelToShow.SetActive(true);
    }
}
