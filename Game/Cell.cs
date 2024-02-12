using System.Numerics;

namespace MineMinderX.Game
{
    public class Cell
    {
        public readonly Vector2 Position;
        public CellType Type { get; set; }
        public CellState State { get; set; }
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                Type = CellType.Numeric;
            }
        }
        /*
         * Neighbor order:
         * 701
         * 6 2
         * 543
         */
        public Cell[] Neighbors { get; set; } = new Cell[8];
        
        private int _value;

        public Cell(Vector2 position) {
            Position = position;
        }
        
        public void SetNeighbor(int index, Cell neighbor) {
            Neighbors[index] = neighbor;
            
            var otherIndex = (index + 4) % 8;
            neighbor.Neighbors[otherIndex] = this;
        }
        
        public enum CellType
        {
            Unknown,
            Blank,
            Numeric,
            Bomb
        }
        
        public enum CellState
        {
            Covered,
            Flagged,
            Opened
        }
    }
}