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
/// Add this Script on "Map Manager Object" to Create Map Auto when Start Scene
/// </summary>
/// <remarks>
/// Use with Camera set 'Orthographic'
/// </remarks>
/// 
public class Isometric_Map : MonoBehaviour
{
    //Notice: Use this on GameObject call "Map" to Manager Map

    #region Public Varible

    /// <summary>
    /// Tag for other Isometric Object to Find
    /// </summary>
    [Header("Isometric Map Tag")]
    [SerializeField]
    private string s_Tag = "IsometricMap";

    /// <summary>
    /// Map Size to Read
    /// </summary>
    [Header("Map")]
    [SerializeField]
    private Vector2Int v2_MapSize = new Vector2Int(9, 9);

    /// <summary>
    /// Pos Offset for Map (With Offset(0,0), Start in Isometric Square(0,0))
    /// </summary>
    [SerializeField]
    private Vector2 v2_Offset = new Vector2();

    /// <summary>
    /// List of Spawm Point(s)
    /// </summary>
    [SerializeField]
    private List<Vector2Int> l_Spawm;

    /// <summary>
    /// Add this Script on "Curson Object" to Edit Map on Object with "Isometric_Map.cs"
    /// </summary>
    [Header("Editor")]
    [SerializeField]
    private Isometric_Editor cl_Editor;

    /// <summary>
    /// Fisrt Load Map for Map Code
    /// </summary>
    [SerializeField]
    private string s_LoadMap = "";

    /// <summary>
    /// Emty Ground Code
    /// </summary>
    [Header("Ground (Prepab 'Isometric_Single')")]
    [SerializeField]
    private char c_GroundEmtyCode = 'E';

    /// <summary>
    /// List of Ground (Index 0 is Primary when new Map)
    /// </summary>
    [SerializeField]
    private List<GameObject> l_Ground;

    /// <summary>
    /// List of Ground Code
    /// </summary>
    [SerializeField]
    private List<char> l_GroundCode;

    /// <summary>
    /// Emty Object Code
    /// </summary>
    [Header("Object (Prepab 'Isometric_Single')")]
    [SerializeField]
    private char c_ObjectEmtyCode = 'E';

    /// <summary>
    /// List of Object ontop of Ground (Index 0 is Primary when new Map)
    /// </summary>
    [SerializeField]
    private List<GameObject> l_Object;

    /// <summary>
    /// List of Object
    /// </summary>
    [SerializeField]
    private List<char> l_ObjectCode;

    /// <summary>
    /// Fence Up Primary
    /// </summary>
    [Header("Fence Primary (Prefab 'Isometric_Single')")]
    [SerializeField]
    private GameObject g_Fence_Up;

    /// <summary>
    /// Fence Down Primary
    /// </summary>
    [SerializeField]
    private GameObject g_Fence_Down;

    /// <summary>
    /// Fence Left Primary
    /// </summary>
    [SerializeField]
    private GameObject g_Fence_Left;

    /// <summary>
    /// Fence Right Primary
    /// </summary>
    [SerializeField]
    private GameObject g_Fence_Right;

    /// <summary>
    /// Code Emty Fence
    /// </summary>
    [HideInInspector]
    public int i_FenceCode_Emty = -1;
    /// <summary>
    /// Code Emty Fence
    /// </summary>
    [HideInInspector]
    public char c_FenceCode_Emty = 'E';

    /// <summary>
    /// Code Fence Up
    /// </summary>
    [HideInInspector]
    public int i_FenceCode_Up = 0;
    /// <summary>
    /// Code Emty Fence
    /// </summary>
    [HideInInspector]
    public char c_FenceCode_Up = 'U';

    /// <summary>
    /// Code Fence Down
    /// </summary>
    [HideInInspector]
    public int i_FenceCode_Down = 1;
    /// <summary>
    /// Code Emty Fence
    /// </summary>
    [HideInInspector]
    public char c_FenceCode_Down = 'D';

    /// <summary>
    /// Code Fence Left
    /// </summary>
    [HideInInspector]
    public int i_FenceCode_Left = 2;
    /// <summary>
    /// Code Emty Fence
    /// </summary>
    [HideInInspector]
    public char c_FenceCode_Left = 'L';

    /// <summary>
    /// Code Fence Right
    /// </summary>
    [HideInInspector]
    public int i_FenceCode_Right = 3;
    /// <summary>
    /// Code Emty Fence
    /// </summary>
    [HideInInspector]
    public char c_FenceCode_Right = 'R';

    //Char Code Fence

    /// <summary>
    /// Fence Code Up Down
    /// </summary>
    private char c_FenceCode_UD     = '0';

    /// <summary>
    /// Fence Code Up Left
    /// </summary>
    private char c_FenceCode_UL     = '1';

    /// <summary>
    /// Fence Code Up Right
    /// </summary>
    private char c_FenceCode_UR     = '2';

    /// <summary>
    /// Fence Code Down Left
    /// </summary>
    private char c_FenceCode_DL     = '3';

    /// <summary>
    /// Fence Code Down Right
    /// </summary>
    private char c_FenceCode_DR     = '4';

    /// <summary>
    /// Fence Code Left Right
    /// </summary>
    private char c_FenceCode_LR     = '5';

    /// <summary>
    /// Fence Code Up Down Left
    /// </summary>
    private char c_FenceCode_UDL    = '6';

    /// <summary>
    /// Fence Code Up Down Right
    /// </summary>
    private char c_FenceCode_UDR    = '7';

    /// <summary>
    /// Fence Code Up Left Right
    /// </summary>
    private char c_FenceCode_ULR    = '8';

    /// <summary>
    /// Fence Code Down Left RIght
    /// </summary>
    private char c_FenceCode_DLR    = '9';

    /// <summary>
    /// Fence Code Up Down Left Right
    /// </summary>
    private char c_FenceCode_UDLR   = 'A';

    /// <summary>
    /// List Of Map Saved (In Unity)
    /// </summary>
    [Header("List Map")]
    [SerializeField]
    private List<string> l_MapSaved;

    #endregion

    #region Private Varible

    /// <summary>
    /// Class Working on Object
    /// </summary>
    private Class_Object cl_Object;

    //Ground

    /// <summary>
    /// Map Matrix
    /// </summary>
    private List<List<GameObject>> l2_Map_Ground;

    /// <summary>
    /// Map Code Matrix
    /// </summary>
    private List<List<char>> l2_Map_GroundCode;

    //Object

    /// <summary>
    /// Map Matrix
    /// </summary>
    private List<List<GameObject>> l2_Map_Object;

    /// <summary>
    /// Map Code Matrix
    /// </summary>
    private List<List<char>> l2_Map_ObjectCode;

    //Fence

    private List<List<List<GameObject>>> l3_Map_Fence;

    private List<List<List<char>>> l3_Map_FenceCode;

    #endregion

    //Method

    private void Awake()
    {
        if (s_Tag != "")
        {
            this.tag = s_Tag;
        }
    }

    private void Start()
    {
        cl_Object = new Class_Object();

        Set_FirstStart();

        //First Load Map if not NULL
        if(s_LoadMap != "")
        {
            Set_Map(s_LoadMap);
        }
    }

    private void Update()
    {
        Set_Offset();
    }

    /// <summary>
    /// Get Offset of MAP
    /// </summary>
    /// <returns></returns>
    public Vector2 Get_Offset()
    {
        return v2_Offset;
    }

