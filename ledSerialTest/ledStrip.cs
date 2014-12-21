using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.IO.Ports;

namespace ledSerialTest
{
    public class ledStrip
    {
        private static int NUMLEDS;
        private static List<Color> strip;
        private static SerialPort _serialPort;

        public ledStrip(int ledCount, SerialPort arduino)
        {
            NUMLEDS = ledCount;
            strip = new List<Color>();
            _serialPort = arduino;

            //Initialize Blank Arrays
            for(int i = 0; i < ledCount; i++)
            {
                strip.Add(Color.FromArgb(0, 0, 0));
            }
            update();
        }

        public void setPixel(int index, Color color)
        {
            //check valid pixel index
            if (index >= NUMLEDS || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

           //set pixel value
           strip[index] = color;
        }

        public Color getPixel(int index)
        {
            //check valid pixel index
            if (index >= NUMLEDS || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            //get pixel value
            return (Color) strip[index];
        }

        public int getSize()
        {
            return NUMLEDS;
        }

        public void update()
        {
            byte[] buf = new byte[NUMLEDS * 3];

            for (int i = 0; i < NUMLEDS; i++)
            {
                Color c = strip[i];

                //Write Bytes
                buf[3 * i + 0] = c.G;
                buf[3 * i + 1] = c.R;
                buf[3 * i + 2] = c.B;
                
                /*_serialPort.Write(BitConverter.GetBytes(c.G), 0, 1);
                _serialPort.Write(BitConverter.GetBytes(c.R), 0, 1);
                _serialPort.Write(BitConverter.GetBytes(c.B), 0, 1);*/
            }

            _serialPort.Write(buf, 0, NUMLEDS * 3);

            Thread.Sleep(8);
        }

        public void clear()
        {
            for (int i = 0; i < NUMLEDS; i++)
            {
                strip[i] = Color.FromArgb(0, 0, 0);
            }
        }
        public void flood(Color c)
        {
            for (int i = 0; i < NUMLEDS; i++)
            {
                strip[i] = c;
            }
        }
    }
}
