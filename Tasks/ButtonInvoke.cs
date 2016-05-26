using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class ButtonInvoke : MonoBehaviour {
    public UnityEvent onRayMatched;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ButtonEevents()
    {
        onRayMatched.Invoke();
    }
}
