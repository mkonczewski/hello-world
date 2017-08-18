using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApplication
{
	public class PresenceClass
	{
		private int[] prn = {
			1, 1, 0, 0,
			1, 0,
			0, 0, 0,
			1, 1, 0,
			1, 0, 1,
		};
		
		private int Sum = 8;
		
		public PresenceClass()
		{
			
		}
	}
	
    /// <summary>
    /// Performs diagnostic measurments.
    /// </summary>
    public abstract class DiagnosticsHelper
    {
        // random number generator
        private Random random = new Random();

        // stopwatch
        private Stopwatch sw = new Stopwatch();

        // times.
        private List<long> pA = new List<long>();
        private List<long> pB = new List<long>();

        // The value of Iterations.
        private int _Iterations = 10;

        /// <summary>
        /// Gets or sets total number of iterations. 10 by default.
        /// </summary>
        public int Iterations
        {
            get { return _Iterations; }
            set { _Iterations = value; }
        }

        /// <summary>
        /// Main method for performance tests. Returns percentege for Method_A to Method_B. 
        /// The higher the value the better performance of Method_A. 
        /// </summary>
        public int RunDiagnostics()
        {
            pA.Clear();
            pB.Clear();

            StartUp();

            for (int i = 0; i < Iterations; i++)
            {
                Update();

                _RunDiagnostics();

                Reset();
            }

            CleanUp();

            return (int)Math.Round(DoMeasures() * 100);
        }

        /// <summary>
        /// Startup method. Runs before all tests.
        /// </summary>
        protected virtual void StartUp() { }

        /// <summary>
        /// Clean up method. Runs after all tests.
        /// </summary>
        protected virtual void CleanUp() { }

        private double DoMeasures()
        {
            List<double> ls = new List<double>();

            for (int i = 0; i < pA.Count; i++)
            {
                if (pA[i] > 15)
                {
                    ls.Add(1.0 * pB[i] / pA[i]);
                }
            }

            if (ls.Count == 0)
            {
                return 100;
            }

            return ls.Average();
        }

        private void _RunDiagnostics()
        {
            if (random.Next(0, 2) == 0)
            {
                sw.Restart();
                TestActionA();
                sw.Stop();
                pA.Add(sw.ElapsedTicks);

                sw.Restart();
                TestActionB();
                sw.Stop();
                pB.Add(sw.ElapsedTicks);
            }
            else
            {
                sw.Restart();
                TestActionB();
                sw.Stop();
                pB.Add(sw.ElapsedTicks);

                sw.Restart();
                TestActionA();
                sw.Stop();
                pA.Add(sw.ElapsedTicks);
            }
        }

        /// <summary>
        /// Firts method to test.
        /// </summary>
        protected abstract void TestActionA();

        /// <summary>
        /// Second method to test.
        /// </summary>
        protected abstract void TestActionB();

        /// <summary>
        /// Update method. Runs before each test. 
        /// </summary>
        protected virtual void Update() { }

        /// <summary>
        /// Reset method. Runs after each test.
        /// </summary>
        protected virtual void Reset() { }
    }
}

