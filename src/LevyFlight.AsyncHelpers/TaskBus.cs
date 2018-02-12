using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevyFlight.AsyncHelpers
{
    public class TaskBus
    {
        private readonly IList<Task> _tasks;

        public TaskBus()
        {
            _tasks = new List<Task>();
        }

        public void AddTask(Task task)
        {
            _tasks.Add(task);
        }

        public async Task WaitAll()
        {
            await Task.WhenAll(_tasks);
            _tasks.Clear();
        }
    }
}
