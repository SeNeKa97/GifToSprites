using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GifToSpritesClasses
{
    class GifToSpritesheet : IConverter
    {
        private Image _gif;
        private int dimX, dimY;
        private FrameDimension dimension;



        public GifToSpritesheet(Image gif)
        {
            if (!ImageFormat.Gif.Equals(gif))
                throw new InvalidImageFormatException("Selected image is not of .gif format");

            this._gif = gif;
            dimY = 1;
            dimension = new FrameDimension(gif.FrameDimensionsList[0]);
            dimX = gif.GetFrameCount(dimension);
        }


        private GifToSpritesheet(Image gif, int dimX, int dimY)
        {
            
        }



        public Image[] Convert()
        {
            //создаем новый спрайтлист - bitmap, с высотой .gif-файла и шириной, полученной путем умножения кол-ва кадров анимации
            //на ширину изображения .gif-файла
            Bitmap spriteSheet = new Bitmap(_gif.Width * dimX, _gif.Height, _gif.PixelFormat);
            
            //Создаем обьект графики из спрайтлиста для обеспечения возможности рисования на нем
            using (Graphics g = Graphics.FromImage(spriteSheet))
            {

                for (int frames = 0; frames < dimX; frames++)
                {
                    _gif.SelectActiveFrame(dimension, frames); //выбираем текущий кадр анимации
                    g.DrawImage(_gif, _gif.Width * frames, 0);  //и переносим его на указанную позицию на спрайтлисте
                }
            }
            return new Image[] { spriteSheet };
        }
    }
}
