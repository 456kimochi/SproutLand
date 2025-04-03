using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot
{
    public int count;
    public int maxCapacity;
    public Collectable item;
    public int price;
    public Slot()
    {
        item = new Collectable();
        count = 0;
        maxCapacity = 99;
        price = 0;
    }
    public void AddInSlot(Collectable add, int number)
    {
        if (count < maxCapacity)
        {
            item.type = add.type;
            item.icon = add.icon;
            count += number;
            if (add.type == Type.NONE)
            {
                price = 0;
            }
            else if (add.type == Type.HOE)
            {
                price = 10;
            }
            else if (add.type == Type.BRIDGE)
            {
                price = 100;
            }
            else if (add.type == Type.GRAPE)
            {
                price = 12;
            }
            else if (add.type == Type.RICE)
            {
                price = 10;
            }
            else if (add.type == Type.RICEPLANT)
            {
                price = 15;
            }
            else if (add.type == Type.GRAPEPLANT)
            {
                price = 20;
            }
        }   
    }
}

