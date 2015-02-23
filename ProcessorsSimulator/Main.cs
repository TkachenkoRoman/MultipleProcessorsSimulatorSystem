using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ProcessorsSimulator
{
    public partial class Main : Form
    {
        private Manager manager;
        public Main()
        {
            InitializeComponent();
            manager = new Manager();
            ManageInterface();
        }
      
        private void ManageInterface()
        {
            this.manager.generator.GenerateTask += new Generator.GenerateTaskHandler(BlinkWhenNewTaskGenerated);
        }
        private void BlinkWhenNewTaskGenerated(Task task)
        {
            this.pictureBoxGeneratorIndicator.BackColor = System.Drawing.SystemColors.Highlight;
            Thread.Sleep(500);
            this.pictureBoxGeneratorIndicator.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        }
        private void buttonGeneratorUpdate_Click(object sender, EventArgs e)
        {

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            manager.generator.workingTime = 10000; // reload
            manager.Manage();
        }
    }
}
