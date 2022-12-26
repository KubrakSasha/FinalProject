using UnityEngine;
using UnityEngine.UI;

public class VolumeMute : MonoBehaviour
{
    private Toggle _toggle;

    void Start()
    {
        _toggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    //public void Toggle() { SoundManager.Instance.ToggleEffects(); }
}
