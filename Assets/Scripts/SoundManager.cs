using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource playerSource;
    [SerializeField] AudioSource weaponSource;
    [SerializeField] AudioSource itemSource;

    void OnEnable()
    {
        onWeaponPlay += WeaponPlay;
        onPlayerPlay += PlayerPlay;
        onItemPlay += ItemPlay;
    }
    void OnDisable()
    {
        onWeaponPlay -= WeaponPlay;
        onPlayerPlay -= PlayerPlay;
        onItemPlay -= ItemPlay;
    }

    public static Action<AudioClip> onWeaponPlay;
    public static Action<AudioClip> onPlayerPlay;
    public static Action<AudioClip> onItemPlay;


    void WeaponPlay(AudioClip sound)
    {
        weaponSource.PlayOneShot(sound);
    }

    void PlayerPlay(AudioClip sound)
    {
        playerSource.PlayOneShot(sound);
    }

    void ItemPlay(AudioClip sound)
    {
        itemSource.PlayOneShot(sound);
    }
}
