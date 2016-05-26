/******************************************************************************\
* Copyright (C) Tianjin Sharpnow technology, Inc. 2011-2015.				   *
* Sharpnow proprietary. Licensed under GPLv3                                   *
* Available at http://www.gnu.org/licenses/gpl-3.0.en.html					   *
* 版权所有 天津锋时互动科技有限公司 2011-2015									   *
* 锋时互动所有权、软件著作权遵循GPLv3协议										   *
* 详细版权协议信息请参考 http://www.gnu.org/licenses/gpl-3.0.en.html			   *
\******************************************************************************/


/*
 *	1、在这套AI系统中，AI生成时便已经知道他的确定目标是玩家，所以不需要设计寻找的过程
 *	2、对于远程法师，他们不需要靠近玩家，他们只需要在远处释放技能即可，场景如下：
 *	   一个法师从墙后面跳了出来，--动画：长牙舞爪--动画：idle -- 动画：释放一种法术砸向玩家--移动/Idle--攻击--
 *	   
 *  
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent))]
public class AiAgent : Unit { 
    //搜寻范围
    public float searchRadius;
    public float speedDownDistance;
    //攻击目标
    public GameObject target;
    //发射点
    public Transform shootPoint;

    public bool hasTarget
    {
        get
        {
            if (target != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    //行为状态机
    public AIFSM ai_fsm;

    //技能系统
    [HideInInspector]
    public SkillSystem skillSystem;

    //伙伴列表
    public List<AiAgent> companionList = new List<AiAgent>();

    public float remainingDistance
    {
        get
        {
            if (navAgent != null)
            {
                return Vector3.Distance(transform.position, navAgent.destination);
            }
            else
            {
                return Mathf.Infinity;
            }
        }
    }
    [HideInInspector]
    public NavMeshAgent navAgent;
    public AiAnimController animController;
    [HideInInspector]
    public float defaultSpeed;
    //////////////////////////////////////////////////////////////////////////
    [SerializeField]
    private bool gizmosOn = true;
    [SerializeField]
    private float m_Theta;
    //////////////////////////////////////////////////////////////////////////

    protected override void Start () {
        this.onHitByMagic += CountDamage;
        navAgent = this.GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
        anim = this.GetComponent<Animator>();
        skillSystem = this.GetComponent<SkillSystem>();
        animController = this.GetComponent<AiAnimController>();
        defaultSpeed = navAgent.speed;
        Initialize();
	}
	
	protected override void Update () {
        ai_fsm.Run();
        skillSystem.Run();
        Recovery();
        SelfCheck();
	}

    /// <summary>
    /// 初始化AI
    /// </summary>
    public void Initialize()
    {
        this.health = 100;
        this.mana = 100;
        this.healthRecoverSpeed = 2f;
        this.manaRecoverSpeed = 3f;
        this.health_max = 100;
        this.health_min = 0;
        this.mana_max = 100;
        this.mana_min = 0;
        this.isAlive = true;
        InitializeFSM();
        InitialzeSkill();
    }

    /// <summary>
    /// 初始化状态机
    /// </summary>
    public void InitializeFSM()
    {
        ai_fsm = new AIFSM(AIFSM_Type.EnemyFSM, this);
        StateNotReady _startState = new StateNotReady();
        ai_fsm.AddStateToList(_startState);
        ai_fsm.AddStateToList(new StateIdle());
        ai_fsm.AddStateToList(new StateAttack());
        ai_fsm.AddStateToList(new StateDeath());
        ai_fsm.SetDefaultState(_startState);
    }

    /// <summary>
    /// 初始化技能系统
    /// </summary>
    public void InitialzeSkill()
    {
        skillSystem.master = this;
        skillSystem.AddSkill(new AI_Skill_FireBall());
        skillSystem.AddSkill(new AI_Skill_WaterBall());
    }

    /// <summary>
    /// 移动到指定位置
    /// </summary>
    /// <param name="tarPos"></param>
    public void MoveTo(Vector3 tarPos)
    {
        if (navAgent == null)
        {
            Debug.LogError("No navmesh agent here in the gameobject:" + this.transform.name);
            return;
        }
        navAgent.SetDestination(tarPos);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (gizmosOn)
        {
            DrawSearchCircle();
        }
    }
    protected void DrawSearchCircle()
    {
        if (m_Theta < 0.01f) m_Theta = 0.01f;
        if (m_Theta > 0.5f) m_Theta = 0.5f;

        // 设置矩阵
        Matrix4x4 defaultMatrix = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;

        // 设置颜色
        Color defaultColor = Gizmos.color;
        Gizmos.color = Color.white;

        // 绘制圆环
        Vector3 beginPoint = Vector3.zero;
        Vector3 firstPoint = Vector3.zero;
        for (float theta = 0; theta < 2 * Mathf.PI; theta += m_Theta)
        {
            float x = searchRadius * Mathf.Cos(theta);
            float z = searchRadius * Mathf.Sin(theta);
            Vector3 endPoint = new Vector3(x, 0, z);
            if (theta == 0)
            {
                firstPoint = endPoint;
            }
            else
            {
                Gizmos.DrawLine(beginPoint, endPoint);
            }
            beginPoint = endPoint;
        }

        // 绘制最后一条线段
        Gizmos.DrawLine(firstPoint, beginPoint);

        // 恢复默认颜色
        Gizmos.color = defaultColor;

        // 恢复默认矩阵
        Gizmos.matrix = defaultMatrix;
    }
#endif

    /// <summary>
    /// 寻找附近伙伴
    /// </summary>
    private void FindCompanion()
    {
        //PENDING
    }


    /// <summary>
    /// 设置自身朝向，朝向玩家的位置
    /// </summary>
    public void SetOrientation()
    {
        if (target == null) return;
        this.transform.DOLookAt(new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z), 0.5f);
    }

    void OnCollisionEnter(Collision col)
    {
        Magic magic = col.transform.GetComponent<Magic>();
        if (magic != null)
        {
            TextMesh go = Instantiate(Resources.Load<TextMesh>("Prefabs/damageNum"), col.contacts[0].point, Quaternion.identity) as TextMesh;
            go.GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0);
            go.text = "-"+magic.skill.damage;
            Destroy(go.gameObject, 0.3f);
            if (this.onHitByMagic != null)
            {
                this.onHitByMagic(magic);
            }
        }
    }

    /// <summary>
    /// 伤害计算，根据击中自己的法术的种类来计算伤害
    /// </summary>
    /// <param name="magic"></param>
    void CountDamage(Magic magic)
    {
        this.health -= magic.skill.damage;
    }
}
