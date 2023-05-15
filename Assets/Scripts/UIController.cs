using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public Player p_player;
    public TextMeshProUGUI t_scores;
    public int wichTextIsOn; // 0-none;1-ready;2-youwon;3-youlose
    public GameObject scoreObj;
    public GameObject[] readyWonLose;
    private void Start()
    {
        wichTextIsOn = 0;
    }

    void Update()
    {
        t_scores.text = p_player.scores.ToString();
        if(wichTextIsOn == 0)
        {
            readyWonLose[0].SetActive(true);
            readyWonLose[1].SetActive(false);
            readyWonLose[2].SetActive(false);
        }
        if(wichTextIsOn == 1)
        {
            readyWonLose[0].SetActive(false);
            readyWonLose[1].SetActive(true);
            readyWonLose[2].SetActive(false);
        }
        if(wichTextIsOn == 2)
        {
            readyWonLose[0].SetActive(false);
            readyWonLose[1].SetActive(false);
            readyWonLose[2].SetActive(true);
        }
        if (wichTextIsOn == 3)
        {
            readyWonLose[0].SetActive(false);
            readyWonLose[1].SetActive(false);
            readyWonLose[2].SetActive(false);
        }
    }
}
