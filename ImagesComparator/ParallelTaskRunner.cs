using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImagesComparator
{
    internal class ParallelTaskRunner
    {
        private List<Task> _tasks = new List<Task>();

        public void Add(Task task)
        {
            _tasks.Add(task);
        }

        public void StartAllTasksAndWaitForFinish()
        {
            foreach (var task in _tasks)
            {
                task.Start();
            }

            Task.WaitAll(_tasks.ToArray());
        }

        public List<int> CalculateTaskRanges(int min, int max, int parallelThreadsCount)
        {
            var result = new List<int>();

            var step = (max - min) / parallelThreadsCount;

            result.Add(min);

            while (result[result.Count - 1] < max)
            {
                var lastItem = result[result.Count - 1];

                result.Add(lastItem + step);
                result.Add(lastItem + step + 1);
            }

            result.RemoveAt(result.Count - 1);
            result.RemoveAt(result.Count - 1);
            result.Add(max);

            return result;
        }
    }
}
