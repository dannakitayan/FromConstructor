using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public struct PlayerCluster
{
    public Dictionary<Weapons, int> WeaponsInBag;
    public Weapons CurrentWeapon;
    public int Health;
    public Transform PlayerPosition;
    public Quaternion PlayerRotation;
    public Quaternion HeadRotation;
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
        player.WeaponsInBag.Add(weapon, 0);
    }
}
