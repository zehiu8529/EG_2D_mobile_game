using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showpan : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Panel;
    int counter;
    public GameObject Setting;

    public void showpanel()
    {
        counter++;
        if(counter % 2==1)
        {
            Panel.gameObject.SetActive(false);
        }else
        {
            Panel.gameObject.SetActive(true);
        }
        
    }

    // hien
    public void showSetting()
    {
        counter++;
        if (counter % 2 == 1)
        {
            Setting.gameObject.SetActive(false);
        }
        else
        {
            Setting.gameObject.SetActive(true);
        }

    }
}
