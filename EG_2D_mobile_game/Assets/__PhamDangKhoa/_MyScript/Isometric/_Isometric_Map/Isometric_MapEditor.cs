using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Isometric_Single))]
public class Isometric_MapEditor : MonoBehaviour
{
    /// <summary>
    /// Tag for other Isometric Object to Find
    /// </summary>
    [Header("Isometric Map Tag")]
    [SerializeField]
    private string s_Tag = "IsometricMap";

    [SerializeField]
    private GameObject g_MapManager;

    #region Control Editor

    /// <summary>
    /// Move Up
    /// </summary>
    [Header("Move the Curson")]
    [SerializeField]
    private KeyCode k_Up = KeyCode.UpArrow;

    /// <summary>
    /// Move Down
    /// </summary>
    [SerializeField]
    private KeyCode k_Down = KeyCode.DownArrow;

    /// <summary>
    /// Move Left
    /// </summary>
    [SerializeField]
    private KeyCode k_Left = KeyCode.LeftArrow;

    /// <summary>
    /// Move Right
    /// </summary>
    [SerializeField]
    private KeyCode k_Right = KeyCode.RightArrow;

    /// <summary>
    /// Chance Square of current Square choice
    /// </summary>
    [Header("Edit a Square")]
    [SerializeField]
    private KeyCode k_Edit = KeyCode.Return;

    /// <summary>
    /// Remove Square of current
    /// </summary>
    [SerializeField]
    private KeyCode k_Remove = KeyCode.Delete;

    /// <summary>
    /// Chance Layer on a Square current
    /// </summary>
    [Header("Choice to Edit")]
    [SerializeField]
    private KeyCode k_Layer = KeyCode.Tab;

    /// <summary>
    /// Next Choice of Square on current Square
    /// </summary>
    [SerializeField]
    private KeyCode k_Next = KeyCode.RightBracket;

    /// <summary>
    /// Previos Choice of Square on current Square
    /// </summary>
    [SerializeField]
    private KeyCode k_Back = KeyCode.LeftBracket;

    /// <summary>
    /// Spawm Point (Set / Not Set)
    /// </summary>
    [Header("Spawm on Map")]
    [SerializeField]
    private KeyCode k_Spawm = KeyCode.Home;

    [Header("File Map Control")]
    [SerializeField]
    private KeyCode k_Open = KeyCode.F5;

    [SerializeField]
    private KeyCode k_New = KeyCode.F9;

    /// <summary>
    /// Name of Map (Will auto add File Exten '*.isomap')
    /// </summary>
    [SerializeField]
    private string s_MapName = "NewMap";

    /// <summary>
    /// Current Choice
    /// </summary>
    [Header("Debug Curson Editor")]
    [SerializeField]
    private SpriteRenderer s_SpriteChoice;

    /// <summary>
    /// Current Layer
    /// </summary>
    [SerializeField]
    private Text s_LayerChoice;

    /// <summary>
    /// Guild Keyboard to Control
    /// </summary>
    [SerializeField]
    private Text s_GuildKeyboard;

    /// <summary>
    /// If not NULL, auto set 's_MapName'
    /// </summary>
    [Header("Debug File Editor")]
    [SerializeField]
    private InputField i_MapName;

    /// <summary>
    /// Map Size X Input
    /// </summary>
    [SerializeField]
    private InputField i_MapSize_X;

    /// <summary>
    /// Map Size Y Input
    /// </summary>
    [SerializeField]
    private InputField i_MapSize_Y;

    /// <summary>
    /// Spawm Point Debug
    /// </summary>
    [Header("Debug File Editor")]
    [SerializeField]
    private GameObject g_SpawmPoint;

    #endregion

    #region Private Varible

    /// <summary>
    /// Add this Script on "2.5D Sprite" with "Ground", "Object" like "Box", "Character", etc...
    /// </summary>
    /// <remarks>
    /// Use with Camera set 'Orthographic'
    /// </remarks>
    private Isometric_Single cl_Single;

    /// <summary>
    /// Map Manager of Matrix Map Code and Matrix Map Isometric GameObject
    /// </summary>
    private Isometric_MapManager cl_MapManager_MapManager;

