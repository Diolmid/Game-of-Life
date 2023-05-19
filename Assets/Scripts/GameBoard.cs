using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private float _updateInterval = 0.05f;
    [SerializeField, Space] private Tilemap _currentState;
    [SerializeField] private Tilemap _nextState;
    [SerializeField, Space] private Tile _aliveTile;
    [SerializeField, Space] private Pattern _pattern;

    private HashSet<Vector3Int> _aliveCells;
    private HashSet<Vector3Int> _cellsToCheck;

    private void Awake()
    {
        _aliveCells = new HashSet<Vector3Int>();
        _cellsToCheck = new HashSet<Vector3Int>();
    }

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
            var cell = (Vector3Int)(_pattern.cells[i] - center);
            _currentState.SetTile(cell, _aliveTile);
            _aliveCells.Add(cell);
        }
    }

    private void Clear()
    {
        _currentState.ClearAllTiles();
        _nextState.ClearAllTiles();
    }

    private void OnEnable()
    {
        StartCoroutine(Simulate());
    }

    private IEnumerator Simulate()
    {
        while (enabled)
        {
            UpdateCells();
            yield return new WaitForSeconds(_updateInterval);
        }
    }

    private void UpdateCells()
    {
        _cellsToCheck.Clear();
        foreach (var cell in _aliveCells)
        {
            for (int x = -1; x <= 1; x++)
            {
                for(int y = -1; y <= 1; y++)
                {
                    _cellsToCheck.Add(cell + new Vector3Int(x, y, 0));
                }
            }
        }

        foreach (var cell in _cellsToCheck)
        {
            int neighbors = CountNeighbors(cell);
            bool alive = IsAlive(cell);

            if (!alive && neighbors == 3)
            {
                _nextState.SetTile(cell, _aliveTile);
                _aliveCells.Add(cell);
            }
            else if (alive && (neighbors < 2 || neighbors > 3))
            {
                _nextState.SetTile(cell, null);
                _aliveCells.Remove(cell);
            }
            else
            {
                _nextState.SetTile(cell, _currentState.GetTile(cell));
            }
        }

        (_currentState, _nextState) = (_nextState, _currentState);
        _nextState.ClearAllTiles();
    }

    private int CountNeighbors(Vector3Int cell)
    {
        int count = 0;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                var neighbor = cell + new Vector3Int(x, y, 0);
                if(IsAlive(neighbor))
                    count++;
            }
        }

        return count;
    }

    private bool IsAlive(Vector3Int cell)
    {
        return _currentState.HasTile(cell);
    }
}