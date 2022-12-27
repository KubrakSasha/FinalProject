using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider _slider;
    
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = SoundManager.Instance.VolumeSettings;
        SoundManager.Instance.ChangeVolume(_slider.value);
        _slider.onValueChanged.AddListener(value => SoundManager.Instance.ChangeVolume(value));
    }   
}
