# PCMonitorForUSBScreen
这是一个将电脑监控数据渲染到 USB 屏幕（用单片机驱动的屏幕）的应用程序，监控屏的界面可以完全自定义。
this is a pc monitor application to drive usb screen with serial portal(com).  
开发这个项目的初衷是因为购买的电脑监控屏附带的软件无法满足个人的需求。
该项目的目标是适配此类以单片机驱动的屏幕，使其可以作为PC的监控屏幕。欢迎硬件大佬合作提供固件接口以进行适配。

[点击下载程序](http://shared.lvhang.site/release/PCMonitor_v0.2.zip)

### 项目简介
该项目的目标是实现一个通用监控副屏应用程序，可以适配各种单片机驱动的屏幕，并且支持完全的监控屏幕内容自定义能力。
该项目的硬件监控数据获取基于 OpenHardwareMonitor 项目。
该项目中包括以下几个csproj项目
USBScreen，该子项目的功能是提供统一的屏幕访问接口，以及适配各类屏幕。
PCMonitor，该子项目包含了该项目的主要功能，包括硬件监控数据的获取以及将主题渲染到屏幕上。
PCMonitor.UI，应用程序界面。
PCMonitor.Editor，用于可视化编辑主题的工具。（开发中...）


### 界面与效果
目前仅提供一个默认主题，样式如下图所示  
实际效果  
![1627887896250 (1)](https://user-images.githubusercontent.com/936437/128889172-e0482213-ce06-4474-a216-d86c750a249b.jpg)  
监控界面  
![7384](https://user-images.githubusercontent.com/936437/129385194-060e62d2-2329-4300-b384-b0744d276f87.png)  
程序界面  
![image](https://user-images.githubusercontent.com/936437/129377518-570441e4-479d-449c-904f-d150f5ca595f.png)


### 支持设备

目前应用的支持设备为：  
[3.5寸IPS TYPEC机箱副屏](https://s.click.taobao.com/KWodsju)

**欢迎硬件开发大佬提供固件接口信息，该项目会进行适配。**


### 主题的配置
主题的配置位于themes文件夹下，一个以主题名称命名的文件夹包含背景图片bg.png和配置文件config.json。背景图定义了主题的静态内容，config.json定义了主题所有显示的数据以及显示的位置和方式。
一个显示内容的区域为一个widget,一个widget需要定义位置，宽高，绑定数据，样式等内容，目前提供三种类型的widget：进度条，波形图，文本，其中**进度条和波形图只支持百分比数据**，目前包含的数据如下表所示。


| 数据 | 数据说明 | 
| --- | --- | 
| CPU_Load | CPU负载 | 
| CPU_Temp | CPU温度 | 
| CPU_Hz | CPU频率 | 支持 |
| CPU_Fan_Speed | CPU风扇转数 | 
| GPU_Load | GPU负载 | 
| GPU_Temp | GPU温度 | 
| GPU_Hz | GPU频率 | 支持 | 
| GPU_RAM_Total | 总显存 | 
| GPU_RAM_Used| 已使用显存 | 
| GPU_RAM_Load | 显存使用百分比 | 
| GPU_Fan_Speed | 显卡风扇转数 | 
| RAM_Used | 已使用内存 | 支持 | 
| RAM_Free | 空闲内存 | 支持 | 
| RAM_Load | 内存负载百分比 | 
| Network_Upload | 网卡上传速度 | 
| Network_Download | 网卡下载速度 |
| Total_Days | 日期计数<br>(根据配置的开始时间） | 



### 计划
1.适配更多的屏幕，欢迎固件开发大佬合作。  
2.提供可视化的配置工具  
3.更健壮的渲染  

### 目前问题
由于对目前设备的固件接口不清楚，所以因为未知的原因当widget的宽度设置为奇数时，以及其他一些不恰当的配置时设备会出现异常，需要断电重启后才能正常使用。

