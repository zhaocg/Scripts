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
//using System;
//[Serializable]
public class CanStayArea : MonoBehaviour {
    
    [SerializeField]
    private Color color = new Color(0.5f, 0.5f, 0.3f, 0.3f);
    private float width = 1f;

    private float length = 1f;

    private float height = 1f;
    [SerializeField]
    private bool gizmosOn = true;

	void Start () {
	    
	}
	
	void Update () {
	    
	}

    void OnDrawGizmos()
    {
        if (!gizmosOn) return;
        Matrix4x4 defaultMatrix = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = color;
        Gizmos.DrawCube(Vector3.zero, new Vector3(width, height, length));
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(width, height, length));
        Gizmos.matrix = defaultMatrix;
    }

    void OnSceneGUI()
    {
        GUI.Box(new Rect(100, 100, 100, 100), "Chen");
    }

    public Vector3 RandomAPosition()
    {
        float x = Random.Range(-width / 2, width / 2);
        float y = Random.Range(-height / 2, height / 2);
        float z = Random.Range(-length / 2, length / 2);
        Vector3 _v = new Vector3(x,y,z);
        Vector3 _v_world = this.transform.TransformPoint(_v);
        return _v_world;
    }
}
