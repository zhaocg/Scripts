/******************************************************************************\
* Copyright (C) Tianjin Sharpnow technology, Inc. 2011-2015.				   *
* Sharpnow proprietary. Licensed under GPLv3                                   *
* Available at http://www.gnu.org/licenses/gpl-3.0.en.html					   *
* 版权所有 天津锋时互动科技有限公司 2011-2015									   *
* 锋时互动所有权、软件著作权遵循GPLv3协议										   *
* 详细版权协议信息请参考 http://www.gnu.org/licenses/gpl-3.0.en.html			   *
\******************************************************************************/
using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public float health_max;
    public float health_min;
    
    public float mana_max;
    public float mana_min;

    public float health;
    public float mana;
    
    public float healthRecoverSpeed;
    public float manaRecoverSpeed;
    
    public float castDistance;
    public Animator anim;

    public delegate void MagicEvent(Magic magic);
    public delegate void UnitEvent();
    public MagicEvent onHitByMagic;
    public UnitEvent onDeath;

    public bool isAlive;

	protected virtual void Start () {
	    
	}
	
	protected virtual void Update () {
	    
	}

    public void Recovery()
    {
        if (!isAlive) return;
        if (health < health_max)
        {
            this.health += healthRecoverSpeed * Time.deltaTime;
            if (this.health > health_max)
            {
                this.health = health_max;
            }
        }


        if (mana < mana_max)
        {
            this.mana += manaRecoverSpeed * Time.deltaTime;
            if(this.mana > this.mana_max)
            {
                this.mana = mana_max;
            }
        }
    }

    public void SelfCheck()
    {
        //血量检查
        if (health <= 0)
        {
            health = 0;
            isAlive = false;
            if (this.onDeath != null)
            {
                this.onDeath();
            }
        }

        //魔法值检查
        if(mana <= 0){
            mana = 0;
        }


    }
}
