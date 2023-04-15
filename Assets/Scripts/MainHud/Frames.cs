using System;
using System.Collections.Generic;
using UnityEngine;

public class Frames : MonoBehaviour
{
    [SerializeField] GameObject Base, SaveLoad, Music, NextLevel, UI;

    public static Action onShowLoading, onShowUI;

    void Awake()
    {
        onShowLoading += ShowLoading;
        onShowUI += ShowUI;
    }

    void OnDestroy()
    {
        onShowLoading -= ShowLoading;
        onShowUI -= ShowUI;
    }

    void ShowLoading()
    {
        UI.SetActive(false);
        NextLevel.SetActive(true);
    }

    void ShowUI()
    {
        NextLevel.SetActive(false);
        UI.SetActive(true);
    }
}
