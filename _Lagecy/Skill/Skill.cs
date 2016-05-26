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
using System;



[Serializable]
public class Skill {

    //技能系统
    [NonSerialized]
    public SkillSystem system;

    //技能名称
    [SerializeField]
    public string name_str;
    
    //是否是被动技能
    [SerializeField]
    public bool isPassive;
    
    //是否是主动技能
    [SerializeField]
    public bool isActive;
    
    //是否具有AOE伤害
    [SerializeField]
    public bool isAOE;
    
    //是否是进攻技能
    [SerializeField]
    public bool isAttackSkill;
    
    //是否是防御技能
    [SerializeField]
    public bool isDefenseSkill;
    
    //是否对地方释放
    [SerializeField]
    public bool isToEnemy;
    
    //是否对右方释放
    [SerializeField]
    public bool isToPartner;
    
    //是否正在冷却
    [SerializeField]
    public bool cooling;

    //技能冷却时间
    [SerializeField]
    public float cd;
    
    //技能伤害
    [SerializeField]
    public float damage;

    //是否为减速技能
    public bool slowDown;
    
    //减速值
    public float slowDownSpeed;

    //技能能量消耗 
    [SerializeField]
    public float energyCost;
    //技能保持时间
    [SerializeField]
    public float keepTime;

    //冷却计时器
    public float cooling_timer;
    //技能保持计时器
    public float keeping_timer;

    public GameObject ball_xuli;
    public GameObject ball;
    public GameObject ballexplosion;

    //起始点
    public Transform startPoint;

    public Magic magicBall;


    public virtual void Release(Transform sp)
    {

    }

    public virtual void Instantiate()
    {
        if (this.energyCost > system.master.mana) { return; }
        if (system.ownerType == SkillSystem.SkillOwnerType.AI)
        {
            AiAgent ag = system.master as AiAgent;
            Magic go = (Magic)MonoBehaviour.Instantiate(this.magicBall, startPoint.position, startPoint.rotation);
            go.target = ag.target.transform;
            go.skill = this;
            this.cooling = true;
        }

        if (system.ownerType == SkillSystem.SkillOwnerType.Player)
        {
            Magic go = (Magic)MonoBehaviour.Instantiate(this.magicBall, startPoint.position, startPoint.rotation);
            go.skill = this;
            go.startPoint = startPoint;
            this.cooling = true;
        }

        this.system.master.mana -= this.energyCost;
        if (this.system.master.mana < 0)
            this.system.master.mana = 0;
    }

    //检查冷却状态
    public void CheckCooling()
    {
        if (cooling)
        {
            cooling_timer -= Time.deltaTime;
            if (cooling_timer <= 0)
            {
                cooling = false;
                ResetCoolingTimer();
            }
        }
    }

    //技能是否有充足的能量来释放
    public bool CanCast(float playerEnergy)
    {
        if (this.energyCost > playerEnergy)
            return false;
        else
            return true;
    }

    //重置CD计时器
    public void ResetCoolingTimer()
    {
        cooling_timer = this.cd;
    }

    //重置技能持续时间计时器
    public void ResetKeepingTimer()
    {
        keeping_timer = this.keepTime;
    }
}



//////////////////////////////////////////////////////////////////////////
/// <summary>
/// 火球术
/// </summary>
public class AI_Skill_FireBall : Skill
{

    public AI_Skill_FireBall()
    {
        this.name_str = "FireBall";
        this.isPassive = false;
        this.isActive = true;
        this.isAOE = false;
        this.isAttackSkill = true;
        this.isDefenseSkill = false;
        this.isToEnemy = true;
        this.isToPartner = false;

        this.cd = 13f;
        this.damage = 10f;
        this.energyCost = 10;
        this.keepTime = 0;

        this.cooling_timer = this.cd;
        this.keeping_timer = this.keepTime;

        this.ball_xuli = Resources.Load("Prefabs/ParticlePrefabs/fire_xuli") as GameObject;
        this.ball = Resources.Load("Prefabs/ParticlePrefabs/fireball_work") as GameObject;
        this.ballexplosion = Resources.Load("Prefabs/ParticlePrefabs/fireExplosion_work") as GameObject;
        this.magicBall = Resources.Load<Magic>("Prefabs/Magic/Magic_FireBall");
    }

    public override void Instantiate()
    {
        base.Instantiate();
    }
}


//////////////////////////////////////////////////////////////////////////
/// <summary>
/// 水球术
/// </summary>
public class AI_Skill_WaterBall : Skill
{
    public AI_Skill_WaterBall()
    {
        this.name_str = "WaterBall";
        this.isPassive = false;
        this.isActive = true;
        this.isAOE = false;
        this.isAttackSkill = true;
        this.isDefenseSkill = false;
        this.isToEnemy = true;
        this.isToPartner = false;

        this.cd = 10f;
        this.damage = 10f;
        this.energyCost = 20;
        this.keepTime = 0;

        this.cooling_timer = this.cd;
        this.keeping_timer = this.keepTime;

        this.ball_xuli = Resources.Load("Prefabs/ParticlePrefabs/ice_xuli") as GameObject;
        this.ball = Resources.Load("Prefabs/ParticlePrefabs/iceball_work") as GameObject;
        this.ballexplosion = Resources.Load("Prefabs/ParticlePrefabs/iceExplosion_work") as GameObject;
        this.magicBall = Resources.Load<Magic>("Prefabs/Magic/Magic_WaterBall");
    }

    public override void Instantiate()
    {
        base.Instantiate();
    }
}

public class AI_Skill_FireHuohau : Skill
{
    public AI_Skill_FireHuohau()
    {
        this.damage = 1000f;
    }
}