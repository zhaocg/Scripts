using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System;

/// <summary>
/// 任务管理
/// </summary>
public class TaskManager : MonoSingletion<TaskManager> {

    public Dictionary<string, Task> dictionary = new Dictionary<string, Task>();//id,task
	//获取xml里的任务事件
    public XElement rootElement;

    public event EventHandler<TaskEventArgs> getEvent;   //接受任务时，更新任务到任务面板等操作
    public event EventHandler<TaskEventArgs> finishEvent;//完成任务时，提示任务完成等操作
    public event EventHandler<TaskEventArgs> rewardEvent;//得到奖励是，显示获取的物品等操作
    public event EventHandler<TaskEventArgs> cancelEvent;//取消任务时，显示提示消息等操作
	// Use this for initialization  
	void Start () {
        MesManager.Instance.checkEvent += CheckTask;
        rootElement = XElement.Load(Application.dataPath + "/FightSystem/Scripts/GameTask/Task.xml");//得到根元素

	}

    /// <summary>
    /// 获取任务
    /// </summary>
    /// <param name="taskID"></param>
    public void GetTask(string taskID)
    {
        if (!dictionary.ContainsKey(taskID))
        {
            Task t = new Task(taskID);
            dictionary.Add(taskID,t);
            TaskEventArgs e = new TaskEventArgs();
            
            e.teskID = taskID;
            getEvent(this, e);
            
        }
    }

    /// <summary>
    /// 检测任务
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void CheckTask(System.Object sender, TaskEventArgs e)
    {
        foreach (KeyValuePair<string, Task> kv in dictionary)
        {
            kv.Value.Check(e);
        }
    }

    /// <summary>
    /// 完成任务
    /// </summary>
    /// <param name="e"></param>
    public void FinishTask(TaskEventArgs e)
    {
        finishEvent(this, e);
    }
    /// <summary>
    /// 获取奖励
    /// </summary>
    /// <param name="e"></param>
    public void GetReward(TaskEventArgs e)
    {
        if (dictionary.ContainsKey(e.teskID))
        {
            Task t = dictionary[e.teskID];

			int randmorewardnumber = UnityEngine.Random.Range (0, 4);
            for (int i = 0; i < t.taskConditions.Count; i++)
            {
                TaskEventArgs a = new TaskEventArgs();
				a.id = t.taskRewards[i].id;
				a.amount = t.taskRewards[i].amount;
                a.teskID = e.teskID;
                rewardEvent(this, a);
            }
        }
        dictionary.Remove(e.teskID);
    }
    /// <summary>
    /// 取消任务
    /// </summary>
    /// <param name="e"></param>

    public void CancelTask(TaskEventArgs e)
    {
        if(dictionary.ContainsKey(e.teskID))
        {
            cancelEvent(this,e);
            dictionary.Remove(e.teskID);
        }
    }
}
