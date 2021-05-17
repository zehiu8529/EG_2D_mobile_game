using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
//Cho phép Script chạy ngay trong Edit Mode thay vì chỉ khi Run Mode

public class Eye2D_Raycast : MonoBehaviour
{
    //Script này hỗ trợ tầm nhìn cho GameObject bằng LineCast lẫn RayCast
    //Hàm này cho phép nhìn thấy tất cả các mục tiêu mà không bị cản trở bởi các mục tiêu khác trên đường truyền
    //Lưu ý là việc mục tiêu có bị nhìn thấy hay không là phụ thuộc vào vị trí tâm của mục tiêu so với vùng nhìn của Eye

    public bool b_GizmosTarket = true;
    public bool b_GizmosVision = true;

    public Vector2 v_CircleEye = new Vector2(0, 0);
    //Vị trí của Eye so với tâm của GameObject chủ

    public LayerMask l_Tarket;
    //Layer của các mục tiêu cần được phát hiện
    public bool b_EyeThrough = true;
    //Nếu là True thì các mục tiêu khác Layer sẽ không thể che chắn cho nhau
    public LayerMask l_Barrier;
    //Layer vật cản

    [Range(0, 360)]
    public float f_DegCentre = 0;
    //Góc giữa
    [Range(0, 360)]
    public float f_DegVision = 60;
    //Góc quét
    public float f_CircleDuration = 3;
    //Bán kính quét

    private List<GameObject> lg_TarketInVision = new List<GameObject>();
    //Danh sách các mục tiêu đã được nhìn thấy

    private bool b_ClosedSorted = false;
    //Luôn luôn sắp xếp danh sách theo khoảng cách gần nhất

    private void Update()
    {
        //Thực hiện cập nhật danh sách mỗi Frame
        lg_TarketInVision = Receive_Vision();
    }

    //Hàm thực hiện cập nhất lại danh sách
    private List<GameObject> Receive_Vision()
    //Sử dụng hàm này ở các Script khác trong hàm Update hoặc tương tự
    {
        List<GameObject> lg_Vision = new List<GameObject>();
        //Danh sách các mục tiêu được nhìn thấy, sau khi xử lý xong thì trả về danh sách này

        Vector2 v_PosCur = (Vector2)transform.position + v_CircleEye;
        //Vị trí hiện tại Obj2D_Eye.cs

        Collider2D[] c_Circle = Physics2D.OverlapCircleAll(v_PosCur, f_CircleDuration, l_Tarket);
        //Quét toàn bộ mục tiêu trong một bán kính xung quanh mình rồi thêm vào danh sách nếu thoả điều kiện

        for (int i = 0; i < c_Circle.Length; i++)
        //Xét từng mục tiêu có trong danh sách
        {
            Collider2D c_Tarket = c_Circle[i].GetComponent<Collider2D>();
            //Nhận Collider đầu tiên của mục tiêu để xác định có nhìn thấy hay không?

            Vector2 v_PosTarket = (Vector2)c_Circle[i].transform.position + c_Tarket.offset;
            //Vị trí của mục tiêu

            Vector2 v_DirTarket = (v_PosTarket - v_PosCur).normalized;
            //Hướng của mục tiêu cần xét

            float f_DegTarket = Receive_DegTarket(v_DirTarket);
            //Lưu góc độ của Tarket so với phương của Centre

            if (f_DegTarket < f_DegVision / 2)
            {
                LayerMask l_Through;
                //Layer hiện đang xét

                if (b_EyeThrough)
                    //Nếu cho phép nhìn xuyên vật thể
                    l_Through = 0;
                else
                    l_Through = l_Tarket - (int)Mathf.Pow(2, c_Circle[i].gameObject.layer);
                //Các Layer Mask tính theo luỹ thừa của 2 (2^n)

                float f_DisTarket = Vector2.Distance(v_PosTarket + c_Tarket.offset, v_PosCur);
                //Khoảng cách đến mục tiêu cần xét (đến Collider của mục tiêu cần xét)

                if (!Physics2D.Raycast(v_PosCur, v_DirTarket, f_DisTarket, l_Barrier + l_Through))
                    //Nếu bắn RayCast trong một khoảng giới hạn DisTarket mà không va chạm bất kì vật cản nào
                    lg_Vision.Add(c_Circle[i].gameObject);
            }
        }

        if (b_ClosedSorted)
        //Nếu luôn phải sắp xếp danh sách theo khoảng cách gần nhất
        {
            if (lg_Vision.Count != 0)
            {
                //Sắp xếp kiểu Interchance Sort
                for (int i = 0; i < lg_Vision.Count - 1; i++)
                {
                    for (int j = i + 1; j < lg_Vision.Count; j++)
                    {
                        float f_DistanceA = Vector2.Distance((Vector2)transform.position, (Vector2)lg_Vision[i].transform.position);
                        float f_DistanceB = Vector2.Distance((Vector2)transform.position, (Vector2)lg_Vision[j].transform.position);
                        //Tính khoảng cách hiện tại
                        if (f_DistanceA > f_DistanceB)
                        {
                            GameObject t = lg_Vision[i];
                            lg_Vision[i] = lg_Vision[j];
                            lg_Vision[j] = t;
                        }
                    }
                }
                //Sắp xếp kiểu Interchance Sort
            }
        }

        return lg_Vision;
    }

