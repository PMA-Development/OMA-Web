using OMA_Web.API;

namespace OMA_Web.watchdog
{
    public class DeviceDataWatchdog(OMAClient client) : IDisposable
    {
        private readonly OMAClient _client = client;

        public bool IsRunning => timer != null;

        public event EventHandler<DeviceDataStatusChanged>? DeviceDataStatusChanged;

        private PeriodicTimer? timer;

        public async Task StartWatchdog()
        {
            timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
            do
            {
                var data = await _client.GetTurbinesLatestDatasAsync();
                DeviceDataStatusChanged?.Invoke(this, new DeviceDataStatusChanged(data));

            } while (await timer.WaitForNextTickAsync());
        }

        public void StopWatchdog()
        {
            timer?.Dispose();
            timer = null;
        }

        public void Dispose()
        {
            timer?.Dispose();
            timer = null;
        }
    }
}
