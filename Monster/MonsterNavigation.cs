using UnityEngine;
using System.Collections;


[RequireComponent(typeof(NavMeshAgent))]
public class MonsterNavigation : MonoBehaviour {

    [HideInInspector] public Monster monster;

    private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        monster = this.GetComponent<Monster>();
    }


}
