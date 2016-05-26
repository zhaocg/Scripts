using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Healthbar : MonoBehaviour {

    public PlayerContorller player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float percent = player.health / player.health_max;
        this.transform.GetComponent<Image>().fillAmount = percent;
	}
}
