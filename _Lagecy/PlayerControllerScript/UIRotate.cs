using UnityEngine;
using System.Collections;
using DG.Tweening;
public class UIRotate : MonoBehaviour {
   // static public UIRotate _instance;
    public Transform cameraRotate;

    public Animator m_animator;
    public bool isRotate;
	// Use this for initialization
    void Awake()
    {
       // _instance = this;
    }

	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (isRotate)
        {
            //跟随摄像机的y轴做移动
            this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, cameraRotate.localEulerAngles.y, this.transform.localEulerAngles.z);
        }
	}
}
