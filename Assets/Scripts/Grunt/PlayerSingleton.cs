using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton
{
    private static PlayerSingleton instance;
    public PlayerParameters Player;

    private PlayerSingleton()
    {
        Player = new PlayerParameters();
    }

    public static PlayerSingleton Get()
    {
        if (instance == null)
            instance = new PlayerSingleton();
        return instance;
    }
}
