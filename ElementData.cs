using System;
using System.Text;

namespace DataLibrary
{
    public class ElementData : IComparable
    {
        /*----------------------------- FIELDS -----------------------------------*/

        /// <summary>
        /// Get full name of this component. FullName consist of name, version and file path.
        /// </summary>
        private string FullName
        {
            get
            {
                return String.Format("{0}-{1}-{2}", Name, Version, Path);
            }
        }

        /// <summary>
        /// Get or set full path on the computer to the file with this data.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Get or set name of this component e.g. "I_337_IBIS_AussenLautspr".
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set sachnummmer e.g "11.12345-1234".
        /// </summary>
        public string Sachnummer { get; set; }

        /// <summary>
        /// Get or set version as integer e.g 6.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Get or set array of names of all input variables/signals.
        /// </summary>
        public string[] Inputs { get; set; }

        /// <summary>
        /// Get or set array of names of all output variables/signals.
        /// </summary>
        public string[] Outputs { get; set; }

        /// <summary>
        /// Get or set array of names of all parameters.
        /// </summary>
        public string[] Params { get; set; }

        /*-------------------------- END OF FIELDS -------------------------------*/

        /*--------------------------- CONSTRUCTOR --------------------------------*/

        /// <summary>
        /// Initialize a new instance of DataLibrary.ElementData with default values of properties.
        /// </summary>
        public ElementData()
            : this(null, null, null, 0, null, null, null)
        {

        }

        /// <summary>
        /// Initialize a new instance of DataLibrary.ElementData with given name, sachnummer and version and other properties are set to deafult.
        /// </summary>
        /// <param name="name">Name of this component e.g. "I_337_IBIS_AussenLautspr".</param>
        /// <param name="sachnummer">Sachnummmer e.g "11.12345-1234".</param>
        /// <param name="version">Version as string e.g "6".</param>
        public ElementData(string name, string sachnummer, int version)
            : this(null, name, sachnummer, version, null, null, null)
        {
            
        }

        /// <summary>
        /// Initialize a new instance of DataLibrary.ElementData with given name, sachnummer, version and input/output signals and other properties are set to deafult.
        /// </summary>
        /// <param name="name">Name of this component e.g. "I_337_IBIS_AussenLautspr".</param>
        /// <param name="sachnummer">Sachnummmer e.g "11.12345-1234".</param>
        /// <param name="version">Version as string e.g "6".</param>
        /// <param name="input">Array of names of all input variables/signals.</param>
        /// <param name="output">Array of names of all output variables/signals.</param>
        public ElementData(string name, string sachnummer, int version, string[] inputs, string[] outputs)
            : this(null, name, sachnummer, version, inputs, outputs, null)
        {
            
        }

        /// <summary>
        /// Initialize a new instance of DataLibrary.ElementData with given name, sachnummer, version and input/output/params signals and other properties are set to deafult.
        /// </summary>
        /// <param name="name">Name of this component e.g. "I_337_IBIS_AussenLautspr".</param>
        /// <param name="sachnummer">Sachnummmer e.g "11.12345-1234".</param>
        /// <param name="version">Version as string e.g "6".</param>
        /// <param name="input">Array of names of all input variables/signals.</param>
        /// <param name="output">Array of names of all output variables/signals.</param>
        /// <param name="parameters">Array of names of all parameters.</param>
        public ElementData(string name, string sachnummer, int version, string[] inputs, string[] outputs, string[] parameters)
            : this(null, name, sachnummer, version, inputs, outputs, parameters)
        {
            
        }

        /// <summary>
        /// Initialize a new instance of DataLibrary.ElementData with given file path, name, sachnummer, version and input/output/params.
        /// </summary>
        /// <param name="name">Name of this component e.g. "I_337_IBIS_AussenLautspr".</param>
        /// <param name="sachnummer">Sachnummmer e.g "11.12345-1234".</param>
        /// <param name="version">Version as string e.g "6".</param>
        /// <param name="input">Array of names of all input variables/signals.</param>
        /// <param name="output">Array of names of all output variables/signals.</param>
        /// <param name="parameters">Array of names of all parameters.</param>
        public ElementData(string filePath, string name, string sachnummer, int version, string[] inputs, string[] outputs, string[] parameters)
        {
            Path = filePath;
            Params = parameters;
            Inputs = inputs;
            Outputs = outputs;
            Name = name;
            Sachnummer = sachnummer;
            Version = version;
        }

        /*------------------------ END OF CONSTRUCTOR ----------------------------*/

        /*----------------------------- METHODS ----------------------------------*/

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            int defaultStringBuilderCapasity = 128;
            StringBuilder sb = new StringBuilder(defaultStringBuilderCapasity);

            sb.Append("Name:");
            sb.Append("\n");
            sb.Append(Name);
            sb.Append("\n");
            sb.Append("Sachnummer:");
            sb.Append("\n");
            sb.Append(Sachnummer);
            sb.Append("\n");
            sb.Append("Version:");
            sb.Append("\n");
            sb.Append(Version.ToString());
            sb.Append("\n");

            if (Inputs != null && Inputs.Length > 0)
            {
                sb.Append("Input:");
                sb.Append("\n");

                foreach (string s in Inputs)
                {
                    sb.Append(s);
                    sb.Append("\n");
                }
            }

            if (Outputs != null && Outputs.Length > 0)
            {
                sb.Append("Output:");
                sb.Append("\n");

                foreach (string s in Outputs)
                {
                    sb.Append(s);
                    sb.Append("\n");
                }
            }

            if (Params != null && Params.Length > 0)
            {
                sb.Append("Params:");
                sb.Append("\n");

                foreach (string s in Params)
                {
                    sb.Append(s);
                    sb.Append("\n");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Determines whether two DataLibrary.ElementData objects have the same value.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            ElementData data = obj as ElementData;

            if (data == null)
            {
                return false;
            }

            return this.FullName.Equals(data.FullName);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown when obj is not the same type as this instance.</exception>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("obj is not the same type as this instance.");
            }

            ElementData data = obj as ElementData;

            if (data == null)
            {
                throw new ArgumentException("obj is not the same type as this instance.");
            }

            return this.FullName.CompareTo(data.FullName);
        }

        /// <summary>
        /// Return the hash code for this component.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.FullName.GetHashCode();
        }
    }
}
