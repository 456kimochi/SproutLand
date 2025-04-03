using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator anim;
    private bool isOpen;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isOpen)
            {
                anim.SetTrigger("Open");
                anim.SetBool("isOpen", true);
                isOpen = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isOpen)
            {
                anim.SetTrigger("Close");
                anim.SetBool("isOpen", false);
                isOpen = false;
            }
        }
    }
}
