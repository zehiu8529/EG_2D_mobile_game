using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Working on IO FILE
/// </summary>
public class Class_FileIO
//File IO for Window
{
    public const string s_ExampleLink = @"D:\Class_FileIO.txt";

    public Class_FileIO()
    {
        FileSet_Write_Clear();
        FileSet_Read_Clear();
    }

    /// <summary>
    /// Get [c_Disk + ":\\" + s_Name + ((s_Type != null) ? "_" : "") + s_Type + "." + s_FileExtend]
    /// </summary>
    /// <param name="c_Disk"></param>
    /// <param name="s_Name"></param>
    /// <param name="s_Type"></param>
    /// <param name="s_FileExtend"></param>
    /// <returns></returns>
    public string Get_FileLink_Disk(char c_Disk, string s_Name, string s_Type, string s_FileExtend)
    {
        return c_Disk + ":\\" + s_Name + ((s_Type != null) ? "_" : "") + s_Type + "." + s_FileExtend;
    }

    /// <summary>
    /// Get [Application.dataPath + s_Name + ((s_Type != null) ? "_" : "") + s_Type + "." + s_FileExten]
    /// </summary>
    /// <param name="s_Name"></param>
    /// <param name="s_Type"></param>
    /// <param name="s_FileExten"></param>
    /// <returns></returns>
    public string Get_FileLink_Application_DataPath(string s_Name, string s_Type, string s_FileExten)
    {
        return Application.dataPath + s_Name + ((s_Type != null) ? "_" : "") + s_Type + "." + s_FileExten;
    }

    /// <summary>
    /// Get [Application.persistentDataPath + s_Name + ((s_Type != null) ? "_" : "") + s_Type + "." + s_FileExten]
    /// </summary>
    /// <param name="s_Name"></param>
    /// <param name="s_Type"></param>
    /// <param name="s_FileExten"></param>
    /// <returns></returns>
    public string Get_FileLink_Application_PersistentDataPath(string s_Name, string s_Type, string s_FileExten)
    {
        return Application.persistentDataPath + s_Name + ((s_Type != null) ? "_" : "") + s_Type + "." + s_FileExten;
    }

    /// <summary>
    /// Check File Exist
    /// </summary>
    /// <param name="s_Link"></param>
    /// <returns></returns>
    public bool Get_FileExist(string s_Link)
    //Check File Exist
    {
        return File.Exists(s_Link);
    }

    //----------------------------------------------------------------------------- Read File

    private string s_TextRead = "";

    /// <summary>
    /// Clear Data to Write
    /// </summary>
    public void FileSet_Write_Clear()
    //Clear Data to Write
    {
        s_TextRead = "";
    }

    /// <summary>
    /// Add Data to Write
    /// </summary>
    /// <param name="s_Add"></param>
    public void FileSet_Write_Add(string s_Add)
    //Add Data to Write
    {
        if (s_TextRead.Length != 0)
            s_TextRead += "\n";
        s_TextRead += s_Add;
    }

    /// <summary>
    /// Add Data to Write
    /// </summary>
    /// <param name="s_Add"></param>
    public void FileSet_Write_Add(int s_Add)
    //Add Data to Write
    {
        if (s_TextRead.Length != 0)
            s_TextRead += "\n";
        s_TextRead += s_Add.ToString();
    }

    /// <summary>
    /// Add Data to Write
    /// </summary>
    /// <param name="s_Add"></param>
    public void FileSet_Write_Add(float s_Add)
    //Add Data to Write
    {
        if (s_TextRead.Length != 0)
            s_TextRead += "\n";
        s_TextRead += s_Add.ToString();
    }

    /// <summary>
    /// Add Data to Write
    /// </summary>
    /// <param name="s_Add"></param>
    public void FileSet_Write_Add(double s_Add)
    //Add Data to Write
    {
        if (s_TextRead.Length != 0)
            s_TextRead += "\n";
        s_TextRead += s_Add.ToString();
    }

    /// <summary>
    /// Start Write to File
    /// </summary>
    /// <param name="s_Link"></param>
    public void FileActive_Write_Start(string s_Link)
    //Start Write to File
    {
        using (FileStream myFile = File.Create(s_Link))
        {
            try
            {
                byte[] b_Info = new UTF8Encoding(true).GetBytes(s_TextRead);
                myFile.Write(b_Info, 0, b_Info.Length);
            }
            catch
            {
                Debug.LogError("FileActive_Write_Start(" + s_Link + ")");
            }
        }
    }

    //----------------------------------------------------------------------------- Input File

    private List<string> ls_TextInput = new List<string>();
    private int i_InputRun = -1;

    /// <summary>
    /// Clear Data to Read
    /// </summary>
    public void FileSet_Read_Clear()
    //Clear Data to Read
    {
        ls_TextInput = new List<string>();
        i_InputRun = -1;
    }

    /// <summary>
    /// Start Read from File
    /// </summary>
    /// <param name="s_Link"></param>
    public void FileActive_Read_Start(string s_Link)
    //Start Read from File
    {
        try
        {
            // Open the stream and read it back.
            using (StreamReader sr = File.OpenText(s_Link))
            {
                string s_ReadRun = "";
                while ((s_ReadRun = sr.ReadLine()) != null)
                {
                    ls_TextInput.Add(s_ReadRun);
                }
            }
        }
        catch
        {
            Debug.LogError("FileActive_Read_Start(" + s_Link + ")");
        }
    }

    /// <summary>
    /// Read Data after Read from File
    /// </summary>
    /// <returns></returns>
    public string FileGet_Read_Auto_String()
    //Read Data after Read from File
    {
        i_InputRun++;
        return ls_TextInput[i_InputRun];
    }

    /// <summary>
    /// Read Data after Read from File
    /// </summary>
    /// <returns></returns>
    public int FileGet_Read_Auto_Int()
    //Read Data after Read from File
    {
        i_InputRun++;
        return int.Parse(ls_TextInput[i_InputRun]);
    }

    /// <summary>
    /// Read Data after Read from File
    /// </summary>
    /// <returns></returns>
    public float FileGet_Read_Auto_Float()
    //Read Data after Read from File
    {
        i_InputRun++;
        return float.Parse(ls_TextInput[i_InputRun]);
    }

    /// <summary>
    /// Read Data after Read from File
    /// </summary>
    /// <returns></returns>
    public double FileGet_Read_Auto_Double()
    //Read Data after Read from File
    {
        i_InputRun++;
        return double.Parse(ls_TextInput[i_InputRun]);
    }

    /// <summary>
    /// Get Index on Auto Read
    /// </summary>
    /// <returns></returns>
    public int FileGet_Read_AutoPresent()
    //Get Index on Auto Read
    {
        return i_InputRun;
    }

    /// <summary>
    /// Get Data on Index line after Read from File
    /// </summary>
    /// <param name="i_Index"></param>
    /// <returns></returns>
    public string FileGet_Read_Index(int i_Index)
    //Read Data after Read from File
    {
        if (i_Index < ls_TextInput.Count)
            return ls_TextInput[i_Index];
        return null;
    }

    /// <summary>
    /// Get All Data line after Read from File
    /// </summary>
    /// <returns></returns>
    public List<string> FileGet_Read_List()
    //Read Data after Read from File
    {
        return ls_TextInput;
    }
}