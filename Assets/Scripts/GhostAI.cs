using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    public bool canWalk;
    [SerializeField] int ghostDirection; //0-left;1-up;2-right;3-down
    [SerializeField] float walkOffset;
    [SerializeField] bool canRotate;
    [SerializeField] int randomizedDirection;
    float rotateValue;
    PlayerRotator PR_playerRotator;
    public GameObject ghostDamageObject;
    public float waitTimer;
    public Player p_player;
    private void Start()
    {
        canWalk = false;
        canRotate = false;
        waitTimer = 3.0f;
    }

    private void Update()
    {
        if(canWalk == true && p_player.isEnd == false)
        {
            this.transform.Translate(Vector3.left / walkOffset);
        }
        this.transform.rotation = Quaternion.Euler(0, 0, rotateValue);
        ghostDamageObject.transform.position = this.transform.position;
        if(waitTimer > 0 && canWalk == false)
        {
            waitTimer -= Time.deltaTime;
            if(waitTimer <= 0)
            {
                canWalk = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Rotator")
        {
            PR_playerRotator = collision.GetComponent<PlayerRotator>();
            RandomizeDirection();
        }
    }
    public void RandomizeDirection()
    {
        randomizedDirection = Random.Range(0, 4);
        CheckRotation(randomizedDirection);
        if(canRotate == true)
        {
            Rotate();
        }
    }
    void CheckRotation(int randomizedValue)
    {
        if (PR_playerRotator.wichSideRotate[randomizedValue] == true)
        {
            canRotate = true;
        }else
        {
            RandomizeDirection();
        }
    }
    void Rotate()
    {
        ghostDirection = randomizedDirection;
        if (ghostDirection == 0)
        {
            rotateValue = 0;
        }
        if (ghostDirection == 1)
        {
            rotateValue = -90;
        }
        if (ghostDirection == 2)
        {
            rotateValue = -180;
        }
        if (ghostDirection == 3)
        {
            rotateValue = 90;
        }
        PR_playerRotator = null;
        canRotate = false;
    }
}

