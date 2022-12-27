using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockSkillSprite : MonoBehaviour
{   
        public void ChangeToLocked()
        {
        this.GetComponent<Image>().sprite = AssetManager.Instance.LockImage;
        }
  
    
}
