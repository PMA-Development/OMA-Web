using OMA_Web.API;

namespace OMA_Web.watchdog
{
    public class DeviceDataStatusChanged(Dictionary<int, List<DeviceData>> DeviceData)
    {
        public Dictionary<int, List<DeviceData>> DeviceData { get; } = DeviceData;
    }
}
