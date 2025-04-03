using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Slot_UI : MonoBehaviour
{
    public GameObject icon;
    public GameObject count;
    public Type type;
    [SerializeField] private GameObject currentItem;
    [SerializeField] private GameObject player;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SetCurrentItem);
    }

    public void SetCurrentItem()
    {
        Inventory bag = player.GetComponent<Player>().inventory;

        for (int i = 0; i < bag.slots.Count; i++)
        {
            if (bag.slots[i].item.type == this.type)
            {
                currentItem.GetComponent<CurrentItem>().SetIcon(bag.slots[i]);
                return;
            }
        }
    }

    public void SetItem(Slot slot)
    {
        if (slot != null)
        {
            icon.GetComponent<Image>().sprite = slot.item.icon;
            count.GetComponent<TMP_Text>().text = slot.count.ToString();
            type = slot.item.type;
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
