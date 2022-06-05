using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CW.DB;

namespace CW.Forms
{
    public partial class StatisticsF : Form
    {
        public StatisticsF()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                comboBox1.Enabled = true;
                comboBox2.Enabled = false;
                dataGridView1.Columns["nameD"].Visible = true;
                dataGridView1.Columns["numberD"].Visible = true;
                dataGridView1.Columns["marks"].Visible = false;
                dataGridView1.Columns["marksN"].Visible = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                comboBox1.Enabled = false;
                comboBox2.Enabled = true;
                dataGridView1.Columns["nameD"].Visible = false;
                dataGridView1.Columns["numberD"].Visible = false;
                dataGridView1.Columns["marks"].Visible = true;
                dataGridView1.Columns["marksN"].Visible = true;
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count == 0)
            {
                DBClass db = new();
                DataTable dt = db.getDisciplines();
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "id";
                comboBox1.DataSource = dt;
            }
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Items.Count == 0)
            {
                DBClass db = new();
                DataTable dt = db.getInstitutes();
                comboBox2.DisplayMember = "name";
                comboBox2.ValueMember = "id";
                comboBox2.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if(comboBox1.Text.Length != 0)
                {
                    dataGridView1.Rows.Clear();
                    string dis = comboBox1.Text;
                    DataTable dt = new DBClass().getFailed(dis);
                    int num = dt.Rows.Count;
                    if(dataGridView1.RowCount == 0)
                    {
                        dataGridView1.Rows.Add();
                    }
                    dataGridView1.Rows[0].Cells[0].Value = dis;
                    dataGridView1.Rows[0].Cells[1].Value = num;
                }
            }
            else
            {
                if (comboBox2.Text.Length != 0)
                {
                    List<string> marks = new() { "5", "4", "3", "2", "Неявка" };
                    dataGridView1.Rows.Clear();
                    foreach(string mark in marks)
                    {
                        DataTable dt = new();
                        if (mark.Equals("Неявка"))
                        {
                            dt = new DBClass().getMarks(comboBox2.SelectedValue.ToString(), "-1");
                        }
                        else
                        {
                            dt = new DBClass().getMarks(comboBox2.SelectedValue.ToString(), mark);
                        }
                        int num = dt.Rows.Count;
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[2].Value = mark;
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[3].Value = num;
                    }
                }
            }
        }
    }
}
