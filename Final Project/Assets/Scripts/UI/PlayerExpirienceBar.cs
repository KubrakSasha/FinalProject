using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerExpirienceBar : MonoBehaviour
{
    [SerializeField] private Text _levelNumber;
    [SerializeField] private Transform _bar;

    private LevelSystem _levelSystem;
    
    public void Setup (LevelSystem levelSystem)
    {        
        _levelSystem = levelSystem;
        _levelSystem.OnExpirienceChanged += LevelSystem_OnExpirienceChanged;
        _levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
        SetLevelNumber(_levelSystem.Level);
    }
    private void SetLevelNumber(int levelNumber)
    {
        _levelNumber.text = "Level" + (_levelSystem.Level + 1);
    }
    private void LevelSystem_OnLevelChanged()
    {
        SetLevelNumber(_levelSystem.Level);
    }

    private void LevelSystem_OnExpirienceChanged()
    {

        _bar.localScale = new Vector2(_levelSystem.GetExpiriencePercent(), 1.0f);
    }
    private void OnDestroy()
    {
        _levelSystem.OnExpirienceChanged -= LevelSystem_OnExpirienceChanged;
        _levelSystem.OnLevelChanged -= LevelSystem_OnLevelChanged;
    }

}
