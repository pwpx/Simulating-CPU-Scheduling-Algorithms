# Simulating CPU Scheduling Algorithms

## Content:

### [First-Come, First-Served (FCFS)](https://github.com/pwpx/Simulating-CPU-Scheduling-Algorithms/blob/78226c0061e53be58a41ac6655e0be0a9b833abe/ConsoleApp1/md/FCFS.md)
### [Shortest Job First (SJF)](https://github.com/pwpx/Simulating-CPU-Scheduling-Algorithms/blob/78226c0061e53be58a41ac6655e0be0a9b833abe/ConsoleApp1/md/SJF.md)
### [Round Robin (RR)](https://github.com/pwpx/Simulating-CPU-Scheduling-Algorithms/blob/78226c0061e53be58a41ac6655e0be0a9b833abe/ConsoleApp1/md/RR.md)
### [Priority Scheduling](https://github.com/pwpx/Simulating-CPU-Scheduling-Algorithms/blob/78226c0061e53be58a41ac6655e0be0a9b833abe/ConsoleApp1/md/PriorityScheduling.md)
### [Multilevel Queue Scheduling](https://github.com/pwpx/Simulating-CPU-Scheduling-Algorithms/blob/78226c0061e53be58a41ac6655e0be0a9b833abe/ConsoleApp1/md/MQScheduling.md)

# Summary of the Assignment:

## Summary of CPU Scheduling Algorithms

#### Algorithms Implemented
1. **First-Come, First-Served (FCFS)**
2. **Shortest Job First (SJF)**
   - Non-Preemptive
   - Preemptive
3. **Round Robin (RR)**
4. **Priority Scheduling**
   - Non-Preemptive
   - Preemptive
5. **Multilevel Queue Scheduling**

#### Performance Metrics
For each algorithm, we measured:
- **Average Turnaround Time**: The average time taken from process arrival to process completion.
- **Average Waiting Time**: The average time a process spends waiting in the ready queue before being executed.
- **CPU Utilization**: The percentage of time the CPU is actively working (not idle).
- **Throughput**: The number of processes completed in a given time period.

#### Results and Analysis
1. **First-Come, First-Served (FCFS)**
   - Simple to implement.
   - Can lead to the "convoy effect" where short processes wait for long processes to complete.
   - Average Turnaround Time and Waiting Time can be high.

2. **Shortest Job First (SJF)**
   - **Non-Preemptive**: Minimizes average waiting time and turnaround time but can lead to starvation of longer processes.
   - **Preemptive (Shortest Remaining Time First)**: Further reduces waiting time and turnaround time but requires more complex implementation.

3. **Round Robin (RR)**
   - Fairly distributes CPU time among processes.
   - Suitable for time-sharing systems.
   - Performance depends on the time quantum; too small can lead to high context switching overhead, too large can degrade to FCFS.

4. **Priority Scheduling**
   - **Non-Preemptive**: Executes high-priority processes first but can lead to starvation of low-priority processes.
   - **Preemptive**: More responsive to high-priority processes but requires dynamic priority handling.

5. **Multilevel Queue Scheduling**
   - Combines multiple scheduling algorithms.
   - Suitable for systems with different types of processes.
   - Complex to implement and tune.

### Best Algorithm
**Shortest Job First (Preemptive)** (Shortest Remaining Time First) generally performed the best in terms of minimizing average turnaround time and waiting time. However, it requires accurate prediction of burst times and can lead to starvation of longer processes.

**Round Robin** is a good choice for time-sharing systems due to its fairness and simplicity, though its performance is highly dependent on the chosen time quantum.

**Priority Scheduling (Preemptive)** is effective for systems where certain tasks must be prioritized, but it requires careful handling to avoid starvation.

### Conclusion
The choice of the best scheduling algorithm depends on the specific requirements and constraints of the system. For minimizing turnaround and waiting times, **Shortest Job First (Preemptive)** is ideal. For fairness and responsiveness in time-sharing systems, **Round Robin** is recommended. For prioritizing critical tasks, **Priority Scheduling (Preemptive)** is suitable. Each algorithm has its trade-offs, and the best choice should align with the system's goals and workload characteristics.
