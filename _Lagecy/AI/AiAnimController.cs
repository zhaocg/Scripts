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

public class AiAnimController : MonoBehaviour {

    private Animator animator;

	void Start () {
        animator = this.GetComponent<Animator>();
	}
	
	void Update () {
	    
	}

    public void Play_Idle()
    {
        Set("Run", false);
        Set("Shot", false);
    }

    public void Play_Shot()
    {
        Set("Shot", true);
    }

    public void Play_Run()
    {
        Set("Run", true);
    }

    public void Play_Death()
    {
        int r = Random.Range(0, 2);
        if (r == 0)
        {
            Set("death0", true);
        }
        else
        {
            Set("death1", true);
        }
    }

    private void Set(string _name, bool val)
    {
        animator.SetBool(_name, val);
    }

    private void Set(string _name, float val)
    {
        animator.SetFloat(_name, val);
    }
}
