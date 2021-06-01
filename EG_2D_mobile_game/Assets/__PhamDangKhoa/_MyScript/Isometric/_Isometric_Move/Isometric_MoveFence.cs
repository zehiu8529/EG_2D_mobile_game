using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Check Fence to Avoid
/// </summary>
[RequireComponent(typeof(Isometric_MoveControl))]
public class Isometric_MoveFence : MonoBehaviour
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

                if (g_MapManager == null)
                {
                    Debug.LogError(this.name + ": Not found 'MapManager GameObject' with tag: " + s_Tag);
                }
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
        if (v2_Dir == cl_MapManager_MapManager.v2_DirUp)
        //If Move Dir Up
        {
            if (
                cl_MapManager_MapManager.Get_MatrixCode_Fence_Down(v2_Pos + v2_Dir) !=
                cl_MapManager_MapRenderer.Get_EmtyCode())
            {
                return true;
            }
        }
        else
        if (v2_Dir == cl_MapManager_MapManager.v2_DirDown)
        //If Move Dir Down
        {
            if (
                cl_MapManager_MapManager.Get_MatrixCode_Fence_Up(v2_Pos + v2_Dir) !=
                cl_MapManager_MapRenderer.Get_EmtyCode())
            {
                return true;
            }
        }
        else
        if (v2_Dir == cl_MapManager_MapManager.v2_DirLeft) 
        //If Move Dir Left
        {
            if(
                cl_MapManager_MapManager.Get_MatrixCode_Fence_Right(v2_Pos +v2_Dir) !=
                cl_MapManager_MapRenderer.Get_EmtyCode())
            {
                return true;
            }
        }
        else
        if(v2_Dir == cl_MapManager_MapManager.v2_DirRight)
        //If Move Dir Right
        {
            if(
                cl_MapManager_MapManager.Get_MatrixCode_Fence_Left(v2_Pos + v2_Dir) !=
                cl_MapManager_MapRenderer.Get_EmtyCode())
            {
                return true;
            }
        }
        return false;
    }
}
