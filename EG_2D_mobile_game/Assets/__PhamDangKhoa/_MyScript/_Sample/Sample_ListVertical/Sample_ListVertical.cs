using UnityEngine;

public class Sample_ListVertical : MonoBehaviour
{
    public ListVertical_Componnet cs_List;

    public GameObject g_Prepab;

    public void Button_Add()
    {
        g_Prepab.GetComponent<Sample_ListVertical_Button>().Set_ListVertical_Button_Text(
            "myHEADER " + (cs_List.Get_ListPrepab_Count() + 1).ToString(),
            "myFOOTER " + (cs_List.Get_ListPrepab_Count() + 1).ToString());

        cs_List.Set_ListPrepab(g_Prepab, (cs_List.Get_ListPrepab_Count() + 1).ToString());
    }

    public void Button_Delete()
    {
        cs_List.Set_ListPrepab_Delete(0);
    }
}