    /// <summary>
    /// Get Map Size
    /// </summary>
    /// <returns></returns>
    public Vector2Int Get_MapSize()
    {
        return v2_MapSize;
    }

    #region Map Working

    /// <summary>
    /// First Load Map
    /// </summary>
    private void Set_FirstStart()
    {
        l2_Map_GroundCode = new List<List<char>>();
        l2_Map_ObjectCode = new List<List<char>>();
        l3_Map_FenceCode = new List<List<List<char>>>(); //**
        l2_Map_Ground = new List<List<GameObject>>();
        l2_Map_Object = new List<List<GameObject>>();
        l3_Map_Fence = new List<List<List<GameObject>>>(); //**

        for (int i = 0; i < v2_MapSize.x; i++)
        {
            l2_Map_GroundCode.Add(new List<char>());
            l2_Map_ObjectCode.Add(new List<char>());
            l3_Map_FenceCode.Add(new List<List<char>>());

            l2_Map_Ground.Add(new List<GameObject>());
            l2_Map_Object.Add(new List<GameObject>());
            l3_Map_Fence.Add(new List<List<GameObject>>());

            for (int j = 0; j < v2_MapSize.y; j++)
            {
                //Ground Code
                l2_Map_GroundCode[i].Add(c_GroundEmtyCode);
                //Object Code
                l2_Map_ObjectCode[i].Add(c_ObjectEmtyCode);
                //Fence Code
                l3_Map_FenceCode[i].Add(new List<char>());

                //Ground Code
                l2_Map_Ground[i].Add(null);
                //Object Code
                l2_Map_Object[i].Add(null);
                //Fence Code
                l3_Map_Fence[i].Add(new List<GameObject>());
            }
        }
    }

    private void Set_Offset()
    {
        for (int i = 0; i < v2_MapSize.x; i++)
        {
            for (int j = 0; j < v2_MapSize.y; j++)
            {
                if(l2_Map_Ground[i][j] != null)
                {
                    if (l2_Map_Ground[i][j].GetComponent<Isometric_Single>() != null)
                    {
                        l2_Map_Ground[i][j].GetComponent<Isometric_Single>().Set_Offset(v2_Offset);
                    }
                }

                if (l2_Map_Object[i][j] != null)
                {
                    if (l2_Map_Object[i][j].GetComponent<Isometric_Single>() != null)
                    {
                        l2_Map_Object[i][j].GetComponent<Isometric_Single>().Set_Offset(v2_Offset);
                    }
                }

                for (int p = 0; p < l3_Map_Fence[i][j].Count; p++) 
                {
                    if (l3_Map_Fence[i][j][p].GetComponent<Isometric_Single>() != null)
                    {
                        l3_Map_Fence[i][j][p].GetComponent<Isometric_Single>().Set_Offset(v2_Offset);
                    }
                }
            }
        }
    }

    #endregion

    #region Ground List

    /// <summary>
    /// Count Ground in List
    /// </summary>
    /// <returns></returns>
    public int Get_Count_GroundList()
    {
        return l_Ground.Count;
    }

    /// <summary>
    /// Get Sprite of Ground in List
    /// </summary>
    /// <param name="i_GroundIndex"></param>
    /// <returns></returns>
    public Sprite Get_Sprite_GroundList(int i_GroundIndex)
    {
        return l_Ground[i_GroundIndex].GetComponent<SpriteRenderer>().sprite;
    }

    /// <summary>
    /// Get Index of Ground Code
    /// </summary>
    /// <param name="c_GroundCode"></param>
    /// <returns></returns>
    public int Get_Index_GroundList(char c_GroundCode)
    {
        for (int i = 0; i < l_GroundCode.Count; i++)
        {
            if (l_GroundCode[i] == c_GroundCode)
            {
                return i;
            }
        }
        Debug.LogError("Get_GroundCode: " + c_GroundCode + " Not found!");
        return 0;
    }

    #endregion

    #region Object List

    /// <summary>
    /// Count Object in List
    /// </summary>
    /// <returns></returns>
    public int Get_Count_ObjectList()
    {
        return l_Object.Count;
    }

    /// <summary>
    /// Get Sprite of Object in List
    /// </summary>
    /// <param name="i_GroundIndex"></param>
    /// <returns></returns>
    public Sprite Get_Sprite_ObjectList(int i_GroundIndex)
    {
        return l_Object[i_GroundIndex].GetComponent<SpriteRenderer>().sprite;
    }

    /// <summary>
    /// Get Index of Object Code
    /// </summary>
    /// <param name="c_ObjectCode"></param>
    /// <returns></returns>
    private int Get_Index_ObjectList(char c_ObjectCode)
    {
        for (int i = 0; i < l_ObjectCode.Count; i++)
        {
            if (l_ObjectCode[i] == c_ObjectCode)
            {
                return i;
            }
        }
        Debug.LogError("Get_ObjectCode: " + c_ObjectCode + " Not found!");
        return 0;
    }

    #endregion

    #region Fence List

    /// <summary>
    /// Count Fence in List
    /// </summary>
    /// <returns></returns>
    public int Get_Count_FenceList()
    {
        return 4;
    }

    /// <summary>
    /// Get Fence in Square
    /// </summary>
    /// <param name="s_FenceDirCode"></param>
    /// <returns></returns>
    public List<GameObject> Get_FenceInSquare(char c_FenceCodeInSquare)
    {
        List<GameObject> l_FenceInSquare = new List<GameObject>();

        //Primary
        if (c_FenceCodeInSquare == c_FenceCode_Up) //Up
        {
            l_FenceInSquare.Add(g_Fence_Up);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_Down) //Down
        {
            l_FenceInSquare.Add(g_Fence_Down);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_Left) //Left
        {
            l_FenceInSquare.Add(g_Fence_Left);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_Right) //Right
        {
            l_FenceInSquare.Add(g_Fence_Right);
        }
        //Muti (2)
        else
        if (c_FenceCodeInSquare == c_FenceCode_UD) //UD
        {
            l_FenceInSquare.Add(g_Fence_Up);
            l_FenceInSquare.Add(g_Fence_Down);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_UL) //UL
        {
            l_FenceInSquare.Add(g_Fence_Up);
            l_FenceInSquare.Add(g_Fence_Left);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_UR) //UR
        {
            l_FenceInSquare.Add(g_Fence_Up);
            l_FenceInSquare.Add(g_Fence_Right);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_DL) //DL
        {
            l_FenceInSquare.Add(g_Fence_Down);
            l_FenceInSquare.Add(g_Fence_Left);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_DR) //DR
        {
            l_FenceInSquare.Add(g_Fence_Down);
            l_FenceInSquare.Add(g_Fence_Right);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_LR) //LR
        {
            l_FenceInSquare.Add(g_Fence_Left);
            l_FenceInSquare.Add(g_Fence_Right);
        }
        //Muti (3)
        else
        if (c_FenceCodeInSquare == c_FenceCode_UDL) //UDL
        {
            l_FenceInSquare.Add(g_Fence_Up);
            l_FenceInSquare.Add(g_Fence_Down);
            l_FenceInSquare.Add(g_Fence_Left);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_UDR) //UDR
        {
            l_FenceInSquare.Add(g_Fence_Up);
            l_FenceInSquare.Add(g_Fence_Down);
            l_FenceInSquare.Add(g_Fence_Right);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_ULR) //ULR
        {
            l_FenceInSquare.Add(g_Fence_Up);
            l_FenceInSquare.Add(g_Fence_Left);
            l_FenceInSquare.Add(g_Fence_Right);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_DLR) //DLR
        {
            l_FenceInSquare.Add(g_Fence_Down);
            l_FenceInSquare.Add(g_Fence_Left);
            l_FenceInSquare.Add(g_Fence_Right);
        }
        //All
        else
        if (c_FenceCodeInSquare == c_FenceCode_UDLR) //UDLR
        {
            l_FenceInSquare.Add(g_Fence_Up);
            l_FenceInSquare.Add(g_Fence_Down);
            l_FenceInSquare.Add(g_Fence_Left);
            l_FenceInSquare.Add(g_Fence_Right);
        }
        else
        {
            Debug.LogError("Get_FenceInSquare: Not Found Code '" + c_FenceCodeInSquare + "'!");
        }
        return l_FenceInSquare;
    }

