using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [SerializeField] private List <Loot> _loots = new List<Loot>();

    Loot GetDroppedItem() 
    {
        int randomNumber = Random.Range(1, 101);
        List<Loot> possibleItems = new List<Loot>();
        foreach (Loot item in _loots) 
        {
            if (randomNumber <= item.DropChance)
            {
                possibleItems.Add(item);
            }
        }
        if (possibleItems.Count > 0)
        {
            Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        return null;
    }
    public void InstantiateLoot(Vector2 spawnPosition) 
    {
        Loot item = GetDroppedItem();
        if (item != null)
        {
            Instantiate(item.Prefab, spawnPosition, Quaternion.identity);
        }
    }



}
