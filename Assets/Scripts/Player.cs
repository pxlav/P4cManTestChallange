using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField] float walkOffset;
    [SerializeField] float rotateValue;
    public int wichSideRotate; //0-left;1-up;2-right;3-down
    public bool canWalk;
    public bool canRotate;
    public bool hitedRotator;
    PlayerRotator rotatorHit;
    public int pointsValue;
    public int collectedPoints;
    public float waitTimer;
    public bool isEnd;
    [SerializeField] float endTimer;
    public UIController ui_Controller;
    public int scores;
    private void Start()
    {
        scores = 0;
        canWalk = false;
        isEnd = false;
        waitTimer = 3.0f;
        endTimer = 3.0f;
        ui_Controller.wichTextIsOn = 0;
    }
    private void Update()
    {
        if(waitTimer > 0 && canWalk == false)
        {
            waitTimer -= Time.deltaTime;
            if(waitTimer <= 0)
            {
                canWalk = true;
                ui_Controller.wichTextIsOn = 3;
            }
        }
        if (canWalk == true)
        {
            this.transform.Translate(Vector3.left / walkOffset);
        }
        if(canRotate == true)
        {
            Controls();
        }
        
        if (wichSideRotate > 3)
        {
            wichSideRotate = 3;
        }
        if (wichSideRotate < 0)
        {
            wichSideRotate = 0;
        }
        if(rotateValue == 0 && Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeDirection(2);
        }
        if (rotateValue == -90 && Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeDirection(3);
        }
        if (rotateValue == -180 && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeDirection(0);
        }
        if (rotateValue == 90 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeDirection(1);
        }
        while (hitedRotator == true)
        {
            canWalk = false;
            if (rotatorHit.wichSideRotate[0] == true)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    ChangeDirection(0);
                    hitedRotator = false;
                    canWalk = true;
                }
            }
            if (rotatorHit.wichSideRotate[1] == true)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    ChangeDirection(1);
                    hitedRotator = false;
                    canWalk = true;
                }
            }
            if (rotatorHit.wichSideRotate[2] == true)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    ChangeDirection(2);
                    hitedRotator = false;
                    canWalk = true;
                }
            }
            if (rotatorHit.wichSideRotate[3] == true)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    ChangeDirection(3);
                    hitedRotator = false;
                    canWalk = true;
                }
            }
            break;
        }
        if(collectedPoints == pointsValue)
        {
            GameWin();
        }
        if(isEnd == true)
        {
            canWalk = false;
            endTimer -= Time.deltaTime;
            if(endTimer <= 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    public void ChangeDirection(int direction)
    {
        if (direction == 0)
        {
            rotateValue = 0;
        }
        if (direction == 1)
        {
            rotateValue = -90;
        }
        if (direction == 2)
        {
            rotateValue = -180;
        }
        if (direction == 3)
        {
            rotateValue = 90;
        }
        this.transform.rotation = Quaternion.Euler(0, 0, rotateValue);
        Debug.Log(rotateValue);
    }
    private void Controls()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeDirection(1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeDirection(3);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeDirection(0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeDirection(2);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Rotator")
        {
            Debug.Log("HittedCorner");
            rotatorHit = collision.gameObject.GetComponent<PlayerRotator>();
            hitedRotator = true;
        }
        if(collision.tag == "Point")
        {
            collectedPoints++;
            scores += 10;
            Destroy(collision.gameObject);
        }
        if(collision.tag == "Ghost")
        {
            GameLose();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Rotator")
        {
            rotatorHit = null;
            hitedRotator = false;
        }
    }
    void GameWin()
    {
        ui_Controller.wichTextIsOn = 1;
        isEnd = true;
        Debug.Log("YOU WON!");
    }
    void GameLose()
    {
        ui_Controller.wichTextIsOn = 2;
        isEnd = true;
        Debug.Log("YOU LOSE :( ");
    }
}
