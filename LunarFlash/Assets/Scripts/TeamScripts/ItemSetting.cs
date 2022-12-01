using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSetting : MonoBehaviour
{

    [SerializeField] string itemInfo;
    // Start is called before the first frame update
    void Start()
    {
        SetUpItem();
    }
    void SetUpItem()
    {
        var index = Random.Range(0, 10);

        if(index == 1 || index == 2 )//|| index == 3)
        {
            itemInfo = "BasicAmmo";
        }
        else if (index == 0)
        {
            itemInfo = "UpgradeAmmo";
        }
        else
        {
            itemInfo = "HPpotion";// Resources.Load<ScriptableObject>("Scriptable/HPpotion");
        }
    }

    public string GetTheItemInfo()
    {
        return itemInfo;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
