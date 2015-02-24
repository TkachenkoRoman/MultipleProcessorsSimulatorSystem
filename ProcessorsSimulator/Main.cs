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
            this.manager.ProcessorsWorkDone += new EventHandler(OnWorkDone); // enable to start again
            this.manager.QueueModified += new EventHandler(OnQueueModified);
            this.manager.SendTaskToProcessor += new EventHandler(BlinkWhenTaskSended);
        }
        private void OnQueueModified(object sender, EventArgs e)
        {
            string res = "";
            if (manager.taskQueue.Count() != 0)
            {
                foreach (Task task in manager.taskQueue)
                {
                    res += String.Format("{0}. Task (operationsAmount={1}, supportedProcessors={2}){3}", 
                                        task.id.ToString(), task.operationsAmont.ToString(), task.getSupportedProcessors(), "\n");
                }
            }
            this.Invoke((MethodInvoker)delegate { richTextBoxManagerQueue.Text = res; });
        }
        private void OnWorkDone(object sender, EventArgs e)
        {
            //this.buttonStart.Enabled = true;
            this.Invoke((MethodInvoker)delegate { buttonStart.Enabled = true; });
        }
        private void BlinkWhenTaskSended(object sender, EventArgs e)
        {
            this.pictureBoxManagerIndicator.BackColor = System.Drawing.SystemColors.Highlight;
            Application.DoEvents();
            Thread.Sleep(100);
            this.pictureBoxManagerIndicator.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        }
        private void BlinkWhenNewTaskGenerated(Task task)
        {
            this.pictureBoxGeneratorIndicator.BackColor = System.Drawing.SystemColors.Highlight;
            Application.DoEvents();
            Thread.Sleep(100);
            this.pictureBoxGeneratorIndicator.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        }
        private void buttonGeneratorUpdate_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            manager.generator.workingTime = 5000; // reload
            this.buttonStart.Enabled = false;
            manager.Manage();
        }
    }
}
