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
/// Map Manager of Matrix Map Code and Matrix Map Isometric GameObject
/// </summary>
[RequireComponent(typeof(Isometric_MapString))]
[RequireComponent(typeof(Isometric_MapRenderer))]
public class Isometric_MapManager : MonoBehaviour
{
    #region Public Varible

    /// <summary>
    /// Tag for other Isometric Object to Find
    /// </summary>
    [Header("Isometric Map-Manager")]
    [SerializeField]
    private string s_Tag = "IsometricMap";

    /// <summary>
    /// Pos Offset for Map (With Offset(0,0), Start in Isometric Square(0,0))
    /// </summary>
    [Header("Isometric On-Map")]
    [SerializeField]
    private Vector2 v2_Offset = new Vector2();

    [SerializeField]
    private float f_ConstDepth = 9;

    #endregion

    #region Private Varible

    #region Component

    /// <summary>
    /// Save Map Code by Single String for Matrix Map Code
    /// </summary>
    private Isometric_MapString cl_MapString;

    /// <summary>
    /// Save Single Code and Single Isometric GameObject of Square on Matrix Map
    /// </summary>
    private Isometric_MapRenderer cl_MapRenderer;

    /// <summary>
    /// Working on GameObject and Prepab
    /// </summary>
    private Class_Object cl_Object;

    #endregion

    #region List Code

    #region Primary List Code

    /// <summary>
    /// Matrix of GROUND Code
    /// </summary>
    private List<List<char>> l2_Map_GroundCode;

    /// <summary>
    /// Matrix of OBJECT Code
    /// </summary>
    private List<List<char>> l2_Map_ObjectCode;

    #endregion

    #region FENCCE List Code

    /// <summary>
    /// Matrix of FENCE UP Code
    /// </summary>
    private List<List<char>> l2_Map_FenceUCode;

    /// <summary>
    /// Matrix of FENCE DOWN Code
    /// </summary>
    private List<List<char>> l2_Map_FenceDCode;

    /// <summary>
    /// Matrix of FENCE LEFT Code
    /// </summary>
    private List<List<char>> l2_Map_FenceLCode;

    /// <summary>
    /// Matrix of FENCE RIGHT Code
    /// </summary>
    private List<List<char>> l2_Map_FenceRCode;

    #endregion

    #region Floor List Code

    /// <summary>
    /// Matrix of FLOOR code
    /// </summary>
    private List<List<char>> l2_Map_FloorCode;

    #endregion

    #endregion

    #region List GameObject

    #region Primary List Code

    /// <summary>
    /// Matrix of GROUND
    /// </summary>
    private List<List<GameObject>> l2_Map_Ground;

    /// <summary>
    /// Matrix of OBJECT
    /// </summary>
    private List<List<GameObject>> l2_Map_Object;

    #endregion

    #region FENCCE List Code

    /// <summary>
    /// Matrix of FENCE UP
    /// </summary>
    private List<List<GameObject>> l2_Map_FenceU;

    /// <summary>
    /// Matrix of FENCE DOWN
    /// </summary>
    private List<List<GameObject>> l2_Map_FenceD;

    /// <summary>
    /// Matrix of FENCE LEFT
    /// </summary>
    private List<List<GameObject>> l2_Map_FenceL;

    /// <summary>
    /// Matrix of FENCE RIGHT
    /// </summary>
    private List<List<GameObject>> l2_Map_FenceR;

    #endregion

    #endregion

    #endregion

    private void Awake()
    {
        if (s_Tag != "")
        {
            this.tag = s_Tag;
        }
    }

    private void Start()
    {
        cl_MapString = GetComponent<Isometric_MapString>();

        cl_MapRenderer = GetComponent<Isometric_MapRenderer>();

        cl_Object = new Class_Object();
    }

    #region Map Manager

    #region Public

    /// <summary>
    /// Start Map Working
    /// </summary>
    public void Set_Map_Generate(bool b_NewMapCode)
    {
        if (b_NewMapCode)
        {
            Set_Map_NewMapCode();
        }
        else
        {
            if (!cl_MapString.Get_MapCode_Invalid())
            //If Map String Check NOT Invalid >> Not Generating Map
            {
                return;
            }
        }

        Set_Map_Auto_Generate();
        Set_Map_Auto_FromCurrentMapCode();
        Set_Map_Auto_Offset();

        Debug.Log("Set_Map_Generate: Done!");
    }

