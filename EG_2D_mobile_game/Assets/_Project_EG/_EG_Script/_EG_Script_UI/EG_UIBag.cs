using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EG_UIBag : MonoBehaviour
{
    #region Public Varible

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

    /// <summary>
    /// Bag Pannel
    /// </summary>
    public void Button_BagActive()
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

    public void Set_BagActive()
    {
        if (!b_BagPannel_Show)
        {
            a_BagPannel.SetTrigger("Trig_Show");
            i_Bag_Button.sprite = s_Bag_Close;
            b_BagPannel_Show = true;
        }
    }
}
