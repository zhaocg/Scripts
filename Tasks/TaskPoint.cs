using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml.Linq;


public enum TPtypeState
{
    idle, //闲置状态
    focus,//焦点状态
}

public class TaskPoint : MonoBehaviour {

    public string taskname;
    public Text tasknametext;   
    public TPtypeState tpstate = TPtypeState.idle;
     
    private string taksname;
    TaskTerrain terrain;
	// Use this for initialization
    public void Awake()
    {
        terrain = GameObject.Find("Terrain").GetComponent<TaskTerrain>();
        taskrandmo();
    }
    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {
	}


    /// <summary>
    /// 随机任务ID
    /// </summary>
    public void taskrandmo()
    {
        //随机任务ID并赋值给领取任务中
        int number = Random.Range(0, 4);
        string taskID = "Task" + number.ToString();
        taskname = taskID;

        //显示任务名称
        XElement xe = TaskManager.Instance.rootElement.Element(taskID);
        tasknametext.text = xe.Element("taskName").Value;

        terrain.taskpiont.Add(this.transform.GetComponent<TaskPoint>());
    }
    /// <summary>
    /// 领取任务
    /// </summary>
    public void getTask()
    {
        TaskManager.Instance.GetTask(taskname);
    }

    /// <summary>
    /// 任务点的状态
    /// </summary>
    /// <param name="s"></param>
    
    public void taskPoint(TPtypeState s)
    {
        tpstate = s;
        switch (tpstate)
        {
            case TPtypeState.idle:
                TSphereColor(Color.white);
                break;
            case TPtypeState.focus:
                TSphereColor(Color.green);
                break;
        }
    }

    public void TSphereColor(Color col)
    {
        this.GetComponent<Renderer>().material.color = col;
    }
}
