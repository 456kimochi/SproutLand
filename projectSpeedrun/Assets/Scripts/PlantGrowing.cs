using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowing : MonoBehaviour
{
    public Animator animator;
    public bool isGrowth = false;
    public Vector3Int plantlandPosition = new Vector3Int(0,0,0);
    public Collectable plantWhenGrowth;
    public void Growth()
    {
        animator.SetBool("isGrowth", true);
        isGrowth = true;
    }

}
