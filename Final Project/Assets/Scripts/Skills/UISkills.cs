using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerSkills;

public class UISkills : MonoBehaviour
{
    private PlayerSkills _playerSkills;
    [SerializeField] private GameObject _skills;

    [SerializeField] private Image _lockImage;
    //private LevelSystem _levelSystem;

    //private List<SkillButton> _buttons;
    private void Awake()//Почему то нужно что бы на старте панель Skills была активна
    {
        //_skills.SetActive(false);
        GameManager.OnGameStatesChanged += OnSkillsMenuActive;
        transform.Find("LongDistanceRunner").GetComponent<Button>().onClick.AddListener(delegate { SetSkillEnable(SkillType.LongDistanceRunner); });
        //transform.Find("LongDistanceRunner").GetComponent<Button>().onClick.AddListener(ChangeImageToLock);

        transform.Find("FastShoot").GetComponent<Button>().onClick.AddListener(delegate { SetSkillEnable(SkillType.FastShoot); });
        transform.Find("FastLoader").GetComponent<Button>().onClick.AddListener(delegate { SetSkillEnable(SkillType.FastLoader); });
        transform.Find("AmmoManiac").GetComponent<Button>().onClick.AddListener(delegate { SetSkillEnable(SkillType.AmmoManiac); });
        transform.Find("X2Damage").GetComponent<Button>().onClick.AddListener(delegate { SetSkillEnable(SkillType.X2Damage); });
        transform.Find("X2Expirience").GetComponent<Button>().onClick.AddListener(delegate { SetSkillEnable(SkillType.X2Expirience); });
        transform.Find("MaxHealth").GetComponent<Button>().onClick.AddListener(delegate { SetSkillEnable(SkillType.MaxHealth); });
        transform.Find("Lucky").GetComponent<Button>().onClick.AddListener(delegate { SetSkillEnable(SkillType.Lucky); });
        transform.Find("ExplosionBullet").GetComponent<Button>().onClick.AddListener(delegate { SetSkillEnable(SkillType.ExplosionBullet); });
        transform.Find("PoisonBullet").GetComponent<Button>().onClick.AddListener(delegate { SetSkillEnable(SkillType.PoisonBullet); });
    }

    public void ChangeImageToLock() 
    {
        transform.Find("LongDistanceRunner").GetComponent<Button>().GetComponent<Image>().sprite = _lockImage.sprite;
        
    }
    private void OnSkillsMenuActive(GameStates state)
    {
        _skills.SetActive(state == GameStates.SkillSelection);       
    }
  
    public void SetSkillEnable(PlayerSkills.SkillType type) 
    {
        _playerSkills.SetActiveSkill(type);
        //GetComponent<Button>().enabled = false;

    }

    public void SetPlayerSkills(PlayerSkills skills)
    {
        _playerSkills = skills;        
    }
    
    private void OnDestroy()
    {
        GameManager.OnGameStatesChanged -= OnSkillsMenuActive;
    }
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
//_buttons = new List<SkillButton>();
//_buttons.Add(new SkillButton(transform.Find("LongDistanceRunner"), _playerSkills, SkillType.LongDistanceRunner));
//_buttons.Add(new SkillButton(transform.Find("FastShoot"), _playerSkills, SkillType.FastShoot));
//_buttons.Add(new SkillButton(transform.Find("FastLoader"), _playerSkills, SkillType.FastLoader));
//_buttons.Add(new SkillButton(transform.Find("AmmoManiac"), _playerSkills, SkillType.AmmoManiac));
//_buttons.Add(new SkillButton(transform.Find("X2Damage"), _playerSkills, SkillType.X2Damage));
//_buttons.Add(new SkillButton(transform.Find("X2Expirience"), _playerSkills, SkillType.X2Expirience));
//_buttons.Add(new SkillButton(transform.Find("MaxHealth"), _playerSkills, SkillType.MaxHealth));
//_buttons.Add(new SkillButton(transform.Find("Lucky"), _playerSkills, SkillType.Lucky));
//_buttons.Add(new SkillButton(transform.Find("ExplosionBullet"), _playerSkills, SkillType.ExplosionBullet));
//_buttons.Add(new SkillButton(transform.Find("PoisonBullet"), _playerSkills, SkillType.PoisonBullet));
//_playerSkills.OnSkillActivate += _playerSkills_OnSkillUnlocked;
