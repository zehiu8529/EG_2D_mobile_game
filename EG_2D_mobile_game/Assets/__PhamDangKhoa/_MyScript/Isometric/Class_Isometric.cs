using UnityEngine;

public class Class_Isometric
{
    #region Dir

    /// <summary>
    /// Dir(-1, 0) on Isometric Square
    /// </summary>
    public readonly Vector2Int v2_DirUp = new Vector2Int(-1, 0);

    /// <summary>
    /// Dir(+1, 0) on Isometric Square
    /// </summary>
    /// <returns></returns>
    public readonly Vector2Int v2_DirDown = new Vector2Int(1, 0);

    /// <summary>
    /// Dir(0, -1) on Isometric Square
    /// </summary>
    /// <returns></returns>
    public readonly Vector2Int v2_DirLeft = new Vector2Int(0, -1);

    /// <summary>
    /// Dir(0, +1) on Isometric Square
    /// </summary>
    /// <returns></returns>
    public readonly Vector2Int v2_DirRight = new Vector2Int(0, 1);

    #endregion

}
