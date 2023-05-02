using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    public UDPReceive Receive;
    float[] position;
    float[] oldpos;


    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Every frame we try to read the UDP message to check whether the user is holding the tangible or not
        //If there is a message, it means that the user is not holding the tangible, therefore we set the position of
        //the tangible to the position that has been broadcast to us
        try
        {
        string data = Receive.data;
        if(data != null)
        {
            data = data.Trim(new char[] {'(', ')'});
            position = System.Array.ConvertAll(data.Split(','), float.Parse);
            position[0] = position[0];
            position[1] = position[1];
            rb.velocity = new Vector3(0,0,0);
            rb.MovePosition(new Vector3(position[0], transform.position.y, position[1]));
            Receive.data = null; //Once we read the data we erase it          
        }
        }
        catch
        {

        }
        
    }
}
