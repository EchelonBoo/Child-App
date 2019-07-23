using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab03
{
    public partial class Form1 : Form
    {
        List<ChildDetails> children = new List<ChildDetails>(); 

        public Form1()
        {
            InitializeComponent();

            tabControl1.SelectedIndex = 0; // default open in first tab
            tab0(); // call the first tab function
        }

        private void DisplayTabInformation()
        {

             if (tabControl1.SelectedIndex == 0)
              {
                  tab0();
              }
              else if (tabControl1.SelectedIndex == 1)
              {
                  tab1();
              }
              else if (tabControl1.SelectedIndex == 2)
              {
                  tab2();
              }
              else if (tabControl1.SelectedIndex == 3)
              {
                  tab3();
              }
              else if (tabControl1.SelectedIndex == 4)
              {
                  tab4();
              }
              else if (tabControl1.SelectedIndex == 5)
              {
                  tab5();
              }
        }

        private void tab0()
        {
            listUpdate(listView1); // call list update and adds children to listview
        }

        private void tab1()
        {
            listUpdate(listView2); // call list update and adds children to listview
        }

        private void tab2()
        {
            listUpdate(listView3); // call list update and adds children to listview
        }

        private void tab3()
        {
            listUpdate(listView4);
        }

        private void tab4() { //done


            var newDate = DateTime.Now.Date.AddDays(7).ToShortDateString();
            listView5.Items.Clear();

            foreach(ChildDetails child in children)
            {
                int difference = child.DOB.DayOfYear - DateTime.Now.DayOfYear;

                if(difference < 3 && difference >= 0)
                {
                    int ageNew = child.age + 1;

                    ListViewItem row = new ListViewItem(child.name);
                                        row.SubItems.Add(child.DOB.ToShortDateString());
                                        row.SubItems.Add(child.fact);
                                        row.SubItems.Add(ageNew.ToString());

                    listView5.Items.Add(row);
                    


                }
            }

        }

        private void tab5()
        {
            listView6.Items.Clear();

            foreach (ChildDetails c in children)
            {
                DateTime data = dateTimePicker3.Value;
                int new_age = data.Year - c.DOB.Year;

                if (data.DayOfYear < c.DOB.DayOfYear)
                {
                    new_age--;
                }

                if (data.Year <= c.DOB.Year)
                {
                    new_age = 0;
                }

                ListViewItem row = new ListViewItem(c.name);
                             row.SubItems.Add(c.DOB.ToShortDateString());
                             row.SubItems.Add(c.fact);
                             row.SubItems.Add(new_age.ToString());

                listView6.Items.Add(row);

                if (new_age <= 5 && new_age > 1)
                {
                    row.BackColor = Color.Yellow;
                }
                if (new_age > 5 && new_age < 19)
                {
                    row.BackColor = Color.Green;
                }
            }

        }

        private void listUpdate(ListView a)
        {
            a.Items.Clear();
            foreach (ChildDetails child in children) //adds each member of the collection to listiew sub items
            {
                ListViewItem rows = new ListViewItem(child.name);

                rows.SubItems.Add(child.DOB.ToShortDateString());
                rows.SubItems.Add(child.fact);
                rows.SubItems.Add(child.age.ToString());
                a.Items.Add(rows);
            }
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView3.SelectedItems.Count > 0)
            {
                ListViewItem child = listView3.SelectedItems[0];

                txtName2.Text = child.SubItems[0].Text;
                dateTimePicker2.Text = child.SubItems[1].Text;
                txtFact2.Text = child.SubItems[2].Text;
                    
            }
            else
            {
                txtName2.Text = string.Empty;
                dateTimePicker2.Value = DateTime.Today;
                txtFact2.Text = string.Empty;
            }
        }


        private void LoadChildren() //Function that loads in the data
        {
            children.Clear(); // clears listview before 

            List<string> data = File.ReadAllLines("lab03input.txt").ToList(); // reads in data from text file line by line


            foreach (string line in data) //loop through the members in the file and write them into a list of tyoe ChildDetails
            {
                string[] info = line.Split('-');
                ChildDetails child = new ChildDetails((info[0].Trim()), DateTime.Parse(info[1].Trim()), info[2].Trim());

                children.Add(child); //Add them all to the children collection
            }

        }

        
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayTabInformation();
        }

        private void btnLoad_Click(object sender, EventArgs e) // done
        {
            children.Clear(); // clears listview before 

            List<string> data = File.ReadAllLines("lab03input.txt").ToList(); // reads in data from text file line by line


            foreach (string line in data) //loop through the members in the file and write them into a list of tyoe ChildDetails
            {
                string[] info = line.Split('-');
                ChildDetails child = new ChildDetails((info[0].Trim()), DateTime.Parse(info[1].Trim()), info[2].Trim());

                children.Add(child); //Add them all to the children collection
            }
            tabControl1.SelectedIndex = 0;
            tab0();
           
        }

        private void btnSave_Click(object sender, EventArgs e) //done
        {
        List<string> data = new List<string>();

        foreach (ChildDetails child in children) //saves each member of the collection and their details with a dash between them to the list
        {
            data.Add(child.name + "-" + child.DOB.ToShortDateString() + "-" + child.fact);
        }
        File.WriteAllLines("lab03input.txt", data); // writes each member of the list to the text file
        MessageBox.Show("All data has been saved!", "Success!"); // message box to show user that it saved
        }

        private void btnLoad2_Click(object sender, EventArgs e) //done
        {
            LoadChildren();

            tabControl1.SelectedIndex = 1;
            tab1();
           
        }

        private void btnAdd_Click(object sender, EventArgs e) //done
        {
            ChildDetails c = new ChildDetails(txtName.Text, dateTimePicker1.Value, txtFact.Text); // get info entered in the fields
            children.Add(c); // adds new child to children collection

            MessageBox.Show("New child added to system!", "Success!");
            listUpdate(listView2);
        }

        private void btnLoad3_Click(object sender, EventArgs e)//done
        {
            LoadChildren();

            tabControl1.SelectedIndex = 2;
            tab2();
            
        }

        private void btnUpdate_Click(object sender, EventArgs e) //done
        {
            

            ListViewItem child = listView3.SelectedItems[0];

            int childIndex = child.Index;

            children[childIndex].name = txtName2.Text;
            children[childIndex].DOB = dateTimePicker2.Value;
            children[childIndex].fact = txtFact2.Text;

            listUpdate(listView3);

        }

        private void btnLoad4_Click(object sender, EventArgs e)
        {
            LoadChildren();

            tabControl1.SelectedIndex = 3;
            tab3();
           
        }

        private void btnDelete_Click(object sender, EventArgs e) // done
        {
            ListView.CheckedIndexCollection checkeditem = listView4.CheckedIndices;

            while(checkeditem.Count > 0)
            {
                listView4.Items.RemoveAt(checkeditem[0]);
            }

            children.Clear();

            int count  = 0;

            foreach(ListViewItem i in listView4.Items)
            {
                string Name = listView4.Items[count].SubItems[0].Text;
                DateTime DOB = Convert.ToDateTime(listView4.Items[count].SubItems[1].Text);
                string Fact = listView4.Items[count].SubItems[2].Text;

                ChildDetails new_Child = new ChildDetails(Name, DOB, Fact);

                children.Add(new_Child);
                count++;
            }
        }

        private void btnLoad5_Click(object sender, EventArgs e)
        {
            LoadChildren();

            tabControl1.SelectedIndex = 4;
            tab4();
           
        }

        private void btnLoad6_Click(object sender, EventArgs e)
        {
           LoadChildren();

            tabControl1.SelectedIndex = 5;
            tab5();
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                txtName2.Text = item.SubItems[0].Text;
                dateTimePicker2.Text = (item.SubItems[1].Text);
                txtFact2.Text = item.SubItems[2].Text;
            }
            else
            {
                txtName2.Text = string.Empty;
                dateTimePicker2.Value = DateTime.Today;
                txtFact2.Text = string.Empty;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e) // exit button
        {
            this.Close();
        }

        private void btnCancel2_Click(object sender, EventArgs e)// exit button
        {
            this.Close();
        }

        private void btnCancel3_Click(object sender, EventArgs e)// exit button
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)// exit button
        {
            this.Close();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            tab5();
        }

        private void btnExit2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void listView6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //sorting
       
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Sort_listview(listView1, e);
        }

        private void listView2_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Sort_listview(listView2, e);
        }

        private void listView3_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Sort_listview(listView3, e);
        }

        private void listView4_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Sort_listview(listView4, e);
        }

        private void listView5_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Sort_listview(listView5, e);
        }

        private void listView6_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Sort_listview(listView6, e);
        }

        private ColumnHeader SortingColumn = null;
        public void Sort_listview(ListView listview, ColumnClickEventArgs e)
        {
            ColumnHeader new_sorting_column = listview.Columns[e.Column];

            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn == null)
            {
                sort_order = SortOrder.Ascending;
            }
            else
            {
                if (new_sorting_column == SortingColumn)
                {
                    if (SortingColumn.Text.StartsWith("> "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    sort_order = SortOrder.Ascending;
                }

                SortingColumn.Text = SortingColumn.Text.Substring(2);
            }

            SortingColumn = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn.Text = "> " + SortingColumn.Text;
            }
            else
            {
                SortingColumn.Text = "< " + SortingColumn.Text;
            }

            listview.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            listview.Sort();
        }

    }
}
