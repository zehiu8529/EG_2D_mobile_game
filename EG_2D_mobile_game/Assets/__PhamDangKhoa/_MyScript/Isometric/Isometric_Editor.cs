using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Add this Script on "Curson Object" to Edit Map on Object with "Isometric_Map.cs"
/// </summary>
/// <remarks>
/// Use with Camera set 'Orthographic'
/// </remarks>
[RequireComponent(typeof(Isometric_Single))]
public class Isometric_Editor : MonoBehaviour
{
    //Notice: Just use this Script when in Scene for Map Editor by Developer

    #region Public Varible

    [Header("Isometric Map Tag")]
    [SerializeField]
    private string s_Tag = "IsometricMap";

    /// <summary>
    /// Map
    /// </summary>
    [SerializeField]
    private GameObject g_Map;

    /// <summary>
    /// Current Map code
    /// </summary>
    [Header("Saved Data")]
    [SerializeField]
    private string s_MapSave;

    /// <summary>
    /// Control Pos
    /// </summary>
    [Header("Control")]
    [SerializeField]
    private KeyCode k_Up = KeyCode.UpArrow;

    /// <summary>
    /// Control Pos
    /// </summary>
    [SerializeField]
    private KeyCode k_Down = KeyCode.DownArrow;

    /// <summary>
    /// Control Pos
    /// </summary>
    [SerializeField]
    private KeyCode k_Left = KeyCode.LeftArrow;

    /// <summary>
    /// Control Pos
    /// </summary>
    [SerializeField]
    private KeyCode k_Right = KeyCode.RightArrow;

    /// <summary>
    /// Update Current Pos
    /// </summary>
    [SerializeField]
    private KeyCode k_Edit = KeyCode.E;

    /// <summary>
    /// Delete Current Pos
    /// </summary>
    [SerializeField]
    private KeyCode k_Remove = KeyCode.R;

    /// <summary>
    /// Chance Current Mode (Between Ground and Object)
    /// </summary>
    [SerializeField]
    private KeyCode k_Mode = KeyCode.T;

    /// <summary>
    /// Chance Current Edit (Ground or Object)
    /// </summary>
    [SerializeField]
    private KeyCode k_Chance = KeyCode.Tab;

    /// <summary>
    /// Spawm Point (Set / Not Set)
    /// </summary>
    [SerializeField]
    private KeyCode k_Spawm = KeyCode.Q;

    /// <summary>
    /// Show Current Ground or Object ready to Add
    /// </summary>
    [Header("UI")]
    [SerializeField]
    private SpriteRenderer s_Current;

    /// <summary>
    /// Show Mode Current for Ground or Object
    /// </summary>
    [SerializeField]
    private Text t_Mode;

    /// <summary>
    /// Spawm Point Debug
    /// </summary>
    [Header("Debug")]
    [SerializeField]
    private GameObject g_SpawmPoint;

    #endregion

    #region Private Varible

    private Isometric_Map cl_Map;

    /// <summary>
    /// Class Working on Object
    /// </summary>
    private Class_Object cl_Object;

    /// <summary>
    /// Console
    /// </summary>
    private Isometric_Single cl_Single;

    /// <summary>
    /// Class Working on String
    /// </summary>
    private Class_Vector cl_Vector;

    /// <summary>
    /// Current Choice
    /// </summary>
    private int i_Choice = 0;

    /// <summary>
    /// Current Mode
    /// </summary>
    private int i_Mode = 0;

    /// <summary>
    /// Spawm Point List Object Debug
    /// </summary>
    private List<GameObject> lg_Spawm;

    #endregion

    private void Start()
    {
        if (g_Map == null)
        {
            if (s_Tag != "")
            {
                g_Map = GameObject.FindGameObjectWithTag(s_Tag);

                if (g_Map == null)
                {
                    Debug.LogError("Not found 'Isometric Map Object' with tag: " + s_Tag);
                }
            }
        }

        cl_Map = g_Map.GetComponent<Isometric_Map>();

        cl_Vector = new Class_Vector();
        cl_Object = new Class_Object();

        cl_Single = GetComponent<Isometric_Single>();
        cl_Single.Set_isObject(true);
        cl_Single.Set_Pos(new Vector2());

        s_Current.sprite = cl_Map.Get_Sprite_GroundList(i_Choice);

        lg_Spawm = new List<GameObject>();
    }

    private void Update()
    {
        Set_ControlCurson();
    }

    #region Control Editor

