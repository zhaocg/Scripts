/******************************************************************************\
* Copyright (C) Tianjin Sharpnow technology, Inc. 2011-2015.				   *
* Sharpnow proprietary. Licensed under GPLv3                                   *
* Available at http://www.gnu.org/licenses/gpl-3.0.en.html					   *
* 版权所有 天津锋时互动科技有限公司 2011-2015									   *
* 锋时互动所有权、软件著作权遵循GPLv3协议										   *
* 详细版权协议信息请参考 http://www.gnu.org/licenses/gpl-3.0.en.html			   *
\******************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using System;

public enum AIFSM_Type{
    EnemyFSM,
}

[Serializable]
public class AIFSM {
    
    [SerializeField]
    public AIFSM_Type type;
    
    [SerializeField]
    public AiAgent agent;
    
    public List<AIState> stateList = new List<AIState>();
    
    
    [SerializeField]
    public AIState currentState;
    
    [SerializeField]
    private AIState defaultState;
    
    private AIState destinateState;
   

    private bool firstRun;

    public AIFSM(AIFSM_Type type, AiAgent ag)
    {
        firstRun = true;
        this.type = type;
        this.agent = ag;
    }

    //////////////////////////////////////////////////////////////////////////
    //状态机设置
    //////////////////////////////////////////////////////////////////////////

    public void AddStateToList(AIState state)
    {
        if (stateList.Contains(state)) return;
        state.fsm = this;
        state.agent = this.agent;
        stateList.Add(state);
    }

    public void RemoveStateFromList(AIState state)
    {
        if (!stateList.Contains(state)) return;
        stateList.Remove(state);
    }

    public void SetDefaultState(AIState state)
    {
        defaultState = state;
    }




    //////////////////////////////////////////////////////////////////////////
    //状态机运行
    //////////////////////////////////////////////////////////////////////////

    public void Run()
    {
        //状态机只有在状态数量大于0的时候才会运行
        if (stateList.Count <= 0) return;
        
        //第一次执行的时候触发默认状态的Enter方法
        if (firstRun)
        {
            if (currentState == null) currentState = defaultState;
            if (currentState == null) return;
            else
            {
                currentState.Enter();
                firstRun = false;
            }
        }

        //如果允许跳转检测才会执行下面步骤
        if (currentState.allowCheck)
        {
            AIState oldState = currentState;
            
            string desTag = currentState.CheckTransitions();
            if(oldState.tag != desTag){
                DoTransitionTo(GetState(desTag));
            }
        }

        //状态内部更新
        currentState.Update();
    }

    void DoTransitionTo(AIState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }


    /// <summary>
    /// 通过状态名称获取状态
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    AIState GetState(string tag)
    {
        if (stateList.Count <= 0) return null;
        for (int i = 0; i < stateList.Count; i++)
        {
            if (stateList[i].tag == tag)
            {
                return stateList[i];
            }
        }
        return null;
    }
}
