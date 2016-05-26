using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class SystemConfig : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Recenter();
	}
	
    public void Recenter()
    {
        InputTracking.Recenter();
    }
}
