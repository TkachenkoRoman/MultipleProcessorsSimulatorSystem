using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

enum processor_condition {waiting_for_task, processing}

namespace ProcessorsSimulator
{
    class Processor
    {
        public void Processor()
        {
            power = 100; // default
            condition = processor_condition.waiting_for_task;
            current_task = null;
        }
        public int power { get; set; } // n operations per milisecond
        public processor_condition condition { get; set; }
        public Task current_task { get; set; }
    }
}
