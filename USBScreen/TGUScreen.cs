using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace USBScreen
{

    //支持冠显串口屏指令集 1.4的屏幕
    public class TGUScreen : IUSBScreen
    {
        public int ScreenWidth { get; private set; } = 480;

        public int ScreenHeight { get; private set; } = 480;

        public eScreenStatus Status { get; private set; }

        public string COMName { get; private set; }


        private int baudRate;


        private static TGUScreen instance;

        public static TGUScreen GetInstance(int width, int height, string comName, int baudRate)
        {
            //TODO 检查参数 默认baudrate为 115200
            //baudrate检查 RS232最大传输速率只能是115200
            //1200	2400	4800	9600	19200	38400	57600	115200


            if (instance == null) instance = new TGUScreen(width, height, comName, baudRate);
            return instance;
        }


        private TGUScreen(int width, int height, string comName, int baudRate)
        {
            this.ScreenWidth = width;
            this.ScreenHeight = height;
            this.COMName = comName;
            this.baudRate = baudRate;

        }


        public SerialPort SerialPort { get; private set; }

        public void Connect()
        {
            try
            {
                if (this.SerialPort == null)
                {
                    this.COMName = this.COMName;
                    this.SerialPort = new SerialPort(this.COMName)
                    {
                        DtrEnable = true,
                        RtsEnable = true,
                        ReadTimeout = 1000,
                        BaudRate = baudRate,
                        DataBits = 8,
                        StopBits = StopBits.One,
                        Parity = Parity.None
                    };

                    this.Status = eScreenStatus.Connected;

                    //}
                    this.SerialPort.Open();

                    //测试代码
                    //this.sendCMD(0x00);

                    //var result = this.readFromSerialPort();
                    //AA 00 ‘OK_V*.*’ P1 P2  Pic_ID CC 33 C3 3C

                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                this.Status = eScreenStatus.Error;
            }
        }

        public void Dispose()
        {
            if (this.SerialPort != null)
            {
                this.SerialPort.Close();
                this.SerialPort.Dispose();
            }
            TGUScreen.instance = null;
        }

        public void RenderBitmap(Bitmap img, int posX, int posY)
        {
            return;
        }

        public void RenderPixels(int offsetX, int offsetY, Color pixelColor, byte[] coordinates)
        {
            throw new NotImplementedException();
        }

        public void RenderPixels(Color pixelColor, IEnumerable<Point> points)
        {
            throw new NotImplementedException();
        }

        public void Restart()
        {
            throw new NotImplementedException();
        }

        public void SetBrightness(int brightness)
        {
            throw new NotImplementedException();
        }

        public void SetLandscapeDisplay(bool isInvert)
        {
            throw new NotImplementedException();
        }

        public void SetMirror(bool isMirror)
        {
            throw new NotImplementedException();
        }

        public void SetVerticalDisplay(bool isInvert)
        {
            throw new NotImplementedException();
        }

        public void Shutdown()
        {
            //TGUS屏幕上电即开机，也未找到开/关机指令
            return;
        }

        public void Startup()
        {
            //TGUS屏幕上电即开机，也未找到开/关机指令
            return;
        }

        public void SendCMD(byte[] data)
        {
            throw new NotImplementedException();
        }



        private void sendCMD(byte cmd_code, byte[] content = null, int delay = 10)
        {
            if (content is null)
            {
                content = new byte[0];
            }

            //content最长 248
            if (content.Length > 248)
            {
                throw new ArgumentException("参数太长 >248，串口设备固件不支持");
            }

            byte frame_head = 0xAA;

            byte command = cmd_code;

            byte[] frame_end = new byte[4] { 0xCC, 0x33, 0xC3, 0x3C };


            List<byte> bytes_list = new List<byte>();

            bytes_list.Add(frame_head);
            bytes_list.Add(command);
            bytes_list.AddRange(content);
            bytes_list.AddRange(frame_end);

            var bytes = bytes_list.ToArray();


            this.writeToSerialPort(bytes, delay);

        }


        private void writeToSerialPort(byte[] bytes,int delay_for_returen = 10)
        {
            if (string.IsNullOrWhiteSpace(this.COMName)) { throw new Exception("尚未连接或未找到设备。"); }

            //发送数据
            if (!this.SerialPort.IsOpen) this.SerialPort.Open();

            this.SerialPort.Write(bytes, 0, bytes.Length);

            Thread.Sleep(delay_for_returen);

        }

        private List<Byte> readFromSerialPort()
        {
            //读取返回
            List<Byte> readBytes = new List<byte>();

            //var bytes_count = this.SerialPort.BytesToRead;

            //var return_bytes = new byte[bytes_count];

            //this.SerialPort.Read(return_bytes, 0, bytes_count);


            while (true)
            {
                var rb = this.SerialPort.ReadByte();
                if (rb > 0)
                {
                    readBytes.Add((byte)rb);
                }
                else
                {
                    break;
                }
            }

            return readBytes;
        }


    }
}
