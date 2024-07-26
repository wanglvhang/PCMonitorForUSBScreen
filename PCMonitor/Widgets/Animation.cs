using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor.Widgets
{
    internal class Animation : WidgetBase
    {

        private DateTime? previousRenderTime; //以调用screen绘制方法时间为准

        private AnimationStage[] animationStages;
        //private Dictionary<string, Image> gifs;

        private Dictionary<string, byte[]> resources;

        private Dictionary<string, GifFramesForRender> frames;

        private string currentGif;
        private int currentFrameIndex;


        public Animation(eMonitorDataType dataType, Rectangle area, AnimationStage[] animationStages, Dictionary<string, byte[]> resources)
        {
            this.DataType = dataType;
            this.Area = area;

            this.animationStages = animationStages;
            this.resources = resources;

            this.frames = new Dictionary<string, GifFramesForRender>();

        }


        public override void Render(IScreen screen, Bitmap widget_canvas, DataForRender data)
        {

            //获取数据

            //获取数据对应的gif
            var sw0 = new Stopwatch();
            sw0.Start();
            var stage = getStageByValue(data);

            var gffr = getGFFR(stage.GifName);

            var frame = gffr.GetNextFrame();

            //获取与之间帧的却别
            if(this.PrevFrame == null)
            {
                //直接渲染当前帧
                screen.RenderBitmap(frame, Area.X, Area.Y);
            }
            else
            {
                //获取与 previous frame 的差异像素
                var changed_pixels = frame.DiffFrom(this.PrevFrame);

                //获取颜色相同的像素

                Dictionary<Color,List<Point>> changed_colors = new Dictionary<Color, List<Point>>();

                foreach (var p in changed_pixels)
                {
                    if (!changed_colors.ContainsKey(p.Color))
                    {
                        changed_colors.Add(p.Color, new List<Point>());
                    }

                    changed_colors[p.Color].Add(p.Point);

                }

                foreach(var kvp in changed_colors)
                {
                    var sw1 = new Stopwatch();
                    sw1.Start();
                    screen.RenderPixels(kvp.Key,kvp.Value);
                    sw1.Stop();
                    Debug.WriteLine($"render pixels cost {sw1.ElapsedMilliseconds}ms");
                }


            }


            

            this.PrevFrame = frame;

            //分析 当前帧对比之前帧 的改变  动画不基于初始背景进行绘制，而应基于上一帧进行绘制
            sw0.Stop();
            Debug.WriteLine($"render frame cost {sw0.ElapsedMilliseconds}ms");
        }



        private AnimationStage getStageByValue(DataForRender data)
        {
            return this.animationStages[0]; //测试，总是获取第一个
        }


        private GifFramesForRender getGFFR(string gif_name)
        {
            if (!this.frames.ContainsKey(gif_name))
            {
                //var resource_img = Image.FromStream(new MemoryStream(this.resources[stage]));
                this.frames.Add(gif_name, new GifFramesForRender(this.resources[gif_name]));
            }

            var gffr =  this.frames[gif_name];


            return gffr;
        }

        public override void Reset()
        {
            this.PrevFrame = null;
        }
    }


    internal class GifFramesForRender
    {
        public GifFramesForRender(byte[] sourceGif)
        {
            this.SourceGifImage = Image.FromStream(new MemoryStream(sourceGif));

            this.Frames = this.SourceGifImage.GetGifFrames();

            this.CurrentIndex = null;
        }

        public Image SourceGifImage { get; private set; }

        public Bitmap[] Frames { get; private set; }

        public int? CurrentIndex { get; private set; }

        //public Bitmap GetCurrentFrame()
        //{
        //    return Frames[CurrentFrameIdx];
        //}

        public Bitmap GetNextFrame()
        {
            if (CurrentIndex == null)
            {
                CurrentIndex = 0;
            }
            else if (CurrentIndex < this.Frames.Length - 1)
            {
                CurrentIndex++;
            }
            else
            {
                CurrentIndex = 0;
            }

            return Frames[CurrentIndex.Value];

        }

    }








}
