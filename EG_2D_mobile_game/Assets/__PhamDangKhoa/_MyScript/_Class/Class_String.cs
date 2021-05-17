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

    //Email Check (Local-Part@Domain-Part)

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

    //Encypt Data

    /// <summary>
    /// Encypt Data to Single String
    /// </summary>
    /// <param name="l_Data"></param>
    /// <returns></returns>
    private string Get_EncyptData(List<string> l_Data)
    {
        string s_Data = "";

        for(int i = 0; i < l_Data.Count; i++)
        {
            s_Data += (l_Data[i] + "$");
        }

        return s_Data;
    }

    /// <summary>
    /// DeEncypt Data from Single String
    /// </summary>
    /// <param name="s_Data"></param>
    /// <returns></returns>
    private List<string> Get_DeEncyptData(string s_Data)
    {
        List<string> l_Data = new List<string>();

        int i_Read = 0;

        for (int i = 0; i < s_Data.Length; i++)
        {
            if (s_Data[i] == '$')
            {
                i_Read++;
            }
            else
            {
                l_Data[i_Read] += s_Data[i];
            }
        }

        return l_Data;
    }
}
