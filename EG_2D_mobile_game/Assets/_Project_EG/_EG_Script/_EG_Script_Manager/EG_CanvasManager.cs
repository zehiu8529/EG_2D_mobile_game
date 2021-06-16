using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EG_CanvasManager : MonoBehaviour
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

    [SerializeField]
    private Image i_Bag_Button;

    [SerializeField]
    private Sprite s_Bag_Open;

    [SerializeField]
    private Sprite s_Bag_Close;

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
            i_Bag_Button.sprite = s_Bag_Close;
        }
        else
        {
            a_BagPannel.SetTrigger("Trig_Hide");
            i_Bag_Button.sprite = s_Bag_Open;
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
