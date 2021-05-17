using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
//Cho phép Script chạy ngay trong Edit Mode thay vì chỉ khi Run Mode

public class Eye2D_Tile : MonoBehaviour
{
    //Script dùng để duyệt tầm nhìn theo Ma trận Điểm ảnh

    public bool b_Debug_Emty = true;
    //Cho phép hiển thị phần tử Emyt trên Ma trận

    public Vector2 v_MatrixEye = new Vector2(0, 0.3f);
    //Vị trí của Eye so với tâm của GameObject chủ

    [Min(0)]
    public int i_Up = 10, i_Down = 10, i_Left = 10, i_Right = 10;
    //Khởi tạo Ma trận Điểm ảnh (Pixel) (Theo mặc định: Ma trận 3x3, với Chủ thể ở [2;2] có kí hiệu "M")

    [Min(0)]
    public float f_Square_Dist = 0.1f;
    //Khoảng cách tính từ Trung tâm (Middle) giữa các Pixel sẽ duyệt

    [Min(0)]
    public float f_Square_Size = 0.05f;
    //Độ rộng tính từ Middle của mỗi Pixel sẽ duyệt

    private List<List<string>> l2_Matrix;
    //Ma trận Điểm ảnh (Quy tắc duyệt: Left >> Right, Up >> Down hay Trái >> Phải, Trên >> Dưới)
    //Nếu phần tử Ma trận là Trống (Tức là không "N" hoặc "D") thì kí hiệu "E" tức Empty.

    public LayerMask l_Normal;
    //Layer duyệt An toàn (Ma trận kí hiệu "N")

    public LayerMask l_Danger;
    //Layer duyệt Nguy hiểm (Ma trận kí hiệu "D")

    private void Update()
    {
        Active_ReStart();
        Update_Vision();
    }

    //Khởi tạo Ma trận
    private void Active_ReStart()
    {
        //Khởi tạo Ma trận sẽ xử lý
        l2_Matrix = new List<List<string>>();
        for (int y = -i_Up; y <= i_Down; y++)
        {
            l2_Matrix.Add(new List<string>() { });
            for (int x = -i_Left; x <= i_Right; x++)
            {
                if (x == 0 && y == 0)
                {
                    l2_Matrix[y + i_Up].Add("M");
                }
                else
                {
                    l2_Matrix[y + i_Up].Add("E");
                }
            }
        }
    }

    private void Update_Vision()
    {
        for (int y = -i_Up; y <= i_Down; y++)
        {
            for (int x = -i_Left; x <= i_Right; x++)
            {
                if (x == 0 && y == 0)
                {
                    //Không duyệt tại vị trí [0;0]
                }
                else
                {
                    //Duyệt Normal (Bình thường)
                    if (Physics2D.BoxCast(Get_Pos(x, y), Get_Size(), 0, Vector2.zero, 0, l_Normal))
                    {
                        l2_Matrix[y + i_Up][x + i_Left] = "N";
                    }
                    else
                    //Duyệt Danger (Nguy hiểm)
                    if (Physics2D.BoxCast(Get_Pos(x, y), Get_Size(), 0, Vector2.zero, 0, l_Danger))
                    {
                        l2_Matrix[y + i_Up][x + i_Left] = "D";
                    }
                    else
                    {
                        l2_Matrix[y + i_Up][x + i_Left] = "E";
                    }
                }
            }
        }
    }

    //Nhận Vector Pos để xử lý
    private Vector3 Get_Pos(float x, float y)
    {
        return new Vector3(
            this.transform.position.x + (x * f_Square_Dist) + v_MatrixEye.x,
            this.transform.position.y + (y * -f_Square_Dist) + v_MatrixEye.y,
            this.transform.position.z);
    }

    //Nhận Vector Size để xử lý
    private Vector3 Get_Size()
    {
        return new Vector3(f_Square_Size, f_Square_Size, 0);
    }

    private void OnDrawGizmosSelected()
    {
        for (int y = -i_Up; y <= i_Down; y++)
        {
            for (int x = -i_Left; x <= i_Right; x++)
            {
                if (l2_Matrix[y + i_Up][x + i_Left] == "N")
                    //Nếu là Normal ("N") >> Màu xanh
                    Gizmos.color = Color.green;
                else
                {
                    if (l2_Matrix[y + i_Up][x + i_Left] == "D")
                        //Nếu là Danger ("D") >> Màu đỏ
                        Gizmos.color = Color.red;
                    else
                    {
                        if (l2_Matrix[y + i_Up][x + i_Left] == "M")
                            //Nếu là Tâm ("M") >> Màu xanh dương
                            Gizmos.color = Color.blue;
                        else
                        {
                            if (l2_Matrix[y + i_Up][x + i_Left] == "E")
                            {
                                //Nếu là Emty ("E") >> Black
                                if (b_Debug_Emty)
                                    Gizmos.color = Color.gray;
                                else
                                    Gizmos.color = Color.clear;
                            }
                        }
                    }
                }

                Gizmos.DrawWireCube(Get_Pos(x, y), Get_Size());
            }
        }
        //End Methode
    }

