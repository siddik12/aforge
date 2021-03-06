﻿/*
 *  This file is adapted from Code Project article "Image Per Pixel Enumeration, Pixel Format Conversion and More"
 *  Copyright (c) 2010-2012 Smart K8
 *  http://www.codeproject.com/Articles/66550/Image-Per-Pixel-Enumeration-Pixel-Format-Conversio
 *  
 *  Released under Code Project Open License, CPOL, http://www.codeproject.com/info/cpol10.aspx
 */

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ImagePixelEnumerator.Helpers.Pixels.NonIndexed
{
    /// <summary>
    /// Name |                     Blue                      |                     Green                     |                      Red                      |                     Alpha                     |
    /// Bit  |00|01|02|03|04|05|06|07|08|09|10|11|12|13|14|15|16|17|18|19|20|21|22|23|24|25|26|27|28|29|30|31|32|33|34|35|36|37|38|39|40|41|42|43|44|45|46|47|48|49|50|51|52|53|54|55|56|57|58|59|60|61|62|63|
    /// Byte |00000000000000000000000|11111111111111111111111|22222222222222222222222|33333333333333333333333|44444444444444444444444|55555555555555555555555|66666666666666666666666|77777777777777777777777|
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    internal struct PixelDataArgb64 : INonIndexedPixel
    {
        // raw component values
        [FieldOffset(0)] private UInt16 blue;   // 00 - 15
        [FieldOffset(2)] private UInt16 green;  // 16 - 31
        [FieldOffset(4)] private UInt16 red;    // 32 - 47
        [FieldOffset(6)] private UInt16 alpha;  // 48 - 63

        // raw high-level values
        [FieldOffset(0)] private UInt64 raw;    // 00 - 63

        // processed component values
        public Int32 Alpha { get { return alpha >> 5; } }
        public Int32 Red { get { return red >> 5; } }
        public Int32 Green { get { return green >> 5; } }
        public Int32 Blue { get { return blue >> 5; } }

        /// <summary>
        /// See <see cref="INonIndexedPixel.Argb"/> for more details.
        /// </summary>
        public Int32 Argb
        {
            get { return Alpha << 48 | Red << 32 | Green << 16 | Blue; }
        }

        /// <summary>
        /// See <see cref="INonIndexedPixel.GetColor"/> for more details.
        /// </summary>
        public Color GetColor()
        {
            return Color.FromArgb(Argb);
        }

        /// <summary>
        /// See <see cref="INonIndexedPixel.SetColor"/> for more details.
        /// </summary>
        public void SetColor(Color color)
        {
            alpha = (UInt16) (color.A << 5);
            red = (UInt16) (color.R << 5);
            green = (UInt16) (color.G << 5);
            blue = (UInt16) (color.B << 5);
        }

        /// <summary>
        /// See <see cref="INonIndexedPixel.Value"/> for more details.
        /// </summary>
        public UInt64 Value
        {
            get { return raw; }
            set { raw = value; }
        }
    }
}
