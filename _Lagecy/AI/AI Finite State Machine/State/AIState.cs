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
using Random = UnityEngine.Random;

/// <summary>
/// 状态接口，所有状态都会去实现该接口，重写父集虚方法来定制自身的状态事件
/// </summary>
public interface IAIFS
{
    void Enter();
    void Exit();
    string CheckTransitions();
    void Update();
}

[System.Serializable]
public class AIState : IAIFS
{
    [NonSerialized]
    public AiAgent agent;
    [NonSerialized]
    public AIFSM fsm;
    public string tag;
    public bool allowCheck = true;
    public TimerEngine timerEngine = new TimerEngine();

    public virtual void Enter()
    {
        //DO NOTHING
    }

    public virtual void Exit()
    {
        //DO NOTHING
    }

    public virtual string CheckTransitions()
    {
        return this.tag;
    }

    public virtual void Update()
    {
        //DO NOTHING
    }

    /// <summary>
    /// 时间引擎：用来控制状态行为的时间机制
    /// </summary>
    [Serializable]
    public class TimerEngine
    {
        public float keepTime_min = 2;
        public float keepTime_max = 3;
        [SerializeField]
        public float keepTime;
        [SerializeField]
        public float timer;
        [SerializeField]
        public float forbiddenCheckTime;
        [SerializeField]
        public float forbiddenCheckTimeCounter;

        public TimerEngine()
        {

        }

        public TimerEngine(float min, float max)
        {
            ClampTimeRange(min, max);
        }

        public void ClampTimeRange(float min, float max)
        {
            this.keepTime_max = max;
            this.keepTime_min = min;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            ResetTimer();
            RandomKeepTime();
        }

        /// <summary>
        /// 重置计时器
        /// </summary>
        public void ResetTimer()
        {
            this.timer = keepTime;
        }

        public void ResetForbiddenCheckTimer()
        {
            this.forbiddenCheckTimeCounter = 0;
        }

        /// <summary>
        /// 随机设定保持时间
        /// </summary>
        public void RandomKeepTime()
        {
            keepTime = Random.Range(this.keepTime_min, this.keepTime_max);
        }

        /// <summary>
        /// 运行
        /// </summary>
        public void Run()
        {
            timer -= Time.deltaTime;
        }

        
    }
}





