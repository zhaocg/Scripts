using UnityEngine;
using System.Collections;
using DG.Tweening;
/// <summary>
/// 能量球状态
/// </summary>

public class Energy : MonoBehaviour {

    public float timing = 0;
    public bool canActive = true;
    public bool canRender = false;

    public bool isClone = false;        //是否为克隆体

    private float distanceToTarget;   //两者之间的距离  
   // private bool move=true;
    private energyCreate energycreate;

    public Transform startPoint;
    public Transform endPoint;
    
    //能否开始移动
    public bool canEnergymove = false;

    //是否在移动过程中
    public bool isEnergymove = false;

    //渲染完成后表示魔法球充能完成,完成累计加一个数,这个数是代表可用的能量球
    public int ChargefinishNumber=0;

    // Use this for initialization 
	void Start () {
        startPoint = this.transform;
        if(this.transform.parent!=null)
        {
            energycreate = this.transform.parent.GetComponent<energyCreate>(); 
        }
       

	}	// Update is called once per frame
	void Update () {
        if (canRender)
        {
            changeColor();
        }
	}
    public void changeColor()
    {
        if (canActive)
        {
            timing += Time.deltaTime * 0.75f;
            if (timing <= 0.95f)
            {
                this.GetComponent<Renderer>().material.mainTextureScale = new Vector2(1, 1 - timing);
            }
            else
            {
                canActive = false;
                ChargefinishNumber = 1;
                canEnergymove = true;
            }
        }
    }

    public void restcolor()
    {
        if (canEnergymove)
        {
            StartCoroutine(PathMove());
            canEnergymove = false;
            this.GetComponent<Renderer>().material.mainTextureScale = new Vector2(1, 1);
            timing = 0f;
            canActive = true;
            canRender = false;
            isEnergymove = true;
        }
    }

  /*  void OnDrawGizmos()
    {
        endPoint = GameObject.Find("Target").transform;
        Vector3 s = startPoint.position;
        Vector3 e = endPoint.position;
        Vector3 e0 = new Vector3(e.x, s.y, e.z);

        Vector3 se = e - s;
        Vector3 m = (s + e) / 2;
        Vector3 sm = m - s;
        Vector3 se0 = e0 - s;
        Vector3 ee0 = e0 - e;

        float sc_Length = sm.magnitude * se.magnitude / se0.magnitude;

        //SE
        Gizmos.DrawLine(s, e);

        //SE0
        Gizmos.DrawLine(s, e0);

        //EE0
        Gizmos.DrawLine(e, e0);

        Vector3 c = sc_Length * se0.normalized;
        c = s + c;
        //C.position = c;
        Gizmos.DrawLine((s + e) / 2, c);
        Gizmos.DrawLine(s, c);
        //Gizmos.DrawLine
    }
   */
    IEnumerator PathMove()
    {
        bool reach = false;
        GameObject energy = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        energy.transform.localScale = Vector3.one * 0.1f;
        energy.GetComponent<Renderer>().material.color = Color.blue;
        energy.transform.position = startPoint.position;
        
        Vector3 center = GetCircleCenter();
        float r = Vector3.Distance(startPoint.position, center);
        float duration = 1f;
        float timer = 0;

        while (!reach)
        {
            timer += Time.deltaTime;

            energy.transform.position = Vector3.Slerp(startPoint.position, endPoint.position, timer / duration);
            float remainingDistance = Vector3.Distance(energy.transform.position, endPoint.position);
            if (remainingDistance <= 0.01f)
            {
                reach = true;
                energy.transform.position = endPoint.position;
            }

            Vector3 DirToCenter = (energy.transform.position - center).normalized;
            energy.transform.position = center + DirToCenter * r;
          //  Debug.DrawLine(center, energy.transform.position, Color.red);

            yield return null;
        }

        if (energy != null)
        {
            Destroy(energy);
        }
    }

    Vector3 GetCircleCenter()
    {
        Vector3 s = startPoint.position;
        Vector3 e = endPoint.position;
        Vector3 e0 = new Vector3(e.x, s.y, e.z);

        Vector3 se = e - s;
        Vector3 m = (s + e) / 2;
        Vector3 sm = m - s;
        Vector3 se0 = e0 - s;
        Vector3 ee0 = e0 - e;

        float sc_Length = sm.magnitude * se.magnitude / se0.magnitude;

        Vector3 c = sc_Length * se0.normalized;
        c = s + c;

        return c;
    }
    public void magicballmove()
    {
        StartCoroutine(PathMove());
    }
}
