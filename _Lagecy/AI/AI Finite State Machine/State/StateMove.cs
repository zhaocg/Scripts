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
/// 移动状态：
/// </summary>
public class StateMove : AIState {

    public StateMove()
    {
        this.tag = "move";
    }

    public override void Enter()
    {
        Debug.Log("Should Move");
        //随机寻找下一个路径点
        //////////////////////////////////////////////////////////////////////////
        CanStayArea g = GameObject.Find("CanStayArea").GetComponent<CanStayArea>();
        this.agent.MoveTo(g.RandomAPosition());
        this.agent.navAgent.speed = this.agent.defaultSpeed;
        this.agent.animController.Play_Run();
    }

    public override void Exit()
    {

    }

    public override string CheckTransitions()
    {
        if (agent.navAgent.hasPath)
        {
            if (agent.remainingDistance <= agent.navAgent.stoppingDistance + 0.1f)
            {
                return "idle";
            }
        }

        return base.CheckTransitions();
    }

    public override void Update()
    {
    }
}
