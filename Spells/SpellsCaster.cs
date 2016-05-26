using UnityEngine;
using System.Collections;



/// <summary>
/// 将该脚本绑定在发射点上,在PlayerSpells脚本中，拥有对本脚本的引用
/// </summary>
public class SpellsCaster : MonoBehaviour {

    public Spells spell;
    public bool hasSpell
    {
        get { if (spell != null) return true; else return false; }
    }
    

    /// <summary>
    /// 设置发射点的法术
    /// </summary>
    /// <param name="sp"></param>
    public void SetSpell(Spells sp)
    {
        this.spell = sp;
    }

    /// <summary>
    /// 清除发射点的法术
    /// </summary>
    public void ClearSpell()
    {
        this.spell = null;
    }

    /// <summary>
    /// 释放法术
    /// </summary>
    public void Cast()
    {

    }
}
