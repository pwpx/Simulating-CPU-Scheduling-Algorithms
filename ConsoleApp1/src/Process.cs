// Process.cs
public class Process
{
    public int ProcessId { get; set; }
    public int ArrivalTime { get; set; }
    public int BurstTime { get; set; }
    public int Priority { get; set; }
    public int StartTime { get; set; }
    public int CompletionTime { get; set; }
    public int TurnaroundTime { get; set; }
    public int WaitingTime { get; set; }
    public int RemainingTime { get; set; }

    public Process(int processId, int arrivalTime, int burstTime, int priority)
    {
        ProcessId = processId;
        ArrivalTime = arrivalTime;
        BurstTime = burstTime;
        Priority = priority;
        RemainingTime = burstTime;
    }
}