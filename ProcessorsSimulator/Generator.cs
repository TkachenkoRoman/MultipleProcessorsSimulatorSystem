using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProcessorsSimulator
{
    class Generator
    {
        public Generator()
        {
            probability = 0.1; //default, should be in range 0..1
            taskComplexityScope = new int[2] { 300, 1000 }; //default
        }
        public double probability { get; set; }
        public int workingTime { get; set; }
        public int[] taskComplexityScope { get; set; }
        public delegate void GenerateTaskHandler(Task task);
        public event GenerateTaskHandler GenerateTask;
        public void GenerateTasks()
        {
            int sleepTime = (int) Math.Round(workingTime * probability, MidpointRounding.AwayFromZero);
            Random random = new Random();

            while (workingTime > 0)
            {
                Thread.Sleep(sleepTime); // simulate waiting for task (create task every n miliseconds)
                workingTime -= sleepTime; 
                Task currentTask = new Task(); 

                currentTask.operationsAmont = random.Next(taskComplexityScope[0], taskComplexityScope[1] + 1); // creates random in my scope range
                int randomProcessorsAmount = random.Next(1, 6); // random processors amount 1..5 
                //currentTask.supportedProcessors = new int[] { 1, 2, 3, 4, 5 };
                currentTask.supportedProcessors = new int[randomProcessorsAmount];
                for (int i = 0; i < randomProcessorsAmount; i++)
                {
                    int processorNumber = random.Next(1, 6);
                    if (currentTask.supportedProcessors != null || currentTask.supportedProcessors.Length != 0) // if array isn`t empty
                        while (currentTask.supportedProcessors.Contains(processorNumber)) // prevent number dublication
                        {
                            processorNumber = random.Next(1, 6);
                        }
                    currentTask.supportedProcessors[i] = processorNumber; // random processor number 
                }

                if (GenerateTask != null) GenerateTask(currentTask); // call GenerateTask event if smb subscribed 
            }
        }
    }
}
