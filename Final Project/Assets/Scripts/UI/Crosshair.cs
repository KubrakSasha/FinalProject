using UnityEngine;

public class Crosshair : MonoBehaviour
{    
    void Start()
    {
        Cursor.visible = false;
    }
   
    void Update()
    {
        Vector2 crosshairPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = crosshairPosition;
    }
}