    /// <summary>
    /// Get Fence in Square
    /// </summary>
    /// <param name="s_FenceDirCode"></param>
    /// <returns></returns>
    public List<char> Get_FenceCodeInSquare(char c_FenceCodeInSquare)
    {
        List<char> l_FenceInSquare = new List<char>();

        //Primary
        if (c_FenceCodeInSquare == c_FenceCode_Up) //Up
        {
            l_FenceInSquare.Add(c_FenceCode_Up);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_Down) //Down
        {
            l_FenceInSquare.Add(c_FenceCode_Down);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_Left) //Left
        {
            l_FenceInSquare.Add(c_FenceCode_Left);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_Right) //Right
        {
            l_FenceInSquare.Add(c_FenceCode_Right);
        }
        //Muti (2)
        else
        if (c_FenceCodeInSquare == c_FenceCode_UD) //UD
        {
            l_FenceInSquare.Add(c_FenceCode_Up);
            l_FenceInSquare.Add(c_FenceCode_Down);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_UL) //UL
        {
            l_FenceInSquare.Add(c_FenceCode_Up);
            l_FenceInSquare.Add(c_FenceCode_Left);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_UR) //UR
        {
            l_FenceInSquare.Add(c_FenceCode_Up);
            l_FenceInSquare.Add(c_FenceCode_Right);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_DL) //DL
        {
            l_FenceInSquare.Add(c_FenceCode_Down);
            l_FenceInSquare.Add(c_FenceCode_Left);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_DR) //DR
        {
            l_FenceInSquare.Add(c_FenceCode_Down);
            l_FenceInSquare.Add(c_FenceCode_Right);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_LR) //LR
        {
            l_FenceInSquare.Add(c_FenceCode_Left);
            l_FenceInSquare.Add(c_FenceCode_Right);
        }
        //Muti (3)
        else
        if (c_FenceCodeInSquare == c_FenceCode_UDL) //UDL
        {
            l_FenceInSquare.Add(c_FenceCode_Up);
            l_FenceInSquare.Add(c_FenceCode_Down);
            l_FenceInSquare.Add(c_FenceCode_Left);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_UDR) //UDR
        {
            l_FenceInSquare.Add(c_FenceCode_Up);
            l_FenceInSquare.Add(c_FenceCode_Down);
            l_FenceInSquare.Add(c_FenceCode_Right);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_ULR) //ULR
        {
            l_FenceInSquare.Add(c_FenceCode_Up);
            l_FenceInSquare.Add(c_FenceCode_Left);
            l_FenceInSquare.Add(c_FenceCode_Right);
        }
        else
        if (c_FenceCodeInSquare == c_FenceCode_DLR) //DLR
        {
            l_FenceInSquare.Add(c_FenceCode_Right);
            l_FenceInSquare.Add(c_FenceCode_Left);
            l_FenceInSquare.Add(c_FenceCode_Right);
        }
        //All
        else
        if (c_FenceCodeInSquare == c_FenceCode_UDLR) //UDLR
        {
            l_FenceInSquare.Add(c_FenceCode_Up);
            l_FenceInSquare.Add(c_FenceCode_Down);
            l_FenceInSquare.Add(c_FenceCode_Left);
            l_FenceInSquare.Add(c_FenceCode_Right);
        }
        else
        {
            Debug.LogError("Get_FenceCodeInSquare: Not Found Code '" + c_FenceCodeInSquare + "'!");
        }
        return l_FenceInSquare;
    }


    /// <summary>
    /// Get Dir code for find Fence in List
    /// </summary>
    /// <param name="i_FenceCodeIndex">[-1:Emty][0:UP][1:DOWN][2:LEFT][3:RIGHT]</param>
    /// <returns></returns>
    public char Get_FenceCode(int i_FenceCodeIndex)
    {
        if(i_FenceCodeIndex == i_FenceCode_Emty)
        {
            return c_FenceCode_Emty;
        }
        if (i_FenceCodeIndex == i_FenceCode_Up)
        {
            return c_FenceCode_Up;
        }
        if (i_FenceCodeIndex == i_FenceCode_Down)
        {
            return c_FenceCode_Down;
        }
        if (i_FenceCodeIndex == i_FenceCode_Left)
        {
            return c_FenceCode_Left;
        }
        if (i_FenceCodeIndex == i_FenceCode_Right)
        {
            return c_FenceCode_Right;
        }
        Debug.LogError("Get_Fence_DirCode: Not Found Code!");
        return ' ';
    }


    /// <summary>
    /// Get Sprite of Fence in List
    /// </summary>
    /// <param name="i_FenceCodeIndex"></param>
    /// <returns></returns>
    public Sprite Get_Sprite_FenceList(int i_FenceCodeIndex)
    {
        List<GameObject> l_FenceInSquare = Get_FenceInSquare(Get_FenceCode(i_FenceCodeIndex));

        return l_FenceInSquare[0].GetComponent<SpriteRenderer>().sprite;
    }

    #endregion

    #region Map Manager

    /// <summary>
    /// Get Code from Map (Save Map and Spawm)
    /// </summary>
    /// <returns></returns>
    public string Get_Map()
    {
        //Ground Code
        string s_MapCode = "";

        for (int i = 0; i < v2_MapSize.x; i++)
        {
            for (int j = 0; j < v2_MapSize.y; j++)
            {
                s_MapCode += l2_Map_GroundCode[i][j];
            }
        }

        //Object Code
        s_MapCode += "$";

        for (int i = 0; i < v2_MapSize.x; i++)
        {
            for (int j = 0; j < v2_MapSize.y; j++)
            {
                s_MapCode += l2_Map_ObjectCode[i][j];
            }
        }

        //Fence Code
        s_MapCode += "$";

        for (int i = 0; i < v2_MapSize.x; i++)
        {
            for (int j = 0; j < v2_MapSize.y; j++)
            {
                s_MapCode += Get_Map_FenceCode(l3_Map_FenceCode[i][j]);
                
            }
        }

        //Spawm Code
        s_MapCode += "$";

        for (int i = 0; i < l_Spawm.Count; i++)
        {
            s_MapCode += l_Spawm[i].x;
            s_MapCode += ";";
            s_MapCode += l_Spawm[i].y;
            s_MapCode += "$";
        }

        return s_MapCode;
    }

