using System;
using System.Collections.Generic;
using Netologia.Necro.Units;
using UnityEngine;
using Netologia.Necro.Settings;

namespace Netologia.Necro.Managers
{
    public class CellManager : MonoBehaviour
    {
        private Dictionary<CellNeighbour, Cell> _neighbours;
        private Cell[] _cells;
        private Cell _selectedCell;

        [SerializeField]
        private CellPaletteSettings _cellPaletteSettings;

        public event Action<Cell> OnCellClicked;

        public void SelectCell(Cell cell)
        {
            if (cell == null)
                return;

            if (_selectedCell != null)
                _selectedCell.ResetSelect();

            _selectedCell = cell;

            _selectedCell.SetSelect(_cellPaletteSettings.SelectCell);
        }

        public Cell GetNeighbour(Cell cell, NeighbourType type)
        {
            var key = new CellNeighbour(type, cell);

            return _neighbours.TryGetValue(key, out var neighbour)
                ? neighbour
                : null;
        }

        private void Awake()
        {
            _cells = FindObjectsOfType<Cell>();
            _neighbours = new Dictionary<CellNeighbour, Cell>(_cells.Length * 8);
            var positions = Array.ConvertAll(_cells, t => t.transform.position);
            for (int i = 0, iMax = _cells.Length; i < iMax; i++)
            {
                _cells[i].OnPointerClickEvent += HandleCellClicked;

#if UNITY_EDITOR
                _cells[i].OnPointerClickEvent += DebugOnPointerClick;
#endif

                for (int j = 0, jMax = _cells.Length; j < jMax; j++)
                {
                    if (i == j) continue;
                    var source = positions[i];
                    var destination = positions[j];

                    var forward = destination.z.CompareTo(source.z);
                    var right = destination.x.CompareTo(source.x);
                    var type = (forward, right) switch
                    {
                        (1, 1) => NeighbourType.ForwardRight,
                        (1, 0) => NeighbourType.Forward,
                        (1, -1) => NeighbourType.ForwardLeft,
                        (0, 1) => NeighbourType.Right,
                        (0, -1) => NeighbourType.Left,
                        (-1, 1) => NeighbourType.BackwardRight,
                        (-1, 0) => NeighbourType.Backward,
                        (-1, -1) => NeighbourType.BackwardLeft,
                        _ => default
                    };

                    var key = new CellNeighbour(type, _cells[i]);
                    var check = _neighbours.TryGetValue(key, out var cell)
                        ? Vector3.Distance(source, cell.transform.position) : float.MaxValue;

                    var currentDistance = Vector3.Distance(source, destination);

                    if (currentDistance < check)
                    {
                        _neighbours[key] = _cells[j];
                    }

                }
            }

            var units = FindObjectsOfType<Unit>();

            foreach (var unit in units)
            {
                Cell closestCell = null;
                float minDistance = float.MaxValue;

                foreach (var cell in _cells)
                {
                    float unitDistance = Vector2.Distance(
                        new Vector2(unit.transform.position.x, unit.transform.position.z),
                        new Vector2(cell.transform.position.x, cell.transform.position.z)
                    );

                    if (unitDistance < minDistance)
                    {
                        minDistance = unitDistance;
                        closestCell = cell;
                    }
                }

                if (closestCell != null)
                {
                    unit.SetCell(closestCell);
                }
            }
        }

#if UNITY_EDITOR
        private void HandleCellClicked(Cell cell)
        {
            SelectCell(cell);
            OnCellClicked?.Invoke(cell);
        }

#if UNITY_EDITOR
        private void DebugOnPointerClick(Cell cell)
        {
            Debug.Log("Нажата клетка: " + cell.name);
        }
#endif
#endif
    }
}