using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Check Fence to Avoid
/// </summary>
[RequireComponent(typeof(Isometric_MoveControl))]
public class Isometric_CheckFence : MonoBehaviour
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
    /// Map Manager of Matrix Map Code and Matrix Map Isometric GameObject
    /// </summary>
    private Isometric_MapManager cl_MapManager_MapManager;

    /// <summary>
    /// Save Single Code and Single Isometric GameObject of Square on Matrix Map
    /// </summary>
    private Isometric_MapRenderer cl_MapManager_MapRenderer;

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

        cl_MapManager_MapRenderer = g_MapManager.GetComponent<Isometric_MapRenderer>();
    }

    /// <summary>
    /// Check Fence Ahead
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="v2_Dir"></param>
    /// <returns>If FALSE >> No FENCE EXIST</returns>
    public bool Get_Check_Fence_Ahead(Vector2Int v2_Pos, Vector2Int v2_Dir)
    {
        if (v2_Dir == new Class_Vector().v2_Isometric_DirUp)
        //If Move Dir Up
        {
            if (
                cl_MapManager_MapManager.Get_MatrixCode_Fence_D(v2_Pos + v2_Dir) !=
                cl_MapManager_MapRenderer.Get_EmtyCode())
            {
                return true;
            }
        }
        else
        if (v2_Dir == new Class_Vector().v2_Isometric_DirDown)
        //If Move Dir Down
        {
            if (
                cl_MapManager_MapManager.Get_MatrixCode_Fence_U(v2_Pos + v2_Dir) !=
                cl_MapManager_MapRenderer.Get_EmtyCode())
            {
                return true;
            }
        }
        else
        if (v2_Dir == new Class_Vector().v2_Isometric_DirLeft)
        //If Move Dir Left
        {
            if (
                cl_MapManager_MapManager.Get_MatrixCode_Fence_R(v2_Pos + v2_Dir) !=
                cl_MapManager_MapRenderer.Get_EmtyCode())
            {
                return true;
            }
        }
        else
        if (v2_Dir == new Class_Vector().v2_Isometric_DirRight) 
        //If Move Dir Right
        {
            if(
                cl_MapManager_MapManager.Get_MatrixCode_Fence_L(v2_Pos + v2_Dir) !=
                cl_MapManager_MapRenderer.Get_EmtyCode())
            {
                return true;
            }
        }
        return false;
    }
}
