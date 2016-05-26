using UnityEngine;
using System.Collections;
using System;

public class Monster : MonoBehaviour {

    public MonsterProperty property;
    public MonsterActions actions;
    public MonsterAnimations animations;
    public MonsterNavigation navigation;

    public event Action<Spells> OnHitBySpells;

    public void InvokeOnHitBySpells(Spells s)
    {
        if(OnHitBySpells != null)
        {
            OnHitBySpells(s);
        }
    }
}
