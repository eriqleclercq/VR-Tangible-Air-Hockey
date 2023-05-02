using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool IsOpponentGoal;
    public ScoreManager manager;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Puck")
        {
            //Once the puck enter the goal, the goal calls a function from the manager that handles the scoring
            manager.GoalScored(IsOpponentGoal); 
        }
    }
}
