using System.Linq;
using System.Numerics;
using MineMinderX.Game;

namespace MineMinderX.AI.Steps
{
    public class OpenStep : AIStep
    {
        public override bool Solve(IBoard board, out BoardAI.SolverAction action) {
            action = new BoardAI.SolverAction();

            for (var i = 0; i < board.Size.Y; i++) {
                for (var j = 0; j < board.Size.X; j++) {
                    var position = new Vector2(j, i);
                    var cell = board.Get(position);

                    if (cell.State != Cell.CellState.Covered) return Chain(board, out action);
                }
            }

            var cellX = (int)board.Size.X / 2;
            var cellY = (int)board.Size.Y / 2;
            action.Position = new Vector2(cellX, cellY);
            action.IsPrimaryAction = true;
            action.NeedUpdate = true;
            
            return true;
        }
    }
}