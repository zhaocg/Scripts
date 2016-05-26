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
using UnityEngine.UI;

public class AI_UI : MonoBehaviour {

    public Image health_img;
    public Image mana_img;

    private AiAgent agent;
	void Start () {
        agent = this.transform.parent.GetComponent<AiAgent>();
	}
	
	void Update () {
        this.transform.LookAt(this.agent.target.transform);
        this.health_img.rectTransform.sizeDelta = new Vector2(this.agent.health, 100);
        this.mana_img.rectTransform.sizeDelta = new Vector2(this.agent.mana, 100);
	}
}
