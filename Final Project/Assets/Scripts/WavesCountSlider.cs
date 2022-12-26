using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesCountSlider : MonoBehaviour
{
    private Slider _slider;

    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = SoundManager.Instance.WavesCount;
        SoundManager.Instance.GetWavesCount(_slider.value);
        _slider.onValueChanged.AddListener(value => SoundManager.Instance.GetWavesCount(_slider.value));
    }
}
