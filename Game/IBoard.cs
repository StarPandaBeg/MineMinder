using System.Numerics;

namespace MineMinderX.Game
{
    public interface IBoard
    {
        public Vector2 Size { get; }
        public int TotalMines { get; }

        public void Init(Vector2 size);
        public void Update();
        public Cell Get(Vector2 position);
        public void Set(Vector2 position, Cell cell);
        public void Open(Vector2 position);
        public void PlaceFlag(Vector2 position);
        public void TakeFlag(Vector2 position);
    }
}