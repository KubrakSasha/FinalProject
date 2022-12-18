using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSkills
{
    //public event Action OnSkillPointsChanged;
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
    //private int _skillPoints;
    //public int SkillPoints => _skillPoints;
    public PlayerSkills()
    {
        _unlockSkillTypesList = new List<SkillType>();
        _lockSkillTypesList = new List<SkillType>();
        _lockSkillTypesList = Enum.GetValues(typeof(SkillType)).Cast<SkillType>().ToList();        
    }

    //public void AddSkillPoint() 
    //{
    //    _skillPoints++;
    //    OnSkillPointsChanged?.Invoke();
    //}
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
    //public bool TryUnlockSkill(SkillType type) 
    //{
    //    if (!_unlockSkillTypesList.Contains(type))
    //    {
    //        if (_skillPoints>0)
    //        {
    //            _skillPoints--;
    //            OnSkillPointsChanged?.Invoke();
    //            SetActiveSkill(type);
    //            return true;
    //        }
    //        return false;
    //    }
    //    return false;
    //}
    //private class SkillButton 
    //{
    //    private Transform _transform;
    //    private PlayerSkills _skills;
    //    private PlayerSkills.SkillType _type;

    //    public SkillButton(Transform transform, PlayerSkills skills, SkillType type)
    //    {
    //        _transform = transform;
    //        _skills = skills;
    //        _type = type;
    //    }
    //}
    

}
