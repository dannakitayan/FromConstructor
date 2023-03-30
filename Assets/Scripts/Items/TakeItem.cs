using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : MonoBehaviour
{
    [SerializeField] ItemSetting itemSetting;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch(itemSetting.Item)
            {
                case ItemsType.Ammo:
                    TakeAmmo();
                    break;
                case ItemsType.Medkit:
                    TakeMedkit();
                    break;
                case ItemsType.Key:
                    TakeKey();
                    break;
                case ItemsType.Weapon1: 
                case ItemsType.Weapon2:
                case ItemsType.Weapon3:
                case ItemsType.Weapon4:
                case ItemsType.Weapon5:
                case ItemsType.Weapon6:
                    TakeWeapon(itemSetting.Item);
                    break;
                default:
                    TakeWeapon(itemSetting.Item);
                    break;
            }

        }
    }

    void DisableItemObject()
    {
        SoundManager.onItemPlay?.Invoke(itemSetting.TakeSound);
        gameObject.SetActive(false);
    }

    void TakeMedkit()
    {
        DisableItemObject();
    }

    void TakeAmmo()
    {
        WeaponManager.onAmmoAdd?.Invoke(itemSetting.Value);
        DisableItemObject();
    }

    Weapons GetWeaponFromItem(ItemsType item)
    {
        switch(item)
        {
            case ItemsType.Weapon1:
                return Weapons.Weapon1;
            case ItemsType.Weapon2:
                return Weapons.Weapon2;
            case ItemsType.Weapon3:
                return Weapons.Weapon3;
            case ItemsType.Weapon4:
                return Weapons.Weapon4;
            case ItemsType.Weapon5:
                return Weapons.Weapon5;
            case ItemsType.Weapon6:
                return Weapons.Weapon6;
            default:
                return Weapons.NULL;
        }
    }

    void TakeWeapon(ItemsType item)
    {
        Weapons weapon = GetWeaponFromItem(item);

        if (weapon == Weapons.NULL) return;

        PlayerParameters.AddWeapon(weapon);
        WeaponManager.onChange?.Invoke(weapon);
        DisableItemObject();
    }

    void TakeKey()
    {
        MainHud.onGetKey?.Invoke();
        DisableItemObject();
    }
}
