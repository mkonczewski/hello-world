using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace AVLWindowsFormsApplication
{
    public partial class App : Form
    {

        // current version
        private string version = "Version 00.00.03"; 

        // temporary variables for mouse movements
        private int px, py;

        // minimum & maximum size of the window
        private int minWidth = 360;
        private int minHeight = 200;

        // frames per secound
        private int renderingSpeed;

        // temporary variable for source directory : C:\Users\u18e86\Desktop\Aplikacja MAN\IBIS SOFTWARE
        private string parserSourceDir;

        // temporary variable for path to "properties.txt" file
        // eg. C:\Users\u18e86\Desktop\Aplikacja MAN
        private string parserSolutionDir;

        public App()
        {
            InitializeComponent();

            // init  components
            init();
        }

        // COMPONENT INITIALIZATION
        private void init()
        {
            versionLabel.Text = version;

            renderingSpeed = 30;

            // timer initialization
            timer.Interval = 1000 / renderingSpeed;
            timer.Tick += UpdateScreen;
            timer.Start();
        }

        // close the application
        public void closeApp()
        {
            this.Close();
        }

        private void searchArea_Click(object sender, EventArgs e)
        {
            if (searchArea.Text.Equals("Search"))
            {
                searchArea.Text = "";
                searchArea.ForeColor = SystemColors.ActiveCaptionText;
            }
        }

        // sets search textBox.Text to "Search" after leaving the window
        // only if the text is empty
        private void searchArea_Leave(object sender, EventArgs e)
        {
            if (searchArea.Text.Equals(""))
            {
                searchArea.Text = "Search";
                searchArea.ForeColor = SystemColors.AppWorkspace;
            }
        }

        private int phVal = 50;
        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            int val = hScrollBar.Value;

            phVal = val;
        }

        private int pvVal = 50;
        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            int val = vScrollBar.Value;

            pvVal = val;
        }

        // sort AppTree
        private bool sortButtonUp = true;
        private void sortButton_Click(object sender, EventArgs e)
        {
            if (sortButtonUp)
            {
                // sort thee in descending order Z-A
                sortButtonUp = false;
                sortButton.Text = "v";
                tree.sortTree(sortButtonUp);
            }
            else
            {
                // sort tree in ascending order A-Z
                sortButtonUp = true;
                sortButton.Text = "^";
                tree.sortTree(sortButtonUp);
            }
        }

        /*------------------------------ MENU LOGIC --------------------------------*/

        // this method run automatically after "Clear" button was clicked
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // clear the tree
            tree.clear();

            // clear all elements in AppBoard
            pictureBox.clear();
        }
        
        // this method run automatically after "Parse" button was clicked
        private void parseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create and display new ParserWindow
            ParserWindow window = new ParserWindow(parserSourceDir, parserSolutionDir);
            DialogResult result = window.ShowDialog();

            // if DialogResult is OK, perform Parser.parse(...) method
            if (result == DialogResult.OK)
            {
                // get  source directory
                parserSourceDir = window.getSourceDirectory();

                // get path to "properties.txt"
                parserSolutionDir = window.getSolutionDirectory();

                // create new parser object
                Parser parser = new Parser();

                // begin Parser.parse(...) method
                parser.parse(parserSourceDir, parserSolutionDir);

                // get error messages as array
                // each message contains information abut an error
                string[] errMsg = parser.getErrorMessage();

                // if length of errMsg is not zero -> error occurred
                if (errMsg.Length != 0)
                {
                    // create and show new ErrorPromptWindow
                    ErrorPromptWindow epw = new ErrorPromptWindow();

                    // fill error window with messages
                    epw.setText(errMsg);
                    epw.ShowDialog();
                }else
                {
                    // if length of errMsg is zero -> no errors
 
                    string message = "Done!";
                    string caption = "Parser";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;

                    // show succes message
                    MessageBox.Show(message, caption, buttons);
                }
            }   
        }

        // this method run automatically after "Read" button was clicked
        private void readToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create and display new ReaderWindow
            ReadWindow window = new ReadWindow(parserSolutionDir);
            DialogResult result = window.ShowDialog();

            // if DialogResult is OK, perform Parser.read(...) method
            if (result == DialogResult.OK)
            {
                //parserSolutionDir = window.getSolutionDirectory();
                string path = window.getSolutionDirectory();

                // create new parser object
                Parser parser = new Parser();

                // begin Parser.read(...) method
                // returns list of elements found in "properties.txt" file
                ElementInfo[] info = parser.read(path);

                // get error messages as array
                // each message contains information abut an error
                string[] errMsg = parser.getErrorMessage();

                // if length of errMsg is not zero -> error occurred
                if (errMsg.Length != 0)
                {
                    // create and show new ErrorPromptWindow
                    ErrorPromptWindow epw = new ErrorPromptWindow();

                    // fill error window with messages
                    epw.setText(errMsg);
                    epw.ShowDialog();
                }
                else
                {
                    parserSolutionDir = path.Substring(0, path.LastIndexOf("\\"));

                    // if length of errMsg is zero -> no errors
                    string message = "Done!";
                    string caption = "Reader";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;

                    // show succes message
                    MessageBox.Show(message, caption, buttons);

                    // fill tree with data from "properties.txt" file (ElementInfo[])
                    string fileName = path.Substring(path.LastIndexOf("\\") + 1);
                    int dotIndex = fileName.LastIndexOf(".");

                    if (dotIndex != -1)
                        fileName = fileName.Substring(0, dotIndex);
                    //tree.setRootName(fileName);
                    tree.fill(info, null);
                }
            }   
        }

        // this method works similar to readToolStripMenuItem_Click(...)
        // arguments:
        // path - path to file or directory as a string
        public void readFromDragAndDropFile(string path)
        {
            // true if path : string is a file
            if (!File.Exists(path))
            {
                return;
            }

            parserSolutionDir = path.Substring(0, path.LastIndexOf("\\"));

            // create new parser object
            Parser parser = new Parser();

            // begin Parser.read(...) method
            // returns list of elements found in "properties.txt" file
            ElementInfo[] info = parser.read(path);

            // get error messages as array
            // each message contains information abut an error
            string[] errMsg = parser.getErrorMessage();

            // if length of errMsg is not zero -> error occurred
            if (errMsg.Length != 0)
            {
                // create and show new ErrorPromptWindow
                ErrorPromptWindow epw = new ErrorPromptWindow();

                // fill error window with messages
                epw.setText(errMsg);
                epw.ShowDialog();
            }
            else
            {
                // if length of errMsg is zero -> no errors
                string message = "Done!";
                string caption = "Reader";
                MessageBoxButtons buttons = MessageBoxButtons.OK;

                // show succes message
                MessageBox.Show(message, caption, buttons);

                // fill tree with data from "properties.txt" file (ElementInfo[])
                string fileName = path.Substring(path.LastIndexOf("\\") + 1);

                int dotIndex = fileName.LastIndexOf(".");

                if(dotIndex != -1)
                    fileName = fileName.Substring(0, dotIndex);

                //tree.setRootName(fileName);
                tree.fill(info, null);
            }
           
        }

        private string busSourceDir;
        private string busSolutionDir;
        private void busToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create and display new BusWindow
            BusWindow window = new BusWindow();
            DialogResult result = window.ShowDialog();

            // if DialogResult is OK, perform Parser.parseBus(...) method
            if (result == DialogResult.OK)
            {
                // get  source directory
                busSourceDir = window.getSourceDirectory();

                // get path to "properties.txt"
                busSolutionDir = window.getSolutionDirectory();

                // create new parser object
                Parser parser = new Parser();

                // begin Parser.parseBus(...) method
                parser.parseBus(busSourceDir, busSolutionDir);

                // get error messages as array
                // each message contains information abut an error
                string[] errMsg = parser.getErrorMessage();

                // if length of errMsg is not zero -> error occurred
                if (errMsg.Length != 0)
                {
                    // create and show new ErrorPromptWindow
                    ErrorPromptWindow epw = new ErrorPromptWindow();

                    // fill error window with messages
                    epw.setText(errMsg);
                    epw.ShowDialog();
                }else
                {
                    // if length of errMsg is zero -> no errors
 
                    string message = "Done!";
                    string caption = "Bus parser";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;

                    // show succes message
                    MessageBox.Show(message, caption, buttons);
                }
            }
        }

        /*------------------------------ START OF DIAGRAM LOGIC --------------------------------*/

        // UpdateScreen is called 'renderingSpeed' times per secound
        private void UpdateScreen(object sender, EventArgs e)
        {
            // redraw pictureBox : AppBoard
            pictureBox.Invalidate();
        }

        // this method run automatically after "Search" button was clicked
        private void pictureBoxSearch_Click(object sender, EventArgs e)
        {
            // todo search button clicked
        }

        // occurs whenever user loads the form 
        private void App_Load(object sender, EventArgs e)
        {
            // add reference to main application 
            tree.setAppHandle(this);

            // add reference to main application 
            pictureBox.setAppHandle(this);       
        }

        public void addToTree(string path)
        {
            string treePath = null;

            if (File.Exists(path))
            {
                // treePath = "properties";
                treePath = getName(path);

                // create new parser object
                Parser parser = new Parser();

                // begin Parser.read(...) method
                // returns list of elements found in "properties.txt" file
                ElementInfo[] info = parser.read(path);

                // get error messages as array
                // each message contains information abut an error
                string[] errMsg = parser.getErrorMessage();

                // if length of errMsg is not zero -> error occurred
                if (errMsg.Length != 0)
                {
                    // create and show new ErrorPromptWindow
                    ErrorPromptWindow epw = new ErrorPromptWindow();

                    // fill error window with messages
                    epw.setText(errMsg);
                    epw.ShowDialog();
                }
                else
                {
                    // if length of errMsg is zero -> no errors
                    string message = "Done!";
                    string caption = "Reader";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;

                    // show succes message
                    MessageBox.Show(message, caption, buttons);

                    tree.fill(info, treePath);
                }

                
            }else if (Directory.Exists(path))
            {
                string[] filesInMainDir = Directory.GetFiles(path);

                // create new parser object
                Parser parser = new Parser();

                foreach (string file in filesInMainDir)
                {

                    // begin Parser.read(...) method
                    // returns list of elements found in "properties.txt" file
                    ElementInfo[] info = parser.read(file);

                    // get error messages as array
                    // each message contains information abut an error
                    string[] errMsg = parser.getErrorMessage();

                    // if length of errMsg is not zero -> error occurred
                    if (errMsg.Length != 0)
                    {
                        // create and show new ErrorPromptWindow
                        ErrorPromptWindow epw = new ErrorPromptWindow();

                        // fill error window with messages
                        epw.setText(errMsg);
                        epw.ShowDialog();
                    }else
                    {
                        treePath = path.Substring(path.LastIndexOf("\\") + 1) + "\\" + getName(file);

                        tree.fill(info, treePath);
                    }
                }
            }
        }

        // fot string "C:\...\file.txt" return "file"
        private string getName(string path)
        {
            int indexA = path.LastIndexOf("\\");
            int indexB = path.LastIndexOf(".");

            return path.Substring(indexA + 1, indexB - indexA - 1);
        }
    }
}