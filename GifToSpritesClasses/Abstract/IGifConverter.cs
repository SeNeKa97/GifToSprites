﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;

namespace GifToSpritesClasses
{
    public interface IGifConverter
    {
        Image[] Convert();
    }
}
