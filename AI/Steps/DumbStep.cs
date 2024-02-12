using System.Linq;
using System.Numerics;
using MineMinderX.Game;

namespace MineMinderX.AI.Steps
{
    public class DumbStep : AIStep
    {
        public override bool Solve(IBoard board, out BoardAI.SolverAction action) {
            action = new BoardAI.SolverAction();

            for (var i = 0; i < board.Size.Y; i++) {
                for (var j = 0; j < board.Size.X; j++) {
                    var position = new Vector2(j, i);
                    var cell = board.Get(position);
                    
                    if (cell.State != Cell.CellState.Opened) continue;
                    if (cell.Type != Cell.CellType.Numeric) continue;
                    if (ProcessCell(cell, ref action)) return true;
                }
            }
            
            return Chain(board, out action);
        }

        private bool ProcessCell(Cell cell, ref BoardAI.SolverAction action) {
            var covered = 0;
            var marked = 0;
            
            foreach (var neighbor in cell.Neighbors) {
                if (neighbor?.State == Cell.CellState.Covered) covered++;
                if (neighbor?.State == Cell.CellState.Flagged) marked++;
            }
            if (covered == 0) return false;
            
            // Если не открыто ровно столько, сколько бомб окружает клетку - ставим флаг
            if (cell.Value - marked == covered) {
                action.IsPrimaryAction = false;
                action.Position = cell.Neighbors.First(n => n is { State: Cell.CellState.Covered }).Position;
                return true;
            }
            
            // Если помечены флагом все бомбы - открываем оставшееся
            if (cell.Value == marked && covered > 0) {
                action.IsPrimaryAction = true;
                action.Position = cell.Neighbors.First(n => n is { State: Cell.CellState.Covered }).Position;
                action.NeedUpdate = true; // Нужно перечитать поле - мы не знаем что было в открытой клетке
                return true;
            }
            
            return false;
        }
    }
}