    //------------------------------------------------------------------------

    //Nhận Danh sách Pixel từ Ma trận Pixel (Nhận bằng kí hiệu "N", "D", "M" và "E")
    public List<string> Get_List_TileMatrix_Str()
    {
        if (l2_Matrix == null)
            return null;

        List<string> l_TileList = new List<string>();

        for (int y = -i_Up; y <= i_Down; y++)
        {
            for (int x = -i_Left; x <= i_Right; x++)
            {
                l_TileList.Add(l2_Matrix[y + i_Up][x + i_Left]);
            }
        }

        return l_TileList;
    }

    //Nhận Danh sách Pixel từ Ma trận Pixel (Nhận bằng số hiệu "N" = 1, "D" = 2, "M" = -1 và "E" = 0)
    public List<int> Get_List_TileMatrix_Int()
    {
        if (l2_Matrix == null)
            return null;

        List<int> l_TileList = new List<int>();

        for (int y = -i_Up; y <= i_Down; y++)
        {
            for (int x = -i_Left; x <= i_Right; x++)
            {
                l_TileList.Add(
                    (l2_Matrix[y + i_Up][x + i_Left] == "E") ? 0 :
                    (l2_Matrix[y + i_Up][x + i_Left] == "N") ? 1 :
                    (l2_Matrix[y + i_Up][x + i_Left] == "D") ? 2 : -1);
            }
        }

        return l_TileList;
    }

    //Nhận Danh sách Pixel từ Ma trận Pixel (Nhận bằng số hiệu "N" = 1, "D" = 2, "M" = -1 và "E" = 0)
    public List<float> Get_List_TileMatrix_Float()
    {
        if (l2_Matrix == null)
            return null;

        List<float> l_TileList = new List<float>();

        for (int y = -i_Up; y <= i_Down; y++)
        {
            for (int x = -i_Left; x <= i_Right; x++)
            {
                l_TileList.Add(
                    (l2_Matrix[y + i_Up][x + i_Left] == "E") ? 0.0f :
                    (l2_Matrix[y + i_Up][x + i_Left] == "N") ? 1.0f :
                    (l2_Matrix[y + i_Up][x + i_Left] == "D") ? 2.0f : -1.0f);
            }
        }

        return l_TileList;
    }

    //Nhận số lượng phần tử trong Ma trận
    public int Get_CountTile()
    {
        return (i_Up + i_Down + 1) * (i_Left + i_Right + 1);
    }

    //------------------------------------------------------------------------ 

    //Trả về phần trăm (0..1) theo hướng Trên
    public float Get_Percent_Dir_Up(bool b_EmtyCheck, bool b_NormalCheck, bool b_DangerCheck)
    {
        int i_Full = i_Up * (i_Left + i_Right + 1);
        int i_Check = 0;
        for (int y = -i_Up; y <= -1; y++) 
        {
            //Up >> 0
            for (int x = -i_Left; x <= i_Right; x++)
            {
                //Left >> Right
                if (
                    b_EmtyCheck && l2_Matrix[y + i_Up][x + i_Left] == "E" ||
                    b_NormalCheck && l2_Matrix[y + i_Up][x + i_Left] == "N" ||
                    b_DangerCheck && l2_Matrix[y + i_Up][x + i_Left] == "D"
                    )
                    i_Check++;
            }
        }
        return 1.0f * i_Check / i_Full;
    }

    //Trả về phần trăm (0..1) theo hướng Dưới
    public float Get_Percent_Dir_Down(bool b_EmtyCheck, bool b_NormalCheck, bool b_DangerCheck)
    {
        int i_Full = i_Down * (i_Left + i_Right + 1);
        int i_Check = 0;
        for (int y = 1; y <= i_Down; y++)
        {
            //1 >> Down
            for (int x = -i_Left; x <= i_Right; x++)
            {
                //Left >> Right
                if (
                    b_EmtyCheck && l2_Matrix[y + i_Up][x + i_Left] == "E" ||
                    b_NormalCheck && l2_Matrix[y + i_Up][x + i_Left] == "N" ||
                    b_DangerCheck && l2_Matrix[y + i_Up][x + i_Left] == "D"
                    )
                    i_Check++;
            }
        }
        return 1.0f * i_Check / i_Full;
    }

