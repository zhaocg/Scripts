using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CardFillController : MonoBehaviour {

    public List<CardFillItem> list = new List<CardFillItem>();
    public Material mat;
    public bool isFilling;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            list[0].StartCoroutine(list[0].FillCoroutine());
            list[1].StartCoroutine(list[1].FillCoroutine());
            list[2].StartCoroutine(list[2].FillCoroutine());
        }
    }

    IEnumerator FillCoroutine()
    {
        isFilling = true;

        float progress = 0;

        float duration = 1f;
        bool reach = false;
        float timer = 0;
        while (!reach)
        {
            timer += Time.deltaTime;
            progress = timer / duration;
            mat.SetFloat("_SliderValue", progress);
            if (progress > 1)
            {
                reach = true;
            }
            yield return null;
        }
        isFilling = false;
    }
}
