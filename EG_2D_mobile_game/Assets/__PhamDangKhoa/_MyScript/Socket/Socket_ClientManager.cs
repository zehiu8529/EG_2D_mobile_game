using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/*
 * Android Run Circle Life:
 * - onCreate()      | 
 * - onStart()       |
 * - onResume()      | OnApplicationPause(false)
 * - ActivityRunning |
 * - onPause()       | OnApplicationPause(true)
 * - onStop()        |
 * - onDestroy()     | OnApplicationQuit()
 */

public class Socket_ClientManager : MonoBehaviour
{
    #region Public Varible

    /// <summary>
    /// Tag with Host
    /// </summary>
    [Header("Network Host Server")]
    [SerializeField]
    private string s_Tag_Host = "SocketHost";

    /// <summary>
    /// Host to Connect
    /// </summary>
    [SerializeField]
    private InputField inp_Host;

    /// <summary>
    /// Tag with Port
    /// </summary>
    [Header("Network Port Server")]
    [SerializeField]
    private string s_Tag_Port = "SocketPort";

    /// <summary>
    /// Port for Connect to Host
    /// </summary>
    [SerializeField]
    private InputField inp_Port;

    /// <summary>
    /// Auto Connect on Start
    /// </summary>
    [Header("Network On Start")]
    [SerializeField]
    private bool b_AutoConnect = true;

    /// <summary>
    /// Host to Connect or Connected
    /// </summary>
    [SerializeField]
    private string s_HostConnect = "192.168.100.38";

    /// <summary>
    /// Port to Connect or Connected
    /// </summary>
    [SerializeField]
    private string s_PortConnect = "5000";

    /// <summary>
    /// Auto Read by Thread
    /// </summary>
    [SerializeField]
    private bool b_AutoRead = true;

    #endregion

    #region Private Varible

    //Socket

    /// <summary>
    /// IP on this Device or this Server
    /// </summary>
    private string s_LocalHost = "localhost";

    /// <summary>
    /// Socket Connect OK?
    /// </summary>
    private bool b_SocketStart = false;

    /// <summary>
    /// Socket Auto Thread Read?
    /// </summary>
    private bool b_SocketRead = false;

    private TcpClient tcp_Socket;
    private NetworkStream net_Stream;
    private StreamWriter st_Writer;
    private StreamReader st_Reader;

    //Data

    private Thread th_GetData;

    [Header("Network Auto Read")]
    [SerializeField]
    private List<string> l_DataQueue;

    #endregion

    private void Start()
    {
        if (inp_Host == null)
        {
            inp_Host = GameObject.FindGameObjectWithTag(s_Tag_Host).GetComponent<InputField>();
        }
        if (inp_Host != null)
        {
            if (inp_Host.text == "")
            {
                inp_Host.text = s_HostConnect;
            }
        }

        if (inp_Port == null)
        {
            inp_Port = GameObject.FindGameObjectWithTag(s_Tag_Port).GetComponent<InputField>();
        }
        if (inp_Port != null)
        {
            if (inp_Port.text == "")
            {
                inp_Port.text = s_PortConnect;
            }
        }

        l_DataQueue = new List<string>();

        if (b_AutoConnect)
        {
            Set_Socket_Start();
        }

        if (b_AutoRead)
        {
            Set_SocketThread_Read(true);
        }

        th_GetData = new Thread(Set_SocketThread_AutoRead);
        th_GetData.Start();
    }

    private void OnDestroy()
    {
        Set_CloseApplication();
    }

    private void OnApplicationQuit()
    {
        Set_CloseApplication();
    }

    private void OnApplicationPause(bool b_OnPause)
    {
        //Android Event onResume() and onPause()
        if (b_OnPause)
        {
            Set_CloseApplication();
        }
        else
        {
            Set_Socket_Start();
        }
    }

    private void Set_CloseApplication()
    {
        if (th_GetData != null)
        {
            if (th_GetData.IsAlive)
            {
                th_GetData.Abort();
            }
        }

        Set_Socket_Close();
    }

    #region Thread Read Data

