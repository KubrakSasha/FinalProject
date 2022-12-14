using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSkills
{    
    public event Action<SkillType> OnSkillActivate;
    public enum SkillType
    {
        LongDistanceRunner,
        FastShoot,
        FastLoader,
        AmmoManiac,
        X2Damage,//
        X2Expirience,//
        MaxHealth,
        Lucky,
        ExplosionBullet,
        PoisonBullet        
    }
    [SerializeField] private List<SkillType> _lockSkillTypesList;
    [SerializeField] private List<SkillType> _unlockSkillTypesList;
    
    public PlayerSkills()
    {
        _unlockSkillTypesList = new List<SkillType>();
        _lockSkillTypesList = new List<SkillType>();
        _lockSkillTypesList = Enum.GetValues(typeof(SkillType)).Cast<SkillType>().ToList();        
    }    
    public void SetActiveSkill(SkillType type) 
    {
        if (!_unlockSkillTypesList.Contains(type)) 
        {
            _unlockSkillTypesList.Add(type);
            _lockSkillTypesList.Remove(type);
            OnSkillActivate?.Invoke(type);
            GameManager.Instance.UpdateGameStates(GameStates.InGame);
        }
    }    
}
