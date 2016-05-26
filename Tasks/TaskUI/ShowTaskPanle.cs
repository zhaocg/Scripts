using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ShowTaskPanle : MonoSingletion<ShowTaskPanle> {

	public Dictionary<string,TaskItem> dictionary  = new Dictionary<string, TaskItem> ();//id, taskItwm

	public GameObject content;//内容
	public Vector2 contenSize;//内容的原始高度

	public GameObject item;//列表项
	public float itemHeight;
	public Vector3 itemLocalPos;


	// Use this for initialization
	void Start () {
		content = transform.Find ("TaskPanle/Content").gameObject;
		contenSize = content.GetComponent<RectTransform> ().sizeDelta;

		item = Resources.Load ("Prefabs/TaskUI/Item")as GameObject;
		itemHeight = item.GetComponent<RectTransform> ().rect.height;
		itemLocalPos = item.transform.localPosition;

		TaskManager.Instance.getEvent += AddItem;
		TaskManager.Instance.cancelEvent += RemoveItem;
        TaskManager.Instance.rewardEvent += RemoveItem;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
    /// 添加列表项
	/// 添加任务的预设体在面板上
	/// </summary>
	/// <param name="sender">Sender</param>
	/// <param name="e">E.</param>
	public void AddItem(System.Object sender, TaskEventArgs e)
	{
		GameObject a = Instantiate(item) as GameObject;
		a.transform.parent = content.transform;
		a.transform.localPosition = new Vector3 (itemLocalPos.x, itemLocalPos.y - dictionary.Count * itemHeight, 0);
		a.transform.localScale = new Vector3 (1f, 1f, 1f);
		TaskItem t = a.GetComponent<TaskItem> ();
		dictionary.Add(e.teskID,t);//通过字典向面板内添加内容
        //添加奖励
		t.Init(e);
		if (contenSize.y <= dictionary.Count * itemHeight) {
			content.GetComponent<RectTransform> ().sizeDelta = new Vector2 (contenSize.x, dictionary.Count * itemHeight);

		}

	}

    /// <summary>
    /// 移除列表项
    /// 移除面板调整其余面板位置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	public void RemoveItem(System.Object sender,TaskEventArgs e)
	{
		if (dictionary.ContainsKey (e.teskID)) {
			TaskItem t = dictionary [e.teskID];
			dictionary.Remove (e.teskID);
			Destroy (t.gameObject);
			//调整位置
			int count = 0;
			foreach (KeyValuePair<string, TaskItem> kv in dictionary)
			{
				kv.Value.gameObject.transform.localPosition = new Vector3(itemLocalPos.x, itemLocalPos.y - count * itemHeight, 0);
				count++;
			}

			if (contenSize.y <= dictionary.Count * itemHeight)//调整内容的高度     
				content.GetComponent<RectTransform>().sizeDelta = new Vector2(contenSize.x, dictionary.Count * itemHeight);
			else
				content.GetComponent<RectTransform>().sizeDelta = contenSize;      
		}
	}
}
