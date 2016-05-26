using UnityEngine;
using System.Collections;


/// <summary>
/// 玩家法术控制脚本，该脚本包括：1.法术发射点，把法术发射点看作一个类SpellsCaster,左右手各一个;
/// </summary>
public class PlayerSpells : MonoBehaviour {
    
    public SpellsCaster leftCaster;
    public SpellsCaster rightCaster;

    public Transform LMagicpos;
    public Transform RMagicpos;

    [SerializeField] private Player player;

    public void SetSpells(HandType handside, Spells skill)
    {
        switch (handside)
        {
            case HandType.Left:
                leftCaster.SetSpell(skill);
                break;
            case HandType.Right:
                rightCaster.SetSpell(skill);
                break;
        }
    }


    public void Shoot_lefthand()
    {
        Debug.Log("sp_left");
        if (leftCaster.spell.energyCost > this.player.energySystem.energy_current) return;
        Debug.Log("sp_left1");
        leftCaster.Cast();
    }


    public void Shoot_rightHand()
    {
        Debug.Log("sp_right");
        if (rightCaster.spell.energyCost > this.player.energySystem.energy_current) return;
        rightCaster.Cast();
    }

}
