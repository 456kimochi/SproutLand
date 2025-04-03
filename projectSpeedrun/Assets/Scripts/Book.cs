using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public Animator anim;
    private bool isOpen;
    public GameObject speech;
    public Animator speechAnim;
    [SerializeField] private GameObject player;

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
                speechAnim.SetTrigger("Open");
            }
            player.GetComponent<Player>().nearBook = true;
            
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
                speechAnim.SetTrigger("Close");
            }
            player.GetComponent<Player>().nearBook = false;
        }      
    }
}
