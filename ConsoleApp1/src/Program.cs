using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        SchedulingAlgorithms scheduler = new SchedulingAlgorithms();
        int timeQuantum = 4;
        
        Console.WriteLine("Running First-Come, First-Served (FCFS)...");
        var processesFCFS = GenerateRandomProcesses(20);
        scheduler.FirstComeFirstServed(processesFCFS);
        DisplayProcessInfo(processesFCFS);

        Console.WriteLine("\nRunning Shortest Job First (SJF) - Non-Preemptive...");
        var processesSJFNonPreemptive = GenerateRandomProcesses(20);
        scheduler.ShortestJobFirstNonPreemptive(processesSJFNonPreemptive);
        DisplayProcessInfo(processesSJFNonPreemptive);

        Console.WriteLine("\nRunning Shortest Job First (SJF) - Preemptive...");
        var processesSJFPreemptive = GenerateRandomProcesses(20);
        scheduler.ShortestJobFirstPreemptive(processesSJFPreemptive);
        DisplayProcessInfo(processesSJFPreemptive);

        Console.WriteLine("\nRunning Round Robin (RR)...");
        var processesRR = GenerateRandomProcesses(20);
        scheduler.RoundRobin(processesRR, timeQuantum);
        DisplayProcessInfo(processesRR);

        Console.WriteLine("\nRunning Priority Scheduling - Non-Preemptive...");
        var processesPriorityNonPreemptive = GenerateRandomProcesses(20);
        scheduler.PrioritySchedulingNonPreemptive(processesPriorityNonPreemptive);
        DisplayProcessInfo(processesPriorityNonPreemptive);

        Console.WriteLine("\nRunning Priority Scheduling - Preemptive...");
        var processesPriorityPreemptive = GenerateRandomProcesses(20);
        scheduler.PrioritySchedulingPreemptive(processesPriorityPreemptive);
        DisplayProcessInfo(processesPriorityPreemptive);

        Console.WriteLine("\nRunning Multilevel Queue Scheduling...");
        var processesMultilevelQueue = GenerateRandomProcesses(20);
        scheduler.MultilevelQueueScheduling(processesMultilevelQueue, timeQuantum);
        DisplayProcessInfo(processesMultilevelQueue);
    }
    
    static List<Process> GenerateRandomProcesses(int count)
    {
        Random rand = new Random();
        List<Process> processes = new List<Process>();

        for (int i = 1; i <= count; i++)
        {
            int arrivalTime = rand.Next(0, 10);
            int burstTime = rand.Next(1, 10);
            int priority = rand.Next(1, 10);

            processes.Add(new Process(i, arrivalTime, burstTime, priority));
        }

        return processes;
    }
    
    static void DisplayProcessInfo(List<Process> processes)
    {
        Console.WriteLine("ProcessID\tArrivalTime\tBurstTime\tCompletionTime\tTurnaroundTime\tWaitingTime\tPriority");
        foreach (var process in processes)
        {
            Console.WriteLine($"{process.ProcessId}\t\t{process.ArrivalTime}\t\t{process.BurstTime}\t\t{process.CompletionTime}\t\t{process.TurnaroundTime}\t\t{process.WaitingTime}\t\t{process.Priority}");
        }
        
        double averageTurnaroundTime = MetricsCalculator.CalculateAverageTurnaroundTime(processes);
        double averageWaitingTime = MetricsCalculator.CalculateAverageWaitingTime(processes);
        double cpuUtilization = MetricsCalculator.CalculateCPUUtilization(processes);
        double throughput = MetricsCalculator.CalculateThroughput(processes);

        Console.WriteLine("\nPerformance Metrics:");
        Console.WriteLine($"Average Turnaround Time: {averageTurnaroundTime:F2}");
        Console.WriteLine($"Average Waiting Time: {averageWaitingTime:F2}");
        Console.WriteLine($"CPU Utilization: {cpuUtilization * 100:F2}%");
        Console.WriteLine($"Throughput: {throughput:F2} processes/unit time");
    }
}
