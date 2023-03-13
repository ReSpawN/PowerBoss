namespace PowerBoss.Domain.Solar.Enums;

public enum OperatingState : ushort
{
    Off = 1, // Off
    Sleeping = 2, // Sleeping (auto-shutdown) – Night mode
    Starting = 3, // Grid Monitoring/wake-up
    Producing = 4, // Inverter is ON and producing power
    Throttled = 5, // Production (curtailed)
    ShuttingDown = 6, // Shutting down
    Fault = 7, // Fault
    Standby = 8 // Maintenance/setup
}