    //Trả về phần trăm (0..1) theo hướng Trái
    public float Get_Percent_Dir_Left(bool b_EmtyCheck, bool b_NormalCheck, bool b_DangerCheck)
    {
        int i_Full = (i_Down + i_Up + 1) * i_Left;
        int i_Check = 0;
        for (int y = -i_Up; y <= i_Down; y++)
        {
            //Up >> Down
            for (int x = -i_Left; x <= -1; x++)
            {
                //Left >> 0
                if (
                    b_EmtyCheck && l2_Matrix[y + i_Up][x + i_Left] == "E" ||
                    b_NormalCheck && l2_Matrix[y + i_Up][x + i_Left] == "N" ||
                    b_DangerCheck && l2_Matrix[y + i_Up][x + i_Left] == "D"
                    )
                    i_Check++;
            }
        }
        return 1.0f * i_Check / i_Full;
    }

    //Trả về phần trăm (0..1) theo hướng Phải
    public float Get_Percent_Dir_Right(bool b_EmtyCheck, bool b_NormalCheck, bool b_DangerCheck)
    {
        int i_Full = (i_Down + i_Up + 1) * i_Right;
        int i_Check = 0;
        for (int y = -i_Up; y <= i_Down; y++)
        {
            //Up >> Down
            for (int x = 1; x <= i_Right; x++)
            {
                //1 >> Right
                if (
                    b_EmtyCheck && l2_Matrix[y + i_Up][x + i_Left] == "E" ||
                    b_NormalCheck && l2_Matrix[y + i_Up][x + i_Left] == "N" ||
                    b_DangerCheck && l2_Matrix[y + i_Up][x + i_Left] == "D"
                    )
                    i_Check++;
            }
        }
        return 1.0f * i_Check / i_Full;
    }

    //Trả về phần trăm (0..1) theo góc phần tư I
    public float Get_Percent_Cir_One(bool b_EmtyCheck, bool b_NormalCheck, bool b_DangerCheck)
    {
        int i_Full = i_Up * i_Right;
        int i_Check = 0;
        for (int y = -i_Up; y <= -1; y++)
        {
            //Up >> 0
            for (int x = 1; x <= i_Right; x++)
            {
                //0 >> Right
                if (
                    b_EmtyCheck && l2_Matrix[y + i_Up][x + i_Left] == "E" ||
                    b_NormalCheck && l2_Matrix[y + i_Up][x + i_Left] == "N" ||
                    b_DangerCheck && l2_Matrix[y + i_Up][x + i_Left] == "D"
                    )
                    i_Check++;
            }
        }
        return 1.0f * i_Check / i_Full;
    }

    //Trả về phần trăm (0..1) theo góc phần tư II
    public float Get_Percent_Cir_Two(bool b_EmtyCheck, bool b_NormalCheck, bool b_DangerCheck)
    {
        int i_Full = i_Up * i_Left;
        int i_Check = 0;
        for (int y = -i_Up; y <= -1; y++)
        {
            //Up >> 0
            for (int x = -i_Left; x <= -1; x++)
            {
                //Left >> 0
                if (
                    b_EmtyCheck && l2_Matrix[y + i_Up][x + i_Left] == "E" ||
                    b_NormalCheck && l2_Matrix[y + i_Up][x + i_Left] == "N" ||
                    b_DangerCheck && l2_Matrix[y + i_Up][x + i_Left] == "D"
                    )
                    i_Check++;
            }
        }
        return 1.0f * i_Check / i_Full;
    }

    //Trả về phần trăm (0..1) theo góc phần tư III
    public float Get_Percent_Cir_Three(bool b_EmtyCheck, bool b_NormalCheck, bool b_DangerCheck)
    {
        int i_Full = i_Down * i_Left;
        int i_Check = 0;
        for (int y = 0; y <= i_Down; y++)
        {
            //0 >> Down
            for (int x = -i_Left; x <= -1; x++)
            {
                //Left >> 0
                if (
                    b_EmtyCheck && l2_Matrix[y + i_Up][x + i_Left] == "E" ||
                    b_NormalCheck && l2_Matrix[y + i_Up][x + i_Left] == "N" ||
                    b_DangerCheck && l2_Matrix[y + i_Up][x + i_Left] == "D"
                    )
                    i_Check++;
            }
        }
        return 1.0f * i_Check / i_Full;
    }

