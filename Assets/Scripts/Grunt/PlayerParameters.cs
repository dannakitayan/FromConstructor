using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class WeaponCluster
{
    public Weapons Weapon;
    public int Ammo;
    public int MaxAmmo;
}


[Serializable]
public class PlayerParameters
{
    int health;
    Weapons currentWeapon = Weapons.NULL;
    List<WeaponCluster> weaponsPack = new List<WeaponCluster>();
    Transform playerPosition;
    Quaternion playerRotation;

    public int Health
    {
        get
        {
            if (health < 0) return 0;
            else return health;
        }

        set
        {
            if (health + value > 100) health = 100;
            health = value;
        }
    }
    public int GetAmmoCount()
    {
        int ammo = 0;
        var weaponList = Cluster(currentWeapon);
        if (weaponList.Count<WeaponCluster>() > 0)
        {
            foreach (var element in weaponList)
            {
                ammo = element.Ammo;
                break;
            }
        }
        return ammo;
    }
    public Weapons CurrentWeapon
    {
        get
        {
            return currentWeapon;
        }

        set
        {
            currentWeapon = value;
        }
    }
    public Transform PlayerPosition
    {
        get
        {
            return playerPosition;
        }

        set
        {
            playerPosition = value;
        }
    }
    public Quaternion PlayerRotation
    {
        get
        {
            return playerRotation;
        }

        set
        {
            playerRotation = value;
        }
    }
    public List<WeaponCluster> WeaponsPack
    {
        get
        {
            return weaponsPack;
        }

        set
        {
            weaponsPack = value;
        }
    }

    #region Cluster
    IEnumerable<WeaponCluster> Cluster(Weapons weapon)
    {
        var weaponList = from w in weaponsPack
                         where w.Weapon == weapon
                         select w;
        return weaponList;
    }
    #endregion

    #region Change an ammo count
    public void ChangeAmmo(int value)
    {
        var weaponList = Cluster(currentWeapon);
        if (weaponList.Count<WeaponCluster>() > 0)
        {
            foreach(var element in weaponList)
            {
                if (element.Ammo + value > element.MaxAmmo) element.Ammo = element.MaxAmmo;
                else element.Ammo += value;
                break;
            }
        }
    }

    public void ChangeAmmo(Weapons weapon, int value)
    {
        var weaponList = Cluster(weapon);
        if (weaponList.Count<WeaponCluster>() > 0)
        {
            foreach (var element in weaponList)
            {
                if (element.Ammo + value > element.MaxAmmo) element.Ammo = element.MaxAmmo;
                else element.Ammo += value;
                break;
            }
        }
    }
    #endregion

    #region A weapon state
    public void AddWeapon(Weapons weapon, int ammo, int maxAmmo)
    {
        if(HaveWeapon(weapon))
        {
            if(currentWeapon == weapon) ChangeAmmo(ammo);
            else ChangeAmmo(weapon, ammo);
        }
        else weaponsPack.Add(new WeaponCluster { Weapon = weapon, Ammo = ammo, MaxAmmo = maxAmmo });
    }

    public bool HaveWeapon(Weapons weapon)
    {
        var weaponList = from w in weaponsPack
                         where w.Weapon == weapon
                         select w;
        Debug.Log(weaponsPack.Count);
        if (weaponList.Count<WeaponCluster>() > 0) return true;
        else return false;
    }
    #endregion
}
