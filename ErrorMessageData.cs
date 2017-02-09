using System;

namespace DataLibrary
{
    public class ErrorMessageData
    {
        /*----------------------------- FIELDS -----------------------------------*/

        /// <summary>
        /// Error description
        /// </summary>
        private string _Message;

        /// <summary>
        /// Get error description.
        /// </summary>
        public string Message
        {
            get 
            { 
                return _Message; 
            }
            private set 
            {
                if (IsMessageValid(value))
                {
                    _Message = value; 
                }
                else
                {
                    throw new ArgumentException("Invalud message value");
                }
            }
        }


        /// <summary>
        /// Get error category.
        /// </summary>
        public string Title { get; private set; }

        /*-------------------------- END OF FIELDS -------------------------------*/

        /*--------------------------- CONSTRUCTOR --------------------------------*/

        /// <summary>
        /// Initialize a new instance of DataLibrary.ErrorMessageData with given messaget and title.
        /// </summary>
        /// <param name="message">Error description.</param>
        /// <param name="title">Error category.</param>
        public ErrorMessageData(string message, string title)
        {
            this.Message = message;
            this.Title = title;
        }

        /// <summary>
        /// Initialize a new instance of DataLibrary.ErrorMessageData with given message and null title.
        /// </summary>
        /// <param name="message">Error description.</param>
        public ErrorMessageData(string message)
            : this(message, null)
        {
            
        }

        /*------------------------ END OF CONSTRUCTOR ----------------------------*/

        /*----------------------------- METHODS ----------------------------------*/

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return String.Format("ErrorMessageData[Title = {0}, Message = {1}]", Title, Message);
        }

        /// <summary>
        /// Indicate whether the string is a valid message.
        /// </summary>
        /// <param name="message">The message to evaluate.</param>
        /// <returns></returns>
        private static bool IsMessageValid(string message)
        {
            if (message == null)
            {
                return false;
            }

            return true;
        }
    }
}
