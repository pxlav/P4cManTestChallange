using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    public GameObject[] uiTexts; // 0-ready;1-win;2-lose
    public bool isLost;
    [SerializeField] float endTimer;
    public int scores;
    public TextMeshProUGUI t_scores;
    private void Start()
    {
        scores = 0;
        canWalk = false;
        isLost = false;
        waitTimer = 3.0f;
        endTimer = 3.0f;
        uiTexts[0].SetActive(true);
        uiTexts[1].SetActive(false);
        uiTexts[2].SetActive(false);
    }
    private void Update()
    {
        t_scores.text = "Scores: " + scores.ToString();
        if(waitTimer > 0 && canWalk == false)
        {
            waitTimer -= Time.deltaTime;
            if(waitTimer <= 0)
            {
                uiTexts[0].SetActive(false);
                canWalk = true;
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

        while(hitedRotator == true)
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
        if(isLost == true)
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
        uiTexts[1].SetActive(true);
        Debug.Log("YOU WON!");
    }
    void GameLose()
    {
        isLost = true;
        uiTexts[2].SetActive(true);
        Debug.Log("YOU LOSE :( ");
    }
}
