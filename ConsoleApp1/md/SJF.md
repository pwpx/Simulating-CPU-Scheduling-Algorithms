[Back to README](https://github.com/pwpx/Simulating-CPU-Scheduling-Algorithms/blob/351f850e70ed00775947d66e0f7b00d0487fe114/README.md)
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

## Example run (non-preemptive)

```Performance Metrics:
Average Turnaround Time: 42.90
Average Waiting Time: 37.05
CPU Utilization: 100.00%
Throughput: 0.17 processes/unit time

ProcessID       ArrivalTime     BurstTime       CompletionTime  TurnaroundTime  WaitingTime     Priority
1               0               8               57              57              49              5
2               3               7               42              39              32              9
3               8               2               12              4               2               7
4               4               1               7               3               2               8
5               5               8               65              60              52              9
6               5               9               99              94              85              3
7               0               5               5               5               0               4
8               5               8               73              68              60              1
9               1               3               10              9               6               8
10              5               6               35              30              24              9
11              1               9               90              89              80              1
12              5               9               108             103             94              4
13              6               3               15              9               6               8
14              7               9               117             110             101             9
15              5               7               49              44              37              1
16              2               1               6               4               3               4
17              7               8               81              74              66              6
18              6               5               24              18              13              4
19              7               5               29              22              17              2
20              3               4               19              16              12              9
```

## Example run (preemptive)

```Performance Metrics:
Average Turnaround Time: 54.30
Average Waiting Time: 47.80
CPU Utilization: 100.00%
Throughput: 0.15 processes/unit time

ProcessID       ArrivalTime     BurstTime       CompletionTime  TurnaroundTime  WaitingTime     Priority
1               2               6               21              19              13              3
2               4               6               39              35              29              3
3               5               7               79              74              67              7
4               9               8               94              85              77              3
5               0               9               103             103             94              5
6               3               9               112             109             100             7
7               3               6               33              30              24              9
8               6               7               86              80              73              1
9               8               6               45              37              31              3
10              3               2               6               3               1               1
11              2               7               65              63              56              8
12              4               7               72              68              61              6
13              5               5               11              6               1               6
14              6               9               121             115             106             5
15              0               7               58              58              51              7
16              2               6               27              25              19              7
17              8               6               51              43              37              3
18              7               4               15              8               4               9
19              0               4               4               4               0               6
20              9               9               130             121             112             4
```

## Explanation

- By looking at the output of preemptive and non-preemptive SJF, we can see that the preemptive version has a higher average turnaround time and waiting time compared to the non-preemptive version.
- This is because the preemptive version may interrupt processes and switch between them, leading to additional overhead and longer execution times.

## Pros and Cons

### Pros

- Non-preemptive SJF minimizes the average waiting time and turnaround time by executing shorter processes first.
- Preemptive SJF allows for better CPU utilization and responsiveness by switching between processes based on their remaining time.
- Both versions of SJF are simple and easy to implement.

### Cons

- Non-preemptive SJF may lead to longer waiting times for processes with longer burst times.
- Preemptive SJF can introduce overhead due to context switching and managing the execution of multiple processes.
- Both versions of SJF may suffer from starvation if shorter processes keep arriving, preventing longer processes from executing.
