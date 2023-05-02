using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{

    public Rigidbody puck;
    public Transform puckStartPosBot;
    public Transform puckStartPosPlayer;

    private int playerScore = 0;
    private int botScore = 0; 
    [SerializeField]
    private TMP_Text _botScoreTxt;
    [SerializeField]
    private TMP_Text _playerScoreTxt;



    private void Start() {
        //Setting the initial score of the players
        _botScoreTxt.text = "0"; 
        _playerScoreTxt.text = "0";
        puck = puck.GetComponent<Rigidbody>();
    }

    //When a goal is scored, we check who scored the goal via the use of the isOpponent flag
    //This flag indicated whether the goal that calls this function is from the opponent or from our user
    public void GoalScored(bool isOpponent)
    {
        //User scored a goal
        if (isOpponent)
        {
            playerScore++;
            _playerScoreTxt.text = playerScore.ToString();
            puck.position = puckStartPosBot.position;
            puck.velocity = new Vector3(0,0,0);
        }
        //Bot scored a goal
        else
        {
            botScore++;
            _botScoreTxt.text = botScore.ToString();
            puck.position = puckStartPosPlayer.position;
            puck.velocity = new Vector3(0,0,0);
        }
    }
}
