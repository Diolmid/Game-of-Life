using UnityEngine;
using UnityEngine.Tilemaps;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private float _updateInterval = 0.05f;
    [SerializeField] private Tilemap _currentState;
    [SerializeField] private Tilemap _nextState;
    [SerializeField] private Tile _aliveTile;
    [SerializeField] private Tile _deadTile;
    [SerializeField] private Pattern _pattern;

    private void Start()
    {
        SetPattern(_pattern);
    }

    private void SetPattern(Pattern pattern)
    {
        Clear();

        var center = _pattern.GetCenter();

        for (int i = 0; i < _pattern.cells.Length; i++)
        {
            var cell = _pattern.cells[i] - center;
            _currentState.SetTile((Vector3Int)cell, _aliveTile);
        }
    }

    private void Clear()
    {
        _currentState.ClearAllTiles();
        _nextState.ClearAllTiles();
    }
}