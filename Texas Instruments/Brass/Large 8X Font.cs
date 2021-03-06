using System;
using System.Collections.Generic;
using System.Text;
using BeeDevelopment.Brass3.Plugins;
using System.ComponentModel;

namespace TexasInstruments.Brass {

	[Description("Provides a string encoder for the TI-83+ OS large font.")]
	public class Large8X : IStringEncoder {

		private const string EncodingList =
			".....▸.." + // 00..07 
			"∫×\n..\r.."+// 08..0F 
			"√.².°..≤" + // 10..17 
			"≠≥..→.↑↓" + // 18..1F 
			" !\"#.%&'"+ // 20..27 
			"()*+,-./" + // 28..2F 
			"01234567" + // 30..37 
			"89:;<=>?" + // 38..3F 
			"@ABCDEFG" + // 40..47 
			"HIJKLMNO" + // 48..4F 
			"PQRSTUVW" + // 50..57 
			"XYZθ\\]^_"+ // 58..5F 
			"`abcdefg" + // 60..67 
			"hijklmno" + // 68..6F 
			"pqrstuvw" + // 70..77 
			"xyz{|}~." + // 78..7F 
			"₀₁₂₃₄₅₆₇" + // 80..87 
			"₈₉ÁÀÂÄáà" + // 88..8F 
			"âäÉÈÊËéè" + // 90..97 
			"êëÍÌÎÏíì" + // 98..9F 
			"îïÓÒÔÖóò" + // A0..A7 
			"ôöÚÙÛÜúù" + // A8..AF 
			"ûüÇçÑñ´`" + // B0..B7 
			"¨¿¡αβγΔδ" + // B8..BF 
			"ε[λμπρΣσ" + // C0..C7 
			"τφΩ...…◀" + // C8..CF 
			"..‐²°³.." + // D0..D7 
			".χ....⦆→" + // D8..DF 
			"........" + // E0..E7 
			".......↑" + // E8..EF 
			"↓.$.ß..." + // F0..F7 
			"........";  // F8..FF 

		public byte[] GetData(string toEncode) {
			byte[] Result = new byte[toEncode.Length];
			for (int i = 0; i < toEncode.Length; ++i) {
				if (toEncode[i] == '.') {
					Result[i] = 0x2E;
				} else {
					int j = EncodingList.IndexOf(toEncode[i]);
					Result[i] = (byte)(j == -1 ? 0x3F : j);
				}
				
			}
			return Result;
		}
		
		public string GetString(byte[] data) {
			StringBuilder Result = new StringBuilder(data.Length);
			for (int i = 0; i < data.Length; ++i) Result[i] = GetChar(data[i]);
			return Result.ToString();
		}

		public char GetChar(int value) {
			if (value == 0x2E) return '.';
			if (value < 0 || value > 255) return '?';
			char Return = EncodingList[value];
			return Return == '.' ? '?' : Return;
		}
	}
}
