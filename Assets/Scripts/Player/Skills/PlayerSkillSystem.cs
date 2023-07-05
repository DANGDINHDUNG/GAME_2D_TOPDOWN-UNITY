using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkillSystem : MonoBehaviour
{
    [SerializeField] private Skills skillToCast;

    [SerializeField] private float manaRechargeRate = 0.25f;
    [SerializeField] public float timeBetweenCasts = 1f;
    private float currentCastTimer;

    [SerializeField] private Transform castPoint;

    private bool castingSkill = true;

    public ManaBar manaBar;

    void Start()
    {
        manaBar = GameObject.FindGameObjectWithTag("ManaBar").GetComponent<ManaBar>();
        manaBar.SetMaxMana(PlayerStatusController.GetInstance().playerCurrentMana);
    }

    private void Update()
    {
        bool hasEnoughMana = PlayerStatusController.GetInstance().currentMana - skillToCast.skillToCast.ManaCost >= 0f;

        if (!castingSkill && IsSkillCastHeldDown() && hasEnoughMana)
        {
            castingSkill = true;
            PlayerStatusController.GetInstance().currentMana -= skillToCast.skillToCast.ManaCost;
            currentCastTimer = 0;
            CastSkill();
        }

        manaBar.SetMana(PlayerStatusController.GetInstance().currentMana);

        if (castingSkill)
        {
            currentCastTimer += Time.deltaTime;

            if (currentCastTimer > timeBetweenCasts)
            {
                castingSkill = false;
            }
        }

        if (PlayerStatusController.GetInstance().currentMana < PlayerStatusController.GetInstance().playerCurrentMana && !castingSkill && !IsSkillCastHeldDown())
        {
            PlayerStatusController.GetInstance().currentMana += manaRechargeRate * Time.deltaTime;

            if (PlayerStatusController.GetInstance().currentMana > PlayerStatusController.GetInstance().playerCurrentMana) PlayerStatusController.GetInstance().currentMana = PlayerStatusController.GetInstance().playerCurrentMana;
        }
    }

    void CastSkill()
    {
        // Cast our spell
        Instantiate(skillToCast, castPoint.position, castPoint.rotation);
    }

    private bool IsSkillCastHeldDown()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            return true;
        }
        else
        {
            return false;
        }       
    }
}
