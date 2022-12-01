using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] bool isFull;
    // Start is called before the first frame update
    void Start()
    {
        isFull = false;
        //this.gameObject.transform.GetChild(0).GetComponent<RawImage>().color = new Color(255, 255, 255, 45);
       // Debug.Log("itemslot color is + " + this.gameObject.transform.GetChild(0).GetComponent<RawImage>().color);
    }

    public void GetsFilled()
    {
        isFull = true;
    }

    public void GetsEmptied()
    {
        isFull = false;
        this.gameObject.transform.GetChild(0).GetComponent<RawImage>().color = new Color(1.000f, 1.000f, 1.000f, 0.176f);
        //Debug.Log("itemslot color is + " +this.gameObject.transform.GetChild(0).GetComponent<RawImage>().color);
    }

    public bool isFullStatus()
    {
        return isFull;
    }
}
