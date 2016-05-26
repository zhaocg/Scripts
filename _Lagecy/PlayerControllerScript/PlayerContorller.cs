using UnityEngine;
using System.Collections;


public class PlayerContorller : Unit{

    public SkillSystem p_skillSystem;
    
    public Skill skill_LeftHand;
    public Skill skill_RightHand;

    public Transform LMagicpos;
    public Transform RMagicpos;

    public HandControlAnimation animController;

    public int ballNumber = 0;

    public int ballNumber1 = 0;

    public CameraRay skillnumberball;

    protected override void Start()
    {
        this.animController = this.GetComponent<HandControlAnimation>();
        p_skillSystem = this.transform.GetComponent<SkillSystem>();

        this.onHitByMagic += CountDamage;
        
        InitializePlayer();
    }

    protected override void Update()
    {
        p_skillSystem.Run();
        Recovery();
        SelfCheck();
        //Changeball();
        magicballpos();
    }

    private void InitializePlayer()
    {
        this.health = 100;
        this.health_max = 100;
        this.health_min = 0;

        this.mana = 20;
        this.mana_max = 100;
        this.mana_min = 0;
        
        this.manaRecoverSpeed = 10f;
        this.healthRecoverSpeed = 10f;
        this.isAlive = true;
        InitializeSkill();
    }

    private void InitializeSkill()
    {
        p_skillSystem.master = this;
        p_skillSystem.AddSkill(new AI_Skill_FireBall());
        p_skillSystem.AddSkill(new AI_Skill_WaterBall());
    }

    //public void Changeball()
    //{
    //    skill_LeftHand = p_skillSystem.skillList[skillnumberball.leftCard_Values];
    //    skill_RightHand = p_skillSystem.skillList[skillnumberball.rightCard_Values];
    //}

    public void magicballpos()
    {
        //skill_LeftHand.Release(skill_LeftHand.ball.transform);
    }
    public void Shoot_lefthand()
    {
        if (skill_LeftHand.energyCost > this.mana) return;
        skill_LeftHand.startPoint = LMagicpos;
        skill_LeftHand.Instantiate();
      
    }
    public void Shoot_rightHand()
    {
        if (skill_RightHand.energyCost > this.mana) return;
        skill_RightHand.startPoint = RMagicpos;
        skill_RightHand.Instantiate();
    }

    void CountDamage(Magic magic)
    {
        this.health -= magic.skill.damage;
        if (this.health < 0)
        {
            this.health = 0;
        }
    }
}
