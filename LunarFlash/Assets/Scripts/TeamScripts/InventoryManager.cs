using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject[] shortCutItem;
    [SerializeField] GameObject[] wholeItem;
    public static InventoryManager IMInstance { get; private set; }
    public static string collectedItemName;
    [Header("ItemInfoList")]
    public  Potion potionItemInfo;
    public AmmoItem basicAmmo;
    public AmmoItem upgradeAmmo;

    //InventoryInfoUpdate
    ScriptableObject collectedItemInfo;
    Texture itemImage;
    int itemCount;
    Color itemColor;
    //shortCutInventory
    bool isShortCutFull = false;
    bool isWholeFull = false;

    public static void GetItemInfo(GameObject item)
    {
        collectedItemName = item.GetComponent<ItemSetting>().GetTheItemInfo();
        Debug.Log("the info of collected item is " + collectedItemName);
        IMInstance.UpdateItemInfo();
       // return collectedItem;
    }
    public static void UpdateInventory()//ScriptableObject itemInfo)
    {
        //when an item object gets destroyed, get the item name and execute this function - so static
       // collectedItem = item;
        if (IMInstance.CheckShortCutIsFull()== false)
        {
            IMInstance.UpdateShortCutInventory();
        }
        else
        {
            Debug.Log("ShortCut Inventory is FULL");
            if (IMInstance.CheckWholeIsFull() == false)
            { 
                IMInstance.UpdateFullInventory(); 
            }
            else
            {
                Debug.Log("Full Inventory is FULL");
            }

        }
        //if shortcut is NOT full
        // && if this is a new item, 
        // 1) Add an item image to the raw image
        // 2) Update the item count number
        //
        //else if this item is already in the shorcut
        //  1) update the item count number only
        //
        //Else (if shortcut IS full)
        // && if this is a new item
        //  Add an item image to the raw image in the FULL canvas
        // update the item count number in the FULL canvas

    }

    void UpdateItemInfo()
    {
        if(collectedItemName == "BasicAmmo")
        {
            collectedItemInfo = basicAmmo;
            itemImage = basicAmmo.itemImage;
            basicAmmo.SetUpAmmoCount();
            itemCount = basicAmmo.ammoCount;
            itemColor = basicAmmo.Color;
        }
        else if (collectedItemName == "UpgradeAmmo")
        {
            collectedItemInfo = upgradeAmmo;
            itemImage = upgradeAmmo.itemImage;
            upgradeAmmo.SetUpAmmoCount();
            itemCount = upgradeAmmo.ammoCount;
            itemColor = upgradeAmmo.Color;
        }
        else if (collectedItemName == "HPpotion")
        {
            collectedItemInfo = potionItemInfo;
            itemImage = potionItemInfo.itemImage;
            potionItemInfo.SetUpPotionCount();
            itemCount = potionItemInfo.potionCount;
            itemColor = new Color(255, 255, 255, 255);
        }
    }


    void UpdateShortCutInventory()//ScriptableObject item)
    {
        for (int i = 0; i < shortCutItem.Length; i++)
        {
            if (shortCutItem[i].GetComponent<InventorySlot>().isFullStatus() == false)
            {
                // shortCutItem[i] = item;
                // 1) Add an item image to the raw image
                shortCutItem[i].transform.GetChild(0).GetComponent<RawImage>().texture = itemImage;
                shortCutItem[i].transform.GetChild(0).GetComponent<RawImage>().color = itemColor;
                shortCutItem[i].transform.GetChild(1).GetComponent<TMP_Text>().text = itemCount.ToString();
                shortCutItem[i].GetComponent<InventorySlot>().GetsFilled();
                // 2) Update the item count number
                break;
            }
            else
            {
               if(shortCutItem[i].transform.GetChild(0).GetComponent<RawImage>().texture == itemImage)
                {
                    //itemCount Update

                    int currentItemCount = Convert.ToInt32(shortCutItem[i].transform.GetChild(1).GetComponent<TMP_Text>().text);
                    currentItemCount += itemCount;
                    shortCutItem[i].transform.GetChild(1).GetComponent<TMP_Text>().text = currentItemCount.ToString();
                }
               
            }
        }
    }
 
    void UpdateFullInventory()
    {
        for (int i = 0; i < wholeItem.Length; i++)
        {
            if (wholeItem[i] == null)
            {
                //  wholeItem[i] = item;
                // 1) Add an item image to the raw image
                // 2) Update the item count number

                wholeItem[i].transform.GetChild(0).GetComponent<RawImage>().texture = itemImage;
                wholeItem[i].transform.GetChild(0).GetComponent<RawImage>().color = itemColor;
                wholeItem[i].transform.GetChild(1).GetComponent<TMP_Text>().text = itemCount.ToString();
                break;
            }
            else
            {
                if (wholeItem[i].transform.GetChild(0).GetComponent<RawImage>().texture == itemImage)
                {
                    //itemCount Update

                    int currentItemCount = Convert.ToInt32(wholeItem[i].transform.GetChild(1).GetComponent<TMP_Text>().text);
                    currentItemCount += itemCount;
                    wholeItem[i].transform.GetChild(1).GetComponent<TMP_Text>().text = currentItemCount.ToString();
                }

            }
        }
    }
    bool CheckShortCutIsFull()
    {
        for(int i = 0; i < shortCutItem.Length; i++)
        {
            if (shortCutItem[i].transform.GetChild(0).GetComponent<RawImage>().texture == null)
            {
                isShortCutFull = false;
                //return isShortCutFull;
                //break;
            }
            else
            {
                isShortCutFull = true;
                Debug.Log(shortCutItem[i] + " is : " + isShortCutFull);
            }
        }

        //isShortCutFull = true;
        Debug.Log("isShortCutFull is : " + isShortCutFull);
        return isShortCutFull;
    }

    bool CheckWholeIsFull()
    {
        for (int i = 0; i < wholeItem.Length; i++)
        {
            if (wholeItem[i].transform.GetChild(0).GetComponent<RawImage>().texture == null)
            {
                isWholeFull = false;
                //return isShortCutFull;
                //break;
            }
            else
            {
                isWholeFull = true;
                Debug.Log(wholeItem[i] + " is : " + isWholeFull);
            }
        }

        //isShortCutFull = true;
        Debug.Log("Is The full inventory FULL?? => " + isWholeFull);
        return isWholeFull;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (IMInstance == null)
        { IMInstance = this; }

       // shortCutItem = new GameObject[3];
       // wholeItem = new GameObject[15];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad1))
        {
            // get what the item is ??? how can I do this????????????????????
           // decrease the count of that item--;
           // apply the effect of the item to the game...
        }
    }
}
