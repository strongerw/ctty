using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Ctty
{
    class Program
    {
        private enum HardwareEnum
        {
            // 硬件
            Win32_Processor, // CPU 处理器
            Win32_PhysicalMemory, // 物理内存条
            Win32_Keyboard, // 键盘
            Win32_PointingDevice, // 点输入设备，包括鼠标。
            Win32_FloppyDrive, // 软盘驱动器
            Win32_DiskDrive, // 硬盘驱动器
            Win32_CDROMDrive, // 光盘驱动器
            Win32_BaseBoard, // 主板
            Win32_BIOS, // BIOS 芯片
            Win32_ParallelPort, // 并口
            Win32_SerialPort, // 串口
            Win32_SerialPortConfiguration, // 串口配置
            Win32_SoundDevice, // 多媒体设置，一般指声卡。
            Win32_SystemSlot, // 主板插槽 (ISA & PCI & AGP)
            Win32_USBController, // USB 控制器
            Win32_NetworkAdapter, // 网络适配器
            Win32_NetworkAdapterConfiguration, // 网络适配器设置
            Win32_Printer, // 打印机
            Win32_PrinterConfiguration, // 打印机设置
            Win32_PrintJob, // 打印机任务
            Win32_TCPIPPrinterPort, // 打印机端口
            Win32_POTSModem, // MODEM
            Win32_POTSModemToSerialPort, // MODEM 端口
            Win32_DesktopMonitor, // 显示器
            Win32_DisplayConfiguration, // 显卡
            Win32_DisplayControllerConfiguration, // 显卡设置
            Win32_VideoController, // 显卡细节。
            Win32_VideoSettings, // 显卡支持的显示模式。

            // 操作系统
            Win32_TimeZone, // 时区
            Win32_SystemDriver, // 驱动程序
            Win32_DiskPartition, // 磁盘分区
            Win32_LogicalDisk, // 逻辑磁盘
            Win32_LogicalDiskToPartition, // 逻辑磁盘所在分区及始末位置。
            Win32_LogicalMemoryConfiguration, // 逻辑内存配置
            Win32_PageFile, // 系统页文件信息
            Win32_PageFileSetting, // 页文件设置
            Win32_BootConfiguration, // 系统启动配置
            Win32_ComputerSystem, // 计算机信息简要
            Win32_OperatingSystem, // 操作系统信息
            Win32_StartupCommand, // 系统自动启动程序
            Win32_Service, // 系统安装的服务
            Win32_Group, // 系统管理组
            Win32_GroupUser, // 系统组帐号
            Win32_UserAccount, // 用户帐号
            Win32_Process, // 系统进程
            Win32_Thread, // 系统线程
            Win32_Share, // 共享
            Win32_NetworkClient, // 已安装的网络客户端
            Win32_NetworkProtocol, // 已安装的网络协议
            Win32_PnPEntity,//all device
        }

        /// <summary>
        /// WMI取硬件信息
        /// </summary>
        /// <param name="hardType"></param>
        /// <param name="propKey"></param>
        /// <returns></returns>
        private static string[] MulGetHardwareInfo(HardwareEnum hardType, string propKey)
        {

            List<string> deviceList = new List<string>();
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + hardType))
                {
                    var hardInfos = searcher.Get();
                    foreach (var hardInfo in hardInfos)
                    {
                        deviceList.Add(hardInfo.Properties[propKey].Value.ToString());
                    }

                    searcher.Dispose();
                }
                return deviceList.ToArray();
            }
            catch
            {
                return null;
            }
            finally
            { deviceList = null; }
        }
        SerialPort serialPort = new SerialPort();
        static void Main(string[] args)
        {
            Msh msh = new Msh();
            msh.RunTerminal();
        }
        void MainProgram(string[] args)
        {
            
            
            Console.WriteLine("Wellcome to this console tty tools.");
            Console.WriteLine("Now the program will scan the Serial Port!");
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            if (ports.Length == 0)
            {
                Console.WriteLine("There is no Serial Port availiable!");
                return;
            }
            Console.WriteLine("The availiable Serial Port Names:");

            for (int i = 0; i < ports.Length; i++)
            {
                Console.Write(i.ToString().ToUpper() + ": --> ");
                Console.WriteLine(ports[i] + (i == 0 ? "(Default)" : " "));
            }
            bool portNameValidate = false;
            while (!portNameValidate)
            {
                string input = Console.ReadLine();
                if (input.Equals(""))
                {
                    serialPort.PortName = ports[0];
                    Console.WriteLine("Used Enter to select " +
                        "the default com port!");
                    Console.WriteLine("Port Name:  " + serialPort.PortName);
                    portNameValidate = true;
                }
                for (int i = 0; i < ports.Length; i++)
                {
                    if (input.ToUpper().Equals(ports[i]))
                    {
                        serialPort.PortName = ports[i];
                        portNameValidate = true;
                        break;
                    }
                }
            }
            bool baudrateValidate = false;
            Console.WriteLine("Now Input the Baudrate(Default value is 115200" +
                ",Click Enter to Validate)");
            while (!baudrateValidate)
            {
                string input = Console.ReadLine();
                if (input.Equals(""))
                {
                    baudrateValidate = true;
                    serialPort.BaudRate = 115200;
                    Console.WriteLine("Used the Enter to set the baudrate to 115200");
                }
                else
                    try
                    {
                        int baud = Convert.ToInt32(input);
                        serialPort.BaudRate = baud;
                        baudrateValidate = true;
                        Console.WriteLine("Now set the baudrate to " + baud.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error ! Invalidate input!");
                        Console.WriteLine("You can click Enter to set baudrate to 115200");
                    }
            }
            try
            {
                serialPort.Parity = Parity.None;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                serialPort.Open();
                serialPort.DataReceived += new SerialDataReceivedEventHandler(this.dataRecEvent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.Clear();
            Console.WriteLine("TTY use Serial Port Tool.");
            Console.WriteLine("Baudrate " + serialPort.BaudRate.ToString()
                                + " @ " + serialPort.PortName);

            while (serialPort.IsOpen)
            {
                //只要串口还处于打开状态，就继续读取数据显示并向串口发送数据

            }



        }

        private void dataRecEvent(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            int dataCnt = serialPort.BytesToRead;
            try
            {
                if(dataCnt > 0)
                {
                    byte[] buffer = new byte[dataCnt];
                    serialPort.Read(buffer, 0, dataCnt);
                    Console.Write(buffer);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
