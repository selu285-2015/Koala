using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PictureHiding
{
    class Program
    {
        static void Main(string[] args)
        {
            
            EncodeImage(
                "c:\\users\\cam\\documents\\visual studio 2013\\Projects\\PictureHiding\\PictureHiding\\Content\\Koala.jpg",
                "Content/Test.txt", "output.jpg");
            DecodeImage("output.jpg", "temp.txt");
        }

        public static void EncodeImage(string imageFilePath, string encodedFilePath, string outputFilePath)
        {
            var picture =
                File.ReadAllBytes(imageFilePath);
            var otherFile = File.ReadAllBytes(encodedFilePath);
            var pictureBits = new BitArray(picture);
            var otherFileBits = new BitArray(otherFile);

            for (int i = 1, j = 8; i < otherFileBits.Length; i += 1, j = +8)
            {
                pictureBits[j] = otherFileBits[i];
            }
            byte[] newPic = new byte[picture.Length];
            pictureBits.CopyTo(newPic, 0);
            var stream = new MemoryStream(newPic);
            Image image = Image.FromStream(stream);
            image.Save(outputFilePath, ImageFormat.Jpeg);  // Or Png
        }

        

        public static void DecodeImage(string imageFilePath, string outputFilePath)
        {
            var picture =
                File.ReadAllBytes(imageFilePath);
            var pictureBits = new BitArray(picture);
            var newBit = new List<Boolean>();
            var newFile = new List<Byte>();
            for (int i = 0, j = 8; j < pictureBits.Length; i++, j = +8)
            {
                if (i > 8)
                {
                    i = 0;
                    newFile.Add(Encodebool(newBit.ToArray()));
                }
                newBit.Add(pictureBits[j]);
            }
            var temp = newFile.ToList();
            //var stream = new MemoryStream();

        }

        public static byte Encodebool(bool[] arr)
        {
            byte val = 0;
            foreach (bool b in arr)
            {
                val <<= 1;
                if (b) val |= 1;
            }
            return val;
        }

        
    }
}
