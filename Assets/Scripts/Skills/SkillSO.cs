using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills")]
public class SkillSO : ScriptableObject
{
    public Sprite Thumbnail;
    public float DamageAmount = 10f;
    public SkillType skillType;
    public float ManaCost = 5f;
    public float LifeTime = 2f;
    public float Speed = 15f;
    public bool DamageOfTime = false;
    public float Cooldown;
    // Status effects
    //Time between casts
    // Skill elements
}

public enum SkillType
{
    Range,
    Summon,
    Spawn,
    Melee,
    Heal,
    Buff
}
