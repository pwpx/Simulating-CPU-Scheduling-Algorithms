# Priority Scheduling

Priority Scheduling is a non-preemptive CPU scheduling algorithm where each process is assigned a priority. The process with the highest priority is selected for execution first. If two processes have the same priority, they are executed in the order they arrive. Priority can be defined based on various factors such as the process's burst time, arrival time, or any other criteria. It can be implemented with both preemptive and non-preemptive versions.

# Non-Preemptive

In the non-preemptive version of Priority Scheduling, once a process starts executing, it continues until it completes its burst time. The priority of a process is determined at the time of arrival and remains fixed throughout its execution. The process with the highest priority is selected for execution first, and the other processes wait in the ready queue. If two processes have the same priority, they are executed in the order they arrive.

## Code

```csharp
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
```

# Example run

```Performance Metrics:
Average Turnaround Time: 51.80
Average Waiting Time: 46.60
CPU Utilization: 100.00%
Throughput: 0.19 processes/unit time

ProcessID       ArrivalTime     BurstTime       CompletionTime  TurnaroundTime  WaitingTime     Priority
1               4               6               17              13              7               2
2               8               5               75              67              62              6
3               9               2               94              85              83              8
4               0               9               33              33              24              4
5               2               4               6               4               0               1
6               1               4               79              78              74              7
7               0               3               20              20              17              3
8               3               5               55              52              47              5
9               6               2               104             98              96              9
10              4               8               70              66              58              6
11              6               5               92              86              81              8
12              2               4               24              22              18              3
13              0               2               2               2               0               2
14              2               5               11              9               4               2
15              3               1               87              84              83              8
16              3               8               102             99              91              9
17              5               7               86              81              74              7
18              2               7               62              60              53              6
19              9               9               50              41              32              4
20              5               8               41              36              28              4
```

# Preemptive

In the preemptive version of Priority Scheduling, the priority of a process can change while it is waiting in the ready queue. If a new process with a higher priority arrives, it preempts the currently executing process and starts executing. The process with the highest priority is always selected for execution, and the priorities are recalculated based on the remaining burst times of the processes.

## Code

```csharp
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
```

# Example run

```Performance Metrics:
Average Turnaround Time: 39.70
Average Waiting Time: 35.25
CPU Utilization: 100.00%
Throughput: 0.22 processes/unit time

ProcessID       ArrivalTime     BurstTime       CompletionTime  TurnaroundTime  WaitingTime     Priority
1               7               2               14              7               5               2
2               4               9               33              29              20              4
3               5               2               24              19              17              3
4               1               4               64              63              59              7
5               4               1               5               1               0               2
6               7               5               59              52              47              6
7               2               2               4               2               0               4
8               9               6               76              67              61              7
9               0               5               81              81              76              8
10              5               9               49              44              35              5
11              4               7               12              8               1               2
12              3               5               54              51              46              6
13              9               3               40              31              28              4
14              4               7               88              84              77              8
15              3               6               70              67              61              7
16              1               1               2               1               0               2
17              7               1               89              82              81              8
18              6               4               37              31              27              4
19              8               8               22              14              6               2
20              0               2               60              60              58              7
```

# Pros and Cons

## Pros

- Allows for the execution of high-priority processes first.
- Can be used to prioritize critical tasks over less important ones.
- Can improve system responsiveness by executing important tasks promptly.

## Cons

- May lead to starvation of low-priority processes if high-priority processes keep arriving.
- Requires a mechanism to handle priority changes dynamically.
- May not be suitable for real-time systems where strict deadlines need to be met.

