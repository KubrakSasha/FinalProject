using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    //public Vector3 _offset = new Vector3(0,3,0);
    void Start()
    {
        Cursor.visible = false;
        PlayerMain.Instance.ShootingHandler.OnAmmoChanged += ShootingHandler_OnAmmoChanged;
    }

    private void ShootingHandler_OnAmmoChanged()
    {
        SetCurrentAmmoCountText();
    }

    void Update()
    {
        Vector2 crosshairPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = crosshairPosition;
        Cursor.lockState = CursorLockMode.Confined;
        //_text.transform.position = transform.position + _offset;

    }
    private void SetCurrentAmmoCountText() 
    {
        if (PlayerMain.Instance.ShootingHandler.CurrentAmmo == 0)
        {
            _text.text = "Reloading...";
        }
        else
        _text.text = "Ammo " + (int)PlayerMain.Instance.ShootingHandler.CurrentAmmo + "." + (int)PlayerMain.Instance.ShootingHandler.Weapon.GetMaxAmmo();
        
    }
}