// -------------------------------------------

    public class MainBoard
    {
        /// <summary>
        /// Paints cells and the grid. Uses circle pattern to display cells.
        /// </summary>
        /// <param name="g">Graphics used to draw elements.</param>
        /// <param name="position">Determines where to draw.</param>
        /// <param name="displayedRows">Determines how many rows to draw.</param>
        /// <param name="displayedColumns">Determines haw many columns to draw.</param>
        private void PaintCircleBoard(Graphics g, Point position, int displayedRows, int displayedColumns)
        {
            int startPositionX = position.X;
            int startPositionY = position.Y;

            System.Drawing.Drawing2D.SmoothingMode sm = g.SmoothingMode;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            for (int i = 0; i < displayedRows; i++)
            {
                for (int j = 0; j < displayedColumns; j++)
                {
                    g.FillEllipse(colors[cellArray[i + startRow, j + startColumn]], startPositionX + j * CellSize.Width, startPositionY + i * CellSize.Height, CellSize.Width, CellSize.Height);
                }
            }

            PaintCircleGrid(g, startPositionX, startPositionY, displayedRows, displayedColumns);
            g.SmoothingMode = sm;
        }

        /// <summary>
        /// Paints grid on the board using circle pattern.
        /// </summary>
        /// <param name="g">Graphics used to draw lines.</param>
        /// <param name="startX">X  position measuren in pixels, determines top left corner of the grid.</param>
        /// <param name="startY">Y  position measuren in pixels, determines top left corner of the grid.</param>
        /// <param name="rows">Determines how many rows to draw.</param>
        /// <param name="columns">Determines how many columns to draw.</param>
        private void PaintCircleGrid(Graphics g, int startX, int startY, int rows, int columns)
        {
            if (GridSize > 0)
            {
                Pen pen = new Pen(GridColor, GridSize);

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        g.DrawEllipse(pen, startX + j * CellSize.Width, startY + i * CellSize.Height, CellSize.Width, CellSize.Height);
                    }
                }
            }
        }

        /// <summary>
        /// Paints cells and the grid. Uses quick compresion to paint cells.
        /// </summary>
        /// <param name="g">Graphics used to draw elements.</param>
        /// <param name="position">Determines where to draw.</param>
        /// <param name="displayedRows">Determines how many rows to draw.</param>
        /// <param name="displayedColumns">Determines haw many columns to draw.</param>
        private void QuickPaintBoard(Graphics g, Point position, int displayedRows, int displayedColumns)
        {
            if (displayedColumns == 0)
            {
                return;
            }

            int startPositionX = position.X;
            int startPositionY = position.Y;

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

            PaintGrid(g, startPositionX, startPositionY, displayedRows, displayedColumns);
        }

        /// <summary>
        /// Paints cells and the grid.
        /// </summary>
        /// <param name="g">Graphics used to draw elements.</param>
        /// <param name="position">Determines where to draw.</param>
        /// <param name="displayedRows">Determines how many rows to draw.</param>
        /// <param name="displayedColumns">Determines haw many columns to draw.</param>
        private void PaintBoard(Graphics g, Point position, int displayedRows, int displayedColumns)
        {
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

        /// <summary>
        /// Paints grid on the board.
        /// </summary>
        /// <param name="g">Graphics used to draw lines.</param>
        /// <param name="startX">X  position measuren in pixels, determines top left corner of the grid.</param>
        /// <param name="startY">Y  position measuren in pixels, determines top left corner of the grid.</param>
        /// <param name="rows">Determines how many rows (horizontal lines) to draw.</param>
        /// <param name="columns">Determines how many columns (vertical lines) to draw.</param>
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
    }
	
	using GolLibrary.Models;
using System.Drawing;

namespace GolLibrary.Controllers
{
    public class MainController
    {

    }
}

namespace GolLibrary.Controllers.Transforms
{
    /// <summary>
    /// Helper for cellboard operations.
    /// </summary>
    public abstract class TransformHelper
    {
        /// <summary>
        /// Gets color palette for this helper.
        /// </summary>
        public ColorPaletteModel ColorPalette { get; protected set; }

        /// <summary>
        /// Gets or sets the value that determines whether the board should be wrapped.
        /// </summary>
        public bool Wrap { get; set; }

        /// <summary>
        /// Gets or sets the value that determines whether cells should leave trail.
        /// </summary>
        public bool Trail { get; set; }

        /// <summary>
        /// Default value of color used to paint the trails.
        /// </summary>
        protected static readonly Color DefaultTrailColor = Color.DarkSeaGreen;

        /// <summary>
        /// Gets or sets the value that determines how many percent should be filled randomly.
        /// </summary>
        public int Percentage { get; set; }

