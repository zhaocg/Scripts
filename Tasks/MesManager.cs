using UnityEngine;
using System.Collections;
using System;
public class MesManager : MonoSingletion<MesManager> {
	//在这里进行事件的处理
    public event EventHandler<TaskEventArgs> checkEvent;
    //检查事件
    public void Check(TaskEventArgs e)
    {
        checkEvent(this, e);
    }
}
