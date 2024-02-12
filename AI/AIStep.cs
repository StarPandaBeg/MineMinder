using MineMinderX.Game;

namespace MineMinderX.AI
{
    public abstract class AIStep
    {
        public bool HasNext => Next != null;
        public AIStep Next { get; set; }
        public abstract bool Solve(IBoard board, out BoardAI.SolverAction action);

        protected bool Chain(IBoard board, out BoardAI.SolverAction action) {
            if (HasNext) return Next.Solve(board, out action);
            
            action = new BoardAI.SolverAction();
            return false;
        }
    }
}