        /// <summary>
        /// Based on values from currentState array performs transformation and fills nextState array.
        /// </summary>
        /// <param name="currentState">Input - Current state of the board.</param>
        /// <param name="nextState">Output - Next state of the board.</param>
        public abstract void Transform(byte[,] currentState, byte[,] nextState);

        // Number of instances.
        private static int numberOfInstances;

        /// <summary>
        /// Initialize new instance of TransformHelper with default properties.
        /// </summary>
        public TransformHelper()
        {
            ColorPalette = new ColorPaletteModel();
            ColorPalette.Id = numberOfInstances++;
        }
    }

    /// <summary>
    /// Default helper for cellboard operations.
    /// </summary>
    public class DefaultTransformHelper : TransformHelper
    {
        private static DefaultTransformHelper _instance;

        /// <summary>
        /// Gets the instance of of DefaultTransformHelper
        /// </summary>
        public static DefaultTransformHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DefaultTransformHelper();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Initialize new instance of DefaultTransformHelper with default properties.
        /// </summary>
        protected DefaultTransformHelper()
            : base()
        {
            ColorPalette.Name = "Default";
            ColorPalette.Colors = new Color[] { Color.White, Color.Black, DefaultTrailColor };
        }

        /// <summary>
        /// Based on values from currentState array performs transformation and fills nextState array.
        /// </summary>
        /// <param name="currentState">Input - Current state of the board.</param>
        /// <param name="nextState">Output - Next state of the board.</param>
        public override void Transform(byte[,] currentState, byte[,] nextState)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// Transparent helper for cellboard operations.
    /// </summary>
    public class TransparentTransformHelper : TransformHelper
    {
        private static TransparentTransformHelper _instance;

        /// <summary>
        /// Gets the instance of of TransparentTransformHelper
        /// </summary>
        public static TransparentTransformHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TransparentTransformHelper();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Initialize new instance of TransparentTransformHelper with default properties.
        /// </summary>
        protected TransparentTransformHelper()
            : base()
        {
            ColorPalette.Name = "Transparent";
            ColorPalette.Colors = new Color[] { Color.Lime, Color.Black, DefaultTrailColor };
        }

        /// <summary>
        /// Based on values from currentState array performs transformation and fills nextState array.
        /// </summary>
        /// <param name="currentState">Input - Current state of the board.</param>
        /// <param name="nextState">Output - Next state of the board.</param>
        public override void Transform(byte[,] currentState, byte[,] nextState)
        {
            throw new System.NotImplementedException();
        }
    }
}

namespace GolLibrary.Models
{
    /// <summary>
    /// Color palette representation
    /// </summary>
    public class ColorPaletteModel
    {
        /// <summary>
        /// Gets or sets unique id number.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name of the patelle.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets array of colors.
        /// </summary>
        public Color[] Colors { get; set; }

        public ColorPaletteModel()
        {

        }
    }
}




using GolLibrary.Controllers.Transforms;
using GolLibrary.Models;
using System;
using System.Drawing;

namespace System
{
    public static class GlobalConfig
    {
        // TODO add color lime to the global config as transparent color.
        public static Color TransparentColor { get; set; }
    }
}

namespace GolLibrary.Controllers
{

    // TODO: add new buttons to the boar menu and to the ribbon (coppy). 1) Change board bgColor 2) Change grid color 3) Change grid size 4) Change cell size 5) Change gen. speed 6) Change each cell type color
    // TODO: add buttons start/stop, stepByStep, Clear
    // TODO: change maximum board size to 10000 x 10000 bytes

    public class MainController
    {
        private static readonly Color trailColor = Color.DarkSeaGreen;

        private TransformHelper[] transforms;
        private ColorPaletteModel[] colors;

