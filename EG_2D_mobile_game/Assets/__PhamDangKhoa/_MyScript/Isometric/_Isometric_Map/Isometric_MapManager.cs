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
    [Header("Isometric Map Tag")]
    [SerializeField]
    private string s_Tag = "IsometricMap";

    /// <summary>
    /// Isomtric Map X Length Dir UP and DOWN and Y Lenght Dir LEFT and RIGHT
    /// </summary>
    [Header("Size Map Code")]

    /// <summary>
    /// Pos Offset for Map (With Offset(0,0), Start in Isometric Square(0,0))
    /// </summary>
    [SerializeField]
    private Vector2 v2_Offset = new Vector2();

    [SerializeField]
    private bool b_StartGenerate = true;

    #endregion

    #region Private Varible

    //Component

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

    //List Code

    /// <summary>
    /// Matrix of Ground Code
    /// </summary>
    private List<List<char>> l2_Map_GroundCode;

    /// <summary>
    /// Matrix of Object Code
    /// </summary>
    private List<List<char>> l2_Map_ObjectCode;

    /// <summary>
    /// Matrix of Fence UP Code
    /// </summary>
    private List<List<char>> l2_Map_FenceUpCode;

    /// <summary>
    /// Matrix of Fence DOWN Code
    /// </summary>
    private List<List<char>> l2_Map_FenceDownCode;

    /// <summary>
    /// Matrix of Fence LEFT Code
    /// </summary>
    private List<List<char>> l2_Map_FenceLeftCode;

    /// <summary>
    /// Matrix of FENCE RIGHT Code
    /// </summary>
    private List<List<char>> l2_Map_FenceRightCode;

    //List GameObject

    /// <summary>
    /// Matrix of GROUND
    /// </summary>
    private List<List<GameObject>> l2_Map_Ground;

    /// <summary>
    /// Matrix of OBJECT
    /// </summary>
    private List<List<GameObject>> l2_Map_Object;

    /// <summary>
    /// Matrix of FENCE UP
    /// </summary>
    private List<List<GameObject>> l2_Map_FenceUp;

    /// <summary>
    /// Matrix of FENCE DOWN
    /// </summary>
    private List<List<GameObject>> l2_Map_FenceDown;

    /// <summary>
    /// Matrix of FENCE LEFT
    /// </summary>
    private List<List<GameObject>> l2_Map_FenceLeft;

    /// <summary>
    /// Matrix of FENCE RIGHT
    /// </summary>
    private List<List<GameObject>> l2_Map_FenceRight;

    //Step

    private const int i_Step_WaitStill = -1;
    private const int i_Step_GenerateMap = 0;
    private const int i_Step_CurrentMap = 1;
    private const int i_Step_AutoOffset = 2;

    /// <summary>
    /// Step Working on Map
    /// </summary>
    private int i_Step = i_Step_WaitStill;

    /// <summary>
    /// Start Generate Map with Ground without Emty
    /// </summary>
    private bool b_FirstGround = false;

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

        if (b_StartGenerate)
        {
            Set_Map_StartGenerate(false);
        }
    }

    private void Update()
    {
        //Working on Step
        Set_Map_Auto_Generate();
        Set_Map_Auto_FromCurrentMapCode();
        Set_Map_Auto_Offset();
        //Working on Step
    }

    #region Map Manager

    //Public

    /// <summary>
    /// Start Map Working
    /// </summary>
    public void Set_Map_StartGenerate(bool b_FirstGround)
    {
        i_Step = i_Step_GenerateMap;
        this.b_FirstGround = b_FirstGround;
    }

    /// <summary>
    /// Stop Map Working and Remove All Map
    /// </summary>
    public void Set_Map_Remove()
    {
        i_Step = i_Step_WaitStill;

        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                Set_GameObject_Remove(l2_Map_Ground[i][j]);
                Set_GameObject_Remove(l2_Map_Object[i][j]);
                Set_GameObject_Remove(l2_Map_FenceUp[i][j]);
                Set_GameObject_Remove(l2_Map_FenceDown[i][j]);
                Set_GameObject_Remove(l2_Map_FenceLeft[i][j]);
                Set_GameObject_Remove(l2_Map_FenceRight[i][j]);

                //Set_MatrixCode_Ground(new Vector2Int(i, j), cl_MapRenderer.Get_EmtyCode());
                //Set_MatrixCode_Object(new Vector2Int(i, j), cl_MapRenderer.Get_EmtyCode());
                //Set_MatrixCode_Fence_Up(new Vector2Int(i, j), cl_MapRenderer.Get_EmtyCode());
                //Set_MatrixCode_Fence_Down(new Vector2Int(i, j), cl_MapRenderer.Get_EmtyCode());
                //Set_MatrixCode_Fence_Left(new Vector2Int(i, j), cl_MapRenderer.Get_EmtyCode());
                //Set_MatrixCode_Fence_Right(new Vector2Int(i, j), cl_MapRenderer.Get_EmtyCode());
            }
        }

        //l2_Map_GroundCode = new List<List<char>>();
        //l2_Map_ObjectCode = new List<List<char>>();
        //l2_Map_FenceUpCode = new List<List<char>>();
        //l2_Map_FenceDownCode = new List<List<char>>();
        //l2_Map_FenceLeftCode = new List<List<char>>();
        //l2_Map_FenceRightCode = new List<List<char>>();

        //l2_Map_Ground = new List<List<GameObject>>();
        //l2_Map_Object = new List<List<GameObject>>();
        //l2_Map_FenceUp = new List<List<GameObject>>();
        //l2_Map_FenceDown = new List<List<GameObject>>();
        //l2_Map_FenceLeft = new List<List<GameObject>>();
        //l2_Map_FenceRight = new List<List<GameObject>>();
    }

    /// <summary>
    /// Set MAP CODE of GROUND, OBJECT and FENCE from MAP CODE MATRIX to Get
    /// </summary>
    public void Set_MapCode_FromMapCodeMatrix()
    {
        Set_MatrixCode_ToMapCodeGround();
        Set_MatrixCode_ToMapCodeObject();
        Set_MatrixCode_ToMapCodeFence_Up();
        Set_MatrixCode_ToMapCodeFence_Down();
        Set_MatrixCode_ToMapCodeFence_Left();
        Set_MatrixCode_ToMapCodeFence_Right();
    }

    /// <summary>
    /// Check if Map is Generated Done
    /// </summary>
    /// <returns></returns>
    public bool Get_Map_DoneGenerate()
    {
        return i_Step == i_Step_AutoOffset;
    }

    /// <summary>
    /// Get Offset
    /// </summary>
    /// <returns></returns>
    public Vector2 Get_Offset()
    {
        return this.v2_Offset;
    }

    //Private (Working on Step)

    /// <summary>
    /// Generating MAP before Set Matrix Map Code from Map Code or Remove Map
    /// </summary>
    private void Set_Map_Auto_Generate()
    {
        if (i_Step != i_Step_GenerateMap)
        {
            return;
        }

        l2_Map_GroundCode = new List<List<char>>();
        l2_Map_ObjectCode = new List<List<char>>();
        l2_Map_FenceUpCode = new List<List<char>>();
        l2_Map_FenceDownCode = new List<List<char>>();
        l2_Map_FenceLeftCode = new List<List<char>>();
        l2_Map_FenceRightCode = new List<List<char>>();

        l2_Map_Ground = new List<List<GameObject>>();
        l2_Map_Object = new List<List<GameObject>>();
        l2_Map_FenceUp = new List<List<GameObject>>();
        l2_Map_FenceDown = new List<List<GameObject>>();
        l2_Map_FenceLeft = new List<List<GameObject>>();
        l2_Map_FenceRight = new List<List<GameObject>>();

        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            l2_Map_GroundCode.Add(new List<char>());
            l2_Map_ObjectCode.Add(new List<char>());
            l2_Map_FenceUpCode.Add(new List<char>());
            l2_Map_FenceDownCode.Add(new List<char>());
            l2_Map_FenceLeftCode.Add(new List<char>());
            l2_Map_FenceRightCode.Add(new List<char>());

            l2_Map_Ground.Add(new List<GameObject>());
            l2_Map_Object.Add(new List<GameObject>());
            l2_Map_FenceUp.Add(new List<GameObject>());
            l2_Map_FenceDown.Add(new List<GameObject>());
            l2_Map_FenceLeft.Add(new List<GameObject>());
            l2_Map_FenceRight.Add(new List<GameObject>());

            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                l2_Map_GroundCode[i].Add(cl_MapRenderer.Get_EmtyCode());
                l2_Map_ObjectCode[i].Add(cl_MapRenderer.Get_EmtyCode());
                l2_Map_FenceUpCode[i].Add(cl_MapRenderer.Get_EmtyCode());
                l2_Map_FenceDownCode[i].Add(cl_MapRenderer.Get_EmtyCode());
                l2_Map_FenceLeftCode[i].Add(cl_MapRenderer.Get_EmtyCode());
                l2_Map_FenceRightCode[i].Add(cl_MapRenderer.Get_EmtyCode());

                l2_Map_Ground[i].Add(null);
                l2_Map_Object[i].Add(null);
                l2_Map_FenceUp[i].Add(null);
                l2_Map_FenceDown[i].Add(null);
                l2_Map_FenceLeft[i].Add(null);
                l2_Map_FenceRight[i].Add(null);
            }
        }

        i_Step = i_Step_CurrentMap;
    }

    /// <summary>
    /// Set MATRIX MAP CODE Generating from GROUND, OBJECT and FENCE CODE from Map Code and Map Size Exist in current
    /// </summary>
    private void Set_Map_Auto_FromCurrentMapCode()
    {
        if (i_Step != i_Step_CurrentMap)
        {
            return;
        }

        if (b_FirstGround)
        {
            Set_Map_FirstGround();
        }

        Set_MatrixCode_FromMapCodeGround();
        Set_MatrixCode_FromMapCodeObject();
        Set_MatrixCode_FromMapCodeFence_Up();
        Set_MatrixCode_FromMapCodeFence_Down();
        Set_MatrixCode_FromMapCodeFence_Left();
        Set_MatrixCode_FromMapCodeFence_Right();

        i_Step = i_Step_AutoOffset;
    }

    private void Set_Map_FirstGround()
    {
        string s_GroundCode = "";
        for (int i = 0; i < cl_MapString.Get_MapSize().x * cl_MapString.Get_MapSize().y; i++)
        {
            s_GroundCode += cl_MapRenderer.Get_SingleCode_Ground(0);
        }
        cl_MapString.Set_MapCode_Ground(s_GroundCode);
    }

    /// <summary>
    /// Set Auto Offset of Map
    /// </summary>
    private void Set_Map_Auto_Offset()
    {
        if (i_Step != i_Step_AutoOffset)
        {
            return;
        }

        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                if (Get_MatrixCode_Ground(new Vector2Int(i, j)) != cl_MapRenderer.Get_EmtyCode())
                {
                    Get_GameObject_Ground(new Vector2Int(i, j)).GetComponent<Isometric_Single>().Set_Isometric_OffsetOnMap(this.v2_Offset);
                }
                if (Get_MatrixCode_Object(new Vector2Int(i, j)) != cl_MapRenderer.Get_EmtyCode())
                {
                    Get_GameObject_Object(new Vector2Int(i, j)).GetComponent<Isometric_Single>().Set_Isometric_OffsetOnMap(this.v2_Offset);
                }
                if (Get_MatrixCode_Fence_Up(new Vector2Int(i, j)) != cl_MapRenderer.Get_EmtyCode())
                {
                    Get_GameObject_Fence_Up(new Vector2Int(i, j)).GetComponent<Isometric_Single>().Set_Isometric_OffsetOnMap(this.v2_Offset);
                }
                if (Get_MatrixCode_Fence_Down(new Vector2Int(i, j)) != cl_MapRenderer.Get_EmtyCode())
                {
                    Get_GameObject_Fence_Down(new Vector2Int(i, j)).GetComponent<Isometric_Single>().Set_Isometric_OffsetOnMap(this.v2_Offset);
                }
                if (Get_MatrixCode_Fence_Left(new Vector2Int(i, j)) != cl_MapRenderer.Get_EmtyCode())
                {
                    Get_GameObject_Fence_Left(new Vector2Int(i, j)).GetComponent<Isometric_Single>().Set_Isometric_OffsetOnMap(this.v2_Offset);
                }
                if (Get_MatrixCode_Fence_Right(new Vector2Int(i, j)) != cl_MapRenderer.Get_EmtyCode())
                {
                    Get_GameObject_Fence_Right(new Vector2Int(i, j)).GetComponent<Isometric_Single>().Set_Isometric_OffsetOnMap(this.v2_Offset);
                }
            }
        }
    }

    #endregion

    #region Code

    #region Ground Code Manager

    //Public (Map Code Matrix)

    /// <summary>
    /// Set Code to a SQUARE of GROUND Map Code
    /// </summary>
    /// <param name="v2_Pos.x">Dir UP and DOWN</param>
    /// <param name="v2_Pos.y">Dir LEFT and RIGHT</param>
    /// <param name="c_GroundCode"></param>
    public void Set_MatrixCode_Ground(Vector2Int v2_Pos, char c_GroundCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (l2_Map_GroundCode[v2_Pos.x][v2_Pos.y] == c_GroundCode)
            return;

        Set_GameObject_Remove(l2_Map_Ground[v2_Pos.x][v2_Pos.y]);

        l2_Map_GroundCode[v2_Pos.x][v2_Pos.y] = c_GroundCode;

        GameObject g_Prefab = cl_MapRenderer.Get_GameObject_Ground(c_GroundCode);

        l2_Map_Ground[v2_Pos.x][v2_Pos.y] = Set_GameObject_Create(v2_Pos, g_Prefab, false);
    }

    /// <summary>
    /// Get Code from a SQUARE of GROUND Map Code
    /// </summary>
    /// <param name="v2_Pos.x">Dir UP and DOWN</param>
    /// <param name="v2_Pos.y">Dir LEFT and RIGHT</param>
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
    /// <param name="v2_Pos"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Ground(Vector2Int v2_Pos)
    {
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
        if (cl_MapString.Get_MapCode_Ground().Length != cl_MapString.Get_MapSize().x * cl_MapString.Get_MapSize().y)
        {
            //Debug.LogError("Set_MatrixCode_FromMapCodeGround: Map Code not fixed to Map Size!");
            return;
        }

        int i_CodeIndex = -1;
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                i_CodeIndex++;

                Set_MatrixCode_Ground(new Vector2Int(i, j), cl_MapString.Get_MapCode_Ground()[i_CodeIndex]);
            }
        }
    }

    #endregion

    #region Object Code Manager

    //Public (Map Code Matrix)

    /// <summary>
    /// Set Code to a SQUARE of OBJECT Map Code
    /// </summary>
    /// <param name="v2_Pos.x">Dir UP and DOWN</param>
    /// <param name="v2_Pos.y">Dir LEFT and RIGHT</param>
    /// <param name="c_ObjectCode"></param>
    public void Set_MatrixCode_Object(Vector2Int v2_Pos, char c_ObjectCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (l2_Map_ObjectCode[v2_Pos.x][v2_Pos.y] == c_ObjectCode)
            return;

        Set_GameObject_Remove(l2_Map_Object[v2_Pos.x][v2_Pos.y]);

        l2_Map_ObjectCode[v2_Pos.x][v2_Pos.y] = c_ObjectCode;

        GameObject g_Prefab = cl_MapRenderer.Get_GameObject_Object(c_ObjectCode);

        l2_Map_Object[v2_Pos.x][v2_Pos.y] = Set_GameObject_Create(v2_Pos, g_Prefab, true);
    }

    /// <summary>
    /// Get Code from a SQUARE of OBJECT Map Code
    /// </summary>
    /// <param name="v2_Pos.x">Dir UP and DOWN</param>
    /// <param name="v2_Pos.y">Dir LEFT and RIGHT</param>
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
    /// <param name="v2_Pos"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Object(Vector2Int v2_Pos)
    {
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
        if (cl_MapString.Get_MapCode_Object().Length != cl_MapString.Get_MapSize().x * cl_MapString.Get_MapSize().y)
        {
            //Debug.LogError("Set_MatrixCode_FromMapCodeObject: Map Code not fixed to Map Size!");
            return;
        }

        int i_CodeIndex = -1;
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                i_CodeIndex++;

                Set_MatrixCode_Object(new Vector2Int(i, j), cl_MapString.Get_MapCode_Object()[i_CodeIndex]);
            }
        }
    }

    #endregion

    #region Fence UP Code Manager

    //Public (Map Code Matrix)

    /// <summary>
    /// Set Code to a SQUARE of FENCE UP Map Code
    /// </summary>
    /// <param name="v2_Pos.x">Dir UP and DOWN</param>
    /// <param name="v2_Pos.y">Dir LEFT and RIGHT</param>
    /// <param name="c_FenceUpCode"></param>
    public void Set_MatrixCode_Fence_Up(Vector2Int v2_Pos, char c_FenceUpCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (l2_Map_FenceUpCode[v2_Pos.x][v2_Pos.y] == c_FenceUpCode)
            return;

        Set_GameObject_Remove(l2_Map_FenceUp[v2_Pos.x][v2_Pos.y]);

        l2_Map_FenceUpCode[v2_Pos.x][v2_Pos.y] = c_FenceUpCode;

        GameObject g_Prefab = cl_MapRenderer.Get_GameObject_Fence_Up(c_FenceUpCode);

        l2_Map_FenceUp[v2_Pos.x][v2_Pos.y] = Set_GameObject_Create(v2_Pos, g_Prefab, true);
    }

    /// <summary>
    /// Get Code from a SQUARE of FENCE UP Map Code
    /// </summary>
    /// <param name="v2_Pos.x">Dir UP and DOWN</param>
    /// <param name="v2_Pos.y">Dir LEFT and RIGHT</param>
    /// <returns></returns>
    public char Get_MatrixCode_Fence_Up(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return cl_MapRenderer.Get_EmtyCode();

        return l2_Map_FenceUpCode[v2_Pos.x][v2_Pos.y];
    }

    /// <summary>
    /// Get Isometric GameObject on FENCE UP Map
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Fence_Up(Vector2Int v2_Pos)
    {
        return l2_Map_FenceUp[v2_Pos.x][v2_Pos.y];
    }

    //Private

    /// <summary>
    /// Get MAP CODE of FENCE UP
    /// </summary>
    private void Set_MatrixCode_ToMapCodeFence_Up()
    {
        string s_Map_Fence_Up = "";
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                s_Map_Fence_Up += l2_Map_FenceUpCode[i][j];
            }
        }
        cl_MapString.Set_MapCode_Fence_Up(s_Map_Fence_Up);
    }

    /// <summary>
    /// Set MAP FENCE UP CODE from FENCE UP CODE
    /// </summary>
    private void Set_MatrixCode_FromMapCodeFence_Up()
    {
        if (cl_MapString.Get_MapCode_Fence_Up().Length != cl_MapString.Get_MapSize().x * cl_MapString.Get_MapSize().y)
        {
            //Debug.LogError("Set_MatrixCode_FromMapCodeFence_Up: Map Code not fixed to Map Size!");
            return;
        }

        int i_CodeIndex = -1;
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                i_CodeIndex++;

                Set_MatrixCode_Fence_Up(new Vector2Int(i, j), cl_MapString.Get_MapCode_Fence_Up()[i_CodeIndex]);
            }
        }
    }

    #endregion

    #region Fence DOWN Code Manager

    //Public (Map Code Matrix)

    /// <summary>
    /// Set Code to a SQUARE of FENCE DOWN Map Code
    /// </summary>
    /// <param name="v2_Pos.x">Dir UP and DOWN</param>
    /// <param name="v2_Pos.y">Dir LEFT and RIGHT</param>
    /// <param name="c_FenceDownCode"></param>
    public void Set_MatrixCode_Fence_Down(Vector2Int v2_Pos, char c_FenceDownCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (l2_Map_FenceDownCode[v2_Pos.x][v2_Pos.y] == c_FenceDownCode)
            return;

        Set_GameObject_Remove(l2_Map_FenceDown[v2_Pos.x][v2_Pos.y]);

        l2_Map_FenceDownCode[v2_Pos.x][v2_Pos.y] = c_FenceDownCode;

        GameObject g_Prefab = cl_MapRenderer.Get_GameObject_Fence_Down(c_FenceDownCode);

        l2_Map_FenceDown[v2_Pos.x][v2_Pos.y] = Set_GameObject_Create(v2_Pos, g_Prefab, true);
    }

    /// <summary>
    /// Get Code from a SQUARE of FENCE DOWN Map Code
    /// </summary>
    /// <param name="v2_Pos.x">Dir UP and DOWN</param>
    /// <param name="v2_Pos.y">Dir LEFT and RIGHT</param>
    /// <returns></returns>
    public char Get_MatrixCode_Fence_Down(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return cl_MapRenderer.Get_EmtyCode();

        return l2_Map_FenceDownCode[v2_Pos.x][v2_Pos.y];
    }

    /// <summary>
    /// Get Isometric GameObject on FENCE UP Map
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Fence_Down(Vector2Int v2_Pos)
    {
        return l2_Map_FenceDown[v2_Pos.x][v2_Pos.y];
    }

    //Private

    /// <summary>
    /// Get MAP CODE of FENCE DOWN
    /// </summary>
    private void Set_MatrixCode_ToMapCodeFence_Down()
    {
        string s_Map_Fence_Down = "";
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                s_Map_Fence_Down += l2_Map_FenceDownCode[i][j];
            }
        }
        cl_MapString.Set_MapCode_Fence_Down(s_Map_Fence_Down);
    }

    /// <summary>
    /// Set MAP FENCE DOWN CODE from FENCE DOWN CODE
    /// </summary>
    private void Set_MatrixCode_FromMapCodeFence_Down()
    {
        if (cl_MapString.Get_MapCode_Fence_Down().Length != cl_MapString.Get_MapSize().x * cl_MapString.Get_MapSize().y)
        {
            //Debug.LogError("Set_MatrixCode_FromMapCodeFence_Down: Map Code not fixed to Map Size!");
            return;
        }

        int i_CodeIndex = -1;
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                i_CodeIndex++;

                Set_MatrixCode_Fence_Down(new Vector2Int(i, j), cl_MapString.Get_MapCode_Fence_Down()[i_CodeIndex]);
            }
        }
    }

    #endregion

    #region Fence LEFT Code Manager

    //Public (Map Code Matrix)

    /// <summary>
    /// Set Code to a SQUARE of FENCE LEFT Map Code
    /// </summary>
    /// <param name="v2_Pos.x">Dir UP and DOWN</param>
    /// <param name="v2_Pos.y">Dir LEFT and RIGHT</param>
    /// <param name="c_FenceLeftCode"></param>
    public void Set_MatrixCode_Fence_Left(Vector2Int v2_Pos, char c_FenceLeftCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (l2_Map_FenceLeftCode[v2_Pos.x][v2_Pos.y] == c_FenceLeftCode)
            return;

        Set_GameObject_Remove(l2_Map_FenceLeft[v2_Pos.x][v2_Pos.y]);

        l2_Map_FenceLeftCode[v2_Pos.x][v2_Pos.y] = c_FenceLeftCode;

        GameObject g_Prefab = cl_MapRenderer.Get_GameObject_Fence_Left(c_FenceLeftCode);

        l2_Map_FenceLeft[v2_Pos.x][v2_Pos.y] = Set_GameObject_Create(v2_Pos, g_Prefab, true);
    }

    /// <summary>
    /// Get Code from a SQUARE of FENCE LEFT Map Code
    /// </summary>
    /// <param name="v2_Pos.x">Dir UP and DOWN</param>
    /// <param name="v2_Pos.y">Dir LEFT and RIGHT</param>
    /// <returns></returns>
    public char Get_MatrixCode_Fence_Left(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return cl_MapRenderer.Get_EmtyCode();

        return l2_Map_FenceLeftCode[v2_Pos.x][v2_Pos.y];
    }

    /// <summary>
    /// Get Isometric GameObject on FENCE LEFT Map
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Fence_Left(Vector2Int v2_Pos)
    {
        return l2_Map_FenceLeft[v2_Pos.x][v2_Pos.y];
    }

    //Private

    /// <summary>
    /// Get MAP CODE of FENCE LEFT
    /// </summary>
    private void Set_MatrixCode_ToMapCodeFence_Left()
    {
        string s_Map_Fence_Left = "";
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                s_Map_Fence_Left += l2_Map_FenceLeftCode[i][j];
            }
        }
        cl_MapString.Set_MapCode_Fence_Left(s_Map_Fence_Left);
    }

    /// <summary>
    /// Set MAP FENCE LEFT CODE from FENCE LEFT CODE
    /// </summary>
    private void Set_MatrixCode_FromMapCodeFence_Left()
    {
        if (cl_MapString.Get_MapCode_Fence_Down().Length != cl_MapString.Get_MapSize().x * cl_MapString.Get_MapSize().y)
        {
            //Debug.LogError("Set_MatrixCode_FromMapCodeFence_Left: Map Code not fixed to Map Size!");
            return;
        }

        int i_CodeIndex = -1;
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                i_CodeIndex++;

                Set_MatrixCode_Fence_Left(new Vector2Int(i, j), cl_MapString.Get_MapCode_Fence_Left()[i_CodeIndex]);
            }
        }
    }

    #endregion

    #region Fence RIGHT Code Manager

    //Public (Map Code Matrix)

    /// <summary>
    /// Set Code to a SQUARE of FENCE RIGHT Map Code
    /// </summary>
    /// <param name="v2_Pos.x">Dir UP and DOWN</param>
    /// <param name="v2_Pos.y">Dir LEFT and RIGHT</param>
    /// <param name="c_FenceRightCode"></param>
    public void Set_MatrixCode_Fence_Right(Vector2Int v2_Pos, char c_FenceRightCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (l2_Map_FenceRightCode[v2_Pos.x][v2_Pos.y] == c_FenceRightCode)
            return;

        Set_GameObject_Remove(l2_Map_FenceRight[v2_Pos.x][v2_Pos.y]);

        l2_Map_FenceRightCode[v2_Pos.x][v2_Pos.y] = c_FenceRightCode;

        GameObject g_Prefab = cl_MapRenderer.Get_GameObject_Fence_Right(c_FenceRightCode);

        l2_Map_FenceRight[v2_Pos.x][v2_Pos.y] = Set_GameObject_Create(v2_Pos, g_Prefab, true);
    }

    /// <summary>
    /// Get Code from a SQUARE of FENCE RIGHT Map Code
    /// </summary>
    /// <param name="v2_Pos.x">Dir UP and DOWN</param>
    /// <param name="v2_Pos.y">Dir LEFT and RIGHT</param>
    /// <returns></returns>
    public char Get_MatrixCode_Fence_Right(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return cl_MapRenderer.Get_EmtyCode();

        return l2_Map_FenceRightCode[v2_Pos.x][v2_Pos.y];
    }

    /// <summary>
    /// Get Isometric GameObject on FENCE RIGHT Map
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Fence_Right(Vector2Int v2_Pos)
    {
        return l2_Map_FenceRight[v2_Pos.x][v2_Pos.y];
    }

    //Private

    /// <summary>
    /// Get MAP CODE of FENCE RIGHT
    /// </summary>
    private void Set_MatrixCode_ToMapCodeFence_Right()
    {
        string s_Map_Fence_Right = "";
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                s_Map_Fence_Right += l2_Map_FenceRightCode[i][j];
            }
        }
        cl_MapString.Set_MapCode_Fence_Right(s_Map_Fence_Right);
    }

    /// <summary>
    /// Set MAP FENCE RIGHT CODE from FENCE RIGHT CODE
    /// </summary>
    private void Set_MatrixCode_FromMapCodeFence_Right()
    {
        if (cl_MapString.Get_MapCode_Fence_Right().Length != cl_MapString.Get_MapSize().x * cl_MapString.Get_MapSize().y)
        {
            //Debug.LogError("Set_MatrixCode_FromMapCodeFence_Right: Map Code not fixed to Map Size!");
            return;
        }

        int i_CodeIndex = -1;
        for (int i = 0; i < cl_MapString.Get_MapSize().x; i++)
        {
            for (int j = 0; j < cl_MapString.Get_MapSize().y; j++)
            {
                i_CodeIndex++;

                Set_MatrixCode_Fence_Right(new Vector2Int(i, j), cl_MapString.Get_MapCode_Fence_Right()[i_CodeIndex]);
            }
        }
    }

    #endregion

    #endregion

    #region GameObject

    /// <summary>
    /// Create Isometric GameObject inside Map Manager GameObject
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="g_Prefab"></param>
    /// <param name="b_isObject"></param>
    private GameObject Set_GameObject_Create(Vector2Int v2_Pos, GameObject g_Prefab, bool b_isObject)
    {
        if (g_Prefab == null)
        {
            //Debug.LogWarning("Set_GameObject_Create: Prefab Emty!");
            return null;
        }

        GameObject g_GameObject = cl_Object.Set_Prepab_Create(g_Prefab, transform);

        g_GameObject.GetComponent<Isometric_Single>().Set_Isometric(v2_Pos);
        g_GameObject.GetComponent<Isometric_Single>().Set_Isometric_OffsetOnMap(v2_Offset);
        g_GameObject.GetComponent<Isometric_Single>().Set_isObject(b_isObject);
        g_GameObject.SetActive(true);

        return g_GameObject;
    }

    /// <summary>
    /// Remove Isometric GameObject
    /// </summary>
    /// <param name="v2_Pos"></param>
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
