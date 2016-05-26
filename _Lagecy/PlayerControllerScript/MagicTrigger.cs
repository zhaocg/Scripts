using UnityEngine;
using System.Collections;

public class MagicTrigger : MonoBehaviour {

    public bool canMagic = false;
    public int isTrigger=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (isTrigger == 1)
        {
            canMagic = true;
            isTrigger = 0;
          //canMagic = false;
        }
        if (isTrigger > 1)
        {
            canMagic = false;
            isTrigger = 0;
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "MagicBall")
        {
            isTrigger++;
        }
    }
}
