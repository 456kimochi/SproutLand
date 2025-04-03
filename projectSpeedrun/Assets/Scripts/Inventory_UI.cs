using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.TextCore.Text;
public class Inventory_UI : MonoBehaviour
{
    
    public GameObject inventoryGameObject;
    public GameObject player;
    public List<Slot_UI> slotUI = new List<Slot_UI>();
    public bool isUIOpen;

    void Start()
    {
        inventoryGameObject.GetComponent<RectTransform>().localScale = new Vector3(0f, 0f, 0f);
        isUIOpen = false;
    }


    void Update()
    {
        Setup();
        if (Input.GetKeyDown(KeyCode.Tab) && !isUIOpen)
        {
            UIToggle();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isUIOpen)
        {
            UIToggle();
        }
    }
    private void UIToggle()
    {
        if (!isUIOpen)
        {
            //inventoryGameObject.SetActive(true);
            inventoryGameObject.transform.DOScale(1, 0.5f).OnComplete(() => isUIOpen = true);
        }
        else
        {
            inventoryGameObject.transform.DOScale(0, 0.5f).OnComplete(() => 
            { //inventoryGameObject.SetActive(false); 
                isUIOpen = false; });
        }
    }
    void Setup()
    {
        if (slotUI.Count == player.GetComponent<Player>().inventory.slots.Count)
        {
            for (int i = 0; i < slotUI.Count; i++)
            {
                if (player.GetComponent<Player>().inventory.slots[i].item.type != Type.NONE)
                {
                    slotUI[i].SetItem(player.GetComponent<Player>().inventory.slots[i]);
                }
                else
                {
                    slotUI[i].SetEmpty();
                }
            }
        }
    }


}
