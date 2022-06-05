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
using CW.Data;

namespace CW.Forms
{
    public partial class ZapolnenieVedomosti : Form
    {
        Sheet sheet1;
        public ZapolnenieVedomosti()
        {
            InitializeComponent();
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count == 0)
            {
                DBClass db = new();
                DataTable dt = db.getInstitutes();
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "id";
                comboBox1.DataSource = dt;
                comboBox2.Enabled = true;
            }
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            DBClass db = new();
            DataTable dt = db.getGroups((int)(long)comboBox1.SelectedValue);
            comboBox2.DisplayMember = "name";
            comboBox2.ValueMember = "id";
            comboBox2.DataSource = dt;
        }

        private void comboBox3_Click(object sender, EventArgs e)
        {
            if (comboBox3.Items.Count == 0)
            {
                DBClass db = new();
                DataTable dt = db.getDisciplines();
                comboBox3.DisplayMember = "name";
                comboBox3.ValueMember = "id";
                comboBox3.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!comboBox1.SelectedItem.ToString().Equals("") && !comboBox2.SelectedItem.ToString().Equals("") && !comboBox3.SelectedItem.ToString().Equals(""))
            {
                Sheet sheet = new Sheet(0, "", new Group((int)(long)comboBox2.SelectedValue, comboBox2.SelectedText,
                    new Institute((int)(long)comboBox1.SelectedValue, comboBox1.SelectedText)), new Discipline((int)(long)comboBox3.SelectedValue, comboBox3.SelectedText),
                    new Institute((int)(long)comboBox1.SelectedValue, comboBox1.SelectedText));
                sheet = new DBClass().getSheet(sheet);
                if (sheet.id != 0)
                {
                    sheet1 = sheet;
                    DataGridViewComboBoxColumn mark = new DataGridViewComboBoxColumn();
                    if (sheet.type.Equals("Экзамен") || sheet.type.Equals("зачёт с оценкой"))
                    {
                        var list11 = new List<string>() { "2", "3", "4", "5", "Неявка" };
                        mark.DataSource = list11;
                    }
                    else
                    {
                        var list11 = new List<string>() { "Зачёт", "Незачёт" };
                        mark.DataSource = list11;
                    }
                    mark.HeaderText = "Оценка";
                    mark.DataPropertyName = "mark";

                    DataTable dt = new DBClass().getStudents(sheet);
                    dt = new DBClass().getStudents(sheet);
                    dataGridView1.DataSource = dt;
                    bool b = dataGridView1.Columns.Count < 5;
                    if (dataGridView1.Columns.Count < 5)
                    {
                        dataGridView1.Columns.Add(mark);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> marks = new();
            List<int> students = new();
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (item.Cells[4].Value.ToString().Equals("Неявка"))
                {
                    marks.Add("-1");
                }
                else
                {
                    marks.Add(item.Cells[4].Value.ToString());
                }
                students.Add((int)(long)item.Cells[0].Value);
            }
            int id = sheet1.id;
            new DBClass().addMarks(marks, students, id);
            Close();
        }
    }
}
