using UnityEngine;
using System.Collections;

public class SpellsPrefab : MonoBehaviour {

    public float lifeTime;                          //生存周期

    public Spells spells;

    public string path_Xuli;                        
    public string path_Ball;
    public string path_Exp;
    public string path_ExpSimple;

    private GameObject magicXuli;
    private GameObject magicBall;
    private GameObject magicExp;
    private GameObject magicExp_simple;

    [SerializeField]
    private Vector3 velDir;
    [SerializeField]
    private float moveSpeed = 10f;
    private Rigidbody rigid;
    private bool exploded;
    private GameObject go;
    private Transform cursor;


    public void Start()
    {
        cursor = GameObject.Find("Cursor").transform;
        rigid = GetComponent<Rigidbody>();
        Initialize();
    }

    public void Initialize()
    {
        exploded = false;

        magicXuli = Resources.Load(path_Xuli) as GameObject;
        magicBall = Resources.Load(path_Ball) as GameObject;
        magicExp = Resources.Load(path_Exp) as GameObject;
        magicExp_simple = Resources.Load(path_ExpSimple) as GameObject;

        go = Instantiate(magicBall, this.transform.position, this.transform.rotation) as GameObject;
        go.transform.parent = this.transform;
        this.velDir = (cursor.position - this.transform.position).normalized;
    }

    void FixedUpdate()
    {
        if (!exploded)
            this.rigid.velocity = velDir * moveSpeed;
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
}
