using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Check Ground to Avoid
/// </summary>
[RequireComponent(typeof(Isometric_MoveControl))]
public class Isometric_CheckGround : MonoBehaviour
{
    /// <summary>
    /// Tag for other Isometric Object to Find
    /// </summary>
    [Header("Isometric Map Tag")]
    [SerializeField]
    private string s_Tag = "IsometricMap";

    /// <summary>
    /// Get MapManager GameObject
    /// </summary>
    [SerializeField]
    private GameObject g_MapManager;

    /// <summary>
    /// List Isometric GameObject Ground to Avoid
    /// </summary>
    [Header("Ground List to Check")]
    [SerializeField]
    private List<GameObject> l_Ground;

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
            }
        }

        cl_MapManager_MapManager = g_MapManager.GetComponent<Isometric_MapManager>();
    }

    /// <summary>
    /// Check Square Avoid or Accept to Move in?
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns>If TRUE >> GROUND ACCEPT</returns>
    public bool Get_Check_Ground_Accept(Vector2Int v2_Pos, Vector2Int v2_Dir)
    {
        for(int i = 0; i < l_Ground.Count; i++)
        {
            if(cl_MapManager_MapManager.Get_MatrixCode_Ground(v2_Pos + v2_Dir) == l_Ground[i].GetComponent<Isometric_Single>().Get_SingleCode())
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Check Square Avoid or Accept to Move in?
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns>If TRUE >> GROUND ACCEPT</returns>
    public bool Get_Check_Ground_Accept(Vector2Int v2_Pos)
    {
        for (int i = 0; i < l_Ground.Count; i++)
        {
            if (cl_MapManager_MapManager.Get_MatrixCode_Ground(v2_Pos) == l_Ground[i].GetComponent<Isometric_Single>().Get_SingleCode())
            {
                return false;
            }
        }
        return true;
    }

}
