using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Item/Weapon")]
public class Weapon : Item
{
    public GameObject prefab;
    public int magazineSize, curMagazine ,bulletperTap;
    public float range;
    public WeaponType wpType;
    public WeaponIndex wpIndex;
}

public enum WeaponType { Melee, Pistol, Rifle, Shotgun, Rocket, Sniper }
public enum WeaponIndex { Primary, Secondary }
