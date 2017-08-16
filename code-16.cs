	public class Diagnostics
	{	
		public void FastestClear()
		{
			byte[,] arr = new byte[10, 10];
			
			Array.Clear(arr, 0, 0);
		}
	
		public void FastestIterationOverArray()
		{
			int temp2, i, j;
			int r = arr.GetLength(0);
			int c = arr.GetLength(1);

			for (i = 0; i < r; i++)
			{
				for (j = 0; j < c; j++)
				{
					temp2 = arr[i, j];
				}
			}
		}
		// many, many problems.
		public void Parallel()
		{
			Parallel.For(0, threads, (index) =>
            {
                int i, j;
                int add = index * c;

                for (i = 0; i < r; i++)
                {
                    for (j = 0; j < c; j++)
                    {
                        arr[i, j + add]++;
                    }
                }
            });
		}
	}
	
	public class Program
	{
		public const int MaxByteSize = 33275; // ?
		
		public Program()
        {
			// get instance of DefaultTransformHelper
            TransformHelper dtrn = DefaultTransformHelper.Instance;
            dtrn.Enabled = true;
            dtrn.Percentage = 12;
            Model m = dtrn.Model;
            byte[,] cs = { { 0, 1 }, { 1, 0 } };
            byte[,] ns = new byte[2, 2];

            dtrn.Transform(cs, ns);

			// get instance of QuadPaletteTransformHelper
            TransformHelper qtrn = QuadPaletteTransformHelper.Instance;
            qtrn.Enabled = true;
            qtrn.Percentage = 17;
            Model m2 = qtrn.Model;

            qtrn.Transform(cs, ns);

			// get instance (the same instance) of QuadPaletteTransformHelper
            TransformHelper qtrn2 = QuadPaletteTransformHelper.Instance;
            qtrn2.Enabled = false;
            qtrn2.Percentage = 15;
            Model m3 = qtrn2.Model;

            qtrn2.Transform(cs, ns);
        }
    }

    public abstract class TransformHelper
    {
        public Model Model { get; protected set; }

        public bool Enabled { get; set; }

        public int Percentage { get; set; }

        public abstract void Transform(byte[,] currentState, byte[,] nextState);

        private static int numberOfInstances;

        public TransformHelper()
        {
            Model = new Model();
            Model.Id = numberOfInstances++;
        }
    }

    public class DefaultTransformHelper : TransformHelper
    {
        protected DefaultTransformHelper()
            : base()
        {
            Model.Name = "default";
        }

        private static DefaultTransformHelper _instance;
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

        public override void Transform(byte[,] currentState, byte[,] nextState)
        {
            for (int i = 0; i < currentState.GetLength(0); i++)
            {
                for (int j = 0; j < currentState.GetLength(1); j++)
                {
                    if (currentState[i, j] == 0)
                    {
                        nextState[i, j] = 1;
                    }
                    else
                    {
                        nextState[i, j] = 0;
                    }
                }
            }
        }
    }

    public class QuadPaletteTransformHelper : TransformHelper
    {
        protected QuadPaletteTransformHelper()
            : base()
        {
            Model.Name = "quadPalette";
        }

        private static QuadPaletteTransformHelper _instance;
        public static QuadPaletteTransformHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new QuadPaletteTransformHelper();
                }
                return _instance;
            }
        }

        public override void Transform(byte[,] currentState, byte[,] nextState)
        {
            for (int i = 0; i < currentState.GetLength(0); i++)
            {
                for (int j = 0; j < currentState.GetLength(1); j++)
                {
                    nextState[i, j] = (byte)(currentState[i, j] + 1);
                }
            }
        }
    }