    /// <summary>
    /// This Object is Curson to Control
    /// </summary>
    private void Set_ControlCurson()
    {
        this.GetComponent<Isometric_Single>().Set_Offset(cl_Map.Get_Offset());

        //Control Curson

        if (Input.GetKeyDown(k_Up))
        {
            Vector2 v2_Current = cl_Single.Get_Pos();

            if (cl_Map.Get_Check_InsideMap(cl_Vector.Get_VectorInt(v2_Current), cl_Map.v2_DirUp))
            {
                cl_Single.Set_Pos(new Vector2(v2_Current.x - 1, v2_Current.y));
            }
        }

        if (Input.GetKeyDown(k_Down))
        {
            Vector2 v2_Current = cl_Single.Get_Pos();

            if (cl_Map.Get_Check_InsideMap(cl_Vector.Get_VectorInt(v2_Current), cl_Map.v2_DirDown))
            {
                cl_Single.Set_Pos(new Vector2(v2_Current.x + 1, v2_Current.y));
            }
        }

        if (Input.GetKeyDown(k_Left))
        {
            Vector2 v2_Current = cl_Single.Get_Pos();

            if (cl_Map.Get_Check_InsideMap(cl_Vector.Get_VectorInt(v2_Current), cl_Map.v2_DirLeft))
            {
                cl_Single.Set_Pos(new Vector2(v2_Current.x, v2_Current.y - 1));
            }
        }

        if (Input.GetKeyDown(k_Right))
        {
            Vector2 v2_Current = cl_Single.Get_Pos();

            if (cl_Map.Get_Check_InsideMap(cl_Vector.Get_VectorInt(v2_Current), cl_Map.v2_DirRight))
            {
                cl_Single.Set_Pos(new Vector2(v2_Current.x, v2_Current.y + 1));
            }
        }

        //Chance

        if (Input.GetKeyDown(k_Chance))
        {
            switch (i_Mode)
            {
                case 0:
                    //Ground
                    i_Choice++;
                    if (i_Choice >= cl_Map.Get_Count_GroundList())
                        i_Choice = 0;

                    s_Current.sprite = cl_Map.Get_Sprite_GroundList(i_Choice);
                    break;
                case 1:
                    //Object
                    i_Choice++;
                    if (i_Choice >= cl_Map.Get_Count_ObjectList())
                        i_Choice = 0;

                    s_Current.sprite = cl_Map.Get_Sprite_ObjectList(i_Choice);
                    break;
                case 2:
                    //Fence
                    i_Choice++;
                    if (i_Choice >= cl_Map.Get_Count_FenceList())
                        i_Choice = 0;

                    s_Current.sprite = cl_Map.Get_Sprite_FenceList(i_Choice);
                    break;
            }
        }

        Set_NumberCurson();

        //Mode

        if (Input.GetKeyDown(k_Mode))
        {
            i_Mode++;

            if (i_Mode > 2)
                i_Mode = 0;

            i_Choice = 0;

            switch (i_Mode)
            {
                case 0:
                    //Ground
                    s_Current.sprite = cl_Map.Get_Sprite_GroundList(i_Choice);
                    t_Mode.text = "Ground";
                    break;
                case 1:
                    //Object
                    s_Current.sprite = cl_Map.Get_Sprite_ObjectList(i_Choice);
                    t_Mode.text = "Object";
                    break;
                case 2:
                    //Fence
                    s_Current.sprite = cl_Map.Get_Sprite_FenceList(i_Choice);
                    t_Mode.text = "Fence";
                    break;
            }
        }

        //Edit

        if (Input.GetKeyDown(k_Edit))
        {
            Vector2Int v2_Current = cl_Vector.Get_VectorInt(cl_Single.Get_Pos());
            switch (i_Mode)
            {
                case 0:
                    //Ground
                    cl_Map.Set_Map_Ground_Chance(v2_Current, i_Choice);
                    break;
                case 1:
                    //Object
                    cl_Map.Set_Map_Object_Chance(v2_Current, i_Choice);
                    break;
                case 2:
                    //Fence
                    cl_Map.Set_Map_Fence_Add(v2_Current, cl_Map.Get_Code_Fence(i_Choice));
                    break;
            }
            s_MapSave = cl_Map.Get_Code_Map();
        }

        //Remove

        if (Input.GetKeyDown(k_Remove))
        {
            Vector2Int v2_Current = cl_Vector.Get_VectorInt(cl_Single.Get_Pos());
            switch (i_Mode)
            {
                case 0:
                    //Ground
                    cl_Map.Set_Map_Ground_Emty(v2_Current);
                    break;
                case 1:
                    //Object
                    cl_Map.Set_Map_Object_Emty(v2_Current);
                    break;
                case 2:
                    //Fence
                    cl_Map.Set_Map_Fence_Remove(v2_Current, cl_Map.Get_Code_Fence(i_Choice));
                    break;
            }
            s_MapSave = cl_Map.Get_Code_Map();
        }

        //Spawm

        if (Input.GetKeyDown(k_Spawm))
        {
            int i_Removed_Index = cl_Map.Set_SpawmChance_Removed(cl_Vector.Get_VectorInt(cl_Single.Get_Pos()));
            if (i_Removed_Index != -1)
            {
                cl_Object.Set_Destroy_GameObject(lg_Spawm[i_Removed_Index]);
                lg_Spawm.RemoveAt(i_Removed_Index);
            }
            else
            {
                Set_SpawmDebug();
            }
            s_MapSave = cl_Map.Get_Code_Map();
        }
    }

