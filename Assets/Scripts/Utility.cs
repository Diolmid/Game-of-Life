using UnityEngine;

public static class Utility
{
    public static Vector2Int GetCenterOfCells(Vector2Int[] cells)
    {
        if (cells == null || cells.Length == 0)
            return Vector2Int.zero;

        var min = Vector2Int.zero;
        var max = Vector2Int.zero;

        for (var i = 0; i < cells.Length; i++)
        {
            var cell = cells[i];

            min.x = Mathf.Min(cell.x, min.x);
            min.y = Mathf.Min(cell.y, min.y);
            max.x = Mathf.Max(cell.x, max.x);
            max.y = Mathf.Max(cell.y, max.y);
        }

        return (min + max) / 2;
    }
}