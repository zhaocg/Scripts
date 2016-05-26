using UnityEngine;
using System.Collections;

public enum AreaTpye
{
    River,   //河流
    mountain,//山
    Forest,  //森林
}
public class TaskArea : MonoBehaviour {
    //区域等级
    //区域坐标
    float objx;
    float objy;
    float objz;

    BoxCollider obj;

    public GameObject prefab;

    void Awake()
    {
    }
	// Use this for initialization
	void Start () {
        //etAreaPos();
        obj = this.GetComponent<BoxCollider>();

	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.childCount<2)
        {
            getAreaPos();
        }
	}

    public void getAreaPos()
    {
        objx = Random.Range(this.GetComponent<BoxCollider>().bounds.center.x - this.GetComponent<BoxCollider>().bounds.extents.x, this.GetComponent<BoxCollider>().bounds.center.x + this.GetComponent<BoxCollider>().bounds.extents.x);
        objy = Random.Range(this.GetComponent<BoxCollider>().bounds.center.y, this.GetComponent<BoxCollider>().bounds.center.y + this.GetComponent<BoxCollider>().bounds.extents.y);
        objz = Random.Range(this.GetComponent<BoxCollider>().bounds.center.z - this.GetComponent<BoxCollider>().bounds.extents.z, this.GetComponent<BoxCollider>().bounds.center.z + this.GetComponent<BoxCollider>().bounds.extents.z);
        //objx = this.transform.position.x;
        //objy = this.transform.position.y;
        //objz = this.transform.position.z;

        Vector3 Pos = new Vector3(objx, objy, objz);
        GameObject clone = Instantiate(prefab, Pos, Quaternion.identity)as GameObject;
        clone.transform.parent = this.transform;
    }
}
