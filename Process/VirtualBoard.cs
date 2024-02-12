using System;
using System.Numerics;
using System.Threading;
using MineMinderX.Game;
using MineMinderX.Util;
using WindowsInput.Events;

namespace MineMinderX.Process
{
    public class VirtualBoard : IEventBoard
    {
        public event Action RequireUpdate;
        public event Action<Vector2> CellUpdated;
        public event Action Reinitialized;
        
        public static readonly Vector2 CellSize = new(16,16);

        public Vector2 Size => _size;
        public int TotalMines => int.MaxValue;

        private Cell[][] _board;
        private ProcessData _processData;
        private Vector2 _size = new (0, 0);

        private Mutex _boardLock = new();

        public VirtualBoard(ProcessData data) {
            _processData = data;
        }

        public void UpdateProcessData(ProcessData data) {
            _processData = data;
        }
        
        public void Init(Vector2 size) {
            lock (_boardLock) {
                _size = size;
                _board = new Cell[(int)size.Y][];
                for (var i = 0; i < size.Y; i++) {
                    _board[i] = new Cell[(int)size.X];
                
                    for (var j = 0; j < size.X; j++) {
                        _board[i][j] = new Cell(new Vector2(j, i));

                        if (i > 0) {
                            _board[i][j].SetNeighbor(0, _board[i-1][j]);
                            if (j > 0) {
                                _board[i][j].SetNeighbor(7, _board[i-1][j-1]);
                            }
                            if (j < size.X - 1) {
                                _board[i][j].SetNeighbor(1, _board[i - 1][j + 1]);
                            }
                        }

                        if (j > 0) {
                            _board[i][j].SetNeighbor(6, _board[i][j-1]);
                        }
                    }
                }
            }
            Reinitialized?.Invoke();
        }

        public void Update() {
            RequireUpdate?.Invoke();
        }

        public Cell Get(Vector2 position) {
            if (position.X < 0 || position.X >= Size.X) return null;
            if (position.Y < 0 || position.Y >= Size.Y) return null;
            
            lock (_boardLock) {
                return _board[(int)position.Y][(int)position.X];
            }
        }
        
        public void Set(Vector2 position, Cell cell) {
            lock (_boardLock) {
                _board[(int)position.Y][(int)position.X] = cell;
            }
            CellUpdated?.Invoke(position);
        }

        public void Open(Vector2 position) {
            if (Get(position).State == Cell.CellState.Opened) return;
            _board[(int)position.Y][(int)position.X].State = Cell.CellState.Opened;

            var relX = (int)(position.X * CellSize.X + CellSize.X / 2);
            var relY = (int)(position.Y * CellSize.Y + CellSize.Y / 2);

            var x = _processData.BoardRect.Left + relX;
            var y = _processData.BoardRect.Top + relY;
            
            WindowsInput.Simulate.Events().MoveTo(x, y).Click(ButtonCode.Left).Invoke().Wait();
            CellUpdated?.Invoke(position);
        }

        public void PlaceFlag(Vector2 position) {
            if (Get(position).State == Cell.CellState.Flagged) return;
            _board[(int)position.Y][(int)position.X].State = Cell.CellState.Flagged;

            var relX = (int)(position.X * CellSize.X + CellSize.X / 2);
            var relY = (int)(position.Y * CellSize.Y + CellSize.Y / 2);

            var x = _processData.BoardRect.Left + relX;
            var y = _processData.BoardRect.Top + relY;

            WindowsHelper.SetForegroundWindow(_processData.Hwnd);
            WindowsHelper.SetFocus(_processData.Hwnd);
            WindowsInput.Simulate.Events().MoveTo(x, y).Click(ButtonCode.Right).Invoke().Wait();
            CellUpdated?.Invoke(position);
        }

        public void TakeFlag(Vector2 position) {
            if (Get(position).State != Cell.CellState.Flagged) return;
            _board[(int)position.Y][(int)position.X].State = Cell.CellState.Covered;

            var relX = (int)(position.X * CellSize.X + CellSize.X / 2);
            var relY = (int)(position.Y * CellSize.Y + CellSize.Y / 2);

            var x = _processData.BoardRect.Left + relX;
            var y = _processData.BoardRect.Top + relY;

            WindowsHelper.SetForegroundWindow(_processData.Hwnd);
            WindowsHelper.SetFocus(_processData.Hwnd);
            WindowsInput.Simulate.Events().MoveTo(x, y).Click(ButtonCode.Right).Invoke().Wait();
            CellUpdated?.Invoke(position);
        }
    }
}