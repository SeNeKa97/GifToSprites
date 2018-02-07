using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace GifToSpritesClasses
{
    public class ImageFileSaver
    {
        public static bool Save(Image image, string fileName)
        {
            string _fileName = fileName + "_spritesheet";
            string ext = ".png";

            string name = CheckName(_fileName, ext);
            try
            {
                image.Save(name, ImageFormat.Png);
            } catch(Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool SaveMultiple(Image[] images, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))           //проверяем, существует ли уже папка с таким названием
                Directory.CreateDirectory(directoryPath);   //если нет, создаем её
            try
            {
                for (int i = 0; i < images.Length; i++)
                {
                    string fileName = directoryPath + "\\frame_" + (i + 1);
                    string ext = ".png";

                    string name = CheckName(fileName, ext);

                    images[0].Save(name, ImageFormat.Png);
                }
            } catch(Exception ex) {
                return false;
            }
            return true;
        }

        private static string CheckName(string fileName, string ext)
        {
            string checkName = fileName;
            int checkCounter = 0;

            while (File.Exists(checkName + ext))
            {
                checkName = String.Format("{0}({1})", fileName, checkCounter);
                checkCounter++;
            }

            return checkName + ext;
        }
    }
}
