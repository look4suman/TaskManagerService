using System.Linq;
using NBench;
using System.Diagnostics.CodeAnalysis;
using TaskManager.Api.Controllers;
using TaskManager.Entities;
using TaskManager.BusinessLayer;

namespace TaskMaster.Tests
{
    [ExcludeFromCodeCoverage]
    public class TaskManager
    {
        private Counter _counter;
        private TaskController _controller;
        private TaskModel _task;
        private int _taskId;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
            _controller = new TaskController();
            _task = new TaskModel() { Task = "Test1", ParentTask = "Test2", Priority = 10, StartDate = System.DateTime.Now.ToString(), EndDate = System.DateTime.Now.AddDays(7).ToString() };
            _taskId = new TaskApplication().GetTasks().FirstOrDefault().TaskId;
        }

        [PerfBenchmark(Description = "Add task through put test.",
        NumberOfIterations = 3, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 50, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void AddTask()
        {
            _controller.AddTask(_task);
            _counter.Increment();
        }

        [PerfBenchmark(Description = "Get Specific task.",
        NumberOfIterations = 3, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 50, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void GetSpecificTask()
        {
            _controller.Task(_taskId);
            _counter.Increment();
        }

        [PerfCleanup]
        public void Cleanup()
        {

        }
    }
}
