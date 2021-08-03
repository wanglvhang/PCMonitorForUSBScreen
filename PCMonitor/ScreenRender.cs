using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PCMonitor.Widgets;
using USBScreen;

namespace PCMonitor
{
    public class ScreenRender:IDisposable
    {

        public Bitmap BGImage { get; private set; }

        public IUSBScreen USBScreen { get; private set; }

        public IList<WidgetBase> Widges { get; private set; }

        public ThemeConfig Config { get; private set; }

        public IMonitorDataProvider MonitorDataProvider { get; private set; }

        public ScreenRender(Bitmap bgimg, IUSBScreen render, IMonitorDataProvider dataProvider, ThemeConfig themeConfig)
        {
            this.BGImage = bgimg;
            this.USBScreen = render;
            this.Widges = new List<WidgetBase>();
            this.Config = themeConfig;
            this.MonitorDataProvider = dataProvider;
        }


        public void Setup()
        {
            USBScreen.Connect();

            USBScreen.Startup();

            USBScreen.Restart();//reset screen

            USBScreen.AjustScreen(false, true, true);

            USBScreen.RenderBitmap(BGImage, 0, 0);

            this.build();
        }

        public async Task Refresh()
        {
            await Task.Run(new Action(() =>
            {

                //刷新数据然后逐个render
                foreach (var w in Widges)
                {
                    var data = getDataForWidget(w.WidgetType, w.DataType);
                    w.Render(this.USBScreen, copyWidgetBG(w), data);//渲染时包含数据的获取操作，可能导致数据的不同步 ！应该先获取全部所需数据，并对所有widges进行更新，然后再进行逐个渲染
                }

                if(USBScreen is VirtualScreen)
                {
                    var vs = USBScreen as VirtualScreen;
                    vs.SaveImage();
                }

                Thread.Sleep(50);

            }));

        }

        private DataForRender getDataForWidget(eWidgetType widgetType,eMonitorDataType dataType)
        {
            //若不是TextLabel 则直接返回原始数据
            if (widgetType != eWidgetType.TextLabel)
            {
                var num = this.MonitorDataProvider.GetData(dataType);
                return new DataForRender(num, null);
            }
            else//用于直接显示数据的预处理
            {
                float? raw_data = null;

                //个性化数据不从
                if (dataType != eMonitorDataType.Custom_GPU_RAM_UsedTotal)
                {
                    raw_data = this.MonitorDataProvider.GetData(dataType);

                    //若获取的数据为空，则直接返回 N/A
                    if (!raw_data.HasValue)
                    {
                        return new DataForRender(null, "N/A");
                    }

                }

                var result_str = "N/A";


                switch (dataType)
                {
                    case eMonitorDataType.CPU_Fan_Speed:
                        result_str = Math.Ceiling(raw_data.Value).ToString();
                        break;
                    case eMonitorDataType.CPU_Hz:
                        result_str = (raw_data.Value / 1000).ToString("f1");  //3.2GHz
                        break;
                    case eMonitorDataType.CPU_Load:
                        result_str = Math.Ceiling(raw_data.Value).ToString();
                        break;
                    case eMonitorDataType.CPU_Temp:
                        result_str = Math.Ceiling(raw_data.Value).ToString();
                        break;
                    case eMonitorDataType.GPU_Fan_Speed:
                        result_str = Math.Ceiling(raw_data.Value).ToString();
                        break;
                    case eMonitorDataType.GPU_Hz:
                        result_str = Math.Ceiling(raw_data.Value).ToString(); //取整 单位为MHz
                        //result_str = "2222";
                        break;
                    case eMonitorDataType.GPU_Load:
                        result_str = Math.Ceiling(raw_data.Value).ToString();
                        break;
                    case eMonitorDataType.GPU_RAM_Load:
                        result_str = Math.Ceiling(raw_data.Value).ToString();
                        break;
                    case eMonitorDataType.GPU_RAM_Total:
                        result_str = Math.Ceiling(raw_data.Value).ToString(); //单位为MB
                        break;
                    case eMonitorDataType.GPU_RAM_Used:
                        result_str = Math.Ceiling(raw_data.Value).ToString(); //单位为MB
                        break;
                    case eMonitorDataType.Custom_GPU_RAM_UsedTotal:
                        var total_gpu_ram = this.MonitorDataProvider.GetData( eMonitorDataType.GPU_RAM_Total);
                        var used_gpu_ram = this.MonitorDataProvider.GetData(eMonitorDataType.GPU_RAM_Used);
                        result_str = $"{Math.Ceiling(used_gpu_ram.Value)}/{total_gpu_ram}";
                        //200/1000
                        break;
                    case eMonitorDataType.GPU_Temp:
                        result_str = Math.Ceiling(raw_data.Value).ToString();
                        break;
                    case eMonitorDataType.Life_Days:
                        //获取birthday 配置
                        result_str = raw_data.Value.ToString("f0");
                        break;
                    case eMonitorDataType.Network_Download:
                        result_str = getNetworkSpeedStr(raw_data.Value);
                        break;
                    case eMonitorDataType.Network_Upload:
                        result_str = getNetworkSpeedStr(raw_data.Value);
                        break;
                    case eMonitorDataType.RAM_Free:
                        result_str = (raw_data.Value).ToString("f1");
                        break;
                    case eMonitorDataType.RAM_Load:
                        result_str = Math.Ceiling(raw_data.Value).ToString();
                        break;
                    case eMonitorDataType.RAM_Used:
                        result_str = (raw_data.Value).ToString("f1");
                        break;
                }

                return new DataForRender(null, result_str);
            }
        }