    /// <summary>
    /// Save Single Code and Single Isometric GameObject of Square on Matrix Map
    /// </summary>
    private Isometric_MapRenderer cl_MapManager_Renderer;

    /// <summary>
    /// Save Map Code by Single String for Matrix Map Code
    /// </summary>
    private Isometric_MapString cl_MapManager_MapString;

    /// <summary>
    /// Working on Vector
    /// </summary>
    private Class_Vector cl_Vector;

    /// <summary>
    /// Working on PlayerPrebs and Scene
    /// </summary>
    private Class_Scene cl_Scene;

    /// <summary>
    /// Working on GameObject and Prepab
    /// </summary>
    private Class_Object cl_Object;

    /// <summary>
    /// Choice Square
    /// </summary>
    private int i_Choice = 0;

    /// <summary>
    /// Choice Layer
    /// </summary>
    private int i_Layer = 0;

    /// <summary>
    /// Spawm Point List Object Debug
    /// </summary>
    private List<GameObject> lg_Spawm;

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

        cl_Single = GetComponent<Isometric_Single>();

        cl_MapManager_MapManager = g_MapManager.GetComponent<Isometric_MapManager>();
        cl_MapManager_Renderer = g_MapManager.GetComponent<Isometric_MapRenderer>();
        cl_MapManager_MapString = g_MapManager.GetComponent<Isometric_MapString>();

        cl_Vector = new Class_Vector();
        cl_Scene = new Class_Scene();
        cl_Object = new Class_Object();

        lg_Spawm = new List<GameObject>();

        s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Ground(0).GetComponent<SpriteRenderer>().sprite;
        s_LayerChoice.text = "Ground [" + cl_MapManager_Renderer.Get_SingleCode_Ground(0) + "]";

        if (i_MapName != null)
        {
            i_MapName.text = cl_Scene.Get_PlayerPrefs_String("_IsoMapName");

            if (i_MapName.text == "")
            {
                i_MapName.text = "NewMap";
            }
        }

        cl_MapManager_MapManager.Set_Map_StartGenerate(false);
        Set_MapCode_FromFile(true);

        i_MapSize_X.text = cl_MapManager_MapManager.Get_MapSize().x.ToString();
        i_MapSize_Y.text = cl_MapManager_MapManager.Get_MapSize().y.ToString();

