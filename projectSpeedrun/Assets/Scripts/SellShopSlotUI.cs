using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SellShopSlotUI : MonoBehaviour
{
    public GameObject icon;
    public GameObject count;
    public Type type;
    public GameObject priceText;
    public Slot slotInSell = new Slot();
    private int numLeft = 0;
    [SerializeField] private GameObject sellShop;

    private void Start()
    {
        gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SellPlant);
    }

    public void SellPlant()
    {
        if (numLeft > 0)
        {
            sellShop.GetComponent<SellShop>().SellPlant(slotInSell);
        }  
    }
    public void SetItem(Slot slot)
    {
        if (slot != null)
        {
            icon.GetComponent<Image>().sprite = slot.item.icon;
            count.GetComponent<TMP_Text>().text = slot.count.ToString();
            priceText.GetComponent<TMP_Text>().text = slot.price.ToString();
            slotInSell = slot;
            numLeft = slot.count;
        }
    }
    public void SetEmpty()
    {
        icon.GetComponent<Image>().sprite = null;
        count.GetComponent<TMP_Text>().text = "0";
        slotInSell = new Slot();
        numLeft = 0;
    }



    private void Update()
    {
        if (count.GetComponent<TMP_Text>().text != "0")
        {
            count.SetActive(true);
        }
        else
        {
            count.SetActive(false);
        }

        if (icon.GetComponent<Image>().sprite != null)
        {
            icon.SetActive(true);
        }
        else
        {
            icon.SetActive(false);
        }
    }
}
