using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

using Brass3;
using Brass3.Attributes;
using Brass3.Plugins;

namespace Core.Listing {

	public class Html : IListingWriter {

		public void WriteListing(Compiler compiler, Stream stream) {
			TextWriter ListWriter = new StreamWriter(stream, Encoding.UTF8);

			ListWriter.WriteLine(Properties.Resources.HtmlListHeader);

			ListWriter.WriteLine("<table>");

			foreach (Compiler.SourceStatement Statement in compiler.Statements) {

				StringBuilder HighlightedCode = new StringBuilder(512);

				ListWriter.Write("<tr>");
				ListWriter.Write("<td class=\"ln\">" + Statement.LineNumber.ToString() + "</td>");
				ListWriter.Write("<td><pre class=\"source");
				if ((!Statement.CompilerWasOnBefore && !Statement.CompilerWasOnAfterwards)) ListWriter.Write(" disabled");
				ListWriter.Write("\">");

				
				KeyValuePair<TokenisedSource.Token.TokenTypes, string>[] Source = Statement.Source.OutermostTokenisedSource.GetTypesAndStrings();

				int MaxValue = Source.Length - 1;
				for (; MaxValue >= 0; MaxValue--) {
					if (!(
						Source[MaxValue].Key == TokenisedSource.Token.TokenTypes.WhiteSpace 
						|| (Source[MaxValue].Key == TokenisedSource.Token.TokenTypes.Seperator && Source[MaxValue].Value.Trim() == ""))) break;
				}

				for (int i = 0; i <= MaxValue; ++i) {
					KeyValuePair<TokenisedSource.Token.TokenTypes, string> T = Source[i];
					HighlightedCode.Append("<span class=\"" + T.Key.ToString().ToLowerInvariant() + "\">");
					HighlightedCode.Append(EscapeHtml(T.Value));
					HighlightedCode.Append("</span>");
				}
				ListWriter.Write(HighlightedCode.ToString());
				ListWriter.Write("</pre></td></tr>");

			}

			ListWriter.WriteLine("</table>");

			ListWriter.WriteLine(Properties.Resources.HtmlListFooter);

			ListWriter.Flush();
		}


		private static string EscapeHtml(string toEscape) {
			return toEscape
					.Replace("&", "&amp;")
					.Replace("<", "&lt;")
					.Replace(">", "&gt;");
		}

	}
}
