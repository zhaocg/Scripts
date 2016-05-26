/******************************************************************************\
* Copyright (C) Tianjin Sharpnow technology, Inc. 2011-2015.				   *
* Sharpnow proprietary. Licensed under GPLv3                                   *
* Available at http://www.gnu.org/licenses/gpl-3.0.en.html					   *
* 版权所有 天津锋时互动科技有限公司 2011-2015									   *
* 锋时互动所有权、软件著作权遵循GPLv3协议										   *
* 详细版权协议信息请参考 http://www.gnu.org/licenses/gpl-3.0.en.html			   *
\******************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class SkillSystem : MonoBehaviour {

    public enum SkillOwnerType
    {
        Player,
        AI,
    }

    [HideInInspector]
    public Unit master;
    public SkillOwnerType ownerType = SkillOwnerType.Player;
    public Skill skillToRelease;

    /// <summary>
    /// 是否拥有已经冷却好的技能
    /// </summary>
    public bool hasCooledSkill
    {
        get {
            if (skillList.Count <= 0) return false;
            for (int i = 0; i < skillList.Count; i++)
            {
                if (!skillList[i].cooling) return true;
            }
            return false;
        }
    }

    /// <summary>
    /// 已经冷却了的技能列表
    /// </summary>
    public List<Skill> CooledList
    {
        get
        {
            List<Skill> _tempList = new List<Skill>();
            if(skillList.Count <=0 || !hasCooledSkill){
                return _tempList;
            }
            
            for (int i = 0; i < skillList.Count; i++)
            {
                if (!skillList[i].cooling)
                    _tempList.Add(skillList[i]);
            }
            return _tempList;
        }
    }


    /// <summary>
    /// 正在冷却的技能列表
    /// </summary>
    public List<Skill> CoolingList
    {
        get
        {
            List<Skill> _tempList = new List<Skill>();
            if (skillList.Count <= 0)
            {
                return _tempList;
            }
            
            for (int i = 0; i < skillList.Count; i++)
            {
                if (skillList[i].cooling)
                    _tempList.Add(skillList[i]);
            }
            return _tempList;
        }
    }

    /// <summary>
    /// 可以被立即使用的技能列表，技能已经冷却而且魔法值充足
    /// </summary>
    public List<Skill> EnabledList
    {
        get
        {
            List<Skill> _tempList = new List<Skill>();
            if (CooledList.Count <= 0)
            {
                return _tempList;
            }

            for (int i = 0; i < CooledList.Count; i++)
            {
                if (CooledList[i].energyCost < master.mana)
                    _tempList.Add(skillList[i]);
            }
            return _tempList;
        }
    }

    /// <summary>
    /// 所有技能列表
    /// </summary>
    public List<Skill> skillList = new List<Skill>();


    void Start()
    {

    }

    /// <summary>
    /// 添加技能
    /// </summary>
    /// <param name="_skill"></param>
    public void AddSkill(Skill _skill)
    {
        if (skillList.Contains(_skill)) return;
        _skill.system = this;
        skillList.Add(_skill);
    }


    /// <summary>
    /// 移除技能
    /// </summary>
    /// <param name="_name"></param>
    public void RemoveSkill(string _name) {
        Skill _skill = FindSkillByName(_name);
        if (_skill != null)
        {
            skillList.Remove(_skill);
        }
    }

    /// <summary>
    /// 通过技能名称寻找技能
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    public Skill FindSkillByName(string _name)
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            if (skillList[i].name_str == _name)
            {
                return skillList[i];
            }
        }
        return null;
    }

    public void ChooseASkill()
    {
        if (ownerType == SkillOwnerType.AI)
        {
            AiAgent ag = this.master as AiAgent;
            //从已冷却的技能列表中随机取出一个技能释放
            //List<Skill> _tSkill = this.CooledList ;
            List<Skill> _tSkill = this.EnabledList;
            if (_tSkill.Count <= 0)
            {
                return;
            }
            int i = Random.Range(0, _tSkill.Count);
            skillToRelease = _tSkill[i];
            skillToRelease.startPoint = ag.shootPoint;
            ag.animController.Play_Shot();
        }
    }

    public void Run()
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            skillList[i].CheckCooling();
        }
    }

    public void ReleaseChoosedSkill()
    {
        if (skillToRelease != null)
        {
            skillToRelease.Instantiate();
            skillToRelease = null;
        }
    }
}
