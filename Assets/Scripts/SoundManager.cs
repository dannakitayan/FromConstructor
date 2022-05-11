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
    [SerializeField]
    AudioSource explosionSource;

    void OnEnable()
    {
        onWeaponPlay += WeaponPlay;
        onPlayerPlay += PlayerPlay;
        onExplosionPlay += ExplosionPlay;
    }
    void OnDisable()
    {
        onWeaponPlay -= WeaponPlay;
        onPlayerPlay -= PlayerPlay;
        onExplosionPlay -= ExplosionPlay;
    }

    public static Action<AudioClip> onWeaponPlay;
    public static Action<AudioClip> onPlayerPlay;
    public static Action onExplosionPlay;


    void WeaponPlay(AudioClip sound)
    {
        weaponSource.PlayOneShot(sound);
    }

    void PlayerPlay(AudioClip sound)
    {
        playerSource.PlayOneShot(sound);
    }

    void ExplosionPlay()
    {
        explosionSource.Play();
    }
}
