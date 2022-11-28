using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoData", menuName = "ScriptableObjects/Ammo", order = 1)]
public class AmmoItem : ItemScriptable
{
    [Header("Ammo Values")]
    public int attackDamage;
    public int ammoCount;
    public Color Color;

    /*private void OnEnable()
    {
        ammoCount = Random.Range(1, 20);
    }*/

    public void SetUpAmmoCount()
    {
        ammoCount = Random.Range(1, 20);
    }
}
