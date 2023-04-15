using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class PlayerCluster
{
    public Dictionary<Weapons, int> WeaponsInBag;
    public Weapons CurrentWeapon;
    public int Health;
    public Transform PlayerPosition;
    public Quaternion PlayerRotation;
    public Quaternion HeadRotation;

    public PlayerCluster()
    {
        CurrentWeapon = Weapons.NULL;
        WeaponsInBag = new Dictionary<Weapons, int>();
        Health = 100;
    }
}

public static class PlayerParameters
{
    static PlayerCluster player = new PlayerCluster();
    public static int Health
    {
        get
        {
            if (player.Health < 0) return 0;
            else return player.Health;
        }

        set
        {
            if (player.Health + value > 100) player.Health = 100;
            else player.Health += value;
        }
    }
    public static Weapons CurrentWeapon
    {
        get
        {
            return player.CurrentWeapon;
        }

        set
        {
            player.CurrentWeapon = value;
        }
    }
    public static Transform PlayerPosition
    {
        get
        {
            return player.PlayerPosition;
        }

        set
        {
            player.PlayerPosition = value;
        }
    }
    public static Quaternion PlayerRotation
    {
        get
        {
            return player.PlayerRotation;
        }

        set
        {
            player.PlayerRotation = value;
        }
    }
    public static Quaternion HeadRotation
    {
        get
        {
            return player.HeadRotation;
        }

        set
        {
            player.HeadRotation = value;
        }
    }

    static void CheckTheDictionary()
    {
        if (player.WeaponsInBag != null) return;
        else player.WeaponsInBag = new Dictionary<Weapons, int>();
    }
    public static bool HaveWeapon(Weapons weapon)
    {
        CheckTheDictionary();
        return player.WeaponsInBag.ContainsKey(weapon);
    }

    public static void AddWeapon(Weapons weapon)
    {
        CheckTheDictionary();
        player.WeaponsInBag.Add(weapon, -1);
    }

    //Save\Load;
    public static void SetPlayerParameters(PlayerCluster cluster)
    {
        player = cluster;
    }

    public static void UpdateWeaponCluster(Weapons weapon, int ammo)
    {
        if (!HaveWeapon(weapon)) return;
        Debug.Log($"{weapon}, {ammo}");
        player.WeaponsInBag[weapon] = ammo;
    }

    public static int LoadAmmoCluster(Weapons weapon)
    {
        player.WeaponsInBag.TryGetValue(weapon, out int value);
        return value;
    }
}
