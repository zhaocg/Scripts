using UnityEngine;
using System.Collections;

public class MonsterAnimations : MonoBehaviour {
    [SerializeField] private Monster monster;
    [SerializeField] private Animator animator;
    
	public void Play_Hit()
    {
        animator.SetTrigger("hit");
    }
}
