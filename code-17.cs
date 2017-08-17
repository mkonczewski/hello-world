    public class MainBoard
    {
        private Random random = new Random();
        private Stopwatch sw = new Stopwatch();
        private List<long> pA = new List<long>();
        private List<long> pB = new List<long>();

        private int minArryaSize = 10;
        private int maxArraySize = 50;

        public void RunDiagnostics()
        {
            CellSize = new Size(2, 2);
            colors = new Brush[] { Brushes.White, Brushes.Black };
            startColumn = 0;
            startRow = 0;

            for (int i = 0; i < 500; i++)
            {
                _RunDiagnostics();
            }

            Console.WriteLine("{0:f2}%", DoMeasures() * 100);
        }

        private double DoMeasures()
        {
            List<double> ls = new List<double>();

            for (int i = 0; i < pA.Count; i++)
            {
                ls.Add(1.0 * pB[i] / pA[i]);
            }

            return ls.Average();
        }

        private void _RunDiagnostics()
        {
            Init();

            Action actA = PaintBoardA;
            Action actB = PaintBoardB;

            if (random.Next(0, 2) == 0)
            {
                sw.Restart();
                actA();
                sw.Stop();
                pA.Add(sw.ElapsedTicks);

                sw.Restart();
                actB();
                sw.Stop();
                pB.Add(sw.ElapsedTicks);
            }
            else
            {
                sw.Restart();
                actB();
                sw.Stop();
                pB.Add(sw.ElapsedTicks);

                sw.Restart();
                actA();
                sw.Stop();
                pA.Add(sw.ElapsedTicks);
            }


        }

        private void Init()
        {
            if (g != null)
            {
                g.Dispose();
                g = null;
            }

            if (bmp != null)
            {
                bmp.Dispose();
                bmp = null;
            }

            bmp = new Bitmap(1200, 900);
            g = Graphics.FromImage(bmp);

            int size = random.Next(minArryaSize, maxArraySize);
            cellArray = new byte[size, size];

            for (int i = 0; i < size * 2; i++)
            {
                cellArray[random.Next(0, size), random.Next(0, size)] = 1;
            }
        }

        public Bitmap bmp { get; set; }
        public byte[,] cellArray { get; set; }
        public int startRow { get; set; }
        public int startColumn { get; set; }
        public Brush[] colors;
        public Size CellSize { get; set; }
        public Graphics g { get; set; }

        /// <summary>
        /// Paint entire board and the grid. Not suitable for small arrays (eg. size < 10)
        /// </summary>
        /// <param name="position">Determines where to draw</param>
        /// <param name="displayedRows">Determines how many rows to draw.</param>
        /// <param name="displayedColumns">Determines haw many columns to draw.</param>
        public void PaintBoardA()
        {
            int displayedRows = cellArray.GetLength(0);
            int displayedColumns = cellArray.GetLength(1);
            int startPositionX = 100;
            int startPositionY = 80;
            byte currentItem;
            byte nextItem;
            int rectangleWidth;
            int startX;

            for (int i = 0; i < displayedRows; i++)
            {
                currentItem = cellArray[i + startRow, startColumn];
                startX = startPositionX;
                rectangleWidth = CellSize.Width;
                for (int j = 1; j < displayedColumns; j++)
                {
                    nextItem = cellArray[i + startRow, j + startColumn];

                    if (currentItem == nextItem)
                    {
                        rectangleWidth += CellSize.Width;
                    }
                    else
                    {
                        g.FillRectangle(colors[currentItem],
                           startX, startPositionY + i * CellSize.Height, rectangleWidth, CellSize.Height);

                        currentItem = nextItem;
                        startX = startPositionX + j * CellSize.Width;
                        rectangleWidth = CellSize.Width;
                    }
                }

                g.FillRectangle(colors[currentItem],
                           startX, startPositionY + i * CellSize.Height, rectangleWidth, CellSize.Height);
            }
        }

        private void _PainBoardC(int startThreadRow, int endThreadRows)
        {
            int displayedColumns = cellArray.GetLength(1);
            int startPositionX = 100;
            int startPositionY = 80;
            byte currentItem;
            byte nextItem;
            int rectangleWidth;
            int startX;

            for (int i = startThreadRow; i < endThreadRows; i++)
            {
                currentItem = cellArray[i + startRow, startColumn];
                startX = startPositionX;
                rectangleWidth = CellSize.Width;
                for (int j = 1; j < displayedColumns; j++)
                {
                    nextItem = cellArray[i + startRow, j + startColumn];

                    if (currentItem == nextItem)
                    {
                        rectangleWidth += CellSize.Width;
                    }
                    else
                    {
                        g.FillRectangle(colors[currentItem],
                           startX, startPositionY + i * CellSize.Height, rectangleWidth, CellSize.Height);

                        currentItem = nextItem;
                        startX = startPositionX + j * CellSize.Width;
                        rectangleWidth = CellSize.Width;
                    }
                }

                g.FillRectangle(colors[currentItem],
                           startX, startPositionY + i * CellSize.Height, rectangleWidth, CellSize.Height);
            }
        }

        public void PaintBoardC()
        {
            int middle = cellArray.GetLength(0) / 2;

            Thread t1 = new Thread(() => _PainBoardC(0, middle));
            Thread t2 = new Thread(() => _PainBoardC(middle, cellArray.GetLength(0)));

            t1.Start();
            t2.Start();
        }

        public void PaintBoardB()
        {
            int displayedRows = cellArray.GetLength(0);
            int displayedColumns = cellArray.GetLength(1);
            int startPositionX = 100;
            int startPositionY = 80;

            for (int i = 0; i < displayedRows; i++)
            {
                for (int j = 0; j < displayedColumns; j++)
                {
                    g.FillRectangle(colors[cellArray[i + startRow, j + startColumn]], startPositionX + j * CellSize.Width, startPositionY + i * CellSize.Height, CellSize.Width, CellSize.Height);
                }
            }
        }
    }