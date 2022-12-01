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
        levelSystem.OnExpirienceChanged += LevelSystem_OnExpirienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
        SetLevelNumber(_levelSystem.GetLevel());
    }
    private void SetLevelNumber(int levelNumber)
    {
        _levelNumber.text = "Level" + (_levelSystem.GetLevel() + 1);
    }
    private void LevelSystem_OnLevelChanged()
    {
        SetLevelNumber(_levelSystem.GetLevel());
    }

    private void LevelSystem_OnExpirienceChanged()
    {
        _bar.localScale = new Vector2(_levelSystem.GetExpiriencePercent(), 1.0f);
    }
}
