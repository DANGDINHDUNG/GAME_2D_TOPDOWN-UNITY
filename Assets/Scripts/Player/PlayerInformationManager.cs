using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInformationManager : MonoBehaviour
{
    [Header("Player Current Stats")]
    [SerializeField] private TextMeshProUGUI playerHP;
    [SerializeField] private TextMeshProUGUI playerMana;
    [SerializeField] private TextMeshProUGUI playerEnergy;
    [SerializeField] private TextMeshProUGUI upgradePoint;

    [Header("Player Informations")]
    [SerializeField] private TextMeshProUGUI attack;
    [SerializeField] private TextMeshProUGUI hp;
    [SerializeField] private TextMeshProUGUI energy;
    [SerializeField] private TextMeshProUGUI defend;
    [SerializeField] private TextMeshProUGUI agility;
    [SerializeField] private TextMeshProUGUI spAttack;
    [SerializeField] private TextMeshProUGUI mana;

    [Header("Player Level")]
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI currentLevel;



    [Header("Player script manager")]
   public PlayerMovement playerMovement;
   public PlayerCombat playerCombat;
   public PlayerSkillSystem playerSkillSystem;
   public PlayerStatus playerStatus;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerSkillSystem = GetComponent<PlayerSkillSystem>();
        playerStatus = GetComponent<PlayerStatus>();

    }

    void Update()
    {
        playerHP.text = "HP: " + PlayerStatusController.GetInstance().currentHealth + "/" + PlayerStatusController.GetInstance().playerCurrentHealth;
        playerMana.text = "MP: " + PlayerStatusController.GetInstance().currentMana + "/" + PlayerStatusController.GetInstance().playerCurrentMana;
        playerEnergy.text = "STA: " + PlayerStatusController.GetInstance().currentEnergy + "/" + PlayerStatusController.GetInstance().playerCurrentEnergy;
        upgradePoint.text = "Upgrade point:  " + PlayerStatusController.GetInstance().upgradePoint;

        attack.text =   PlayerStatusController.GetInstance().playerMaxAttack + "   +   (" + PlayerStatusController.GetInstance().attackPlus + ")";
        hp.text =       PlayerStatusController.GetInstance().playerMaxHealth + "   +   (" + PlayerStatusController.GetInstance().healthPlus + ")";
        mana.text =     PlayerStatusController.GetInstance().playerMaxMana + "   +   (" + PlayerStatusController.GetInstance().manaPlus + ")";
        energy.text =   PlayerStatusController.GetInstance().playerMaxEnergy + "   +   (" + PlayerStatusController.GetInstance().energyPlus + ")";
        agility.text =  PlayerStatusController.GetInstance().playerMaxSpeed + "   +   (" + PlayerStatusController.GetInstance().agilityPlus + ")";
        defend.text =   PlayerStatusController.GetInstance().playerMaxDefend + "   +   (" + PlayerStatusController.GetInstance().defendPlus + ")";
        spAttack.text = PlayerStatusController.GetInstance().playerMaxSpAttack + "   +   (" + PlayerStatusController.GetInstance().spAttackPlus + ")";

        level.text = PlayerStatusController.GetInstance().playerLevel.ToString();
        currentLevel.text = "Lvl: " + PlayerStatusController.GetInstance().playerCurrentLevelPoint + "/" + PlayerStatusController.GetInstance().playerMaxLevelPoint;
    }
}