using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EG_CanvasManager : MonoBehaviour
{
    #region Public Varible

    [Header("Socket Canvas")]
    [SerializeField]
    private Socket_ClientManager cl_SocketClientManager;

    /// <summary>
    /// Socket Canvas before Player Canvas
    /// </summary>
    [SerializeField]
    private GameObject g_SocketCanvas;

    /// <summary>
    /// Character Canvas before Player Canvas
    /// </summary>
    [SerializeField]
    private GameObject g_CharacterCanvas;

    /// <summary>
    /// Game Canvas after Socket Canvas
    /// </summary>
    [Header("Game Canvas")]
    [SerializeField]
    private GameObject g_GameCanvas;

    private int i_Step = 0;

    #endregion

    private void Start()
    {
        g_GameCanvas.SetActive(false);
        g_SocketCanvas.SetActive(true);
    }

    private void Update()
    {
        switch (i_Step)
        {
            case 0:
                if (cl_SocketClientManager.Get_Socket_Start())
                {
                    g_SocketCanvas.SetActive(false);
                    g_CharacterCanvas.SetActive(true);

                    i_Step = 1;
                }
                break;
            case 1:
                if (cl_SocketClientManager.Get_SocketThread_Read())
                {
                    g_CharacterCanvas.SetActive(false);
                    g_GameCanvas.SetActive(true);

                    i_Step = 2;
                }
                break;
        }
    }
}
