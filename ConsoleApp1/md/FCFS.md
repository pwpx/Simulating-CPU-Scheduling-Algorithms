[Back to README](https://github.com/pwpx/Simulating-CPU-Scheduling-Algorithms/blob/351f850e70ed00775947d66e0f7b00d0487fe114/README.md)
# First-Come, First-Served (FCFS) Scheduling Algorithm
The First-Come, First-Served (FCFS) scheduling algorithm is the simplest CPU scheduling algorithm. In this algorithm, the process that arrives first is the one that gets executed first. The processes are executed in the order they arrive, without any preemption. This algorithm is non-preemptive, meaning that once a process starts executing, it continues until it completes its CPU burst.


## Code
```csharp
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
```
## Explanation
- The `FirstComeFirstServed` method takes a list of processes as input and sorts them based on their arrival times in ascending order.
- The `currentTime` variable is used to keep track of the current time during the execution of processes.
- The processes are then executed in the order they arrive, starting from the process with the earliest arrival time.
- If the current time is less than the arrival time of the next process, the current time is updated to the arrival time of the next process.
- The completion time, turnaround time, and waiting time for each process are calculated based on the current time and the burst time of the process.

## Example run
```Performance Metrics:
Performance Metrics:
Average Turnaround Time: 46.20
Average Waiting Time: 41.80
CPU Utilization: 100.00%
Throughput: 0.23 processes/unit time

ProcessID       ArrivalTime     BurstTime       CompletionTime  TurnaroundTime  WaitingTime     Priority
1               0               1               1               1               0               9
18              0               9               10              10              1               6
3               0               7               17              17              10              4
4               0               3               20              20              17              3
5               0               8               28              28              20              7
15              0               4               32              32              28              7
13              1               5               37              36              31              2
10              1               7               44              43              36              9
6               1               4               48              47              43              3
19              3               6               54              51              45              2
16              4               1               55              51              50              4
17              5               3               58              53              50              2
7               6               2               60              54              52              4
2               6               3               63              57              54              2
8               6               3               66              60              57              4
20              6               4               70              64              60              9
9               7               5               75              68              63              5
14              8               8               83              75              67              1
11              9               4               87              78              74              3
12              9               1               88              79              78              6

```

## Pros and Cons

### Pros
- Simple and easy to implement.
- Guarantees fairness as processes are executed in the order they arrive.
- No starvation as every process eventually gets executed.
- No overhead of context switching as it is a non-preemptive algorithm.

### Cons

- Inefficient for processes with long burst times, as it may lead to high average waiting times.
- Poor performance in terms of average turnaround time, especially when there are long-running processes.
- Not suitable for time-sharing systems or real-time applications due to its non-preemptive nature.
- Can lead to convoy effect, where short processes are delayed by long processes at the beginning of the queue.
