using System;
using System.Collections.Generic;
using System.Linq;

public static class MetricsCalculator
{
    public static void CalculateMetrics(List<Process> processes)
    {
        double averageTurnaroundTime = CalculateAverageTurnaroundTime(processes);
        double averageWaitingTime = CalculateAverageWaitingTime(processes);
        double cpuUtilization = CalculateCPUUtilization(processes);
        double throughput = CalculateThroughput(processes);

        Console.WriteLine("\nPerformance Metrics:");
        Console.WriteLine($"Average Turnaround Time: {averageTurnaroundTime:F2}");
        Console.WriteLine($"Average Waiting Time: {averageWaitingTime:F2}");
        Console.WriteLine($"CPU Utilization: {cpuUtilization * 100:F2}%");
        Console.WriteLine($"Throughput: {throughput:F2} processes/unit time");
    }

    public static double CalculateAverageTurnaroundTime(List<Process> processes)
    {
        return processes.Average(p => p.TurnaroundTime);
    }

    public static double CalculateAverageWaitingTime(List<Process> processes)
    {
        return processes.Average(p => p.WaitingTime);
    }

    public static double CalculateCPUUtilization(List<Process> processes)
    {
        int totalBurstTime = processes.Sum(p => p.BurstTime);
        int totalTime = processes.Max(p => p.CompletionTime);
        return (double)totalBurstTime / totalTime;
    }

    public static double CalculateThroughput(List<Process> processes)
    {
        int totalTime = processes.Max(p => p.CompletionTime);
        return (double)processes.Count / totalTime;
    }
}