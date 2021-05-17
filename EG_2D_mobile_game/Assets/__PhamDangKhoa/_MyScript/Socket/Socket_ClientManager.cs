using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class Socket_ClientManager : MonoBehaviour
{
    [Header("Client Manager Tag")]
    [SerializeField]
    private string s_Tag = "ClientManager";

    /// <summary>
    /// If Test Network on this Device
    /// </summary>
    [Header("Network Server")]
    [SerializeField]
    private bool b_LocalHost = true;

    /// <summary>
    /// IP on other Device or other Server
    /// </summary>
    [SerializeField]
    private String s_Host = "192.168.100.38";

    /// <summary>
    /// IP on this Device or this Server
    /// </summary>
    private String s_LocalHost = "localhost";

    /// <summary>
    /// Port on Server
    /// </summary>
    [SerializeField]
    private Int32 Port = 5056;

    internal Boolean socketReady = false;

    private TcpClient mySocket;
    private NetworkStream theStream;
    private StreamWriter theWriter;
    private StreamReader theReader;

    private string s_ReadData = "";

    private void Awake()
    {
        if (s_Tag != "")
        {
            this.tag = s_Tag;
        }
    }

    private void Start()
    {
        mySocket = new TcpClient();

        if (socketReady)
        {
            Debug.Log("Client: Socket on!");
        }
    }

    private void Update()
    {
        if (!socketReady)
        {
            Set_SocketReady();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Set_Socket_Write("Exit");
            Set_Socket_Close();
        }

        s_ReadData = Get_Socket_Read();
        if (!s_ReadData.Equals(""))
        {
            Debug.Log("Client: Get '" + s_ReadData + "'");
        }
    }

    private void OnDestroy()
    {
        
    }

    private void OnApplicationQuit()
    {
        if (mySocket != null && mySocket.Connected)
            mySocket.Close();
    }

    /// <summary>
    /// Set Socket Ready
    /// </summary>
    private void Set_SocketReady()
    {
        try
        {
            if (b_LocalHost)
            {
                mySocket.Connect(s_LocalHost, Port);
            }
            else
            {
                mySocket.Connect(s_Host, Port);
            }
            theStream = mySocket.GetStream();
            theStream.ReadTimeout = 1;
            theWriter = new StreamWriter(theStream);
            theReader = new StreamReader(theStream);
            socketReady = true;
            Debug.Log("Client: Socket Send!");
        }
        catch (Exception e)
        {
            Debug.Log("Client: Socket error '" + e + "'");
        }
    }

    /// <summary>
    /// Sent Data to Server
    /// </summary>
    /// <param name="s_Data"></param>
    public void Set_Socket_Write(string s_Data)
    {
        if (!socketReady)
            return;
        String foo = s_Data + "\r\n";
        theWriter.Write(foo);
        theWriter.Flush();
        Debug.Log("Client: Send '" + s_Data + "'");
    }

    /// <summary>
    /// Get Data from Server
    /// </summary>
    /// <returns></returns>
    private String Get_Socket_Read()
    {
        if (!socketReady)
            return "";
        if (theStream.DataAvailable)
            return theReader.ReadLine();
        return "";
    }

    /// <summary>
    /// Close Connect to Server
    /// </summary>
    private void Set_Socket_Close()
    {
        if (!socketReady)
            return;
        theWriter.Close();
        theReader.Close();
        mySocket.Close();
        socketReady = false;
    }

    public string Get_Data()
    {
        return s_ReadData;
    }
}