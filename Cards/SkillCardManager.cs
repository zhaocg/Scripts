using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;
using System.Collections.Generic;

public class SkillCardManager : MonoBehaviour {

    [HideInInspector] public SkillCard currentSelect;

    public static SkillCardManager instance;

    public SpellsSystem spellsSystem;

    public List<SkillCard> cards_show;
    public List<SkillCard> cards_all;
    public List<Transform> cards_anchors;

    public List<GameObject> cards;

    public LinkedList<SkillCard> cards_deckLink = new LinkedList<SkillCard>();


    [SerializeField]
    private CameraRay m_CameraRay;
    [SerializeField]
    private VRInput m_VrInput;
    private Transform m_CardsRoot;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //cards_deckLink.AddLast(cards_show[0]);
        //cards_deckLink.AddLast(cards_show[1]);
        //cards_deckLink.AddLast(cards_show[2]);
    }

    void OnEnable()
    {
        m_CameraRay.OnRaycasthit += OnRaycastHit;
    }

    void OnDisable()
    {
        m_CameraRay.OnRaycasthit -= OnRaycastHit;
    }
    

    void OnRaycastHit(RaycastHit hit)
    {
        SkillCard card = hit.collider.GetComponent<SkillCard>();
        currentSelect = card;
    }


    /// <summary>
    /// 随机生成一张新卡牌
    /// </summary>
    /// <returns></returns>
    public SkillCard RandomANewCard()
    {
        int i = Random.Range(0, cards.Count);
        Debug.Log(i);
        SkillCard card = Instantiate(cards[i], this.transform.position, Quaternion.identity) as SkillCard;
        card.transform.parent = this.transform;
        card.Initialize();
        return card;
    }
    

}
