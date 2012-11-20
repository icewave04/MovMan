using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace movieData
{
    //Search movie form
    public partial class searchMovie : UserControl
    {
        private readonly int resultWidthPercent = 35;
        private readonly int resultHeightPercent = 70;
        private readonly int spaceBetweenReulstsAndOutputPercent = 15;
        private readonly int searchBarWidthPercent = 65;
        private readonly int searchButtingWidthPercent = 5;
        private readonly int checkBoxWidthPercent = 5;
        private readonly int buffer = 20;
        private dataManager DataManager;
        info exactMovie;
        private List<info> resultList;
        private List<int> resultKey;
        List<TreeNode> treeList;
        TreeNode exactMatch;
        private string resultString;
        private string searchParam;
        private bool performSearch = false;

        private string searchttString = String.Format("{0}\n{1}\n{2}\n{3}\n{4}", "Actor Only Search returns values that best match the search from the Actor fields.",
                "Specific Actor Search forces the search to meet at least 50% of the parameters before returning a value.",
                "Detailed search performs the search on each word, instead of just the entire phrase.",
                "Advanced Search searches all fields, this search is some what slower but also searches Description and Year of all Movie Data (As opposed to just searching Titles).",
                "Detailed and Advanced can be combined, Actor/Actor Specific override.");

        public MouseEventHandler viewItem = null;

        
        public searchMovie(dataManager dm)
        {
            InitializeComponent();
            resultString = String.Empty;
            this.DataManager = dm;
            resultList = new List<info>();
            resultKey = new List<int>();
            
            


        }




        public void resize(int x, int y) //Resize method used when the main window has changed in size, considering chaning z/100 to z*0.01 (requires conversion between int's and doubles)
        {
            this.Height = y;
            this.Width = x;
            searchT.Size = new Size((x / 100) * searchBarWidthPercent, searchT.Size.Height);
            searchButton.Size = new Size((x / 100) * searchButtingWidthPercent, searchButton.Size.Height);
            resultTree.Size = new Size(((x / 100) * resultWidthPercent), ((y / 100) * resultHeightPercent));
            resultInfoT.Size = new Size(((x / 100) * resultWidthPercent), ((y / 100) * resultHeightPercent));
            searchT.Location = new Point(buffer, y - (searchT.Size.Height * 2) - buffer);
            resultTree.Location = new Point(searchT.Location.X, y-(y-searchT.Location.Y)-resultTree.Size.Height-buffer);
            resultInfoT.Location = new Point(resultTree.Location.X+resultTree.Size.Width+buffer, resultTree.Location.Y);
            searchButton.Location = new Point((searchT.Location.X + searchT.Size.Width + ((x / 100) * 1)), searchT.Location.Y);
            searchCB.Location = new Point((searchButton.Location.X+searchButton.Size.Width)+buffer, searchT.Location.Y - searchCB.Size.Height+searchT.Size.Height);
            
            
        }
        
        //Actor Search, searches only for actors.
        public void actorSearch(string searchParam)
        {
            searchT.Text = searchParam;
            searchCB.SetItemChecked(0, true);
            searchCB.SetItemChecked(1, false);
            searchCB.SetItemChecked(2, false);
            searchCB.SetItemChecked(3, false);
            treeList = new List<TreeNode>();
            resultList.Clear();
            resultKey.Clear();
            resultTree.Nodes.Clear();
            string extraParams = "Returing only results matching from Actor's Field";
            DateTime startActor = DateTime.Now;
            DataManager.performActorOnlySearch(searchParam, searchCB.CheckedIndices.Contains(1), ref resultList);
            DateTime endActor = DateTime.Now;
            if ((endActor - startActor).TotalMilliseconds > 1000)
            {
                resultString = String.Format("{0}\r\n{1}{2}{3}", resultString, "Search Completed: ", decimal.Round((decimal)(endActor - startActor).TotalSeconds, 2, MidpointRounding.AwayFromZero), " seconds");
            }
            else
            {
                resultString = String.Format("{0}\r\n{1}0.{2}{3}", resultString, "Search Completed: ", decimal.Round((decimal)(endActor - startActor).TotalMilliseconds, 0, MidpointRounding.AwayFromZero), " seconds");
            }
            if (resultList.Count > 0)
            {
                TreeNode[] anotherArray;
                int i = 0;
                int k = 0;
                anotherArray = new TreeNode[resultList.Count];

                for (; i < resultList.Count; i++)
                {
                    anotherArray[k] = new TreeNode(resultList[i].getTitle());
                    anotherArray[k].Tag = resultList[i];
                    k++;
                    resultKey.Add(i);
                }

                TreeNode node = new TreeNode("Partial Match", anotherArray);
                node.Expand();
                
                resultTree.Nodes.Add(node);
            }
            resultString = String.Format("{0}{1}{2}\r\n{3}\r\n{4}\r\n\r\n", ">>> ", searchParam, " <<<", extraParams, resultString);
            resultInfoT.SelectionStart = 0;
            resultInfoT.SelectionLength = 0;
            resultInfoT.SelectedText = resultString;
            resultString = String.Empty;
        }

        //The button that makes it all happen
        public void searchButton_Click(object sender, EventArgs e)
        {
            performSearch = true; //This is to prevent the delay between clicks and release cause multiple searches causing System Failure
        }

        private void enterPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)13)
            {
                doSearch(null, null); //This is unchecked and exploitable, examine this at a latter date
                e.Handled = true;
            }
        }

        private void selectItem(object sender, MouseEventArgs e)
        {
            // Get the node that was clicked
            TreeNode selectedNode = resultTree.HitTest(e.Location).Node;

            if (selectedNode != null && selectedNode.Tag != null && selectedNode.Tag.GetType() == typeof(info))
            {
                // Do something with the selected node here...
                viewItem(selectedNode.Tag, e);
            }
        }



        private void doSearch(object sender, MouseEventArgs e)
        {
            searchParam = searchT.Text.Trim();
            treeList = new List<TreeNode>();
            resultList.Clear();
            resultKey.Clear();
            resultTree.Nodes.Clear();
            bool exact = false;
            string extraParams = "No additional parameters";
            if (searchParam != "")
            {
                DateTime start = DateTime.Now;
                if (searchCB.CheckedItems.Count > 0 && searchCB.CheckedIndices.Contains(0)) //Actor only search ignores all other search parameters
                {

                    extraParams = "Returing only results matching from Actor's Field";
                    DateTime startActor = DateTime.Now;
                    DataManager.performActorOnlySearch(searchParam, searchCB.CheckedIndices.Contains(1), ref resultList); //Determinds if each word is searched, or if exact matches are
                    DateTime endActor = DateTime.Now;
                    if ((endActor - startActor).TotalMilliseconds > 1000)
                    {
                        resultString = String.Format("{0}\r\n{1}{2}{3}", resultString, "Search Completed: ", decimal.Round((decimal)(endActor - startActor).TotalSeconds, 2, MidpointRounding.AwayFromZero), " seconds");
                    }
                    else
                    {
                        resultString = String.Format("{0}\r\n{1}0.{2}{3}", resultString, "Search Completed: ", decimal.Round((decimal)(endActor - startActor).TotalMilliseconds, 0, MidpointRounding.AwayFromZero), " seconds");
                    }
                    if (resultList.Count == 0)
                    {
                        searchCB.SetItemChecked(0, false);
                        searchCB.SetItemChecked(1, false);
                        searchCB.SetItemChecked(2, false);
                        searchCB.SetItemChecked(3, false);
                        searchButton_Click(sender, e);
                        extraParams = "No results found, performing default search";
                        resultString = String.Format("{0}{1}{2}\r\n{3}\r\n{4}\r\n", ">>> ", searchParam, " <<<", extraParams, resultString);
                        resultInfoT.SelectionStart = 0;
                        resultInfoT.SelectionLength = 0;
                        resultInfoT.SelectedText = resultString;
                        return;
                    }
                }
                else
                {
                    DateTime startSearch = DateTime.Now;
                    exactMovie = DataManager.performSearch(searchParam);
                    DateTime endSearch = DateTime.Now;
                    if ((endSearch - startSearch).TotalMilliseconds > 1000)
                    {
                        resultString = String.Format("{0}\r\n{1}{2}{3}", resultString, "Search Completed: ", decimal.Round((decimal)(endSearch - startSearch).TotalSeconds, 2, MidpointRounding.AwayFromZero), " seconds");
                    }
                    else
                    {
                        resultString = String.Format("{0}\r\n{1}0.{2}{3}", resultString, "Search Completed: ", decimal.Round((decimal)(endSearch - startSearch).TotalMilliseconds, 0, MidpointRounding.AwayFromZero), " seconds");
                    }

                    DateTime startRegex = DateTime.Now;
                    if (exactMovie != null)
                    {
                        exactMatch = new TreeNode(exactMovie.getTitle());
                        TreeNode[] array = new TreeNode[] { exactMatch };
                        TreeNode treeNode = new TreeNode("Exact Match", array);
                        treeNode.Expand();
                        resultTree.Nodes.Add(treeNode);
                        exact = true;
                        exactMatch.Tag = exactMovie;
                    }

                    DataManager.performRegexSearch(searchParam, ref resultList);
                    DateTime endRegex = DateTime.Now;
                    if ((endRegex - startRegex).TotalMilliseconds > 1000)
                    {
                        resultString = String.Format("{0}\r\n{1}{2}{3}", resultString, "Regex Search Completed: ", decimal.Round((decimal)(endRegex - startRegex).TotalSeconds, 2, MidpointRounding.AwayFromZero), " seconds");
                    }
                    else
                    {
                        resultString = String.Format("{0}\r\n{1}0.{2}{3}", resultString, "Regex Search Completed: ", decimal.Round((decimal)(endRegex - startRegex).TotalMilliseconds, 0, MidpointRounding.AwayFromZero), " seconds");
                    }



                    if (searchCB.CheckedItems.Count > 0 && searchCB.CheckedIndices.Contains(2) && !searchCB.CheckedIndices.Contains(3)) //Detailed but not advanced
                    {
                        DateTime startDetailed = DateTime.Now;
                        DataManager.performDetailedSearch(searchParam, ref resultList);
                        DateTime endDetailed = DateTime.Now;
                        if ((endDetailed - startDetailed).TotalMilliseconds > 1000)
                        {
                            resultString = String.Format("{0}\r\n{1}{2}{3}", resultString, "Detailed Search Completed: ", decimal.Round((decimal)(endDetailed - startDetailed).TotalSeconds, 2, MidpointRounding.AwayFromZero), " seconds");
                        }
                        else
                        {
                            resultString = String.Format("{0}\r\n{1}0.{2}{3}", resultString, "Detailed Search Completed: ", decimal.Round((decimal)(endDetailed - startDetailed).TotalMilliseconds, 0, MidpointRounding.AwayFromZero), " seconds");
                        }
                        extraParams = "Detailed but not Advanced";
                    }
                    else if (searchCB.CheckedItems.Count > 0 && searchCB.CheckedIndices.Contains(3) && !searchCB.CheckedIndices.Contains(2)) //Advanced but not detailed
                    {
                        DateTime startAdvanced = DateTime.Now;
                        DataManager.performAdvancedSearch(searchParam, ref resultList);
                        DateTime endAdvanced = DateTime.Now;
                        if ((endAdvanced - startAdvanced).TotalMilliseconds > 1000)
                        {
                            resultString = String.Format("{0}\r\n{1}{2}{3}", resultString, "Advanced Search Completed: ", decimal.Round((decimal)(endAdvanced - startAdvanced).TotalSeconds, 2, MidpointRounding.AwayFromZero), " seconds");
                        }
                        else
                        {
                            resultString = String.Format("{0}\r\n{1}0.{2}{3}", resultString, "Advanced Search Completed: ", decimal.Round((decimal)(endAdvanced - startAdvanced).TotalMilliseconds, 0, MidpointRounding.AwayFromZero), " seconds");
                        }
                        extraParams = "Advanced but not detailed";
                    }
                    else if (searchCB.CheckedItems.Count > 0 && searchCB.CheckedIndices.Contains(2) && searchCB.CheckedIndices.Contains(3)) //Detailed and Advanced
                    {
                        DateTime startAdvancedDetailed = DateTime.Now;
                        DataManager.performDetailedAdvancedSearch(searchParam, ref resultList);
                        DateTime endAdvancedDetailed = DateTime.Now;
                        if ((endAdvancedDetailed - startAdvancedDetailed).TotalMilliseconds > 1000)
                        {
                            resultString = String.Format("{0}\r\n{1}{2}{3}", resultString, "Advanced Detailed Search Completed: ", decimal.Round((decimal)(endAdvancedDetailed - startAdvancedDetailed).TotalSeconds, 2, MidpointRounding.AwayFromZero), " seconds");
                        }
                        else
                        {
                            resultString = String.Format("{0}\r\n{1}0.{2}{3}", resultString, "Advanced Detailed Search Completed: ", decimal.Round((decimal)(endAdvancedDetailed - startAdvancedDetailed).TotalMilliseconds, 0, MidpointRounding.AwayFromZero), " seconds");
                        }
                        extraParams = "Detailed and Advanced";
                    }
                }
                DataManager.quickSort(ref resultList);
                DataManager.cullSorted(ref resultList);
                DateTime buildList = DateTime.Now;
                resultList.Remove(exactMovie);
                if (resultList.Count > 0)
                {
                    TreeNode[] anotherArray;
                    int i = 0;
                    int k = 0;
                    anotherArray = new TreeNode[resultList.Count];

                    for (; i < resultList.Count; i++)
                    {
                        anotherArray[k] = new TreeNode(resultList[i].getTitle());
                        anotherArray[k].Tag = resultList[i];
                        k++;
                        resultKey.Add(i);
                    }

                    TreeNode node = new TreeNode("Partial Match", anotherArray);
                    if (!exact)
                    {
                        node.Expand();
                    }
                    resultTree.Nodes.Add(node);
                }
                DateTime builtList = DateTime.Now;
                if ((builtList - buildList).TotalMilliseconds > 1000)
                {
                    resultString = String.Format("{0}\r\n{1}{2}{3}", resultString, "List Built: ", decimal.Round((decimal)(builtList - buildList).TotalSeconds, 2, MidpointRounding.AwayFromZero), " seconds");
                }
                else
                {
                    resultString = String.Format("{0}\r\n{1}0.{2}{3}", resultString, "List Built: ", decimal.Round((decimal)(builtList - buildList).TotalMilliseconds, 0, MidpointRounding.AwayFromZero), " seconds");
                }

                if (resultList.Count == 0 && !exact)
                {
                    resultTree.Nodes.Add(new TreeNode("No Results"));
                }
                DateTime end = DateTime.Now;
                int f = 0;
                if (exactMatch != null)
                {
                    f = 1;
                    exactMatch = null;
                }
                if ((end - start).TotalMilliseconds < 1000)
                {
                    resultString = String.Format("{0}\r\n{1}{2}{3}\r\n\r\n{4}{5}\r\n", resultString, "Entire Operation: ", decimal.Round((decimal)(end - start).TotalSeconds, 2, MidpointRounding.AwayFromZero), " seconds", resultList.Count + f, " results returned");
                }
                else
                {
                    resultString = String.Format("{0}\r\n{1}{2}{3}\r\n\r\n{4}{5}\r\n", resultString, "Entire Operation: ", decimal.Round((decimal)(end - start).TotalSeconds, 0, MidpointRounding.AwayFromZero), " seconds", resultList.Count + f, " results returned");
                }

                resultString = String.Format("{0}{1}{2}\r\n{3}\r\n{4}\r\n", ">>> ", searchParam, " <<<", extraParams, resultString);
                resultInfoT.SelectionStart = 0;
                resultInfoT.SelectionLength = 0;
                resultInfoT.SelectedText = resultString;
                resultString = "";
            }
            else
            {
                resultInfoT.SelectionStart = 0;
                resultInfoT.SelectionLength = 0;
                resultInfoT.SelectedText = "Not a valid search\r\n";
            }

            exactMovie = null;
            performSearch = false;
        }

        private void prompt(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DialogResult dr = MessageBox.Show(searchttString, "Search Parameters", MessageBoxButtons.OK);
            }
        }

        //List View Code
        /* *
         public void searchButton_Click(object sender, EventArgs e)
        {
            List<info> resultList = new List<info>();
            resultList.Clear();
            resultsList.Clear();
            bool exact = false;
            if (searchT.Text.Trim() != "")
            {
                dm.performSearch(searchT.Text.Trim(), ref resultList);
            }
            if (resultList.Count > 0)
            {
                resultsList.Items.Add("Exact Match");
                resultsList.Items.Add(resultList[0].getTitle());
                exact = true;
            }
            dm.performRegexSearch(searchT.Text.Trim(), ref resultList);
            if ((exact && resultList.Count > 1) || (!exact && resultList.Count > 0))
            {
                resultsList.Items.Add("Matched By Regular Expression");
                int i = 0;
                if(exact){
                    i = 1;
                }
                for (; i < resultList.Count; i++)
                {
                    resultsList.Items.Add(resultList[i].getTitle());
                }
            }
            
            if (resultList.Count == 0)
            {
                resultsList.Items.Add("No Results");
            }
        }
         * */
    }
}
