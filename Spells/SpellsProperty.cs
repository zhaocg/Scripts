using UnityEngine;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[Serializable]
[XmlRoot("Spell")]
public class SpellsProperty{

    public enum Type
    {
        Attack,                                 //进攻技能
        Defend                                  //防御技能
    }
    
    [XmlAttribute("name")]
    [SerializeField]
    public string name;                         //技能名称
    
    [XmlElement("description")]
    [SerializeField]
    public string description;                  //技能描述
    
    [XmlElement("level")]
    public int level;                           //技能等级
    
    [XmlElement("energyCost")]
    public int energyCost;                      //技能消耗
    
    [XmlElement("element")]
    public Element element;                     //技能元素种类
    
    [XmlElement("type")]
    public Type type;                           //技能类别，进攻或者防御
    
    [XmlElement("color")]
    public int color;                           //技能品质，数值越高则品质越高
    
    [XmlElement("castTimes")]
    public int castTimes;                       //施法次数
    
    [XmlElement("castSustainTime")]
    public int castSustainTime;                 //技能持续时间，主要针对持续性法术
    
    [XmlElement("damage")]
    public float damage;                        //技能伤害
    
    [XmlElement("distance")]
    public float distance;                      //施法距离
    
    [XmlElement("damage_type")]
    public float damage_type;                   //类型伤害
    
    [XmlElement("effect")]
    public Effect effect;                       //效果
    
    [XmlElement("effectProbility")]
    public float effectProbility;               //效果触发几率
    
    [XmlElement("effectTime")]
    public float effectTime;                    //效果保持时间
    
    [XmlElement("damage_effect")]
    public float damage_effect;                 //效果伤害
}



