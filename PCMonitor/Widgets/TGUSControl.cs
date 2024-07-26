using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor.Widgets
{
    /// <summary>
    /// 暂时废弃，后续若有屏幕设备需要纯数据的渲染方式，再启用
    /// </summary>
    internal class TGUSControl : WidgetBase
    {

        private string _address;
        private int _strLength;
        private StringAlignment _strAligment;

        public TGUSControl(eMonitorDataType dataType, string address,int strLength, StringAlignment strAligment)
        {
            this.DataType = dataType;
            this._address = address;
            this._strLength = strLength;
            this._strAligment = strAligment;
        }

        public override void Render(IScreen screen, Bitmap widget_canvas, DataForRender data)
        {
            //数据类型  整数/浮点数/

            //直接调用screen的send cmd 发送指令更新空间数据

            //控制寄存器地址  1个字节
            //数据寄存器地址  2个字节

            // 5A A5 帧头
            // 一个字节长度0~256  00~FF   指令、数据和校验
            //一个字节指令  80 写控制寄存  81 读控制寄存  82 写数据寄存  83 读数据寄存
            //起始地址 address 
            //
            var fixed_bytes = new byte[6]; //固定长度 即不包含数据的部分

            fixed_bytes[0] = 0x5A;
            fixed_bytes[1] = 0xA5;
            //fixed_bytes[2]
            fixed_bytes[3] = 0x82; //指令


            var adr_bytes = hexStrToBytes(this._address); //地址固定为两字节
            fixed_bytes[4] = adr_bytes[0];
            fixed_bytes[5] = adr_bytes[1];


            //写入字符串
            var data_bytes = Encoding.ASCII.GetBytes(data.Str).ToList();


            //补齐空格 0x20 空格的ascii码
            if (data_bytes.Count() > this._strLength)
            {
                data_bytes = data_bytes.Take(this._strLength).ToList();
                //data_bytes = data_bytes[0..3];
            }
            else if (data_bytes.Count() < this._strLength)
            {

                while (data_bytes.Count() < this._strLength)
                {
                    if (this._strAligment == StringAlignment.Near) //左对其则 补齐右侧空格
                    {
                        data_bytes.Add(0x20); 
                    }
                    else //非左对其则补齐 左侧空格
                    {
                        //data_bytes.Prepend<byte>(0x20);
                        data_bytes.Insert(0, 0x20);
                    }
                }

            }

            //拼接
            var result_bytes = fixed_bytes.Concat(data_bytes).ToArray();
            //最后设置长度
            result_bytes[2] = (byte)(result_bytes.Length - 3); //长度


            screen.SendRaw(result_bytes);

        }

        public byte[] hexStrToBytes(string hexStr)
        {
            hexStr = hexStr.Trim();

            if((hexStr.Length % 2) != 0)
            {
                throw new ArgumentException("value is not wanted hexstring");
            }

            byte[] bytes = new byte[hexStr.Length/2];

            for(int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexStr.Substring(i * 2, 2), 16);
            }

            return bytes;
            
        }

        public override void Reset()
        {
            
        }
    }
}
