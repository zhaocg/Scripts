using UnityEngine;
using System.Collections;
using System;

public class MonsterActions : MonoBehaviour {

    

    [HideInInspector] public Monster monster;
    public event Action<Spells> OnHitBySpells;



    void OnEnable()
    {
        if(monster == null)
        {
            monster = this.GetComponent<Monster>();
        }

        this.OnHitBySpells += GetHurt;
    }

    void OnDisable()
    {
        this.OnHitBySpells -= GetHurt;
    }

    void Start()
    {
        
    }

    /// <summary>
    /// 攻击行为
    /// </summary>
    public void Attack()
    {

    } 

    public void Idle()
    {

    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Death()
    {

    }

    /// <summary>
    /// 复活
    /// </summary>
    public void Relive()
    {

    }

    public void GetHurt(Spells magic)
    {
        monster.animations.Play_Hit();
        monster.property.LoseHealth(magic.damage);
    }

    public void InvokeOnHitBySpells(Spells s)
    {
        if (OnHitBySpells != null)
        {
            OnHitBySpells(s);
        }
    }

}
