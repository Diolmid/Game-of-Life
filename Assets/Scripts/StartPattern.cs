using UnityEngine;

[CreateAssetMenu(menuName = "Game of Life/Start Pattern")]
public class StartPattern : ScriptableObject
{
    public Vector2Int[] cells;
}