using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AssetManager : Singleton<AssetManager>
{
    public GameObject BigExplosionPrefab;
    public GameObject SmallExplosionPrefab;
    public GameObject LevelUpPrefab;
    public GameObject PoisonPrefab;
    public GameObject BloodPrefab;

    public GameObject PerkLuckyPrefab;
    public GameObject PerkHealthPrefab;
    public GameObject PerkGodModePrefab;
    public GameObject PerkFreezePrefab;

    public Sprite LockImage;


    public SoundAudioClip[] soundAudioClips;

    [Serializable]
    public class SoundAudioClip 
    {
        public SoundManager.Sound Sound;
        public AudioClip clip;
    }
}