        public void ControllerInitialization()
        {
            transforms = new TransformHelper[]{
                new DefaultTransformHelper(),
                new QuadLifeTransformHelper(),
            };

            // setup colors
            colors = new ColorPaletteModel[]{
                new ColorPaletteModel{ 
                    Transform = transforms[0],
                    Colors = new Color[]{Color.White, Color.Black, trailColor}, 
                    Name = "Default varian A"},
                new ColorPaletteModel{ 
                    Transform = transforms[0],
                    Colors = new Color[]{Color.Wheat, Color.DarkGray, trailColor}, 
                    Name = "Default varian B"},
                new ColorPaletteModel{ 
                    Transform = transforms[0],
                    Colors = new Color[]{Color.LightSlateGray, Color.Crimson, trailColor}, 
                    Name = "Default varian C"},
                new ColorPaletteModel{ 
                    Transform = transforms[0],
                    Colors = new Color[]{GlobalConfig.TransparentColor, Color.Black, trailColor}, 
                    Name = "Default varian Transparent"},
                new ColorPaletteModel{
                    Transform = transforms[1],
                    Colors = new Color[]{Color.WhiteSmoke, Color.Red, Color.Green, Color.Blue, trailColor},
                    Name = "QuadLf"}
            };

            ColorPaletteModel selectedModel = ShowDialog(colors);
            TransformHelper selectedHelper = selectedModel.Transform;

            // get properties from the window and set selectedHelper
        }

        private ColorPaletteModel ShowDialog(ColorPaletteModel[] colors)
        {
            Random rnd = new Random();
            int index = rnd.Next(0, colors.Length);

            return colors[index];
        }
    }
}

namespace GolLibrary.Controllers.Transforms
{
    /// <summary>
    /// Helper for cellboard operations.
    /// </summary>
    public abstract class TransformHelper
    {
        /// <summary>
        /// Gets or sets the value that determines whether the board should be wrapped.
        /// </summary>
        public bool Wrap { get; set; }

        /// <summary>
        /// Gets or sets the value that determines whether cells should leave trail.
        /// </summary>
        public bool Trail { get; set; }

        /// <summary>
        /// Gets or sets the value that determines how many percent should be filled randomly.
        /// </summary>
        public int Percentage { get; set; }

        /// <summary>
        /// Based on values from currentState array performs transformation and fills nextState array.
        /// </summary>
        /// <param name="currentState">Input - Current state of the board.</param>
        /// <param name="nextState">Output - Next state of the board.</param>
        public abstract void Transform(byte[,] currentState, byte[,] nextState);
    }

    /// <summary>
    /// Default helper for cellboard operations.
    /// </summary>
    public class DefaultTransformHelper : TransformHelper
    {
        /// <summary>
        /// Based on values from currentState array performs transformation and fills nextState array.
        /// </summary>
        /// <param name="currentState">Input - Current state of the board.</param>
        /// <param name="nextState">Output - Next state of the board.</param>
        public override void Transform(byte[,] currentState, byte[,] nextState)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Transparent helper for cellboard operations.
    /// </summary>
    public class QuadLifeTransformHelper : TransformHelper
    {
        /// <summary>
        /// Based on values from currentState array performs transformation and fills nextState array.
        /// </summary>
        /// <param name="currentState">Input - Current state of the board.</param>
        /// <param name="nextState">Output - Next state of the board.</param>
        public override void Transform(byte[,] currentState, byte[,] nextState)
        {
            throw new NotImplementedException();
        }
    }
}

namespace GolLibrary.Models
{
    /// <summary>
    /// Color palette representation
    /// </summary>
    public class ColorPaletteModel
    {
        /// <summary>
        /// Gets or sets unique id number.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets related TransformHelper.
        /// </summary>
        public TransformHelper Transform { get; set; }

        /// <summary>
        /// Gets or sets name of the patelle.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets array of colors.
        /// </summary>
        public Color[] Colors { get; set; }

        public ColorPaletteModel()
        {

        }
    }
}