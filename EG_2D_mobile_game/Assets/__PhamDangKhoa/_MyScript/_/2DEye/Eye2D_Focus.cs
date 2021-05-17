using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))] 
//Yêu cầu một Collider bất kì (vuông, tròn, ovan,...) có bật IsTrigger

public class Eye2D_Focus : MonoBehaviour
{
    //Script này gán vào một GameObject đã gán Collider bật IsTrigger
    //Script này có chức năng lưu lại toàn bộ GameObject mà nó va chạm
    //Script có thể trả về 1 GameObject hoặc danh sách các GameObject này
    //Để thuận tiện khi trả về 1 GameObject, Script có tính năng chọn đơn mục tiêu, yêu cầu GameObject ngắm có gán Object_Stick.cs

    public bool b_Debug = true;

    public KeyCode k_BeforeTarket = KeyCode.Q;
    public KeyCode k_AfterTarket = KeyCode.E;
    public KeyCode k_ClosestTarket = KeyCode.F;

    public GameObject g_Mask;                   
    //Gán Prefab "Hồng tâm" hoặc "Mũi tên" vào đây, có gán Obj2d_Stick.cs
    public Vector2 v_Mask = new Vector2(0, 1);  
    //Vị trí mà g_Mask sẽ trị vì trên mục tiêu được chọn
    private GameObject g_MaskClone;             
    //Clone g_Mask

    private List<GameObject> l_List = new List<GameObject>();
    private int i_Tarket = 0;                   
    //Chỉ số mục tiêu trong danh sách

    private void Update()
    {
        GetComponent<Collider2D>().isTrigger = true;    

        if (g_Mask != null)
        //Nếu có gán Prefab g_Mask thì mục tiêu được chọn sẽ hiển thị trên Gameplay
        {
            Active_Choice();

            if (l_List.Count == 0)    
                //Nếu không có mục tiêu nào trong danh sách
                Active_Destroy();
            else                      
                //Nếu còn mục tiêu trong danh sách
                Active_Mask();
        }
    }

    private void Active_Choice()
    //Thực hiện chọn lựa mục tiêu
    {
        if (l_List.Count == 0)
        {
            i_Tarket = 0;
            return;
        }

        if (Input.GetKeyDown(k_BeforeTarket))
        //Chọn mục tiêu trước
        {
            if (i_Tarket <= 0)
                i_Tarket = l_List.Count - 1;
            else
                i_Tarket--;
        }
        else
            if (Input.GetKeyDown(k_AfterTarket))
        //Chọn mục tiêu tiếp theo
        {
            if (i_Tarket >= l_List.Count - 1)
                i_Tarket = 0;
            else
                i_Tarket++;
        }
        else
            if (Input.GetKeyDown(k_ClosestTarket)) 
            //Chọn mục tiêu gần nhất
            i_Tarket = Tarket_Closed();
    }

    private int Tarket_Closed() 
    //Chọn mục tiêu gần nhất
    {
        int i_Closed = 0; 
        //Lưu vị trí thoả đầu tiên
        float f_Closed = Vector2.Distance((Vector2)transform.position, (Vector2)l_List[0].transform.position);

        for (int i = 1; i < l_List.Count; i++)
        {
            float f_ClosedCheck = Vector2.Distance((Vector2)transform.position, (Vector2)l_List[i].transform.position);

            if (f_ClosedCheck < f_Closed)
            {
                i_Closed = i;
                f_Closed = f_ClosedCheck;
            }
        }
        return i_Closed;
    }

    private void Active_Mask() 
    //Tạo mới hoặc thuyên chuyển Clone
    {
        //if (g_MaskClone == null)                            
        ////Nếu chưa có Clone thì tạo mới Clone
        //{
        //    Vector3 Pos = new Vector3(
        //        l_List[i_Tarket].transform.position.x + v_Mask.x,
        //        l_List[i_Tarket].transform.position.y + v_Mask.y,
        //        l_List[i_Tarket].transform.position.z - 1);
        //    //Vị trí hiện tại của mục tiêu

        //    g_MaskClone = Instantiate(g_Mask, Pos, transform.rotation) as GameObject;
        //    //Tạo Clone

        //    g_MaskClone.GetComponent<Control2D_Sticky>().t_Tarket = l_List[i_Tarket].gameObject.transform;
        //    g_MaskClone.GetComponent<Control2D_Sticky>().v_Lock_Pos = v_Mask;
        //    g_MaskClone.GetComponent<Control2D_Sticky>().b_Lock_Sca = false;
        //    //Truy cập vào Component Obj2D_Stick.cs
        //}
        //else  //Nếu đã có thì chỉ cần thay đổi vị trí của nó trên mục tiêu hiện tại
        //    g_MaskClone.GetComponent<Control2D_Sticky>().t_Tarket = l_List[i_Tarket].gameObject.transform;
    }

