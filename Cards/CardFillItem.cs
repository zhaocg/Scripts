using UnityEngine;
using System.Collections;

public class CardFillItem : MonoBehaviour {

    public bool isFilling;
    public MeshRenderer fillMesh;

    void Start()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(FillCoroutine());
        }
    }
	

    public IEnumerator FillCoroutine()
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
            fillMesh.material.SetFloat("_SliderValue", progress);
            if (progress > 1)
            {
                reach = true;
            }
            yield return null;
        }
        isFilling = false;
    }
}
