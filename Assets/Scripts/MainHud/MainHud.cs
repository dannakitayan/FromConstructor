using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MainHud : MonoBehaviour
{
    public TMP_Text Ammo;
    public TMP_Text Health;
    [SerializeField] GameObject keyIcon;

    public static Action<string> onHealthSet;
    public static Action<string> onAmmoSet;
    public static Action onGetKey;

    void OnEnable()
    {
        onAmmoSet += AmmoSet;
        onHealthSet += HealthSet;
        onGetKey += KeyIconShow;
    }

    void OnDisable()
    {
        onAmmoSet -= AmmoSet;
        onHealthSet -= HealthSet;
        onGetKey -= KeyIconShow;
    }

    void HealthSet(string text)
    {
        Health.text = text;
    }

    void AmmoSet(string text)
    {
        Ammo.text = text;
    }

    void KeyIconShow()
    {
        keyIcon.SetActive(true);
    }
}
