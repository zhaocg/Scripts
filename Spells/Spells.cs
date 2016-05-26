using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Xml.Serialization;

public enum Type
{
    Attack,                                 //进攻技能
    Defend                                  //防御技能
}

public enum SpellsType
{
    FireBall,
    IceCone,
    Posion,
    Shield
}


[Serializable]
public class Spells{
    public string name;                         //技能名称
    public SpellsType spellType;
    public string description;                  //技能描述
    public int level;                           //技能等级
    public int energyCost;                      //技能消耗
    public Element element;                     //技能元素种类
    public Type type;                           //技能类别，进攻或者防御
    public int color;                           //技能品质，数值越高则品质越高
    public int castTimes;                       //施法次数
    public int castSustainTime;                 //技能持续时间，主要针对持续性法术
    public float damage;                        //技能伤害
    public float distance;                      //施法距离
    public float damage_type;                   //类型伤害
    public Effect effect;                       //效果
    public float effectProbility;               //效果触发几率
    public float effectTime;                    //效果保持时间
    public float damage_effect;                 //效果伤害
    public string prefabPath;                   //法术预制物体路径

    //public SpellsPrefab prefab;
    //public string itemPath;
    //public Transform spellsPoint;

    public void Cast(Transform startPoint)
    {
        SpellsPrefab sp = (SpellsPrefab)MonoBehaviour.Instantiate(Resources.Load<SpellsPrefab>(prefabPath), startPoint.position, startPoint.rotation);
        sp.spells = this;
        Debug.Log("Cast");
    }
}

