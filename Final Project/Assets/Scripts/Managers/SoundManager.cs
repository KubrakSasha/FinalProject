using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _musicSorce;
    [SerializeField] private AudioSource _effectSorce;
    [SerializeField] private AudioSource _enemySorce;

    public float VolumeSettings;
    public float WavesCount;
    private float _tempCount;

    private void Awake()
    {
        this.AddComponent<AudioSource>();
        _effectSorce = GetComponent<AudioSource>();
        VolumeSettings = PlayerPrefs.GetFloat("Volume");

        WavesCount = PlayerPrefs.GetFloat("WavesCount");
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
        Explosion,

        WellDone,
        FinishHim

    }
    private void Update()// Наверное так не правильно
    {
        VolumeSettings = AudioListener.volume;
        PlayerPrefs.SetFloat("Volume", VolumeSettings);

        _tempCount = WavesCount;
        PlayerPrefs.SetFloat("WavesCount", _tempCount);        
    }
    public void GetWavesCount(float amount)
    {
        WavesCount = amount;
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
    //public void ToggleEffects() 
    //{
    //    _effectSorce.mute = !_effectSorce.mute;
    //}
    public AudioSource Get() 
    {
        return _effectSorce;    
    }    
}
