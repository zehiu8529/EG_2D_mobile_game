using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG_UIManager : MonoBehaviour
{
    #region Public Varible

    /// <summary>
    /// Game Canvas after Socket Canvas
    /// </summary>
    [Header("Game Canvas")]
    [SerializeField]
    private GameObject g_GameCanvas;

    /// <summary>
    /// Socket Canvas before Player Canvas
    /// </summary>
    [Header("Socket Canvas")]
    [SerializeField]
    private GameObject g_SocketCanvas;

    /// <summary>
    /// Animator of Bag Pannel (On/Off)
    /// </summary>
    [Header("Bag Pannel")]
    [SerializeField]
    private Animator a_BagPannel;

    #endregion

    #region Private Varible

    /// <summary>
    /// Bag Pannel Show?
    /// </summary>
    private bool b_BagPannel_Show = false;

    #endregion

    private void Start()
    {
        g_GameCanvas.SetActive(false);
        g_SocketCanvas.SetActive(true);
    }

    /// <summary>
    /// Bag Pannel
    /// </summary>
    public void Button_BagPannel()
    {
        if (!b_BagPannel_Show)
        {
            a_BagPannel.SetTrigger("Trig_Show");
        }
        else
        {
            a_BagPannel.SetTrigger("Trig_Hide");
        }
        b_BagPannel_Show = !b_BagPannel_Show;
    }

    /// <summary>
    /// Socket Pannel
    /// </summary>
    public void Button_SocketCanvas()
    {
        g_GameCanvas.SetActive(true);
        g_SocketCanvas.SetActive(false);
    }
}
