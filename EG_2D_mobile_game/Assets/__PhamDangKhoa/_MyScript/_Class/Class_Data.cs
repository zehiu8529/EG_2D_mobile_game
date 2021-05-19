using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Working on Data and List
/// </summary>
public class Class_Data
{
    public bool b_Debug = false;

    /// <summary>
    /// Working on Data & List
    /// </summary>
    public Class_Data()
    {
        this.b_Debug = false;
    }

    /// <summary>
    /// Working on Data & List
    /// </summary>
    public Class_Data(bool b_Debug)
    {
        this.b_Debug = b_Debug;
    }

    #region Convert

    /// <summary>
    /// Convert OBJECT to INT
    /// </summary>
    /// <param name="s_Value"></param>
    /// <returns>If Convert fail, return 0</returns>
    public int Get_Convert_Int(object s_Value)
    {
        string s_ValueCheck = s_Value.ToString();

        if (s_ValueCheck == Get_String_Data_NotFound() || s_Value == null || s_ValueCheck == "")
        {
            if(b_Debug)
                Debug.LogError("Get_Exchance_Int: \"s_Value\" To (INT)\"0\"");
            return 0;
        }
        return int.Parse(s_ValueCheck);
    }

    /// <summary>
    /// Convert OBJECT to FLOAT
    /// </summary>
    /// <param name="s_Value"></param>
    /// <returns>If Convert fail, return 0.0</returns>
    public float Get_Convert_Float(object s_Value)
    {
        string s_ValueCheck = s_Value.ToString();

        if (s_ValueCheck == Get_String_Data_NotFound() || s_Value == null || s_ValueCheck == "")
        {
            if (b_Debug)
                Debug.LogError("Get_Exchance_Float: \"s_Value\" To (FLOAT)\"0.0\"");
            return 0.0f;
        }
        return float.Parse(s_ValueCheck);
    }

    /// <summary>
    /// Convert OBJECT to BOOL
    /// </summary>
    /// <param name="s_Value"></param>
    /// <returns>If Convert fail, return FALSE</returns>
    public bool Get_Convert_Bool(object s_Value)
    {
        string s_ValueCheck = s_Value.ToString();

        if (s_ValueCheck == Get_String_Data_NotFound() || s_Value == null || s_ValueCheck == "")
        {
            if (b_Debug)
                Debug.LogError("Get_Exchance_Bool: \"s_Value\" To (BOOL)\"FALSE\"");
            return false;
        }
        return bool.Parse(s_ValueCheck);
    }

    /// <summary>
    /// Convert OBJECT to STRING
    /// </summary>
    /// <param name="s_Value"></param>
    /// <returns>If Convert fail, return NULL</returns>
    public string Get_Convert_String(object s_Value)
    {
        string s_ValueCheck = s_Value.ToString();
        return s_ValueCheck.ToString();
    }

    #endregion

    #region Data

    /// <summary>
    /// Data Name to Access
    /// </summary>
    private List<string> l_Data_Name = new List<string>();

    /// <summary>
    /// Data Value (INT, FLOAT, BOOL, STRING) saved at STRING
    /// </summary>
    private List<object> l_Data_Value = new List<object>();

    /// <summary>
    /// Get List of Data Name
    /// </summary>
    /// <returns></returns>
    public List<string> Get_List_Name()
    {
        return l_Data_Name;
    }

    /// <summary>
    /// Get List of Data Value
    /// </summary>
    /// <returns></returns>
    public List<object> Get_List_Value()
    {
        return l_Data_Value;
    }

    private int i_Index_Auto = -1;

    /// <summary>
    /// Set Auto Index to "-1" before start use "Set_Index_Plus()"
    /// </summary>
    public void Set_Index_Restart()
    {
        this.i_Index_Auto = -1;
    }

    /// <summary>
    /// Set Auto Index "+1" each time called
    /// </summary>
    public void Set_Index_Plus()
    {
        this.i_Index_Auto++;
    }

    /// <summary>
    /// Get Auto Index
    /// </summary>
    /// <returns></returns>
    public int Get_Index()
    {
        return i_Index_Auto;
    }

    #endregion

    #region List (Single Value)

    /// <summary>
    /// Get Index of Exist Data Name
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <returns></returns>
    public int Get_Int_Data_Exist(string s_DataName)
    {
        for (int i = 0; i < l_Data_Name.Count; i++)
        {
            if (l_Data_Name[i] == s_DataName)
                return i;
        }
        return -1;
    }