    /// <summary>
    /// Set Map from Code (Read Map and Spawm)
    /// </summary>
    /// <param name="s_MapCode"></param>
    public void Set_Map(string s_MapCode)
    {
        int i_Read = -1;

        //Read Ground
        for (int i = 0; i < v2_MapSize.x; i++)
        {
            for (int j = 0; j < v2_MapSize.y; j++)
            {
                i_Read++;
                Set_Map_Ground_Chance(new Vector2Int(i, j), s_MapCode[i_Read]);
            }
        }

        //Read Object
        i_Read++;

        for (int i = 0; i < v2_MapSize.x; i++)
        {
            for (int j = 0; j < v2_MapSize.y; j++)
            {
                i_Read++;
                Set_Map_Object_Chance(new Vector2Int(i, j), s_MapCode[i_Read]);
            }
        }

        //Read Fence
        i_Read++;

        for (int i = 0; i < v2_MapSize.x; i++)
        {
            for (int j = 0; j < v2_MapSize.y; j++)
            {
                i_Read++;
                Set_Map_Fence_Emty(new Vector2Int(i, j));
                Set_Map_Fence_Add(new Vector2Int(i, j), s_MapCode[i_Read]);
            }
        }

        //Read Spawm
        i_Read++;
        l_Spawm = new List<Vector2Int>();
        bool b_x_Read = true;
        string s_NumberSave = "";
        Vector2Int v2_Save = new Vector2Int();
        //Start Read Spawm
        while (i_Read + 1 < s_MapCode.Length)
        {
            i_Read++;
            if (b_x_Read)
            {
                if (s_MapCode[i_Read] != ';')
                {
                    s_NumberSave += s_MapCode[i_Read];
                }
                else
                {
                    v2_Save.x = int.Parse(s_NumberSave);
                    s_NumberSave = "";
                    b_x_Read = false;
                }
            }
            else
            {
                if (s_MapCode[i_Read] != '$')
                {
                    s_NumberSave += s_MapCode[i_Read];
                }
                else
                {
                    v2_Save.y = int.Parse(s_NumberSave);
                    s_NumberSave = "";
                    b_x_Read = true;

                    l_Spawm.Add(v2_Save);
                }
            }
        }

        //If Editor Curson not NULL
        if (cl_Editor != null)
        {
            cl_Editor.Set_SpawmDebug_Destroy();
            for (int i = 0; i < l_Spawm.Count; i++)
            {
                cl_Editor.Set_SpawmDebug_Add(l_Spawm[i]);
            }
        }
    }

    #endregion

    #region Map Ground Manager

    /// <summary>
    /// Ground Create
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="g_Prefab"></param>
    public void Set_Map_Ground_Create(Vector2Int v2_Pos, GameObject g_Prefab)
    {
        l2_Map_Ground[v2_Pos.x][v2_Pos.y] = cl_Object.Set_Prepab_Create(g_Prefab, this.transform);
        l2_Map_Ground[v2_Pos.x][v2_Pos.y].GetComponent<Isometric_Single>().Set_Pos(v2_Pos);
        l2_Map_Ground[v2_Pos.x][v2_Pos.y].GetComponent<Isometric_Single>().Set_Offset(v2_Offset);
        l2_Map_Ground[v2_Pos.x][v2_Pos.y].GetComponent<Isometric_Single>().Set_isObject(false);
        //l2_Ground[v2_Pos.x][v2_Pos.y].GetComponent<Isometric_Single>().Set_MapSize(v2_MapSize);
        l2_Map_Ground[v2_Pos.x][v2_Pos.y].SetActive(true);
    }

    /// <summary>
    /// Set Emty Ground
    /// </summary>
    /// <param name="v2_Pos"></param>
    public void Set_Map_Ground_Emty(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        l2_Map_GroundCode[v2_Pos.x][v2_Pos.y] = c_GroundEmtyCode;

        cl_Object.Set_Destroy_GameObject(l2_Map_Ground[v2_Pos.x][v2_Pos.y]);
    }

    //Chance i_GroundCodeIndex 

    /// <summary>
    /// Set Ground Chance
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="i_GroundCodeIndex">Use "Get_Index_GroundList()" to find Index of Ground</param>
    public void Set_Map_Ground_Chance(Vector2Int v2_Pos, int i_GroundCodeIndex)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        char c_GroundCode = l_GroundCode[i_GroundCodeIndex];

        l2_Map_GroundCode[v2_Pos.x][v2_Pos.y] = c_GroundCode;

        cl_Object.Set_Destroy_GameObject(l2_Map_Ground[v2_Pos.x][v2_Pos.y]);

        if (c_GroundCode == c_GroundEmtyCode)
            //If Emty Code >> Just Destroy and not do anything
            return;

