using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ManaBar : MonoBehaviour {

    public Image m_Fill;
    //魔法蓄力时间
    public float Timing = 0.2f;
    //增长速度
    public float timeSpeed = 0;
    public PlayerContorller player;
    //public CameraRay m_cameraRay;
    // Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float cur_mana = player.mana;
        float percent = cur_mana / player.mana_max;
        m_Fill.transform.GetComponent<Image>().fillAmount = percent;
	}
}
