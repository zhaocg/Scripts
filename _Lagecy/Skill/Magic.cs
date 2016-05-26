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
using DG.Tweening;

/*
public class Magic : MonoBehaviour {
    public enum MagicType
    {
        ShootInDirection,
        AOE
    }

    public MagicType magicType = MagicType.ShootInDirection;
    public Skill skill;
    public GameObject magicXuli;
    public GameObject magicBall;
    public GameObject magicExp;

    public float lifeTime = 5f;
    public float moveSpeed = 10f;
    public Transform target;

    private GameObject go;
    private Rigidbody rigid;
    private bool exploded;

	void Start () {
        exploded = false;
        Destroy(this.gameObject, lifeTime);
	    go = Instantiate(magicBall, this.transform.position, this.transform.rotation) as GameObject;
        go.transform.parent = this.transform;
        rigid = this.GetComponent<Rigidbody>();
        if (skill.system.ownerType == SkillSystem.SkillOwnerType.AI)
        {
            this.gameObject.layer = LayerMask.NameToLayer("MagicAI");
        }
        else if(skill.system.ownerType == SkillSystem.SkillOwnerType.Player)
        {
            this.gameObject.layer = LayerMask.NameToLayer("MagicPlayer");
        }
	}
	
	void Update () {
        //this.transform.LookAt(target);
	}

    void FixedUpdate()
    {
        if(skill.system.ownerType==SkillSystem.SkillOwnerType.AI)
        {
            this.transform.DOLookAt(target.position, 1f);
        }   
        if(!exploded)
            this.rigid.velocity = transform.forward * moveSpeed;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        Explode();
    }

    public void Explode()
    {
        GameObject prtc2 = Instantiate(magicExp, this.transform.position, this.transform.rotation) as GameObject;
        prtc2.transform.parent = this.transform;
        rigid.velocity = Vector3.zero;
        exploded = true;
        this.gameObject.GetComponent<Collider>().enabled = false;
        Destroy(prtc2, 1.5f);
        Destroy(go);
        Destroy(this.gameObject, 1.6f);
    }

}
 * */

public class Magic : MonoBehaviour
{
    public enum MagicType
    {
        ShootInDirection,
        AOE
    }

    public MagicType magicType = MagicType.ShootInDirection;
    [SerializeField]
    public Skill skill;
    public GameObject magicXuli;
    public GameObject magicBall;
    public GameObject magicExp;
    public GameObject magicExp_simple;

    public float lifeTime = 5f;
    public float moveSpeed = 10f;
    public Transform target;

    //法术的生成点
    public Transform startPoint;

    //速度方向
    [SerializeField]
    private Vector3 velDir;
    
    //生成的物体 
    private GameObject go;
    
    //粒子刚体
    private Rigidbody rigid;
    
    //是否已经爆炸了
    private bool exploded;
    
    //光标位置
    private Transform cursor;

    void Start()
    {
        exploded = false;
        Destroy(this.gameObject, lifeTime);
        go = Instantiate(magicBall, this.transform.position, this.transform.rotation) as GameObject;
        go.transform.parent = this.transform;
        rigid = this.GetComponent<Rigidbody>();
        cursor = GameObject.Find("Cursor").transform;

        if (skill.system.ownerType == SkillSystem.SkillOwnerType.AI)
        {
            this.velDir = transform.forward;
            this.gameObject.layer = LayerMask.NameToLayer("MagicAI");
        }
        else if (skill.system.ownerType == SkillSystem.SkillOwnerType.Player)
        {
            this.velDir = (cursor.position - startPoint.position).normalized;
            this.gameObject.layer = LayerMask.NameToLayer("MagicPlayer");
        }
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        //如果炮弹由AI发射，则使用DOtween调整炮弹移动方向
        if (skill.system.ownerType == SkillSystem.SkillOwnerType.AI)
        {
            this.transform.DOLookAt(target.position, 1f);
            velDir = transform.forward;
        }

        if (!exploded)
            this.rigid.velocity = velDir * moveSpeed;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.transform.tag == "Player")
        {
            Explode_Simple();
        }
        else
        {
            Explode();
        }
        
    }

    public void Explode()
    {
        GameObject prtc2 = Instantiate(magicExp, this.transform.position, this.transform.rotation) as GameObject;
        prtc2.transform.parent = this.transform;
        rigid.velocity = Vector3.zero;
        exploded = true;
        this.gameObject.GetComponent<Collider>().enabled = false;
        Destroy(prtc2, 1.5f);
        Destroy(go);
        Destroy(this.gameObject, 1.6f);
    }

    public void Explode_Simple()
    {
        GameObject prtc2 = Instantiate(magicExp_simple, this.transform.position, this.transform.rotation) as GameObject;
        prtc2.transform.parent = this.transform;
        rigid.velocity = Vector3.zero;
        exploded = true;
        this.gameObject.GetComponent<Collider>().enabled = false;
        Destroy(prtc2, 1.5f);
        Destroy(go);
        Destroy(this.gameObject, 1.6f);
    }
}

