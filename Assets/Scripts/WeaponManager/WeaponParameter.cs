using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "PAK/Weapon parameter", order = 1)]
public class WeaponParameter : ScriptableObject
{
    public int StartAmmo;
    public int MaxAmmo;
    public int MinimumAmmoInMagazine;
    [Space]
    public float TimeBetweenShots;
    [Space]
    public float Range;
    [Space]
    public int Angle;
    [Space]
    public string ShotTriggerName;
    [Space]
    public AudioClip Shot;
}