        private void build()
        {
            foreach (var wc in this.Config.Widgets)
            {
                var frontColor = wc.FrontColor.ToColor();
                var bgColor = wc.BackgroundColor.ToColor();
                var borderColor = wc.BorderColor.ToColor();

                if (wc.Type == eWidgetType.Oscillogram)
                {
                    this.Widges.Add(new Oscillogram(wc.Data,
                                new Rectangle(wc.X, wc.Y, wc.Width, wc.Height),
                                frontColor == null ? Color.Red : frontColor.Value,
                                wc.BackgroundColor.ToColor(),
                                wc.BorderWidth,
                                borderColor == null ? Color.Black : borderColor.Value)
                        );
                }
                else if (wc.Type == eWidgetType.TextLabel)
                {
                    var font = new Font(wc.TextFontFamily, wc.TextSize, wc.TextStyle, GraphicsUnit.Pixel);
                    //添加TextLabel
                    this.Widges.Add(new TextLabel(wc.Data,
                        new Rectangle(wc.X, wc.Y, wc.Width, wc.Height),
                        frontColor == null ? Color.Red : frontColor.Value,
                        wc.BackgroundColor.ToColor(),
                        font,
                        wc.TextAlignment,
                        wc.TextLineAlignment
                        ));
                }
                else if(wc.Type == eWidgetType.PercentBar)
                {
                    this.Widges.Add(new PercentBar(wc.Data,
                         new Rectangle(wc.X, wc.Y, wc.Width, wc.Height),
                         frontColor == null ? Color.Red : frontColor.Value,
                         wc.BackgroundColor.ToColor().Value));
                }

            }
        }

        private Bitmap copyWidgetBG(WidgetBase widget)
        {
            if (widget.Area.X + widget.Area.Width > USBScreen.ScreenWidth
                || widget.Area.Y + widget.Area.Height > USBScreen.ScreenHeight
                || widget.Area.Width > USBScreen.ScreenWidth
                || widget.Area.Height > USBScreen.ScreenHeight)
            {
                throw new Exception("widget渲染区域超出屏幕");
            }
            return this.BGImage.Clone(widget.Area, System.Drawing.Imaging.PixelFormat.Format16bppRgb565);
        }

        private string getNetworkSpeedStr(float bytesPerSec)
        {
            if (bytesPerSec >= 0 && bytesPerSec <= 1024)
            {
                return $"{Math.Ceiling(bytesPerSec)}B/s";
            }
            else if (bytesPerSec > 1024 && bytesPerSec <= 1048576)
            {
                return $"{Math.Ceiling(bytesPerSec / 1024)}KB/s";
            }
            else
            {
                return $"{Math.Ceiling(bytesPerSec / (1024 * 1024))}MB/s";
            }
        }

        public void Dispose()
        {
            this.USBScreen.Shutdown();
            this.USBScreen.Dispose();
        }
    }


    public enum eMonitorDataType
    {
        CPU_Load, //小数点后一位
        CPU_Temp, //小数点后三位
        CPU_Hz, //小数点后两位 MHz
        CPU_Fan_Speed, //小数点后三位

        GPU_Load,//
        GPU_Temp,//
        GPU_Hz, // 

        GPU_RAM_Total, //
        GPU_RAM_Used,
        GPU_RAM_Load,//

        //GPU_Fan_Load,//float 0.1
        GPU_Fan_Speed,

        //RAM_Total,
        RAM_Used,
        RAM_Free,
        RAM_Load,//float 0.1

        //IPAddress,

        Network_Upload,//B
        Network_Download,

        Life_Days,


        Custom_GPU_RAM_UsedTotal,//

    }

}
