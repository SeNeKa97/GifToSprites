using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using GifToSpritesClasses;

namespace gifToSprites
{
    public partial class Form1 : Form
    {
        private Image gifImg;
        private IGifConverter converter;

        public Form1()
        {
            InitializeComponent();

            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.FileName = null;
            openFileDialog1.DefaultExt = ".gif";
            openFileDialog1.Filter = "Animation files |*.gif";
            openFileDialog1.Multiselect = false;
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            gifImg = Image.FromFile(openFileDialog1.FileName);
            pictureBox1.Image = gifImg;
            sliceButton.Enabled = true;
        }




        //Метод используется для нарезания анимации на отдельные файлы изображений, каждый 
        //из которых содержит в себе отдельный кадр анимации
        //все полученные изображения помещаются в новую папку, имеющую имя, идентичное имени .gif-файла, 
        //для чего вырезается расширение из имени файла
        private void gifToDifferentFrames(Image gif) 
        {
            
            FrameDimension dimension = new FrameDimension(gif.FrameDimensionsList[0]);
            int frameCount = gif.GetFrameCount(dimension);

            string directoryPath = cropExtension(openFileDialog1.FileName, ".gif"); //получаем путь к создаваемой папке путем вырезания расширения из имени .gif-файла 
            MessageBox.Show(PathHelper.GetFullPathWithoutExtension(openFileDialog1.FileName));
            Image[] frames = new Image[frameCount];         //Создаем массив кадров с длинной, равной количеству кадров анимации
            Graphics g;

            if (!Directory.Exists(directoryPath))           //проверяем, существует ли уже папка с таким названием
                Directory.CreateDirectory(directoryPath);   //если нет, создаем её


            for (int i = 0; i < frameCount; i++) 
            {
                frames[i] = new Bitmap(gif.Width, gif.Height, gif.PixelFormat); //Каждый элемент массива кадров инициализируем отдельным bitmap'ом,
                                                                                //имеющий ширину, высоту и кол-во битов на пиксель как у оригинального .gif

                gif.SelectActiveFrame(dimension, i);                            //извлекаем из .gif текущий кадр
                g = Graphics.FromImage(frames[i]);                              //создаем новый холст из текущего элемента массива кадров
                g.DrawImage(gif,0,0);                                           //и переносим кадр анимации на холст

                frames[i].Save(directoryPath + "\\" + "frame_" + (i + 1) + ".png");//сохраняем измененный элемент массива кадров
            }
            
            foreach (var item in frames) 
                if(item!=null)
                    item.Dispose();      //освобождаем ресурсы, занимаемые элементами массива кадров
        }




        //Метод позволяет сохранить кадры анимации в виде её последовательной раскадровки на одном изображении (спрайтлисте)
        private void gifToSpriteSheet(Image gif)
        {
            FrameDimension dimension = new FrameDimension(gif.FrameDimensionsList[0]);
            int frameCount = gif.GetFrameCount(dimension);

            //обрезаем расширение, чтобы иметь возможность назвать результирующий файл так же, как и .gif-файл,
            string directoryPath = cropExtension(openFileDialog1.FileName, ".gif");


            //создаем новый спрайтлист - bitmap, с высотой .gif-файла и шириной, полученной путем умножения кол-ва кадров анимации
            //на ширину изображения .gif-файла
            using (Bitmap spriteSheet = new Bitmap(gif.Width * frameCount, gif.Height, gif.PixelFormat))
            {
                //Создаем обьект графики из спрайтлиста для обеспечения возможности рисования на нем
                using (Graphics g = Graphics.FromImage(spriteSheet))
                {
                    
                    for (int frames = 0; frames < frameCount; frames++)
                    {
                        gif.SelectActiveFrame(dimension, frames); //выбираем текущий кадр анимации
                        g.DrawImage(gif, gif.Width * frames, 0);  //и переносим его на указанную позицию на спрайтлисте
                    }

                    spriteSheet.Save(directoryPath + "_spritesheet" + ".png"); //сохраняем спрайтлист с названием файла анимации, но с приставкой "_spritesheet"
                }
            }
        }



        private string cropExtension(string path, string extension) 
        {
            StringBuilder sb = new StringBuilder(path);
            for (int i = 0; i < sb.Length; i++) 
                if (sb[i] == extension[0]) 
                    sb.Remove(i, extension.Length);

            return sb.ToString();
        }



        private void sliceButton_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
                gifToSpriteSheet(gifImg);
            else
                gifToDifferentFrames(gifImg);
        }
    }
}
