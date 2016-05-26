using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class energyCreate : MonoBehaviour {
    public List<Energy> ObjPos = new List<Energy>();
    public float time = 0;
    public int energyNumber = 0;
    private bool energyStop=false;
    public int consume = 0;
    int laststone = 0;
    int growStar = 0;

    public GameObject EnergyPrefab;
	
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame 
	void Update () {
        energyPos(growStar);
	}



    /// <summary>
    /// 蓄力魔法能量球
    /// </summary>
    public void energyPos(int energyN)
    {
        //if (energyStop == false)
        //{
           
            time += Time.deltaTime;
            if (time >= 1f && energyN < 10)
            {
                if (!ObjPos[energyN].canEnergymove)
                {
                    ObjPos[energyN].canRender = true;
                    energyNumber = energyN + ObjPos[energyN].ChargefinishNumber;
                    //++energyNumber;
                    // Debug.Log(" first" + energyNumber);
                    //laststone++;
                    time = 0;
                }
                else
                    energyPos(energyN + 1);
           
               // if (energyNumber > 9)
              //  {
                   // energyNumber = 9;
               // }
            }
        //}
    }

    /// <summary>
    /// 使用魔法能量
    /// </summary>
    public void Energys(int consumes, bool isFocus)
    {
        //此时的能量球是不是在聚焦状态下
       

       // int temp = consume;
        if (consume < energyNumber) {
            for (int i = 9; i >= 0 && laststone > 0; i--)
            {
                if (ObjPos[i].canEnergymove)
                {
                    laststone--;
                    ObjPos[i].restcolor();
                    growStar = i; 
                }


            }
            energyNumber = energyNumber - consume;
        }
    }

    public void magicCharge()
    {
        laststone = consume;
        Energys(consume, true);
    }

}
