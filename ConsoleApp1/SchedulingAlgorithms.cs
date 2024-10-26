using System;
using System.Collections.Generic;
using System.Linq;

public class SchedulingAlgorithms
{
    public void FirstComeFirstServed(List<Process> processes)
    {
        processes.Sort((p1, p2) => p1.ArrivalTime.CompareTo(p2.ArrivalTime));
        int currentTime = 0;
        foreach (var process in processes)
        {
            if (currentTime < process.ArrivalTime)
            {
                currentTime = process.ArrivalTime;
            }
            process.CompletionTime = currentTime + process.BurstTime;
            process.TurnaroundTime = process.CompletionTime - process.ArrivalTime;
            process.WaitingTime = process.TurnaroundTime - process.BurstTime;
            currentTime += process.BurstTime;
        }
        MetricsCalculator.CalculateMetrics(processes);
    }

    public void ShortestJobFirstNonPreemptive(List<Process> processes)
    {
        processes = processes.OrderBy(p => p.ArrivalTime).ToList();
        int currentTime = 0;
        List<Process> completedProcesses = new List<Process>();

        while (completedProcesses.Count < processes.Count)
        {
            var availableProcesses = processes
                .Where(p => !completedProcesses.Contains(p) && p.ArrivalTime <= currentTime)
                .OrderBy(p => p.BurstTime)
                .ToList();

            if (availableProcesses.Count > 0)
            {
                var process = availableProcesses.First();
                process.CompletionTime = currentTime + process.BurstTime;
                process.TurnaroundTime = process.CompletionTime - process.ArrivalTime;
                process.WaitingTime = process.TurnaroundTime - process.BurstTime;
                completedProcesses.Add(process);
                currentTime += process.BurstTime;
            }
            else
            {
                currentTime++;
            }
        }

        MetricsCalculator.CalculateMetrics(processes);
    }

    public void ShortestJobFirstPreemptive(List<Process> processes)
    {
        processes = processes.OrderBy(p => p.ArrivalTime).ToList();
        int currentTime = 0;
        int completed = 0;
        int totalProcesses = processes.Count;

        while (completed < totalProcesses)
        {
            var availableProcesses = processes
                .Where(p => p.ArrivalTime <= currentTime && p.RemainingTime > 0)
                .OrderBy(p => p.RemainingTime)
                .ToList();

            if (availableProcesses.Count > 0)
            {
                var process = availableProcesses.First();
                process.RemainingTime--;
                currentTime++;

                if (process.RemainingTime == 0)
                {
                    completed++;
                    process.CompletionTime = currentTime;
                    process.TurnaroundTime = process.CompletionTime - process.ArrivalTime;
                    process.WaitingTime = process.TurnaroundTime - process.BurstTime;
                }
            }
            else
            {
                currentTime++;
            }
        }

        MetricsCalculator.CalculateMetrics(processes);
    }

    public void RoundRobin(List<Process> processes, int timeQuantum)
    {
        Queue<Process> readyQueue = new Queue<Process>();
        int currentTime = 0;
        processes = processes.OrderBy(p => p.ArrivalTime).ToList();
        int index = 0;
        int completed = 0;
        int totalProcesses = processes.Count;

        while (completed < totalProcesses)
        {
            while (index < processes.Count && processes[index].ArrivalTime <= currentTime)
            {
                readyQueue.Enqueue(processes[index]);
                index++;
            }

            if (readyQueue.Count > 0)
            {
                var process = readyQueue.Dequeue();

                if (process.RemainingTime > timeQuantum)
                {
                    process.RemainingTime -= timeQuantum;
                    currentTime += timeQuantum;
                    readyQueue.Enqueue(process);
                }
                else
                {
                    currentTime += process.RemainingTime;
                    process.RemainingTime = 0;
                    process.CompletionTime = currentTime;
                    process.TurnaroundTime = process.CompletionTime - process.ArrivalTime;
                    process.WaitingTime = process.TurnaroundTime - process.BurstTime;
                    completed++;
                }
            }
            else
            {
                currentTime++;
            }
        }

        MetricsCalculator.CalculateMetrics(processes);
    }

