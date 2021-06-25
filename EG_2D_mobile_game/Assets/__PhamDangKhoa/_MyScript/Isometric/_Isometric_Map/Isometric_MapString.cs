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
    private string s_MapCode_Ground = "";

    /// <summary>
    /// Isomtric GameObject Code that ONTOP Isomtric Ground Code
    /// </summary>
    /// <remarks>
    /// Isomtric Object(s) STAND on Isomtric Ground(s)
    /// </remarks>
    [SerializeField]
    private string s_MapCode_Object = "";

    /// <summary>
    /// Isometric GameObject Code that BETWEEN 2 isometric Grounds Code  with side UP
    /// </summary>
    /// <remarks>
    /// Isomtric Object(s) BETWEEN 2 Isomtric Grounds with side UP
    /// </remarks>
    [Header("Special Map Code")]
    [SerializeField]
    private string s_MapCode_Fence_Up = "";

    /// <summary>
    /// Isometric GameObject Code that BETWEEN 2 isometric Grounds Code  with side DOWN
    /// </summary>
    /// <remarks>
    /// Isomtric Object(s) BETWEEN 2 Isomtric Grounds with side DOWN
    /// </remarks>
    [SerializeField]
    private string s_MapCode_Fence_Down = "";

    /// <summary>
    /// Isometric GameObject Code that BETWEEN 2 isometric Grounds Code  with side LEFT
    /// </summary>
    /// <remarks>
    /// Isomtric Object(s) BETWEEN 2 Isomtric Grounds with side LEFT
    /// </remarks>
    [SerializeField]
    private string s_MapCode_Fence_Left = "";

    /// <summary>
    /// Isometric GameObject Code that BETWEEN 2 isometric Grounds Code  with side RIGHT
    /// </summary>
    /// <remarks>
    /// Isomtric Object(s) BETWEEN 2 Isomtric Grounds with side RIGHT
    /// </remarks>
    [SerializeField]
    private string s_MapCode_Fence_Right = "";

    /// <summary>
    /// Isomtric Map X Length Dir UP and DOWN and Y Lenght Dir LEFT and RIGHT
    /// </summary>
    [Header("Map Size for Map Code")]
    [SerializeField]
    private Vector2Int v2_MapCode_Size = new Vector2Int(0, 0);

    /// <summary>
    /// List of Spawm Point(s)
    /// </summary>
    [Header("Spawm Point")]
    [SerializeField]
    private List<Vector2Int> l_SpawmPoint;

    #endregion

    #region Map Size

    /// <summary>
    /// Set Map Size
    /// </summary>
    /// <param name="v2_MapCodeSize"></param>
    public void Set_MapSize(Vector2Int v2_MapCodeSize)
    {
        this.v2_MapCode_Size = v2_MapCodeSize;
    }

    /// <summary>
    /// Set Map Size
    /// </summary>
    /// <param name="v2_MapCodeSize"></param>
    public void Set_MapSize(int i_MapCodeSize_X, int i_MapCodeSize_Y)
    {
        this.v2_MapCode_Size = new Vector2Int(i_MapCodeSize_X, i_MapCodeSize_Y);
    }

    /// <summary>
    /// Get Map Size
    /// </summary>
    /// <returns></returns>
    public Vector2Int Get_MapSize()
    {
        return v2_MapCode_Size;
    }

    #endregion

    #region Map Code

    public void Set_MapCode_ClearAll()
    {
        s_MapCode_Ground = "";
        s_MapCode_Object = "";
        s_MapCode_Fence_Up = "";
        s_MapCode_Fence_Down = "";
        s_MapCode_Fence_Left = "";
        s_MapCode_Fence_Right = "";
        l_SpawmPoint = new List<Vector2Int>();
    }

    #endregion

    #region Ground Code Manager

    /// <summary>
    /// Set MAP GROUND CODE before Generating MAP
    /// </summary>
    /// <param name="s_MapGroundCode"></param>
    public void Set_MapCode_Ground(string s_MapGroundCode)
    {
        this.s_MapCode_Ground = s_MapGroundCode;
    }

    /// <summary>
    /// Get MAP GROUND CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Ground()
    {
        return s_MapCode_Ground;
    }

    #endregion

    #region Object Code Manager

    /// <summary>
    /// Set MAP OBJECT CODE before Generating MAP
    /// </summary>
    /// <param name="s_MapGroundCode"></param>
    public void Set_MapCode_Object(string s_MapObjectCode)
    {
        this.s_MapCode_Object = s_MapObjectCode;
    }

    /// <summary>
    /// Get MAP OBJECT CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Object()
    {
        return s_MapCode_Object;
    }

    #endregion

    #region Fence UP Code Manager

    /// <summary>
    /// Set MAP FENCE UP CODE before Generating MAP
    /// </summary>
    /// <param name="s_MapGroundCode"></param>
    public void Set_MapCode_Fence_Up(string s_MapFenceUpCode)
    {
        this.s_MapCode_Fence_Up = s_MapFenceUpCode;
    }

    /// <summary>
    /// Get MAP FENCE UP CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Fence_Up()
    {
        return s_MapCode_Fence_Up;
    }

    #endregion

    #region Fence DOWN Code Manager

    /// <summary>
    /// Set MAP FENCE DOWN CODE before Generating MAP
    /// </summary>
    /// <param name="s_MapGroundCode"></param>
    public void Set_MapCode_Fence_Down(string s_MapFenceDownCode)
    {
        this.s_MapCode_Fence_Down = s_MapFenceDownCode;
    }

    /// <summary>
    /// Get MAP FENCE DOWN CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Fence_Down()
    {
        return s_MapCode_Fence_Down;
    }

    #endregion

    #region Fence LEFT Code Manager

    /// <summary>
    /// Set MAP FENCE LEFT CODE before Generating MAP
    /// </summary>
    /// <param name="s_MapGroundCode"></param>
    public void Set_MapCode_Fence_Left(string s_MapFenceLeftCode)
    {
        this.s_MapCode_Fence_Left = s_MapFenceLeftCode;
    }

    /// <summary>
    /// Get MAP FENCE LEFT CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Fence_Left()
    {
        return s_MapCode_Fence_Left;
    }

    #endregion

    #region Fence RIGHT Code Manager

    /// <summary>
    /// Set MAP FENCE RIGHT CODE before Generating MAP
    /// </summary>
    /// <param name="s_MapGroundCode"></param>
    public void Set_MapCode_Fence_Right(string s_MapFenceRightCode)
    {
        this.s_MapCode_Fence_Right = s_MapFenceRightCode;
    }

    /// <summary>
    /// Get MAP FENCE RIGHT CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Fence_Right()
    {
        return s_MapCode_Fence_Right;
    }

    #endregion

    #region Spawm Point

    /// <summary>
    /// List of Spawm Point(s)
    /// </summary>
    /// <returns></returns>
    public List<Vector2Int> Get_List_SpawmPoint()
    {
        return l_SpawmPoint;
    }

    /// <summary>
    /// Add new Pos Spawm Point to List
    /// </summary>
    /// <param name="v2_Pos"></param>
    public void Set_List_SpawmPoint_Add(Vector2Int v2_Pos)
    {
        l_SpawmPoint.Add(v2_Pos);
    }

    /// <summary>
    /// Remove Spawm Point at Index
    /// </summary>
    /// <param name="i_IndexRemove"></param>
    public void Set_List_SpawmPoint_Remove(int i_IndexRemove)
    {
        l_SpawmPoint.RemoveAt(i_IndexRemove);
    }
    
    /// <summary>
    /// Remove all List of Spawm Point
    /// </summary>
    public void Set_List_SpawmPoint_Reset()
    {
        l_SpawmPoint = new List<Vector2Int>();
    }

    #endregion
}
