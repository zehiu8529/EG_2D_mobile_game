using UnityEngine;

public class Active_Mask : MonoBehaviour
{
    //Script điều khiển gán Mask lên chính mục tiêu được gắn Script này

    public GameObject g_Mask;
    private GameObject g_Mask_Clone;

    public Vector3 v_PosMask = new Vector3(0f, 0f, -1f);

    private bool b_MaskActive = false;

    private void Update()
    {
        Active_MaskControl();
    }

    private void Active_MaskControl()
    {
        if (b_MaskActive)
        //Nếu cho phép kích hoạt hiển thị
        {
            if (g_Mask_Clone == null)
                //Nếu chưa có thì tạo mới và gán vị trí cho nó
                g_Mask_Clone = Instantiate(g_Mask, this.transform.position + v_PosMask, transform.rotation) as GameObject;
            else
                //Thay đổi vị trí cho nó nếu đã có
                g_Mask_Clone.transform.position = this.transform.position + v_PosMask;
        }
        else
        //Nếu không cho phép kích hoạt hiển thị
        {
            if (g_Mask_Clone != null)
            //Nếu nó vẫn còn tồn tại thì phá huỷ nó đi
            {
                Destroy(g_Mask_Clone);
                g_Mask_Clone = null;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(this.transform.position + v_PosMask, 0.1f);
    }

    //--------------------------------------------------------------------------------
    public void Active_SetMaskControl(bool b_MaskActive)
    //Kích hoạt Mask
    {
        this.b_MaskActive = b_MaskActive;
    }
}
