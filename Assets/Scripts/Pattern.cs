using UnityEngine;

[CreateAssetMenu(menuName = "Game of Life/Start Pattern")]
public class Pattern : ScriptableObject
{
    public Vector2Int[] cells;

    public Vector2Int GetCenter()
    {
        if (cells == null || cells.Length == 0)
            return Vector2Int.zero;

        var min = Vector2Int.zero;
        var max = Vector2Int.zero;

        for (var i = 0; i < cells.Length; i++)
        {
            var cell = cells[i];

            min.x = Mathf.Min(cell.x, min.x);
            min.y = Mathf.Min (cell.y, min.y);
            max.x = Mathf.Max(cell.x, max.x);
            max.y = Mathf.Max(cell.y, max.y);
        }

        return (min + max) / 2;
    }
}