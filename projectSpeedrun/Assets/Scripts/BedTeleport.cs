using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using DG.Tweening;

public class BedTeleport : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pannel;

    public Vector3 teleportPoint;

    public GameObject speech;
    public Animator speechAnim;

    private bool isPannelUIOpen = false;


    private void Start()
    {
        pannel.SetActive(true);
        pannel.GetComponent<RectTransform>().localScale = new Vector3(0f, 0f, 0f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            speechAnim.SetTrigger("Open");
            player.GetComponent<Player>().NearBed(this);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            speechAnim.SetTrigger("Close");
            player.GetComponent<Player>().nearBed = false;
        }
    }

    public void Teleport()
    {
        PannelToggle();
        Invoke("PannelToggle", 0.6f);
    }

    public void PannelToggle()
    {
        if (!isPannelUIOpen)
        {
            player.GetComponent<Player>().lockMove = true;
            pannel.transform.DOScale(1, 0.5f).OnComplete(() => 
            {
                isPannelUIOpen = true;
                player.GetComponent<Player>().lockMove = false;
                player.transform.position = teleportPoint;
            });
        }
        else
        {
            player.GetComponent<Player>().lockMove = true;
            pannel.transform.DOScale(0, 0.5f).OnComplete(() =>
            { 
                isPannelUIOpen = false;
                player.GetComponent<Player>().lockMove = false;
            });
        }
    }
}
