﻿using System.Collections;
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
    /// Next Layer on a Square current
    /// </summary>
    [Header("Choice Layer to Edit")]
    [SerializeField]
    private KeyCode k_Layer_Next = KeyCode.PageUp;

    /// <summary>
    /// Previos Layer on a Square current
    /// </summary>
    [SerializeField]
    private KeyCode k_Layer_Back = KeyCode.PageDown;

    /// <summary>
    /// Next Choice of Square on current Square
    /// </summary>
    [Header("Choice Square to Edit")]
    [SerializeField]
    private KeyCode k_Square_Next = KeyCode.RightBracket;

    /// <summary>
    /// Previos Choice of Square on current Square
    /// </summary>
    [SerializeField]
    private KeyCode k_Square_Back = KeyCode.LeftBracket;

    [Header("Choice Floor to Edit")]
    [SerializeField]
    private KeyCode k_Floor_Up = KeyCode.Home;

    [SerializeField]
    private KeyCode k_Floor_Down = KeyCode.End;

    /// <summary>
    /// Spawm Point (Set / Not Set)
    /// </summary>
    [Header("Spawm on Map")]
    [SerializeField]
    private KeyCode k_Spawm = KeyCode.Tab;

    [Header("File Map Control")]
    [SerializeField]
    private KeyCode k_Open_Normal = KeyCode.F5;

    [SerializeField]
    private KeyCode k_Open_Temp = KeyCode.F6;

    [SerializeField]
    private KeyCode k_New = KeyCode.F9;

    [SerializeField]
    private KeyCode k_Save = KeyCode.F12;

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
        cl_Single.Set_Isometric_Pos(new Class_Vector().Get_VectorInt(new Vector2(0, 0)));

        cl_MapManager_MapManager = g_MapManager.GetComponent<Isometric_MapManager>();
        cl_MapManager_Renderer = g_MapManager.GetComponent<Isometric_MapRenderer>();
        cl_MapManager_MapString = g_MapManager.GetComponent<Isometric_MapString>();

        cl_Vector = new Class_Vector();
        cl_Object = new Class_Object();

        lg_Spawm = new List<GameObject>();

        s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Ground(0).GetComponent<SpriteRenderer>().sprite;
        s_LayerChoice.text = "Ground [" + cl_MapManager_Renderer.Get_SingleCode_Ground(0) + "]";

        i_MapSize_X.text = cl_MapManager_MapString.Get_MapSize().x.ToString();
        i_MapSize_Y.text = cl_MapManager_MapString.Get_MapSize().y.ToString();

        Class_KeyCode cl_String = new Class_KeyCode();

        s_GuildKeyboard.text =
            "[Edit]\n" +
            "- Edit: '" + cl_String.Get_KeyCode_Simple(k_Edit) + "'\n" +
            "- Remove: '" + cl_String.Get_KeyCode_Simple(k_Remove) + "'\n" +
            "- Spawm: '" + cl_String.Get_KeyCode_Simple(k_Spawm) + "'\n" +
            "[Choice]\n" +
            "- Layer: '" + cl_String.Get_KeyCode_Simple(k_Layer_Next) + "' '" + cl_String.Get_KeyCode_Simple(k_Layer_Back) + "'\n" +
            "- Square: '" + cl_String.Get_KeyCode_Simple(k_Square_Next) + "' '" + cl_String.Get_KeyCode_Simple(k_Square_Back) + "'\n" +
            "- Floor: '" + cl_String.Get_KeyCode_Simple(k_Floor_Up) + "' '" + cl_String.Get_KeyCode_Simple(k_Floor_Down) + "'\n" +
            "[File]\n" +
            "- Open: '" + cl_String.Get_KeyCode_Simple(k_Open_Normal) + "'\n" +
            "- Open Temp: '" + cl_String.Get_KeyCode_Simple(k_Open_Temp) + "'\n" +
            "- New: '" + cl_String.Get_KeyCode_Simple(k_New) + "'\n" +
            "- Save: '" + cl_String.Get_KeyCode_Simple(k_Save) + "'\n";
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
        //Control Curson

        if (Input.GetKeyDown(k_Up))
        {
            Vector2Int v2_Current = new Class_Vector().Get_VectorInt(cl_Single.Get_Isometric_Pos());

            if (cl_MapManager_MapManager.Get_Check_InsideMap(v2_Current, new Class_Vector().v2_Isometric_DirUp))
            {
                cl_Single.Set_Isometric_Pos(new Vector2(v2_Current.x - 1, v2_Current.y));

                Button_Floor(new Class_Vector().Get_VectorInt(cl_Single.Get_Isometric_Pos()));
            }
        }

        if (Input.GetKeyDown(k_Down))
        {
            Vector2Int v2_Current = new Class_Vector().Get_VectorInt(cl_Single.Get_Isometric_Pos());

            if (cl_MapManager_MapManager.Get_Check_InsideMap(v2_Current, new Class_Vector().v2_Isometric_DirDown))
            {
                cl_Single.Set_Isometric_Pos(new Vector2(v2_Current.x + 1, v2_Current.y));

                Button_Floor(new Class_Vector().Get_VectorInt(cl_Single.Get_Isometric_Pos()));
            }
        }

        if (Input.GetKeyDown(k_Left))
        {
            Vector2Int v2_Current = new Class_Vector().Get_VectorInt(cl_Single.Get_Isometric_Pos());

            if (cl_MapManager_MapManager.Get_Check_InsideMap(v2_Current, new Class_Vector().v2_Isometric_DirLeft))
            {
                cl_Single.Set_Isometric_Pos(new Vector2(v2_Current.x, v2_Current.y - 1));

                Button_Floor(new Class_Vector().Get_VectorInt(cl_Single.Get_Isometric_Pos()));
            }
        }

        if (Input.GetKeyDown(k_Right))
        {
            Vector2Int v2_Current = new Class_Vector().Get_VectorInt(cl_Single.Get_Isometric_Pos());

            if (cl_MapManager_MapManager.Get_Check_InsideMap(v2_Current, new Class_Vector().v2_Isometric_DirRight))
            {
                cl_Single.Set_Isometric_Pos(new Vector2(v2_Current.x, v2_Current.y + 1));

                Button_Floor(new Class_Vector().Get_VectorInt(cl_Single.Get_Isometric_Pos()));
            }
        }

        //Square

        if (Input.GetKeyDown(k_Square_Next))
        {
            i_Choice++;
            Button_Square();
        }

        if (Input.GetKeyDown(k_Square_Back))
        {
            i_Choice--;
            Button_Square();
        }

        //Layer

        if (Input.GetKeyDown(k_Layer_Next))
        {
            i_Layer++;
            Button_Layer();
        }

        if (Input.GetKeyDown(k_Layer_Back))
        {
            i_Layer--;
            Button_Layer();
        }

        //Floor

        if (Input.GetKeyDown(k_Floor_Up))
        {
            Vector2Int v2_Current = new Class_Vector().Get_VectorInt(cl_Single.Get_Isometric_Pos());
            cl_MapManager_MapManager.Set_MaxtrixCode_Floor(
                v2_Current,
                cl_MapManager_MapManager.Get_MatrixCode_Floor_ToInt(v2_Current) + 1);

            Button_Floor(new Class_Vector().Get_VectorInt(cl_Single.Get_Isometric_Pos()));

            //Auto Save
            Set_MapCode_ToFile(true);
        }

        if (Input.GetKeyDown(k_Floor_Down))
        {
            Vector2Int v2_Current = new Class_Vector().Get_VectorInt(cl_Single.Get_Isometric_Pos());
            cl_MapManager_MapManager.Set_MaxtrixCode_Floor(
                v2_Current,
                cl_MapManager_MapManager.Get_MatrixCode_Floor_ToInt(v2_Current) - 1);

            Button_Floor(new Class_Vector().Get_VectorInt(cl_Single.Get_Isometric_Pos()));

            //Auto Save
            Set_MapCode_ToFile(true);
        }

        //Edit

        if (Input.GetKeyDown(k_Edit))
        {
            Button_Edit();
        }

        //Remove

        if (Input.GetKeyDown(k_Remove))
        {
            Button_Remove();
        }

        //Open

        if (Input.GetKeyDown(k_Open_Normal))
        {
            Set_MapCode_FromFile(false);

            Debug.Log("Set_ControlKeyboard: Open Map!");
        }

        if (Input.GetKeyDown(k_Open_Temp))
        {
            Set_MapCode_FromFile(true);

            Debug.Log("Set_ControlKeyboard: Open Temp Map!");
        }

        //New

        if (Input.GetKeyDown(k_New))
        {
            Set_MapGenerate_NewMap();

            Debug.Log("Set_ControlKeyboard: New Map!");
        }

        //Save

        if(Input.GetKeyDown(k_Save))
        {
            Set_MapCode_ToFile(false);

            Debug.Log("Set_ControlKeyboard: Save Map!");
        }

        //Spawm

        if (Input.GetKeyDown(k_Spawm))
        {
            Set_Debug_SpawmPoint_Chance();

            //Auto Save
            Set_MapCode_ToFile(true);
        }
    }

    #region Button Methode

    /// <summary>
    /// Button Square
    /// </summary>
    private void Button_Square()
    {
        switch (i_Layer)
        {
            case 0:
                //Ground
                if (i_Choice >= cl_MapManager_Renderer.Get_CountList_Ground())
                    i_Choice = 0;
                else
                if (i_Choice < 0)
                    i_Choice = cl_MapManager_Renderer.Get_CountList_Ground() - 1;

                s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Ground(i_Choice).GetComponent<SpriteRenderer>().sprite;
                s_LayerChoice.text = "Ground [" + cl_MapManager_Renderer.Get_SingleCode_Ground(i_Choice) + "]";
                break;
            case 1:
                //Object
                if (i_Choice >= cl_MapManager_Renderer.Get_CountList_Object())
                    i_Choice = 0;
                else
                if (i_Choice < 0)
                    i_Choice = cl_MapManager_Renderer.Get_CountList_Object() - 1;

                s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Object(i_Choice).GetComponent<SpriteRenderer>().sprite;
                s_LayerChoice.text = "Object [" + cl_MapManager_Renderer.Get_SingleCode_Object(i_Choice) + "]";
                break;
            case 2:
                //Fence U
                if (i_Choice >= cl_MapManager_Renderer.Get_CountList_Fence_U())
                    i_Choice = 0;
                else
                if (i_Choice < 0)
                    i_Choice = cl_MapManager_Renderer.Get_CountList_Fence_U() - 1;

                s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_U(i_Choice).GetComponent<SpriteRenderer>().sprite;
                s_LayerChoice.text = "Fence U [" + cl_MapManager_Renderer.Get_SingleCode_Fence_U(i_Choice) + "]";
                break;
            case 3:
                //Fence D
                if (i_Choice >= cl_MapManager_Renderer.Get_CountList_Fence_D())
                    i_Choice = 0;
                else
                if (i_Choice < 0)
                    i_Choice = cl_MapManager_Renderer.Get_CountList_Fence_D() - 1;

                s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_D(i_Choice).GetComponent<SpriteRenderer>().sprite;
                s_LayerChoice.text = "Fence D [" + cl_MapManager_Renderer.Get_SingleCode_Fence_D(i_Choice) + "]";
                break;
            case 4:
                //Fence L
                if (i_Choice >= cl_MapManager_Renderer.Get_CountList_Fence_L())
                    i_Choice = 0;
                else
                if (i_Choice < 0)
                    i_Choice = cl_MapManager_Renderer.Get_CountList_Fence_L() - 1;

                s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_L(i_Choice).GetComponent<SpriteRenderer>().sprite;
                s_LayerChoice.text = "Fence L [" + cl_MapManager_Renderer.Get_SingleCode_Fence_L(i_Choice) + "]";
                break;
            case 5:
                //Fence R
                if (i_Choice >= cl_MapManager_Renderer.Get_CountList_Fence_R())
                    i_Choice = 0;
                else
                if (i_Choice < 0)
                    i_Choice = cl_MapManager_Renderer.Get_CountList_Fence_R() - 1;

                s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_R(i_Choice).GetComponent<SpriteRenderer>().sprite;
                s_LayerChoice.text = "Fence R [" + cl_MapManager_Renderer.Get_SingleCode_Fence_Right(i_Choice) + "]";
                break;
        }
    }

    /// <summary>
    /// Button Layer
    /// </summary>
    private void Button_Layer()
    {
        if (i_Layer > 5)
            i_Layer = 0;
        else
            if (i_Layer < 0)
            i_Layer = 5;

        i_Choice = 0;

        switch (i_Layer)
        {
            case 0:
                //Ground
                if (cl_MapManager_Renderer.Get_CountList_Ground() > 0)
                {
                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Ground(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Ground [" + cl_MapManager_Renderer.Get_SingleCode_Ground(i_Choice) + "]";
                }
                else
                {
                    s_LayerChoice.text = "Ground [NONE]";
                }
                break;
            case 1:
                //Object
                if (cl_MapManager_Renderer.Get_CountList_Object() > 0)
                {
                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Object(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Object [" + cl_MapManager_Renderer.Get_SingleCode_Object(i_Choice) + "]";
                }
                else
                {
                    s_LayerChoice.text = "Object [NONE]";
                }
                break;
            case 2:
                //Fence U
                if (cl_MapManager_Renderer.Get_CountList_Fence_U() > 0)
                {
                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_U(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Fence U [" + cl_MapManager_Renderer.Get_SingleCode_Fence_U(i_Choice) + "]";
                }
                else
                {
                    s_LayerChoice.text = "Fence U [NONE]";
                }
                break;
            case 3:
                //Fence
                if (cl_MapManager_Renderer.Get_CountList_Fence_D() > 0)
                {
                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_D(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Fence D [" + cl_MapManager_Renderer.Get_SingleCode_Fence_D(i_Choice) + "]";
                }
                else
                {
                    s_LayerChoice.text = "Fence D [NONE]";
                }
                break;
            case 4:
                //Fence
                if (cl_MapManager_Renderer.Get_CountList_Fence_L() > 0)
                {
                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_L(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Fence L [" + cl_MapManager_Renderer.Get_SingleCode_Fence_L(i_Choice) + "]";
                }
                else
                {
                    s_LayerChoice.text = "Fence L [NONE]";
                }
                break;
            case 5:
                //Fence
                if (cl_MapManager_Renderer.Get_CountList_Fence_R() > 0)
                {
                    s_SpriteChoice.sprite = cl_MapManager_Renderer.Get_GameObject_Fence_R(i_Choice).GetComponent<SpriteRenderer>().sprite;
                    s_LayerChoice.text = "Fence R [" + cl_MapManager_Renderer.Get_SingleCode_Fence_Right(i_Choice) + "]";
                }
                else
                {
                    s_LayerChoice.text = "Fence R [NONE]";
                }
                break;
        }
    }

    /// <summary>
    /// Button Edit
    /// </summary>
    private void Button_Edit()
    {
        Vector2Int v2_Current = cl_Vector.Get_VectorInt(cl_Single.Get_Isometric_Pos());
        switch (i_Layer)
        {
            case 0:
                //Ground
                cl_MapManager_MapManager.Set_MatrixCode_Ground(
                    v2_Current, 
                    cl_MapManager_Renderer.Get_SingleCode_Ground(i_Choice),
                    cl_MapManager_MapManager.Get_MatrixCode_Floor(v2_Current));
                break;
            case 1:
                //Object
                cl_MapManager_MapManager.Set_MatrixCode_Object(
                    v2_Current, 
                    cl_MapManager_Renderer.Get_SingleCode_Object(i_Choice),
                    cl_MapManager_MapManager.Get_MatrixCode_Floor(v2_Current));
                break;
            case 2:
                //Fence
                cl_MapManager_MapManager.Set_MatrixCode_Fence_U(
                    v2_Current, 
                    cl_MapManager_Renderer.Get_SingleCode_Fence_U(i_Choice),
                    cl_MapManager_MapManager.Get_MatrixCode_Floor(v2_Current));
                break;
            case 3:
                //Fence
                cl_MapManager_MapManager.Set_MatrixCode_Fence_D(
                    v2_Current,
                    cl_MapManager_Renderer.Get_SingleCode_Fence_D(i_Choice),
                    cl_MapManager_MapManager.Get_MatrixCode_Floor(v2_Current));
                break;
            case 4:
                //Fence
                cl_MapManager_MapManager.Set_MatrixCode_Fence_L(
                    v2_Current, 
                    cl_MapManager_Renderer.Get_SingleCode_Fence_L(i_Choice),
                    cl_MapManager_MapManager.Get_MatrixCode_Floor(v2_Current));
                break;
            case 5:
                //Fence
                cl_MapManager_MapManager.Set_MatrixCode_Fence_R(
                    v2_Current, 
                    cl_MapManager_Renderer.Get_SingleCode_Fence_Right(i_Choice),
                    cl_MapManager_MapManager.Get_MatrixCode_Floor(v2_Current));
                break;
        }

        //Auto Save
        Set_MapCode_ToFile(true);
    }

    /// <summary>
    /// Button Remove
    /// </summary>
    private void Button_Remove()
    {
        Vector2Int v2_Current = cl_Vector.Get_VectorInt(cl_Single.Get_Isometric_Pos());
        switch (i_Layer)
        {
            case 0:
                //Ground
                cl_MapManager_MapManager.Set_MatrixCode_Ground(
                    v2_Current, 
                    cl_MapManager_Renderer.Get_EmtyCode(),
                    cl_MapManager_MapManager.Get_MatrixCode_Floor(v2_Current));
                break;
            case 1:
                //Object
                cl_MapManager_MapManager.Set_MatrixCode_Object(
                    v2_Current, 
                    cl_MapManager_Renderer.Get_EmtyCode(),
                    cl_MapManager_MapManager.Get_MatrixCode_Floor(v2_Current));
                break;
            case 2:
                //Fence
                cl_MapManager_MapManager.Set_MatrixCode_Fence_U(
                    v2_Current, 
                    cl_MapManager_Renderer.Get_EmtyCode(),
                    cl_MapManager_MapManager.Get_MatrixCode_Floor(v2_Current));
                break;
            case 3:
                //Fence
                cl_MapManager_MapManager.Set_MatrixCode_Fence_D(
                    v2_Current, 
                    cl_MapManager_Renderer.Get_EmtyCode(),
                    cl_MapManager_MapManager.Get_MatrixCode_Floor(v2_Current));
                break;
            case 4:
                //Fence
                cl_MapManager_MapManager.Set_MatrixCode_Fence_L(
                    v2_Current, 
                    cl_MapManager_Renderer.Get_EmtyCode(),
                    cl_MapManager_MapManager.Get_MatrixCode_Floor(v2_Current));
                break;
            case 5:
                //Fence
                cl_MapManager_MapManager.Set_MatrixCode_Fence_R(
                    v2_Current, 
                    cl_MapManager_Renderer.Get_EmtyCode(),
                    cl_MapManager_MapManager.Get_MatrixCode_Floor(v2_Current));
                break;
        }

        //Auto Save
        Set_MapCode_ToFile(true);
    }

    private void Button_Floor(Vector2Int v2_Pos)
    {
        cl_Single.Set_Isometric_Floor(cl_MapManager_MapManager.Get_MatrixCode_Floor_ToInt(v2_Pos));
    }

    #endregion

    #region Public (Button for Generate)

    /// <summary>
    /// Generate new Emty Map and Save to File
    /// </summary>
    public void Set_MapGenerate_NewMap()
    {
        cl_Single.Set_Isometric_Pos(new Vector2(0, 0));

        Set_Debug_SpawmPoint_Remove();
        cl_MapManager_MapManager.Set_Map_Remove();
        cl_MapManager_MapString.Set_MapCode_ClearAll();

        cl_MapManager_MapString.Set_MapSize(new Vector2Int(
            int.Parse(i_MapSize_X.text),
            int.Parse(i_MapSize_Y.text)));

        cl_MapManager_MapManager.Set_Map_Generate(true);
    }

    #endregion

    #region Public (Set Matrix Map Code from File)

    /// <summary>
    /// Set MAP CODE Generating from GROUND, OBJECT and FENCE CODE from File
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="b_TempFile">Temp File is a copy of current File during Edit</param>
    private void Set_MapCode_FromFile(bool b_TempFile)
    {
        Class_FileIO cl_File = new Class_FileIO();
        string s_File = "C:\\Users\\Admin\\Documents\\" + s_MapName + ".isomap";

        if (b_TempFile)
        {
            s_File = "C:\\Users\\Admin\\Documents\\" + s_MapName + "_Temp.isomap";
        }

        Set_Debug_SpawmPoint_Remove();
        cl_MapManager_MapManager.Set_Map_Remove();
        cl_MapManager_MapString.Set_MapCode_ClearAll();

        if (!cl_File.Get_FileExist(s_File))
        {
            Debug.LogWarning("Set_MapCode_FromFile: File Map not Exist!");
            return;
        }

        cl_File.Set_Act_Read_Clear();
        cl_File.Set_Act_Read_Start(s_File);

        //Size
        int i_MapXCount = cl_File.Get_Read_Auto_Int();
        int i_MapYCount = cl_File.Get_Read_Auto_Int();

        i_MapSize_X.text = i_MapXCount.ToString();
        i_MapSize_Y.text = i_MapYCount.ToString();

        cl_MapManager_MapString.Set_MapSize(new Vector2Int(i_MapXCount, i_MapYCount));

        //Primary
        cl_MapManager_MapString.Set_MapCode_Ground(cl_File.Get_Read_Auto_String());
        cl_MapManager_MapString.Set_MapCode_Object(cl_File.Get_Read_Auto_String());

        //Fence
        cl_MapManager_MapString.Set_MapCode_Fence_U(cl_File.Get_Read_Auto_String());
        cl_MapManager_MapString.Set_MapCode_Fence_D(cl_File.Get_Read_Auto_String());
        cl_MapManager_MapString.Set_MapCode_Fence_L(cl_File.Get_Read_Auto_String());
        cl_MapManager_MapString.Set_MapCode_Fence_R(cl_File.Get_Read_Auto_String());

        //Floor
        cl_MapManager_MapString.Set_MapCode_Floor(cl_File.Get_Read_Auto_String());

        cl_MapManager_MapString.Set_List_SpawmPoint_Reset();
        int i_SpawmPoint = cl_File.Get_Read_Auto_Int();
        for (int i = 0; i < i_SpawmPoint; i++)
        {
            int i_x = cl_File.Get_Read_Auto_Int();
            int i_y = cl_File.Get_Read_Auto_Int();
            cl_MapManager_MapString.Set_List_SpawmPoint_Add(new Vector2Int(i_x, i_y));
        }
        Set_Debug_SpawmPoint_New();

        cl_MapManager_MapManager.Set_Map_Generate(false);
    }

    private void Set_MapCode_ToFile(bool b_TempFile)
    {
        cl_MapManager_MapManager.Set_MapCode_FromMapCodeMatrix();

        Class_FileIO cl_File = new Class_FileIO();
        string s_File = "C:\\Users\\Admin\\Documents\\" + s_MapName + ".isomap";

        if (b_TempFile)
        {
            s_File = "C:\\Users\\Admin\\Documents\\" + s_MapName + "_Temp.isomap";
        }

        cl_File.Set_Act_Write_Clear();

        //Size
        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_MapSize().x);
        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_MapSize().y);

        //Primary
        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_MapCode_Ground());
        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_MapCode_Object());

        //Fence
        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_MapCode_Fence_U());
        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_MapCode_Fence_D());
        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_MapCode_Fence_L());
        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_MapCode_Fence_R());

        //Floor
        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_MapCode_Floor());

        cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_List_SpawmPoint().Count);
        for (int i = 0; i < cl_MapManager_MapString.Get_List_SpawmPoint().Count; i++)
        {
            cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_List_SpawmPoint()[i].x);
            cl_File.Set_Act_Write_Add(cl_MapManager_MapString.Get_List_SpawmPoint()[i].y);
        }

        cl_File.Set_Act_Write_Start(s_File);
    }

    #endregion

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
            cl_Object.Set_Destroy_GameObject(lg_Spawm[i]);
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
            if (cl_Single.Get_Isometric_Pos() == cl_MapManager_MapString.Get_List_SpawmPoint()[i])
            {
                cl_Object.Set_Destroy_GameObject(lg_Spawm[i]);
                cl_MapManager_MapString.Set_List_SpawmPoint_Remove(i);
                lg_Spawm.RemoveAt(i);
                return;
            }
        }
        Set_Debug_SpawmPoint_Prefab(new Vector2Int((int)cl_Single.Get_Isometric_Pos().x, (int)cl_Single.Get_Isometric_Pos().y));
        cl_MapManager_MapString.Set_List_SpawmPoint_Add(
            new Vector2Int((int)cl_Single.Get_Isometric_Pos().x, (int)cl_Single.Get_Isometric_Pos().y));
    }

    /// <summary>
    /// Create Spawm Point Prefab
    /// </summary>
    /// <param name="v2_Pos"></param>
    private void Set_Debug_SpawmPoint_Prefab(Vector2Int v2_Pos)
    {
        GameObject g_SpawmPoint = cl_Object.Set_Prepab_Create(this.g_SpawmPoint, g_MapManager.transform);

        g_SpawmPoint.GetComponent<Isometric_Single>().Set_Isometric_Pos(v2_Pos);
        g_SpawmPoint.GetComponent<Isometric_Single>().Set_Isometric_Offset(cl_MapManager_MapManager.Get_Offset());
        g_SpawmPoint.GetComponent<Isometric_Single>().Set_onGround(true);
        g_SpawmPoint.SetActive(true);

        lg_Spawm.Add(g_SpawmPoint);
    }

    #endregion
}
