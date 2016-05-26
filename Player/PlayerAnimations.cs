using UnityEngine;
using System.Collections;
using DG.Tweening;


public class PlayerAnimations : MonoBehaviour {

    public float handattackvalue = 0f;
    public float HandAnimationSpeed = 0.3f;
    public CameraRay cameraRay;

    [SerializeField] private Animator animator;
    [SerializeField] private Player player;

    private Tween tweens;
	
	// Update is called once per frame
	void Update () {
        animator.SetFloat("HandState", handattackvalue);
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
                player.spells.Shoot_rightHand();
            }
            else
            {
                player.spells.Shoot_lefthand();
            }
        });
    }
}
