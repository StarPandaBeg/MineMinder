using System;
using System.Numerics;

namespace MineMinderX.Game
{
    public interface IEventBoard : IBoard
    {
        public event Action RequireUpdate;
        public event Action<Vector2> CellUpdated;
        public event Action Reinitialized;
    }
}