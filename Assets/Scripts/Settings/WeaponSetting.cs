using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "InGameItems/ImportWeapons", order = 1)]
public class WeaponSetting : ScriptableObject
{
    public bool CanShoot;
    [Range(1,5)]
    public int RepetitionSpeed = 1;
    [Range(1, 10)]
    public int Power = 1;
    [Range(0, 99)]
    public int StartAmmo;
    [Range(0, 99)]
    public int MaxAmmo;
    [Range(1, 5)]
    public int BulletsPerShot = 1;
    public bool ShowBullet
    {
        get
        {
            return !Bullet;
        }
    }

    [Header("Animation")]
    public Texture2D[] Sprites = new Texture2D[5];
    [Header("Bullet sprite")]
    public Texture2D Bullet;
    [Header("Sounds")]
    public AudioClip Shot;
    public AudioClip Empty;
    [Header("Weapon type")]
    public Weapons WeaponType;
}
