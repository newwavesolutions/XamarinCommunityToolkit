using System;
using Microsoft.Maui; using Microsoft.Maui.Controls; using Microsoft.Maui.Graphics; using Microsoft.Maui.Controls.Compatibility;

namespace Xamarin.CommunityToolkit.Extensions
{
	public static class ColorExtension
	{
		/// <returns>RGB(255, 255, 255)</returns>
		public static string ToRgbString(this Color c) => $"RGB({c.GetByteRed()},{c.GetByteGreen()},{c.GetByteBlue()})";

		/// <returns>RGBA(255, 255, 255, 255)</returns>
		public static string ToRgbaString(this Color c) =>
			$"RGBA({c.GetByteRed()}, {c.GetByteGreen()}, {c.GetByteBlue()}, {c.GetByteAlpha()})";

		/// <returns>#FFFFFF</returns>
		public static string ToHexRgbString(this Color c) =>
			$"#{c.GetByteRed():X2}{c.GetByteGreen():X2}{c.GetByteBlue():X2}";

		/// <returns>#FFFFFFFF</returns>
		public static string ToHexRgbaString(this Color c) =>
			$"#{c.GetByteRed():X2}{c.GetByteGreen():X2}{c.GetByteBlue():X2}{c.GetByteAlpha():X2}";

		/// <returns>CMYK(100%,100%,100%,100%)</returns>
		public static string ToCmykString(this Color c) =>
			$"CMYK({c.GetPercentCyan():P},{c.GetPercentMagenta():P},{c.GetPercentYellow():P},{c.GetPercentBlackKey():P})";

		/// <returns>CMYK(100%,100%,100%,100%,100%)</returns>
		public static string ToCmykaString(this Color c) =>
			$"CMYKA({c.GetPercentCyan():P},{c.GetPercentMagenta():P},{c.GetPercentYellow():P},{c.GetPercentBlackKey():P},{c.A:P})";

		/// <returns>HSLA(360,100%,100%)</returns>
		public static string ToHslString(this Color c) => $"HSL({c.Hue:P},{c.Saturation:P},{c.Luminosity:P})";

		/// <returns>HSLA(360°,100%,100%,100%)</returns>
		public static string ToHslaString(this Color c) =>
			$"HSLA({c.GetDegreeHue()}°,{c.Saturation:P},{c.Luminosity:P},{c.A:P})";

		public static Color WithRed(this Color baseColor, double newR) =>
			Colors.FromRgba(newR, baseColor.G, baseColor.B, baseColor.A);

		public static Color WithGreen(this Color baseColor, double newG) =>
			Colors.FromRgba(baseColor.R, newG, baseColor.B, baseColor.A);

		public static Color WithBlue(this Color baseColor, double newB) =>
			Colors.FromRgba(baseColor.R, baseColor.G, newB, baseColor.A);

		public static Color WithAlpha(this Color baseColor, double newA) =>
			Colors.FromRgba(baseColor.R, baseColor.G, baseColor.B, newA);

		public static Color WithRed(this Color baseColor, byte newR) =>
			Colors.FromRgba((double)newR / 255, baseColor.G, baseColor.B, baseColor.A);

		public static Color WithGreen(this Color baseColor, byte newG) =>
			Colors.FromRgba(baseColor.R, (double)newG / 255, baseColor.B, baseColor.A);

		public static Color WithBlue(this Color baseColor, byte newB) =>
			Colors.FromRgba(baseColor.R, baseColor.G, (double)newB / 255, baseColor.A);

		public static Color WithAlpha(this Color baseColor, byte newA) =>
			Colors.FromRgba(baseColor.R, baseColor.G, baseColor.B, (double)newA / 255);

		public static Color WithCyan(this Color baseColor, double newC) =>
			Colors.FromRgba((1 - newC) * (1 - baseColor.GetPercentBlackKey()),
						   (1 - baseColor.GetPercentMagenta()) * (1 - baseColor.GetPercentBlackKey()),
						   (1 - baseColor.GetPercentYellow()) * (1 - baseColor.GetPercentBlackKey()),
						   baseColor.A);

		public static Color WithMagenta(this Color baseColor, double newM) =>
			Colors.FromRgba((1 - baseColor.GetPercentCyan()) * (1 - baseColor.GetPercentBlackKey()),
						   (1 - newM) * (1 - baseColor.GetPercentBlackKey()),
						   (1 - baseColor.GetPercentYellow()) * (1 - baseColor.GetPercentBlackKey()),
						   baseColor.A);

		public static Color WithYellow(this Color baseColor, double newY) =>
			Colors.FromRgba((1 - baseColor.GetPercentCyan()) * (1 - baseColor.GetPercentBlackKey()),
						   (1 - baseColor.GetPercentMagenta()) * (1 - baseColor.GetPercentBlackKey()),
						   (1 - newY) * (1 - baseColor.GetPercentBlackKey()),
						   baseColor.A);

		public static Color WithBlackKey(this Color baseColor, double newK) =>
			Colors.FromRgba((1 - baseColor.GetPercentCyan()) * (1 - newK),
						   (1 - baseColor.GetPercentMagenta()) * (1 - newK),
						   (1 - baseColor.GetPercentYellow()) * (1 - newK),
						   baseColor.A);

		public static byte GetByteRed(this Color c) => ToByte(c.R * 255);

		public static byte GetByteGreen(this Color c) => ToByte(c.G * 255);

		public static byte GetByteBlue(this Color c) => ToByte(c.B * 255);

		public static byte GetByteAlpha(this Color c) => ToByte(c.A * 255);

		// Hue is a degree on the color wheel from 0 to 360. 0 is red, 120 is green, 240 is blue.
		public static double GetDegreeHue(this Color c) => c.Hue * 360;

		// Note : double Percent R, G and B are simply Colors.R, Colors.G and Colors.B

		public static double GetPercentBlackKey(this Color c) => 1 - Math.Max(Math.Max(c.R, c.G), c.B);

		public static double GetPercentCyan(this Color c) =>
			(1 - c.R - c.GetPercentBlackKey()) / (1 - c.GetPercentBlackKey());

		public static double GetPercentMagenta(this Color c) =>
			(1 - c.G - c.GetPercentBlackKey()) / (1 - c.GetPercentBlackKey());

		public static double GetPercentYellow(this Color c) =>
			(1 - c.B - c.GetPercentBlackKey()) / (1 - c.GetPercentBlackKey());

		public static Color ToInverseColor(this Color baseColor) =>
			Colors.FromRgb(1 - baseColor.R, 1 - baseColor.G, 1 - baseColor.B);

		public static Color ToBlackOrWhite(this Color baseColor) => baseColor.IsDark() ? Colors.Black : Colors.White;

		public static Color ToBlackOrWhiteForText(this Color baseColor) =>
			baseColor.IsDarkForTheEye() ? Colors.White : Colors.Black;

		public static Color ToGrayScale(this Color baseColor)
		{
			var avg = (baseColor.R + baseColor.B + baseColor.G) / 3;
			return Colors.FromRgb(avg, avg, avg);
		}

		public static bool IsDarkForTheEye(this Color c) =>
			(c.GetByteRed() * 0.299) + (c.GetByteGreen() * 0.587) + (c.GetByteBlue() * 0.114) <= 186;

		public static bool IsDark(this Color c) => c.GetByteRed() + c.GetByteGreen() + c.GetByteBlue() <= 127 * 3;

		static byte ToByte(double input)
		{
			if (input < 0)
				return 0;
			if (input > 255)
				return 255;
			return (byte)Math.Round(input);
		}
	}
}