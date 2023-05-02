using System;
using Unity.VisualScripting;
using UnityEngine;

public class BotScript : MonoBehaviour
{
    public float MaxSpeed;
    private Rigidbody rb;
    private Vector3 startPos;

    public Rigidbody puck;
    public Transform PlayerBoundaryHolder;
    public Transform StartingPosition;
    public Transform GroundLevel;
    private Boundary playerBoundary;
    public RigidbodyInterpolation interpolation;

    public Transform PuckBoundaryHolder;
    private Boundary puckBoundary;

    private Vector3 targetPos;
    public float MovementSpeedTowardsPuck;
    public float PrecisionScaleFactor;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = new Vector3(StartingPosition.position.x, GroundLevel.position.y, StartingPosition.position.z);
        rb.MovePosition(startPos);


        //The orientation of the boundary is by the player's point of view
        playerBoundary = new Boundary(PlayerBoundaryHolder.GetChild(0),
                                      PlayerBoundaryHolder.GetChild(1),
                                      PlayerBoundaryHolder.GetChild(2),
                                      PlayerBoundaryHolder.GetChild(3));

        puckBoundary = new Boundary(PuckBoundaryHolder.GetChild(0),
                                    PuckBoundaryHolder.GetChild(1),
                                    PuckBoundaryHolder.GetChild(2),
                                    PuckBoundaryHolder.GetChild(3));
    }

    private void Update()
    {

        //ScaleFactor at 0.4 seems like a very good agent
        var distToPuck = Vector3.Distance(rb.position, puck.position);
        //Some maths to figure out where the puck will be in the future
        transform.LookAt(puck.position + distToPuck*(PrecisionScaleFactor * puck.velocity * distToPuck));

        if (puck.position.z > puckBoundary.Up.z) //The puck entered the bot's half of the table
        {
            var xDir = (puck.position - rb.position).normalized;
            rb.AddRelativeForce(new Vector3(0, 0, MovementSpeedTowardsPuck), ForceMode.VelocityChange);
        }
        else //the puck is not on the bot's half of the table, therefore it goes back to its starting position
        {
            var dirHome = Vector3.MoveTowards(rb.position, startPos, MaxSpeed* Time.deltaTime);
            rb.velocity = new Vector3(0,0,0);
            rb.MovePosition(new Vector3(dirHome.x, rb.position.y, dirHome.z));
        }

        //Reset the bot to its origin in the case of it leaving its boundary
        if((rb.position.x < playerBoundary.Right.x || rb.position.x > playerBoundary.Left.x) || 
            (rb.position.z > playerBoundary.Down.z || rb.position.z < playerBoundary.Up.z))
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.MovePosition(startPos);
        }

    }
}

