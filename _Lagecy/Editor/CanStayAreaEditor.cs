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
using UnityEditor;
[CustomEditor(typeof(CanStayArea))]
public class CanStayAreaEditor : Editor {
    CanStayArea area;
    void OnEnable()
    {
        area = (CanStayArea)target;
    }

    void OnSceneGUI()
    {
        //绘制文本框
        Handles.Label(area.transform.position + Vector3.up * 2,
                   area.transform.name);
    }
}