        Set_Map_Ground_Create(v2_Pos, l_Ground[i_GroundCodeIndex]);
    }

    /// <summary>
    /// Set Ground Chance
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="i_GroundCodeIndex">Use "Get_Index_GroundList()" to find Index of Ground</param>
    public void Set_Map_Ground_Chance(Vector2Int v2_Pos, Vector2Int v2_Dir, int i_GroundCodeIndex)
    {
        if (!Get_Check_InsideMap(v2_Pos, v2_Dir))
            return;

        char c_GroundCode = l_GroundCode[i_GroundCodeIndex];

        l2_Map_GroundCode[v2_Pos.x + v2_Dir.x][v2_Pos.y + v2_Dir.y] = c_GroundCode;

        cl_Object.Set_Destroy_GameObject(l2_Map_Ground[v2_Pos.x][v2_Pos.y]);

        if (c_GroundCode == c_GroundEmtyCode)
            //If Emty Code >> Just Destroy and not do anything
            return;

        Set_Map_Ground_Create(v2_Pos + v2_Dir, l_Ground[i_GroundCodeIndex]);
    }

    // Chance c_GroundCode

    /// <summary>
    /// Set Ground Chance
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="c_GroundCode"></param>
    public void Set_Map_Ground_Chance(Vector2Int v2_Pos, char c_GroundCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        l2_Map_GroundCode[v2_Pos.x][v2_Pos.y] = c_GroundCode;

        cl_Object.Set_Destroy_GameObject(l2_Map_Ground[v2_Pos.x][v2_Pos.y]);

        if (c_GroundCode == c_GroundEmtyCode)
            //If Emty Code >> Just Destroy and not do anything
            return;

        int i_GroundCodeIndex = Get_Index_GroundList(c_GroundCode);
        Set_Map_Ground_Create(v2_Pos, l_Ground[i_GroundCodeIndex]);
    }

    /// <summary>
    /// Set Ground Chance
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="c_GroundCode"></param>
    public void Set_Map_Ground_Chance(Vector2Int v2_Pos, Vector2Int v2_Dir, char c_GroundCode)
    {
        if (!Get_Check_InsideMap(v2_Pos, v2_Dir))
            return;

        l2_Map_GroundCode[v2_Pos.x + v2_Dir.x][v2_Pos.y + v2_Dir.y] = c_GroundCode;

        cl_Object.Set_Destroy_GameObject(l2_Map_Ground[v2_Pos.x + v2_Dir.x][v2_Pos.y + v2_Dir.y]);

        if (c_GroundCode == c_GroundEmtyCode)
            //If Emty Code >> Just Destroy and not do anything
            return;

        int i_GroundCodeIndex = Get_Index_GroundList(c_GroundCode);
        Set_Map_Ground_Create(v2_Pos + v2_Dir, l_Ground[i_GroundCodeIndex]);
    }

    //Get GameObject

    /// <summary>
    /// Get Object Isometric of Ground on Map
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns></returns>
    public GameObject Get_Map_Ground(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return null;
        return l2_Map_Ground[v2_Pos.x][v2_Pos.y];
    }

    /// <summary>
    /// Get Object Isometric of Ground on Map
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="v2_Dir"></param>
    /// <returns></returns>
    public GameObject Get_Map_Ground(Vector2Int v2_Pos, Vector2Int v2_Dir)
    {
        if (!Get_Check_InsideMap(v2_Pos, v2_Dir))
            return null;
        return l2_Map_Ground[v2_Pos.x + v2_Dir.x][v2_Pos.y + v2_Dir.y];
    }

    //Get char

    /// <summary>
    /// Get Code on Map
    /// </summary>
    /// <param name="v2_Pos">Pos to Get Code on Map</param>
    /// <returns>If out Limit, return ' ' (Space)</returns>
    public char Get_Map_GroundCode(Vector2Int v2_Pos)
    {
        if (Get_Check_InsideMap(v2_Pos))
            return l2_Map_GroundCode[v2_Pos.x][v2_Pos.y];
        return ' ';
    }

    /// <summary>
    /// Get Code on Map
    /// </summary>
    /// <param name="v2_Pos">Pos to Get Code on Map</param>
    /// <returns>If out Limit, return ' ' (Space)</returns>
    public char Get_Map_GroundCode(Vector2Int v2_Pos, Vector2Int v2_Dir)
    {
        if (Get_Check_InsideMap(v2_Pos, v2_Dir))
            return l2_Map_GroundCode[v2_Pos.x + v2_Dir.x][v2_Pos.y + v2_Dir.y];
        return ' ';
    }

    #endregion

    #region Map Object Manager

    /// <summary>
    /// Object Create
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="g_Prefab"></param>
    public void Set_Map_Object_Create(Vector2Int v2_Pos, GameObject g_Prefab)
    {
        l2_Map_Object[v2_Pos.x][v2_Pos.y] = cl_Object.Set_Prepab_Create(g_Prefab, this.transform);
        l2_Map_Object[v2_Pos.x][v2_Pos.y].GetComponent<Isometric_Single>().Set_Pos(v2_Pos);
        l2_Map_Object[v2_Pos.x][v2_Pos.y].GetComponent<Isometric_Single>().Set_Offset(v2_Offset);
        l2_Map_Object[v2_Pos.x][v2_Pos.y].GetComponent<Isometric_Single>().Set_isObject(true);
        //l2_Object[v2_Pos.x][v2_Pos.y].GetComponent<Isometric_Single>().Set_MapSize(v2_MapSize);
        l2_Map_Object[v2_Pos.x][v2_Pos.y].SetActive(true);
    }

    /// <summary>
    /// Set Emty Object
    /// </summary>
    /// <param name="v2_Pos"></param>
    public void Set_Map_Object_Emty(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        l2_Map_ObjectCode[v2_Pos.x][v2_Pos.y] = c_ObjectEmtyCode;

        cl_Object.Set_Destroy_GameObject(l2_Map_Object[v2_Pos.x][v2_Pos.y]);
    }

    //Chance i_ObjectCodeIndex

    /// <summary>
    /// Set Object Chance
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="i_ObjectCodeIndex">Use "Get_Index_ObjectList()" to find Index of Ground</param>
    public void Set_Map_Object_Chance(Vector2Int v2_Pos, int i_ObjectCodeIndex)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        char c_ObjectCode = l_ObjectCode[i_ObjectCodeIndex];

        l2_Map_ObjectCode[v2_Pos.x][v2_Pos.y] = c_ObjectCode;

        cl_Object.Set_Destroy_GameObject(l2_Map_Object[v2_Pos.x][v2_Pos.y]);

        if (c_ObjectCode == c_ObjectEmtyCode)
            //If Emty Code >> Just Destroy and not do anything
            return;

        Set_Map_Object_Create(v2_Pos, l_Object[i_ObjectCodeIndex]);
    }

    /// <summary>
    /// Set Object Chance
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="v2_Dir"></param>
    /// <param name="i_ObjectCodeIndex">Use "Get_Index_ObjectList()" to find Index of Ground</param>
    public void Set_Map_Object_Chance(Vector2Int v2_Pos, Vector2Int v2_Dir, int i_ObjectCodeIndex)
    {
        if (!Get_Check_InsideMap(v2_Pos, v2_Dir))
            return;

        char c_ObjectCode = l_ObjectCode[i_ObjectCodeIndex];

        l2_Map_ObjectCode[v2_Pos.x + v2_Dir.x][v2_Pos.y + v2_Dir.y] = c_ObjectCode;

        cl_Object.Set_Destroy_GameObject(l2_Map_Object[v2_Pos.x + v2_Dir.x][v2_Pos.y + v2_Dir.y]);

        if (c_ObjectCode == c_ObjectEmtyCode)
            //If Emty Code >> Just Destroy and not do anything
            return;

        Set_Map_Object_Create(v2_Pos + v2_Dir, l_Object[i_ObjectCodeIndex]);
    }

    //Chance c_ObjectCode

    /// <summary>
    /// Set Object Chance
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="c_ObjectCode"></param>
    public void Set_Map_Object_Chance(Vector2Int v2_Pos, char c_ObjectCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        l2_Map_ObjectCode[v2_Pos.x][v2_Pos.y] = c_ObjectCode;

        cl_Object.Set_Destroy_GameObject(l2_Map_Object[v2_Pos.x][v2_Pos.y]);

        if (c_ObjectCode == c_ObjectEmtyCode)
            //If Emty Code >> Just Destroy and not do anything
            return;

        int i_ObjectCodeIndex = Get_Index_ObjectList(c_ObjectCode);

        Set_Map_Object_Create(v2_Pos, l_Object[i_ObjectCodeIndex]);
    }

    /// <summary>
    /// Set Object Chance
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="c_ObjectCode"></param>
    public void Set_Map_Object_Chance(Vector2Int v2_Pos, Vector2Int v2_Dir, char c_ObjectCode)
    {
        if (!Get_Check_InsideMap(v2_Pos, v2_Dir))
            return;

        l2_Map_ObjectCode[v2_Pos.x + v2_Dir.x][v2_Pos.y + v2_Dir.y] = c_ObjectCode;

        cl_Object.Set_Destroy_GameObject(l2_Map_Object[v2_Pos.x + v2_Dir.x][v2_Pos.y + v2_Dir.y]);

        if (c_ObjectCode == c_ObjectEmtyCode)
            //If Emty Code >> Just Destroy and not do anything
            return;

        int i_ObjectCodeIndex = Get_Index_ObjectList(c_ObjectCode);

        Set_Map_Object_Create(v2_Pos + v2_Dir, l_Object[i_ObjectCodeIndex]);
    }

    //Get GameObject

    /// <summary>
    /// Get Object Isometric of Object on Map
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns></returns>
    public GameObject Get_Map_Object(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return null;
        return l2_Map_Object[v2_Pos.x][v2_Pos.y];
    }

    /// <summary>
    /// Get Object Isometric of Object on Map
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="v2_Dir"></param>
    /// <returns></returns>
    public GameObject Get_Map_Object(Vector2Int v2_Pos, Vector2Int v2_Dir)
    {
        if (!Get_Check_InsideMap(v2_Pos, v2_Dir))
            return null;
        return l2_Map_Object[v2_Pos.x + v2_Dir.x][v2_Pos.y + v2_Dir.y];
    }

    //Get char

    /// <summary>
    /// Get Code on Map
    /// </summary>
    /// <param name="v2_Pos">Pos to Get Code on Map</param>
    /// <returns>If out Limit, return ' ' (Space)</returns>
    public char Get_Map_ObjectCode(Vector2Int v2_Pos)
    {
        if (Get_Check_InsideMap(v2_Pos))
            return l2_Map_ObjectCode[v2_Pos.x][v2_Pos.y];
        return ' ';
    }

    /// <summary>
    /// Get Code on Map
    /// </summary>
    /// <param name="v2_Pos">Pos to Get Code on Map</param>
    /// <returns>If out Limit, return ' ' (Space)</returns>
    public char Get_Map_ObjectCode(Vector2Int v2_Pos, Vector2Int v2_Dir)
    {
        if (Get_Check_InsideMap(v2_Pos, v2_Dir))
            return l2_Map_ObjectCode[v2_Pos.x + v2_Dir.x][v2_Pos.y + v2_Dir.y];
        return ' ';
    }

    #endregion

    #region Map Fence Manager

    /// <summary>
    /// Get Fence Code from Map
    /// </summary>
    /// <param name="l_MapFenceCode"></param>
    /// <returns></returns>
    public char Get_Map_FenceCode(List<char> l_MapFenceCode)
    {
        //Check Code Fence
        bool b_U = false, b_D = false, b_L = false, b_R = false;
        for (int i = 0; i < l_MapFenceCode.Count; i++)
        {
            if (l_MapFenceCode[i] == c_FenceCode_Up)
            {
                b_U = true;
                //Debug.Log("U");
            }
            else
            if (l_MapFenceCode[i] == c_FenceCode_Down)
            {
                b_D = true;
                //Debug.Log("D");
            }
            else
            if (l_MapFenceCode[i] == c_FenceCode_Left)
            {
                b_L = true;
                //Debug.Log("L");
            }
            else
            if (l_MapFenceCode[i] == c_FenceCode_Right)
            {
                b_R = true;
                //Debug.Log("R");
            }
        }
        //Primary
        if (b_U && !b_D && !b_L && !b_R) //U
            return c_FenceCode_Up;
        if (!b_U && b_D && !b_L && !b_R) //D
            return c_FenceCode_Down;
        if (!b_U && !b_D && b_L && !b_R) //L
            return c_FenceCode_Left;
        if (!b_U && !b_D && !b_L && b_R) //R
            return c_FenceCode_Right;

        //Muti (2)

        if (b_U && b_D && !b_L && !b_R) //UD
            return c_FenceCode_UD;
        if (b_U && !b_D && b_L && !b_R) //DL
            return c_FenceCode_UL;
        if (b_U && !b_D && !b_L && b_R) //UR
            return c_FenceCode_UR;

        if (!b_U && b_D && b_L && !b_R) //DL
            return c_FenceCode_DL;
        if (!b_U && b_D && !b_L && b_R) //DR
            return c_FenceCode_DR;

        if (!b_U && !b_D && b_L && b_R) //LR
            return c_FenceCode_LR;

        //Muti (3)

        if (b_U && b_D && b_L && !b_R) //UDL
            return c_FenceCode_UDL;
        if (b_U && b_D && !b_L && b_R) //UDR
            return c_FenceCode_UDR;
        if (b_U && !b_D && b_L && b_R) //ULR
            return c_FenceCode_ULR;

        if (!b_U && b_D && b_L && b_R) //DLR
            return c_FenceCode_DLR;

        //Muti (4)

        if (b_U && b_D && b_L && b_R) //UDLR
            return c_FenceCode_UDLR;

        return c_FenceCode_Emty;
    }

    private int Get_Map_Fence_Layer(Vector2Int v2_Pos, char c_FenceCode)
    {
        for (int i = 0; i < l3_Map_FenceCode[v2_Pos.x][v2_Pos.y].Count; i++) 
        {
            if (l3_Map_FenceCode[v2_Pos.x][v2_Pos.y][i] == c_FenceCode)
                return i;
        }
        return -1;
    }

    /// <summary>
    /// Fence Create
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="lg_Prefab"></param>
    public void Set_Map_Fence_Create(Vector2Int v2_Pos, List<GameObject> lg_Prefab, List<char> lg_Code)
    {
        for (int i = 0; i < lg_Prefab.Count; i++)
        {
            l3_Map_Fence[v2_Pos.x][v2_Pos.y].Add(cl_Object.Set_Prepab_Create(lg_Prefab[i], this.transform));
            l3_Map_Fence[v2_Pos.x][v2_Pos.y][l3_Map_Fence[v2_Pos.x][v2_Pos.y].Count - 1].GetComponent<Isometric_Single>().Set_Pos(v2_Pos);
            l3_Map_Fence[v2_Pos.x][v2_Pos.y][l3_Map_Fence[v2_Pos.x][v2_Pos.y].Count - 1].GetComponent<Isometric_Single>().Set_Offset(v2_Offset);
            l3_Map_Fence[v2_Pos.x][v2_Pos.y][l3_Map_Fence[v2_Pos.x][v2_Pos.y].Count - 1].GetComponent<Isometric_Single>().Set_isObject(true);
            //l3_Fence[v2_Pos.x][v2_Pos.y][l3_Fence[v2_Pos.x][v2_Pos.y].Count - 1].GetComponent<Isometric_Single>().Set_MapSize(v2_MapSize);
            l3_Map_Fence[v2_Pos.x][v2_Pos.y][l3_Map_Fence[v2_Pos.x][v2_Pos.y].Count - 1].SetActive(true);

            l3_Map_FenceCode[v2_Pos.x][v2_Pos.y].Add(lg_Code[i]);
        }
    }

    /// <summary>
    /// Emty All Fence in Square
    /// </summary>
    /// <param name="v2_Pos"></param>
    public void Set_Map_Fence_Emty(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        for (int i = 0; i < l3_Map_Fence[v2_Pos.x][v2_Pos.y].Count; i++)
        {
            cl_Object.Set_Destroy_GameObject(l3_Map_Fence[v2_Pos.x][v2_Pos.y][i]);
        }
        l3_Map_Fence[v2_Pos.x][v2_Pos.y] = new List<GameObject>();
        l3_Map_FenceCode[v2_Pos.x][v2_Pos.y] = new List<char>();
    }

    /// <summary>
    /// Set Fence Remove in Square
    /// </summary>
    /// <param name="v2_Pos"></param>
    public void Set_Map_Fence_Remove(Vector2Int v2_Pos, char c_FenceCodeToEmty)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (c_FenceCodeToEmty == c_FenceCode_Emty)
            return;

        int i_FenceLayer = Get_Map_Fence_Layer(v2_Pos, c_FenceCodeToEmty);

        if (i_FenceLayer != -1)
        {
            cl_Object.Set_Destroy_GameObject(l3_Map_Fence[v2_Pos.x][v2_Pos.y][i_FenceLayer]);

            l3_Map_FenceCode[v2_Pos.x][v2_Pos.y].RemoveAt(i_FenceLayer);
            l3_Map_Fence[v2_Pos.x][v2_Pos.y].RemoveAt(i_FenceLayer);
        }
    }

    //Add i_FenceCodeIndex

    /// <summary>
    /// Map Fence Add in Square
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="s_FenceDirCode">Use 'Get_Fence_DirCode()' instead</param>
    public void Set_Map_Fence_Add(Vector2Int v2_Pos, int i_FenceCodeIndex)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        char c_FenceCode = Get_FenceCode(i_FenceCodeIndex);

        if (c_FenceCode == c_FenceCode_Emty)
            return;

        int i_FenceLayer = Get_Map_Fence_Layer(v2_Pos, c_FenceCode);

        if(i_FenceLayer == -1)
        {
            Set_Map_Fence_Create(v2_Pos, Get_FenceInSquare(c_FenceCode), Get_FenceCodeInSquare(c_FenceCode));
        }
    }

    /// <summary>
    /// Map Fence Add in Square
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="v2_Dir"></param>
    /// <param name="s_FenceDirCode">Use 'Get_Fence_DirCode()' instead</param>
    public void Set_Map_Fence_Add(Vector2Int v2_Pos, Vector2Int v2_Dir, int i_FenceCodeIndex)
    {
        if (!Get_Check_InsideMap(v2_Pos+ v2_Dir))
            return;

        char c_FenceCode = Get_FenceCode(i_FenceCodeIndex);

        if (c_FenceCode == c_FenceCode_Emty)
            return;

        int i_FenceLayer = Get_Map_Fence_Layer(v2_Pos + v2_Dir, c_FenceCode);

        if (i_FenceLayer == -1)
        {
            Set_Map_Fence_Create(v2_Pos + v2_Dir, Get_FenceInSquare(c_FenceCode), Get_FenceCodeInSquare(c_FenceCode));
        }
    }

    //Add c_FenceCode

    /// <summary>
    /// Map Fence Add in Square
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="s_FenceDirCode">Use 'Get_Fence_DirCode()' instead</param>
    public void Set_Map_Fence_Add(Vector2Int v2_Pos, char c_FenceCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (c_FenceCode == c_FenceCode_Emty)
            return;

        int i_FenceLayer = Get_Map_Fence_Layer(v2_Pos, c_FenceCode);

        if (i_FenceLayer == -1)
        {
            Set_Map_Fence_Create(v2_Pos, Get_FenceInSquare(c_FenceCode), Get_FenceCodeInSquare(c_FenceCode));
        }
    }

    /// <summary>
    /// Map Fence Add in Square
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="v2_Dir"></param>
    /// <param name="s_FenceDirCode">Use 'Get_Fence_DirCode()' instead</param>
    public void Set_Map_Fence_Add(Vector2Int v2_Pos, Vector2Int v2_Dir, char c_FenceCode)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return;

        if (c_FenceCode == c_FenceCode_Emty)
            return;

        int i_FenceLayer = Get_Map_Fence_Layer(v2_Pos + v2_Dir, c_FenceCode);

        if (i_FenceLayer == -1)
        {
            Set_Map_Fence_Create(v2_Pos + v2_Dir, Get_FenceInSquare(c_FenceCode), Get_FenceCodeInSquare(c_FenceCode));
        }
    }

    //Get GameObject

    /// <summary>
    /// Get Object Isometric of Fence on Map
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns></returns>
    public List<GameObject> Get_Map_Fence(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return new List<GameObject>();
        return l3_Map_Fence[v2_Pos.x][v2_Pos.y];
    }

    /// <summary>
    /// Get Object Isometric of Fence on Map
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="v2_Dir"></param>
    /// <returns></returns>
    public List<GameObject> Get_Map_Fence(Vector2Int v2_Pos, Vector2Int v2_Dir)
    {
        if (!Get_Check_InsideMap(v2_Pos, v2_Dir))
            return new List<GameObject>();
        return l3_Map_Fence[v2_Pos.x + v2_Dir.x][v2_Pos.y + v2_Dir.y];
    }

    //Get char

    /// <summary>
    /// Get Code on Map
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns></returns>
    public List<char> Get_Map_FenceCode(Vector2Int v2_Pos)
    {
        if (!Get_Check_InsideMap(v2_Pos))
            return new List<char>();
        return l3_Map_FenceCode[v2_Pos.x][v2_Pos.y];
    }

    /// <summary>
    /// Get Code on Map
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="v2_Dir"></param>
    /// <returns></returns>
    public List<char> Get_Map_FenceCode(Vector2Int v2_Pos, Vector2Int v2_Dir)
    {
        if (!Get_Check_InsideMap(v2_Pos, v2_Dir))
            return new List<char>();
        return l3_Map_FenceCode[v2_Pos.x + v2_Dir.x][v2_Pos.y + v2_Dir.y];
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
        if (v2_Pos.x >= v2_MapSize.x || v2_Pos.x < 0)
            return false;
        if (v2_Pos.y >= v2_MapSize.y || v2_Pos.y < 0)
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
        if (v2_Pos.x + v2_Dir.x >= v2_MapSize.x || v2_Pos.x + v2_Dir.x < 0)
            return false;
        if (v2_Pos.y + v2_Dir.y >= v2_MapSize.y || v2_Pos.y + v2_Dir.y < 0)
            return false;
        return true;
    }

    #endregion

    #region Dir check

    //Forward (Up)

    /// <summary>
    /// Dir(-1, 0) on Isometric Square
    /// </summary>
    [HideInInspector]
    public Vector2Int v2_DirUp = new Vector2Int(-1, 0);

    /// <summary>
    /// Dir(-1 * i_Distance, 0) on Isometric Square
    /// </summary>
    /// <param name="i_Distance">Distance</param>
    /// <returns></returns>
    public Vector2Int Get_DirUp(int i_Distance)
    {
        return v2_DirUp * i_Distance;
    }

    //Backward (Down)

    /// <summary>
    /// Dir(+1, 0) on Isometric Square
    /// </summary>
    /// <returns></returns>
    [HideInInspector]
    public Vector2Int v2_DirDown = new Vector2Int(1, 0);

    /// <summary>
    /// Dir(+1 * i_Distance, 0) on Isometric Square
    /// </summary>
    /// <param name="i_Distance">Distance</param>
    /// <returns></returns>
    public Vector2Int Get_DirDown(int i_Distance)
    {
        return v2_DirDown * i_Distance;
    }

    //Left

    /// <summary>
    /// Dir(0, -1) on Isometric Square
    /// </summary>
    /// <returns></returns>
    [HideInInspector]
    public Vector2Int v2_DirLeft = new Vector2Int(0, -1);

    /// <summary>
    /// Dir(0, -1 * i_Distance) on Isometric Square
    /// </summary>
    /// <param name="i_Distance">Distance</param>
    /// <returns></returns>
    public Vector2Int Get_DirLeft(int i_Distance)
    {
        return v2_DirLeft * i_Distance;
    }

    //Right

    /// <summary>
    /// Dir(0, +1) on Isometric Square
    /// </summary>
    /// <returns></returns>
    [HideInInspector]
    public Vector2Int v2_DirRight = new Vector2Int(0, 1);

    /// <summary>
    /// Dir(0, +1 * i_Distance) on Isometric Square
    /// </summary>
    /// <param name="i_Distance">Distance</param>
    /// <returns></returns>
    public Vector2Int Get_DirRight(int i_Distance)
    {
        return v2_DirRight * i_Distance;
    }

    #endregion

    #region Fence Check

    /// <summary>
    /// Check is Up Dir have Fence?
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns>If TRUE, Square can be Move in</returns>
    public bool Get_Check_Fence_Up(Vector2Int v2_Pos)
    {
        //Pos (UP)
        if (Get_Map_FenceCode(Get_Map_FenceCode(v2_Pos)) == Get_FenceCode(i_FenceCode_Up))
            return true;
        //Dir (DOWN)
        if (Get_Map_FenceCode(Get_Map_FenceCode(v2_Pos, v2_DirUp)) == Get_FenceCode(i_FenceCode_Down))
            return true;
        return false;
    }

    /// <summary>
    /// Check is Down Dir have Fence?
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns>If TRUE, Square can be Move in</returns>
    public bool Get_Check_Fence_Down(Vector2Int v2_Pos)
    {
        //Pos (DOWN)
        if (Get_Map_FenceCode(Get_Map_FenceCode(v2_Pos)) == Get_FenceCode(i_FenceCode_Down))
            return true;
        //Dir (UP)
        if (Get_Map_FenceCode(Get_Map_FenceCode(v2_Pos, v2_DirDown)) == Get_FenceCode(i_FenceCode_Up))
            return true;
        return false;
    }

    /// <summary>
    /// Check is Left Dir have Fence?
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns></returns>
    public bool Get_Check_Fence_Left(Vector2Int v2_Pos)
    {
        //Pos (LEFT)
        if (Get_Map_FenceCode(Get_Map_FenceCode(v2_Pos)) == Get_FenceCode(i_FenceCode_Left))
            return true;
        //Dir (RIGHT)
        if (Get_Map_FenceCode(Get_Map_FenceCode(v2_Pos, v2_DirLeft)) == Get_FenceCode(i_FenceCode_Right))
            return true;
        return false;
    }

    /// <summary>
    /// Check is Right Dir have Fence?
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns></returns>
    public bool Get_Check_Fence_Right(Vector2Int v2_Pos)
    {
        //Pos (RIGHT)
        if (Get_Map_FenceCode(Get_Map_FenceCode(v2_Pos)) == Get_FenceCode(i_FenceCode_Right))
            return true;
        //Dir (LEFT)
        if (Get_Map_FenceCode(Get_Map_FenceCode(v2_Pos, v2_DirRight)) == Get_FenceCode(i_FenceCode_Left))
            return true;
        return false;
    }

    /// <summary>
    /// Check if Dir have Fence?
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <param name="v2_Dir"></param>
    /// <returns>If TRUE, Square can be Move in</returns>
    public bool Get_Check_Fence(Vector2Int v2_Pos, Vector2Int v2_Dir)
    {
        if (v2_Dir == v2_DirUp)
        {
            if (Get_Check_Fence_Up(v2_Pos))
            {
                return true;
            }
        }
        else
        if (v2_Dir == v2_DirDown)
        {
            if (Get_Check_Fence_Down(v2_Pos))
            {
                return true;
            }
        }
        else
        if (v2_Dir == v2_DirLeft)
        {
            if (Get_Check_Fence_Left(v2_Pos))
            {
                return true;
            }
        }
        else
        if (v2_Dir == v2_DirRight)
        {
            if (Get_Check_Fence_Right(v2_Pos))
            {
                return true;
            }
        }
        else
        {
            Debug.LogError("Get_Check_Fence: Dir not correct to check Fence!");
        }
        return false;
    }

    #endregion

    #region Spawm

    /// <summary>
    /// Set Add or Remove Spawm at Pos Square
    /// </summary>
    /// <param name="v2_Pos"></param>
    public int Set_SpawmChance_Removed(Vector2Int v2_Pos)
    {
        for (int i = 0; i < l_Spawm.Count; i++)
        {
            if (l_Spawm[i] == v2_Pos)
            {
                l_Spawm.RemoveAt(i);
                return i;
            }
        }
        l_Spawm.Add(v2_Pos);
        return -1;
    }

    /// <summary>
    /// Set Spawm a Object on Map when in PLAY MODE
    /// </summary>
    /// <param name="g_Prefab">Object like "Player", "Enermy", "Box", etc...</param>
    /// <param name="v2_Pos"></param>
    /// <returns>Get GameObject Created to Manager by other Script</returns>
    public GameObject Set_Spawm(GameObject g_Prefab, Vector2Int v2_Pos)
    {
        GameObject g_Object = cl_Object.Set_Prepab_Create(g_Prefab, this.transform);
        g_Object.GetComponent<Isometric_Single>().Set_Pos(v2_Pos);
        g_Object.GetComponent<Isometric_Single>().Set_Offset(v2_Offset);
        g_Object.GetComponent<Isometric_Single>().Set_isObject(true);

        if(g_Object.GetComponent<Isometric_Move>() != null)
        {
            g_Object.GetComponent<Isometric_Move>().Set_Map(this);
        }

        return g_Object;
    }

    /// <summary>
    /// Set Spawm a Object on Map when in PLAY MODE
    /// </summary>
    /// <param name="g_Prefab">Object like "Player", "Enermy", "Box", etc...</param>
    /// <param name="i_Pos_x"></param>
    /// <param name="i_Pos_y"></param>
    /// <returns>Get GameObject Created to Manager by other Script</returns>
    public GameObject Set_Spawm(GameObject g_Prefab, int i_Pos_x, int i_Pos_y)
    {
        GameObject g_Object = cl_Object.Set_Prepab_Create(g_Prefab, this.transform);
        g_Object.GetComponent<Isometric_Single>().Set_Pos(new Vector2(i_Pos_x, i_Pos_y));
        g_Object.GetComponent<Isometric_Single>().Set_Offset(v2_Offset);
        g_Object.GetComponent<Isometric_Single>().Set_isObject(true);

        if (g_Object.GetComponent<Isometric_Move>() != null)
        {
            g_Object.GetComponent<Isometric_Move>().Set_Map(this);
        }

        return g_Object;
    }

    /// <summary>
    /// Get List of Spawm Point
    /// </summary>
    /// <returns></returns>
    public List<Vector2Int> Get_SpawmList()
    {
        return l_Spawm;
    }

    /// <summary>
    /// Get Spawm from List
    /// </summary>
    /// <param name="i_IndexInList"></param>
    /// <returns></returns>
    public Vector2Int Get_Spawm(int i_IndexInList)
    {
        return l_Spawm[i_IndexInList];
    }

    /// <summary>
    /// Get Count of List of Spawm Point
    /// </summary>
    /// <returns></returns>
    public int Get_SpawmListCount()
    {
        return l_Spawm.Count;
    }

    #endregion

    #region List Map Saved (Add in Unity and Use in Script)

    /// <summary>
    /// Set Map Saved
    /// </summary>
    /// <param name="l_MapSaved"></param>
    public void Set_Map_MapSaved(List<string> l_MapSaved)
    {
        this.l_MapSaved = l_MapSaved;
    }

    /// <summary>
    /// Get Code From List Map Code
    /// </summary>
    /// <param name="i_MapCodeListIndex"></param>
    /// <returns></returns>
    public string Get_Map_MapSaved(int i_MapCodeListIndex)
    {
        return l_MapSaved[i_MapCodeListIndex];
    }

    /// <summary>
    /// Get List Map Code Count
    /// </summary>
    /// <returns></returns>
    public int Get_Count_MapSaved()
    {
        return l_MapSaved.Count;
    }

    #endregion
}
