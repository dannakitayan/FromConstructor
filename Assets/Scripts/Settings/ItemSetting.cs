using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "InGameItems/ImportAmmoAndHealth", order = 2)]
public class ItemSetting : ScriptableObject
{
    [Range(1, 100)]
    public int Value = 1;
    public ItemsType Item;
    public AudioClip TakeSound;
}
