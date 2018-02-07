using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GifToSpritesClasses
{
    public class InvalidImageFormatException:Exception
    {
        public InvalidImageFormatException(){}

        public InvalidImageFormatException(string message):base(message){}

        public InvalidImageFormatException(string message, Exception inner):base(message,inner){}

        public InvalidImageFormatException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
