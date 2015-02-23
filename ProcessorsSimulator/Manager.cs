using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

enum conditionManager {waiting_for_task, waiting_for_proc, stopped}

namespace ProcessorsSimulator
{
    class Manager
    {
        public Manager()
        {
            taskQueue = new Queue<Task>();
            processors = new List<Processor>();
            processorsThreads = new Thread[5];
            CreateProcessors();
            CreateGenerator();
            CreateGeneratorThread();
            CreateManageProcessors();
        }

        public conditionManager condition;
        public Queue<Task> taskQueue;
        public Generator generator;
        private List<Processor> processors;
        public Thread generatorThread;
        private Thread processorManager;
        private Thread[] processorsThreads;

        public void Manage()
        {
            StartProcessors();
            StartGenerator();
            StartManageProcessors();    
        }
        private void GetTask(Task task)
        {
            taskQueue.Enqueue(task);
        }
        private void OnWorkDone(object sender, EventArgs e)
        {
            //generatorThread.Abort();
            //processorManager.Abort();
            //AbortProcessors();
            taskQueue = new Queue<Task>(); // reload
            processors = new List<Processor>();
            processorsThreads = new Thread[5];
            CreateProcessors();
            CreateGeneratorThread();
            CreateManageProcessors();
        }
        private void CreateGenerator()
        {
            generator = new Generator();
            generator.workingTime = 10000;
            generator.GenerateTask += new Generator.GenerateTaskHandler(GetTask);
        }
        private void CreateGeneratorThread()
        {
            generatorThread = new Thread(new
                ThreadStart(generator.GenerateTasks));
            generatorThread.Name = "Generator";
        }
        private void StartGenerator()
        {
            generatorThread.Start();
            generator.WorkDone += new EventHandler(OnWorkDone);
        }
        private void CreateProcessors() // filling processors
        {
            for(int i = 0; i < 5; i++)
            {
                Processor currentProc = new Processor();
                processors.Add(currentProc);
                Thread currentThread = new Thread(new ThreadStart(currentProc.DoWork));
                currentThread.Name = "Processor" + (i + 1).ToString();
                if (currentThread != null)
                    processorsThreads[i] = currentThread;
            }
        }
        private void StartProcessors()
        {
            for (int i = 0; i < processorsThreads.Count(); i++)
            {
                processorsThreads[i].Start();
            }
        }
        private void AbortProcessors()
        {
            for (int i = 0; i < processorsThreads.Count(); i++)
            {
                processorsThreads[i].Abort();
            }
        }
        private void CreateManageProcessors()
        {
            processorManager = new Thread(new
                ThreadStart(ManageProcessors));
            processorManager.Name = "Manager";
        }
        private void StartManageProcessors()
        {
            processorManager.Start(); // start managing
        }
        private void ManageProcessors() // if task_queue has tasks - give them to processors
        {
            while(true)
            {
                if (taskQueue.Count != 0)
                {
                    Task currentTask = taskQueue.Peek(); // peeks first elem in queue
                    for(int i = 0; i < processors.Count; i++)
                    {
                        if (processors[i].condition == processor_condition.waitingForTask)
                        {
                            if (taskQueue.Count != 0)
                                currentTask = taskQueue.Dequeue(); // return first elem and delete it
                            processors[i].currentTask = currentTask;
                            processors[i].condition = processor_condition.processing;
                        }             
                    }
                }
                else
                {
                    if (generator.workingTime <= 0)
                        break;
                }
            }
        }
    }
}
