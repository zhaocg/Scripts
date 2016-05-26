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

public class StateLaugh : AIState {

    public StateLaugh()
    {
        this.tag = "laugh";
    }

    public override void Enter()
    {
        //Debug.Log("Laugh");
        //this.agent.animController.Play_Laugh(true);
        this.timerEngine.Initialize();
    }

    public override void Exit()
    {

    }

    public override string CheckTransitions()
    {
        //AnimatorStateInfo info = this.agent.anim.GetCurrentAnimatorStateInfo(0);
        if (timerEngine.timer <= 0)
        {
            timerEngine.ResetTimer();
            //this.agent.animController.Play_Laugh(false);
            return "idle";
        }
        return base.CheckTransitions();
    }

    public override void Update()
    {
        this.timerEngine.Run();
    }
}
;