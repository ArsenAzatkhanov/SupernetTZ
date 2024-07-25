using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public float shotInterval;
    public float bulletSpeed;
    public int bulletDamage;
    public GameObject bulletObject;
}