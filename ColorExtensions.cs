using System.Globalization;
using UnityEngine;

public static class ColorExtensions
{
    public static Color Parse(this Color color, string hex)
    {
        var r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
        var g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
        var b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
        var a = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);

        return new Color(r, g, b, a);
    }

    public static string ToHex(this Color color)
    {
        return string.Format("#{0:x2}{1:x2}{2:x2}{3:x2}", (int)color.r * 255, (int)color.g * 255, (int)color.b * 255,
            (int)color.a * 255);
    }

    public static float[] Values(this Color color)
    {
        var values = new float[4];
        values[0] = color.r;
        values[1] = color.g;
        values[2] = color.b;
        values[3] = color.a;

        return values;
    }
}