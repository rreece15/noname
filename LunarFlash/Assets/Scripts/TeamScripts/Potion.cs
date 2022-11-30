using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PotionData", menuName = "ScriptableObjects/Potion", order = 0)]
public class Potion : ItemScriptable
{
    [Header("Potion Values")]
    public int hpIncrease;
    public int potionCount;// { get; private set; } = Random.Range(1, 10);

    /*private void OnEnable()
    {
        potionCount = Random.Range(1, 10);
    }*/

    public void SetUpPotionCount()
    {
        potionCount = Random.Range(1, 6);
    }
}
