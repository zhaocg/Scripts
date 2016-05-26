using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using VRStandardAssets.Utils;
using System;

public class CameraRay : MonoBehaviour {

    public event Action<RaycastHit> OnRaycasthit;
    //射线所要照射的层
    public  LayerMask m_ExclusionLayers;
    //在Scene视图中是否要显示射线
    public bool m_ShowDebugRay = false;
    public bool isfocus = false;

    [SerializeField] private VRInput m_VrInput;
    private Coroutine m_coroutine;
    private VRInteractiveItem m_CurrentInteractible;
    private VRInteractiveItem m_LastInteractible;
    private Transform m_Camera;                         //获得摄像机
    

    //public EnergySystem Energy_system;

    //public Transform magicTarget;

    //获取卡牌固定的下标
    public int cardIndex;

    private void OnEnable()
    {
        m_VrInput.OnClick += HandleClick;
        m_VrInput.OnDoubleClick += HandleDoubleClick;
        m_VrInput.OnUp += HandleUp;
        m_VrInput.OnDown += HandleDown;
    }


    private void OnDisable()
    {
        m_VrInput.OnClick -= HandleClick;
        m_VrInput.OnDoubleClick -= HandleDoubleClick;
        m_VrInput.OnUp -= HandleUp;
        m_VrInput.OnDown -= HandleDown;
    }

    void Start () {
        m_Camera = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        
        //射线检测
        Ray camera_ray = new Ray(m_Camera.position, m_Camera.forward);
        RaycastHit camera_hit;
        bool hitsomthing = Physics.Raycast(camera_ray, out camera_hit, Mathf.Infinity, m_ExclusionLayers);
        if (hitsomthing)
        {
            VRInteractiveItem interactible = camera_hit.collider.GetComponent<VRInteractiveItem>();
            m_CurrentInteractible = interactible;

            // If we hit an interactive item and it's not the same as the last interactive item, then call Over
            if (interactible && interactible != m_LastInteractible)
                interactible.Over();

            // Deactive the last interactive item 
            if (interactible != m_LastInteractible)
                DeactiveLastInteractible();

            m_LastInteractible = interactible;

            if (OnRaycasthit != null)
                OnRaycasthit(camera_hit);
        }
        else
        {

            DeactiveLastInteractible();
            m_CurrentInteractible = null;
        }


        if (m_ShowDebugRay)
        {
            Debug.DrawRay(m_Camera.position, m_Camera.forward * 100, Color.blue);
        }
	}

    //public void ChangeLeftHandSkill()
    //{
    //    if (playerMagicball.LMagicpos.transform.childCount > 0 && playerMagicball.LMagicpos.transform.childCount < 2)
    //    {
    //        leftCard_Values = m_carDetailed.magicExpendValue;
    //        playerMagicball.LMagicpos.transform.GetComponentInChildren<MagicProp>().canDestroy = true;
    //        canLTime = true;
    //    }
    //}

    //public void ChangeRightHandSkill()
    //{
    //    if (playerMagicball.RMagicpos.transform.childCount > 0 && playerMagicball.LMagicpos.transform.childCount < 2)
    //    {
    //        Debug.Log(playerMagicball.RMagicpos.transform.childCount);
    //        rightCard_Values = m_carDetailed.magicExpendValue;
    //        playerMagicball.RMagicpos.transform.GetComponentInChildren<MagicProp>().canDestroy = true;
    //        canRTime = true;
    //    }
    //}

    //IEnumerator CursorOnTiming()
    //{

    //    if (isFilling) yield break;

    //    isFilling = true;

    //    bool isFocus = false;
    //    float duration = 1f;
    //    while (!isFocus)
    //    {
    //        Times += Time.deltaTime;
    //        if (Times < duration)
    //        {
    //            m_Cursor_fill.fillAmount = Times / duration;
    //        }
    //        else
    //        {
    //            Times = 0f;
    //            isFocus = true;
    //        }

    //        yield return null;
    //    }

    //    //isFilling = false;
    //}

    //public void GetSpellsFromPanel()
    //{
    //    magicTarget = m_carDetailed.objpos;
    //    cardIndex = m_carDetailed.energyIndex;                                 //动态获取当前卡牌对应能量球的下标
    //    if (!isFilling)
    //    {
    //        m_coroutine = StartCoroutine(CursorOnTiming());
    //        Energy_system.EnergyCost(m_carDetailed.energyIndex, m_carDetailed.spells.property.energyCost);
    //    }
    //}

    private void DeactiveLastInteractible()
    {
        if (m_LastInteractible == null)
            return;

        m_LastInteractible.Out();
        m_LastInteractible = null;
    }


    private void HandleUp()
    {
        if (m_CurrentInteractible != null)
            m_CurrentInteractible.Up();
    }


    private void HandleDown()
    {
        if (m_CurrentInteractible != null)
            m_CurrentInteractible.Down();
    }


    private void HandleClick()
    {
        if (m_CurrentInteractible != null)
            m_CurrentInteractible.Click();
    }


    private void HandleDoubleClick()
    {
        if (m_CurrentInteractible != null)
            m_CurrentInteractible.DoubleClick();

    }
}
