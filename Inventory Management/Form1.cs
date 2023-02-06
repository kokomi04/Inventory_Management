using Inventory_Management.DAO;
using Inventory_Management.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management
{
    public partial class InventoryManagement : Form
    {
        public InventoryManagement()
        {
            InitializeComponent();
            Load();
            
        }

        void Load()
        {
            listView1.Items.Clear();
            List<Load> load = LoadDAO.Instance.LoadMenu();
            foreach (Load item in load)
            {
                ListViewItem lvItem = new ListViewItem(item.Name);
                lvItem.SubItems.Add(item.Ngoaichuyen.ToString());
                lvItem.SubItems.Add(item.Trongkho.ToString());
                lvItem.SubItems.Add(item.Mica.ToString());
                lvItem.SubItems.Add(item.Tray.ToString());
                lvItem.SubItems.Add(item.Note.ToString());
                listView1.Items.Add(lvItem);
                
            }
            comboBox1.DataSource = load;
            comboBox1.DisplayMember ="Name";
            comboBox1.ValueMember = "Id";
            if(comboBox1.Tag != null)
            {
                int index = (int)comboBox1.Tag;
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf(load[index]);
            }

            comboBox3.DataSource = load;
            comboBox3.DisplayMember = "Name";
            comboBox3.ValueMember = "Id";
            if (comboBox3.Tag != null)
            {
                int index = (int)comboBox3.Tag;
                comboBox3.SelectedIndex = comboBox3.Items.IndexOf(load[index]);
            }
            comboBox2.SelectedItem = "Duy";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lv = (sender as ListView);         
            int duy = lv.SelectedItems[0].Index;
            comboBox1.Tag = duy;
            comboBox3.Tag = duy;
            Load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)comboBox1.SelectedValue;
                string nameProduct = (comboBox1.SelectedItem as Load).Name;
                int count = (int)numericUpDown1.Value;
                int type = 0;
                string unit ="";
                if (radioButton1.Checked == true)
                {
                    type = 1;
                    unit = "Bộ";
                }      
                if (radioButton2.Checked == true)
                {
                    type = 2;
                    unit = "Mica";
                }
                if (radioButton3.Checked == true)
                {
                    type = 3;
                    unit = "Tray";
                } 
                string nameUser = comboBox2.Text ;
                DateTime dateEdit = dateTimePicker1.Value.Date;
                string note = richTextBox1.Text;

                if(InoutDAO.Instance.Nhap(id, count, type, nameUser, dateEdit, note) > 0)
                {
                    writeLog.Log(nameProduct, count, unit, nameUser, "Nhập kho");
                }
                Load();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Tên hàng không tồn tại. Hãy thêm hàng mới", "Thông báo");
            }        
        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)comboBox1.SelectedValue;
                string nameProduct = (comboBox1.SelectedItem as Load).Name;
                int count = (int)numericUpDown1.Value;
                int type = 0;
                string unit = "";
                if (radioButton1.Checked == true)
                {
                    type = 1;
                    unit = "Bộ";
                }
                if (radioButton2.Checked == true)
                {
                    type = 2;
                    unit = "Mica";
                }
                if (radioButton3.Checked == true)
                {
                    type = 3;
                    unit = "Tray";
                }
                string nameUser = comboBox2.Text;
                DateTime dateEdit = dateTimePicker1.Value.Date;
                string note = richTextBox1.Text;
                if (InoutDAO.Instance.Xuat(id, count, type, nameUser, dateEdit, note) > 0)
                {
                    writeLog.Log(nameProduct, count, unit, nameUser, "Xuất kho");
                }
                Load();                
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Tên hàng không tồn tại. Hãy thêm hàng mới", "Thông báo");
            }           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nameProduct = comboBox3.Text;
            int bo1 = (int)numericUpDown3.Value;
            int bo2 = (int)numericUpDown2.Value;

            ProductDAO.Instance.AddProduct(nameProduct);
            InoutDAO.Instance.AddInout(bo1, bo2);
            Load();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = (int)comboBox3.SelectedValue;
            ProductDAO.Instance.DelProduct(id);
            comboBox1.Tag = null;
            comboBox3.Tag = null;
            Load();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int id = (int)comboBox3.SelectedValue;
            int bo1 = (int)numericUpDown3.Value;
            int bo2 = (int)numericUpDown2.Value;

            ProductDAO.Instance.EditProduct(id, bo1, bo2);
            Load();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string logPath = string.Format(@"{0}\Log", Environment.CurrentDirectory);

            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            Process prc = new Process();
            prc.StartInfo.FileName = logPath;
            prc.Start();
        }
    }
}
