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
    [SerializeField] Gun gunScript;
  
    public static InventoryManager IMInstance { get; private set; }
    public static string collectedItemName;
    public static bool isInventoryLocked = false;  // this is for deciding whether a player can collect a new item or not
    [Header("ItemInfoList")]
    public  Potion potionItemInfo;
    public AmmoItem basicAmmo;
    public AmmoItem upgradeAmmo;

    GameObject playerInstance;

    //InventoryInfoUpdate
    ScriptableObject collectedItemInfo;
    Texture itemImage;
    int itemCount;
    Color itemColor;
    //shortCutInventory
    bool isShortCutFull = false;
    bool isWholeFull = false;
    bool isInventoryOpen = false;
    GameObject firstSlot;
    GameObject secondSlot;
    GameObject thirdSlot;
    int firstItemcount =-1;
    int secondItemcount = -1;
    int thirdItemcount = -1;


    public void InventoryOpen()
    {
        isInventoryOpen = true;
        //Debug.Log("Inventory Open");
    }

    public void InventoryClose()
    {
        isInventoryOpen = false;
        //Debug.Log("Inventory Close");
    }
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
       /* else
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

        }*/
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
            //basicAmmo.SetUpAmmoCount(); - upgrade item = only 1 
            itemCount = basicAmmo.ammoCount;
            itemColor = basicAmmo.Color;
        }
        else if (collectedItemName == "UpgradeAmmo")
        {
            collectedItemInfo = upgradeAmmo;
            itemImage = upgradeAmmo.itemImage;
            // upgradeAmmo.SetUpAmmoCount(); - upgrade item = only 1 
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

                if (i == 0)
                {
                    firstItemcount = itemCount;
                    Debug.Log("firstItemcount is " + firstItemcount);
                }
                else if (i == 1)
                {
                    secondItemcount = itemCount;
                    Debug.Log("secondItemcount is " + secondItemcount);
                }
                else if (i == 2)
                {
                    thirdItemcount = itemCount;
                    Debug.Log("thridItemcount is " + thirdItemcount);
                }
                // 2) Update the item count number
                break;
            }
          /*  else
            {
               if(shortCutItem[i].transform.GetChild(0).GetComponent<RawImage>().texture == itemImage)
                {
                    //itemCount Update

                    int currentItemCount = Convert.ToInt32(shortCutItem[i].transform.GetChild(1).GetComponent<TMP_Text>().text);
                    currentItemCount += itemCount;
                    shortCutItem[i].transform.GetChild(1).GetComponent<TMP_Text>().text = currentItemCount.ToString();
                }
               
            }*/
        }
    }
 
    void UpdateFullInventory() // not going to use this 
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

    bool CheckWholeIsFull() // not going to use it
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

        if (playerInstance == null)
        {
            playerInstance = GameObject.FindGameObjectWithTag("Player");
        }

        // shortCutItem = new GameObject[3];
        // wholeItem = new GameObject[15];

        if (CheckShortCutIsFull() == true)
        {
            isInventoryLocked = true;
            UIManager.inventoryFullUION = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInventoryOpen)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //shortCutItem[0]
                // get what the item is ??? how can I do this????????????????????
                // decrease the count of that item--;
                // apply the effect of the item to the game...
                Debug.Log("1 is pressed");
                UseItem(shortCutItem[0], 0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UseItem(shortCutItem[1], 1);
                Debug.Log("2 is pressed");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UseItem(shortCutItem[2], 2);
                Debug.Log("3 is pressed");
            }
        }


        if (CheckShortCutIsFull() == true)
        {
            isInventoryLocked = true;
            UIManager.inventoryFullUION = true;
        }
    }


   

    public void UseItem(GameObject itemInSlot, int i)
    {
        if(itemInSlot.transform.GetChild(0).GetComponent<RawImage>().texture.name.Contains("HP"))
        {
            Debug.Log("HP ITEM");
            if (i == 0 && firstItemcount >0)
            {
                firstItemcount--;
                UpdateItemCount(itemInSlot, firstItemcount);
                
                Debug.Log("first item count it " + firstItemcount);
                /* if (firstItemcount > 1)
                 {
                     firstItemcount--;
                     itemInSlot.transform.GetChild(1).GetComponent<TMP_Text>().text = firstItemcount.ToString();
                 }
                 else if(firstItemcount == 1)
                 {
                     firstItemcount = 0;
                     itemInSlot.transform.GetChild(0).GetComponent<RawImage>().texture = null;
                     itemInSlot.transform.GetChild(0).GetComponent<RawImage>().color = new Color(255,255,255,45);
                     itemInSlot.transform.GetChild(1).GetComponent<TMP_Text>().text = firstItemcount.ToString();
                 }*/

                playerInstance.GetComponent<Player>().IncreasePlayerHP(20); //HPpotion will increase player's HP by 20
            }
           else if (i == 1 && secondItemcount > 0)
            {
                secondItemcount--;
                UpdateItemCount(itemInSlot, secondItemcount);
               
                Debug.Log("2nd item count it " + secondItemcount);
                // secondItemcount--;
                //itemInSlot.transform.GetChild(1).GetComponent<TMP_Text>().text = secondItemcount.ToString();
                playerInstance.GetComponent<Player>().IncreasePlayerHP(20);
            }
            else if (i == 2 && thirdItemcount > 0)
            {
                thirdItemcount--;
                UpdateItemCount(itemInSlot, thirdItemcount);
                
                Debug.Log("third item count it " + thirdItemcount);
                //thirdItemcount--;
                //itemInSlot.transform.GetChild(1).GetComponent<TMP_Text>().text = thirdItemcount.ToString();
                playerInstance.GetComponent<Player>().IncreasePlayerHP(20);
            }
        }
        else if (itemInSlot.transform.GetChild(0).GetComponent<RawImage>().texture.name.Contains("Basic"))
        {
            Debug.Log("BASIC ITEM");
            if (i == 0 && firstItemcount > 0)
            {
                firstItemcount--;
                UpdateItemCount(itemInSlot, firstItemcount);
                
                gunScript.GunDamageUpgradeExecution(0.3f, 3);
               
            }
            else if (i == 1 && secondItemcount > 0)
            {
                secondItemcount--;
                UpdateItemCount(itemInSlot, secondItemcount);
                
                gunScript.GunDamageUpgradeExecution(0.3f, 3);

            }
            else if (i == 2 && thirdItemcount > 0)
            {
                thirdItemcount--;
                UpdateItemCount(itemInSlot, thirdItemcount);
                
                gunScript.GunDamageUpgradeExecution(0.3f, 3);

            }
        }
        else if (itemInSlot.transform.GetChild(0).GetComponent<RawImage>().texture.name.Contains("Upgrade"))
        {

            Debug.Log("UPGRADE ITEM");
            if (i == 0 && firstItemcount > 0)
            {
                firstItemcount--;
                UpdateItemCount(itemInSlot, firstItemcount);
                
                gunScript.GunDamageUpgradeExecution(0.3f, 3);

            }
            else if (i == 1 && secondItemcount > 0)
            {
               secondItemcount--;
                UpdateItemCount(itemInSlot, secondItemcount);
                
                gunScript.GunDamageUpgradeExecution(0.3f, 3);

            }
            else if (i == 2 && thirdItemcount > 0)
            {
                thirdItemcount--;
                UpdateItemCount(itemInSlot, thirdItemcount);
                
                gunScript.GunDamageUpgradeExecution(0.3f, 3);

            }
        }
    }

    void UpdateItemCount(GameObject item, int count)
    {
        if (count >= 1)
        {
           // count--;
            item.transform.GetChild(1).GetComponent<TMP_Text>().text = count.ToString();
        }
        else if (count == 0)
        {
            count = -1;
            item.transform.GetChild(0).GetComponent<RawImage>().color = new Color(1.000f, 1.000f, 1.000f, 0.176f); ;
            item.transform.GetChild(0).GetComponent<RawImage>().texture = null;
            item.transform.GetChild(1).GetComponent<TMP_Text>().text = "0";// count.ToString();
            item.GetComponent<InventorySlot>().GetsEmptied();
            isInventoryLocked = false;
            UIManager.inventoryFullUIOFF = true;
            UIManager.inventoryFullUION = false;
        }
    }
}
