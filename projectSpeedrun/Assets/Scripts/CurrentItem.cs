using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class CurrentItem : MonoBehaviour
{
    public GameObject Icon;
    public GameObject count;
    public Slot slotInBag = new Slot();
    public List<GameObject> plants;
    public GameObject currentPlant;
    private void Update()
    {
        if (slotInBag != null)
        {
            count.GetComponent<TMP_Text>().text = slotInBag.count.ToString();
            Icon.GetComponent<UnityEngine.UI.Image>().sprite = slotInBag.item.icon;
            if (slotInBag.count == 0)
            {
                Color color = Color.white;
                color.a = 0;
                Icon.GetComponent<UnityEngine.UI.Image>().color = color;
                count.SetActive(false);
            }
            else
            {
                Color color = Color.white;
                color.a = 1;
                Icon.GetComponent<UnityEngine.UI.Image>().color = color;
                count.SetActive(true);
            }
            if (slotInBag.item.type == Type.GRAPE)
            {
                currentPlant = plants[0];
            }
            else if (slotInBag.item.type == Type.RICE)
            {
                currentPlant = plants[1];
            }
        }
    }

    public void SetIcon(Slot slotInBag)
    {
        this.slotInBag = slotInBag;
    }
}
