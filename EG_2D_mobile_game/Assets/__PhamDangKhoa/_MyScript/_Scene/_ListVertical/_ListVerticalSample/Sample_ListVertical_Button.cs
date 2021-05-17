using UnityEngine;
using UnityEngine.UI;

public class Sample_ListVertical_Button : MonoBehaviour
{
    /// <summary>
    /// Self Control on List (For send Request, etc)
    /// </summary>
    [HideInInspector]
    public ListVertical_Componnet cs_Component;

    [Header("Header")]
    public Text t_Header;
    //Header Text
    public FontStyle f_Header_FontStyle = FontStyle.Bold;
    public int i_Header_FontSize = 25;
    public int i_Header_LineSpacing = 1;
    public TextAnchor t_Header_TextAnchor = TextAnchor.MiddleLeft;
    public Color c_Header_Color = Color.black;
    public HorizontalWrapMode h_Header_HorizontalWrapMode = HorizontalWrapMode.Overflow;
    public VerticalWrapMode h_Header_VerticalWrapMode = VerticalWrapMode.Overflow;

    [Header("Footer")]
    public Text t_Footer;
    //Footer Text
    public FontStyle f_Footer_FontStyle = FontStyle.Normal;
    public int i_Footer_FontSize = 15;
    public int i_Footer_LineSpacing = 1;
    public TextAnchor t_Footer_TextAnchor = TextAnchor.MiddleLeft;
    public Color c_Footer_Color = Color.black;
    public HorizontalWrapMode h_Footer_HorizontalWrapMode = HorizontalWrapMode.Overflow;
    public VerticalWrapMode h_Footer_VerticalWrapMode = VerticalWrapMode.Overflow;

    [Header("Button Image")]
    public GameObject g_Button;
    public Image i_ButtonImage;
    //Image
    public Sprite s_ButtonImage_Sprite;
    public Color c_ButtonImage_Color;

    private void Awake()
    {
        if (t_Header != null)
        {
            //Header
            //t_Header.text = s_Header;
            t_Header.fontStyle = f_Header_FontStyle;
            t_Header.fontSize = i_Header_FontSize;
            t_Header.lineSpacing = i_Header_LineSpacing;
            t_Header.alignment = t_Header_TextAnchor;
            t_Header.color = c_Header_Color;
            t_Header.horizontalOverflow = h_Header_HorizontalWrapMode;
            t_Header.verticalOverflow = h_Header_VerticalWrapMode;
        }

        if (t_Footer != null)
        {
            //Footer
            //t_Footer.text = s_Footer;
            t_Footer.fontStyle = f_Footer_FontStyle;
            t_Footer.fontSize = i_Footer_FontSize;
            t_Footer.lineSpacing = i_Footer_LineSpacing;
            t_Footer.alignment = t_Footer_TextAnchor;
            t_Footer.color = c_Footer_Color;
            t_Footer.horizontalOverflow = h_Footer_HorizontalWrapMode;
            t_Footer.verticalOverflow = h_Footer_VerticalWrapMode;
        }

        if (i_ButtonImage != null)
        {
            //Image
            if(s_ButtonImage_Sprite!= null)
            {
                i_ButtonImage.sprite = s_ButtonImage_Sprite;
                i_ButtonImage.color = c_ButtonImage_Color;
            }
        }
    }

    /// <summary>
    /// Set Text for this Button
    /// </summary>
    /// <param name="s_Header"></param>
    /// <param name="s_Footer"></param>
    public void Set_ListVertical_Button_Text(string s_Header, string s_Footer)
    {
        if (t_Header != null)
            t_Header.text = s_Header;
        if (t_Footer != null)
            t_Footer.text = s_Footer;
    }

    /// <summary>
    /// Set Button Show or not
    /// </summary>
    /// <param name="b_Show"></param>
    public void Set_ListVertical_Button_Show(bool b_Show)
    {
        g_Button.SetActive(b_Show);
    }

    /// <summary>
    /// Send a Request to List
    /// </summary>
    public void Button_Room_Request()
    {
        cs_Component.Set_Button_Request(this.gameObject);
    }
}
