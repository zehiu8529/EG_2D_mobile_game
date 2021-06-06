using System.Collections.Generic;
using UnityEngine;

public class ListVertical_Componnet : MonoBehaviour
{
    /// <summary>
    /// ListContent
    /// </summary>
    public Transform t_ListContent;

    /// <summary>
    /// List Prepab inside ListContent
    /// </summary>
    private List<GameObject> l_Prepab;

    /// <summary>
    /// List ID for Prepab inside ListContent
    /// </summary>
    private List<string> l_ID;

    private void Start()
    {
        l_Prepab = new List<GameObject>();

        l_ID = new List<string>();
    }

    //Clear

    /// <summary>
    /// Clear All List
    /// </summary>
    public void Set_ListPrepab_Clear()
    {
        Debug.Log("Set_ListPrepab_Clear");
        for(int i=0; i < l_Prepab.Count; i++)
        {
            Destroy(l_Prepab[i].gameObject);
        }
        l_Prepab.Clear();
        l_ID.Clear();
    }

    //Add

    /// <summary>
    /// Add to List
    /// </summary>
    /// <param name="g_Prepab"></param>
    public void Set_ListPrepab(GameObject g_Prepab, string s_ID)
    {
        int i_Exist = Get_Prepab_Index(s_ID);

        if(i_Exist == Get_Prepab_Index_NotFound())
        {
            Class_Object cs_Object = new Class_Object();
            l_Prepab.Add(cs_Object.Set_Prepab_Create(g_Prepab, t_ListContent));
            l_ID.Add(s_ID);
        }
        else
        {
            Set_ListPrepab(g_Prepab, i_Exist);
        }
    }

    /// <summary>
    /// Chance Prepab Object in List
    /// </summary>
    /// <param name="g_PrepabChanged"></param>
    /// <param name="i_Index"></param>
    public void Set_ListPrepab(GameObject g_PrepabChanged, int i_Index)
    {
        l_Prepab[i_Index] = g_PrepabChanged;
    }

    //Delete

    /// <summary>
    /// Delete in List
    /// </summary>
    /// <param name="i_Index"></param>
    public void Set_ListPrepab_Delete(int i_Index)
    {
        Destroy(l_Prepab[i_Index].gameObject);
        l_Prepab.RemoveAt(i_Index);
        l_ID.RemoveAt(i_Index);
    }

    //Count

    /// <summary>
    /// Count in List
    /// </summary>
    /// <returns></returns>
    public int Get_ListPrepab_Count()
    {
        return l_Prepab.Count;
    }

    //Get

    /// <summary>
    /// Get Prepab Object on List
    /// </summary>
    /// <param name="i_Index"></param>
    /// <returns></returns>
    public GameObject Get_Prepab(int i_Index)
    {
        return l_Prepab[i_Index];
    }

    /// <summary>
    /// Get Index of List by ID
    /// </summary>
    /// <param name="s_ID_Find"></param>
    /// <returns>Index of ID and Prepab (If not found, return -1)</returns>
    public int Get_Prepab_Index(string s_ID_Find)
    {
        if(l_ID.Count == 0)
            return Get_Prepab_Index_NotFound();

        for (int i = 0; i < l_ID.Count; i++)
        {
            if (l_ID[i] == s_ID_Find)
                return i;
        }
        return Get_Prepab_Index_NotFound();
    }

    /// <summary>
    /// Get "Not Found" Value when use "Get_Prepab_Index()"
    /// </summary>
    /// <returns></returns>
    public int Get_Prepab_Index_NotFound()
    {
        return -1;
    }

    /// <summary>
    /// Get ID
    /// </summary>
    /// <param name="i_Index"></param>
    /// <returns></returns>
    public string Get_Prepab_ID(int i_Index)
    {
        return l_ID[i_Index];
    }

    //Request

    /// <summary>
    /// Request from Button
    /// </summary>
    private int i_Request = -1;

    /// <summary>
    /// Just Use in Button for Send a Request
    /// </summary>
    public void Set_Button_Request(GameObject g_ButtonObject)
    {
        i_Request = l_Prepab.IndexOf(g_ButtonObject);
    }

    /// <summary>
    /// Get Current Request
    /// </summary>
    /// <returns></returns>
    public int Get_Button_Request()
    {
        return i_Request;
    }

    /// <summary>
    /// Reset Request
    /// </summary>
    public void Set_Button_Request_Reset()
    {
        this.i_Request = -1;
    }

    /// <summary>
    /// Get the Value for Not have any Request found
    /// </summary>
    /// <returns></returns>
    public int Get_Button_Request_Not()
    {
        return -1;
    }
}
