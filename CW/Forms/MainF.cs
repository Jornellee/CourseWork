using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CW.Forms;

namespace CW
{
    public partial class MainF : Form
    {
        public MainF()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SozdanieShablona f = new();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ZapolnenieVedomosti f = new();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StatisticsF f = new();
            f.Show();
        }
    }
}
