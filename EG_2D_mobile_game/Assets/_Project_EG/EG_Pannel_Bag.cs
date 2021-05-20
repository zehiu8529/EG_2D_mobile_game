using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EG_Pannel_Bag : MonoBehaviour
{
    /// <summary>
    /// Animator
    /// </summary>
    private Animator a_Animator;

    /// <summary>
    /// Sprite in Show
    /// </summary>
    [SerializeField]
    private Sprite sp_inShow;

    /// <summary>
    /// Sprite in Hide
    /// </summary>
    [SerializeField]
    private Sprite sp_inHide;

    /// <summary>
    /// Button Image
    /// </summary>
    [SerializeField]
    private Image i_ButtonBag;

    private bool b_BagShow = false;

    private void Start()
    {
        a_Animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Button Pressed
    /// </summary>
    public void Button_Pressed()
    {
        if (b_BagShow)
        {
            Button_Hide();
        }
        else
        {
            Button_Show();
        }
        b_BagShow = !b_BagShow;
    }

    /// <summary>
    /// Button Show
    /// </summary>
    private void Button_Show()
    {
        a_Animator.SetTrigger("Trig_Show");
        i_ButtonBag.sprite = sp_inShow;
    }

    /// <summary>
    /// Button Hide
    /// </summary>
    private void Button_Hide()
    {
        a_Animator.SetTrigger("Trig_Hide");
        i_ButtonBag.sprite = sp_inHide;
    }
}
