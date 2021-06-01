using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isometric_CheckObject : MonoBehaviour
{
    /// <summary>
    /// Tag for other Isometric Object to Find
    /// </summary>
    [Header("Isometric Map Tag")]
    [SerializeField]
    private string s_Tag = "IsometricMap";

    [SerializeField]
    private GameObject g_MapManager;

    /// <summary>
    /// List Isometric GameObject Object to Avoid
    /// </summary>
    [Header("Object List to Check")]
    [SerializeField]
    private List<GameObject> l_ObjectCheck;

    #region Private Varible

    /// <summary>
    /// Map Manager of Matrix Map Code and Matrix Map Isometric GameObject
    /// </summary>
    private Isometric_MapManager cl_MapManager_MapManager;

    #endregion

    private void Start()
    {
        if (g_MapManager == null)
        {
            if (s_Tag != "")
            {
                g_MapManager = GameObject.FindGameObjectWithTag(s_Tag);

                if (g_MapManager == null)
                {
                    Debug.LogError(this.name + ": Not found 'MapManager GameObject' with tag: " + s_Tag);
                }
            }
        }

        cl_MapManager_MapManager = g_MapManager.GetComponent<Isometric_MapManager>();
    }

    /// <summary>
    /// Check Square Avoid or Accept to Move in?
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns>If TRUE >> OBJECT ACCEPT</returns>
    public bool Get_Check_Object_Accept(Vector2Int v2_Pos, Vector2Int v2_Dir)
    {
        for (int i = 0; i < l_ObjectCheck.Count; i++)
        {
            if (cl_MapManager_MapManager.Get_MatrixCode_Object(v2_Pos + v2_Dir) == l_ObjectCheck[i].GetComponent<Isometric_Single>().Get_SingleCode())
            {
                return false;
            }
        }
        return true;
    }

}