    //Trả về phần trăm (0..1) theo góc phần tư IV
    public float Get_Percent_Cir_Four(bool b_EmtyCheck, bool b_NormalCheck, bool b_DangerCheck)
    {
        int i_Full = i_Down * i_Right;
        int i_Check = 0;
        for (int y = 1; y <= i_Down; y++)
        {
            //0 >> Down
            for (int x = 1; x <= i_Right; x++)
            {
                //Left >> 0
                if (
                    b_EmtyCheck && l2_Matrix[y + i_Up][x + i_Left] == "E" ||
                    b_NormalCheck && l2_Matrix[y + i_Up][x + i_Left] == "N" ||
                    b_DangerCheck && l2_Matrix[y + i_Up][x + i_Left] == "D"
                    )
                    i_Check++;
            }
        }
        return 1.0f * i_Check / i_Full;
    }

    //Trả về phần trăm (0..1) theo đường thẳng hướng tâm Trên
    public float Get_Percent_Lin_Up(bool b_EmtyCheck, bool b_NormalCheck, bool b_DangerCheck)
    {
        int i_Full = i_Up;
        int i_Check = 0;
        for (int y = -i_Up; y <= 0; y++)
        {
            //0 >> Down
            for (int x = 0; x <= 0; x++)
            {
                //Left >> 0
                if (
                    b_EmtyCheck && l2_Matrix[y + i_Up][x + i_Left] == "E" ||
                    b_NormalCheck && l2_Matrix[y + i_Up][x + i_Left] == "N" ||
                    b_DangerCheck && l2_Matrix[y + i_Up][x + i_Left] == "D"
                    )
                    i_Check++;
            }
        }
        return 1.0f * i_Check / i_Full;
    }

    //Trả về phần trăm (0..1) theo đường thẳng hướng tâm Dưới
    public float Get_Percent_Lin_Down(bool b_EmtyCheck, bool b_NormalCheck, bool b_DangerCheck)
    {
        int i_Full = i_Down;
        int i_Check = 0;
        for (int y = 0; y <= i_Down; y++)
        {
            //0 >> Down
            for (int x = 0; x <= 0; x++)
            {
                //Left >> 0
                if (
                    b_EmtyCheck && l2_Matrix[y + i_Up][x + i_Left] == "E" ||
                    b_NormalCheck && l2_Matrix[y + i_Up][x + i_Left] == "N" ||
                    b_DangerCheck && l2_Matrix[y + i_Up][x + i_Left] == "D"
                    )
                    i_Check++;
            }
        }
        return 1.0f * i_Check / i_Full;
    }

    //Trả về phần trăm (0..1) theo đường thẳng hướng tâm Trái
    public float Get_Percent_Lin_Left(bool b_EmtyCheck, bool b_NormalCheck, bool b_DangerCheck)
    {
        int i_Full = i_Left;
        int i_Check = 0;
        for (int y = 0; y <= 0; y++)
        {
            //0 >> Down
            for (int x = -i_Left; x <= 0; x++)
            {
                //Left >> 0
                if (
                    b_EmtyCheck && l2_Matrix[y + i_Up][x + i_Left] == "E" ||
                    b_NormalCheck && l2_Matrix[y + i_Up][x + i_Left] == "N" ||
                    b_DangerCheck && l2_Matrix[y + i_Up][x + i_Left] == "D"
                    )
                    i_Check++;
            }
        }
        return 1.0f * i_Check / i_Full;
    }

    //Trả về phần trăm (0..1) theo đường thẳng hướng tâm Phải
    public float Get_Percent_Lin_Right(bool b_EmtyCheck, bool b_NormalCheck, bool b_DangerCheck)
    {
        int i_Full = i_Left;
        int i_Check = 0;
        for (int y = 0; y <= 0; y++)
        {
            //0 >> Down
            for (int x = 0; x <= i_Right; x++)
            {
                //Left >> 0
                if (
                    b_EmtyCheck && l2_Matrix[y + i_Up][x + i_Left] == "E" ||
                    b_NormalCheck && l2_Matrix[y + i_Up][x + i_Left] == "N" ||
                    b_DangerCheck && l2_Matrix[y + i_Up][x + i_Left] == "D"
                    )
                    i_Check++;
            }
        }
        return 1.0f * i_Check / i_Full;
    }
}
