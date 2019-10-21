using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TouchScript.Gestures;
using System;
using TouchScript;

public class UIRoll : MonoBehaviour {
	private Button but;
	private GameObject pan;
	private RectTransform btnTransform;
	bool shown = false;
	private Animator anim;

	// Use this for initialization
	void Start () {
        pan = GameObject.Find("UIPanel");
		but = gameObject.GetComponent<Button>();
		btnTransform = but.GetComponent<RectTransform>();
		anim = pan.GetComponent<Animator>();
	}

    private void OnEnable()
    {
        GetComponent<TapGesture>().Tapped += OnClick;
    }

    private void OnDisable()
    {
        GetComponent<TapGesture>().Tapped -= OnClick;
    }

	private void OnClick(object s, EventArgs arg0)
	{
		if (shown)
		{
			btnTransform.Rotate(0, 0, 180);
			anim.CrossFade("UISlideOut", 1f, 0);
			shown = !shown;
		}
		else {
			btnTransform.Rotate(0, 0, -180);
			anim.CrossFade("UISlide", 1f, 0);
			shown = !shown;
		}
        GetComponent<AudioSource>().Play();
	}

    public void Show() {
        if (!shown) {
            btnTransform.Rotate(0, 0, -180);
            anim.CrossFade("UISlide", 1f, 0);
            shown = !shown;
        }
    }

    public void Hide() {
        if (shown) {
            btnTransform.Rotate(0, 0, 180);
            anim.CrossFade("UISlideOut", 1f, 0);
            shown = !shown;
        }
    }
}