    /// <summary>
    /// Auto Read Data for Debug
    /// </summary>
    private void Set_SocketThread_AutoRead()
    {
        while (true)
        {
            if (b_SocketStart)
            //If Socket Started
            {
                if (b_SocketRead)
                //If Socket Read
                {
                    string s_DataGet = Get_SocketData_Read();
                    if (s_DataGet != "")
                    {
                        //Debug.Log("Set_Thread_AutoRead: " + s_DataGet);
                        l_DataQueue.Add(s_DataGet);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Set Socket Read by Thread
    /// </summary>
    /// <param name="b_SocketRead"></param>
    public void Set_SocketThread_Read(bool b_SocketRead)
    {
        this.b_SocketRead = b_SocketRead;
    }

    /// <summary>
    /// Get Socket Read by Thread
    /// </summary>
    /// <returns></returns>
    public bool Get_SocketThread_Read()
    {
        return b_SocketRead;
    }

    #endregion

    #region Start Connect to Server

    /// <summary>
    /// Set Socket Ready
    /// </summary>
    public void Set_Socket_Start()
    {
        if (!Get_Socket_Start())
        {
            try
            {
                tcp_Socket = new TcpClient();

                if (inp_Port == null || inp_Port == null)
                {
                    Debug.LogError("Set_Socket_Start: Require Input Field!");
                    return;
                }
                else
                {
                    if (inp_Port.text == "")
                    {
                        Debug.LogWarning("Set_Socket_Start: Port Require!");
                        return;
                    }

                    if (inp_Host.text == "")
                    {
                        Debug.LogWarning("Set_Socket_Start: Local Host Instead!");
                        Debug.LogWarning("Set_Socket_Start: Device " + SystemInfo.deviceUniqueIdentifier);
                        tcp_Socket.Connect(s_LocalHost, int.Parse(inp_Port.text));
                    }
                    else
                    {
                        Debug.LogWarning("Set_Socket_Start: Host " + inp_Host.text);
                        Debug.LogWarning("Set_Socket_Start: Device " + SystemInfo.deviceUniqueIdentifier);
                        tcp_Socket.Connect(inp_Host.text, int.Parse(inp_Port.text));
                    }
                }

                net_Stream = tcp_Socket.GetStream();
                net_Stream.ReadTimeout = 1;
                st_Writer = new StreamWriter(net_Stream);
                st_Reader = new StreamReader(net_Stream);

                s_HostConnect = inp_Host.text;
                s_PortConnect = inp_Port.text;

                b_SocketStart = true;

                Debug.LogWarning("Set_Socket_Start: Socket Start!");
            }
            catch (Exception e)
            {
                Debug.LogError("Set_Socket_Start: Socket error '" + e + "'");
            }
        }
    }

    /// <summary>
    /// Socket is Started?
    /// </summary>
    /// <returns></returns>
    public bool Get_Socket_Start()
    {
        return b_SocketStart;
    }

    #endregion

    #region Write Data to Server

    /// <summary>
    /// Sent Data to Server
    /// </summary>
    /// <param name="s_Data"></param>
    public void Set_Socket_Write(string s_Data)
    {
        if (!Get_Socket_Start())
            return;
        String foo = s_Data + "\r\n";
        st_Writer.Write(foo);
        st_Writer.Flush();

        Debug.Log("Set_Socket_Write: " + s_Data);
    }

    #endregion

    #region Read Data from Server

    //Socket

    /// <summary>
    /// Get Data from Server
    /// </summary>
    /// <remarks>
    /// Should use this in 'void FixedUpdate()' or use with 'Thread'
    /// </remarks>
    /// <returns></returns>
    private string Get_SocketData_Read()
    {
        if (!Get_Socket_Start())
        {
            return "";
        }
        if (net_Stream.DataAvailable)
        {
            string s_ReadData = st_Reader.ReadLine();
            Debug.Log("Get_Socket_Read: " + s_ReadData);
            return s_ReadData;
        }
        return "";
    }

    //Queue

    /// <summary>
    /// Get Data from Queue List
    /// </summary>
    /// <returns></returns>
    public string Get_SocketQueue_Read()
    {
        if (Get_SocketQueue_Count() <= 0)
        {
            return "";
        }
        string s_DataGet = l_DataQueue[0];
        l_DataQueue.RemoveAt(0);
        return s_DataGet;
    }

    /// <summary>
    /// Get Data Exist from Queue List
    /// </summary>
    /// <returns></returns>
    public int Get_SocketQueue_Count()
    {
        return l_DataQueue.Count;
    }

    #endregion

    #region Close Connect

    /// <summary>
    /// Close Connect to Server
    /// </summary>
    public void Set_Socket_Close()
    {
        if (!Get_Socket_Start())
            return;
        Set_Socket_Write("Exit");
        st_Writer.Close();
        st_Reader.Close();
        tcp_Socket.Close();
        b_SocketStart = false;

        Debug.LogWarning("Set_Socket_Close: Called!");
    }

    #endregion

    /// <summary>
    /// Get ID of this Device
    /// </summary>
    /// <returns></returns>
    public string Get_DeviceID()
    {
        return SystemInfo.deviceUniqueIdentifier;
    }
}