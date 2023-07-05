//using SaveLoadSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSaveData : MonoBehaviour
{
    [SerializeField] private PlayerData MyData;

    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        SaveLoad.OnLoadGame += LoadStatus;
        SaveLoad.OnSaveGame += SaveStatus;

        MyData = new PlayerData(transform.position, PlayerStatusController.GetInstance().playerCurrentHealth,
            PlayerStatusController.GetInstance().playerCurrentMana, PlayerStatusController.GetInstance().playerCurrentEnergy,
            PlayerStatusController.GetInstance().currentHealth, PlayerStatusController.GetInstance().currentMana,
            PlayerStatusController.GetInstance().currentEnergy, PlayerStatusController.GetInstance().playerCurrentAttack,
            PlayerStatusController.GetInstance().playerCurrentDefend, PlayerStatusController.GetInstance().playerCurrentSpeed,
            PlayerStatusController.GetInstance().playerCurrentSpAttack, PlayerStatusController.GetInstance().upgradePoint,
            PlayerStatusController.GetInstance().attackPlus, PlayerStatusController.GetInstance().defendPlus,
            PlayerStatusController.GetInstance().healthPlus, PlayerStatusController.GetInstance().agilityPlus,
            PlayerStatusController.GetInstance().spAttackPlus, PlayerStatusController.GetInstance().manaPlus,
            PlayerStatusController.GetInstance().energyPlus, PlayerStatusController.GetInstance().currentAttackPoint,
            PlayerStatusController.GetInstance().currentDefendPoint, PlayerStatusController.GetInstance().currentHealthPoint,
            PlayerStatusController.GetInstance().currentAgilityPoint, PlayerStatusController.GetInstance().currentSpAttackPoint,
            PlayerStatusController.GetInstance().playerLevel, PlayerStatusController.GetInstance().playerCurrentLevelPoint,
            PlayerStatusController.GetInstance().playerMaxLevelPoint);
    }

    private void SaveStatus()
    {
        var transform1 = transform;
        MyData.PlayerPosition = transform1.position;
        MyData.CurrentMana = PlayerStatusController.GetInstance().currentMana;
        MyData.CurrentHealth = PlayerStatusController.GetInstance().currentHealth;
        MyData.CurrentEnergy = PlayerStatusController.GetInstance().currentEnergy;
        MyData.MaxMana = PlayerStatusController.GetInstance().playerCurrentMana;
        MyData.MaxHealth = PlayerStatusController.GetInstance().playerCurrentHealth;
        MyData.MaxEnergy = PlayerStatusController.GetInstance().playerCurrentEnergy;
        MyData.CurrentAttack = PlayerStatusController.GetInstance().playerCurrentAttack;
        MyData.CurrentDefend = PlayerStatusController.GetInstance().playerCurrentDefend;
        MyData.CurrentAgility = PlayerStatusController.GetInstance().playerCurrentSpeed;
        MyData.CurrentSpAttack = PlayerStatusController.GetInstance().playerCurrentSpAttack;
        MyData.CurrentUP = PlayerStatusController.GetInstance().upgradePoint;
        MyData.CurrentAttackPlus = PlayerStatusController.GetInstance().attackPlus;
        MyData.CurrentDefendPlus = PlayerStatusController.GetInstance().defendPlus;
        MyData.CurrentHealthPlus = PlayerStatusController.GetInstance().healthPlus;
        MyData.CurrentAgilityPlus = PlayerStatusController.GetInstance().agilityPlus;
        MyData.CurrentSpAttackPlus = PlayerStatusController.GetInstance().spAttackPlus;
        MyData.CurrentManaPlus = PlayerStatusController.GetInstance().manaPlus;
        MyData.CurrentEnergyPlus = PlayerStatusController.GetInstance().energyPlus;
        MyData.CurrentAttackPoint = PlayerStatusController.GetInstance().currentAttackPoint;
        MyData.CurrentDefendPoint = PlayerStatusController.GetInstance().currentDefendPoint;
        MyData.CurrentHealthPoint = PlayerStatusController.GetInstance().currentHealthPoint;
        MyData.CurrentAgilityPoint = PlayerStatusController.GetInstance().currentAgilityPoint;
        MyData.CurrentSpAttackPoint = PlayerStatusController.GetInstance().currentSpAttackPoint;
        MyData.Level = PlayerStatusController.GetInstance().playerLevel;
        MyData.CurrentLevel = PlayerStatusController.GetInstance().playerCurrentLevelPoint;
        MyData.MaxLevel = PlayerStatusController.GetInstance().playerMaxLevelPoint;

        SaveGameManager.data.CurrentSaveData = MyData;
    }

    private void LoadStatus(SaveData data)
    {
        //MyData = SaveGameManager.data.CurrentSaveData;
        transform.position = data.CurrentSaveData.PlayerPosition;

        PlayerStatusController.GetInstance().currentMana = data.CurrentSaveData.CurrentMana;
        PlayerStatusController.GetInstance().currentHealth = data.CurrentSaveData.CurrentHealth;
        PlayerStatusController.GetInstance().currentEnergy = data.CurrentSaveData.CurrentEnergy;
        PlayerStatusController.GetInstance().playerCurrentMana = data.CurrentSaveData.MaxMana;
        PlayerStatusController.GetInstance().playerCurrentHealth = data.CurrentSaveData.MaxHealth;
        PlayerStatusController.GetInstance().playerCurrentEnergy = data.CurrentSaveData.MaxEnergy;
        PlayerStatusController.GetInstance().playerCurrentAttack = data.CurrentSaveData.CurrentAttack;
        PlayerStatusController.GetInstance().playerCurrentDefend = data.CurrentSaveData.CurrentDefend;
        PlayerStatusController.GetInstance().playerCurrentSpeed = data.CurrentSaveData.CurrentAgility;
        PlayerStatusController.GetInstance().playerCurrentSpAttack = data.CurrentSaveData.CurrentSpAttack;
        PlayerStatusController.GetInstance().upgradePoint = data.CurrentSaveData.CurrentUP;
        PlayerStatusController.GetInstance().attackPlus = data.CurrentSaveData.CurrentAttackPlus;
        PlayerStatusController.GetInstance().defendPlus = data.CurrentSaveData.CurrentDefendPlus;
        PlayerStatusController.GetInstance().healthPlus = data.CurrentSaveData.CurrentHealthPlus;
        PlayerStatusController.GetInstance().agilityPlus = data.CurrentSaveData.CurrentAgilityPlus;
        PlayerStatusController.GetInstance().spAttackPlus = data.CurrentSaveData.CurrentSpAttackPlus;
        PlayerStatusController.GetInstance().manaPlus = data.CurrentSaveData.CurrentManaPlus;
        PlayerStatusController.GetInstance().energyPlus = data.CurrentSaveData.CurrentEnergyPlus;
        PlayerStatusController.GetInstance().currentAttackPoint = data.CurrentSaveData.CurrentAttackPoint;
        PlayerStatusController.GetInstance().currentDefendPoint = data.CurrentSaveData.CurrentDefendPoint;
        PlayerStatusController.GetInstance().currentHealthPoint = data.CurrentSaveData.CurrentHealthPoint;
        PlayerStatusController.GetInstance().currentAgilityPoint = data.CurrentSaveData.CurrentAgilityPoint;
        PlayerStatusController.GetInstance().currentSpAttackPoint = data.CurrentSaveData.CurrentSpAttackPoint;

        PlayerStatusController.GetInstance().playerLevel = data.CurrentSaveData.Level;
        PlayerStatusController.GetInstance().playerCurrentLevelPoint = data.CurrentSaveData.CurrentLevel;
        PlayerStatusController.GetInstance().playerMaxLevelPoint = data.CurrentSaveData.MaxLevel;

        PlayerStatusController.GetInstance().attackBar.SetAttackPoint(data.CurrentSaveData.CurrentAttackPoint);
        PlayerStatusController.GetInstance().defendBar.SetDefendPoint(data.CurrentSaveData.CurrentDefendPoint);
        PlayerStatusController.GetInstance().hpBar.SetHPPoint(data.CurrentSaveData.CurrentHealthPoint);
        PlayerStatusController.GetInstance().agilityBar.SetAgilityPoint(data.CurrentSaveData.CurrentAgilityPoint);
        PlayerStatusController.GetInstance().spAttackBar.SetSpAttackPoint(data.CurrentSaveData.CurrentAgilityPoint);


        Invoke(nameof(EnableController), 0.25f);
    }  

    private void EnableController()
    {
        playerMovement.enabled = true;
    }
}


