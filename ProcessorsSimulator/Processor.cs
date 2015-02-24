using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

enum processor_condition {waitingForTask, processing}

namespace ProcessorsSimulator
{
    class Processor
    {
        public Processor()
        {
            power = 100; // default
            condition = processor_condition.waitingForTask;
            currentTask = null;
        }
        public int power { get; set; } // n operations per milisecond
        public processor_condition condition { get; set; }
        public Task currentTask { get; set; }
        public void DoWork()
        {
            while(true)
            {
                if (condition == processor_condition.processing && currentTask != null)
                {
                    Thread.Sleep(1000); // TODO
                    condition = processor_condition.waitingForTask; // work done, processor is free
                }
                else
                {
                    Thread.Sleep(10);
                }
                    
            } 
        }
    }
}
