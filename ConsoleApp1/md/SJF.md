# Shortest Job First (SJF)
The Shortest Job First (SJF) scheduling algorithm is a non-preemptive algorithm that selects the process with the smallest burst time to execute next. This algorithm minimizes the average waiting time and turnaround time by executing shorter processes first. The SJF algorithm can be implemented in both preemptive and non-preemptive modes.


# Non-Preemptive
## Code
```csharp
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
```
# Preemptive
## Code
```csharp
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
```
## Difference between Preemptive and Non-Preemptive

The main difference between the preemptive and non-preemptive versions of the SJF algorithm lies in how they handle the execution of processes. In the non-preemptive version, once a process starts executing, it continues until it completes its burst time. This can lead to longer waiting times for processes with longer burst times, as shorter processes may arrive later but get executed first.

In the preemptive version, processes can be interrupted and switched out if a shorter process arrives while a longer process is still executing. This allows for better utilization of the CPU and can lead to shorter waiting times for processes overall. However, the overhead of context switching and managing the execution of multiple processes can impact the performance of the preemptive SJF algorithm.

## Example run