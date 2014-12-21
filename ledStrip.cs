using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO.Ports;

namespace ledSerialTest
{
    public class ledStrip
    {
        private static int NUMLEDS;
        private static List<Color> strip;
        private static List<Color> lastWrite;
        private static SerialPort _serialPort;

        public ledStrip(int ledCount, SerialPort arduino)
        {
            NUMLEDS = ledCount;
            strip = new List<Color>();
            lastWrite = new List<Color>();
            _serialPort = arduino;

            //Initialize Blank Arrays
            for(int i = 0; i < ledCount; i++)
            {
                strip.Add(Color.FromArgb(0, 0, 0));
            }
            for (int i = 0; i < ledCount; i++)
            {
                lastWrite.Add(Color.FromArgb(255, 255, 255));
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

            for (int i = 0; i < NUMLEDS; i++)
            {
                if(!strip[i].Equals(lastWrite[i]))
                {
                    Color c = strip[i];

                    _serialPort.Write(BitConverter.GetBytes(i), 0, 1);
                    _serialPort.Write(BitConverter.GetBytes(c.G), 0, 1);
                    _serialPort.Write(BitConverter.GetBytes(c.R), 0, 1);
                    _serialPort.Write(BitConverter.GetBytes(c.B), 0, 1);

                    lastWrite[i] = c;
                }
            }

            _serialPort.Write(BitConverter.GetBytes('e'), 0, 1);
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
