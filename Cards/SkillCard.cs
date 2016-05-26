using UnityEngine;
using DG.Tweening;
using System;
using VRStandardAssets.Utils;

public class SkillCard : MonoBehaviour {
    public enum Type
    {
        FireBall,
        IceCone,
        PosionKnife,
        Shield
    }
    public SpellsType type = SpellsType.FireBall;       //卡牌类型
    public Spells spells;                               //卡牌上的法术

    public Transform objpos;                            //卡牌特效位置
    public bool canCreatemagic = false;                 //是否可以聚集能量
    public Transform SpecialEffects;                    //特效
    
    public int indexInList = 0;

    public int energyIndex = 0;
    
    
    //记录特效开始的位置
    public Vector3 StartSpecialPos;


    public bool isActive
    {
        get
        {
            return m_isActive;
        }

        set
        {
            m_isActive = value;
            if (value)
            {
                cardCollider.enabled = true;
            }
            else
            {
                cardCollider.enabled = false;
            }
        }
    }
    
    private bool m_isActive;
    private VRInteractiveItem interactiveItem;
    private SkillCardManager skillCardManager;
    private SkillCardAnimation cardAnimations;
    private Rigidbody rigid;
    private MeshRenderer cardMesh;
    private BoxCollider cardCollider;

    void OnEnable()
    {
        if (interactiveItem == null) interactiveItem = this.GetComponent<VRInteractiveItem>();
        skillCardManager = SkillCardManager.instance;
        interactiveItem.OnOver += OnRayOver;
        interactiveItem.OnOut += OnRayOut;
        interactiveItem.OnClick += OnPadClick;
        interactiveItem.OnDoubleClick += OnPadDoubleClick;
    }
    

	void Start () {
        //获取卡片管理器实例
        cardAnimations = this.GetComponent<SkillCardAnimation>();
        cardCollider = this.GetComponent<BoxCollider>();
        cardMesh = this.GetComponent<MeshRenderer>();
        rigid = this.GetComponent<Rigidbody>();
        //记录特效初始位置
        if (SpecialEffects!=null)
        {
            StartSpecialPos = SpecialEffects.transform.position;
        }
        isActive = true;
        Initialize();
	}


    /// <summary>
    /// 特效移动控制
    /// </summary>
    /// <param name="ismove"></param>
    public void SpecialEffectsMove(bool ismove)
    {
        if (SpecialEffects != null)
        {
            if (ismove)
            {
                SpecialEffects.gameObject.SetActive(true);
                SpecialEffects.DOMove(objpos.transform.position, 0.5f);
            }
            else
            {
                SpecialEffects.DOMove(StartSpecialPos, 0.5f);
                SpecialEffects.gameObject.SetActive(false);
            }
        }
    }


    /// <summary>
    /// 卡牌初始化
    /// </summary>
    public void Initialize()
    {
        spells = skillCardManager.spellsSystem.spellsContainer.GetSpellsByType(type);
    }

    public void Reset(Transform targetPoint)
    {
        ResetRigidState();
        ResetTransform(targetPoint);
    }

    public void ResetTransform(Transform targetPoint)
    {
        this.transform.position = targetPoint.position;
        this.transform.rotation = targetPoint.rotation;
    }

    public void ResetAlpha(float a)
    {
        Color c = this.cardMesh.material.color;
        //重置卡牌的alpha值
        this.cardMesh.material.color = new Color(c.r, c.g, c.b, a);
    }

    public void ResetRigidState()
    {
        this.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<Rigidbody>().isKinematic = true;
    }



    /////////////////////////////////////////////////////////////
    //               Motions                                   
    /////////////////////////////////////////////////////////////
    public void DropIn(int index)
    {
        Debug.Log("index:" + index);
        Transform targetPos = skillCardManager.cards_anchors[2];
        
        Reset(targetPos);
        cardCollider.enabled = true;
        this.transform.localRotation = targetPos.localRotation;
        this.transform.position = targetPos.position + new Vector3(0f, 0.2f, 0f);
        this.transform.localScale = Vector3.one * 1.24f;
        this.ResetAlpha(0);
        this.transform.DOMove(targetPos.position, 0.2f);
        this.cardMesh.material.DOFade(1f, 0.2f);
    }

    public void DropOut()
    {
        this.cardAnimations.Fall();

        SkillCard c = skillCardManager.RandomANewCard();
        c.indexInList = this.indexInList;
        c.DropIn(c.indexInList);
    }

    /////////////////////////////////////////////////////////////
    //               EVENTS                                    
    /////////////////////////////////////////////////////////////



    /// <summary>
    /// 当卡牌被射线照射
    /// </summary>
    public void OnRayOver()
    {
        if (isActive)
        {
            SpecialEffectsMove(true);
            canCreatemagic = true;
        }
    }


    /// <summary>
    /// 当射线从卡牌上移出
    /// </summary>
    public void OnRayOut()
    {
        SpecialEffectsMove(false);
        canCreatemagic = false;
        skillCardManager.currentSelect = null;
    }
    

    /// <summary>
    /// 点击事件发生时，如果该卡片是玩家正在注视的卡牌，则执行吟唱过程
    /// </summary>
    public void OnPadClick()
    {
        if(canCreatemagic)
            Player.instance.energySystem.EnergyCost(energyIndex, spells.energyCost);

        //卡牌失去重力掉落
        DropOut();
    }


    public void OnPadDoubleClick()
    {

    }

}
