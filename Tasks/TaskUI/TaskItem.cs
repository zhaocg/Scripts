using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour{

    public Task task;//对应的任务逻辑

    public Text taskName;//任务名称
    public Text caption; //描述
    public Text buttonText;//按钮文字提示

    public GameObject process; //处理对象
    public Vector3 processLocalPos;//过程对象的坐标
    public List<TaskItemProcess> processText = new List<TaskItemProcess>();

    public GameObject reward;       //奖励对象 
    public Vector3 rewardLocalPos;  //在面板中的位置
    public List<TaskItemReward> rewardText = new List<TaskItemReward>();


    /// <summary>
    /// 添加奖励
    /// </summary>
    /// <param name="e"></param> 
    public void Init(TaskEventArgs e)
    {
		process = Resources.Load("Prefabs/TaskUI/Process") as GameObject; 
		//process.transform.localScale = new Vector3 (1, 1, 1);
		processLocalPos = process.transform.localPosition;

        reward = Resources.Load("Prefabs/TaskUI/Reward") as GameObject;
        rewardLocalPos = reward.transform.localPosition;

        task = TaskManager.Instance.dictionary[e.teskID];
        task.taskItem = this;

        taskName.text = task.taskName;
        caption.text = task.caption;

        for (int i = 0; i < task.taskConditions.Count; i++)
        {
		   GameObject processobj = Instantiate(process) as GameObject;
          
            processobj.transform.parent = transform;
			processobj.transform.localPosition = new Vector3(processLocalPos.x + 10f, processLocalPos.y - processText.Count * 20, 0);
            processobj.transform.localScale = new Vector3(1f, 1f, 1f);

			TaskItemProcess tP = processobj.GetComponent<TaskItemProcess>();
            processText.Add(tP);

            tP.id.text = task.taskConditions[i].id;
            tP.now.text = task.taskConditions[i].nowAmount.ToString();
            tP.target.text = task.taskConditions[i].targetAmount.ToString();
        }


        for (int i = 0; i < task.taskRewards.Count; i++)
        {
            GameObject rewardobj = Instantiate(reward) as GameObject;      
            rewardobj.transform.parent = transform;
            rewardobj.transform.localPosition = new Vector3(rewardLocalPos.x, rewardLocalPos.y - rewardText.Count * 20, 0);
            rewardobj.transform.localScale = new Vector3(1f, 1f, 1f);

            TaskItemReward tR = rewardobj.GetComponent<TaskItemReward>();
            rewardText.Add(tR);

            tR.id.text = task.taskRewards[i].id;
            tR.amount.text = task.taskRewards[i].amount.ToString();
        }
    }

    //修改条件的当前进度
    public void Modify(string id, int amount)
    {
        for (int i = 0; i < processText.Count; i++)
        {
            if (processText[i].id.text == id)
                processText[i].now.text = amount.ToString();
        }
    }
    //任务是否完成
    public void Finish(bool isFinish)
    {
        if (isFinish)
            buttonText.text = "完成了";
        else
            buttonText.text = "未完成";
    }
    //任务奖励
    public void Reward()
    {
        if (buttonText.text == "完成了")
            task.Reward();
    }
    //取消任务
    public void Cancel()
    {
        task.Cancel();
    }
}
