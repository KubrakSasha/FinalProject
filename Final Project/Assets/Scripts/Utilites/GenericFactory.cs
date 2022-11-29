using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] T prefab;
    [SerializeField] Vector2 spawnPoint;
    
   public T GetNewInstance(Vector2 spawnPoint)
    {
        return Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
}
