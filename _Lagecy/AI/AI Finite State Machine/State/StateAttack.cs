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

/// <summary>
/// 施法状态
/// </summary>
public class StateAttack : AIState
{
    //额外属性
    //////////////////////////////////////////////////////////////////////////
    private float time_interval;


    //////////////////////////////////////////////////////////////////////////
    
    public StateAttack()
    {
        this.tag = "attack";
    }

    public override void Enter()
    {
        this.timerEngine.keepTime_min = 6f;
        this.timerEngine.keepTime_max = 10f;
        this.timerEngine.RandomKeepTime();
    }

    public override void Exit()
    {

    }

    public override string CheckTransitions()
    {
        if (agent.isAlive == false)
        {
            return "death";
        }

        if (agent.skillSystem.skillToRelease != null)
        {
            if (agent.mana < agent.skillSystem.skillToRelease.energyCost)
            {
                return "idle";
            }
        }
        return base.CheckTransitions();
    }

    public override void Update()
    {
        if (!agent.anim.GetCurrentAnimatorStateInfo(0).IsName("attack_stab"))
            this.allowCheck = true;
        else
            this.allowCheck = false;

        this.agent.skillSystem.ChooseASkill();
        this.agent.SetOrientation();
    }
}