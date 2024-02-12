using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using MineMinderX.Game;

namespace MineMinderX.GUI
{
    public class BoardRenderer
    {
        private IEventBoard _board;
        private Panel _canvas;
        
        private static readonly Pen CellBorderPen = new Pen(Color.Black);
        private static readonly Brush CellDefaultBrush = new SolidBrush(Color.LightSlateGray);
        private static readonly Brush CellOpenedBrush = new SolidBrush(Color.White);

        private static readonly Font CellLabelFont = new Font("Arial", 10, FontStyle.Bold);
        private static readonly StringFormat CellLabelFormat;

        private static readonly Color[] CellLabelColors = {
            Color.Blue,
            Color.DarkGreen,
            Color.Red,
            Color.DarkBlue,
            Color.Magenta,
            Color.IndianRed,
            Color.YellowGreen,
            Color.DarkCyan,
        };

        static BoardRenderer() {
            CellLabelFormat = new StringFormat();
            CellLabelFormat.Alignment = StringAlignment.Center;
            CellLabelFormat.LineAlignment = StringAlignment.Center;
        }

        public BoardRenderer(IEventBoard board, Panel canvas) {
            _board = board;
            _canvas = canvas;
            
            _board.CellUpdated += OnCellUpdated;
            _canvas.Paint += OnCanvasPaint;
        }

        private void OnCellUpdated(Vector2 obj) {
            _canvas.Invalidate();
        }

        private void OnCanvasPaint(object sender, PaintEventArgs e) {
            if (_board == null) return;

            var offsetX = (int)(e.ClipRectangle.Width / 2f - _board.Size.X * 16 / 2);
            var offsetY = (int)(e.ClipRectangle.Height / 2f - _board.Size.Y * 16 / 2);
            
            for (int i = 0; i < _board.Size.Y; i++) {
                for (int j = 0; j < _board.Size.X; j++) {
                    var cell = _board.Get(new Vector2(j, i));
                    if (cell == null) continue;
                    
                    PaintCell(e.Graphics, offsetX + j * 16, offsetY + i * 16, cell);
                }
            }
        }

        private void PaintCell(Graphics g, int x, int y, Cell cell) {
            var opened = (cell.State == Cell.CellState.Opened);
            var backgroundBrush = opened ? CellOpenedBrush : CellDefaultBrush;
            g.FillRectangle(backgroundBrush, x, y, 16, 16);
            
            var symbol = GetLabelForCell(cell);
            var color = GetLabelColorForCell(cell);
            var brush = new SolidBrush(color);
            
            g.DrawString(symbol, CellLabelFont, brush, x + 16 / 2, y + 16 / 2, CellLabelFormat);
            g.DrawRectangle(CellBorderPen, x, y, 16, 16);
        }
        
        private string GetLabelForCell(Cell cell) {
            if (cell.Type == Cell.CellType.Bomb) return "B";
            if (cell.State == Cell.CellState.Flagged) return "F";
            if (cell.State == Cell.CellState.Covered) return " ";
            
            return cell.Type switch
            {
                Cell.CellType.Numeric => cell.Value.ToString(),
                Cell.CellType.Bomb => "B",
                Cell.CellType.Unknown => "?",
                _ => " ",
            };
        }
        
        private Color GetLabelColorForCell(Cell cell) {
            if (cell.State == Cell.CellState.Flagged) {
                return Color.Red;
            }
            if (cell.Type == Cell.CellType.Numeric) {
                return CellLabelColors[(cell.Value - 1) % CellLabelColors.Length];
            }

            return Color.Black;
        }
    }
}