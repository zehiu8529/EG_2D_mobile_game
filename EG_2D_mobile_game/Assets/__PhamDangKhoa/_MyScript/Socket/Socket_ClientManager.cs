using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Socket_ClientManager : MonoBehaviour
{
    #region Public Varible

    [Header("Client Manager Tag")]
    [SerializeField]
    private string s_Tag = "ClientManager";

    /// <summary>
    /// Host to Connect
    /// </summary>
    [Header("Network Server")]
    [SerializeField]
    private InputField inp_Host;

    /// <summary>
    /// Port for Connect to Host
    /// </summary>
    [SerializeField]
    private InputField inp_Port;

    /// <summary>
    /// Port for Connect to Host
    /// </summary>
    [SerializeField]
    private InputField inp_Message;

    /// <summary>
    /// IP on this Device or this Server
    /// </summary>
    private String s_LocalHost = "localhost";

    #endregion

    #region Private Varible

    //Socket

    /// <summary>
    /// Socket Connect OK?
    /// </summary>
    internal Boolean b_SocketStart = false;

    private TcpClient tcp_Socket;
    private NetworkStream net_Stream;
    private StreamWriter st_Writer;
    private StreamReader st_Reader;

    private string s_ReadData = "";

    //Data

    /// <summary>
    /// Step = 0 to Start
    /// </summary>
    private int i_Step = -1;

    /// <summary>
    /// Stack of Data get from Server
    /// </summary>
    [Header("Data Get from Server")]
    [SerializeField]
    private List<string> l_Data_Queue;

    [SerializeField]
    private Text t_Debug;

    private int i_Get = 4;

    public void Set_Get(int i_Get)
    {
        this.i_Get = i_Get;
    }

    #endregion

    private void Awake()
    {
        if (s_Tag != "")
        {
            this.tag = s_Tag;
        }
    }

    private void Start()
    {
        tcp_Socket = new TcpClient();

        if (b_SocketStart)
        {
            Debug.Log("Client: Socket on!");
        }

        l_Data_Queue = new List<string>();

        if (inp_Host != null)
        {
            inp_Host.text = "192.168.100.38";
        }

        if(inp_Port != null)
        {
            inp_Port.text = "5000";
        }
    }

    private void Update()
    {
        for (int i = 0; i < i_Get; i++) 
        {
            Set_Socket_Auto_Read();
        }
    }

    private void OnApplicationQuit()
    {
        Set_Socket_Close();
    }

    //Ready And Start

    /// <summary>
    /// Set Socket Ready
    /// </summary>
    public void Set_Socket_Start()
    {
        if (!b_SocketStart)
        {
            try
            {
                if(inp_Host == null || inp_Port == null)
                {
                    if (t_Debug != null)
                    {
                        t_Debug.text += "Set_Socket_Start: Require Input Field!" + "\n";
                    }

                    Debug.LogError("Set_Socket_Start: Require Input Field!");
                    return;
                }
                else
                {
                    if(inp_Port.text == "")
                    {
                        if (t_Debug != null)
                        {
                            t_Debug.text += "Set_Socket_Start: Port Require!" + "\n";
                        }

                        Debug.LogWarning("Set_Socket_Start: Port Require!");
                        return;
                    }

                    if(inp_Host.text == "")
                    {
                        if (t_Debug != null)
                        {
                            t_Debug.text += "Set_Socket_Start: Local Host Instead!" + "\n";
                        }

                        Debug.LogWarning("Set_Socket_Start: Local Host Instead!");
                        tcp_Socket.Connect(s_LocalHost, int.Parse(inp_Port.text));
                    }
                    else
                    {
                        if (t_Debug != null)
                        {
                            t_Debug.text += "Set_Socket_Start: " + inp_Host.text + " Connecting!" + "\n";
                            t_Debug.text += "Set_Socket_Start: Device " + SystemInfo.deviceUniqueIdentifier + "\n";
                        }

                        Debug.LogWarning("Set_Socket_Start: " + inp_Host.text + " Connecting!");
                        Debug.LogWarning("Set_Socket_Start: Device " + SystemInfo.deviceUniqueIdentifier);
                        tcp_Socket.Connect(inp_Host.text, int.Parse(inp_Port.text));
                    }
                }

                net_Stream = tcp_Socket.GetStream();
                net_Stream.ReadTimeout = 1;
                st_Writer = new StreamWriter(net_Stream);
                st_Reader = new StreamReader(net_Stream);
                b_SocketStart = true;

                if (t_Debug != null)
                {
                    t_Debug.text += "Client: Socket Start!" + "\n";
                }

                Debug.LogWarning("Client: Socket Start!");
            }
            catch (Exception e)
            {
                if (t_Debug != null)
                {
                    t_Debug.text += "Client: Socket error '" + e + "'" + "\n";
                }

                Debug.LogError("Client: Socket error '" + e + "'");
            }
        }
    }
    
    //Write

    /// <summary>
    /// Sent Data to Server
    /// </summary>
    /// <param name="s_Data"></param>
    public void Set_Socket_Write(bool b_DeviceID, string s_Data)
    {
        if (!b_SocketStart)
            return;
        String foo = ((b_DeviceID) ? (SystemInfo.deviceUniqueIdentifier + ":") : "") + s_Data + "\r\n";
        st_Writer.Write(foo);
        st_Writer.Flush();

        if (t_Debug != null)
        {
            t_Debug.text += "Set_Socket_Write: " + s_Data + "\n";
        }

        Debug.Log("Set_Socket_Write: " + s_Data);
    }

    /// <summary>
    /// Sent Data to Server by InputField
    /// </summary>
    public void Set_Socket_Write(bool b_DeviceID)
    {
        if (!b_SocketStart)
            return;
        String foo = ((b_DeviceID) ? (SystemInfo.deviceUniqueIdentifier + ":") : "") + inp_Message.text + "\r\n";
        st_Writer.Write(foo);
        st_Writer.Flush();

        if (t_Debug != null)
        {
            t_Debug.text += "Set_Socket_Write: " + inp_Message.text + "\n";
        }

        Debug.Log("Set_Socket_Write: " + inp_Message.text);
    }

    //Read

    /// <summary>
    /// Get Data from Server
    /// </summary>
    /// <returns></returns>
    private String Get_Socket_Read()
    {
        if (!b_SocketStart)
            return "";
        if (net_Stream.DataAvailable)
            return st_Reader.ReadLine();
        return "";
    }

    /// <summary>
    /// Get Data Auto from Server
    /// </summary>
    private void Set_Socket_Auto_Read()
    {
        s_ReadData = Get_Socket_Read();
        if (!s_ReadData.Equals(""))
        {
            l_Data_Queue.Add(s_ReadData);

            if(t_Debug != null)
            {
                t_Debug.text += "Set_Socket_Auto_Read: " + s_ReadData + "\n";
            }

            Debug.Log("Set_Socket_Auto_Read: " + s_ReadData);
        }
    }

    /// <summary>
    /// Get Data Queue Read from Server add Local
    /// </summary>
    /// <returns></returns>
    public string Get_Socket_Queue_Read()
    {
        if (!Get_Socket_Queue_Read_Exist())
        {
            return "";
        }
        string s_DataGet = l_Data_Queue[0];
        l_Data_Queue.RemoveAt(0);
        return s_DataGet;
    }

    /// <summary>
    /// Get Data Read Queue from Server
    /// </summary>
    /// <returns></returns>
    public bool Get_Socket_Queue_Read_Exist()
    {
        return l_Data_Queue.Count > 0;
    }

    //Close

    /// <summary>
    /// Close Connect to Server
    /// </summary>
    public void Set_Socket_Close()
    {
        if (!b_SocketStart)
            return;
        Set_Socket_Write(false, "Exit");
        st_Writer.Close();
        st_Reader.Close();
        tcp_Socket.Close();
        b_SocketStart = false;

        if (t_Debug != null)
        {
            t_Debug.text += "Set_Socket_Close: Called!" + "\n";
        }

        Debug.LogWarning("Set_Socket_Close: Called!");
    }
}