using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject weaponPref;
    public GameObject weaponHolder;

    private void Start()
    {
        GameObject currentWeapon = Instantiate(weaponPref, weaponHolder.transform.position, Quaternion.identity) as GameObject;
        currentWeapon.transform.parent = weaponHolder.transform;
    }
}