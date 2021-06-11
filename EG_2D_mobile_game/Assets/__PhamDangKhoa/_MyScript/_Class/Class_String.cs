using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Working on String
/// </summary>
public class Class_String
{
    /// <summary>
    /// Working on String
    /// </summary>
    public Class_String()
    {

    }

    #region String Command

    //Count

    /// <summary>
    /// Count String in String Array
    /// </summary>
    /// <param name="s_StringArray"></param>
    /// <returns></returns>
    public int Get_StringArray_Count(string[] s_StringArray)
    {
        return s_StringArray.Length;
    }

    /// <summary>
    /// Count String in String List
    /// </summary>
    /// <param name="s_StringArray"></param>
    /// <returns></returns>
    public int Get_StringList_Count(List<string> l_StringList)
    {
        return l_StringList.Count;
    }

    //Exist

    /// <summary>
    /// Check Child String inside Father String
    /// </summary>
    /// <param name="s_FatherString"></param>
    /// <param name="s_ChildString"></param>
    /// <returns></returns>
    public bool Get_String_Exist(string s_FatherString, string s_ChildString)
    {
        return s_FatherString.Contains(s_ChildString);
    }

    //Replace

    /// <summary>
    /// Replace Child String Inside Father String with a Replace String
    /// </summary>
    /// <param name="s_FatherString"></param>
    /// <param name="s_CheckString"></param>
    /// <param name="s_ReplaceString"></param>
    /// <returns></returns>
    public string Get_String_Replace(string s_FatherString, string s_CheckString, string s_ReplaceString)
    {
        return s_FatherString.Replace(s_CheckString, s_ReplaceString);
    }

    //Split

    /// <summary>
    /// Split Child String inside Father String between Check Char
    /// </summary>
    /// <param name="s_FatherString"></param>
    /// <param name="c_CheckChar"></param>
    /// <returns></returns>
    public string[] Get_String_Split_Array(string s_FatherString, char c_CheckChar)
    {
        return s_FatherString.Split(c_CheckChar);
    }

    /// <summary>
    /// Split Child String inside Father String between Check Char
    /// </summary>
    /// <param name="s_FatherString"></param>
    /// <param name="c_CheckChar"></param>
    /// <returns></returns>
    public List<string> Get_String_Split_List(string s_FatherString, char c_CheckChar)
    {
        string[] s_SplitString = Get_String_Split_Array(s_FatherString, c_CheckChar);
        List<string> l_SplitString = new List<string>();
        foreach(string s_String in s_SplitString)
        {
            l_SplitString.Add(s_String);
        }
        return l_SplitString;
    }

    #endregion

    #region String Data

    /// <summary>
    /// Get String Data of All String Data from String List
    /// </summary>
    /// <param name="l_StringDataList"></param>
    /// <param name="c_SpaceChar"></param>
    /// <returns></returns>
    public string Get_StringData_Encypt(List<string> l_StringDataList, char c_SpaceChar)
    {
        string s_StringData = "";
        for(int i = 0; i < l_StringDataList.Count; i++)
        {
            s_StringData += (l_StringDataList[i] + c_SpaceChar);
        }
        return s_StringData;
    }

    /// <summary>
    /// Get String List Data from String Data
    /// </summary>
    /// <param name="s_StringData"></param>
    /// <param name="c_SpaceChar"></param>
    /// <returns></returns>
    public List<string> Get_StringData_Dencypt(string s_StringData, char c_SpaceChar)
    {
        return Get_String_Split_List(s_StringData, c_SpaceChar);
    }

    #endregion

    #region Email Check (Local-Part@Domain-Part)

    /// <summary>
    /// Check if EMAIL NOT INVAILID (NOT FALSE)
    /// </summary>
    /// <param name="s_EmailCheck"></param>
    /// <returns>If NOT INVAILID, get TRUE</returns>
    private bool Get_CheckEmail_NotInvalid(string s_EmailCheck)
    {
        //Check SPACE
        if (s_EmailCheck.Contains(" "))
            return false;

        //Check @
        bool b_Exist_AA = false;
        for(int i = 0; i < s_EmailCheck.Length; i++)
        {
            if (!b_Exist_AA && s_EmailCheck[i] == '@')
                b_Exist_AA = true;
            else
            if (b_Exist_AA && s_EmailCheck[i] == '@')
                return false;
        }
        if (!b_Exist_AA)
            return false;

        //All Check Done
        return true;
    }

    /// <summary>
    /// Check if STRING is EMAIL format
    /// </summary>
    /// <param name="s_EmailCheck"></param>
    /// <returns></returns>
    public bool Get_CheckEmail(string s_EmailCheck)
    {
        //Check Not Invalid
        if (!Get_CheckEmail_NotInvalid(s_EmailCheck))
            return false;

        //Lower MAIL
        s_EmailCheck = s_EmailCheck.ToLower();

        return 
            Get_CheckEmail_Gmail(s_EmailCheck) && 
            Get_CheckEmail_Yahoo(s_EmailCheck);
    }

    /// <summary>
    /// Check if GMAIL NOT INVAILID
    /// </summary>
    /// <param name="s_EmailCheck"></param>
    /// <returns>If NOT INVAILID, get TRUE</returns>
    private bool Get_CheckEmail_Gmail(string s_EmailCheck)
    {
        //Check if GMAIL
        if (s_EmailCheck.Contains("@gmail.com"))
        {
            //Get ASCII
            byte[] ba_Ascii = Encoding.ASCII.GetBytes(s_EmailCheck);

            //First Character (Just Allow '0-9' and 'a-z')
            if (ba_Ascii[0] >= 48 && ba_Ascii[0] <= 57 || 
                ba_Ascii[0] >= 97 && ba_Ascii[0] <= 122)
            {
                //Next Character (Just Allow '0-9' and 'a-z' and '.')
                for (int i = 1; i < s_EmailCheck.Length; i++)
                {
                    if (s_EmailCheck[i] == '@')
                        break;

                    if (ba_Ascii[i] >= 48 && ba_Ascii[i] <= 57 ||
                        ba_Ascii[i] >= 97 && ba_Ascii[i] <= 122 ||
                        s_EmailCheck[i] == '.')
                    {

                    }
                    else
                        return false;
                }
            }
            else
                return false;
        }

        //All Check Done
        return true;
    }

    /// <summary>
    /// Check if YAHOO NOT INVAILID
    /// </summary>
    /// <param name="s_EmailCheck"></param>
    /// <returns>If NOT INVAILID, get TRUE</returns>
    private bool Get_CheckEmail_Yahoo(string s_EmailCheck)
    {
        //Check if GMAIL
        if (s_EmailCheck.Contains("@yahoo.com"))
        {
            //Get ASCII
            byte[] ba_Ascii = Encoding.ASCII.GetBytes(s_EmailCheck);

            //First Character (Just Allow 'a-z')
            if (ba_Ascii[0] >= 97 && ba_Ascii[0] <= 122)
            {
                //Next Character (Just Allow '0-9' and 'a-z' and '.' and '_')
                for (int i = 1; i < s_EmailCheck.Length; i++)
                {
                    if (s_EmailCheck[i] == '@')
                        break;

                    if (ba_Ascii[i] >= 48 && ba_Ascii[i] <= 57 ||
                        ba_Ascii[i] >= 97 && ba_Ascii[i] <= 122 ||
                        s_EmailCheck[i] == '.' ||
                        s_EmailCheck[i] == '_')
                    {

                    }
                    else
                        return false;
                }
            }
            else
                return false;
        }

        //All Check Done
        return true;
    }

    #endregion
}