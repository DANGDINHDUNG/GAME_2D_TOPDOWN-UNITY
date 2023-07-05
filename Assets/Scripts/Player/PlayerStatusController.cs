using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerStatusController : MonoBehaviour
{
    [Header("Player Max Status")]
    [SerializeField] public float playerMaxHealth = 200f;
    [SerializeField] public float playerMaxMana = 200f;
    [SerializeField] public float playerMaxEnergy = 100f;
    [SerializeField] public float playerMaxDefend = 5f;
    [SerializeField] public float playerMaxSpeed = 5f;
    [SerializeField] public float playerMaxAttack = 50f;
    [SerializeField] public float playerMaxSpAttack = 5f;

    [Header("Player Default Status")]
    [SerializeField] public float playerCurrentHealth;
    [SerializeField] public float playerCurrentMana;
    [SerializeField] public float playerCurrentEnergy;
    [SerializeField] public float playerCurrentDefend;
    [SerializeField] public float playerCurrentSpeed;
    [SerializeField] public float playerCurrentAttack;
    [SerializeField] public float playerCurrentSpAttack;
    [SerializeField] public float playerCurrentAttackSpeed = 0.5f;

    [Header("Current status")]
    [SerializeField] public float currentHealth;
    [SerializeField] public float currentMana;
    [SerializeField] public float currentEnergy;


    [Header("Player Upgrade Point")]
    [SerializeField] private float maxPoint = 100f;
    [SerializeField] public float currentAttackPoint = 0f;
    [SerializeField] public float currentDefendPoint = 0f;
    [SerializeField] public float currentHealthPoint = 0f;
    [SerializeField] public float currentAgilityPoint = 0f;
    [SerializeField] public float currentSpAttackPoint = 0f;
    [SerializeField] public float upgradePoint = 10f;

    [Header("Player Upgrade Stats")]
    [SerializeField] public float attackPlus = 0f;
    [SerializeField] public float healthPlus = 0f;
    [SerializeField] public float manaPlus = 0f;
    [SerializeField] public float defendPlus = 0f;
    [SerializeField] public float agilityPlus = 0f;
    [SerializeField] public float spAttackPlus = 0f;
    [SerializeField] public float energyPlus = 0f;

    [Header("Player Level")]
    [SerializeField] public int playerLevel = 1;
    [SerializeField] public int playerCurrentLevelPoint = 0;
    [SerializeField] public int playerMaxLevelPoint = 2000;

    [Header("Upgrade Bar System")]
    [SerializeField] public AttackBar attackBar;
    [SerializeField] public DefendBar defendBar;
    [SerializeField] public HPBar hpBar;
    [SerializeField] public AgilityBar agilityBar;
    [SerializeField] public SpAttackBar spAttackBar;

    private static PlayerStatusController playerInstance;

    public void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);

        if (playerInstance != null) { Destroy(this.gameObject); }
        else
        {
            DontDestroyOnLoad(gameObject);
            playerInstance = this;
            attackBar.SetMaxAttackPoint(maxPoint);
            defendBar.SetMaxDefendPoint(maxPoint);
            hpBar.SetMaxHPPoint(maxPoint);
            agilityBar.SetMaxAgilityPoint(maxPoint);
            spAttackBar.SetMaxSpAttackPoint(maxPoint);

            Calculate();
            currentHealth = playerCurrentHealth;
            currentMana = playerCurrentMana;
            currentEnergy = playerCurrentEnergy;
        }
        
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MenuScene") { Destroy(this.gameObject); }

        Calculate();
        LevelController();
    }

    public static PlayerStatusController GetInstance()
    {
        return playerInstance;
    }

    public void UpgradePoint(int state)
    {
        if (upgradePoint > 0)
        {
            upgradePoint--;
            if (state == 0) // Upgrade attack
            {
                currentAttackPoint++;
                attackPlus += 5;
                attackBar.SetAttackPoint(currentAttackPoint);
            }
            else if (state == 1)    // Upgrade defend
            {
                currentDefendPoint++;
                defendPlus += 3;
                defendBar.SetDefendPoint(currentDefendPoint);
            }
            else if (state == 2)    // Upgrade hp
            {
                currentHealthPoint++;
                healthPlus += 10;
                hpBar.SetHPPoint(currentHealthPoint);
            }
            else if (state == 3)    // Upgrade speed and energy
            {
                currentAgilityPoint++;
                agilityPlus += 0.1f;
                energyPlus += 5;
                agilityBar.SetAgilityPoint(currentAgilityPoint);
            }
            else if (state == 4)    // Upgrade sp attack and mana
            {
                currentSpAttackPoint++;
                spAttackPlus += 5;
                manaPlus += 10;
                spAttackBar.SetSpAttackPoint(currentSpAttackPoint);
            }
        }
        else return;

    }

    void Calculate()
    {
        playerCurrentAttack = playerMaxAttack + attackPlus;
        playerCurrentHealth = playerMaxHealth + healthPlus;
        playerCurrentMana = playerMaxMana + manaPlus;
        playerCurrentEnergy = playerMaxEnergy + energyPlus;
        playerCurrentDefend = playerMaxDefend + defendPlus;
        playerCurrentSpeed = playerMaxSpeed + agilityPlus;
        playerCurrentSpAttack = playerMaxSpAttack + spAttackPlus;
    }

    void LevelController()
    {
        if (playerCurrentLevelPoint >= playerMaxLevelPoint)
        {
            playerLevel++;
            playerCurrentLevelPoint = 0;
            playerMaxLevelPoint += 1000;
            upgradePoint += 10;
        }
    }
}
