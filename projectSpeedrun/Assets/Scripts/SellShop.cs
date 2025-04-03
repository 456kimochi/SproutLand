using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class SellShop : MonoBehaviour
{
    public Inventory sellShopSlots;
    public List<GameObject> sellShopSlotsUI;
    public List<Collectable> seedType;

    [SerializeField] private Player player;

    public List<int> quantity;
    private int numberOfSellProducts = 0;
    void Start()
    {
        sellShopSlots = player.inventory;
    }

    public void ReloadSellShop()
    {
        for (int i = 0; i < player.inventory.slots.Count; i++)
        {
            if (player.inventory.slots[i].item.type == Type.RICEPLANT || player.inventory.slots[i].item.type == Type.GRAPEPLANT)
            {
                sellShopSlots.Add(player.inventory.slots[i].item, player.inventory.slots[i].count);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*numberOfSellProducts = 0;
        for (int i = 0; i < sellShopSlots.slots.Count; i++)
        {
            if (sellShopSlots.slots[i].item.type == Type.GRAPEPLANT || sellShopSlots.slots[i].item.type == Type.RICEPLANT)
            {
                numberOfSellProducts++;
            }
        }
        int j = 0;
        for (int i = 0; i < numberOfSellProducts; i++)
        {
            for (; j < sellShopSlots.slots.Count; j++)
            {
                if (sellShopSlots.slots[j].item.type == Type.GRAPEPLANT || sellShopSlots.slots[j].item.type == Type.RICEPLANT)
                {
                    sellShopSlotsUI[i].GetComponent<SellShopSlotUI>().SetItem(sellShopSlots.slots[j]);
                    i++;
                }
            }
        }*/

        for (int i = 0; i < sellShopSlotsUI.Count; i++)
        {
            for (int j = 0; j < sellShopSlots.slots.Count; j++)
            {
                if (sellShopSlotsUI[i].GetComponent<SellShopSlotUI>().type == sellShopSlots.slots[j].item.type)
                {
                    sellShopSlotsUI[i].GetComponent<SellShopSlotUI>().SetItem(sellShopSlots.slots[j]);
                }
            }
        }

        for (int i = 0; i < sellShopSlotsUI.Count; i++)
        {
            if (sellShopSlotsUI[i].GetComponent<SellShopSlotUI>().slotInSell.count <= 0)
            {
                Color color = sellShopSlotsUI[i].GetComponent<Image>().color;
                color.a = 0.5f;
                sellShopSlotsUI[i].GetComponent<Image>().color = color;
                sellShopSlotsUI[i].GetComponent<SellShopSlotUI>().icon.GetComponent<Image>().color = color;
            }
            else
            {
                Color color = Color.white;
                sellShopSlotsUI[i].GetComponent<Image>().color = color;
                sellShopSlotsUI[i].GetComponent<SellShopSlotUI>().icon.GetComponent<Image>().color = color;
            }
        }
    }

    public void SellPlant(Slot slotInSell)
    {
        if (slotInSell.count <= 1)
        {
            for (int i = 0; i < sellShopSlotsUI.Count; i++)
            {
                if (sellShopSlotsUI[i].GetComponent<SellShopSlotUI>().slotInSell == slotInSell)
                {
                    sellShopSlotsUI[i].GetComponent<SellShopSlotUI>().SetEmpty();
                }
            }          
        }
        player.GetComponent<Player>().money += slotInSell.price;
        sellShopSlots.Subtract(slotInSell);   
    }
}
