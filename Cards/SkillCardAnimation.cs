using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SkillCardAnimation : MonoBehaviour {
    public Rigidbody rigid;
    public MeshRenderer mesh;
    public MeshRenderer specialEffect;
    public float fadeInDuration;
    public float fadeOutDuration;

	// Use this for initialization
	void Start () {
        rigid = this.GetComponent<Rigidbody>();

	}
	

    public void MoveIn(Transform targetPoint)
    {
        mesh.material.DOFade(1, fadeInDuration);
    }

    public void Fall()
    {
        rigid.useGravity = true;
        rigid.isKinematic = false;
        StartCoroutine(DestroyAnimationCoroutine());
    }

    public IEnumerator DestroyAnimationCoroutine()
    {
        yield return new WaitForSeconds(2f);
        this.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    public void PourEngergyBar()
    {

    }


}
