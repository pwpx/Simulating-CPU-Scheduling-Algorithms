[Back to README](../../README.md)
# Multilevel Queue Scheduling

Multilevel Queue Scheduling is a CPU scheduling algorithm that partitions the ready queue into multiple queues based on process characteristics such as priority, process type, or other criteria. Each queue can have its own scheduling algorithm, priority, and time quantum. Processes are assigned to different queues based on their attributes, and each queue is processed independently.

## Code

```csharp
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
```

# Example run

```Performance Metrics:
Average Turnaround Time: 54.50
Average Waiting Time: 49.25
CPU Utilization: 100.00%
Throughput: 0.19 processes/unit time

ProcessID       ArrivalTime     BurstTime       CompletionTime  TurnaroundTime  WaitingTime     Priority
1               5               2               68              63              61              9
2               5               6               31              26              20              1
3               1               8               91              90              82              8
4               0               7               58              58              51              8
5               6               4               75              69              65              9
6               7               2               41              34              32              1
7               3               7               18              15              8               1
8               7               2               85              78              76              7
9               0               6               87              87              81              7
10              6               5               39              33              28              2
11              2               9               104             102             93              7
12              6               9               105             99              90              6
13              4               4               22              18              14              4
14              4               3               25              21              18              3
15              0               2               55              55              53              7
16              5               3               34              29              26              2
17              5               3               71              66              63              7
18              6               8               103             97              89              8
19              8               8               49              41              33              4
20              2               7               11              9               2               2
```

## Pros and Cons

### Pros
- Allows for different scheduling algorithms to be used for different types of processes.
- Provides flexibility in managing processes with different priorities or characteristics.
- Can improve system performance by optimizing the scheduling of processes based on their attributes.

### Cons

- Complexity in managing multiple queues and scheduling algorithms.
- Requires careful design and tuning of parameters to achieve optimal performance.
- May lead to starvation or priority inversion if not implemented correctly.
```
