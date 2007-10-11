using System;
using System.Collections.Generic;
using System.Text;
using Brass3;
using Brass3.Plugins;
using Brass3.Attributes;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Help {
	public class HelpProvider : IDisposable {

		private int tabWidth = 4;
		public int TabWidth {
			get { return this.tabWidth; }
			set { this.tabWidth = value; }
		}


		private Dictionary<Image, string> ImagePaths;
		public string GetImagePath(Image img, string name, bool forExporting) {
			lock (ImagePaths) {
				string Output;
				if (!ImagePaths.TryGetValue(img, out Output)) {
					if (forExporting) {
						Output = name;
					} else {
						Output = Path.GetTempFileName() + "." + name;
						img.Save(Output, ImageFormat.Png);
						Output = "file:///" + Output;
						ImagePaths.Add(img, Output);
					}
					
				}
				return Output;
			}
		}

		private readonly Compiler compiler;
		/// <summary>
		/// Gets the compiler used to get help for.
		/// </summary>
		public Compiler Compiler {
			get { return this.compiler; }
		}


		public HelpProvider(Compiler compiler) {
			this.compiler = compiler;
			this.ImagePaths = new Dictionary<Image, string>();

		}

		/// <summary>
		/// Get help in HTML format from a plugin.
		/// </summary>
		/// <param name="plugin">The plugin to get help from.</param>
		public string GetHelpHtml(IPlugin plugin, bool forExporting) {
			StringBuilder HelpFile = new StringBuilder(1024);

			Type T = plugin.GetType();
			

			List<Type> ImplementedInterfaces = new List<Type>(T.GetInterfaces());

			string Title = plugin.Name;

			if (ImplementedInterfaces.Contains(typeof(IDirective))) {
				IDirective Directive = plugin as IDirective;
				Title = string.Join("/", Directive.Names);
			}

			string CollectionName = T.Assembly.GetName().Name;
			object[] CollectionTitle = T.Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
			if (CollectionTitle.Length == 1) {
				CollectionName = (CollectionTitle[0] as AssemblyTitleAttribute).Title;
			}

			HelpFile.Append("<!doctype><html><head><style>" + Properties.Resources.ViewerCss.Replace("WarningImgUrl", GetImagePath(Properties.Resources.Icon_Error, "icon_error.png", forExporting)) + "</style></head><body>");
			HelpFile.Append("<div class=\"header\"><p class=\"plugincollection\">" + EscapeHtml(CollectionName) + "</p><h1>" + EscapeHtml(Title) + "</h1></div><div class=\"content\">");
			int HelpFileLength = HelpFile.Length;

			// Description:
			foreach (object o in T.GetCustomAttributes(typeof(DescriptionAttribute), false)) {
				DescriptionAttribute D = (o as DescriptionAttribute);
				if (D != null) {
					HelpFile.Append("<div class=\"description\">" + NewLinesToParagraphs(EscapeHtml(D.Description)) + "</div>");
				}
			}


			// Syntax:
			object[] SyntaxAttributes = T.GetCustomAttributes(typeof(SyntaxAttribute), false);
			if (SyntaxAttributes.Length > 0) {
				HelpFile.AppendLine("<h2>Syntax</h2>");
				foreach (object o in SyntaxAttributes) {
					SyntaxAttribute S = (o as SyntaxAttribute);
					if (S != null) {
						HelpFile.AppendLine("<pre class=\"syntax\">" + EscapeHtml(S.Syntax) + "</pre>");
					}
				}
			}

			// Remarks:
			object[] RemarksAttributes = T.GetCustomAttributes(typeof(RemarksAttribute), false);
			if (RemarksAttributes.Length > 0) {
				HelpFile.AppendLine("<h2>Remarks</h2>");
				foreach (object o in RemarksAttributes) {
					RemarksAttribute R = (o as RemarksAttribute);
					if (R != null) {
						HelpFile.Append("<div class=\"remarks\">" + NewLinesToParagraphs(EscapeHtml(R.Remarks)) + "</div>");
					}
				}
			}

			// Warning:
			foreach (object o in T.GetCustomAttributes(typeof(WarningAttribute), false)) {
				WarningAttribute W = (o as WarningAttribute);
				if (W != null) {
					HelpFile.Append("<h2 class=\"warning\">Warning</h3><div class=\"warning\">" + NewLinesToParagraphs(EscapeHtml(W.Warning)) + "</div>");
				}
			}

			// Example:
			object[] ExampleAttributes = T.GetCustomAttributes(typeof(CodeExampleAttribute), false);
			if (ExampleAttributes.Length > 0) {
				HelpFile.AppendLine("<h2>Example" + (ExampleAttributes.Length == 1 ? "" : "s") + "</h2>");
				foreach (object o in ExampleAttributes) {
					CodeExampleAttribute C = (o as CodeExampleAttribute);
					if (C != null && C.Example != null) {
						if (C.Caption != null && !string.IsNullOrEmpty(C.Caption.Trim())) {
							HelpFile.Append("<h3 class=\"example\">" + EscapeHtml(C.Caption) + "</h3>");
						}
						TokenisedSource[] CompiledExample = TokenisedSource.FromString(this.Compiler, this.ExpandTabs(C.Example.Replace("\r\n", "\n")));
						StringBuilder OutputExample = new StringBuilder(C.Example.Length);
						foreach (TokenisedSource TS in CompiledExample) {
							foreach (TokenisedSource.Token Token in TS.Tokens) {
								bool IsLinked = false;
								string Link = "";
								switch (Token.Type) {
									case TokenisedSource.Token.TokenTypes.Function:
										IsLinked = this.Compiler.Functions.PluginExists(Token.Name);
										if (IsLinked) Link = GetSeeAlsoUrl(Compiler.Functions[Token.Name], forExporting);
										break;
									case TokenisedSource.Token.TokenTypes.Directive:
										IsLinked = this.Compiler.Directives.PluginExists(Token.Name);
										if (IsLinked) Link = GetSeeAlsoUrl(Compiler.Directives[Token.Name], forExporting);
										break;
								}
								if (IsLinked) OutputExample.Append("<a href=\"" + Link + "\">");
								OutputExample.Append("<span class=\"" + Token.Type.ToString().ToLowerInvariant() + "\">");
								OutputExample.Append(EscapeHtml(Token.Data));
								OutputExample.Append("</span>");
								if (IsLinked) OutputExample.Append("</a>");
							}
						}
						HelpFile.Append("<pre class=\"example\"");
						if (!forExporting) HelpFile.Append(" base64code=\"" + Convert.ToBase64String(Encoding.Unicode.GetBytes(C.Example)) + "\"");
						HelpFile.Append(">" + OutputExample.ToString() + "</pre>");
					}
				}
			}

			// See also:
			object[] SeeAlsoAttributes = T.GetCustomAttributes(typeof(SeeAlsoAttribute), false);
			if (SeeAlsoAttributes.Length > 0) {
				HelpFile.AppendLine("<h2>See Also</h2><ul class=\"seealso\">");
				List<IPlugin> SeeAlsoPlugins = new List<IPlugin>();
				foreach (object o in SeeAlsoAttributes) {
					SeeAlsoAttribute S = (o as SeeAlsoAttribute);
					if (S != null) {
						IPlugin Plugin = this.Compiler.GetPluginInstanceFromType(S.TypeToSeeAlso);
						if (Plugin != null) SeeAlsoPlugins.Add(Plugin);
					}
				}

				SeeAlsoPlugins.Sort(delegate(IPlugin a, IPlugin b) { return a.Name.CompareTo(b.Name); });
				foreach (IPlugin P in SeeAlsoPlugins) {
					HelpFile.AppendLine("<li><a href=\"" + GetSeeAlsoUrl(P, forExporting) + "\">" + EscapeHtml(P.Name) + "</a></li>");
				}
				

				HelpFile.AppendLine("</ul>");
			}


			if (HelpFile.Length == HelpFileLength) {
				HelpFile.Append("<p class=\"undocumented\">[There is currently no documentation for this plugin].</p>");
			}

			HelpFile.Append("</div></body></html>");

			return HelpFile.ToString();

		}


		public string GetHelpHtml(Assembly pluginCollection, bool forExporting) {
			StringBuilder HelpFile = new StringBuilder(1024);


			string CollectionName = pluginCollection.GetName().Name;
			object[] CollectionTitle = pluginCollection.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
			if (CollectionTitle.Length == 1) {
				CollectionName = (CollectionTitle[0] as AssemblyTitleAttribute).Title;
			}

			HelpFile.Append("<!doctype><html><head><style>" + Properties.Resources.ViewerCss.Replace("WarningImgUrl", GetImagePath(Properties.Resources.Icon_Error, "icon_error.png", forExporting)) + "</style></head><body>");
			HelpFile.Append("<div class=\"header\"><p class=\"plugincollection\">" + EscapeHtml(CollectionName) + "</p><h1>" + EscapeHtml(CollectionName) + "</h1></div><div class=\"content\">");

			int HelpFileLength = HelpFile.Length;

			HelpFile.Append(GetPluginList<IDirective>(pluginCollection, compiler.Directives, "Directives", forExporting));
			HelpFile.Append(GetPluginList<IFunction>(pluginCollection, compiler.Functions, "Functions", forExporting));
			HelpFile.Append(GetPluginList<IStringEncoder>(pluginCollection, compiler.StringEncoders, "String Encoding", forExporting));
			HelpFile.Append(GetPluginList<INumberEncoder>(pluginCollection, compiler.NumberEncoders, "Number Encoding", forExporting));
			HelpFile.Append(GetPluginList<IOutputWriter>(pluginCollection, compiler.OutputWriters, "Output Writers", forExporting));
			HelpFile.Append(GetPluginList<IOutputModifier>(pluginCollection, compiler.OutputModifiers, "Output Modifiers", forExporting));
			HelpFile.Append(GetPluginList<IAssembler>(pluginCollection, compiler.Assemblers, "Assemblers", forExporting));


			if (HelpFile.Length == HelpFileLength) {
				HelpFile.Append("<p class=\"undocumented\">[There is currently no documentation for this plugin].</p>");
			}

			HelpFile.Append("</div></body></html>");

			return HelpFile.ToString();

		}

		private string GetPluginList<T>(Assembly collection, NamedPluginCollection<T> source, string name, bool forExporting) where T : IPlugin {
			StringBuilder Result = new StringBuilder(1024);
			
			List<T> Matches = new List<T>();
			foreach (T Plugin in source) {
				if (Plugin.GetType().Assembly == collection) {
					Matches.Add(Plugin);
				}
			}
			Matches.Sort(delegate(T a, T b) { return a.Name.CompareTo(b.Name); });

			if (Matches.Count > 0) {
				Result.Append("<h2>" + name + "</h2>");
				Result.Append("<div><table>");
				foreach (T Plugin in Matches) {
					Result.Append("<tr>");
					Result.Append("<th style=\"width: 100px;\"><a href=\"" + GetSeeAlsoUrl(Plugin, forExporting) + "\">" + SomethingOrNbsp(EscapeHtml(Plugin.Name)) + "</a></th><td>");
					
					string Description = "";
					object[] DescriptionAttributes = Plugin.GetType().GetCustomAttributes(typeof(DescriptionAttribute), false);
					if (DescriptionAttributes.Length == 1) {
						Description = (DescriptionAttributes[0] as DescriptionAttribute).Description;
					}
					Result.Append(NewLinesToParagraphs(SomethingOrNbsp(EscapeHtml(Description))));


					Result.Append("</td></tr>");
				}
				Result.Append("</table></div>");
			}

			return Result.ToString();
			
		}

		private static string GetSeeAlsoUrl(IPlugin type, bool forExporting) {
			if (forExporting) {
				return type.GetType().GUID + ".html";
			} else {
				return "brass_help_type=" + type.GetType().GUID;				
			}
		}
		private static string SomethingOrNbsp(string source) {
			return (string.IsNullOrEmpty((source ?? "").Trim())) ? "&nbsp;" : source;
		}
		private static string NewLinesToParagraphs(string toEscape) {
			StringBuilder Result = new StringBuilder(toEscape.Length * 2);
			foreach (string s in toEscape.Split('\n')) {
				Result.Append("<p>" + s + "</p>");
			}
			return Result.ToString();
		}

		private string ExpandTabs(string toExpand) {
			StringBuilder Result = new StringBuilder(toExpand.Length * 2);
			foreach (string s in toExpand.Split(new char[] { '\n' }, StringSplitOptions.None)) {
				int CurrentPosition = 0;
				for (int i = 0; i < s.Length; ++i) {
					switch (s[i]) {
						case '\t':
							int Target = ((CurrentPosition + this.TabWidth) / this.TabWidth) * this.TabWidth;
							for (int j = CurrentPosition; j < Target; ++j, ++CurrentPosition) {
								Result.Append(' ');
							}
							break;
						default:
							Result.Append(s[i]);
							++CurrentPosition;
							break;
					}
				}
				Result.Append('\n');
			}
			return Result.ToString();
		}

		private static string EscapeHtml(string toEscape) {
			return toEscape
					.Replace("&", "&amp;")
					.Replace("<", "&lt;")
					.Replace(">", "&gt;")
					.Replace("&lt;c&gt;", "<tt class=\"code\">")
					.Replace("&lt;/c&gt;", "</tt>")
					.Replace("&lt;para&gt;", "<p>")
					.Replace("&lt;/para&gt;", "</p>")
					.Replace("&lt;param&gt;", "<span class=\"param\">")
					.Replace("&lt;/param&gt;", "</span>");
		}



		public void Dispose() {
			foreach (string s in ImagePaths.Values) {
				try {
					File.Delete(s);
				} catch { }
			}
		}
	}
}
