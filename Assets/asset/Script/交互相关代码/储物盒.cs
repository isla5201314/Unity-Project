using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 储物盒 : MonoBehaviour
{
    public Items item;
    public Inventory PlayerInventory;
    public GameObject internalUI;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        internalUI.SetActive(true);

        if (other.CompareTag("Player") && Input.GetKey(KeyCode.F))
        {

            AddNewItem();
            internalUI.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        internalUI.SetActive(false);
    }

    public void AddNewItem()
    {
        //if (!PlayerInventory.itemsList.Contains(item))
        //{
        //    PlayerInventory.itemsList.Add(item);
        //    item.Num += 1;
        //    //InventoryManager.WaitToLoad(item);
        //}
        //else
        //{
        //    item.Num += 1;
        //}

        item.Num += 1;
    }

}
