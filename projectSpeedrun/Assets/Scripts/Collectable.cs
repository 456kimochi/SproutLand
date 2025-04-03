using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Sprite icon;
    public Type type;
    //If the player collides with a collectable thing, it will be destroyed and add into the inventory
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            //Add the collectable into the inventory in particular
            other.gameObject.GetComponent<Player>().inventory.Add(this, 1);
        }
    }

    public Collectable()
    {
        icon = null;
        type = Type.NONE;
    }
}
//Types of collectable. If you want to add more collectable things, add here
public enum Type
{
    NONE, HOE, RICE, GRAPE, BRIDGE, RICEPLANT, GRAPEPLANT
}