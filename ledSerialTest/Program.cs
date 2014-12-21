using System;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ledSerialTest
{
    class Program
    {
        static SerialPort _serialPort;
        static int LEDCOUNT;
        

        public static void Main()
        {
            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort();
            _serialPort.PortName = "com4";
            _serialPort.BaudRate = 250000;
            _serialPort.Open();
            
            //Prompt user to reset
            Console.WriteLine("Please reset the Arduino...");
            Console.ReadLine();


            //Initialize strip
            ledStrip strip = new ledStrip(240, _serialPort);


            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            while (true)
            {
                sw.Start();

                for (int i = 0; i < 240; i++)
                {
                    for(int j = 0; j < strip.getSize(); j++)
                    {
                        int color = ((i + j)%60)*4;
                        strip.setPixel(j, Color.FromArgb(color,color,255));
                    }
                    strip.update();
                }

                    sw.Stop();
                Console.WriteLine(2/sw.Elapsed.TotalSeconds);
                sw.Reset();
            }

            
        }

        public static void christmas(ledStrip strip)
        {
            //Pixels in group
            int GROUPPIXELS = 120;
            int NUMFRAMES = 480;

            //Times
            int DWELL = 5000;
            int WAIT = 33;

            for (int k = 0; k < NUMFRAMES / 4; k++)
            {
                for (int i = 0; i < strip.getSize() / GROUPPIXELS; i++)
                {
                    // pixels.Color takes RGB values, from 0,0,0 up to 255,255,255
                    //pixels.setPixelColor(i, pixels.Color(0,150,0)); // Moderately bright green color.

                    for (int j = 0; j < GROUPPIXELS / 2; j++)
                    {
                        //Do math for colour interpolation here
                        strip.setPixel(i * GROUPPIXELS + j, Color.FromArgb(120 - 120 / (NUMFRAMES / 4) * k, 120 / (NUMFRAMES / 4) * k, 0));
                    }

                }
                strip.update();
                Thread.Sleep(WAIT);
            }
            Thread.Sleep(DWELL);

            for (int k = 0; k < NUMFRAMES / 4; k++)
            {
                for (int i = 0; i < strip.getSize() / GROUPPIXELS; i++)
                {

                    for (int j = 0; j < GROUPPIXELS / 2; j++)
                    {
                        //Do math for colour interpolation here
                        strip.setPixel(i * GROUPPIXELS + j + GROUPPIXELS / 2, Color.FromArgb(120 / (NUMFRAMES / 4) * k, 120 - 120 / (NUMFRAMES / 4) * k, 0));
                    }
                }

                strip.update();
                Thread.Sleep(WAIT);
            }
            Thread.Sleep(DWELL);

            for (int k = 0; k < NUMFRAMES / 4; k++)
            {
                for (int i = 0; i < strip.getSize() / GROUPPIXELS; i++)
                {
                    // pixels.Color takes RGB values, from 0,0,0 up to 255,255,255
                    //pixels.setPixelColor(i, pixels.Color(0,150,0)); // Moderately bright green color.

                    for (int j = 0; j < GROUPPIXELS / 2; j++)
                    {
                        //Do math for colour interpolation here
                        strip.setPixel(i * GROUPPIXELS + j, Color.FromArgb(120 / (NUMFRAMES / 4) * k, 120 - 120 / (NUMFRAMES / 4) * k, 0));
                    }
                }

                strip.update();
                Thread.Sleep(WAIT);
            }
            Thread.Sleep(DWELL);

            for (int k = 0; k < NUMFRAMES / 4; k++)
            {
                for (int i = 0; i < strip.getSize() / GROUPPIXELS; i++)
                {
                    // pixels.Color takes RGB values, from 0,0,0 up to 255,255,255
                    //pixels.setPixelColor(i, pixels.Color(0,150,0)); // Moderately bright green color.

                    for (int j = 0; j < GROUPPIXELS / 2; j++)
                    {
                        //Do math for colour interpolation here
                        strip.setPixel(i * GROUPPIXELS + j + GROUPPIXELS / 2, Color.FromArgb(120 - 120 / (NUMFRAMES / 4) * k, 120 / (NUMFRAMES / 4) * k, 0));
                    }
                }

                strip.update();
                Thread.Sleep(WAIT);
            }
            Thread.Sleep(DWELL);
        }


        // Display Port values and prompt user to enter a port. 
        public static string SetPortName(string defaultPortName)
        {
            string portName;

            Console.WriteLine("Available Ports:");
            foreach (string s in SerialPort.GetPortNames())
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter COM port value (Default: {0}): ", defaultPortName);
            portName = Console.ReadLine();

            if (portName == "" || !(portName.ToLower()).StartsWith("com"))
            {
                portName = defaultPortName;
            }
            return portName;
        }

    }
}