    private void Active_Destroy()   //Phá huỷ Clone
    {
        if (g_MaskClone != null)    //Nếu Clone chưa bị phá huỷ
        {
            Destroy(g_MaskClone);   //Phá huỷ Clone
            g_MaskClone = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D c_Col)     //Thêm vào danh sách
    {
        l_List.Add(c_Col.gameObject);
    }

    private void OnTriggerExit2D(Collider2D c_Col)      //Xoá khỏi danh sách
    {
        if (l_List.IndexOf(c_Col.gameObject) <= i_Tarket)
        {
            if (i_Tarket == 0)
                i_Tarket = 0;
            else
                i_Tarket--;
        }
        l_List.Remove(c_Col.gameObject);
    }

    //Hàm này được chạy liên tục trên Scene nhằm giúp chúng ta Debug ngay cả khi không chạy game
    private void OnDrawGizmosSelected()
    {
        if (!b_Debug)
            return;

        Vector2 v_Centre = new Vector2(transform.position.x, transform.position.y);

        if (GetComponent<BoxCollider2D>() != null)
        {
            BoxCollider2D b_BoxCollider = GetComponent<BoxCollider2D>();

            Gizmos.DrawLine(
                new Vector2(
                    transform.position.x + b_BoxCollider.offset.x - (b_BoxCollider.size.x / 2),
                    transform.position.y + b_BoxCollider.offset.y - (b_BoxCollider.size.y / 2)),
                new Vector2(
                    transform.position.x + b_BoxCollider.offset.x + (b_BoxCollider.size.x / 2),
                    transform.position.y + b_BoxCollider.offset.y - (b_BoxCollider.size.y / 2)));

            Gizmos.DrawLine(
                new Vector2(
                    transform.position.x + b_BoxCollider.offset.x - (b_BoxCollider.size.x / 2),
                    transform.position.y + b_BoxCollider.offset.y + (b_BoxCollider.size.y / 2)),
                new Vector2(
                    transform.position.x + b_BoxCollider.offset.x + (b_BoxCollider.size.x / 2),
                    transform.position.y + b_BoxCollider.offset.y + (b_BoxCollider.size.y / 2)));

            Gizmos.DrawLine(
                new Vector2(
                    transform.position.x + b_BoxCollider.offset.x - (b_BoxCollider.size.x / 2), 
                    transform.position.y + b_BoxCollider.offset.y - (b_BoxCollider.size.y / 2)),
                new Vector2(
                    transform.position.x + b_BoxCollider.offset.x - (b_BoxCollider.size.x / 2), 
                    transform.position.y + b_BoxCollider.offset.y + (b_BoxCollider.size.y / 2)));

            Gizmos.DrawLine(
                new Vector2(
                    transform.position.x + b_BoxCollider.offset.x + (b_BoxCollider.size.x / 2), 
                    transform.position.y + b_BoxCollider.offset.y - (b_BoxCollider.size.y / 2)),
                new Vector2(
                    transform.position.x + b_BoxCollider.offset.x + (b_BoxCollider.size.x / 2), 
                    transform.position.y + b_BoxCollider.offset.y + (b_BoxCollider.size.y / 2)));
        }
        else
            if (GetComponent<CircleCollider2D>() != null)
        {
            CircleCollider2D c_CircleCollider = GetComponent<CircleCollider2D>();

            Gizmos.DrawWireSphere(
                new Vector2(transform.position.x + c_CircleCollider.offset.x, transform.position.y + c_CircleCollider.offset.y),
                c_CircleCollider.radius);
        }
    }

    //-----------------------------------------------------------
    public GameObject Receive_Tarket()                  
    //Trả về mục tiêu hiện tại
    {
        if (l_List.Count == 0)
            return null;
        return l_List[i_Tarket].gameObject;
    }

    public List<GameObject> Receive_List()              
    //Trả về danh sách mục tiêu trong tầm ngắm
    {
        if (l_List.Count == 0)
            return null;
        return l_List;
    }

    public int Receive_Count()                          
    //Trả về số lượng mục tiêu hiện có trong danh sách
    {
        return l_List.Count;
    }
}