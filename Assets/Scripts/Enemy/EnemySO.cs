using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies")]
public class EnemySO : ScriptableObject
{
    [SerializeField] public float Health;
    [SerializeField] public float Defend;
    [SerializeField] public float MagicRes;
    [SerializeField] public float PhysicRes;
    [SerializeField] public float Attack;
}
