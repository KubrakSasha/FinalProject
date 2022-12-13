using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponTypes { Pistol, Rifle, Shotgun }
    public WeaponTypes WeaponType;
    


    public float GetTimeBetweenShoot(WeaponTypes types) 
    {
        switch (types)
        {
            case WeaponTypes.Pistol:
                return 0.3f;                
            case WeaponTypes.Rifle:
                return 0.1f;                
            case WeaponTypes.Shotgun:
                return 0.5f;                                
            default:
                return 0;//
        }
    }
    public float GetMaxAmmo(WeaponTypes types)
    {
        switch (types)
        {
            case WeaponTypes.Pistol:
                return 12f;
            case WeaponTypes.Rifle:
                return 30f;
            case WeaponTypes.Shotgun:
                return 6f;
            default:
                return 0;//
        }
    }
    public float GetTimeReloadTime(WeaponTypes types)
    {
        switch (types)
        {
            case WeaponTypes.Pistol:
                return 2.0f;
            case WeaponTypes.Rifle:
                return 3.0f;
            case WeaponTypes.Shotgun:
                return 1.0f;
            default:
                return 0;//
        }
    }
    public float GetShootForce(WeaponTypes types)
    {
        switch (types)
        {
            case WeaponTypes.Pistol:
                return 50f;

            case WeaponTypes.Rifle:
                return 100f;

            case WeaponTypes.Shotgun:
                return 70f;

            default:
                return 0;//
        }
    }
}
