using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSkillSystem : MonoBehaviour
{
    [SerializeField] public Skills currentSkillToCast;
    [SerializeField] public EquipmentSlotUI[] skills;
    [SerializeField] private Image[] imageSkills;
    [SerializeField] private Image[] coverSkills;
    [SerializeField] private Image[] selectionSkills;
    [SerializeField] private Text[] coolDownTimer;

    private Camera mainCam;
    [SerializeField] private Vector3 mouseWorldPosition;

    [SerializeField] private float manaRechargeRate = 0.25f;

    [SerializeField] private Transform castPoint;

    public ManaBar manaBar;

    private void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Start()
    {
        manaBar = GameObject.FindGameObjectWithTag("ManaBar").GetComponent<ManaBar>();
        manaBar.SetMaxMana(PlayerStatusController.GetInstance().playerCurrentMana);
    }

    private void Update()
    {
        UpdateIconSkill();
        UpdateSkillChange();
        Cooldown();
        mouseWorldPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;

        bool hasEnoughMana = PlayerStatusController.GetInstance().currentMana - currentSkillToCast.skillToCast.ManaCost >= 0f;
        
        if (!currentSkillToCast.castingSkill && IsSkillCastHeldDown() && hasEnoughMana)
        {
            currentSkillToCast.castingSkill = true;
            PlayerStatusController.GetInstance().currentMana -= currentSkillToCast.skillToCast.ManaCost;
            currentSkillToCast.currentCastTimer = currentSkillToCast.skillToCast.Cooldown;
            CastSkill();
        }

        manaBar.SetMana(PlayerStatusController.GetInstance().currentMana);

        if (PlayerStatusController.GetInstance().currentMana < PlayerStatusController.GetInstance().playerCurrentMana /*&& !castingSkill*/ && !IsSkillCastHeldDown())
        {
            PlayerStatusController.GetInstance().currentMana += manaRechargeRate * Time.deltaTime;

            if (PlayerStatusController.GetInstance().currentMana > PlayerStatusController.GetInstance().playerCurrentMana) PlayerStatusController.GetInstance().currentMana = PlayerStatusController.GetInstance().playerCurrentMana;
        }
    }

    void Cooldown()
    {
        for (int i = 0; i < 3; i++)
        {
            if (skills[i].AssignedInventorySlot.ItemData != null)
            {
                if (skills[i].AssignedInventorySlot.ItemData.weaponSkill.castingSkill)
                {
                    skills[i].AssignedInventorySlot.ItemData.weaponSkill.currentCastTimer -= Time.deltaTime;

                    if (skills[i].AssignedInventorySlot.ItemData.weaponSkill.currentCastTimer <= 0)
                    {
                        skills[i].AssignedInventorySlot.ItemData.weaponSkill.castingSkill = false;
                    }
                }
            }
        }
    }

    void CastSkill()
    {
        // Cast our spell
        if (currentSkillToCast.skillToCast.skillType == SkillType.Range)
        {
            Instantiate(currentSkillToCast, castPoint.position, castPoint.rotation);
        }
        else if (currentSkillToCast.skillToCast.skillType == SkillType.Spawn)
        {
            Instantiate(currentSkillToCast, mouseWorldPosition, Quaternion.identity);
        }   
        else if (currentSkillToCast.skillToCast.skillType == SkillType.Melee)
        {
            Transform target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Instantiate(currentSkillToCast, target.position, Quaternion.identity);
        }
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

    private void UpdateIconSkill()
    {
        for (int i = 0; i < 3; i++)
        {
            if (skills[i].AssignedInventorySlot.ItemData != null)
            {
                imageSkills[i].color = Color.white;
                imageSkills[i].sprite = skills[i].AssignedInventorySlot.ItemData.weaponSkill.skillToCast.Thumbnail;
                coverSkills[i].fillAmount = skills[i].AssignedInventorySlot.ItemData.weaponSkill.currentCastTimer / skills[i].AssignedInventorySlot.ItemData.weaponSkill.skillToCast.Cooldown;
                coolDownTimer[i].text = Mathf.Ceil(skills[i].AssignedInventorySlot.ItemData.weaponSkill.currentCastTimer).ToString();
                if (!skills[i].AssignedInventorySlot.ItemData.weaponSkill.castingSkill)
                {
                    coolDownTimer[i].text = "";
                }
            }
            else
            {
                imageSkills[i].color = Color.clear;
                imageSkills[i].sprite = null;
                coolDownTimer[i].text = "";

            }
        }
    }

    private void UpdateSkillChange()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            if (skills[0].AssignedInventorySlot.ItemData != null)
            {
                currentSkillToCast = skills[0].AssignedInventorySlot.ItemData.weaponSkill;
                SkillSelection(0);
            }
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            if (skills[1].AssignedInventorySlot.ItemData != null)
            {
                currentSkillToCast = skills[1].AssignedInventorySlot.ItemData.weaponSkill;
                SkillSelection(1);
            }
        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            if (skills[2].AssignedInventorySlot.ItemData != null)
            {
                currentSkillToCast = skills[2].AssignedInventorySlot.ItemData.weaponSkill;
                SkillSelection(2);
            }
        }

    }

    private void SkillSelection(int skill)
    {
        selectionSkills[0].color = Color.clear;
        selectionSkills[1].color = Color.clear;
        selectionSkills[2].color = Color.clear;
        selectionSkills[skill].color = Color.white;
    }
}
