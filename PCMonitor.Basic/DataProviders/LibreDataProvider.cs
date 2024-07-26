using LibreHardwareMonitor.Hardware;
using PCMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace PCMonitor.Basic.DataProviders
{

    [DataProviderMeta(ProviderName = "LibreHardwareMonitor")]
    public class LibreDataProvider : IMonitorDataProvider
    {

        public NetworkInterface NetworkInterface { get; private set; }

        private UploadNetworkStatisticsSnapshot uploadSnapshot;

        private DownloadNetworkStatisticsSnapshot downloadSnapshot;

        private int cpu_fan_index;

        private int mainboard_fan_index;

        private string network_interface_name;


        private DateTime start_date;


        public Computer Computer { get; private set; }

        public IHardware CPU { get; private set; }
        public IHardware GPU { get; private set; }
        public IHardware RAM { get; private set; }
        public IHardware Mainboard { get; private set; } //IsMotherboardEnabled 
        public IHardware SuperIO { get; private set; } //IsControllerEnabled 

        //IsNetworkEnabled 

        //IsStorageEnabled 

        public LibreDataProvider()
        {

        }

        public DataForRender GetData(eMonitorDataType dataType)
        {
            return new DataForRender(getData(dataType), "");

            switch (dataType)
            {
                case eMonitorDataType.CPU_Fan_Speed:
                    break;
                case eMonitorDataType.CPU_Hz:
                    break;
                case eMonitorDataType.CPU_Load:
                    break;
                case eMonitorDataType.CPU_Temp:
                    break;

                case eMonitorDataType.GPU_Fan_Speed:
                    break;
                case eMonitorDataType.GPU_Hz:
                    break;
                case eMonitorDataType.GPU_Load:
                    break;

                case eMonitorDataType.GPU_RAM_Load:
                    break;
                case eMonitorDataType.GPU_RAM_Used:
                    break;
                case eMonitorDataType.GPU_RAM_Total:
                    break;

                case eMonitorDataType.RAM_Free:
                    break;
                case eMonitorDataType.RAM_Load:
                    break;
                case eMonitorDataType.RAM_Used:
                    break;

                case eMonitorDataType.Mainboard_Fan:
                    break;

                case eMonitorDataType.Network_Download:
                    break;
                case eMonitorDataType.Network_Upload:
                    break;

                case eMonitorDataType.Total_Days:
                    break;

            }
        }

        public List<string> GetFans()
        {
            if (this.SuperIO is null) return new List<string>();

            return this.SuperIO.Sensors.Where(s => s.SensorType == SensorType.Fan)
                .Select(s => s.Name).ToList();
        }

        public void Initialize(AppConfig config)
        {

            this.cpu_fan_index = config.CPUFanIndex;
            this.mainboard_fan_index = config.MainboardFanIndex;
            this.network_interface_name = config.NetworkInterface;
            this.start_date = Convert.ToDateTime(config.StartDate);

            if (!string.IsNullOrWhiteSpace(this.network_interface_name))
            {
                var network_interfaces = NetworkInterface.GetAllNetworkInterfaces();

                this.NetworkInterface = network_interfaces.Where(i => i.Name == this.network_interface_name).FirstOrDefault();
            }


            this.Computer = new Computer()
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsControllerEnabled = true,
                IsNetworkEnabled = true,
                IsStorageEnabled = true
            };

            this.Computer.Open();

            this.Computer.Accept(new UpdateVisitor());

            this.CPU = this.Computer.Hardware.Where(x => x.HardwareType == HardwareType.Cpu).First();
            this.GPU = this.Computer.Hardware.Where(x => x.HardwareType == HardwareType.GpuAmd).First();
            this.RAM = this.Computer.Hardware.Where(x => x.HardwareType == HardwareType.Memory).First();
            this.Mainboard = this.Computer.Hardware.Where(x => x.HardwareType == HardwareType.Motherboard).First();
            this.SuperIO = this.Mainboard.SubHardware.Where(x => x.HardwareType == HardwareType.SuperIO).FirstOrDefault();
        }




        private float? getData(eMonitorDataType dataType)
        {
            //上传下载速度
            if (dataType == eMonitorDataType.Network_Upload)
            {
                return getNetworkUploadSpeed();
            }
            else if (dataType == eMonitorDataType.Network_Download)
            {
                return getNetworkDownloadSpeed();
            }
            else if (dataType == eMonitorDataType.Total_Days)
            {
                return getTotalDays();
            }
            else
            {
                var sensor = getSensor(dataType);
                if (sensor is null)
                {
                    return 0;
                }
                return sensor.Value;
            }

        }

        private ISensor getSensor(eMonitorDataType dataType)
        {
            ISensor sensor = null;
            switch (dataType)
            {
                case eMonitorDataType.CPU_Load:
                    this.CPU.Update();//
                    sensor = this.CPU.Sensors.Where(s => s.SensorType == SensorType.Load && s.Name.Contains("Total")).FirstOrDefault();
                    break;
                case eMonitorDataType.CPU_Temp:
                    this.CPU.Update();//
                    sensor = this.CPU.Sensors.Where(s => s.SensorType == SensorType.Temperature).FirstOrDefault();
                    break;
                case eMonitorDataType.CPU_Hz:
                    this.CPU.Update();//
                    sensor = this.CPU.Sensors.Where(s => s.SensorType == SensorType.Clock)
                        .OrderByDescending(s => s.Value).FirstOrDefault();
                    break;
                case eMonitorDataType.CPU_Fan_Speed:
                    //TODO 确定获取cpu的风扇
                    if (this.SuperIO == null) return null;
                    this.SuperIO.Update();
                    //var ss = this.SuperIO.Sensors.Where(s => s.SensorType == SensorType.Fan).ToArray();
                    sensor = this.SuperIO.Sensors.Where(s => s.SensorType == SensorType.Fan).ToArray()[this.cpu_fan_index];
                    //this.CPU.Update();
                    //sensor = this.CPU.Sensors.Where(s => s.SensorType == SensorType.Fan).FirstOrDefault();
                    break;
                case eMonitorDataType.GPU_Load:
                    this.GPU.Update();//
                    sensor = this.GPU.Sensors.Where(s => s.SensorType == SensorType.Load && s.Name.Contains("GPU Core")).FirstOrDefault();
                    break;
                case eMonitorDataType.GPU_Fan_Speed:
                    this.GPU.Update();//
                    sensor = this.GPU.Sensors.Where(s => s.SensorType == SensorType.Fan).FirstOrDefault();
                    break;
                case eMonitorDataType.GPU_Hz:
                    this.GPU.Update();//
                    sensor = this.GPU.Sensors.Where(s => s.SensorType == SensorType.Clock && s.Name.Contains("GPU Core")).FirstOrDefault();
                    break;
                case eMonitorDataType.GPU_Temp:
                    this.GPU.Update();//
                    sensor = this.GPU.Sensors.Where(s => s.SensorType == SensorType.Temperature).FirstOrDefault();
                    break;
                case eMonitorDataType.GPU_RAM_Used:
                    this.GPU.Update();//
                    sensor = this.GPU.Sensors.Where(s => s.SensorType == SensorType.SmallData && s.Name.Contains("GPU Memory Used")).FirstOrDefault();
                    if (sensor is null)
                    {
                        //针对AMD 显卡的情况
                        sensor = this.GPU.Sensors.Where(s => s.SensorType == SensorType.Load && s.Name.Contains("GPU Memory")).FirstOrDefault();
                    }

                    break;
                case eMonitorDataType.GPU_RAM_Total:
                    //不需更新
                    sensor = this.GPU.Sensors.Where(s => s.SensorType == SensorType.SmallData && s.Name.Contains("GPU Memory Total")).FirstOrDefault();
                    break;
                case eMonitorDataType.RAM_Load:
                    this.RAM.Update();//
                    sensor = this.RAM.Sensors.Where(s => s.SensorType == SensorType.Load).FirstOrDefault();
                    break;
                case eMonitorDataType.RAM_Free:
                    this.RAM.Update();//
                    sensor = this.RAM.Sensors.Where(s => s.SensorType == SensorType.Data && s.Name.Contains("Available Memory")).FirstOrDefault();
                    break;
                case eMonitorDataType.RAM_Used:
                    this.RAM.Update();//
                    sensor = this.RAM.Sensors.Where(s => s.SensorType == SensorType.Data && s.Name.Contains("Used Memory")).FirstOrDefault();
                    break;
                default:
                    throw new Exception("the data type is not supported by Monitor Data Provider");

            }

            return sensor;
        }

        private float? getNetworkUploadSpeed()
        {
            //初始化网络统计快照
            if (this.uploadSnapshot == null)
            {
                this.uploadSnapshot = new UploadNetworkStatisticsSnapshot()
                {
                    BytesSent = this.NetworkInterface.GetIPStatistics().BytesSent,
                    SentSnapshotTime = DateTime.Now,
                };
                return null;
            }
            else
            {

                var bytes_sent = this.NetworkInterface.GetIPStatistics().BytesSent - this.uploadSnapshot.BytesSent;
                var millisecond = (DateTime.Now - this.uploadSnapshot.SentSnapshotTime).TotalMilliseconds;

                var speed = Convert.ToSingle((bytes_sent / millisecond) * 1000f);

                this.uploadSnapshot.BytesSent = this.NetworkInterface.GetIPStatistics().BytesSent;
                this.uploadSnapshot.SentSnapshotTime = DateTime.Now;

                return speed;
            }
        }

        private float? getNetworkDownloadSpeed()
        {
            if (this.downloadSnapshot == null)
            {
                this.downloadSnapshot = new DownloadNetworkStatisticsSnapshot()
                {
                    BytesReceived = this.NetworkInterface.GetIPStatistics().BytesReceived,
                    ReceivedSnapshotTime = DateTime.Now,
                };
                return null;
            }
            else
            {
                var bytes_download = this.NetworkInterface.GetIPStatistics().BytesReceived - this.downloadSnapshot.BytesReceived;
                var millisecond = (DateTime.Now - this.downloadSnapshot.ReceivedSnapshotTime).TotalMilliseconds;

                var speed = Convert.ToSingle((bytes_download / millisecond) * 1000f);

                this.downloadSnapshot.BytesReceived = this.NetworkInterface.GetIPStatistics().BytesReceived;
                this.downloadSnapshot.ReceivedSnapshotTime = DateTime.Now;


                return speed;
            }
        }

        private float? getTotalDays()
        {
            try
            {
                return Convert.ToSingle((DateTime.Now - start_date).TotalDays);
            }
            catch (Exception e)
            {
                return null;
            }

        }

    }




    //https://github.com/LibreHardwareMonitor/LibreHardwareMonitor


    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }
        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
        }
        public void VisitSensor(ISensor sensor) { }
        public void VisitParameter(IParameter parameter) { }
    }

    public class UploadNetworkStatisticsSnapshot
    {
        public DateTime SentSnapshotTime { get; set; }
        public long BytesSent { get; set; }
    }

    public class DownloadNetworkStatisticsSnapshot
    {
        public DateTime ReceivedSnapshotTime { get; set; }
        public long BytesReceived { get; set; }
    }

}
