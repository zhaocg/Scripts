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

public class StateIdle : AIState
{

    public StateIdle()
    {
        this.tag = "idle";
    }

    public override void Enter()
    {
        allowCheck = true;
        this.agent.animController.Play_Idle();
        this.timerEngine.ResetTimer();
        this.timerEngine.keepTime_min = 0.5f;
        this.timerEngine.keepTime_max = 1f;
        this.timerEngine.RandomKeepTime();
    }

    public override void Exit()
    {

    }

    public override string CheckTransitions()
    {
        if (timerEngine.timer <= 0)
        {
            timerEngine.ResetTimer();
            int r = Random.Range(0, 100);
            if (r > 20)
            {
                return "attack";
            }
        }
        return base.CheckTransitions();
    }

    public override void Update()
    {
        this.timerEngine.Run();
        this.agent.SetOrientation();
    }

    
}