    /// <summary>
    /// Number Curson
    /// </summary>
    private void Set_NumberCurson()
    {
        //Number

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Set_NumberCurson(0);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Set_NumberCurson(1);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Set_NumberCurson(2);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Set_NumberCurson(3);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Set_NumberCurson(4);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Set_NumberCurson(5);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Set_NumberCurson(6);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Set_NumberCurson(7);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Set_NumberCurson(8);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Set_NumberCurson(9);
        }
    }

    /// <summary>
    /// Number Choice
    /// </summary>
    /// <param name="i_Num"></param>
    private void Set_NumberCurson(int i_Num)
    {
        switch (i_Mode)
        {
            case 0:
                if (i_Num < cl_Map.Get_Count_GroundList())
                {
                    s_Current.sprite = cl_Map.Get_Sprite_GroundList(i_Num);
                    i_Choice = i_Num;
                }
                break;
            case 1:
                if (i_Num < cl_Map.Get_Count_ObjectList())
                {
                    s_Current.sprite = cl_Map.Get_Sprite_ObjectList(i_Num);
                    i_Choice = i_Num;
                }
                break;
        }
    }

    #endregion

    #region Spawm Debug Manager

    /// <summary>
    /// Control Set / Destroy Spawm Point
    /// </summary>
    private void Set_SpawmDebug()
    {
        GameObject g_Spawm = cl_Object.Set_Prepab_Create(g_SpawmPoint, g_Map.transform);
        g_Spawm.GetComponent<Isometric_Single>().Set_Pos(cl_Vector.Get_VectorInt(cl_Single.Get_Pos()));
        g_Spawm.GetComponent<Isometric_Single>().Set_Offset(cl_Map.Get_Offset());
        g_Spawm.GetComponent<Isometric_Single>().Set_isObject(true);
        lg_Spawm.Add(g_Spawm);
    }

    /// <summary>
    /// Use in "Isometric_Map.cs" to Add Spawm Point when "Set_Map()"
    /// </summary>
    /// <param name="v2_Pos"></param>
    public void Set_SpawmDebug_Add(Vector2Int v2_Pos)
    {
        GameObject g_Spawm = cl_Object.Set_Prepab_Create(g_SpawmPoint, g_Map.transform);
        g_Spawm.GetComponent<Isometric_Single>().Set_Pos(cl_Vector.Get_VectorInt(v2_Pos));
        g_Spawm.GetComponent<Isometric_Single>().Set_Offset(cl_Map.Get_Offset());
        g_Spawm.GetComponent<Isometric_Single>().Set_isObject(true);
        lg_Spawm.Add(g_Spawm);
    }

    /// <summary>
    /// Use in "Isometric_Map.cs" to Destroy Spawm Point before "Set_Map()"
    /// </summary>
    /// <param name="i_Removed_Index"></param>
    public void Set_SpawmDebug_Destroy(int i_Removed_Index)
    {
        cl_Object.Set_Destroy_GameObject(lg_Spawm[i_Removed_Index]);
        lg_Spawm.RemoveAt(i_Removed_Index);
    }

    /// <summary>
    /// Use in "Isometric_Map.cs" to Destroy ALL Spawm Point before "Set_Map()"
    /// </summary>
    public void Set_SpawmDebug_Destroy()
    {
        for (int i = 0; i < lg_Spawm.Count; i++)
        {
            Set_SpawmDebug_Destroy(i);
        }
    }

    #endregion
}
