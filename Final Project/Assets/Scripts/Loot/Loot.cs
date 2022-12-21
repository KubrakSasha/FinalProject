using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//[Serializable]
public class Loot : MonoBehaviour
{
    [SerializeField] protected GameObject _prefab;    
    [SerializeField] [Range (1, 100)]protected float _dropChance;

    public float DropChance => _dropChance * PlayerMain.Instance.Stats.DropChanceMultyply;
    public GameObject Prefab => _prefab;
}
