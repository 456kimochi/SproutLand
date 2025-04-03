using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Inventory
{
    public List<Slot> slots;
    public Inventory(int nums)
    {
        slots = new List<Slot>();
        for (int i = 0; i < nums; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }
    public void Add(Collectable add, int num)
    {
        foreach (Slot slot in slots)
        {
            if (slot.item.type == add.type)
            {
                slot.AddInSlot(add, num);
                return;
            }
        }
        foreach (Slot slot in slots)
        {
            if (slot.item.type == Type.NONE)
            {
                slot.AddInSlot(add, num);
                return;
            }
        }
    }
    
    public int Subtract(Slot subtractSlot)
    {
        if (subtractSlot.count <= 1)
        {
            slots.Remove(subtractSlot);
            Slot slot = new Slot();
            slots.Add(slot);
            return 0;
        }
        else
        {
            subtractSlot.count--;
            return subtractSlot.count;
        }
    } 
}
