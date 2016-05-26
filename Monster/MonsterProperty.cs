using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class MonsterProperty
{

    public enum Type
    {

    }

    [SerializeField] public string name;                                                         //怪物名称
    [SerializeField] public string description;                                                  //描述
    [SerializeField] public Type type;                                                           //怪物类型
    [SerializeField] public Element element;                                                     //元素种类
    [SerializeField] public bool fly;                                                            //是否为飞行怪
    [SerializeField] public bool isBoss;                                                         //是否为boss
    [SerializeField] public int level;                                                           //等级
    [SerializeField] public float health;                                                        //生命
    [SerializeField] public float damage;                                                        //伤害
    [SerializeField] public float frequency;                                                     //攻击间隔
    [SerializeField] public float distance;                                                      //攻击距离
    [SerializeField] public float blockProbability;                                              //格挡几率
    
    public void LoadDataFromFile()
    {

    }

    public void SaveDataToFile()
    {

    }


    /// <summary>
    /// 损失血量
    /// </summary>
    /// <param name="num"></param>
    public void LoseHealth(float num)
    {
        this.health -= num;
    }


    /// <summary>
    /// 恢复血量
    /// </summary>
    /// <param name="num"></param>
    public void RecoverHealth(float num)
    {
        this.health += num;
    }
}
