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
    [Header("Fence Map Code")]
    [SerializeField]
    private string s_MapCode_Fence_U = "";

    /// <summary>
    /// Isometric GameObject Code that BETWEEN 2 isometric Grounds Code  with side DOWN
    /// </summary>
    /// <remarks>
    /// Isomtric Object(s) BETWEEN 2 Isomtric Grounds with side DOWN
    /// </remarks>
    [SerializeField]
    private string s_MapCode_Fence_D = "";

    /// <summary>
    /// Isometric GameObject Code that BETWEEN 2 isometric Grounds Code  with side LEFT
    /// </summary>
    /// <remarks>
    /// Isomtric Object(s) BETWEEN 2 Isomtric Grounds with side LEFT
    /// </remarks>
    [SerializeField]
    private string s_MapCode_Fence_L = "";

    /// <summary>
    /// Isometric GameObject Code that BETWEEN 2 isometric Grounds Code  with side RIGHT
    /// </summary>
    /// <remarks>
    /// Isomtric Object(s) BETWEEN 2 Isomtric Grounds with side RIGHT
    /// </remarks>
    [SerializeField]
    private string s_MapCode_Fence_R = "";

    /// <summary>
    /// Isometric Floor Code that Set Height of Ground and Object
    /// </summary>
    [Header("Floor Map Code")]
    [SerializeField]
    private string s_MapCode_Floor;

    /// <summary>
    /// List of Isometric Under-Ground Code that UNDER Ground Code of that Ground's Floor
    /// </summary>
    [Header("Under Map Code")]
    [SerializeField]
    private List<string> l2_MapCode_Under;

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

    /// <summary>
    /// Clear All Map Code Saved in Current
    /// </summary>
    public void Set_MapCode_ClearAll()
    {
        s_MapCode_Ground = "";
        s_MapCode_Object = "";
        s_MapCode_Fence_U = "";
        s_MapCode_Fence_D = "";
        s_MapCode_Fence_L = "";
        s_MapCode_Fence_R = "";
        s_MapCode_Floor = "";
        l2_MapCode_Under = new List<string>();
        l_SpawmPoint = new List<Vector2Int>();
    }

    /// <summary>
    /// Check if All Map Code Invalis to Generating Map
    /// </summary>
    /// <returns></returns>
    public bool Get_MapCode_Invalid()
    {
        int i_MapSize = Get_MapSize().x * Get_MapSize().y;

        if (s_MapCode_Ground.Length != i_MapSize)
        {
            Debug.LogError("Get_MapCode_Invalid: Map String Code not Invalid!");
            return false;
        }
        if (s_MapCode_Object.Length != i_MapSize)
        {
            Debug.LogError("Get_MapCode_Invalid: Map String Code not Invalid!");
            return false;
        }
        if (s_MapCode_Fence_U.Length != i_MapSize)
        {
            Debug.LogError("Get_MapCode_Invalid: Map String Code not Invalid!");
            return false;
        }
        if (s_MapCode_Fence_D.Length != i_MapSize)
        {
            Debug.LogError("Get_MapCode_Invalid: Map String Code not Invalid!");
            return false;
        }
        if (s_MapCode_Fence_L.Length != i_MapSize)
        {
            Debug.LogError("Get_MapCode_Invalid: Map String Code not Invalid!");
            return false;
        }
        if (s_MapCode_Fence_R.Length != i_MapSize)
        {
            Debug.LogError("Get_MapCode_Invalid: Map String Code not Invalid!");
            return false;
        }
        return true;
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
    public void Set_MapCode_Fence_U(string s_MapFenceUpCode)
    {
        this.s_MapCode_Fence_U = s_MapFenceUpCode;
    }

    /// <summary>
    /// Get MAP FENCE UP CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Fence_U()
    {
        return s_MapCode_Fence_U;
    }

    #endregion

    #region Fence DOWN Code Manager

    /// <summary>
    /// Set MAP FENCE DOWN CODE before Generating MAP
    /// </summary>
    /// <param name="s_MapGroundCode"></param>
    public void Set_MapCode_Fence_D(string s_MapFenceDownCode)
    {
        this.s_MapCode_Fence_D = s_MapFenceDownCode;
    }

    /// <summary>
    /// Get MAP FENCE DOWN CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Fence_D()
    {
        return s_MapCode_Fence_D;
    }

    #endregion

    #region Fence LEFT Code Manager

    /// <summary>
    /// Set MAP FENCE LEFT CODE before Generating MAP
    /// </summary>
    /// <param name="s_MapGroundCode"></param>
    public void Set_MapCode_Fence_L(string s_MapFenceLeftCode)
    {
        this.s_MapCode_Fence_L = s_MapFenceLeftCode;
    }

    /// <summary>
    /// Get MAP FENCE LEFT CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Fence_L()
    {
        return s_MapCode_Fence_L;
    }

    #endregion

    #region Fence RIGHT Code Manager

    /// <summary>
    /// Set MAP FENCE RIGHT CODE before Generating MAP
    /// </summary>
    /// <param name="s_MapGroundCode"></param>
    public void Set_MapCode_Fence_R(string s_MapFenceRightCode)
    {
        this.s_MapCode_Fence_R = s_MapFenceRightCode;
    }

    /// <summary>
    /// Get MAP FENCE RIGHT CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Fence_R()
    {
        return s_MapCode_Fence_R;
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

    #region Floor Code Manager

    /// <summary>
    /// Set MAP FLOOR CODE before Generating MAP
    /// </summary>
    /// <param name="s_MapFloorCode"></param>
    public void Set_MapCode_Floor(string s_MapFloorCode)
    {
        this.s_MapCode_Floor = s_MapFloorCode;
    }

    /// <summary>
    /// Get MAP FLOOR CODE
    /// </summary>
    /// <returns></returns>
    public string Get_MapCode_Floor()
    {
        return this.s_MapCode_Floor;
    }

    /// <summary>
    /// Get Floor Code
    /// </summary>
    /// <param name="i_Floor"></param>
    /// <returns></returns>
    public char Get_Floor(int i_Floor)
    {
        if (i_Floor == 0)
            return '0';
        if (i_Floor == 1)
            return '1';
        if (i_Floor == 2)
            return '2';
        if (i_Floor == 3)
            return '3';
        if (i_Floor == 4)
            return '4';
        if (i_Floor == 5)
            return '5';
        if (i_Floor == 6)
            return '6';
        if (i_Floor == 7)
            return '7';
        if (i_Floor == 8)
            return '8';
        if (i_Floor == 9)
            return '9';
        if(i_Floor < 0)
        {
            Debug.LogError("Get_Floor: Out of Floor!");
            return '0';
        }
        if (i_Floor > 9)
        {
            Debug.LogError("Get_Floor: Out of Floor!");
            return '9';
        }
        Debug.LogError("Get_Floor: Out of Floor!");
        return '~';
    }

    /// <summary>
    /// Get Floor Code
    /// </summary>
    /// <param name="c_Floor"></param>
    /// <returns></returns>
    public int Get_Floor(char c_Floor)
    {
        if (c_Floor == '0')
            return 0;
        if (c_Floor == '1')
            return 1;
        if (c_Floor == '2')
            return 2;
        if (c_Floor == '3')
            return 3;
        if (c_Floor == '4')
            return 4;
        if (c_Floor == '5')
            return 5;
        if (c_Floor == '6')
            return 6;
        if (c_Floor == '7')
            return 7;
        if (c_Floor == '8')
            return 8;
        if (c_Floor == '9')
            return 9;
        Debug.LogError("Get_Floor: Out of Floor!");
        return 0;
    }

    #endregion

    #region Under Code Manager

    /// <summary>
    /// Set MAP UNDER CODE before Generating MAP
    /// </summary>
    /// <remarks>
    /// Auto ADD to List when Called
    /// </remarks>
    /// <param name="s_MapUnderCode"></param>
    public void Set_MapCode_Under(string s_MapUnderCode)
    {
        if (l2_MapCode_Under == null)
        {
            l2_MapCode_Under = new List<string>();
        }

        this.l2_MapCode_Under.Add(s_MapUnderCode);
    }

    /// <summary>
    /// Set MAP UNDER CODE before Generating MAP
    /// </summary>
    /// <remarks>
    /// Auto ADD to List when Called if NOT Exist Floor, else CHANCE Exist Floor in List
    /// </remarks>
    /// <param name="i_Floor"></param>
    /// <param name="s_MapUnderCode"></param>
    public void Set_MapCode_Under(int i_Floor, string s_MapUnderCode)
    {
        if (l2_MapCode_Under == null)
        {
            l2_MapCode_Under = new List<string>();
        }

        if (i_Floor < Get_MapCode_Under_Count())
        {
            this.l2_MapCode_Under[i_Floor] = s_MapUnderCode;
        }
        else
        {
            int i_MapCodeUnder_Count = Get_MapCode_Under_Count();

            for (int i = 0; i < i_Floor - i_MapCodeUnder_Count; i++)
            {
                this.l2_MapCode_Under.Add("");
            }

            this.l2_MapCode_Under.Add(s_MapUnderCode);
        }
    }

    /// <summary>
    /// Get Count of MAP UNDER CODE List
    /// </summary>
    /// <returns></returns>
    public int Get_MapCode_Under_Count()
    {
        return this.l2_MapCode_Under.Count;
    }

    /// <summary>
    /// Get MAP UNDER CODE
    /// </summary>
    /// <param name="i_Floor"></param>
    /// <returns></returns>
    public string Get_MapCode_Under(int i_Floor)
    {
        if (i_Floor < l2_MapCode_Under.Count)
        {
            return this.l2_MapCode_Under[i_Floor];
        }
        Debug.LogError("Get_MapCode_Under: Not Exist UNDER CODE of Floor " + i_Floor);
        return "";
    }

    #endregion
}
