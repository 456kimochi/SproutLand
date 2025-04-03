using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public Animator animator;
    public GameObject visual;
    public float speed;
    private float preSpeed;
    public Vector3 direction = new Vector3(0, 0, 0);
    public Vector3 dir;
    
    private Vector3 hitDirection;
    private bool beingHit = false;

    public Inventory inventory;
    public int numSlots;

    [SerializeField] private GameObject GameController;

    public int money;
    public bool nearBook;
    public bool nearFences;
    public bool nearBed;
    private BedTeleport bedTeleport;
    public bool lockMove;

    public CurrentItem currentItem;
    public LayerMask plant;
    RaycastHit2D hit;

    void Awake()
    {
        inventory = new Inventory(numSlots);
        nearBook = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (lockMove)
        {
            return;
        }
        if (beingHit)
        {
            transform.position += hitDirection * 10 * Time.deltaTime;
            visual.GetComponent<SpriteRenderer>().color = Color.red;
            return;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            dir = new Vector3(horizontal, vertical, 0);
        }
        direction = new Vector3(horizontal, vertical, 0);
        transform.position += direction.normalized * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", 1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetFloat("X", -1);
            animator.SetFloat("Y", 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", -1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetFloat("X", 1);
            animator.SetFloat("Y", 0);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
    private void Update()
    {
        if (nearBook)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                GameController.GetComponent<GameController>().shop.SetActive(true);
            }
        }
        else
        {
            GameController.GetComponent<GameController>().shop.SetActive(false);
            GameController.GetComponent<GameController>().itemsShopSlot.SetActive(false);
            GameController.GetComponent<GameController>().seedsShopSlot.SetActive(false);
        }


        if (nearFences)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameController.GetComponent<GameController>().UseBridge();
            }
        }

        if (nearBed)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                bedTeleport.Teleport();
            }
        }


        hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, plant);
        if (hit.collider)
        {
            if (hit.collider.gameObject.GetComponent<PlantGrowing>().isGrowth)
            {
                if (Input.GetKeyDown (KeyCode.F))
                {
                    GameController.GetComponent<GameController>().Harvest(hit.collider.gameObject.GetComponent<PlantGrowing>().plantlandPosition);
                    inventory.Add(hit.collider.gameObject.GetComponent<PlantGrowing>().plantWhenGrowth, 1);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
    public void Plough()
    {
        animator.SetTrigger("plough");
    }

    public void UseItem(Slot slotInbag)
    {
        int numLeft = inventory.Subtract(slotInbag);
        if (numLeft <= 0)
        {
            currentItem.SetIcon(new Slot());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Animal"))
        {
            hitDirection = gameObject.transform.position - collision.gameObject.transform.position;
            beingHit = true;
            Invoke("ResetBeingHit", 0.3f);
        }
    }

    private void ResetBeingHit()
    {
        beingHit = false;
        visual.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void NearBed(BedTeleport bedTeleport)
    {
        nearBed = true;
        this.bedTeleport = bedTeleport;
    }
}
