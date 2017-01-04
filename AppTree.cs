using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System;
using System.IO;

namespace AVLWindowsFormsApplication
{
    public class AppTree : TreeView
    {
        // reference to main application 
        private App appHandle;

        // temporary variable for extendable data
        private ElementInfo nodeData;

        public AppTree()
        {
            // set AllowDrop to true in order to accept dropped data
            AllowDrop = true;
        }

        // set reference to main application 
        public void setAppHandle(App app)
        {
            appHandle = app;
        }

        // bool type = true, A-Z (ascending order), false Z-A (descending order)
        public void sortTree(bool type)
        {
            //Sort();
        }

        // sets ElementInfo stored in tree
        // path = "A40\1147";
        public void fill(ElementInfo[] info, string path)
        {
            BeginUpdate();

            string[] sub = path.Split('\\');
            string name = sub[sub.Length - 1];

            AppNode n = new AppNode();
            n.fill(info);
            n.Text = name;

            TreeNodeCollection temp = Nodes;

            for (int i = 0; i < sub.Length - 1; i++)
            {
                TreeNode node = new TreeNode(sub[i]);

                if(!nodeContains(temp, node))
                    temp.Add(node);

                int index = nodeIndexOf(temp, node);
                temp = temp[index].Nodes;
            }

            if (!nodeContains(temp, n))
                temp.Add(n);

            EndUpdate();
        }

        private int nodeIndexOf(TreeNodeCollection t, TreeNode n)
        {
            int index = 0;
            foreach (TreeNode x in t)
            {
                if (x.Text.Equals(n.Text))
                    return index;

                index++;
            }

            return -1;
        }

        private bool nodeContains(TreeNodeCollection t, TreeNode n)
        {
            foreach (TreeNode x in t)
                if (x.Text.Equals(n.Text))
                    return true;

            return false;
        }

        // clear all data stored in three
        public void clear()
        {
            Nodes.Clear();
        }

        // This method runs when mouse leaves the area of this component
        protected override void OnMouseLeave(EventArgs e)
        {
            // if variable for extendable data is empty then do nothing
            // otherwise send this data
            if (nodeData != null)
            {
                // send stored data as string and clear it
                DoDragDrop(nodeData.ToString(), DragDropEffects.Move);
                nodeData = null;
            }

        }

        // clear nodeData : ElementInfo on mouse up
        protected override void OnMouseUp(MouseEventArgs e)
        {
            nodeData = null;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            // do nothing when the tree is empty
            if (Nodes.Count == 0)
                return;

            // store parent TreeNode if it exists 
            TreeNode parent = Nodes[0];

            // for each node in the list of parent nodes 
            int index = 0;
            foreach (TreeNode node in parent.Nodes)
            {
                // get bounds of current node
                Rectangle rc = node.Bounds;

                // stop the loop then current node is below e.Y
                if (rc.Y > e.Y)
                {
                    return;
                }

                // Contains(x, y) returns true when Point(x, y) is inside bounds of this rectandle
                if (rc.Contains(e.X, e.Y))
                {
                    // get current node and assign it to nodeData:ElementInfo
                    nodeData = info[index];
                    return;
                }

                index++;
            }

        }

        // DragDropEffects.Move - It represents how moving cursor will look
        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
            {
                drgevent.Effect = DragDropEffects.Copy;
            }
        }

        // method executed when something is dropped on the area of this component
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            // files array contains names dropped files / directories
            string[] files = (string[])drgevent.Data.GetData(DataFormats.FileDrop);

            if (files != null)
            {
                foreach (string s in files)
                {
                    appHandle.addToTree(s);
                }
            }
        }

        private class AppNode : TreeNode
        {
            // list of all elements in this node
            public ElementInfo[] info;

            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;

                AppNode n = (AppNode) obj;

                if (n == null)
                    return false;

                return Text.Equals(n.Text);
            }

            public override int GetHashCode()
            {
                return Text.GetHashCode();
            }

            public void fill(ElementInfo[] info)
            {
                if (info == null)
                    return;

                this.info = info;

                int index = 0;

                foreach (ElementInfo ei in info)
                {
                    Nodes.Add(ei.Name);
                    TreeNode childNode = Nodes[index];

                    childNode.Nodes.Add("Sachnummer : " + ei.Sachnummer);
                    childNode.Nodes.Add("Revision : " + ei.Version);

                    int addToIndex = 1;
                    if (ei.Input.Length != 0)
                    {
                        childNode.Nodes.Add("Input : ");
                        addToIndex++;
                    }

                    foreach (string s in ei.Input)
                    {
                        childNode.Nodes[addToIndex].Nodes.Add(s);
                    }

                    if (ei.Output.Length != 0)
                    {
                        childNode.Nodes.Add("Output : ");
                        addToIndex++;
                    }

                    foreach (string s in ei.Output)
                    {
                        childNode.Nodes[addToIndex].Nodes.Add(s);
                    }

                    if (ei.Params.Length != 0)
                    {
                        childNode.Nodes.Add("Parameters : ");
                        addToIndex++;
                    }

                    foreach (string s in ei.Params)
                    {
                        childNode.Nodes[addToIndex].Nodes.Add(s);
                    }

                    index++;
                }
            }
        }
    }
}
