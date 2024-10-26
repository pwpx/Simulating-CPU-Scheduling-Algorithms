[Back to README](../../README.md)
# Assignment: Simulating CPU Scheduling Algorithms

## Objective
The purpose of this assignment is to deepen your understanding of CPU scheduling by implementing and simulating five different scheduling algorithms. You will simulate process generation, apply scheduling algorithms, and measure performance using both preemptive and non-preemptive scheduling techniques.

## Instructions
You are required to implement five CPU scheduling algorithms in the programming language of your choice. The simulation will involve the generation of **20 processes** with random arrival times and burst times. Each scheduling algorithm will be tested in both **preemptive and non-preemptive** modes (where applicable).

### Algorithms to Implement
1. **First-Come, First-Served (FCFS)**
2. **Shortest Job First (SJF)** (with both preemptive and non-preemptive versions)
3. **Round Robin (RR)** (preemptive by design)
4. **Priority Scheduling** (with both preemptive and non-preemptive versions)
5. **Multilevel Queue Scheduling** (as an extension of your implementation)

### Process Generation
- Simulate the generation of **20 processes**.
- Each process should have:
    - **Random arrival time** (within a certain realistic range).
    - **Random burst time** (representing the execution time required by the process).
- Processes should be generated before scheduling begins, and their arrival and burst times should be realistic.

### Single Processor Simulation
- Assume a **single CPU core**.
- The CPU can handle **one process at a time**, following the rules of the scheduling algorithm applied.

### Performance Measurement
For each scheduling algorithm, measure and report the following metrics:
- **Average Turnaround Time**: The average time taken from process arrival to process completion.
- **Average Waiting Time**: The average time a process spends waiting in the ready queue before being executed.
- **CPU Utilization**: The percentage of time the CPU is actively working (not idle).
- **Throughput**: The number of processes completed in a given time period.

### Preemptive vs. Non-Preemptive Scheduling
- For algorithms where both preemptive and non-preemptive versions exist (e.g., SJF, Priority Scheduling), **simulate both versions and compare performance**.
- For Round Robin, assume a **predefined time quantum** for the preemptive mode.

## Simulation Example
**Example**: Generate 20 processes with the following attributes:
- **Process 1**: Arrival Time = 0 ms, Burst Time = 5 ms
- **Process 2**: Arrival Time = 2 ms, Burst Time = 3 ms
- [...]

Apply the **FCFS algorithm** to determine the sequence of execution and calculate the turnaround and waiting times for each process.

## Deliverable
Submit the following:
1. **Source code** for all five scheduling algorithms.
2. **Report** that includes:
    - Detailed explanation of each scheduling algorithm.
    - Simulation results with performance metrics for each algorithm (including comparisons between preemptive and non-preemptive versions).
    - **Graphs or tables** that represent performance metrics such as turnaround time, waiting time, and CPU utilization.
    - **Optional**: A brief reflection on which scheduling algorithm performed best in terms of the metrics above and under what conditions.


## Assessment Criteria
- **Correctness**: Implementation of each scheduling algorithm should correctly simulate process scheduling according to the specified rules.
- **Performance Measurement**: Accurately measure and compare performance metrics (turnaround time, waiting time, CPU utilization, throughput) for each algorithm.
- **Clarity of Code**: Code should be well-documented, modular, and easy to understand.
- **Report Quality**: The report should clearly present the methodology, simulation results, and comparative analysis of preemptive and non-preemptive scheduling algorithms.