    private float Receive_DegTarket(Vector2 v_DirTarket)
    {
        return Vector2.Angle(Chance_DegToVector(f_DegCentre, f_CircleDuration), v_DirTarket);
        //Trả về góc độ Deg của Tarket so với phương của Centre
        //"Chance_DegToVector(f_DegCentre, f_CircleDuration)" nhận kết quả Vector hướng theo phương Centre
    }

    private Vector2 Chance_DegToVector(float Angle, float Duration)
    {
        return new Vector2(Mathf.Cos(Angle * Mathf.Deg2Rad), Mathf.Sin(Angle * Mathf.Deg2Rad)) * Duration;
        //Trả về vector hướng từ tâm đến vị trí trên cung tròn với góc Deg của Angle và bán kính Duration
    }

    private void OnDrawGizmos()
    {
        Vector2 v_PosCur = (Vector2)transform.position;
        v_PosCur.x = v_PosCur.x + v_CircleEye.x;
        v_PosCur.y = v_PosCur.y + v_CircleEye.y;

        if(b_GizmosVision)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(
                v_PosCur,
                v_PosCur + Chance_DegToVector(f_DegCentre + f_DegVision / 2, f_CircleDuration));
            //Vẽ khoảng trên theo ngược chiều kim đồng hồ
            Gizmos.DrawLine(
                v_PosCur,
                v_PosCur + Chance_DegToVector(f_DegCentre - f_DegVision / 2, f_CircleDuration));
            //Vẽ khoảng dưới theo ngược chiều kim đồng hồ

            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                v_PosCur,
                v_PosCur + Chance_DegToVector(f_DegCentre, f_CircleDuration));
            //Vẽ khoảng giữa Centre
        }

        if (b_GizmosTarket)
        {
            if (lg_TarketInVision.Count > 0)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < lg_TarketInVision.Count; i++)
                {
                    Collider2D c_Tarket = lg_TarketInVision[i].GetComponent<Collider2D>();
                    Gizmos.DrawWireSphere((Vector2)lg_TarketInVision[i].transform.position + c_Tarket.offset, 0.1f);
                    Gizmos.DrawLine(v_PosCur, (Vector2)lg_TarketInVision[i].transform.position + c_Tarket.offset);
                }
                //Xác định mục tiêu hiện đang nhìn thấy bằng chính Collider của mục tiêu đó
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 v_PosCur = (Vector2)transform.position;
        v_PosCur.x = v_PosCur.x + v_CircleEye.x;
        v_PosCur.y = v_PosCur.y + v_CircleEye.y;

        //if(b_GizmosVision)
        //{
        //    Gizmos.color = Color.gray;
        //    Gizmos.DrawWireSphere(v_PosCur, f_CircleDuration);
        //    //Vẽ cung tròn
        //}

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(v_PosCur, (f_DegVision == 360) ? f_CircleDuration : (f_CircleDuration / 100));
        //Nơi bắt đầu của tầm nhìn
    }

    //----------------------------------------------------------------

    //Xác định có luôn phải sắp xếp danh sách tầm nhìn theo khoảng cách gần nhất hay không
    public void Set_ClosedSorted(bool b_ClosedSorted)
    {
        this.b_ClosedSorted = b_ClosedSorted;
    }

    //Trả về kết quả có luôn phải sắp xếp danh sách tầm nhìn theo khoảng cách gần nhất hay không
    public bool Get_ClosedSorted()
    {
        return this.b_ClosedSorted;
    }

    public int Get_Count()
    //Trả về số lượng mục tiêu trong danh sách tầm nhìn
    {
        return lg_TarketInVision.Count;
    }

    public List<GameObject> Get_List()
    //Trả về danh sách mục tiêu trong tầm nhìn
    {
        return lg_TarketInVision;
    }

    public GameObject Get_Closed()
    //Trả về mục tiêu có khoảng cách gần nhất
    {
        if (lg_TarketInVision.Count == 0)
            return null;

        int i_Closed = 0;
        float f_Closed = Vector2.Distance((Vector2)transform.position, lg_TarketInVision[0].transform.position);

        for (int i = 1; i < lg_TarketInVision.Count; i++)
        {
            float f_ClosedCheck =
                Vector2.Distance((Vector2)transform.position, (Vector2)lg_TarketInVision[i].transform.position);
            //Tính khoảng cách hiện tại

            if (f_ClosedCheck < f_Closed)
            {
                i_Closed = i;
                f_Closed = f_ClosedCheck;
            }
        }

        //if(b_Debug)
        //{
        //    Vector2 v_PosCur = (Vector2)transform.position;
        //    v_PosCur.x = v_PosCur.x + v_CircleEye.x;
        //    v_PosCur.y = v_PosCur.y + v_CircleEye.y;
        //    Collider2D c_Tarket = lg_TarketInVision[i_Closed].GetComponent<Collider2D>();
        //    Debug.DrawLine(v_PosCur, (Vector2)lg_TarketInVision[i_Closed].transform.position + c_Tarket.offset, Color.blue);
        //}

        return lg_TarketInVision[i_Closed].gameObject;
    }

    //Trả về danh sách khoảng cách theo danh sách mục tiêu nhìn thấy hiện tại
    public List<float> Get_Distance()
    {
        if (lg_TarketInVision.Count == 0)
            return null;

        List<float> l_Distance = new List<float>();

        for (int i = 0; i < lg_TarketInVision.Count; i++)
        {
            l_Distance.Add(Vector2.Distance((Vector2)gameObject.transform.position, (Vector2)lg_TarketInVision[i].transform.position));
        }

        return l_Distance;
    }

    //Trả về danh sách khoảng cách theo trục X theo danh sách mục tiêu nhìn thấy hiện tại
    public List<float> Get_Distance_X()
    {
        if (lg_TarketInVision.Count == 0)
            return null;

        List<float> l_Distance = new List<float>();

        for (int i = 0; i < lg_TarketInVision.Count; i++)
        {
            l_Distance.Add(Mathf.Abs(transform.position.x - lg_TarketInVision[i].transform.position.x));
        }

        return l_Distance;
    }

    //Trả về danh sách khoảng cách theo trục Y theo danh sách mục tiêu nhìn thấy hiện tại
    public List<float> Get_Distance_Y()
    {
        if (lg_TarketInVision.Count == 0)
            return null;

        List<float> l_Distance = new List<float>();

        for (int i = 0; i < lg_TarketInVision.Count; i++)
        {
            l_Distance.Add(Mathf.Abs(transform.position.y - lg_TarketInVision[i].transform.position.y));
        }

        return l_Distance;
    }

    //Trả về danh sách khoảng cách theo trục Z theo danh sách mục tiêu nhìn thấy hiện tại
    public List<float> Get_Distance_Z()
    {
        if (lg_TarketInVision.Count == 0)
            return null;

        List<float> l_Distance = new List<float>();

        for (int i = 0; i < lg_TarketInVision.Count; i++)
        {
            l_Distance.Add(Mathf.Abs(transform.position.z - lg_TarketInVision[i].transform.position.z));
        }

        return l_Distance;
    }

    public void Set_Rotation()
    //Hàm này giúp khoảng nhìn xoay theo góc xoay của GameObject
    {
        f_DegCentre = transform.rotation.z;
    }
}