        s_GuildKeyboard.text =
            "- Edit Square: " + k_Edit + "\n" +
            "- Remove Square: " + k_Remove + "\n" +
            "- Change Layer: " + k_Layer + "\n" +
            "- Next Choice: " + k_Next + "\n" +
            "- Previous Choice: " + k_Back + "\n" +
            "- Open Map: " + k_Open + "\n" +
            "- New Map: " + k_New;
    }

    private void Update()
    {
        Set_ControlKeyboard();

        if (i_MapName != null)
        {
            s_MapName = i_MapName.text;
        }
    }

    /// <summary>
    /// Control Curson
    /// </summary>
    private void Set_ControlKeyboard()
    {
        //if (!cl_MapManager_MapManager.Get_Map_DoneGenerate())
        //    return;

        cl_Single.Set_Offset(cl_MapManager_MapManager.Get_Offset());

        //Control Curson

        if (Input.GetKeyDown(k_Up))
        {
            Vector2 v2_Current = cl_Single.Get_Pos();

            if (cl_MapManager_MapManager.Get_Check_InsideMap(cl_Vector.Get_VectorInt(v2_Current), new Class_Isometric().v2_DirUp))
            {
                cl_Single.Set_Pos(new Vector2(v2_Current.x - 1, v2_Current.y));
            }
        }

        if (Input.GetKeyDown(k_Down))
        {
            Vector2 v2_Current = cl_Single.Get_Pos();

            if (cl_MapManager_MapManager.Get_Check_InsideMap(cl_Vector.Get_VectorInt(v2_Current), new Class_Isometric().v2_DirDown))
            {
                cl_Single.Set_Pos(new Vector2(v2_Current.x + 1, v2_Current.y));
            }
        }

        if (Input.GetKeyDown(k_Left))
        {
            Vector2 v2_Current = cl_Single.Get_Pos();

            if (cl_MapManager_MapManager.Get_Check_InsideMap(cl_Vector.Get_VectorInt(v2_Current), new Class_Isometric().v2_DirLeft))
            {
                cl_Single.Set_Pos(new Vector2(v2_Current.x, v2_Current.y - 1));
            }
        }

        if (Input.GetKeyDown(k_Right))
        {
            Vector2 v2_Current = cl_Single.Get_Pos();

            if (cl_MapManager_MapManager.Get_Check_InsideMap(cl_Vector.Get_VectorInt(v2_Current), new Class_Isometric().v2_DirRight))
            {
                cl_Single.Set_Pos(new Vector2(v2_Current.x, v2_Current.y + 1));
            }
        }

        //Choice

        if (Input.GetKeyDown(k_Next))
        {
            switch (i_Layer)
            {
                case 0:
                    //Ground
                    i_Choice++;
                    if (i_Choice >= cl_MapManager_Renderer.Get_CountList_Ground())
                        i_Choice = 0;

                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Ground(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Ground [" + cl_MapManager_Renderer.Get_SingleCode_Ground(i_Choice) + "]";
                    break;
                case 1:
                    //Object
                    i_Choice++;
                    if (i_Choice >= cl_MapManager_Renderer.Get_CountList_Object())
                        i_Choice = 0;

                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Object(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Object [" + cl_MapManager_Renderer.Get_SingleCode_Object(i_Choice) + "]";
                    break;
                case 2:
                    //Fence U
                    i_Choice++;
                    if (i_Choice >= cl_MapManager_Renderer.Get_CountList_Fence_Up())
                        i_Choice = 0;

                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_Up(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Fence U [" + cl_MapManager_Renderer.Get_SingleCode_Fence_Up(i_Choice) + "]";
                    break;
                case 3:
                    //Fence D
                    i_Choice++;
                    if (i_Choice >= cl_MapManager_Renderer.Get_CountList_Fence_Down())
                        i_Choice = 0;

                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_Down(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Fence D [" + cl_MapManager_Renderer.Get_SingleCode_Fence_Down(i_Choice) + "]";
                    break;
                case 4:
                    //Fence L
                    i_Choice++;
                    if (i_Choice >= cl_MapManager_Renderer.Get_CountList_Fence_Left())
                        i_Choice = 0;

                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_Left(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Fence L [" + cl_MapManager_Renderer.Get_SingleCode_Fence_Left(i_Choice) + "]";
                    break;
                case 5:
                    //Fence R
                    i_Choice++;
                    if (i_Choice >= cl_MapManager_Renderer.Get_CountList_Fence_Right())
                        i_Choice = 0;

                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_Right(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Fence R [" + cl_MapManager_Renderer.Get_SingleCode_Fence_Right(i_Choice) + "]";
                    break;
            }
        }

        if (Input.GetKeyDown(k_Back))
        {
            switch (i_Layer)
            {
                case 0:
                    //Ground
                    i_Choice--;
                    if (i_Choice < 0)
                        i_Choice = cl_MapManager_Renderer.Get_CountList_Ground() - 1;

                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Ground(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Ground [" + cl_MapManager_Renderer.Get_SingleCode_Ground(i_Choice) + "]";
                    break;
                case 1:
                    //Object
                    i_Choice--;
                    if (i_Choice < 0)
                        i_Choice = cl_MapManager_Renderer.Get_CountList_Object() - 1;

                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Object(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Object [" + cl_MapManager_Renderer.Get_SingleCode_Object(i_Choice) + "]";
                    break;
                case 2:
                    //Fence U
                    i_Choice--;
                    if (i_Choice < 0)
                        i_Choice = cl_MapManager_Renderer.Get_CountList_Fence_Up() - 1;

                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_Up(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Fence U [" + cl_MapManager_Renderer.Get_SingleCode_Fence_Up(i_Choice) + "]";
                    break;
                case 3:
                    //Fence D
                    i_Choice--;
                    if (i_Choice < 0)
                        i_Choice = cl_MapManager_Renderer.Get_CountList_Fence_Down() - 1;

                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_Down(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Fence D [" + cl_MapManager_Renderer.Get_SingleCode_Fence_Down(i_Choice) + "]";
                    break;
                case 4:
                    //Fence L
                    i_Choice--;
                    if (i_Choice < 0)
                        i_Choice = cl_MapManager_Renderer.Get_CountList_Fence_Left() - 1;

                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_Left(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Fence L [" + cl_MapManager_Renderer.Get_SingleCode_Fence_Left(i_Choice) + "]";
                    break;
                case 5:
                    //Fence R
                    i_Choice--;
                    if (i_Choice < 0)
                        i_Choice = cl_MapManager_Renderer.Get_CountList_Fence_Right() - 1;

                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_Right(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Fence R [" + cl_MapManager_Renderer.Get_SingleCode_Fence_Right(i_Choice) + "]";
                    break;
            }
        }

        //Mode

        if (Input.GetKeyDown(k_Layer))
        {
            i_Layer++;

            if (i_Layer > 5)
                i_Layer = 0;

            i_Choice = 0;

            switch (i_Layer)
            {
                case 0:
                    //Ground
                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Ground(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Ground [" + cl_MapManager_Renderer.Get_SingleCode_Ground(i_Choice) + "]";
                    break;
                case 1:
                    //Object
                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Object(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Object [" + cl_MapManager_Renderer.Get_SingleCode_Object(i_Choice) + "]";
                    break;
                case 2:
                    //Fence U
                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_Up(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Fence U [" + cl_MapManager_Renderer.Get_SingleCode_Fence_Up(i_Choice) + "]";
                    break;
                case 3:
                    //Fence
                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_Down(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Fence D [" + cl_MapManager_Renderer.Get_SingleCode_Fence_Down(i_Choice) + "]";
                    break;
                case 4:
                    //Fence
                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_Left(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Fence L [" + cl_MapManager_Renderer.Get_SingleCode_Fence_Left(i_Choice) + "]";
                    break;
                case 5:
                    //Fence
                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_Right(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Fence R [" + cl_MapManager_Renderer.Get_SingleCode_Fence_Right(i_Choice) + "]";
                    break;
            }
        }

        //Edit

        if (Input.GetKeyDown(k_Edit))
        {
            Vector2Int v2_Current = cl_Vector.Get_VectorInt(cl_Single.Get_Pos());
            switch (i_Layer)
            {
                case 0:
                    //Ground
                    cl_MapManager_MapManager.Set_MatrixCode_Ground(v2_Current, cl_MapManager_Renderer.Get_SingleCode_Ground(i_Choice));
                    break;
                case 1:
                    //Object
                    cl_MapManager_MapManager.Set_MatrixCode_Object(v2_Current, cl_MapManager_Renderer.Get_SingleCode_Object(i_Choice));
                    break;
                case 2:
                    //Fence
                    cl_MapManager_MapManager.Set_MatrixCode_Fence_Up(v2_Current, cl_MapManager_Renderer.Get_SingleCode_Fence_Up(i_Choice));
                    break;
                case 3:
                    //Fence
                    cl_MapManager_MapManager.Set_MatrixCode_Fence_Down(v2_Current, cl_MapManager_Renderer.Get_SingleCode_Fence_Down(i_Choice));
                    break;
                case 4:
                    //Fence
                    cl_MapManager_MapManager.Set_MatrixCode_Fence_Left(v2_Current, cl_MapManager_Renderer.Get_SingleCode_Fence_Left(i_Choice));
                    break;
                case 5:
                    //Fence
                    cl_MapManager_MapManager.Set_MatrixCode_Fence_Right(v2_Current, cl_MapManager_Renderer.Get_SingleCode_Fence_Right(i_Choice));
                    break;
            }

            //Auto Save
            Set_MapCode_ToFile();

            //Save Map Name to cache
            cl_Scene.Set_PlayerPrefs("_IsoMapName", s_MapName);
        }

        //Remove

        if (Input.GetKeyDown(k_Remove))
        {
            Vector2Int v2_Current = cl_Vector.Get_VectorInt(cl_Single.Get_Pos());
            switch (i_Layer)
            {
                case 0:
                    //Ground
                    cl_MapManager_MapManager.Set_MatrixCode_Ground(v2_Current, cl_MapManager_Renderer.Get_EmtyCode());
                    break;
                case 1:
                    //Object
                    cl_MapManager_MapManager.Set_MatrixCode_Object(v2_Current, cl_MapManager_Renderer.Get_EmtyCode());
                    break;
                case 2:
                    //Fence
                    cl_MapManager_MapManager.Set_MatrixCode_Fence_Up(v2_Current, cl_MapManager_Renderer.Get_EmtyCode());
                    break;
                case 3:
                    //Fence
                    cl_MapManager_MapManager.Set_MatrixCode_Fence_Down(v2_Current, cl_MapManager_Renderer.Get_EmtyCode());
                    break;
                case 4:
                    //Fence
                    cl_MapManager_MapManager.Set_MatrixCode_Fence_Left(v2_Current, cl_MapManager_Renderer.Get_EmtyCode());
                    break;
                case 5:
                    //Fence
                    cl_MapManager_MapManager.Set_MatrixCode_Fence_Right(v2_Current, cl_MapManager_Renderer.Get_EmtyCode());
                    break;
            }

            //Auto Save
            Set_MapCode_ToFile();

            //Save Map Name to cache
            cl_Scene.Set_PlayerPrefs("_IsoMapName", s_MapName);
        }

        //Open

        if (Input.GetKeyDown(k_Open))
        {
            Set_MapCode_FromFile(false);
        }

        //New

        if (Input.GetKeyDown(k_New))
        {
            Set_MapGenerate_NewMap();
        }

        //Spawm

        if (Input.GetKeyDown(k_Spawm))
        {
            Set_Debug_SpawmPoint_Chance();

            //Auto Save
            Set_MapCode_ToFile();
        }
    }

    //Public (Button for Generate)

    /// <summary>
    /// Generate new Emty Map and Save to File
    /// </summary>
    public void Set_MapGenerate_NewMap()
    {
        cl_Single.Set_Pos(new Vector2(0, 0));

        cl_MapManager_MapManager.Set_Map_Remove();

        cl_MapManager_MapManager.Set_Map_MapSize(new Vector2Int(
            int.Parse(i_MapSize_X.text),
            int.Parse(i_MapSize_Y.text)));

        cl_MapManager_MapManager.Set_Map_StartGenerate(true);

        Set_MapCode_ToFile();
    }

    //Public (Set Matrix Map Code from File)

    /// <summary>
    /// Set MAP CODE Generating from GROUND, OBJECT and FENCE CODE from File
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="b_FromTempFile">Temp File is a copy of current File during Edit</param>
    private void Set_MapCode_FromFile(bool b_FromTempFile)
    {
        Class_FileIO cl_File = new Class_FileIO();
        string s_File = "C:\\Users\\Admin\\Documents\\" + s_MapName + ".isomap";

        if (b_FromTempFile)
        {
            s_File = "C:\\Users\\Admin\\Documents\\_Temp.isomap";
        }
        else
        {
            cl_MapManager_MapManager.Set_Map_Remove();
            Set_Debug_SpawmPoint_Remove();
        }

        if (!cl_File.Get_FileExist(s_File))
        {
            Debug.LogWarning("Set_MapCode_FromFile: File Map not Exist!");
            return;
        }

        cl_File.Set_Act_Read_Clear();
        cl_File.Set_Act_Read_Start(s_File);

        int i_MapXCount = cl_File.Get_Read_Auto_Int();
        int i_MapYCount = cl_File.Get_Read_Auto_Int();
        cl_MapManager_MapManager.Set_Map_MapSize(new Vector2Int(i_MapXCount, i_MapYCount));

        cl_MapManager_MapString.Set_MapCode_Ground(cl_File.Get_Read_Auto_String());
        cl_MapManager_MapString.Set_MapCode_Object(cl_File.Get_Read_Auto_String());
        cl_MapManager_MapString.Set_MapCode_Fence_Up(cl_File.Get_Read_Auto_String());
        cl_MapManager_MapString.Set_MapCode_Fence_Down(cl_File.Get_Read_Auto_String());
        cl_MapManager_MapString.Set_MapCode_Fence_Left(cl_File.Get_Read_Auto_String());
        cl_MapManager_MapString.Set_MapCode_Fence_Right(cl_File.Get_Read_Auto_String());

        cl_MapManager_MapString.Set_List_SpawmPoint_Reset();
        int i_SpawmPoint = cl_File.Get_Read_Auto_Int();
        for (int i = 0; i < i_SpawmPoint; i++)
        {
            int i_x = cl_File.Get_Read_Auto_Int();
            int i_y = cl_File.Get_Read_Auto_Int();
            cl_MapManager_MapString.Set_List_SpawmPoint_Add(new Vector2Int(i_x, i_y));
        }
        Set_Debug_SpawmPoint_New();

        cl_MapManager_MapManager.Set_Map_StartGenerate(false);
    }

    private void Set_MapCode_ToFile()
    {
        cl_MapManager_MapManager.Set_MapCode_FromMapCodeMatrix();

        Class_FileIO cl_File = new Class_FileIO();
        string s_File = "C:\\Users\\Admin\\Documents\\" + s_MapName + ".isomap";
        string s_FileTemp = "C:\\Users\\Admin\\Documents\\_Temp.isomap";

        cl_File.Set_Act_Write_Clear();

        cl_File.Set_Act_Write_Add(cl_MapManager_MapManager.Get_MapSize().x);
        cl_File.Set_Act_Write_Add(cl_MapManager_MapManager.Get_MapSize().y);

        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_MapCode_Ground());
        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_MapCode_Object());
        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_MapCode_Fence_Up());
        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_MapCode_Fence_Down());
        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_MapCode_Fence_Left());
        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_MapCode_Fence_Right());

        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_List_SpawmPoint().Count);
        for (int i = 0; i < cl_MapManager_MapString.Get_List_SpawmPoint().Count; i++)
        {
            cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_List_SpawmPoint()[i].x);
            cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_List_SpawmPoint()[i].y);
        }

        cl_File.Set_Act_Write_Start(s_File);
        cl_File.Set_Act_Write_Start(s_FileTemp);
    }

    #region Spawm Debug Manager

    /// <summary>
    /// New Debug Spawm Point(s)
    /// </summary>
    private void Set_Debug_SpawmPoint_New()
    {
        for (int i = 0; i < cl_MapManager_MapString.Get_List_SpawmPoint().Count; i++)
        {
            Set_Debug_SpawmPoint_Prefab(cl_MapManager_MapString.Get_List_SpawmPoint()[i]);
        }
    }

    /// <summary>
    /// Remove all Debug Spawm Point(s)
    /// </summary>
    private void Set_Debug_SpawmPoint_Remove()
    {
        for (int i = 0; i < cl_MapManager_MapString.Get_List_SpawmPoint().Count; i++)
        {
            cl_Object.Set_Destroy_GameObject(lg_Spawm[0]);
        }
        lg_Spawm = new List<GameObject>();
    }

    /// <summary>
    /// Chance Spawm Point on Pos
    /// </summary>
    private void Set_Debug_SpawmPoint_Chance()
    {
        for (int i = 0; i < cl_MapManager_MapString.Get_List_SpawmPoint().Count; i++)
        {
            if (cl_Single.Get_Pos() == cl_MapManager_MapString.Get_List_SpawmPoint()[i])
            {
                cl_Object.Set_Destroy_GameObject(lg_Spawm[i]);
                cl_MapManager_MapString.Set_List_SpawmPoint_Remove(i);
                lg_Spawm.RemoveAt(i);
                return;
            }
        }
        Set_Debug_SpawmPoint_Prefab(new Vector2Int((int)cl_Single.Get_Pos().x, (int)cl_Single.Get_Pos().y));
        cl_MapManager_MapString.Set_List_SpawmPoint_Add(
            new Vector2Int((int)cl_Single.Get_Pos().x, (int)cl_Single.Get_Pos().y));
    }

    /// <summary>
    /// Create Spawm Point Prefab
    /// </summary>
    /// <param name="v2_Pos"></param>
    private void Set_Debug_SpawmPoint_Prefab(Vector2Int v2_Pos)
    {
        GameObject g_SpawmPoint = cl_Object.Set_Prepab_Create(this.g_SpawmPoint, g_MapManager.transform);

        g_SpawmPoint.GetComponent<Isometric_Single>().Set_Pos(v2_Pos);
        g_SpawmPoint.GetComponent<Isometric_Single>().Set_Offset(cl_MapManager_MapManager.Get_Offset());
        g_SpawmPoint.GetComponent<Isometric_Single>().Set_isObject(true);
        g_SpawmPoint.SetActive(true);
        
        lg_Spawm.Add(g_SpawmPoint);
    }

    #endregion
}
