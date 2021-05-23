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
    /// Isomtric Map X Length Dir UP and DOWN
    /// </summary>
    [Header("Size Map Code")]
    [SerializeField]
    private int i_Map_X = 9;

    /// <summary>
    /// Isomtric Map Y Length Dir LEFT and RIGHT
    /// </summary>
    [SerializeField]
    private int i_Map_Y = 9;

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
    private List<List<char>> l2_Map_Fence_UpCode;

    /// <summary>
    /// Matrix of Fence DOWN Code
    /// </summary>
    private List<List<char>> l2_Map_Fence_DownCode;

    /// <summary>
    /// Matrix of Fence LEFT Code
    /// </summary>
    private List<List<char>> l2_Map_Fence_LeftCode;

    /// <summary>
    /// Matrix of FENCE RIGHT Code
    /// </summary>
    private List<List<char>> l2_Map_Fence_RightCode;

    //List GameObject

    //...

    #endregion

    private void Start()
    {
        cl_MapString = GetComponent<Isometric_MapString>();

        cl_MapRenderer = GetComponent<Isometric_MapRenderer>();
    }

    #region Map Manager

    /// <summary>
    /// Set Size of MATRIX MAP CODE
    /// </summary>
    /// <param name="i_MapXCount"></param>
    /// <param name="i_MapYCount"></param>
    public void Set_MapCodeMatrix_Size(int i_MapXCount, int i_MapYCount)
    {
        this.i_Map_X = i_MapXCount;
        this.i_Map_Y = i_MapYCount;
    }

    //Public (Set Map Code from Current Map Code Matrix)

    /// <summary>
    /// Set MAP CODE of GROUND, OBJECT and FENCE from MAP CODE MATRIX to Get
    /// </summary>
    public void Set_MapCode_FromMapCodeMatrix()
    {
        Set_MapCodeMatrix_ToMapCodeGround();
        Get_MapCodeMatrix_ToMapCodeObject();
        Get_MapCodeMatrix_ToMapCodeFence_Up();
        Get_MapCodeMatrix_ToMapCodeFence_Down();
        Get_MapCodeMatrix_ToMapCodeFence_Left();
        Get_MapCodeMatrix_ToMapCodeFence_Right();
    }

    //Public (Set Map Code Matrix from Map Code)

    /// <summary>
    /// Set MAP CODE Generating from GROUND, OBJECT and FENCE CODE
    /// </summary>
    /// <remarks>
    /// Map Code Exist in current
    /// </remarks>
    public void Set_MapCodeMatrix_FromMapCode()
    {
        Set_MapCodeMatrix_ToEmty();
        Set_MapCodeMatrix_FromMapCodeGround();
        Set_MapCodeMatrix_FromMapCodeObject();
        Set_MapCodeMatrix_FromMapCodeFence_Up();
        Set_MapCodeMatrix_FromMapCodeFence_Down();
        Set_MapCodeMatrix_FromMapCodeFence_Left();
        Set_MapCodeMatrix_FromMapCodeFence_Right();
    }

    /// <summary>
    /// Set MAP CODE Generating from GROUND, OBJECT and FENCE CODE to Generating MATRIX MAP CODE
    /// </summary>
    /// <param name="s_MapGroundCode"></param>
    /// <param name="s_MapObjectCode"></param>
    /// <param name="s_MapFenceUpCode"></param>
    /// <param name="s_MapFenceDownCode"></param>
    /// <param name="s_MapFenceLeftCode"></param>
    /// <param name="s_MapFenceRightCode"></param>
    public void Set_MapCodeMatrix_FromMapCode(string s_MapGroundCode, string s_MapObjectCode, string s_MapFenceUpCode, string s_MapFenceDownCode, string s_MapFenceLeftCode, string s_MapFenceRightCode)
    {
        cl_MapString.Set_MapCode_Ground(s_MapGroundCode);
        cl_MapString.Set_MapCode_Object(s_MapObjectCode);
        cl_MapString.Set_MapCode_Fence_Up(s_MapFenceUpCode);
        cl_MapString.Set_MapCode_Fence_Down(s_MapFenceDownCode);
        cl_MapString.Set_MapCode_Fence_Left(s_MapFenceLeftCode);
        cl_MapString.Set_MapCode_Fence_Right(s_MapFenceRightCode);

        Set_MapCodeMatrix_FromMapCode();
    }

    /// <summary>
    /// Set MAP CODE Generating from GROUND, OBJECT and FENCE CODE to Generating MATRIX MAP CODE
    /// </summary>
    /// <param name="cl_MapCode"></param>
    public void Set_MapCodeMatrix_FromMapCode(Isometric_MapString cl_MapCode)
    {
        cl_MapString.Set_MapCode_Ground(cl_MapCode.Get_MapCode_Ground());
        cl_MapString.Set_MapCode_Object(cl_MapCode.Get_MapCode_Object());
        cl_MapString.Set_MapCode_Fence_Up(cl_MapCode.Get_MapCode_Fence_Up());
        cl_MapString.Set_MapCode_Fence_Down(cl_MapCode.Get_MapCode_Fence_Down());
        cl_MapString.Set_MapCode_Fence_Left(cl_MapCode.Get_MapCode_Fence_Left());
        cl_MapString.Set_MapCode_Fence_Right(cl_MapCode.Get_MapCode_Fence_Right());

        Set_MapCodeMatrix_FromMapCode();
    }

    /// <summary>
    /// Set MAP CODE Generating from GROUND, OBJECT and FENCE CODE to Generating MATRIX MAP CODE
    /// </summary>
    /// <remarks>
    /// Map Code Exist in File
    /// </remarks>
    /// <param name="s_MapCodeFileName"></param>
    public void Set_MapCodeMatrix_FromFile_Android(string s_MapCodeFileName)
    {
        Class_FileIO cl_File = new Class_FileIO();
        string s_File = cl_File.Get_FileLink_Application_PersistentDataPath(s_MapCodeFileName, "", ".isomap");

        cl_File.Set_Act_Write_Clear();
        cl_File.Set_Act_Read_Start(s_File);

        int i_MapXCount = cl_File.Get_Read_Auto_Int();
        int i_MapYCount = cl_File.Get_Read_Auto_Int();
        Set_MapCodeMatrix_Size(i_MapXCount, i_MapYCount);

        cl_MapString.Set_MapCode_Ground(cl_File.Get_Read_Auto_String());
        cl_MapString.Set_MapCode_Object(cl_File.Get_Read_Auto_String());
        cl_MapString.Set_MapCode_Fence_Up(cl_File.Get_Read_Auto_String());
        cl_MapString.Set_MapCode_Fence_Down(cl_File.Get_Read_Auto_String());
        cl_MapString.Set_MapCode_Fence_Left(cl_File.Get_Read_Auto_String());
        cl_MapString.Set_MapCode_Fence_Right(cl_File.Get_Read_Auto_String());

        Set_MapCodeMatrix_FromMapCode();
    }

    //Private

    /// <summary>
    /// Set MATRIX MAP CODE Size BEFORE Generating MAP
    /// </summary>
    /// <remarks>
    /// CLEAR ALL or CREATE NEW the Map Code
    /// </remarks>
    /// <param name="i_Map_X">Dir UP and DOWN</param>
    /// <param name="i_Map_Y">Dir LEFT and RIGHT</param>
    private void Set_MapCodeMatrix_ToEmty()
    {
        Set_Map_Emty();

        for (int i_Y = 0; i_Y < this.i_Map_Y; i_Y++)
        {
            l2_Map_GroundCode.Add(new List<char>());
            l2_Map_ObjectCode.Add(new List<char>());
            l2_Map_Fence_UpCode.Add(new List<char>());
            l2_Map_Fence_DownCode.Add(new List<char>());
            l2_Map_Fence_LeftCode.Add(new List<char>());
            l2_Map_Fence_RightCode.Add(new List<char>());

            for (int i_X = 0; i_X < this.i_Map_X; i_X++)
            {
                l2_Map_GroundCode[i_Y].Add(cl_MapRenderer.Get_EmtyCode());
                l2_Map_ObjectCode[i_Y].Add(cl_MapRenderer.Get_EmtyCode());
                l2_Map_Fence_UpCode[i_Y].Add(cl_MapRenderer.Get_EmtyCode());
                l2_Map_Fence_DownCode[i_Y].Add(cl_MapRenderer.Get_EmtyCode());
                l2_Map_Fence_LeftCode[i_Y].Add(cl_MapRenderer.Get_EmtyCode());
                l2_Map_Fence_RightCode[i_Y].Add(cl_MapRenderer.Get_EmtyCode());
            }
        }
    }

    /// <summary>
    /// CLEAR ALL or CREATE NEW the Map Code
    /// </summary>
    private void Set_Map_Emty()
    {
        l2_Map_GroundCode = new List<List<char>>();
        l2_Map_ObjectCode = new List<List<char>>();
        l2_Map_Fence_UpCode = new List<List<char>>();
        l2_Map_Fence_DownCode = new List<List<char>>();
        l2_Map_Fence_LeftCode = new List<List<char>>();
        l2_Map_Fence_RightCode = new List<List<char>>();

        cl_MapString.Set_MapCode_Ground("");
        cl_MapString.Set_MapCode_Object("");
        cl_MapString.Set_MapCode_Fence_Up("");
        cl_MapString.Set_MapCode_Fence_Down("");
        cl_MapString.Set_MapCode_Fence_Left("");
        cl_MapString.Set_MapCode_Fence_Right("");
    }

    #endregion

    //Code

    #region Ground Code Manager

    //Public (Map Code Matrix)

    /// <summary>
    /// Set Code to a SQUARE of GROUND Map Code
    /// </summary>
    /// <param name="i_X">Dir UP and DOWN</param>
    /// <param name="i_Y">Dir LEFT and RIGHT</param>
    /// <param name="c_Code"></param>
    public void Set_MapCodeMatrix_Ground(int i_X, int i_Y, char c_Code)
    {
        l2_Map_GroundCode[i_X][i_Y] = c_Code;
    }

    /// <summary>
    /// Get Code from a SQUARE of GROUND Map Code
    /// </summary>
    /// <param name="i_X">Dir UP and DOWN</param>
    /// <param name="i_Y">Dir LEFT and RIGHT</param>
    /// <returns></returns>
    public char Get_MapCodeMatrix_Ground(int i_X, int i_Y)
    {
        return l2_Map_GroundCode[i_X][i_Y];
    }

    //Private

    /// <summary>
    /// Get MAP CODE of GROUND
    /// </summary>
    private void Set_MapCodeMatrix_ToMapCodeGround()
    {
        string s_Map_Ground = "";
        for (int i_Y = 0; i_Y < this.i_Map_Y; i_Y++)
        {
            for (int i_X = 0; i_X < this.i_Map_X; i_X++)
            {
                s_Map_Ground += l2_Map_GroundCode[i_Y][i_X];
            }
        }
        cl_MapString.Set_MapCode_Ground(s_Map_Ground);
    }

    /// <summary>
    /// Set MAP GROUND CODE from GROUND CODE
    /// </summary>
    private void Set_MapCodeMatrix_FromMapCodeGround()
    {
        int i_CodeIndex = -1;
        for (int i_Y = 0; i_Y < this.i_Map_Y; i_Y++)
        {
            for (int i_X = 0; i_X < this.i_Map_X; i_X++)
            {
                i_CodeIndex++;
                l2_Map_GroundCode[i_Y][i_X] = cl_MapString.Get_MapCode_Ground()[i_CodeIndex];
            }
        }
    }

    #endregion

    #region Object Code Manager

    //Public (Map Code Matrix)

    /// <summary>
    /// Set Code to a SQUARE of OBJECT Map Code
    /// </summary>
    /// <param name="i_X">Dir UP and DOWN</param>
    /// <param name="i_Y">Dir LEFT and RIGHT</param>
    /// <param name="c_Code"></param>
    public void Set_MapCodeMatrix_Object(int i_X, int i_Y, char c_Code)
    {
        l2_Map_ObjectCode[i_X][i_Y] = c_Code;
    }

    /// <summary>
    /// Get Code from a SQUARE of OBJECT Map Code
    /// </summary>
    /// <param name="i_X">Dir UP and DOWN</param>
    /// <param name="i_Y">Dir LEFT and RIGHT</param>
    /// <returns></returns>
    public char Get_MapCodeMatrix_Object(int i_X, int i_Y)
    {
        return l2_Map_ObjectCode[i_X][i_Y];
    }

    //Private

    /// <summary>
    /// Get MAP CODE of OBJECT
    /// </summary>
    private void Get_MapCodeMatrix_ToMapCodeObject()
    {
        string s_Map_Object = "";
        for (int i_Y = 0; i_Y < this.i_Map_Y; i_Y++)
        {
            for (int i_X = 0; i_X < this.i_Map_X; i_X++)
            {
                s_Map_Object += l2_Map_ObjectCode[i_Y][i_X];
            }
        }
        cl_MapString.Set_MapCode_Object(s_Map_Object);
    }

    /// <summary>
    /// Set MAP OBJECT CODE from OBJECT CODE
    /// </summary>
    private void Set_MapCodeMatrix_FromMapCodeObject()
    {
        int i_CodeIndex = -1;
        for (int i_Y = 0; i_Y < this.i_Map_Y; i_Y++)
        {
            for (int i_X = 0; i_X < this.i_Map_X; i_X++)
            {
                i_CodeIndex++;
                l2_Map_ObjectCode[i_Y][i_X] = cl_MapString.Get_MapCode_Object()[i_CodeIndex];
            }
        }
    }

    #endregion

    #region Fence UP Code Manager

    //Public (Map Code Matrix)

    /// <summary>
    /// Set Code to a SQUARE of FENCE UP Map Code
    /// </summary>
    /// <param name="i_X">Dir UP and DOWN</param>
    /// <param name="i_Y">Dir LEFT and RIGHT</param>
    /// <param name="c_Code"></param>
    public void Set_MapCodeMatrix_Fence_Up(int i_X, int i_Y, char c_Code)
    {
        l2_Map_Fence_UpCode[i_X][i_Y] = c_Code;
    }

    /// <summary>
    /// Get Code from a SQUARE of FENCE UP Map Code
    /// </summary>
    /// <param name="i_X">Dir UP and DOWN</param>
    /// <param name="i_Y">Dir LEFT and RIGHT</param>
    /// <returns></returns>
    public char Get_MapCodeMatrix_Fence_Up(int i_X, int i_Y)
    {
        return l2_Map_Fence_UpCode[i_X][i_Y];
    }

    //Private

    /// <summary>
    /// Get MAP CODE of FENCE UP
    /// </summary>
    private void Get_MapCodeMatrix_ToMapCodeFence_Up()
    {
        string s_Map_Fence_Up = "";
        for (int i_Y = 0; i_Y < this.i_Map_Y; i_Y++)
        {
            for (int i_X = 0; i_X < this.i_Map_X; i_X++)
            {
                s_Map_Fence_Up += l2_Map_Fence_UpCode[i_Y][i_X];
            }
        }
        cl_MapString.Set_MapCode_Fence_Up(s_Map_Fence_Up);
    }

    /// <summary>
    /// Set MAP FENCE UP CODE from FENCE UP CODE
    /// </summary>
    private void Set_MapCodeMatrix_FromMapCodeFence_Up()
    {
        int i_CodeIndex = -1;
        for (int i_Y = 0; i_Y < this.i_Map_Y; i_Y++)
        {
            for (int i_X = 0; i_X < this.i_Map_X; i_X++)
            {
                i_CodeIndex++;
                l2_Map_Fence_UpCode[i_Y][i_X] = cl_MapString.Get_MapCode_Fence_Up()[i_CodeIndex];
            }
        }
    }

    #endregion

    #region Fence DOWN Code Manager

    //Public (Map Code Matrix)

    /// <summary>
    /// Set Code to a SQUARE of FENCE DOWN Map Code
    /// </summary>
    /// <param name="i_X">Dir UP and DOWN</param>
    /// <param name="i_Y">Dir LEFT and RIGHT</param>
    /// <param name="c_Code"></param>
    public void Set_MapCodeMatrix_Fence_Down(int i_X, int i_Y, char c_Code)
    {
        l2_Map_Fence_DownCode[i_X][i_Y] = c_Code;
    }

    /// <summary>
    /// Get Code from a SQUARE of FENCE DOWN Map Code
    /// </summary>
    /// <param name="i_X">Dir UP and DOWN</param>
    /// <param name="i_Y">Dir LEFT and RIGHT</param>
    /// <returns></returns>
    public char Get_MapCodeMatrix_Fence_Down(int i_X, int i_Y)
    {
        return l2_Map_Fence_DownCode[i_X][i_Y];
    }

    //Private

    /// <summary>
    /// Get MAP CODE of FENCE DOWN
    /// </summary>
    private void Get_MapCodeMatrix_ToMapCodeFence_Down()
    {
        string s_Map_Fence_Down = "";
        for (int i_Y = 0; i_Y < this.i_Map_Y; i_Y++)
        {
            for (int i_X = 0; i_X < this.i_Map_X; i_X++)
            {
                s_Map_Fence_Down += l2_Map_Fence_DownCode[i_Y][i_X];
            }
        }
        cl_MapString.Set_MapCode_Fence_Down(s_Map_Fence_Down);
    }

    /// <summary>
    /// Set MAP FENCE DOWN CODE from FENCE DOWN CODE
    /// </summary>
    private void Set_MapCodeMatrix_FromMapCodeFence_Down()
    {
        int i_CodeIndex = -1;
        for (int i_Y = 0; i_Y < this.i_Map_Y; i_Y++)
        {
            for (int i_X = 0; i_X < this.i_Map_X; i_X++)
            {
                i_CodeIndex++;
                l2_Map_Fence_DownCode[i_Y][i_X] = cl_MapString.Get_MapCode_Fence_Down()[i_CodeIndex];
            }
        }
    }

    #endregion

    #region Fence LEFT Code Manager

    //Public (Map Code Matrix)

    /// <summary>
    /// Set Code to a SQUARE of FENCE LEFT Map Code
    /// </summary>
    /// <param name="i_X">Dir UP and DOWN</param>
    /// <param name="i_Y">Dir LEFT and RIGHT</param>
    /// <param name="c_Code"></param>
    public void Set_MapCodeMatrix_Fence_Left(int i_X, int i_Y, char c_Code)
    {
        l2_Map_Fence_LeftCode[i_X][i_Y] = c_Code;
    }

    /// <summary>
    /// Get Code from a SQUARE of FENCE LEFT Map Code
    /// </summary>
    /// <param name="i_X">Dir UP and DOWN</param>
    /// <param name="i_Y">Dir LEFT and RIGHT</param>
    /// <returns></returns>
    public char Get_MapCodeMatrix_Fence_Left(int i_X, int i_Y)
    {
        return l2_Map_Fence_LeftCode[i_X][i_Y];
    }

    //Private

    /// <summary>
    /// Get MAP CODE of FENCE LEFT
    /// </summary>
    private void Get_MapCodeMatrix_ToMapCodeFence_Left()
    {
        string s_Map_Fence_Left = "";
        for (int i_Y = 0; i_Y < this.i_Map_Y; i_Y++)
        {
            for (int i_X = 0; i_X < this.i_Map_X; i_X++)
            {
                s_Map_Fence_Left += l2_Map_Fence_LeftCode[i_Y][i_X];
            }
        }
        cl_MapString.Set_MapCode_Fence_Left(s_Map_Fence_Left);
    }

    /// <summary>
    /// Set MAP FENCE LEFT CODE from FENCE LEFT CODE
    /// </summary>
    private void Set_MapCodeMatrix_FromMapCodeFence_Left()
    {
        int i_CodeIndex = -1;
        for (int i_Y = 0; i_Y < this.i_Map_Y; i_Y++)
        {
            for (int i_X = 0; i_X < this.i_Map_X; i_X++)
            {
                i_CodeIndex++;
                l2_Map_Fence_LeftCode[i_Y][i_X] = cl_MapString.Get_MapCode_Fence_Left()[i_CodeIndex];
            }
        }
    }

    #endregion

    #region Fence RIGHT Code Manager

    //Public (Map Code Matrix)

    /// <summary>
    /// Set Code to a SQUARE of FENCE RIGHT Map Code
    /// </summary>
    /// <param name="i_X">Dir UP and DOWN</param>
    /// <param name="i_Y">Dir LEFT and RIGHT</param>
    /// <param name="c_Code"></param>
    public void Set_MapCodeMatrix_Fence_Right(int i_X, int i_Y, char c_Code)
    {
        l2_Map_Fence_RightCode[i_X][i_Y] = c_Code;
    }

    /// <summary>
    /// Get Code from a SQUARE of FENCE RIGHT Map Code
    /// </summary>
    /// <param name="i_X">Dir UP and DOWN</param>
    /// <param name="i_Y">Dir LEFT and RIGHT</param>
    /// <returns></returns>
    public char Get_MapCodeMatrix_Fence_Right(int i_X, int i_Y)
    {
        return l2_Map_Fence_RightCode[i_X][i_Y];
    }

    //Private

    /// <summary>
    /// Get MAP CODE of FENCE RIGHT
    /// </summary>
    private void Get_MapCodeMatrix_ToMapCodeFence_Right()
    {
        string s_Map_Fence_Right = "";
        for (int i_Y = 0; i_Y < this.i_Map_Y; i_Y++)
        {
            for (int i_X = 0; i_X < this.i_Map_X; i_X++)
            {
                s_Map_Fence_Right += l2_Map_Fence_RightCode[i_Y][i_X];
            }
        }
        cl_MapString.Set_MapCode_Fence_Right(s_Map_Fence_Right);
    }

    /// <summary>
    /// Set MAP FENCE RIGHT CODE from FENCE RIGHT CODE
    /// </summary>
    private void Set_MapCodeMatrix_FromMapCodeFence_Right()
    {
        int i_CodeIndex = -1;
        for (int i_Y = 0; i_Y < this.i_Map_Y; i_Y++)
        {
            for (int i_X = 0; i_X < this.i_Map_X; i_X++)
            {
                i_CodeIndex++;
                l2_Map_Fence_RightCode[i_Y][i_X] = cl_MapString.Get_MapCode_Fence_Right()[i_CodeIndex];
            }
        }
    }

    #endregion

    //GameObject

    //...
}
