using DataLibrary;
using System;
using System.Collections.Generic;
using System.IO;

namespace IOLibrary
{
    public class ConfigFileReader
    {
        /*----------------------------- FIELDS -----------------------------------*/
        private static LinkedList<string> listOfSachnummers;

        /*-------------------------- END OF FIELDS -------------------------------*/

        /*--------------------------- CONSTRUCTOR --------------------------------*/

        /*------------------------ END OF CONSTRUCTOR ----------------------------*/

        /*----------------------------- METHODS ----------------------------------*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static LinkedList<string> ReadSNConfigurationFile(ElementData info)
        {
            listOfSachnummers = new LinkedList<string>();

            string fullPathToSNConfigurationDirectory = GetFullPathToSNConfigurationDirectory();

            if (Directory.Exists(fullPathToSNConfigurationDirectory))
            {
                using (FileStream fs = File.Open("", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (BufferedStream bs = new BufferedStream(fs))
                using (StreamReader sr = new StreamReader(bs))
                {
                    // current line
                    string s;

                    bool startReading = false;

                    // read line by line all documnt
                    while ((s = sr.ReadLine()) != null)
                    {
 
                    }
                }
            }

            return listOfSachnummers;
        }

        /// <summary>
        /// Returns full path to directory with sachnummer (SN) configuration files.
        /// </summary>
        /// <returns></returns>
        public static string GetFullPathToSNConfigurationDirectory()
        {
            string fullPathToCurrentDirectory = Directory.GetCurrentDirectory();

            string fullPathToSNConfigurationDirectory = Path.Combine(fullPathToCurrentDirectory, "config", "SN", "robo");

            return fullPathToSNConfigurationDirectory;
        }

        /// <summary>
        /// Returns full path to directory with verkausgroup (VKG) configuration files.
        /// </summary>
        /// <returns></returns>
        public static string GetFullPathToVKGConfigurationDirectory()
        {
            string fullPathToCurrentDirectory = Directory.GetCurrentDirectory();

            string fullPathToVKGConfigurationDirectory = Path.Combine(fullPathToCurrentDirectory, "config", "VKG", "robo");

            return fullPathToVKGConfigurationDirectory;
        }
    }
}
