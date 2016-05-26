using UnityEngine;
using System.Collections;
using System;
/// <summary>
/// 任务事件参数
/// </summary>
public class TaskEventArgs : EventArgs {

    public string teskID;//当前任务的ID
    public string id;//发生时间的对象的ID（例如敌人，商品）
    public int amount;//数量
}
