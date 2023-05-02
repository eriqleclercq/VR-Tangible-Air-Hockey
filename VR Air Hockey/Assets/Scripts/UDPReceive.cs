using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System;


//UDP code from: https://youtu.be/rPVy-OzWatM
public class UDPReceive : MonoBehaviour
{
    Thread receiveThread;
    UdpClient client;
    public int port = 5055;
    public bool startReceiving = true;
    public bool printToConsole = false;
    public string data;

    // Start is called before the first frame update
    void Start()
    {
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }
    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (startReceiving)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataByte = client.Receive(ref anyIP);
                data = Encoding.UTF8.GetString(dataByte);
                if (printToConsole)
                {
                    print(data);
                }
            }
            catch (Exception err)
            {
                data = null;
                Debug.Log("Something went wrong with receiving data");
            }
        }
    }
}
