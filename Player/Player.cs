using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public PlayerProperty property;
    [HideInInspector] public EnergySystem energySystem;
    [HideInInspector] public PlayerActions actions;
    [HideInInspector] public PlayerAnimations animations;
    [HideInInspector] public PlayerSpells spells;
    [HideInInspector] public SpellsSystem spellsSystem;


    public static Player instance;


    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        energySystem = this.GetComponent<EnergySystem>();
        actions = this.GetComponent<PlayerActions>();
        animations = this.GetComponent<PlayerAnimations>();
        spells = this.GetComponent<PlayerSpells>();
        spellsSystem = this.GetComponent<SpellsSystem>();

        //InitializeSkill();
        //InitSpellsSystem();
        //spells.SetSpells(HandType.Left, spellsSystem.spellsList[0]);
        //spells.SetSpells(HandType.Right, spellsSystem.spellsList[1]);
    }


    //private void InitSpellsSystem()
    //{
    //    spellsSystem.AddSpells(new FireSpells());
    //    spellsSystem.AddSpells(new IceCone());
    //}
}

