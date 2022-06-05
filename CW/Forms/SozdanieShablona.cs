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
    public partial class SozdanieShablona : Form
    {
        public SozdanieShablona()
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
            if (!comboBox1.SelectedItem.ToString().Equals("") && !comboBox2.SelectedItem.ToString().Equals("") &&
                !comboBox3.SelectedItem.ToString().Equals("") && !comboBox4.SelectedItem.ToString().Equals(""))
            {
                Sheet sheet = new Sheet(0, comboBox4.SelectedItem.ToString(), new Group((int)(long)comboBox2.SelectedValue, comboBox2.SelectedText, 
                    new Institute((int)(long)comboBox1.SelectedValue, comboBox1.SelectedText)), new Discipline((int)(long)comboBox3.SelectedValue, comboBox3.SelectedText),
                    new Institute((int)(long)comboBox1.SelectedValue, comboBox1.SelectedText));
                new DBClass().addSheet(sheet);
                Close();
            }
        }
    }
}
