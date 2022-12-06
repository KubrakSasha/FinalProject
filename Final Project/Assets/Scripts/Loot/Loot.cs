using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//[Serializable]
public class Loot : MonoBehaviour
{
    [SerializeField] protected GameObject _prefab;    
    [SerializeField] protected int _dropChance;

    public int DropChance => _dropChance;
    public GameObject Prefab => _prefab;
}