    /// <summary>
    /// Set Data Single
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <param name="o_DataValue"></param>
    public void Input_Data(string s_DataName, object o_DataValue)
    {
        int Index = Get_Int_Data_Exist(s_DataName);

        if (Index != -1)
        {
            if (b_Debug)
                Debug.Log("Set_Data: " + "\"" + s_DataName + "\"" + " Updated " + "\"" + o_DataValue + "\"");

            if (o_DataValue == null)
                l_Data_Value[Index] = Get_String_Data_NULL();
            else
            if (o_DataValue.GetType() == typeof(int))
                l_Data_Value[Index] = o_DataValue.ToString();
            else
            if (o_DataValue.GetType() == typeof(float))
                l_Data_Value[Index] = o_DataValue.ToString();
            else
            if (o_DataValue.GetType() == typeof(bool))
                l_Data_Value[Index] = o_DataValue.ToString();
            else
            if (o_DataValue.GetType() == typeof(string))
                l_Data_Value[Index] = o_DataValue.ToString();
            else
            if(b_Debug)
                Debug.LogError("Set_Data: " + s_DataName + " not support Updated TYPE");
        }
        else
        {
            if (b_Debug)
                Debug.Log("Set_Data: " + "\"" + s_DataName + "\"" + " Added " + "\"" + o_DataValue + "\"");

            if (o_DataValue == null)
            {
                l_Data_Name.Add(s_DataName);
                l_Data_Value.Add(Get_String_Data_NULL());
            }
            else
            if (o_DataValue.GetType() == typeof(int))
            {
                l_Data_Name.Add(s_DataName);
                l_Data_Value.Add(o_DataValue.ToString());
            }
            else
            if (o_DataValue.GetType() == typeof(float))
            {
                l_Data_Name.Add(s_DataName);
                l_Data_Value.Add(o_DataValue.ToString());
            }
            else
            if (o_DataValue.GetType() == typeof(bool))
            {
                l_Data_Name.Add(s_DataName);
                l_Data_Value.Add(o_DataValue.ToString());
            }
            else
            if (o_DataValue.GetType() == typeof(string))
            {
                l_Data_Name.Add(s_DataName);
                l_Data_Value.Add(o_DataValue.ToString());
            }
            else
            if (b_Debug)
                Debug.LogError("Set_Data: " + s_DataName + " not support Added TYPE");
        }
    }

    /// <summary>
    /// Get Data Single
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <returns>If not found Data, return "@NotFound"</returns>
    public object Get_Object_Data(string s_DataName)
    {
        for (int i = 0; i < l_Data_Name.Count; i++)
        {
            if (l_Data_Name[i] == s_DataName)
            {
                return l_Data_Value[i];
            }
        }
        if (b_Debug)
            Debug.LogError("Get_Data: " + "\"" + s_DataName + "\" Not found");
        return "@NotFound";
    }

    /// <summary>
    /// Use to Get "@NotFound" value for "Get_Data_String()"
    /// </summary>
    /// <returns></returns>
    public string Get_String_Data_NotFound()
    {
        return "@NotFound";
    }

    /// <summary>
    /// Use to Get "@Null" value for "Get_Data_String()"
    /// </summary>
    /// <returns></returns>
    public string Get_String_Data_NULL()
    {
        return "@Null";
    }

    #endregion

    #region List (Muti Value)

    /// <summary>
    /// Set Data Muti
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <param name="i_Index"></param>
    /// <param name="o_DataValue"></param>
    public void Input_Data(string s_DataName, int i_Index, object o_DataValue)
    {
        string s_DataCheck = s_DataName + "_" + i_Index.ToString();
        Input_Data(s_DataCheck, o_DataValue);
    }

    /// <summary>
    /// Set Data Muti Count
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <param name="i_Count"></param>
    public void Input_Data_Count(string s_DataName, int i_Count)
    {
        string s_DataCheck = s_DataName + "_Count";
        Input_Data(s_DataCheck, i_Count);
    }

    /// <summary>
    /// Get Data Muti
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <param name="i_Index"></param>
    /// <returns></returns>
    public object Get_Object_Data(string s_DataName, int i_Index)
    {
        string s_DataCheck = s_DataName + "_" + i_Index.ToString();
        return Get_Object_Data(s_DataCheck);
    }

    /// <summary>
    /// Get Data Muti Count
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <returns></returns>
    public int Get_Int_Data_Count(string s_DataName)
    {
        string s_DataCheck = s_DataName + "_Count";
        if (Get_Convert_String(Get_Object_Data(s_DataCheck)) == Get_String_Data_NotFound())
            return -1;
        return Get_Convert_Int(Get_Object_Data(s_DataCheck).ToString());
    }

    /// <summary>
    /// Get own Index Name Data
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <param name="i_Index"></param>
    /// <returns></returns>
    public string Get_String_Get_Convert_NameIndex(string s_DataName, int i_Index)
    {
        return s_DataName + "_" + i_Index.ToString();
    }

    /// <summary>
    /// Get own Count Name Data
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <param name="i_Count"></param>
    /// <returns></returns>
    public string Get_String_Get_Convert_NameCount(string s_DataName)
    {
        return s_DataName + "_Count";
    }

    #endregion
}
