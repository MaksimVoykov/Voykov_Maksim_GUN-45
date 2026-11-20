using System;

namespace Netologia.Necro
{
    public struct CellNeighbour : IEquatable<CellNeighbour>
    {
        public NeighbourType Type { get; }
        public Cell Cell { get; }

        public CellNeighbour(NeighbourType type, Cell cell)
        {
            Type = type;
            Cell = cell;
        }

        public bool Equals(CellNeighbour other)
        {
            return Type == other.Type && Cell == other.Cell;
        }

        public override bool Equals(object obj)
        {
            return obj is CellNeighbour other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Cell);
        }
    }
}