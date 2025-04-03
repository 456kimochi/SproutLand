using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualAnimation : MonoBehaviour
{
    public GameObject player;
    private float preSpeed;
    public void StopCharacter()
    {
        preSpeed = player.GetComponent<Player>().speed;
        player.GetComponent<Player>().speed = 0;
    }
    public void ResumeCharacter()
    {
        player.GetComponent<Player>().speed = preSpeed;
    }
}
