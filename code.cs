		/// <summary>
        /// This method is used to paint cells on the borad using 'Constant' display type.
        /// This display mode means that the number of rows and columns is constant and does not depend 
        /// on the size of the board
        /// </summary>
        /// <param name="g"></param>
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

        private void DrawCellsUsingAutoDisplayType(Graphics g)
        {
            int arrayRowNumber = cellArray.GetLength(0) - startRow; // find maximum number of rows in array.
            int arrayColNumber = cellArray.GetLength(1) - startColumn; // find maximum number of columns in array.

            Rows = Height / CellSize.Height; // find maximum (visible) number of rows
            Columns = Width / CellSize.Width; // find maximum (visible) number of columns

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
            int startPositionX = position.X;
            int startPositionY = position.Y;

			int lastRowIndex = startRow + displayedRows;
			int lastColumnIndex = startColumn + displayedColumns

            for (int i = startRow; i < lastRowIndex; i++) 
            {
                for (int j = startColumn; j < lastColumnIndex; j++)
                {
                    g.FillRectangle(colors[cellArray[i, j]], startPositionX + j * CellSize.Width, startPositionY + i * CellSize.Height, CellSize.Width, CellSize.Height);
                }
            }

            PaintGrid(g, startPositionX, startPositionY, displayedColumns, displayedRows);
        }

		// Paints grid on the board
		// g - Graphics used to draw lines
		// startX - x  position measuren in pixels, determines top left corner of the grid
		// startY - y  position measuren in pixels, determines top left corner of the grid
		// rows - determines how many rows (horizontal lines) to draw
		// columns - determines how many columns (vertical lines) to draw
        private void PaintGrid(Graphics g, int startX, int startY, int rows, int columns)
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
                    startPositionY = (Height - height / 2);
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
    }