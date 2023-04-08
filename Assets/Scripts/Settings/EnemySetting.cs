using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "InGameItems/EnemySetup", order = 4)]
public class EnemySetting : ScriptableObject
{
    public bool CanShoot = true;
    [Range(0, 12)]
    public int Speed = 0;
    [Range(1, 100)]
    public int Endurance = 1;
    [Range(1, 40)]
    public int Power = 1;
    [Range(2, 15)]
    public int Time = 6;
    [Range(1, 200)]
    public int AttractionDistance = 1;
    [Range(1, 200)]
    public int AttackDistance = 1;
    [Header("Animations")]
    public Texture2D[] Walk;
    public Texture2D[] Attack;
    public Texture2D[] Die;
}
