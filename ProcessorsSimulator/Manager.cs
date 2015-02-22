using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

enum condition_manager {waiting_for_task, waiting_for_proc, stopped}

namespace ProcessorsSimulator
{
    class Manager
    {
        public Manager()
        {
            task_queue = new Queue<Task>();
        }
        public condition_manager condition;
        public Queue<Task> task_queue;
        private Generator generator;
        private List<Processor> processors;
        private Thread generator_thread;
        public void manage()
        {
            create_processors();
            create_generator();
            start_generator();
            manage_processors();
        }
        private void get_task(Task task)
        {
            task_queue.Enqueue(task);
        }
        private void create_generator()
        {
            generator = new Generator();
            generator.workingTime = 10000;
            generator_thread = new Thread(new
                ThreadStart(generator.GenerateTasks));
            generator.GenerateTask += new Generator.GenerateTaskHandler(get_task);
        }
        private void start_generator()
        {
            generator_thread.Start();
        }
        private void create_processors() // filling processors
        {
            Processor p1 = new Processor();
            Processor p2 = new Processor();
            Processor p3 = new Processor();
            Processor p4 = new Processor();
            Processor p5 = new Processor();
        }
        private void manage_processors() // if task_queue has tasks - give them to processors
        {

        }
    }
}
