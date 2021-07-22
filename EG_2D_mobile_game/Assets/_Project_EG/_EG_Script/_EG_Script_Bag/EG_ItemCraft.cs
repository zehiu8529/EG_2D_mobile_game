using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG_ItemCraft : MonoBehaviour
{
    [Header("Poision Require")]
    [SerializeField]
    private int i_Poision_Red_Require;

    [SerializeField]
    private int i_Poision_Blue_Require;

    [SerializeField]
    private int i_Poision_Green_Require;

    [Header("Item Text")]
    [SerializeField]
    [TextArea(1, 10)]
    private string s_ItemText;

    public int Get_Poision_Red_Require()
    {
        return i_Poision_Red_Require;
    }

    public int Get_Poision_Blue_Require()
    {
        return i_Poision_Blue_Require;
    }

    public int Get_Poision_Green_Require()
    {
        return i_Poision_Green_Require;
    }

    public string Get_ItemText()
    {
        return s_ItemText;
    }
}
