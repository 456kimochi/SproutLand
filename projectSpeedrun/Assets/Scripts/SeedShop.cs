using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class SeedShop : MonoBehaviour
{
    public Inventory seedShopSlots;
    public List<GameObject> seedShopSlotsUI;
    public List<Collectable> seedType;

    [SerializeField] private GameObject player;

    public List<int> quantity;
    void Start()
    {
        seedShopSlots = new Inventory(seedShopSlotsUI.Count);
        for (int i =0; i < seedShopSlotsUI.Count; i++)
        {
            seedShopSlots.Add(seedType[i], quantity[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < seedShopSlotsUI.Count; i++)
        {
            seedShopSlotsUI[i].GetComponent<SeedShopSlotUI>().SetItem(seedShopSlots.slots[i]);
            if (seedShopSlots.slots[i].count <= 0)
            {
                Color color = seedShopSlotsUI[i].GetComponent<Image>().color;
                color.a = 0.5f;
                seedShopSlotsUI[i].GetComponent<Image>().color = color;
                seedShopSlotsUI[i].GetComponent<SeedShopSlotUI>().icon.GetComponent<Image>().color = color;
            }
        }
    }

    public void BuySeeds(Type type)
    {   
        for (int i = 0; i < seedShopSlotsUI.Count; i++)
        {
            if (seedShopSlots.slots[i].item.type == type)
            {  
                if (seedShopSlots.slots[i].count >= 1)
                {
                    if (player.GetComponent<Player>().money >= seedShopSlots.slots[i].price)
                    {
                        seedShopSlots.slots[i].count--;
                        player.GetComponent<Player>().inventory.Add(seedShopSlots.slots[i].item, 1);
                        player.GetComponent<Player>().money -= seedShopSlots.slots[i].price;
                    }         
                }
            }
        }
    }
}
