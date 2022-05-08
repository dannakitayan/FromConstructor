using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MainHud : MonoBehaviour
{
    public TMP_Text Ammo;
    public TMP_Text Health;

    public static Action<string> onHealthSet;
    public static Action<string> onAmmoSet;

    void OnEnable()
    {
        onAmmoSet += AmmoSet;
        onHealthSet += HealthSet;
    }

    void OnDisable()
    {
        onAmmoSet -= AmmoSet;
        onHealthSet -= HealthSet;
    }

    void HealthSet(string text)
    {
        Health.text = text;
    }

    void AmmoSet(string text)
    {
        Ammo.text = text;
    }
}
