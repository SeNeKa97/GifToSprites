using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GifToSpritesClasses
{
    public class GifToSprites : IGifConverter
    {
        private Image _gif;
        private FrameDimension dimension;


        public GifToSprites(Image gif)
        {
            if (!ImageFormat.Gif.Equals(gif.RawFormat))
                throw new InvalidImageFormatException("Selected image is not of .gif format");

            _gif = gif;
            dimension = new FrameDimension(gif.FrameDimensionsList[0]);
        }



        public Image[] Convert()
        {
            int frameCount = _gif.GetFrameCount(dimension);

            Image[] frames = new Image[frameCount];     //Creating an array with a length equal to animation frame count
            Graphics g;

            for (int i = 0; i < frameCount; i++)
            {
                frames[i] = new Bitmap(     //Each frame array element is initialized by a separate bitmap
                        _gif.Width,         //having width, height and pixelFormat as those of a source .gif
                        _gif.Height, 
                        _gif.PixelFormat); 
                                                                                

                _gif.SelectActiveFrame(dimension, i);   //extracting current frame from .gif
                g = Graphics.FromImage(frames[i]);      //creating a new canvas from a current frame array element
                g.DrawImage(_gif, 0, 0);                //relocating a frame onto a canvas
            }

            return frames;
        }
    }
}
