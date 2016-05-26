using UnityEngine;
using System.Collections;

public class MonsterActions : MonoBehaviour {

    

    [HideInInspector] public Monster monster;

    void OnEnable()
    {
        if(monster == null)
        {
            monster = this.GetComponent<Monster>();
        }

        monster.OnHitBySpells += GetHurt;
    }

    void OnDisable()
    {
        monster.OnHitBySpells -= GetHurt;
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


}
