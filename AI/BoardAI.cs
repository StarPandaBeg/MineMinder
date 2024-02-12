using System.Numerics;
using System.Threading;
using MineMinderX.AI.Steps;
using MineMinderX.Game;
using MineMinderX.Process;
using MineMinderX.Util;

namespace MineMinderX.AI
{
    public class BoardAI
    {
        private IBoard _board;
        private AIStep _step;
        private ProcessData _processData;
        private bool _isAutoMode = false;

        public BoardAI(IBoard board, ProcessData data) {
            _board = board;

            RegisterStep(new CheckStep());
            RegisterStep(new DumbStep());
        }

        public void UpdateProcessData(ProcessData data) {
            _processData = data;
            
            if (_isAutoMode) {
                Focus();
            }
        }

        public void DoWork() {
            _isAutoMode = true;
            Focus();
            
            while (StepLogic()) {
                Thread.Sleep(10);
            }
            _isAutoMode = false;
        }

        public bool Step() {
            if (_isAutoMode) return false;
            Focus();
            return StepLogic();
        }

        private bool StepLogic() {
            var result = Step(_board, out var action);
            if (!result) return false;
            
            GUILog.Log(action.Position + " - " + (action.IsPrimaryAction ? "Открыта клетка" : "Поставлен флаг"));
            
            if (action.IsPrimaryAction) _board.Open(action.Position);
            else _board.PlaceFlag(action.Position);
            
            if (action.NeedUpdate) _board.Update();
            return true;
        }

        private bool Step(IBoard b, out SolverAction action) {
            return _step.Solve(b, out action);
        }

        private void RegisterStep(AIStep step) {
            if (_step == null) {
                _step = step;
                return;
            }

            var parentStep = _step;
            while (parentStep.HasNext) parentStep = parentStep.Next;
            parentStep.Next = step;
        }

        private void Focus() {
            WindowsHelper.SetForegroundWindow(_processData.Hwnd);
            WindowsHelper.SetFocus(_processData.Hwnd);
        }
        
        public struct SolverAction
        {
            public Vector2 Position;
            public bool IsPrimaryAction;
            public bool NeedUpdate;
        }
    }
}