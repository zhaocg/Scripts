using UnityEngine;
using System.Collections;

public class MagicProp : MonoBehaviour {

    public bool canDestroy = false;

    //public int MagicNumber = 0;

    //private float Timing = 0;
	
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (canDestroy == true)
        {
            Destroy(this.gameObject, 0.2f);
        }
	
	}
}
