namespace DeviceMonitoring.Models
{
    public class Device
    {
        public Guid Id { get; set; } /*= new Guid();*/
        public string Name { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Version { get; set; } = string.Empty;
    }
}
