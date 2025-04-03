using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SeedShopSlotUI : MonoBehaviour
{
    public GameObject icon;
    public GameObject count;
    public Type type;
    public GameObject priceImage;
    public GameObject priceText;
    private int price;
    [SerializeField] private GameObject seedShop;

    private void Start()
    {
        gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(BuySeed);
    }

    public void BuySeed()
    {
        seedShop.GetComponent<SeedShop>().BuySeeds(type);
    }
    public void SetItem(Slot slot)
    {
        if (slot != null)
        {
            icon.GetComponent<Image>().sprite = slot.item.icon;
            count.GetComponent<TMP_Text>().text = slot.count.ToString();
            type = slot.item.type;
            priceText.GetComponent<TMP_Text>().text = slot.price.ToString();
            price = slot.price;
        }
    }
    public void SetEmpty()
    {
        icon.GetComponent<Image>().sprite = null;
        count.GetComponent<TMP_Text>().text = "0";
        type = Type.NONE;
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
