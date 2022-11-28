using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] bool isFull;
    // Start is called before the first frame update
    void Start()
    {
        isFull = false;
    }

    public void GetsFilled()
    {
        isFull = true;
    }

    public void GetsEmptied()
    {
        isFull = false;
    }

    public bool isFullStatus()
    {
        return isFull;
    }
}
