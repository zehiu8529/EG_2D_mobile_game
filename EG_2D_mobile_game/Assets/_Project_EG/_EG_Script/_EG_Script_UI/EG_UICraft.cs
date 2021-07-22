using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG_UICraft : MonoBehaviour
{
    [Header("Craft Pannel")]
    [SerializeField]
    private Animator a_CraftPannel;

    private bool b_CraftPannel_Show = false;

    private EG_UIBag cl_EGBag;

    private void Start()
    {
        cl_EGBag = GetComponent<EG_UIBag>();
    }

    public void Set_CraftActive()
    {
        if (!b_CraftPannel_Show)
        {
            a_CraftPannel.SetTrigger("Trig_Show");
            b_CraftPannel_Show = true;
            cl_EGBag.Set_BagActive();
        }
    }

    public void Button_CraftClose()
    {
        if (b_CraftPannel_Show)
        {
            a_CraftPannel.SetTrigger("Trig_Hide");
            b_CraftPannel_Show = false;
        }
    }

    public bool Get_CraftShow()
    {
        return b_CraftPannel_Show;
    }
}
