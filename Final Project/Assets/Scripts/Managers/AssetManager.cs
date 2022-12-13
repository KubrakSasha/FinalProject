using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AssetManager : Singleton<AssetManager>
{
    public SoundAudioClip[] soundAudioClips;

    [Serializable]
    public class SoundAudioClip 
    {
        public SoundManager.Sound Sound;
        public AudioClip clip;
    }
}
