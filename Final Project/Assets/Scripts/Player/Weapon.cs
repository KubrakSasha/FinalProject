using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponTypes { Pistol, Rifle, Shotgun }
    public WeaponTypes WeaponType;

    private float _timeBetweenShotsMultiply = 1.0f;
    private float _maxAmmoMultyply = 1.0f;
    private float _timeReloadMultiply = 1.0f;
    public List<WeaponTypes> WeaponTypess;



    public void SetTimeBetweenShootCoefficient(float coef) 
    {
        _timeBetweenShotsMultiply = coef;
    }
    public void SetMaxAmmoCoefficient(float coef)
    {
        _maxAmmoMultyply = coef;
    }
    public void SetTimeReloadCoefficient(float coef)
    {
        _timeReloadMultiply = coef;
    }


    public float GetTimeBetweenShoot()
    {
        switch (WeaponType)
        {
            case WeaponTypes.Pistol:
                return 0.3f * _timeBetweenShotsMultiply;
            case WeaponTypes.Rifle:
                return 0.1f *_timeBetweenShotsMultiply;
            case WeaponTypes.Shotgun:
                return 0.5f * _timeBetweenShotsMultiply;
            default:
                return 0;//
        }
    }
    public float GetMaxAmmo()
    {
        switch (WeaponType)
        {
            case WeaponTypes.Pistol:
                return 12f * _maxAmmoMultyply;
            case WeaponTypes.Rifle:
                return 30f * _maxAmmoMultyply;
            case WeaponTypes.Shotgun:
                return 6f * _maxAmmoMultyply;
            default:
                return 0;//
        }
    }
    public float GetTimeReloadTime()
    {
        switch (WeaponType)
        {
            case WeaponTypes.Pistol:
                return 2.0f * _timeReloadMultiply;
            case WeaponTypes.Rifle:
                return 3.0f * _timeReloadMultiply;
            case WeaponTypes.Shotgun:
                return 1.0f * _timeReloadMultiply;
            default:
                return 0;//
        }
    }
    public float GetShootForce()
    {
        switch (WeaponType)
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
    public List<WeaponTypes> GetAllWeaponTypes()
    {
        WeaponTypess.Add(WeaponTypes.Pistol);
        WeaponTypess.Add(WeaponTypes.Rifle);
        WeaponTypess.Add(WeaponTypes.Shotgun);
        return WeaponTypess;
    }
}
