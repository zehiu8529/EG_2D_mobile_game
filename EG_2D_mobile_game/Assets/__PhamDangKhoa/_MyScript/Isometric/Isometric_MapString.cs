using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* - ISOMETRIC SQUARE:
 * 
 *                  ..
 *    UP(-1;+0)   ......   RIGHT(+0;+1)
 *              ..........
 *            .............. --> SQUARE(0;0)
 *              ..........
 *  LEFT(+0,-1)   ......   DOWN(+1;+0)
 *                  ..
 *  
 */

/// <summary>
/// Save Map Code by Single String for Matrix Map Code
/// </summary>
public class Isometric_MapString : MonoBehaviour
{
    #region Public Varible

    /// <summary>
    /// Isometric GameObject Code that UNDER Isometric Object Code
    /// </summary>
    /// <remarks>
    /// Isomtric Object(s) STAND on them
    /// </remarks>
    [Header("Primary Map Code")]
    [SerializeField]
    private string s_Map_Ground = "";

    /// <summary>
    /// Isomtric GameObject Code that ONTOP Isomtric Ground Code
    /// </summary>
    /// <remarks>
    /// Isomtric Object(s) STAND on Isomtric Ground(s)
    /// </remarks>
    [SerializeField]
    private string s_Map_Object = "";

    /// <summary>
    /// Isometric GameObject Code that BETWEEN 2 isometric Grounds Code  with side UP
    /// </summary>
    /// <remarks>
    /// Isomtric Object(s) BETWEEN 2 Isomtric Grounds with side UP
    /// </remarks>
    [Header("Special Map Code")]
    [SerializeField]
    private string s_Map_Fence_Up = "";

    /// <summary>
    /// Isometric GameObject Code that BETWEEN 2 isometric Grounds Code  with side DOWN
    /// </summary>
    /// <remarks>
    /// Isomtric Object(s) BETWEEN 2 Isomtric Grounds with side DOWN
    /// </remarks>
    [SerializeField]
    private string s_Map_Fence_Down = "";

    /// <summary>
    /// Isometric GameObject Code that BETWEEN 2 isometric Grounds Code  with side LEFT
    /// </summary>
    /// <remarks>
    /// Isomtric Object(s) BETWEEN 2 Isomtric Grounds with side LEFT
    /// </remarks>
    [SerializeField]
    private string s_Map_Fence_Left = "";

    /// <summary>
    /// Isometric GameObject Code that BETWEEN 2 isometric Grounds Code  with side RIGHT
    /// </summary>
    /// <remarks>
    /// Isomtric Object(s) BETWEEN 2 Isomtric Grounds with side RIGHT
    /// </remarks>
    [SerializeField]
    private string s_Map_Fence_Right = "";

    #endregion

    #region Ground Code Manager

    /// <summary>
    /// Set MAP GROUND CODE before Generating MAP
    /// </summary>
    /// <param name="s_MapGroundCode"></param>
    public void Set_MapCode_Ground(string s_MapGroundCode)
    {
        this.s_Map_Ground = s_MapGroundCode;
    }

    /// <summary>
    /// Get MAP GROUND CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Ground()
    {
        return s_Map_Ground;
    }

    #endregion

    #region Object Code Manager

    /// <summary>
    /// Set MAP OBJECT CODE before Generating MAP
    /// </summary>
    /// <param name="s_MapGroundCode"></param>
    public void Set_MapCode_Object(string s_MapObjectCode)
    {
        this.s_Map_Object = s_MapObjectCode;
    }

    /// <summary>
    /// Get MAP OBJECT CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Object()
    {
        return s_Map_Object;
    }

    #endregion

    #region Fence UP Code Manager

    /// <summary>
    /// Set MAP FENCE UP CODE before Generating MAP
    /// </summary>
    /// <param name="s_MapGroundCode"></param>
    public void Set_MapCode_Fence_Up(string s_MapFenceUpCode)
    {
        this.s_Map_Fence_Up = s_MapFenceUpCode;
    }

    /// <summary>
    /// Get MAP FENCE UP CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Fence_Up()
    {
        return s_Map_Fence_Up;
    }

    #endregion

    #region Fence DOWN Code Manager

    /// <summary>
    /// Set MAP FENCE DOWN CODE before Generating MAP
    /// </summary>
    /// <param name="s_MapGroundCode"></param>
    public void Set_MapCode_Fence_Down(string s_MapFenceDownCode)
    {
        this.s_Map_Fence_Down = s_MapFenceDownCode;
    }

    /// <summary>
    /// Get MAP FENCE DOWN CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Fence_Down()
    {
        return s_Map_Fence_Down;
    }

    #endregion

    #region Fence LEFT Code Manager

    /// <summary>
    /// Set MAP FENCE LEFT CODE before Generating MAP
    /// </summary>
    /// <param name="s_MapGroundCode"></param>
    public void Set_MapCode_Fence_Left(string s_MapFenceLeftCode)
    {
        this.s_Map_Fence_Left = s_MapFenceLeftCode;
    }

    /// <summary>
    /// Get MAP FENCE LEFT CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Fence_Left()
    {
        return s_Map_Fence_Left;
    }

    #endregion

    #region Fence RIGHT Code Manager

    /// <summary>
    /// Set MAP FENCE RIGHT CODE before Generating MAP
    /// </summary>
    /// <param name="s_MapGroundCode"></param>
    public void Set_MapCode_Fence_Right(string s_MapFenceRightCode)
    {
        this.s_Map_Fence_Right = s_MapFenceRightCode;
    }

    /// <summary>
    /// Get MAP FENCE RIGHT CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Fence_Right()
    {
        return s_Map_Fence_Right;
    }

    #endregion
}
