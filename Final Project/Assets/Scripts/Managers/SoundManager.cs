using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _musicSorce;
    [SerializeField] private AudioSource _effectSorce;
    [SerializeField] private AudioSource _enemySorce;

    public float VolumeSettings;

    
    //[SerializeField] private Slider _soundSlider;

    private void Awake()
    {
        this.AddComponent<AudioSource>();
        _effectSorce = GetComponent<AudioSource>();
        VolumeSettings = PlayerPrefs.GetFloat("Volume");
    }
   
    public enum Sound 
    {
        PistolShot,
        RifleShot,
        ShotgunShot,

        PistolReloading,
        RifleReloading,
        ShotgunReloading,

        EnemyHit,
        EnemiDie,
        EnemyMoving,
        Explosion

    }
    private void Update()// Наверное так не правильно
    {
        VolumeSettings = AudioListener.volume;
        PlayerPrefs.SetFloat("Volume", VolumeSettings);
        
    }

    public void PlaySound(Sound sound) 
    {
        _effectSorce.PlayOneShot(GetAudioClip(sound));
    }
    public void PlayEnemySound(Sound sound, AudioSource source) 
    {
        source.PlayOneShot(GetAudioClip(sound));
    }
    public AudioClip GetAudioClip(Sound sound) 
    {
        foreach (AssetManager.SoundAudioClip  clip in AssetManager.Instance.soundAudioClips)
        {
            if (clip.Sound == sound)
            {
                return clip.clip;
            }
        }
        Debug.LogError("There is NO such SOUND");
        return null;
    }
    public void ChangeVolume(float value) 
    {
        AudioListener.volume = value;
    }
    public void ToggleEffects() 
    {
        _effectSorce.mute = !_effectSorce.mute;
    }
    public AudioSource Get() 
    {
        return _effectSorce;    
    }
    //public void 
    



}
