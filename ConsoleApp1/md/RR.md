[Back to README](https://github.com/pwpx/Simulating-CPU-Scheduling-Algorithms/blob/351f850e70ed00775947d66e0f7b00d0487fe114/README.md)
# Round Robin Scheduling

The Round Robin (RR) scheduling algorithm is a preemptive CPU scheduling algorithm that assigns a fixed time unit per process, known as a time quantum or time slice. The processes are executed in a circular queue, with each process getting a turn to execute for a fixed time quantum. If a process does not complete its execution within the time quantum, it is moved to the end of the queue, and the next process in line gets a turn to execute.

## Code
```csharp
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
```

## Explanation
- The `RoundRobin` method takes a list of processes and a time quantum as input.
- The processes are sorted based on their arrival times in ascending order.
- A `Queue` data structure is used to maintain the ready queue of processes.
- The `currentTime` variable is used to keep track of the current time during the execution of processes.
- The `index` variable is used to iterate over the list of processes.
- The `completed` variable keeps track of the number of processes that have completed their execution.
- The processes are added to the ready queue based on their arrival times.
- Each process is executed for a fixed time quantum, and if it does not complete its execution within the time quantum, it is moved to the end of the queue.

## Example run

```Performance Metrics:
Average Turnaround Time: 44.70
Average Waiting Time: 40.55
CPU Utilization: 100.00%
Throughput: 0.24 processes/unit time

ProcessID       ArrivalTime     BurstTime       CompletionTime  TurnaroundTime  WaitingTime     Priority
1               2               3               23              21              18              7
2               2               7               67              65              58              7
3               7               6               77              70              64              4
4               8               6               79              71              65              3
5               4               2               33              29              27              6
6               6               5               74              68              63              8
7               8               2               59              51              49              1
8               0               8               16              16              8               6
9               2               9               83              81              72              1
10              0               4               8               8               4               9
11              6               5               75              69              64              9
12              5               1               40              35              34              2
13              9               7               82              73              66              7
14              4               2               35              31              29              3
15              4               6               73              69              63              4
16              8               1               60              52              51              8
17              1               4               20              19              15              6
18              0               3               11              11              8               6
19              0               1               12              12              11              5
20              6               1               49              43              42              8
```

## Pros and Cons

### Pros

- Simple and easy to implement.
- Ensures fairness by giving each process a turn to execute.
- Prevents starvation as processes are executed in a circular queue.

### Cons

- May lead to high waiting times for processes with longer burst times.
- The choice of time quantum can affect the performance of the algorithm.
- Context switching overhead can impact the overall performance of the system.
