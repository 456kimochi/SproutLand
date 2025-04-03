using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FenceToNextIsland : MonoBehaviour
{
    public Vector3Int fence1;
    public Vector3Int fence2;

    public GameObject speech;
    public Animator speechAnim;
    [SerializeField] private GameObject player;

    public GameObject brigde;
    [SerializeField] private Tilemap fenceland;
    [SerializeField] private Tile grassNoneSprite;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            speechAnim.SetTrigger("Open");
            player.GetComponent<Player>().nearFences = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            speechAnim.SetTrigger("Close");
            player.GetComponent<Player>().nearFences = false;
        }     
    }

    public void BuildBridge()
    {
        fenceland.SetTile(new Vector3Int(fence1.x-1, fence1.y-1,0), grassNoneSprite);
        fenceland.SetTile(new Vector3Int(fence2.x - 1, fence2.y - 1, 0), grassNoneSprite);
        Vector3 pos = new Vector3((fence1.x + fence2.x) / 2f, fence1.y - 1.5f, 0);
        GameObject bridge = Instantiate(brigde, pos, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
