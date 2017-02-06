using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyStack.Modules.iUtility.InterFaceUtility;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace TinyStack.Modules.iUtility.iTools
{
    public class ImageOperate : IImageOperate
    {
        #region 把输入的位图对象转换成特定格式的图片的位图对象
        /// <summary>
        /// 把输入位图文件转换成特定的格式
        /// </summary>
        /// <param name="bitmap">位图文件</param>
        /// <param name="ImaFormat">格式的字符串</param>
        /// <returns>位图文件</returns>
        public Bitmap ImaConvertBitmap(Bitmap bitmap, string ImaFormat)
        {
            try
            {
                return new Bitmap(ImaMemoryStream(bitmap, ImaFormat));  //调用函数ImaMemoryStream，返回修改成特定格式的位图对象
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        #endregion

        #region 把输入的位图文件转换成特定比例的图片的位图文件
        /// <summary>
        /// 把输入的位图文件转换成特定比例的图片的位图文件
        /// </summary>
        /// <param name="bitmap">位图文件</param>
        /// <param name="Percent">缩放的比例</param>
        /// <returns>位图文件</returns>
        public Bitmap ImaZoom(Bitmap bitmap, int Percent)
        {
            try
            {
                return PublicZoom(bitmap, Percent);  //调用函数ImaMemoryStream，返回修改成要求缩放程度的位图对象
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        #endregion

        #region 把输入的位图文件转换成特定宽和高的图片的位图文件
        /// <summary>
        /// 把输入的位图文件转换成特定宽和高的图片的位图文件
        /// </summary>
        /// <param name="bitmap">位图文件</param>
        /// <param name="Width">宽</param>
        /// <param name="Height">高</param>
        /// <returns>位图文件</returns>
        public Bitmap ImaZoom(Bitmap bitmap, int Width, int Height)
        {
            try
            {
                return DoZoom(bitmap, Width, Height);               //调用函数ImaMemoryStream，返回修改成要求缩放程度的位图对象
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        #endregion

        #region 把输入的位图文件转换成在指定位置添加了指定水印内容的图片的位图文件
        /// <summary>
        /// 把输入的位图文件转换成在指定位置添加了指定水印内容的图片的位图文件
        /// </summary>
        /// <param name="bitmap">位图文件</param>
        /// <param name="character">水印的文字内容</param>
        /// <param name="font">水印的字体</param>
        /// <param name="characherColor">文字的颜色</param>
        /// <param name="stringAlignment">文字的对齐方式</param>
        /// <param name="stringFormatFlags">文字的布局方式</param>
        /// <param name="X_Postion">特定位置的横坐标</param>
        /// <param name="Y_Position">特定位置的纵坐标</param>
        /// <returns>位图文件</returns>
        public Bitmap ImaPixScale(Bitmap bitmap, string character, Font font, Color characterColor, int X_Postion, int Y_Position)
        {
            StringFormat stringFormat = new StringFormat(StringFormatFlags.LineLimit);
            return PublicCreateWaterMarkText(bitmap, character, font, characterColor, X_Postion, Y_Position, stringFormat);
        }
        #endregion

        #region 把输入的位图文件转换成在特定位置添加了特定水印内容的图片的位图文件
        /// <summary>
        /// 把输入的位图文件转换成在特定位置添加了特定水印内容的图片的位图文件
        /// </summary>
        /// <param name="bitmap">位图文件</param>
        /// <param name="Character">水印的文字内容</param>
        /// <param name="font">水印的字体</param>
        /// <param name="characterColor">文字的颜色</param>
        /// <param name="stringAlignment">文字的对齐方式</param>
        /// <param name="stringFormatFlags">文字的布局方式</param>
        /// <param name="Position">代表图片位置，数字代表如下：1:左上；2：中上；3：右上；4:左；5：中；6：右；7：左下；8：中下；9：右下</param>      
        /// <returns>位图文件</returns>
        public Bitmap ImaPixScale(Bitmap bitmap, string character, Font font, Color characterColor, int Position)
        {
            {
                try
                {
                    SizeF sizef = SizeWatchMark(character, font);   //取得水印所占像素
                    int Witdth = (int)sizef.Width;                  //取得水印所占像素的宽
                    int Height = (int)sizef.Height;                 //取得水印所占像素的高
                    int bitwith = bitmap.Width;                     //取得需要加水印的图片的像素的宽
                    int bitheight = bitmap.Height;                  //取得需要加水印的图片的像素的高
                    int x = 0;                                        //设置需要加水印的横坐标，默认为0
                    int y = 0;                                      //设置需要加水印的纵坐标，默认为0
                    StringFormat stringFormat = new StringFormat(StringFormatFlags.LineLimit);//设置文字的对齐方式
                    /*根据不同位置，求出水印需要打到的特定为，数字代表如下：
                     1  2  3
                     4  5  6
                     7  8  9
                     */
                    switch (Position)
                    {
                        case 1:
                            break;
                        case 2:
                            x = (bitwith / 2) - (Witdth / 2);
                            break;
                        case 3:
                            x = bitwith - Witdth;
                            break;
                        case 4:
                            y = (bitheight / 2) - (Height / 2);
                            break;
                        case 5:
                            x = (bitwith / 2) - (Witdth / 2);
                            y = (bitheight / 2) - (Height / 2);
                            break;
                        case 6:
                            x = bitwith - Witdth;
                            y = (bitheight / 2) - (Height / 2);
                            break;
                        case 7:
                            y = bitheight - Height;
                            break;
                        case 8:
                            x = (bitwith / 2) - (Witdth / 2);
                            y = bitheight - Height;
                            break;
                        case 9:
                            x = bitwith - Witdth;
                            y = bitheight - Height;
                            break;
                        default:
                            return bitmap;
                    }
                    return PublicCreateWaterMarkText(bitmap, character, font, characterColor, x, y, stringFormat);//返回添加水印后的位图文件
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }
            }
        }
        #endregion

        #region 格式转换的方法
        /// <summary>
        /// 格式转换
        /// </summary>
        /// <param name="bitmap">需要修改格式图片的位图文件</param>
        /// <param name="ImaFormat">需要赚的格式字符串缩写</param>
        /// <returns></returns>
        private MemoryStream ImaMemoryStream(Bitmap bitmap, string ImaFormat)
        {
            MemoryStream stream2; //定义一个用于返回数据的流
            try
            {
                MemoryStream stream = new MemoryStream();
                switch (ImaFormat.ToString())    //将需要转换的格式转换成字符串
                {
                    case "Bmp":                                 //当字符等于“Bmp”时，将存储区的内存流转换成Bmp格式的数据。
                        bitmap.Save(stream, ImageFormat.Bmp);
                        break;

                    case "Gif":
                        bitmap.Save(stream, ImageFormat.Gif);
                        break;

                    case "Jpeg":
                        bitmap.Save(stream, ImageFormat.Jpeg);
                        break;

                    case "Png":
                        bitmap.Save(stream, ImageFormat.Png);
                        break;

                    case "Tiff":
                        bitmap.Save(stream, ImageFormat.Tiff);
                        break;

                    default:
                        break;

                }
                stream2 = stream;               //将已经转换的数据复制到事先定义好的流中
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return stream2;                     //返回已经处理好的流
        }
        #endregion

        #region 图片缩放的方法
        /// <summary> 
        /// 图片缩放的方法
        /// </summary>
        /// <param name="bitmap">位图文件</param>
        /// <param name="percentage">需要缩放到的百分比</param>
        /// <returns></returns>
        private Bitmap PublicZoom(Bitmap bitmap, int percentage)
        {
            int thumbWidth = (bitmap.Width * percentage) / 100;                                 //被缩放后，图片的宽
            int thumbHeight = (bitmap.Height * percentage) / 100;                               //被缩放后，图片的高
            return (Bitmap)bitmap.GetThumbnailImage(thumbWidth, thumbHeight, null, IntPtr.Zero);//生成特定比例的图片。
        }
        #endregion

        #region 位图文件转换成特定宽和高的图片的位图文件的方法
        /// <summary>
        /// 位图文件转换成特定宽和高的图片的位图文件的方法
        /// </summary>
        /// <param name="bitmap">位图文件</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns></returns>
        private Bitmap DoZoom(Bitmap bitmap, int width, int height)
        {
            int thumbWidth = (width > bitmap.Width) ? bitmap.Width : width;  //取得图片宽度以及所要得到的宽度中，较小的一个
            int thumbHeight = (thumbWidth * bitmap.Height) / bitmap.Width;   //取得图片按照较小的宽度，按照比例得到图片的高度
            if (thumbHeight > height)                                        //比较两个高度，取得较小的一个。这样可以保证图片的质量
            {
                thumbHeight = height;                                        //将较小的高度赋予最终需要处理的图片
                thumbWidth = (thumbHeight * bitmap.Width) / bitmap.Height;   //将较小的宽度赋予最终需要处理的图片
            }
            return (Bitmap)bitmap.GetThumbnailImage(thumbWidth, thumbHeight, null, IntPtr.Zero);//调用函数，得到缩略图
        }
        #endregion

        #region 按照图对应条件添加图片水印
        /// <summary>
        /// 按照图对应条件添加图片水印
        /// </summary>
        /// <param name="bitmap">文图文件</param>
        /// <param name="character">字符串</param>
        /// <param name="font">文本格式</param>
        /// <param name="characterColor">文字颜色</param>
        /// <param name="stringAlignment">文字的对齐方式</param>
        /// <param name="stringFormatFlags">文字的布局方式</param>
        /// <returns></returns>
        private Bitmap PublicCreateWaterMarkText(Bitmap bitmap, string character, Font font, Color characterColor, StringAlignment stringAlignment, StringFormatFlags stringFormatFlags)
        {
            StringFormat stringFormat = new StringFormat(stringFormatFlags);
            stringFormat.Alignment = stringAlignment;
            int? x = null;
            return PublicCreateWaterMarkText(bitmap, character, font, characterColor, x, null, stringFormat);
        }
        #endregion

        #region 将图片打到指定位置
        /// <summary>
        /// 将图片打到指定位置
        /// </summary>
        /// <param name="bitmap">位图文件</param>
        /// <param name="character">文字内容</param>
        /// <param name="font">文字格式</param>
        /// <param name="characterColor">文字颜色</param>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="stringFormat">对齐方式</param>
        /// <returns></returns>
        private Bitmap PublicCreateWaterMarkText(Bitmap bitmap, string character, Font font, Color characterColor, int? x, int? y, StringFormat stringFormat)
        {
            return DrawWatermark(bitmap, character, font, characterColor, x, y, stringFormat, 1.0);//调用方法，返回处理过的位图文件
        }
        #endregion

        #region 绘制含有水印图片
        /// <summary>
        /// 绘制含有水印图片
        /// </summary>
        /// <param name="bitmap">位图文件</param>
        /// <param name="text">内容</param>
        /// <param name="font">字体</param>
        /// <param name="characterColor">颜色</param>
        /// <param name="xx">横坐标</param>
        /// <param name="yy">纵坐标</param>
        /// <param name="stringFormat">对齐方式</param>
        /// <param name="opacity">透明度</param>
        /// <returns></returns>
        private Bitmap DrawWatermark(Bitmap bitmap, string text, Font font, Color characterColor, int? xx, int? yy, StringFormat stringFormat, double opacity)
        {
            int width = bitmap.Width;                                           //取得位图文件的宽 
            int height = bitmap.Height;                                         //取得位图文件的高
            Graphics graphics = Graphics.FromImage(bitmap);                     //将位图文件封装成一个绘图图面
            SizeF ef = new SizeF();                                             //定义一个SizeF对象
            int num3 = 0;                                                       //定义一个整形数字
            int num4 = 0;                                                       //定义一个整形数字
            ef = graphics.MeasureString(text, font, bitmap.Width, stringFormat);//将字符所占的像素传到SizeF中
            int num5 = (int)(height * 0.05);                                    //
            num4 = (height - num5) - (((int)ef.Height) / 2);
            num3 = width / 2;
            if (xx.HasValue)
            {
                num3 = xx.Value;
            }
            if (yy.HasValue)
            {
                num4 = yy.Value;
            }
            Bitmap image = new Bitmap(bitmap.Width, bitmap.Height);   //定义一个位图文件，与原图片大小一致
            Graphics graphics2 = Graphics.FromImage(image);           //将此位图文件封装成一个绘图图面
            graphics2.SmoothingMode = SmoothingMode.HighQuality;      //设定此图面为指定高质量、低速度呈现。
            graphics2.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);//在0，0并且按对应大小绘制指定的 Image
            SolidBrush brush = new SolidBrush(characterColor);            //定义画笔的颜色
            graphics2.DrawString(text, font, brush, (float)(num3 + 1), (float)(num4 + 1), stringFormat);//使用指定 StringFormat 的格式化属性，用指定的 Brush 和 Font 对象在指定的位置绘制指定的文本字符串。 
            SolidBrush brush2 = new SolidBrush(Color.FromArgb(0x99, 0xff, 0xff, 0xff));//创建一个color结构
            graphics2.DrawString(text, font, brush2, (float)num3, (float)num4, stringFormat);//使用指定 StringFormat 的格式化属性，用指定的 Brush 和 Font 对象在指定的位置绘制指定的文本字符串。
            bitmap = PublicCreateWaterMarkPic(bitmap, image, 0, 0, opacity);
            graphics2.Dispose();
            return bitmap;
        }
        #endregion

        #region 合成水印以及图片
        /// <summary> 合成水印以及图片
        /// 合成水印以及图片
        /// </summary>
        /// <param name="bitmapPic">图片位图</param>
        /// <param name="bitmapWatermark">水印图片位图</param>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="opacity">透明度</param>
        /// <returns></returns>
        private Bitmap PublicCreateWaterMarkPic(Bitmap bitmapPic, Bitmap bitmapWatermark, int? x, int? y, double opacity)
        {
            int width = bitmapPic.Width;                                        //取得图片的宽
            int height = bitmapPic.Height;                                      //取得图片的高
            Bitmap image = new Bitmap(width, height, PixelFormat.Format24bppRgb);//创新一个新图片
            image.SetResolution(bitmapPic.HorizontalResolution, bitmapPic.VerticalResolution);//设置这个图片的分辨率
            Graphics graphics = Graphics.FromImage(image);                      //将位图文件封装成一个绘图图面
            graphics.SmoothingMode = SmoothingMode.AntiAlias;                   //设定此图面为指定消除锯齿的呈现。 
            graphics.DrawImage(bitmapPic, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);//在指定位置并且按指定大小绘制指定的 Image 的指定部分。 
            int srcWidth = bitmapWatermark.Width;//水印的宽
            int srcHeight = bitmapWatermark.Height;//水印的长
            Bitmap bitmap2 = new Bitmap(image);//定义一个新的位图文件
            bitmap2.SetResolution(bitmapPic.HorizontalResolution, bitmapPic.VerticalResolution);//设置这个图片的分辨率
            Graphics graphics2 = Graphics.FromImage(bitmap2);//将位图文件封装成一个绘图图面
            ImageAttributes imageAttr = new ImageAttributes();//创建一个ImageAttributes类，包含有关在呈现时如何操作位图和图元文件颜色的信息。 
            ColorMap map = new ColorMap();//创建ColorMap类，用来定义转换颜色的映射。
            map.OldColor = Color.FromArgb(0xff, 0, 0xff, 0);//
            map.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] mapArray = new ColorMap[] { map };
            imageAttr.SetRemapTable(mapArray, ColorAdjustType.Bitmap);
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            numArray3 = new float[5];
            numArray3[1] = 1f;
            numArray2[1] = numArray3;
            numArray3 = new float[5];
            numArray3[2] = 1f;
            numArray2[2] = numArray3;
            numArray3 = new float[5];
            numArray3[3] = (float)opacity;
            numArray2[3] = numArray3;
            numArray3 = new float[5];
            numArray3[4] = 1f;
            numArray2[4] = numArray3;
            float[][] newColorMatrix = numArray2;
            ColorMatrix matrix = new ColorMatrix(newColorMatrix);
            imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            int num5 = (width - srcWidth) - 10;
            int num6 = 10;
            if (x.HasValue)
            {
                num5 = x.Value;
            }
            if (y.HasValue)
            {
                num6 = y.Value;
            }
            graphics2.DrawImage(bitmapWatermark, new Rectangle(num5, num6, srcWidth, srcHeight), 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel, imageAttr);//合成图片
            return bitmap2;
        }
        #endregion

        #region 取得水印所占像素的方法
        /// <summary>
        /// 取得水印所占像素
        /// </summary>
        /// <param name="character">字数</param>
        /// <param name="font">文本格式</param>
        /// <returns></returns>
        private SizeF SizeWatchMark(string character, Font font)
        {
            Bitmap temp = new Bitmap(100, 100);
            Graphics graphics = Graphics.FromImage(temp);
            SizeF sizeF = graphics.MeasureString(character, font);
            graphics.Dispose();
            return sizeF;
        }
        #endregion
    }
}
