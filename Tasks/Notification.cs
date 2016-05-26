using UnityEngine;
using System.Collections;

public class Notification : MonoSingletion<Notification> {
	

	// Use this for initialization
	void Start () {
		TaskManager.Instance.getEvent += getPrintInfo;
		TaskManager.Instance.finishEvent += finishPrintInfo;
		TaskManager.Instance.rewardEvent += rewardPrintInfo;
		TaskManager.Instance.cancelEvent += cancelPrintInfo;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void getPrintInfo(System.Object sender,TaskEventArgs e)
	{
		Debug.Log ("接受任务" + e.teskID);	
	}

	public void finishPrintInfo(System.Object sender, TaskEventArgs e)
	{
		Debug.Log("完成任务" + e.teskID);
	}

	public void rewardPrintInfo(System.Object sender, TaskEventArgs e)
	{
		Debug.Log("奖励物品" + e.id + "数量 " + e.amount);
	}

	public void cancelPrintInfo(System.Object sender, TaskEventArgs e)
	{
		Debug.Log("取消任务" + e.teskID);
	}
}
