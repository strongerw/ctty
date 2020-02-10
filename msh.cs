using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctty
{
    class Msh
    {
        private SerialPort serialPort = new SerialPort();
        public void RunTerminal()
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
                /*
                serialPort.Parity = Parity.None;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                */
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

                
                //int ch = Console.Read();
                //serialputchar(ch);
                //只要串口还处于打开状态，就继续读取数据显示并向串口发送数据
                
                ConsoleKeyInfo key = Console.ReadKey(true);
                serialputchar(key.KeyChar);
                if(false)
                if(key.Modifiers != ConsoleModifiers.Shift)
                {
                    //说明用户还按下了别的按键（CTR  SHITFT  ALT中一个或多个）
                    if(key.Modifiers == ConsoleModifiers.Control)
                    {
                        if(key.Key == ConsoleKey.D)
                        {
                            serialPort.Write("reboot\r\n");
                        }
                    }
                }
                else
                {
                    //说明用户只是输入内容
                    char[] buffer = new char[1];
                    buffer[0] = (char)(key.Key );
                    serialPort.Write(buffer, 0, 1);
                }
            }
            try
            {
                serialPort.Close();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void dataRecEvent(object sender, SerialDataReceivedEventArgs e)
        {
            int dataCnt = serialPort.BytesToRead;
            try
            {
                if (dataCnt > 0)
                {
                    char[] buffer = new char[dataCnt];
                    serialPort.Read(buffer, 0, dataCnt);
                        Console.Write(buffer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void serialputchar(int c)
        {
            char ch = (char)c;

            String tmp = new string(ch, 1);
            serialPort.Write(tmp);

        }
    }
}
