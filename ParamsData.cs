using System;

namespace DataLibrary
{
    public class ParamsData
    {
        /*----------------------------- FIELDS -----------------------------------*/


        /// <summary>
        /// Name of this parameter e.g. "G_02_PARA_BusTyp_AdBlue".
        /// </summary>
        private string _Name;

        /// <summary>
        /// Get name of this parameter e.g. "G_02_PARA_BusTyp_AdBlue".
        /// </summary>
        public string Name
        {
            get 
            { 
                return _Name; 
            }
            private set 
            {
                if (IsValidVariable(value))
                {
                    _Name = value; 
                }
                else
                {
                    throw new ArgumentException("Invalud name value");
                }
            }
        }

        /// <summary>
        /// Type of this parameter e.g. "BOOL".
        /// </summary>
        private string _Type;

        /// <summary>
        /// Get type of this parameter e.g. "BOOL".
        /// </summary>
        public string Type
        {
            get 
            { 
                return _Type; 
            }
            private set 
            {
                if (IsValidVariable(value))
                {
                    _Type = value;
                }
                else
                {
                    throw new ArgumentException("Invalud type value");
                }
            }
        }

        /// <summary>
        /// Value (as string) of this parameter e.g. "1".
        /// </summary>
        private string _Value;

        /// <summary>
        /// Get value (as string) of this parameter e.g. "1".
        /// </summary>
        public string Value
        {
            get
            {
                return _Value;
            }
            private set
            {
                if (IsValidVariable(value))
                {
                    _Value = value;
                }
                else
                {
                    throw new ArgumentException("Invalud value");
                }
            }
        }


        /// <summary>
        /// Separate values in line.
        /// </summary>
        private static char Delimiter = ';';

        /*-------------------------- END OF FIELDS -------------------------------*/

        /*--------------------------- CONSTRUCTOR --------------------------------*/

        /// <summary>
        /// Initialize a new instance of DataLibrary.ParamsData with given values of name, type and value.
        /// </summary>
        /// <param name="name">Name of this parameter.</param>
        /// <param name="type">Type of this parameter.</param>
        /// <param name="value">Value as string of this parameter.</param>
        public ParamsData(string name, string type, string value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        /// <summary>
        /// Initialize a new instance of DataLibrary.ParamsData with values from given line.
        /// </summary>
        /// <param name="line">Contain values in form of single line. Line in format: this.Name;this.Value;this.Type.</param>
        /// <exception cref="System.NullReferenceException">Thrown when line is empty.</exception>
        /// <exception cref="System.ArgumentException">Thrown when line format is incorrect.</exception>
        public ParamsData(string line)
        {
            GetPropertiesFromString(line);
        }

        /*------------------------ END OF CONSTRUCTOR ----------------------------*/

        /*----------------------------- METHODS ----------------------------------*/

        /// <summary>
        /// Set values of Name, Value and Type based on given line.
        /// </summary>
        /// <param name="line">Contain values in form of single line. Line in format: this.Name;this.Value;this.Type.</param>
        /// <exception cref="System.NullReferenceException">Thrown when line is empty.</exception>
        /// <exception cref="System.ArgumentException">Thrown when line format is incorrect.</exception>
        private void GetPropertiesFromString(string line)
        {
            string[] parts = line.Split(Delimiter);

            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid line format: this.Name;this.Value;this.Type");
            }

            Name = parts[0];
            Value = parts[1];
            Type = parts[2];
        }

        /// <summary>
        /// Indicate whether the string has a valid value.
        /// </summary>
        /// <param name="variable">The variable to evaluate.</param>
        /// <returns></returns>
        private static bool IsValidVariable(string variable)
        {
            if (String.IsNullOrWhiteSpace(variable))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// Format: this.Name;this.Value;this.Type.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0}{1}{2}{1}{3}", Name, Delimiter, Value, Type);
        }
    }
}