[System.Serializable]
public struct PlayerData
{
    public Vector3 PlayerPosition;
    public float MaxHealth;
    public float MaxMana;
    public float MaxEnergy;
    public float CurrentHealth;
    public float CurrentMana;
    public float CurrentEnergy;
    public float CurrentAttack;
    public float CurrentDefend;
    public float CurrentAgility;
    public float CurrentSpAttack;
    public float CurrentUP;
    public float CurrentAttackPlus;
    public float CurrentDefendPlus;
    public float CurrentHealthPlus;
    public float CurrentAgilityPlus;
    public float CurrentSpAttackPlus;
    public float CurrentManaPlus;
    public float CurrentEnergyPlus;

    public float CurrentAttackPoint;
    public float CurrentDefendPoint;
    public float CurrentHealthPoint;
    public float CurrentAgilityPoint;
    public float CurrentSpAttackPoint;

    public int Level;
    public int CurrentLevel;
    public int MaxLevel;
        

    public PlayerData(Vector3 playerPosition, float _maxHealth, float _maxMana, float _maxEnergy, float _currentHealth, float _currentMana,
        float _currentEnergy, float _currentAttack, float _currentDefend, float _currentAgility, float _currentSpAttack, float _currentUP,
        float _currentAttackPlus, float _currentDefendPlus, float _currentHealthPlus, float _currentAgilityPlus, float _currentSpAttackPlus,
        float _currentManaPlus, float _currentEnergyPlus, float _currentAttackPoint, float _currentDefendPoint, float _currentHealthPoint,
        float _currentAgilityPoint, float _currentSpAttackPoint, int _level, int _currentLevel, int _maxLevel)
    {
        PlayerPosition = playerPosition;
        MaxHealth = _maxHealth;
        MaxMana = _maxMana;
        MaxEnergy = _maxEnergy;
        CurrentHealth = _currentHealth;
        CurrentMana = _currentMana;
        CurrentEnergy = _currentEnergy;
        CurrentAttack = _currentAttack;
        CurrentDefend = _currentDefend;
        CurrentAgility = _currentAgility;
        CurrentSpAttack = _currentSpAttack;
        CurrentUP = _currentUP;
        CurrentSpAttackPlus = _currentSpAttackPlus;
        CurrentAttackPlus = _currentAttackPlus;
        CurrentDefendPlus = _currentDefendPlus;
        CurrentHealthPlus = _currentHealthPlus;
        CurrentAgilityPlus = _currentAgilityPlus; 
        CurrentManaPlus =  _currentManaPlus;
        CurrentEnergyPlus = _currentEnergyPlus;
        CurrentAttackPoint = _currentAttackPoint;
        CurrentDefendPoint = _currentDefendPoint;
        CurrentHealthPoint = _currentHealthPoint;
        CurrentAgilityPoint = _currentAgilityPoint;
        CurrentSpAttackPoint = _currentSpAttackPoint;

        Level = _level;
        CurrentLevel = _currentLevel;
        MaxLevel = _maxLevel;
    }
}
