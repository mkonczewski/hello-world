using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication
{
    public partial class Board : UserControl
    {
        private void DrawCellsUsingConstantDisplayType(Graphics g)
        {
            int arrayRowNumber = cellArray.GetLength(0) - startRow; // find maximum number of rows in array.
            int arrayColNumber = cellArray.GetLength(1) - startColumn; // find maximum number of columns in array.

            // the folowing lines are uset to set the value of Rows and Columns when they
            // are greater than the size of the array.
            Rows = Math.Min(Rows, arrayRowNumber); // find actual number of rows to display
            Columns = Math.Min(Columns, arrayColNumber); // find actual number of cols to display

            int maxRowNumber = Height / CellSize.Height + 1; // find maximum (visible) number of rows
            int maxColNumber = Width / CellSize.Width + 1; // find maximum (visible) number of columns

            // if the number of rows or columns is greater than the actuals size of the board 
            // then trim Rows and Columns to the size that can be displayed (because there is no need for
            // drawing cells that are not on the board).
            int displayedRows = Math.Min(Rows, maxRowNumber);
            int displayedColumns = Math.Min(Columns, maxColNumber);

            // calculate the size of the cell board (measured in pixels).
            int displayedWidth = displayedColumns * CellSize.Width;
            int displayedHeight = displayedRows * CellSize.Height;

            // get the position to draw the board.
            Point pt = CalculateStartPosition(displayedWidth, displayedHeight);
            PainBoard(g, pt, displayedRows, displayedColumns);
        }

		// TODO: Possible new function GetMaxCellDistance(type, width/hegith, cellWindth/ cellHeight)
		// returns walue based on the type
		// Height / CellSize.Height; for type = small
		// Height / CellSize.Height + 1; for type = normal
		
		// TODO: Additional check for Display -> cellBoard[,] rows = 0, cols = 0 -> check what will happended
        private void DrawCellsUsingAutoDisplayType(Graphics g)
        {
            int arrayRowNumber = cellArray.GetLength(0) - startRow; // find maximum number of rows in array.
            int arrayColNumber = cellArray.GetLength(1) - startColumn; // find maximum number of columns in array.

            int maxRowNumber = Height / CellSize.Height; // find maximum (visible) number of rows
            int maxColNumber = Width / CellSize.Width; // find maximum (visible) number of columns

            Rows = Math.Min(arrayRowNumber, maxRowNumber); // find maximum (visible) number of rows
            Columns = Math.Min(arrayColNumber, maxColNumber); // find maximum (visible) number of columns

            // calculate the size of the cell board (measured in pixels).
            int displayedWidth = Columns * CellSize.Width;
            int displayedHeight = Rows * CellSize.Height;

            // get the position to draw the board.
            Point pt = CalculateStartPosition(displayedWidth, displayedHeight);
            PainBoard(g, pt, Rows, Columns);
        }

        // Paint entire board and the grid
        // g - Graphics used to draw elements
        // position - determines where to draw
        // displayedRows - determines how many rows to draw
        // displayedColumns - determines haw many columns to draw
        private void PainBoard(Graphics g, Point position, int displayedRows, int displayedColumns)
        {
            Console.WriteLine("PaintBoard for [" + displayedRows + ", " + displayedColumns + "]");

            int startPositionX = position.X;
            int startPositionY = position.Y;

            for (int i = 0; i < displayedRows; i++)
            {
                for (int j = 0; j < displayedColumns; j++)
                {
                    g.FillRectangle(colors[cellArray[i + startRow, j + startColumn]], startPositionX + j * CellSize.Width, startPositionY + i * CellSize.Height, CellSize.Width, CellSize.Height);
                }
            }

            PaintGrid(g, startPositionX, startPositionY, displayedRows, displayedColumns);
        }

        // Paints grid on the board
        // g - Graphics used to draw lines
        // startX - x  position measuren in pixels, determines top left corner of the grid
        // startY - y  position measuren in pixels, determines top left corner of the grid
        // rows - determines how many rows (horizontal lines) to draw
        // columns - determines how many columns (vertical lines) to draw
        private void PaintGrid(Graphics g, int startX, int startY, int rows, int columns)
        {
            if (GridSize > 0)
            {
                Pen pen = new Pen(GridColor, GridSize);
                int width = columns * CellSize.Width;
                int height = rows * CellSize.Height;

                int temp;
                for (int i = 1; i < rows; i++)
                {
                    temp = i * CellSize.Height + startY;
                    g.DrawLine(pen, startX, temp, width + startX, temp);
                }

                for (int i = 1; i < columns; i++)
                {
                    temp = i * CellSize.Width + startX;
                    g.DrawLine(pen, temp, startY, temp, height + startY);
                }

                g.DrawRectangle(pen, startX, startY, width, height);
            }
        }

        // find the starting position based on board anchor type.
        private Point CalculateStartPosition(int width, int height)
        {
            int startPositionX = 0;
            int startPositionY = 0;

            switch (AnchorType)
            {
                case BoardAnchor.BottomLeft:
                    // start position at (0, y)
                    startPositionY = Height - height;
                    break;
                case BoardAnchor.BottomRight:
                    // start position at (x, y)
                    startPositionX = Width - width;
                    startPositionY = Height - height;
                    break;
                case BoardAnchor.Center:
                    // start position at (x/2, y/2)
                    startPositionX = (Width - width) / 2;
                    startPositionY = (Height - height) / 2;
                    break;
                case BoardAnchor.TopLeft:
                    // start position at (0,0)
                    break;
                case BoardAnchor.TopRight:
                    // start position at (x, 0)
                    startPositionX = Width - width;
                    break;
                default:
                    throw new NotSupportedException("Not supported value of AnchorType");
            }

            if (startPositionX < 0)
            {
                startPositionX = 0;
            }

            if (startPositionY < 0)
            {
                startPositionY = 0;
            }

            return new Point(startPositionX, startPositionY);
        }

        public Size CellSize = new Size(30, 3);
        private List<SolidBrush> colors = new List<SolidBrush>() { new SolidBrush(Color.WhiteSmoke), new SolidBrush(Color.LightGray) };
        public Color GridColor = Color.Brown;
        public int GridSize = 0;
        public int Rows = 5;
        public int Columns = 10;

        private int startRow;
        private int startColumn;
        private byte[,] cellArray;
        private BoardAnchor AnchorType = BoardAnchor.TopLeft;

        public void Display(byte[,] array, int stR, int stC)
        {
            cellArray = array;
            startRow = stR;
            startColumn = stC;

            Invalidate();
        }

        public Board()
        {
            InitializeComponent();

            byte[,] arr = {
               {1, 0, 1, 0, 1, 0, 1, 0, 1}
            };

            AnchorType = BoardAnchor.BottomLeft;
            Display(arr, 0, 0);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            //DrawCellsUsingConstantDisplayType(e.Graphics);
            DrawCellsUsingAutoDisplayType(e.Graphics);
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();
        }
    }

    public enum BoardAnchor
    {
        TopLeft,
        BottomLeft,
        TopRight,
        BottomRight,
        Center
    }
}