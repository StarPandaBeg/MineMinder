using System.ComponentModel;
using System.Threading;
using MineMinderX.Game;
using MineMinderX.Process;
using MineMinderX.Util;

namespace MineMinderX.AI
{
    public class BoardWorker
    {
        private const int RefreshAfterTicks = 40;
        
        private VirtualBoard _board;
        private ProcessData _processData;
        private bool _boardInitialized = false;
        private bool _boardNeedToUpdate = false;

        public IEventBoard Board => _board;
        private bool NeedToRefresh => !_boardInitialized || _boardNeedToUpdate;

        public BoardWorker() {
            _board = new VirtualBoard(null);
            _board.RequireUpdate += BoardOnRequireUpdate;
        }

        public void DoWork(object sender, DoWorkEventArgs e) {
            var worker = (BackgroundWorker)sender;
            var counter = 1;
            
            while (!worker.CancellationPending) {
                if (counter % RefreshAfterTicks == 0) { 
                    _boardNeedToUpdate = true;
                }
                
                Tick(worker, e, counter++);
                Thread.Sleep(50);
            }
        }

        public void UpdateProcessData(ProcessData data) {
            _processData = data;
            _board.UpdateProcessData(data);

            _boardInitialized = false;
        }

        private void Tick(BackgroundWorker worker, DoWorkEventArgs e, int tickCounter) {
            if (_processData == null) return;

            if (NeedToRefresh) {
                RefreshBoard();
            }
        }

        private void RefreshBoard() {
            var winScreenshot = WindowsHelper.PrintClientWindow(_processData.Hwnd);
            
            var parser = new BoardParser();
            parser.FromBitmap(_board, winScreenshot);
            
            _boardInitialized = true;
            _boardNeedToUpdate = false;
        }

        private void BoardOnRequireUpdate() {
            RefreshBoard();
        }
    }
}