    public void PrioritySchedulingNonPreemptive(List<Process> processes)
    {
        processes = processes.OrderBy(p => p.ArrivalTime).ToList();
        int currentTime = 0;
        List<Process> completedProcesses = new List<Process>();

        while (completedProcesses.Count < processes.Count)
        {
            var availableProcesses = processes
                .Where(p => !completedProcesses.Contains(p) && p.ArrivalTime <= currentTime)
                .OrderBy(p => p.Priority)
                .ToList();

            if (availableProcesses.Count > 0)
            {
                var process = availableProcesses.First();
                process.CompletionTime = currentTime + process.BurstTime;
                process.TurnaroundTime = process.CompletionTime - process.ArrivalTime;
                process.WaitingTime = process.TurnaroundTime - process.BurstTime;
                completedProcesses.Add(process);
                currentTime += process.BurstTime;
            }
            else
            {
                currentTime++;
            }
        }

        MetricsCalculator.CalculateMetrics(processes);
    }

    public void PrioritySchedulingPreemptive(List<Process> processes)
    {
        processes = processes.OrderBy(p => p.ArrivalTime).ToList();
        int currentTime = 0;
        int completed = 0;
        int totalProcesses = processes.Count;

        while (completed < totalProcesses)
        {
            var availableProcesses = processes
                .Where(p => p.ArrivalTime <= currentTime && p.RemainingTime > 0)
                .OrderBy(p => p.Priority)
                .ToList();

            if (availableProcesses.Count > 0)
            {
                var process = availableProcesses.First();
                process.RemainingTime--;
                currentTime++;

                if (process.RemainingTime == 0)
                {
                    completed++;
                    process.CompletionTime = currentTime;
                    process.TurnaroundTime = process.CompletionTime - process.ArrivalTime;
                    process.WaitingTime = process.TurnaroundTime - process.BurstTime;
                }
            }
            else
            {
                currentTime++;
            }
        }

        MetricsCalculator.CalculateMetrics(processes);
    }

    public void MultilevelQueueScheduling(List<Process> processes, int timeQuantum)
    {
        Queue<Process> fcfsQueue = new Queue<Process>();
        Queue<Process> rrQueue = new Queue<Process>();

        int currentTime = 0;
        processes = processes.OrderBy(p => p.ArrivalTime).ToList();
        int index = 0;

        while (index < processes.Count || fcfsQueue.Count > 0 || rrQueue.Count > 0)
        {
            while (index < processes.Count && processes[index].ArrivalTime <= currentTime)
            {
                if (processes[index].Priority <= 5)
                {
                    fcfsQueue.Enqueue(processes[index]);
                }
                else
                {
                    rrQueue.Enqueue(processes[index]);
                }
                index++;
            }

            if (fcfsQueue.Count > 0)
            {
                var process = fcfsQueue.Dequeue();
                currentTime += process.BurstTime;
                process.CompletionTime = currentTime;
                process.TurnaroundTime = process.CompletionTime - process.ArrivalTime;
                process.WaitingTime = process.TurnaroundTime - process.BurstTime;
            }
            else if (rrQueue.Count > 0)
            {
                var process = rrQueue.Dequeue();
                if (process.RemainingTime > timeQuantum)
                {
                    process.RemainingTime -= timeQuantum;
                    currentTime += timeQuantum;
                    rrQueue.Enqueue(process);
                }
                else
                {
                    currentTime += process.RemainingTime;
                    process.RemainingTime = 0;
                    process.CompletionTime = currentTime;
                    process.TurnaroundTime = process.CompletionTime - process.ArrivalTime;
                    process.WaitingTime = process.TurnaroundTime - process.BurstTime;
                }
            }
            else
            {
                currentTime++;
            }
        }

        MetricsCalculator.CalculateMetrics(processes);
    }
}
