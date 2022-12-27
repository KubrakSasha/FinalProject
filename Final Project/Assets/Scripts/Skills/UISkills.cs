using UnityEngine;
using UnityEngine.UI;
using static PlayerSkills;

public class UISkills : MonoBehaviour
{
    private PlayerSkills _playerSkills;
    [SerializeField] private GameObject _skills;
    [SerializeField] private Image _lockImage;

    private void Awake()//Почему то нужно что бы на старте панель Skills была активна
    {
        GameManager.OnGameStatesChanged += OnSkillsMenuActive;
        transform.Find("LongDistanceRunner").GetComponent<Button>().onClick.AddListener(delegate { SetSkillEnable(SkillType.LongDistanceRunner); });
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
    private void OnSkillsMenuActive(GameStates state)
    {
        _skills.SetActive(state == GameStates.SkillSelection);
    }
    public void SetSkillEnable(PlayerSkills.SkillType type)
    {
        _playerSkills.SetActiveSkill(type);
    }
    public void SetPlayerSkills(PlayerSkills skills)
    {
        _playerSkills = skills;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStatesChanged -= OnSkillsMenuActive;
    }
}