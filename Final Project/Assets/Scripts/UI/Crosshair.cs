using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Text _text;
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
        //_text.transform.position = transform.position + _offset;

    }
    private void SetCurrentAmmoCountText() 
    {
        if (PlayerMain.Instance.ShootingHandler.CurrentAmmo == 0) 
        {
            _text.text = "Reloading";
        }
        _text.text = "." + PlayerMain.Instance.ShootingHandler.CurrentAmmo ;
    }
}
