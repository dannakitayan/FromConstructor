using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] GameObject weapon1;
    [SerializeField] GameObject weapon2;
    [SerializeField] GameObject weapon3;
    [SerializeField] GameObject weapon4;
    [SerializeField] GameObject weapon5;
    [SerializeField] GameObject weapon6;

    public Weapons StartWith;

    public static Action onShot;

    void Start()
    {
        SetWeapon(StartWith);
    }

    void SetWeapon(Weapons weapon)
    {
        switch(weapon)
        {
            case Weapons.Weapon1:
                weapon1.SetActive(true);
                break;
            case Weapons.Weapon2:
                weapon2.SetActive(true);
                break;
            case Weapons.Weapon3:
                weapon3.SetActive(true);
                break;
            case Weapons.Weapon4:
                weapon4.SetActive(true);
                break;
            case Weapons.Weapon5:
                weapon5.SetActive(true);
                break;
            case Weapons.Weapon6:
                weapon6.SetActive(true);
                break;
        }
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            onShot?.Invoke();
        }
    }
}
