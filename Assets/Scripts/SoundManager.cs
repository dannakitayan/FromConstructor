using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource playerSource;
    [SerializeField]
    AudioSource weaponSource;

    void OnEnable()
    {
        onWeaponPlay += WeaponPlay;
        onPlayerPlay += PlayerPlay;
    }
    void OnDisable()
    {
        onWeaponPlay -= WeaponPlay;
        onPlayerPlay -= PlayerPlay;
    }

    public static Action<AudioClip> onWeaponPlay;
    public static Action<AudioClip> onPlayerPlay;

    void WeaponPlay(AudioClip sound)
    {
        weaponSource.PlayOneShot(sound);
    }

    void PlayerPlay(AudioClip sound)
    {
        playerSource.PlayOneShot(sound);
    }
}
