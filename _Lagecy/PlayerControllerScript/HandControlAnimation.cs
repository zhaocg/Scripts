using UnityEngine;
using System.Collections;
using DG.Tweening;
public class HandControlAnimation : MonoBehaviour {

     Animator m_animator;

    public float handattackvalue = 0;

    public float HandAnimationSpeed = 0;
    
    private Tween tweens;

    public PlayerContorller player;

    public CameraRay cameraRay;
	// Use this for initialization
	void Start () {
        m_animator = this.GetComponent<Animator>();
	}

    /// <summary>
    /// 控制动画的播发
    /// </summary>
    /// <param name="targetValue"></param>
    public void HandAttack(float targetValue)
    {
        tweens = DOTween.To(() => handattackvalue, x => handattackvalue = x, targetValue, HandAnimationSpeed);
        tweens.OnComplete(delegate
        { 
            DOTween.To(() => handattackvalue, x => handattackvalue = x, 0, HandAnimationSpeed);
            if (handattackvalue < 0)
            {
                player.Shoot_rightHand();
            }
            else
            {
                player.Shoot_lefthand();
            }
            
        });
    }
}
