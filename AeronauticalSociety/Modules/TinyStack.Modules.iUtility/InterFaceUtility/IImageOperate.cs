using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TinyStack.Modules.iUtility.InterFaceUtility
{
    /// <summary>
    /// 图片操作类接口
    /// </summary>
    interface IImageOperate
    {
        Bitmap ImaConvertBitmap(Bitmap bitmap, string ImaFormat);
        Bitmap ImaZoom(Bitmap bitmap, int Percent);
        Bitmap ImaZoom(Bitmap bitmap, int Width, int Height);
        Bitmap ImaPixScale(Bitmap bitmap, string Character, Font font, Color characherColor, int X_Postion, int Y_Position);
        Bitmap ImaPixScale(Bitmap bitmap, string character, Font font, Color characherColor, int Position);
    }
}
