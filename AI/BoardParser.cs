using System.Drawing;
using System.Numerics;
using MineMinder.Recognizer;
using MineMinderX.Game;

namespace MineMinderX.AI
{
    public class BoardParser
    {
        private const int BoardPaddingL = 12;
        private const int BoardPaddingR = 8;
        private const int BoardPaddingB = 8;
        private const int BoardPaddingT = 55;
        private const int CellSizeX = 16;
        private const int CellSizeY = 16;

        private static readonly int CellCoveredColor = ColorTranslator.FromHtml("#FFFFFFFF").ToArgb();
        private static readonly int CellOpenedColor = ColorTranslator.FromHtml("#FF808080").ToArgb();
        private static readonly int CellBombColor = ColorTranslator.FromHtml("#FF000000").ToArgb();
        private static readonly int CellBombCrossColor = ColorTranslator.FromHtml("#FFFF0000").ToArgb();
        private static readonly int CellFlaggedColor = ColorTranslator.FromHtml("#FF000000").ToArgb();
        private static readonly int[] CellValueColors =
        {
            ColorTranslator.FromHtml("#FF0000FF").ToArgb(), 
            ColorTranslator.FromHtml("#FF008000").ToArgb(), 
            ColorTranslator.FromHtml("#FFFF0000").ToArgb(),
            ColorTranslator.FromHtml("#FF000080").ToArgb(),
            ColorTranslator.FromHtml("#FF800000").ToArgb(),
        };

        public void FromBitmap(IBoard board, Bitmap bmp) {
            var gameBoard = CropRawBitmap(bmp);
            var size = GetBoardSize(gameBoard);
            
            if (board.Size != size) {
                board.Init(size);
            }

            for (var i = 0; i < size.Y; i++) {
                for (var j = 0; j < size.X; j++) {
                    var position = new Vector2(j, i);
                    var cell = board.Get(position);
                    ParseCell(cell, gameBoard, j, i);
                    
                    board.Set(position, cell);
                }
            }
        }

        private Bitmap CropRawBitmap(Bitmap bmp) {
            var width = bmp.Width - BoardPaddingR - BoardPaddingL;
            var height = bmp.Height - BoardPaddingB - BoardPaddingT;

            return BitmapHelper.Crop(bmp, BoardPaddingL, BoardPaddingT, width, height);
        }

        private Vector2 GetBoardSize(Bitmap board) {
            var cellCountX = board.Width / CellSizeX;
            var cellCountY = board.Height / CellSizeY;
            return new Vector2(cellCountX, cellCountY);
        }

        private void ParseCell(Cell cell, Bitmap board, int x, int y) {
            var topLeftColor = BitmapHelper.GetColorSliced(board, CellSizeX, CellSizeY, x, y, 0, 0);
            var centerColor = BitmapHelper.GetColorSliced(board, CellSizeX, CellSizeY, x, y, 8f / CellSizeX, 8f / CellSizeY);

            if (topLeftColor.ToArgb() == CellCoveredColor) {
                if (centerColor.ToArgb() == CellFlaggedColor) {
                    cell.State = Cell.CellState.Flagged;
                    return;
                }
                
                cell.State = Cell.CellState.Covered;
                cell.Type = Cell.CellType.Unknown;
                return;
            }

            if (centerColor.ToArgb() == CellBombColor) {
                cell.State = Cell.CellState.Opened;
                cell.Type = Cell.CellType.Bomb;
                return;
            }

            if (centerColor.ToArgb() == CellBombCrossColor) {
                var topCenterColor = BitmapHelper.GetColorSliced(board, CellSizeX, CellSizeY, x, y, 8f / CellSizeX, 4f / CellSizeY);
                if (topCenterColor.ToArgb() == CellBombColor) {
                    cell.State = Cell.CellState.Opened;
                    cell.Type = Cell.CellType.Unknown;
                    return;
                }
            }

            for (var i = 0; i < CellValueColors.Length; i++) {
                if (centerColor.ToArgb() != CellValueColors[i]) continue;
                
                cell.Value = i + 1;
                cell.State = Cell.CellState.Opened;
                return;
            }

            cell.State = Cell.CellState.Opened;
            cell.Type = Cell.CellType.Blank;
        }
    }
}