    /// <summary>
    /// Stop Map Working and Remove All Map
    /// </summary>
    public void Set_Map_Remove()
    {
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                Set_GameObject_Remove(l2_Map_Ground[i][j]);
                Set_GameObject_Remove(l2_Map_Object[i][j]);
                Set_GameObject_Remove(l2_Map_FenceU[i][j]);
                Set_GameObject_Remove(l2_Map_FenceD[i][j]);
                Set_GameObject_Remove(l2_Map_FenceL[i][j]);
                Set_GameObject_Remove(l2_Map_FenceR[i][j]);
            }
        }
    }

    /// <summary>
    /// Set MAP CODE of GROUND, OBJECT and FENCE from MAP CODE MATRIX to Get
    /// </summary>
    public void Set_MapCode_FromMapCodeMatrix()
    {
        //Primary
        Set_MatrixCode_ToMapCodeGround();
        Set_MatrixCode_ToMapCodeObject();

        //Fence
        Set_MatrixCode_ToMapCodeFence_U();
        Set_MatrixCode_ToMapCodeFence_D();
        Set_MatrixCode_ToMapCodeFence_L();
        Set_MatrixCode_ToMapCodeFence_R();

        //Floor
        Set_MatrixCode_ToMapCodeFloor();
    }

    /// <summary>
    /// Get Offset
    /// </summary>
    /// <returns></returns>
    public Vector2 Get_Offset()
    {
        return this.v2_Offset;
    }

    #endregion

    #region Private (Working on Step)

    /// <summary>
    /// Generating MAP before Set Matrix Map Code from Map Code or Remove Map
    /// </summary>
    private void Set_Map_Auto_Generate()
    {
        {
            //Primary Code
            l2_Map_GroundCode = new List<List<char>>();
            l2_Map_ObjectCode = new List<List<char>>();

            //Fence Code
            l2_Map_FenceUCode = new List<List<char>>();
            l2_Map_FenceDCode = new List<List<char>>();
            l2_Map_FenceLCode = new List<List<char>>();
            l2_Map_FenceRCode = new List<List<char>>();

            //Floor Code
            l2_Map_FloorCode = new List<List<char>>();
        }

        {
            //Primary
            l2_Map_Ground = new List<List<GameObject>>();
            l2_Map_Object = new List<List<GameObject>>();

            //Fence
            l2_Map_FenceU = new List<List<GameObject>>();
            l2_Map_FenceD = new List<List<GameObject>>();
            l2_Map_FenceL = new List<List<GameObject>>();
            l2_Map_FenceR = new List<List<GameObject>>();
        }

        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            {
                //Primay Code
                l2_Map_GroundCode.Add(new List<char>());
                l2_Map_ObjectCode.Add(new List<char>());

                //Fence Code
                l2_Map_FenceUCode.Add(new List<char>());
                l2_Map_FenceDCode.Add(new List<char>());
                l2_Map_FenceLCode.Add(new List<char>());
                l2_Map_FenceRCode.Add(new List<char>());

                //Floor
                l2_Map_FloorCode.Add(new List<char>());
            }

            {
                //Primary
                l2_Map_Ground.Add(new List<GameObject>());
                l2_Map_Object.Add(new List<GameObject>());

                //Fence
                l2_Map_FenceU.Add(new List<GameObject>());
                l2_Map_FenceD.Add(new List<GameObject>());
                l2_Map_FenceL.Add(new List<GameObject>());
                l2_Map_FenceR.Add(new List<GameObject>());
            }

            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                {
                    //Primary Code
                    l2_Map_GroundCode[i].Add(cl_MapRenderer.Get_EmtyCode());
                    l2_Map_ObjectCode[i].Add(cl_MapRenderer.Get_EmtyCode());

                    //Fence Codde
                    l2_Map_FenceUCode[i].Add(cl_MapRenderer.Get_EmtyCode());
                    l2_Map_FenceDCode[i].Add(cl_MapRenderer.Get_EmtyCode());
                    l2_Map_FenceLCode[i].Add(cl_MapRenderer.Get_EmtyCode());
                    l2_Map_FenceRCode[i].Add(cl_MapRenderer.Get_EmtyCode());

                    //Floor
                    l2_Map_FloorCode[i].Add(cl_MapString.Get_Floor(0));
                }

                {
                    //Primary
                    l2_Map_Ground[i].Add(null);
                    l2_Map_Object[i].Add(null);

                    //Fence
                    l2_Map_FenceU[i].Add(null);
                    l2_Map_FenceD[i].Add(null);
                    l2_Map_FenceL[i].Add(null);
                    l2_Map_FenceR[i].Add(null);
                }
            }
        }
    }

    /// <summary>
    /// Set MATRIX MAP CODE Generating from GROUND, OBJECT and FENCE CODE from Map Code and Map Size Exist in current
    /// </summary>
    private void Set_Map_Auto_FromCurrentMapCode()
    {
        //Primary
        Set_MatrixCode_FromMapCodeGround();
        Set_MatrixCode_FromMapCodeObject();

        //Fence
        Set_MatrixCode_FromMapCodeFence_U();
        Set_MatrixCode_FromMapCodeFence_D();
        Set_MatrixCode_FromMapCodeFence_L();
        Set_MatrixCode_FromMapCodeFence_R();

        //Floor
        Set_MatrixCode_FromMapCodeFloor();
    }

    /// <summary>
    /// Set Auto Offset of Map
    /// </summary>
    private void Set_Map_Auto_Offset()
    {
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                if (Get_MatrixCode_Ground(new Vector2Int(i, j)) != cl_MapRenderer.Get_EmtyCode())
                {
                    Get_GameObject_Ground(new Vector2Int(i, j)).GetComponent<Isometric_Single>().Set_Isometric_Offset(this.v2_Offset);
                }
                if (Get_MatrixCode_Object(new Vector2Int(i, j)) != cl_MapRenderer.Get_EmtyCode())
                {
                    Get_GameObject_Object(new Vector2Int(i, j)).GetComponent<Isometric_Single>().Set_Isometric_Offset(this.v2_Offset);
                }
                if (Get_MatrixCode_Fence_U(new Vector2Int(i, j)) != cl_MapRenderer.Get_EmtyCode())
                {
                    Get_GameObject_Fence_U(new Vector2Int(i, j)).GetComponent<Isometric_Single>().Set_Isometric_Offset(this.v2_Offset);
                }
                if (Get_MatrixCode_Fence_D(new Vector2Int(i, j)) != cl_MapRenderer.Get_EmtyCode())
                {
                    Get_GameObject_Fence_D(new Vector2Int(i, j)).GetComponent<Isometric_Single>().Set_Isometric_Offset(this.v2_Offset);
                }
                if (Get_MatrixCode_Fence_L(new Vector2Int(i, j)) != cl_MapRenderer.Get_EmtyCode())
                {
                    Get_GameObject_Fence_L(new Vector2Int(i, j)).GetComponent<Isometric_Single>().Set_Isometric_Offset(this.v2_Offset);
                }
                if (Get_MatrixCode_Fence_R(new Vector2Int(i, j)) != cl_MapRenderer.Get_EmtyCode())
                {
                    Get_GameObject_Fence_R(new Vector2Int(i, j)).GetComponent<Isometric_Single>().Set_Isometric_Offset(this.v2_Offset);
                }
            }
        }
    }

    #endregion

    /// <summary>
    /// Set Map Code to 'Isometric_MapString.cs'
    /// </summary>
    private void Set_Map_NewMapCode()
    {
        //Primary Code
        string s_MapGroundCode = "";
        string s_MapObjectCode = "";

        //Fence Code
        string s_MapFenceUCode = "";
        string s_MapFenceDCode = "";
        string s_MapFenceLCode = "";
        string s_MapFenceRCode = "";

        //Floor Code
        string s_MapFloorCode = "";

        for (int i = 0; i < cl_MapString.Get_MapSize().x * cl_MapString.Get_MapSize().y; i++)
        {
            //Primary Code
            s_MapGroundCode += cl_MapRenderer.Get_SingleCode_Ground(0);
            s_MapObjectCode += cl_MapRenderer.Get_EmtyCode();

            //Fence Code
            s_MapFenceUCode += cl_MapRenderer.Get_EmtyCode();
            s_MapFenceDCode += cl_MapRenderer.Get_EmtyCode();
            s_MapFenceLCode += cl_MapRenderer.Get_EmtyCode();
            s_MapFenceRCode += cl_MapRenderer.Get_EmtyCode();

            //Floor Code
            s_MapFloorCode += cl_MapString.Get_Floor(0);
        }

        //Primary
        cl_MapString.Set_MapCode_Ground(s_MapGroundCode);
        cl_MapString.Set_MapCode_Object(s_MapObjectCode);

        //Fence
        cl_MapString.Set_MapCode_Fence_U(s_MapFenceUCode);
        cl_MapString.Set_MapCode_Fence_D(s_MapFenceDCode);
        cl_MapString.Set_MapCode_Fence_L(s_MapFenceLCode);
        cl_MapString.Set_MapCode_Fence_R(s_MapFenceRCode);

        //Floor
        cl_MapString.Set_MapCode_Floor(s_MapFloorCode);
    }

    #endregion

    #region Code

    #region GROUND Code Manager

    //Public (Map Matrix)

    /// <summary>
    /// Set Code to a SQUARE of GROUND Map Code
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <param name="c_GroundCode"></param>
    public void Set_MatrixCode_Ground(Vector2Int v2_Pos, char c_GroundCode, char c_FloorCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (l2_Map_GroundCode[v2_Pos.x][v2_Pos.y] == c_GroundCode)
            return;

        Set_GameObject_Remove(l2_Map_Ground[v2_Pos.x][v2_Pos.y]);

        l2_Map_GroundCode[v2_Pos.x][v2_Pos.y] = c_GroundCode;

        GameObject g_Prefab = cl_MapRenderer.Get_GameObject_Ground(c_GroundCode);
        
        l2_Map_Ground[v2_Pos.x][v2_Pos.y] = Set_GameObject_Create(v2_Pos, g_Prefab, c_FloorCode, false);

        //Set_Floor_Ground(v2_Pos);
    }

    /// <summary>
    /// Get Code from a SQUARE of GROUND Map Code
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns></returns>
    public char Get_MatrixCode_Ground(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return cl_MapRenderer.Get_EmtyCode();

        return l2_Map_GroundCode[v2_Pos.x][v2_Pos.y];
    }

    /// <summary>
    /// Get Isometric GameObject on GROUND Map
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns></returns>
    public GameObject Get_GameObject_Ground(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return null;

        return l2_Map_Ground[v2_Pos.x][v2_Pos.y];
    }

    //Private

    /// <summary>
    /// Get MAP CODE of GROUND
    /// </summary>
    private void Set_MatrixCode_ToMapCodeGround()
    {
        string s_Map_Ground = "";
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                s_Map_Ground += l2_Map_GroundCode[i][j];
            }
        }
        cl_MapString.Set_MapCode_Ground(s_Map_Ground);
    }

    /// <summary>
    /// Set MAP GROUND CODE from GROUND CODE
    /// </summary>
    private void Set_MatrixCode_FromMapCodeGround()
    {
        int i_CodeIndex = -1;
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                i_CodeIndex++;

                Set_MatrixCode_Ground(
                    new Vector2Int(i, j), 
                    cl_MapString.Get_MapCode_Ground()[i_CodeIndex],
                    cl_MapString.Get_MapCode_Floor()[i_CodeIndex]);
            }
        }
    }

    #endregion

    #region OBJECT Code Manager

    //Public (Map Matrix)

    /// <summary>
    /// Set Code to a SQUARE of OBJECT Map Code
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <param name="c_ObjectCode"></param>
    public void Set_MatrixCode_Object(Vector2Int v2_Pos, char c_ObjectCode, char c_FloorCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (l2_Map_ObjectCode[v2_Pos.x][v2_Pos.y] == c_ObjectCode)
            return;

        Set_GameObject_Remove(l2_Map_Object[v2_Pos.x][v2_Pos.y]);

        l2_Map_ObjectCode[v2_Pos.x][v2_Pos.y] = c_ObjectCode;

        GameObject g_Prefab = cl_MapRenderer.Get_GameObject_Object(c_ObjectCode);
        
        l2_Map_Object[v2_Pos.x][v2_Pos.y] = Set_GameObject_Create(v2_Pos, g_Prefab, c_FloorCode, true);

        //Set_Floor_Object(v2_Pos);
    }

    /// <summary>
    /// Get Code from a SQUARE of OBJECT Map Code
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns></returns>
    public char Get_MatrixCode_Object(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return cl_MapRenderer.Get_EmtyCode();

        return l2_Map_ObjectCode[v2_Pos.x][v2_Pos.y];
    }

    /// <summary>
    /// Get Isometric GameObject on OBJECT Map
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns></returns>
    public GameObject Get_GameObject_Object(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return null;

        return l2_Map_Object[v2_Pos.x][v2_Pos.y];
    }

    //Private

    /// <summary>
    /// Get MAP CODE of OBJECT
    /// </summary>
    private void Set_MatrixCode_ToMapCodeObject()
    {
        string s_Map_Object = "";
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                s_Map_Object += l2_Map_ObjectCode[i][j];
            }
        }
        cl_MapString.Set_MapCode_Object(s_Map_Object);
    }

    /// <summary>
    /// Set MAP OBJECT CODE from OBJECT CODE
    /// </summary>
    private void Set_MatrixCode_FromMapCodeObject()
    {
        int i_CodeIndex = -1;
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                i_CodeIndex++;

                Set_MatrixCode_Object(
                    new Vector2Int(i, j), 
                    cl_MapString.Get_MapCode_Object()[i_CodeIndex],
                    cl_MapString.Get_MapCode_Floor()[i_CodeIndex]);
            }
        }
    }

    #endregion

    #region FENCCE UP Code Manager

    //Public (Map Matrix)

    /// <summary>
    /// Set Code to a SQUARE of FENCE UP Map Code
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <param name="c_FenceUpCode"></param>
    public void Set_MatrixCode_Fence_U(Vector2Int v2_Pos, char c_FenceUpCode, char c_FloorCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (l2_Map_FenceUCode[v2_Pos.x][v2_Pos.y] == c_FenceUpCode)
            return;

        Set_GameObject_Remove(l2_Map_FenceU[v2_Pos.x][v2_Pos.y]);

        l2_Map_FenceUCode[v2_Pos.x][v2_Pos.y] = c_FenceUpCode;

        GameObject g_Prefab = cl_MapRenderer.Get_GameObject_Fence_U(c_FenceUpCode);
        
        l2_Map_FenceU[v2_Pos.x][v2_Pos.y] = Set_GameObject_Create(v2_Pos, g_Prefab, c_FloorCode, true);

        //Set_Floor_Fence_U(v2_Pos);
    }

    /// <summary>
    /// Get Code from a SQUARE of FENCE UP Map Code
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns></returns>
    public char Get_MatrixCode_Fence_U(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return cl_MapRenderer.Get_EmtyCode();

        return l2_Map_FenceUCode[v2_Pos.x][v2_Pos.y];
    }

    /// <summary>
    /// Get Isometric GameObject on FENCE UP Map
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns></returns>
    public GameObject Get_GameObject_Fence_U(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return null;

        return l2_Map_FenceU[v2_Pos.x][v2_Pos.y];
    }

    //Private

    /// <summary>
    /// Get MAP CODE of FENCE UP
    /// </summary>
    private void Set_MatrixCode_ToMapCodeFence_U()
    {
        string s_Map_Fence_Up = "";
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                s_Map_Fence_Up += l2_Map_FenceUCode[i][j];
            }
        }
        cl_MapString.Set_MapCode_Fence_U(s_Map_Fence_Up);
    }

    /// <summary>
    /// Set MAP FENCE UP CODE from FENCE UP CODE
    /// </summary>
    private void Set_MatrixCode_FromMapCodeFence_U()
    {
        int i_CodeIndex = -1;
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                i_CodeIndex++;

                Set_MatrixCode_Fence_U(
                    new Vector2Int(i, j), 
                    cl_MapString.Get_MapCode_Fence_U()[i_CodeIndex],
                    cl_MapString.Get_MapCode_Floor()[i_CodeIndex]);
            }
        }
    }

    #endregion

    #region FENCCE DOWN Code Manager

    //Public (Map Matrix)

    /// <summary>
    /// Set Code to a SQUARE of FENCE DOWN Map Code
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <param name="c_FenceDownCode"></param>
    public void Set_MatrixCode_Fence_D(Vector2Int v2_Pos, char c_FenceDownCode, char c_FloorCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (l2_Map_FenceDCode[v2_Pos.x][v2_Pos.y] == c_FenceDownCode)
            return;

        Set_GameObject_Remove(l2_Map_FenceD[v2_Pos.x][v2_Pos.y]);

        l2_Map_FenceDCode[v2_Pos.x][v2_Pos.y] = c_FenceDownCode;

        GameObject g_Prefab = cl_MapRenderer.Get_GameObject_Fence_D(c_FenceDownCode);
        
        l2_Map_FenceD[v2_Pos.x][v2_Pos.y] = Set_GameObject_Create(v2_Pos, g_Prefab, c_FloorCode, true);

        //Set_Floor_Fence_D(v2_Pos);
    }

    /// <summary>
    /// Get Code from a SQUARE of FENCE DOWN Map Code
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns></returns>
    public char Get_MatrixCode_Fence_D(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return cl_MapRenderer.Get_EmtyCode();

        return l2_Map_FenceDCode[v2_Pos.x][v2_Pos.y];
    }

    /// <summary>
    /// Get Isometric GameObject on FENCE UP Map
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns></returns>
    public GameObject Get_GameObject_Fence_D(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return null;

        return l2_Map_FenceD[v2_Pos.x][v2_Pos.y];
    }

    //Private

    /// <summary>
    /// Get MAP CODE of FENCE DOWN
    /// </summary>
    private void Set_MatrixCode_ToMapCodeFence_D()
    {
        string s_Map_Fence_Down = "";
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                s_Map_Fence_Down += l2_Map_FenceDCode[i][j];
            }
        }
        cl_MapString.Set_MapCode_Fence_D(s_Map_Fence_Down);
    }

    /// <summary>
    /// Set MAP FENCE DOWN CODE from FENCE DOWN CODE
    /// </summary>
    private void Set_MatrixCode_FromMapCodeFence_D()
    {
        int i_CodeIndex = -1;
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                i_CodeIndex++;

                Set_MatrixCode_Fence_D(
                    new Vector2Int(i, j), 
                    cl_MapString.Get_MapCode_Fence_D()[i_CodeIndex],
                    cl_MapString.Get_MapCode_Floor()[i_CodeIndex]);
            }
        }
    }

    #endregion

    #region FENCCE LEFT Code Manager

    //Public (Map Matrix)

    /// <summary>
    /// Set Code to a SQUARE of FENCE LEFT Map Code
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <param name="c_FenceLeftCode"></param>
    public void Set_MatrixCode_Fence_L(Vector2Int v2_Pos, char c_FenceLeftCode, char c_FloorCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (l2_Map_FenceLCode[v2_Pos.x][v2_Pos.y] == c_FenceLeftCode)
            return;

        Set_GameObject_Remove(l2_Map_FenceL[v2_Pos.x][v2_Pos.y]);

        l2_Map_FenceLCode[v2_Pos.x][v2_Pos.y] = c_FenceLeftCode;

        GameObject g_Prefab = cl_MapRenderer.Get_GameObject_Fence_L(c_FenceLeftCode);
        
        l2_Map_FenceL[v2_Pos.x][v2_Pos.y] = Set_GameObject_Create(v2_Pos, g_Prefab, c_FloorCode, true);

        //Set_Floor_Fence_L(v2_Pos);
    }

    /// <summary>
    /// Get Code from a SQUARE of FENCE LEFT Map Code
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns></returns>
    public char Get_MatrixCode_Fence_L(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return cl_MapRenderer.Get_EmtyCode();

        return l2_Map_FenceLCode[v2_Pos.x][v2_Pos.y];
    }

    /// <summary>
    /// Get Isometric GameObject on FENCE LEFT Map
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns></returns>
    public GameObject Get_GameObject_Fence_L(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return null;

        return l2_Map_FenceL[v2_Pos.x][v2_Pos.y];
    }

    //Private

    /// <summary>
    /// Get MAP CODE of FENCE LEFT
    /// </summary>
    private void Set_MatrixCode_ToMapCodeFence_L()
    {
        string s_Map_Fence_Left = "";
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                s_Map_Fence_Left += l2_Map_FenceLCode[i][j];
            }
        }
        cl_MapString.Set_MapCode_Fence_L(s_Map_Fence_Left);
    }

    /// <summary>
    /// Set MAP FENCE LEFT CODE from FENCE LEFT CODE
    /// </summary>
    private void Set_MatrixCode_FromMapCodeFence_L()
    {
        int i_CodeIndex = -1;
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                i_CodeIndex++;

                Set_MatrixCode_Fence_L(
                    new Vector2Int(i, j), 
                    cl_MapString.Get_MapCode_Fence_L()[i_CodeIndex],
                    cl_MapString.Get_MapCode_Floor()[i_CodeIndex]);
            }
        }
    }

    #endregion

    #region FENCCE RIGHT Code Manager

    //Public (Map Matrix)

    /// <summary>
    /// Set Code to a SQUARE of FENCE RIGHT Map Code
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <param name="c_FenceRightCode"></param>
    public void Set_MatrixCode_Fence_R(Vector2Int v2_Pos, char c_FenceRightCode, char c_FloorCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (l2_Map_FenceRCode[v2_Pos.x][v2_Pos.y] == c_FenceRightCode)
            return;

        Set_GameObject_Remove(l2_Map_FenceR[v2_Pos.x][v2_Pos.y]);

        l2_Map_FenceRCode[v2_Pos.x][v2_Pos.y] = c_FenceRightCode;

        GameObject g_Prefab = cl_MapRenderer.Get_GameObject_Fence_R(c_FenceRightCode);
        
        l2_Map_FenceR[v2_Pos.x][v2_Pos.y] = Set_GameObject_Create(v2_Pos, g_Prefab, c_FloorCode, true);

        //Set_Floor_Fence_R(v2_Pos);
    }

    /// <summary>
    /// Get Code from a SQUARE of FENCE RIGHT Map Code
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns></returns>
    public char Get_MatrixCode_Fence_R(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return cl_MapRenderer.Get_EmtyCode();

        return l2_Map_FenceRCode[v2_Pos.x][v2_Pos.y];
    }

    /// <summary>
    /// Get Isometric GameObject on FENCE RIGHT Map
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns></returns>
    public GameObject Get_GameObject_Fence_R(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return null;

        return l2_Map_FenceR[v2_Pos.x][v2_Pos.y];
    }

    //Private

    /// <summary>
    /// Get MAP CODE of FENCE RIGHT
    /// </summary>
    private void Set_MatrixCode_ToMapCodeFence_R()
    {
        string s_Map_Fence_Right = "";
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                s_Map_Fence_Right += l2_Map_FenceRCode[i][j];
            }
        }
        cl_MapString.Set_MapCode_Fence_R(s_Map_Fence_Right);
    }

    /// <summary>
    /// Set MAP FENCE RIGHT CODE from FENCE RIGHT CODE
    /// </summary>
    private void Set_MatrixCode_FromMapCodeFence_R()
    {
        int i_CodeIndex = -1;
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                i_CodeIndex++;

                Set_MatrixCode_Fence_R(
                    new Vector2Int(i, j), 
                    cl_MapString.Get_MapCode_Fence_R()[i_CodeIndex],
                    cl_MapString.Get_MapCode_Floor()[i_CodeIndex]);
            }
        }
    }

    #endregion

    #region FLOOR Code Manager

    #region Public (Map Matrix)

    /// <summary>
    /// Set Code to SQUARE(s) of GROUND, OBJECT and FENCE Map Code
    /// </summary>
    /// <param name="v2_Pos.x">Dir UP and DOWN</param>
    /// <param name="v2_Pos.y">Dir LEFT and RIGHT</param>
    /// <param name="i_FloorChance"></param>
    public void Set_MaxtrixCode_Floor(Vector2Int v2_Pos, int i_FloorChance)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        char c_FloorChance = cl_MapString.Get_Floor(i_FloorChance);

        if (l2_Map_FloorCode[v2_Pos.x][v2_Pos.y] == c_FloorChance)
            return;

        l2_Map_FloorCode[v2_Pos.x][v2_Pos.y] = c_FloorChance;

        //Primary
        Set_Floor_Ground(v2_Pos, i_FloorChance);
        Set_Floor_Object(v2_Pos, i_FloorChance);

        //Fence
        Set_Floor_Fence_U(v2_Pos, i_FloorChance);
        Set_Floor_Fence_D(v2_Pos, i_FloorChance);
        Set_Floor_Fence_L(v2_Pos, i_FloorChance);
        Set_Floor_Fence_R(v2_Pos, i_FloorChance);
    }

    /// <summary>
    /// Set Code to SQUARE(s) of GROUND, OBJECT and FENCE Map Code
    /// </summary>
    /// <param name="v2_Pos.x">Dir UP and DOWN</param>
    /// <param name="v2_Pos.y">Dir LEFT and RIGHT</param>
    /// <param name="c_FloorCode"></param>
    public void Set_MaxtrixCode_Floor(Vector2Int v2_Pos, char c_FloorCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (l2_Map_FloorCode[v2_Pos.x][v2_Pos.y] == c_FloorCode)
            return;

        l2_Map_FloorCode[v2_Pos.x][v2_Pos.y] = c_FloorCode;

        int i_Floor = cl_MapString.Get_Floor(c_FloorCode);

        //Primary
        Set_Floor_Ground(v2_Pos, i_Floor);
        Set_Floor_Object(v2_Pos, i_Floor);

        //Fence
        Set_Floor_Fence_U(v2_Pos, i_Floor);
        Set_Floor_Fence_D(v2_Pos, i_Floor);
        Set_Floor_Fence_L(v2_Pos, i_Floor);
        Set_Floor_Fence_R(v2_Pos, i_Floor);
    }

    /// <summary>
    /// Get Code from a SQUARE of FLOOR Map Code
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns></returns>
    public char Get_MatrixCode_Floor(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return cl_MapRenderer.Get_EmtyCode();

        return l2_Map_FloorCode[v2_Pos.x][v2_Pos.y];
    }

    /// <summary>
    /// Get Code from a SQUARE of FLOOR Map Code
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns></returns>
    public int Get_MatrixCode_Floor_ToInt(Vector2Int v2_Pos)
    {
        return cl_MapString.Get_Floor(Get_MatrixCode_Floor(v2_Pos));
    }

    #endregion

    #region Private (Single Code)

    //Primary

    /// <summary>
    /// Set Floor to GROUND
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <param name="i_Floor"></param>
    private void Set_Floor_Ground(Vector2Int v2_Pos, int i_Floor)
    {
        if (Get_GameObject_Ground(v2_Pos) != null)
        {
            if (Get_GameObject_Ground(v2_Pos).GetComponent<Isometric_Single>() != null)
            {
                Get_GameObject_Ground(v2_Pos).GetComponent<Isometric_Single>().Set_Isometric_Floor(i_Floor);
            }
        }
    }

    /// <summary>
    /// Set Current Floor to GROUND
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    private void Set_Floor_Ground(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (Get_GameObject_Ground(v2_Pos) != null)
        {
            if (Get_GameObject_Ground(v2_Pos).GetComponent<Isometric_Single>() != null)
            {
                int i_Floor = cl_MapString.Get_Floor(l2_Map_FloorCode[v2_Pos.x][v2_Pos.y]);

                Get_GameObject_Ground(v2_Pos).GetComponent<Isometric_Single>().Set_Isometric_Floor(i_Floor);
            }
        }
    }

    /// <summary>
    /// Set Floor to OBJECT
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <param name="i_Floor"></param>
    private void Set_Floor_Object(Vector2Int v2_Pos, int i_Floor)
    {
        if (Get_GameObject_Object(v2_Pos) != null)
        {
            if (Get_GameObject_Object(v2_Pos).GetComponent<Isometric_Single>() != null)
            {
                Get_GameObject_Object(v2_Pos).GetComponent<Isometric_Single>().Set_Isometric_Floor(i_Floor);
            }
        }
    }

    /// <summary>
    /// Set Current Floor to OBJECT
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    private void Set_Floor_Object(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (Get_GameObject_Object(v2_Pos) != null)
        {
            if (Get_GameObject_Object(v2_Pos).GetComponent<Isometric_Single>() != null)
            {
                int i_Floor = cl_MapString.Get_Floor(l2_Map_FloorCode[v2_Pos.x][v2_Pos.y]);

                Get_GameObject_Object(v2_Pos).GetComponent<Isometric_Single>().Set_Isometric_Floor(i_Floor);
            }
        }
    }

    //Fence

    /// <summary>
    /// Set Floor to FENCE UP
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <param name="i_Floor"></param>
    private void Set_Floor_Fence_U(Vector2Int v2_Pos, int i_Floor)
    {
        if (Get_GameObject_Fence_U(v2_Pos) != null)
        {
            if (Get_GameObject_Fence_U(v2_Pos).GetComponent<Isometric_Single>() != null)
            {
                Get_GameObject_Fence_U(v2_Pos).GetComponent<Isometric_Single>().Set_Isometric_Floor(i_Floor);
            }
        }
    }

    /// <summary>
    /// Set Current Floor to FENCE UP
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    private void Set_Floor_Fence_U(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (Get_GameObject_Fence_U(v2_Pos) != null)
        {
            if (Get_GameObject_Fence_U(v2_Pos).GetComponent<Isometric_Single>() != null)
            {
                int i_Floor = cl_MapString.Get_Floor(l2_Map_FloorCode[v2_Pos.x][v2_Pos.y]);

                Get_GameObject_Fence_U(v2_Pos).GetComponent<Isometric_Single>().Set_Isometric_Floor(i_Floor);
            }
        }
    }

    /// <summary>
    /// Set Floor to FENCE DOWN
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <param name="i_Floor"></param>
    private void Set_Floor_Fence_D(Vector2Int v2_Pos, int i_Floor)
    {
        if (Get_GameObject_Fence_D(v2_Pos) != null)
        {
            if (Get_GameObject_Fence_D(v2_Pos).GetComponent<Isometric_Single>() != null)
            {
                Get_GameObject_Fence_D(v2_Pos).GetComponent<Isometric_Single>().Set_Isometric_Floor(i_Floor);
            }
        }
    }

    /// <summary>
    /// Set Current Floor to FENCE DOWN
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    private void Set_Floor_Fence_D(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (Get_GameObject_Fence_D(v2_Pos) != null)
        {
            if (Get_GameObject_Fence_D(v2_Pos).GetComponent<Isometric_Single>() != null)
            {
                int i_Floor = cl_MapString.Get_Floor(l2_Map_FloorCode[v2_Pos.x][v2_Pos.y]);

                Get_GameObject_Fence_D(v2_Pos).GetComponent<Isometric_Single>().Set_Isometric_Floor(i_Floor);
            }
        }
    }

    /// <summary>
    /// Set Floor to FENCE LEFT
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <param name="i_Floor"></param>
    private void Set_Floor_Fence_L(Vector2Int v2_Pos, int i_Floor)
    {
        if (Get_GameObject_Fence_L(v2_Pos) != null)
        {
            if (Get_GameObject_Fence_L(v2_Pos).GetComponent<Isometric_Single>() != null)
            {
                Get_GameObject_Fence_L(v2_Pos).GetComponent<Isometric_Single>().Set_Isometric_Floor(i_Floor);
            }
        }
    }

    /// <summary>
    /// Set Current Floor to FENCE LEFT
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    private void Set_Floor_Fence_L(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (Get_GameObject_Fence_L(v2_Pos) != null)
        {
            if (Get_GameObject_Fence_L(v2_Pos).GetComponent<Isometric_Single>() != null)
            {
                int i_Floor = cl_MapString.Get_Floor(l2_Map_FloorCode[v2_Pos.x][v2_Pos.y]);

                Get_GameObject_Fence_L(v2_Pos).GetComponent<Isometric_Single>().Set_Isometric_Floor(i_Floor);
            }
        }
    }

    /// <summary>
    /// Set Floor to FENCE RIGHT
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <param name="i_Floor"></param>
    private void Set_Floor_Fence_R(Vector2Int v2_Pos, int i_Floor)
    {
        if (Get_GameObject_Fence_R(v2_Pos) != null)
        {
            if (Get_GameObject_Fence_R(v2_Pos).GetComponent<Isometric_Single>() != null)
            {
                Get_GameObject_Fence_R(v2_Pos).GetComponent<Isometric_Single>().Set_Isometric_Floor(i_Floor);
            }
        }
    }

    /// <summary>
    /// Set Current Floor to FENCE RIGHT
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    private void Set_Floor_Fence_R(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (Get_GameObject_Fence_R(v2_Pos) != null)
        {
            if (Get_GameObject_Fence_R(v2_Pos).GetComponent<Isometric_Single>() != null)
            {
                int i_Floor = cl_MapString.Get_Floor(l2_Map_FloorCode[v2_Pos.x][v2_Pos.y]);

                Get_GameObject_Fence_R(v2_Pos).GetComponent<Isometric_Single>().Set_Isometric_Floor(i_Floor);
            }
        }
    }

    #endregion

    #region Private (Matrix Code)

    /// <summary>
    /// Get MAP CODE of FLOOR
    /// </summary>
    private void Set_MatrixCode_ToMapCodeFloor()
    {
        string s_Map_Floor = "";
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                s_Map_Floor += l2_Map_FloorCode[i][j];
            }
        }
        cl_MapString.Set_MapCode_Floor(s_Map_Floor);
    }

    /// <summary>
    /// Set MAP FLOOR CODE from FLOOR CODE
    /// </summary>
    private void Set_MatrixCode_FromMapCodeFloor()
    {
        int i_CodeIndex = -1;
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                i_CodeIndex++;

                Set_MaxtrixCode_Floor(new Vector2Int(i, j), cl_MapString.Get_MapCode_Floor()[i_CodeIndex]);
            }
        }
    }

    #endregion

    #endregion

    #endregion

    #region GameObject

    /// <summary>
    /// Create Isometric GameObject inside Map Manager GameObject
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <param name="g_Prefab"></param>
    /// <param name="b_isObject"></param>
    private GameObject Set_GameObject_Create(Vector2Int v2_Pos, GameObject g_Prefab, char c_Floor, bool b_onGround)
    {
        if (g_Prefab == null)
        {
            //Debug.LogWarning("Set_GameObject_Create: Prefab Emty!");
            return null;
        }

        GameObject g_GameObject = cl_Object.Set_Prepab_Create(g_Prefab, transform);

        //g_GameObject.GetComponent<Isometric_Single>().Set_Isometric_Pos(v2_Pos);
        //g_GameObject.GetComponent<Isometric_Single>().Set_Isometric_Offset(v2_Offset);
        //g_GameObject.GetComponent<Isometric_Single>().Set_onGround(b_isObject);

        int i_Floor = cl_MapString.Get_Floor(c_Floor);

        g_GameObject.GetComponent<Isometric_Single>().Set_Isometric(
            v2_Pos,
            i_Floor,
            this.f_ConstDepth,
            this.v2_Offset,
            b_onGround);

        g_GameObject.SetActive(true);

        return g_GameObject;
    }

    /// <summary>
    /// Remove Isometric GameObject
    /// </summary>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    private void Set_GameObject_Remove(GameObject g_Prefab)
    {
        cl_Object.Set_Destroy_GameObject(g_Prefab);
    }

    #endregion

    #region Limit Check

    /// <summary>
    /// Check if Pos is In Limit
    /// </summary>
    /// <param name="v2_Pos">Check this Pos if Out Limit (Ex: Get_Pos + Get_DirForward)</param>
    /// <returns></returns>
    public bool Get_Check_InsideMap(Vector2Int v2_Pos)
    {
        if (v2_Pos.x >= cl_MapString.Get_MapSize().x || v2_Pos.x < 0)
            return false;
        if (v2_Pos.y >= cl_MapString.Get_MapSize().y || v2_Pos.y < 0)
            return false;
        return true;
    }

    /// <summary>
    /// Check if Pos is In Limit
    /// </summary>
    /// <param name="v2_Pos">Start Check from Current Pos (Ex: Get_Pos)</param>
    /// <param name="v2_Dir">Check to Dir from Current Pos (Ex: Get_DirForward)</param>
    /// <returns></returns>
    public bool Get_Check_InsideMap(Vector2Int v2_Pos, Vector2Int v2_Dir)
    {
        if (v2_Pos.x + v2_Dir.x >= cl_MapString.Get_MapSize().x || v2_Pos.x + v2_Dir.x < 0)
            return false;
        if (v2_Pos.y + v2_Dir.y >= cl_MapString.Get_MapSize().y || v2_Pos.y + v2_Dir.y < 0)
            return false;
        return true;
    }

    #endregion
}
