using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BeeDevelopment.Brass3;
using BeeDevelopment.Brass3.Plugins;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;
using BeeDevelopment.Brass3.Attributes;
using System.Diagnostics;

namespace Help {
	public partial class HelpViewer : UserControl {

		#region Constructor

		public HelpViewer() {
			InitializeComponent();
			this.IconImages.Images.Add(Properties.Resources.Icon_BookClosed);
			this.IconImages.Images.Add(Properties.Resources.Icon_BookOpen);
			this.IconImages.Images.Add(Properties.Resources.Icon_PageBlue);
			this.History = new LinkedList<TreeNode>();
			History.AddFirst(null as TreeNode);
			this.CurrentHistoryPosition = History.First;
		}

		#endregion

		#region Accessing Help

		private HelpProvider helpProvider;
		/// <summary>
		/// Gets or sets the help provider used by this viewer.
		/// </summary>
		public HelpProvider HelpProvider {
			get { return this.helpProvider; }
			set {
				this.helpProvider = value;


				this.Contents.Nodes.Clear();

				if (this.HelpProvider == null) return;

				List<Assembly> PluginCollections = new List<Assembly>();

				TreeNode GeneralHelp = GetHelpForPlugin<IPlugin>(this.helpProvider.Compiler.InvisiblePlugins, "Help", PluginCollections, DocumentationUsageAttribute.DocumentationType.DocumentationOnly);
				if (GeneralHelp != null) this.Contents.Nodes.Add(GeneralHelp);

				TreeNode Collections = new TreeNode("Collections");
				this.Contents.Nodes.Add(Collections);

				TreeNode Directives = GetHelpForPlugin<IDirective>(this.helpProvider.Compiler.Directives, "Directives", PluginCollections, DocumentationUsageAttribute.DocumentationType.FunctionalityAndDocumentation);
				if (Directives != null) this.Contents.Nodes.Add(Directives);

				TreeNode Functions = GetHelpForPlugin<IFunction>(this.helpProvider.Compiler.Functions, "Functions", PluginCollections, DocumentationUsageAttribute.DocumentationType.FunctionalityAndDocumentation);
				if (Functions != null) this.Contents.Nodes.Add(Functions);

				TreeNode Encoders = GetHelpForPlugin<IStringEncoder>(this.helpProvider.Compiler.StringEncoders, "String Encoding", PluginCollections, DocumentationUsageAttribute.DocumentationType.FunctionalityAndDocumentation);
				if (Encoders != null) this.Contents.Nodes.Add(Encoders);

				TreeNode NumberEncoders = GetHelpForPlugin<INumberEncoder>(this.helpProvider.Compiler.NumberEncoders, "Number Encoding", PluginCollections, DocumentationUsageAttribute.DocumentationType.FunctionalityAndDocumentation);
				if (NumberEncoders != null) this.Contents.Nodes.Add(NumberEncoders);

				TreeNode OutputWriters = GetHelpForPlugin<IOutputWriter>(this.helpProvider.Compiler.OutputWriters, "Output Writers", PluginCollections, DocumentationUsageAttribute.DocumentationType.FunctionalityAndDocumentation);
				if (OutputWriters != null) this.Contents.Nodes.Add(OutputWriters);

				TreeNode OutputModifiers = GetHelpForPlugin<IOutputModifier>(this.helpProvider.Compiler.OutputModifiers, "Output Modifiers", PluginCollections, DocumentationUsageAttribute.DocumentationType.FunctionalityAndDocumentation);
				if (OutputModifiers != null) this.Contents.Nodes.Add(OutputModifiers);

				TreeNode ListingWriters = GetHelpForPlugin<IListingWriter>(this.helpProvider.Compiler.ListingWriters, "Listing Writers", PluginCollections, DocumentationUsageAttribute.DocumentationType.FunctionalityAndDocumentation);
				if (ListingWriters != null) this.Contents.Nodes.Add(ListingWriters);

				TreeNode Assemblers = GetHelpForPlugin<IAssembler>(this.helpProvider.Compiler.Assemblers, "Assemblers", PluginCollections, DocumentationUsageAttribute.DocumentationType.FunctionalityAndDocumentation);
				if (Assemblers != null) this.Contents.Nodes.Add(Assemblers);

				TreeNode Other = GetHelpForPlugin<IPlugin>(this.helpProvider.Compiler.InvisiblePlugins, "Other Plugins", PluginCollections, DocumentationUsageAttribute.DocumentationType.FunctionalityAndDocumentation);
				if (Other != null) this.Contents.Nodes.Add(Other);

				List<TreeNode> CollectionNodes = new List<TreeNode>();
				foreach (Assembly A in PluginCollections) {
					string Name = A.GetName().Name;
					object[] o = A.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
					if (o.Length == 1) Name = (o[0] as AssemblyTitleAttribute).Title;
					TreeNode CollectionNode = new TreeNode(Name);
					CollectionNode.Tag = A;
					CollectionNode.ImageIndex = 2;
					CollectionNode.SelectedImageIndex = 2;
					CollectionNodes.Add(CollectionNode);
				}
				if (CollectionNodes.Count == 0) {
					Contents.Nodes.Remove(Collections);
				} else {
					CollectionNodes.Sort(delegate(TreeNode a, TreeNode b) { return a.Text.CompareTo(b.Text); });
					Collections.Nodes.AddRange(CollectionNodes.ToArray());
				}

				this.RepopulateIndex();
				this.SearchIndex("");


			}
		}

		private TreeNode GetHelpForPlugin<T>(IEnumerable<T> source, string sectionName, List<Assembly> assemblies, DocumentationUsageAttribute.DocumentationType documentationType) where T : IPlugin {

			Dictionary<string, List<TreeNode>> CategoryNodes = new Dictionary<string, List<TreeNode>>();

			Comparison<TreeNode> TreeNodeSorter = delegate(TreeNode a, TreeNode b) { return a.Text.CompareTo(b.Text); };


			foreach (T D in source) {

				Assembly A = D.GetType().Assembly;
				if (!assemblies.Contains(A)) assemblies.Add(A);

				object[] o;
				o = D.GetType().GetCustomAttributes(typeof(DocumentationUsageAttribute), false);
				if (o.Length == 1) {
					DocumentationUsageAttribute.DocumentationType DocumentationType = (o[0] as DocumentationUsageAttribute).Documentation;
					if (DocumentationType != documentationType) continue;
				} else {
					if (documentationType == DocumentationUsageAttribute.DocumentationType.DocumentationOnly) continue;
				}

				string Category = "";
				o = D.GetType().GetCustomAttributes(typeof(CategoryAttribute), false);
				if (o.Length == 1) {
					Category = (o[0] as CategoryAttribute).Category;
				}

				List<TreeNode> DirectiveNodes;
				if (!CategoryNodes.TryGetValue(Category, out DirectiveNodes)) {
					DirectiveNodes = new List<TreeNode>();
					CategoryNodes.Add(Category, DirectiveNodes);
				}

				TreeNode DirectiveNode = new TreeNode(Compiler.GetPluginDisplayName(D));
				DirectiveNode.Tag = D;
				DirectiveNode.ImageIndex = 2;
				DirectiveNode.SelectedImageIndex = 2;
				DirectiveNodes.Add(DirectiveNode);
			}

			if (CategoryNodes.Count > 0) {
				TreeNode Directives = new TreeNode(sectionName);

				Directives.ImageIndex = 0;
				Directives.SelectedImageIndex = 0;

				List<string> Categories = new List<string>(CategoryNodes.Keys);

				Categories.Sort(delegate(string a, string b) { // Custom sorter that sticks "" after everything.
					if (a == "" && b == "") {
						return 0;
					} else if (a == "") {
						return 1;
					} else if (b == "") {
						return -1;
					} else {
						return a.CompareTo(b);
					}
				});

				foreach (string CategoryName in Categories) {
					TreeNode Category;

					if (CategoryName != "") {
						Category = new TreeNode(CategoryName);
						Directives.Nodes.Add(Category);
						Category.ImageIndex = 0;
						Category.SelectedImageIndex = 0;
					} else {
						Category = Directives;
					}

					List<TreeNode> Subnodes = CategoryNodes[CategoryName];
					Subnodes.Sort(TreeNodeSorter);
					Category.Nodes.AddRange(Subnodes.ToArray());
				}

				return Directives;
			} else {
				return null;
			}

		}


		#endregion

		#region Index

		private Dictionary<string, TreeNode> Index;

		private void RepopulateIndex() {
			Dictionary<string, List<TreeNode>> NewIndex = new Dictionary<string, List<TreeNode>>();

			this.RepopulateIndex<IDirective>(this.HelpProvider.Compiler.Directives, NewIndex);
			this.RepopulateIndex<IFunction>(this.HelpProvider.Compiler.Functions, NewIndex);
			this.RepopulateIndex<IAssembler>(this.HelpProvider.Compiler.Assemblers, NewIndex);
			this.RepopulateIndex<IOutputWriter>(this.HelpProvider.Compiler.OutputWriters, NewIndex);
			this.RepopulateIndex<IOutputModifier>(this.HelpProvider.Compiler.OutputModifiers, NewIndex);
			this.RepopulateIndex<IStringEncoder>(this.HelpProvider.Compiler.StringEncoders, NewIndex);
			this.RepopulateIndex<INumberEncoder>(this.HelpProvider.Compiler.NumberEncoders, NewIndex);
			this.RepopulateIndex<IListingWriter>(this.HelpProvider.Compiler.ListingWriters, NewIndex);
			this.RepopulateIndex<IPlugin>(this.HelpProvider.Compiler.InvisiblePlugins, NewIndex);



			KeyValuePair<Type, string>[] PossibleTypes = new KeyValuePair<Type, string>[] { 
				new KeyValuePair<Type, string>(typeof(IDirective), "directive"),
				new KeyValuePair<Type, string>(typeof(IFunction), "function"),
				new KeyValuePair<Type, string>(typeof(IOutputWriter), "output writer"),
				new KeyValuePair<Type, string>(typeof(IOutputModifier), "output modifier"),
				new KeyValuePair<Type, string>(typeof(IStringEncoder), "string encoder"),
				new KeyValuePair<Type, string>(typeof(INumberEncoder), "number encoder"),
				new KeyValuePair<Type, string>(typeof(IAssembler), "assembler"),
				new KeyValuePair<Type, string>(typeof(IListingWriter), "listing writer")				
			};

			foreach (KeyValuePair<string, List<TreeNode>> CollapseLikeNames in NewIndex) {

				if (CollapseLikeNames.Value.Count == 1) continue;


				TreeNode N = new TreeNode(CollapseLikeNames.Value[0].Text);

				foreach (TreeNode Subsection in CollapseLikeNames.Value) {

					List<Type> PluginTypes = new List<Type>(Subsection.Tag.GetType().GetInterfaces());
					if (Subsection.Tag is Assembly) PluginTypes.Add(typeof(Assembly));

					List<string> Types = new List<string>();
					foreach (KeyValuePair<Type, string> CheckType in PossibleTypes) {
						if (PluginTypes.Contains(CheckType.Key)) {
							Types.Add(CheckType.Value);
						}
					}

					if (Types.Count > 0) {
						Subsection.Text += " " + string.Join("/", Types.ToArray());
					}

					N.Nodes.Add(Subsection);
				}

				CollapseLikeNames.Value.Clear();
				CollapseLikeNames.Value.Add(N);
			}


			List<string> SortedKeys = new List<string>(NewIndex.Keys);
			SortedKeys.Sort();

			this.Index = new Dictionary<string, TreeNode>(SortedKeys.Count);
			foreach (string Key in SortedKeys) {
				this.Index.Add(Key, NewIndex[Key][0]);				
			}

		}

		private void SearchIndex(string search) {

			List<TreeNode> Results = new List<TreeNode>(this.Index.Count);

			search = search.Trim().ToLowerInvariant();

			foreach (KeyValuePair<string, TreeNode> Searcher in this.Index) {
				if (Searcher.Key.StartsWith(search)) Results.Add(Searcher.Value);
			}

			this.IndexResults.Visible = false;
			this.IndexResults.Nodes.Clear();
			this.IndexResults.Nodes.AddRange(Results.ToArray());
			this.IndexResults.Visible = true;
			this.IndexResults.ExpandAll();
		}

		private void RepopulateIndex<T>(PluginCollection<T> plugins, Dictionary<string, List<TreeNode>> totalIndex) where T : class, IPlugin {

			foreach (T CurrentPlugin in plugins) {

				string[] IndexKeys = Compiler.GetPluginNames(CurrentPlugin);

				foreach (string IndexKeyOriginal in IndexKeys) {
					string IndexKey = IndexKeyOriginal.ToLowerInvariant();
					List<TreeNode> LN;
					if (!totalIndex.TryGetValue(IndexKey, out LN)) {
						LN = new List<TreeNode>();
						totalIndex.Add(IndexKey, LN);
					}

					bool AlreadyAdded =false;
					foreach (TreeNode N in LN) {
						if (N.Tag == CurrentPlugin) {
							AlreadyAdded = true;
							break;
						}
					}
					if (!AlreadyAdded) {
						TreeNode Node = new TreeNode(IndexKeyOriginal);
						Node.Tag = CurrentPlugin;
						LN.Add(Node);
					}
				}
			}
		}

		private void IndexResults_BeforeCollapse(object sender, TreeViewCancelEventArgs e) { e.Cancel = true; }


		private void IndexResults_AfterSelect(object sender, TreeViewEventArgs e) {
			SelectHelpEntry(e.Node, true);
		}

		private void IndexSearch_TextChanged(object sender, EventArgs e) {
			this.SearchIndex(IndexSearch.Text);
		}


		#endregion

		#region History Navigation

		private LinkedList<TreeNode> History;
		private LinkedListNode<TreeNode> CurrentHistoryPosition;

		private bool MovingThroughHistory = false;

		private void SetHistory(TreeNode helpTopic) {
			if (MovingThroughHistory) return;
			while (CurrentHistoryPosition.Next != null) {
				History.Remove(CurrentHistoryPosition.Next);
			}
			History.AddAfter(CurrentHistoryPosition, helpTopic);
			CurrentHistoryPosition = CurrentHistoryPosition.Next;
		}

		public bool CanGoBack {
			get { return this.CurrentHistoryPosition.Previous != null && this.CurrentHistoryPosition.Previous.Value != null; }
		}
		public bool CanGoForwards {
			get { return this.CurrentHistoryPosition.Next != null; }
		}
		public void GoBack() {
			if (this.CanGoBack) {
				try {
					MovingThroughHistory = true;
					CurrentHistoryPosition = CurrentHistoryPosition.Previous;
					this.Contents.SelectedNode = CurrentHistoryPosition.Value;
				} finally {
					MovingThroughHistory = false;
				}
			}
		}
		public void GoForwards() {
			if (this.CanGoForwards) {
				try {
					MovingThroughHistory = true;
					CurrentHistoryPosition = CurrentHistoryPosition.Next;
					this.Contents.SelectedNode = CurrentHistoryPosition.Value;
				} finally {
					MovingThroughHistory = false;
				}
			}
		}

		private TreeNode FindMatchingNodeFromContents(TreeNodeCollection root, TreeNode node) {
			foreach (TreeNode N in root) {
				if (N.Tag == node.Tag) return N;
				TreeNode Found = FindMatchingNodeFromContents(N.Nodes, node);
				if (Found != null) return Found;
			}
			return null;


		}

		private void SelectHelpEntry(TreeNode node, bool fromIndex) {
			if (node == null) return;
			if (node.Tag == null) return;

			if (fromIndex) {
				node = FindMatchingNodeFromContents(Contents.Nodes, node);
			}

			string Help = null;

			IPlugin P = node.Tag as IPlugin;
			if (P != null) {
				this.SetHistory(node);
				Help = this.HelpProvider.GetHelpHtml(P, false);
			} else {
				Assembly PluginCollection = node.Tag as Assembly;
				if (PluginCollection != null) {
					this.SetHistory(node);
					Help = this.HelpProvider.GetHelpHtml(PluginCollection, false);
				}
			}
			if (Help != null) {
				this.Viewer.DocumentText = Help;
			}
			this.Viewer.Document.MouseDown += new HtmlElementEventHandler(Document_MouseDown);
		}

		#endregion

		#region Misc

		private void Contents_AfterSelect(object sender, TreeViewEventArgs e) {
			SelectHelpEntry(e.Node, false);
		}



		private void Contents_AfterExpand(object sender, TreeViewEventArgs e) {
			if (e.Node != null && e.Node.ImageIndex < 2) {
				e.Node.ImageIndex = 1;
				e.Node.SelectedImageIndex = 1;
			}
		}

		private void Contents_AfterCollapse(object sender, TreeViewEventArgs e) {
			if (e.Node != null && e.Node.ImageIndex < 2) {
				e.Node.ImageIndex = 0;
				e.Node.SelectedImageIndex = 0;
			}
		}

		private TreeNode GetCollectionNode(TreeNodeCollection root, Guid guid) {
			foreach (TreeNode N in root) {
				if (N.Tag != null && N.Tag.GetType() == typeof(Assembly)) {
					if (new Guid((((N.Tag as Assembly).GetCustomAttributes(typeof(GuidAttribute), false)[0]) as GuidAttribute).Value) == guid) return N;
				}
				if (N.Nodes.Count > 0) {
					TreeNode N2 = GetCollectionNode(N.Nodes, guid);
					if (N2 != null) return N2;
				}
			}
			return null;
		}

		private void Viewer_Navigating(object sender, WebBrowserNavigatingEventArgs e) {
			if (e.Url.AbsolutePath.StartsWith("brass_help")) {
				string Action = e.Url.AbsolutePath.Substring(11).Split('=')[0];
				string Arguments = e.Url.AbsolutePath.Substring(12 + Action.Length);
				e.Cancel = true; // Cancel navigation.
				IPlugin Plugin = this.HelpProvider.Compiler.GetPluginInstanceFromGuid(new Guid(Arguments));
				if (Plugin != null) {
					this.SelectedPlugin = Plugin;
				} else {
					TreeNode N = this.GetCollectionNode(this.Contents.Nodes, new Guid(Arguments));
					if (N != null) this.Contents.SelectedNode = N;
				}
			} else if (e.Url.Scheme == "http") {
				Process.Start(e.Url.AbsoluteUri);
			}
		}

		private IPlugin SelectedPlugin {
			get {
				if (this.Contents.SelectedNode != null && this.Contents.SelectedNode.Tag != null) return this.Contents.SelectedNode.Tag as IPlugin;
				return null;
			}
			set {
				TreeNode N = SearchForPlugin(value, null);
				if (N != null) {
					this.Contents.SelectedNode = N;
				}
			}
		}

		private TreeNode SearchForPlugin(IPlugin plugin, TreeNodeCollection start) {
			if (start == null) start = this.Contents.Nodes;
			foreach (TreeNode N in start) {
				// Check my node:
				if (N.Tag != null && N.Tag as IPlugin == plugin) return N;

				// Check subnodes:
				TreeNode TryLowerNode = SearchForPlugin(plugin, N.Nodes);
				if (TryLowerNode != null) return TryLowerNode;

			}
			return null;
		}

		#endregion

		#region Printing


		public void Print() {
			this.Viewer.Print();
		}

		public void ShowPrintPreviewDialog() {
			this.Viewer.ShowPrintPreviewDialog();
		}

		public void ShowPrintDialog() {
			this.Viewer.ShowPrintDialog();
		}

		#endregion

		#region Copying

		private HtmlElement LastSelectedElement;
		void Document_MouseDown(object sender, HtmlElementEventArgs e) {
			LastSelectedElement = this.Viewer.Document.GetElementFromPoint(e.MousePosition);
			while (LastSelectedElement != null && LastSelectedElement.TagName.ToLowerInvariant() != "pre") {
				LastSelectedElement = LastSelectedElement.Parent;
			}
		}

		private void ViewerContext_Opening(object sender, CancelEventArgs e) {
			ViewerContextCopy.Enabled = ExampleWasClicked;
		}

		private bool ExampleWasClicked {
			get {
				return LastSelectedElement != null && LastSelectedElement.TagName.ToLowerInvariant() == "pre" && LastSelectedElement.GetAttribute("className").Contains("example");
			}
		}

		private void ViewerContextCopy_Click(object sender, EventArgs e) {
			if (ExampleWasClicked) {
				Clipboard.SetText(Encoding.Unicode.GetString(Convert.FromBase64String(LastSelectedElement.GetAttribute("base64code"))));
			}
		}

		#endregion

		#region Exporting

		public void ExportWebsite(string indexFile) {
			string BasePath = Path.GetDirectoryName(indexFile);
			File.WriteAllText(indexFile, Properties.Resources.FramesetHtml);
			Directory.CreateDirectory(Path.Combine(BasePath, "help"));
			StringBuilder Contents = new StringBuilder(1024);
			Contents.Append(Properties.Resources.ContentsHtml);
			WebsiteWriteTreeNodes(Contents, this.Contents.Nodes, (Path.Combine(BasePath, "help")));
			Contents.Append("</body></html>");
			File.WriteAllText(Path.Combine(BasePath, Path.Combine("help", "contents.html")), Contents.ToString());
			Properties.Resources.Icon_Error.Save(Path.Combine(BasePath, Path.Combine("help", "icon_error.png")));
			Properties.Resources.Icon_BookOpen.Save(Path.Combine(BasePath, Path.Combine("help", "icon_book_open.png")));
			Properties.Resources.Icon_BookClosed.Save(Path.Combine(BasePath, Path.Combine("help", "icon_book_closed.png")));
			Properties.Resources.Icon_PageBlue.Save(Path.Combine(BasePath, Path.Combine("help", "icon_page.png")));
		}


		private void WebsiteWriteTreeNodes(StringBuilder contents, TreeNodeCollection nodes, string baseDirectory) {
			contents.Append("<ul>");
			foreach (TreeNode N in nodes) {
				contents.Append("<li class=\"");
				contents.Append(N.Tag == null ? "section" : "topic");
				contents.Append("\"");
				string Guid = N.Tag == null ? "" : N.Tag.GetType().GUID.ToString();
				if (N.Tag != null) {
					if (N.Tag as Assembly != null) {
						Guid = ((N.Tag as Assembly).GetCustomAttributes(typeof(GuidAttribute), false)[0] as GuidAttribute).Value;
					}
					contents.Append(" id=\"" + Guid + "\"");
					contents.Append("><a href=\"" + Guid + ".html\" target=\"viewer\">");
				} else {
					contents.Append(">");
				}
				contents.Append("<span class=\"section\">" + N.Text + "</span>");
				if (N.Tag != null) {
					string HelpHtml = "";

					if (N.Tag as IPlugin != null) {
						HelpHtml = this.HelpProvider.GetHelpHtml(N.Tag as IPlugin, true);
					} else if (N.Tag as Assembly != null) {
						HelpHtml = this.HelpProvider.GetHelpHtml(N.Tag as Assembly, true);
					}

					File.WriteAllText(Path.Combine(baseDirectory, Guid + ".html"), HelpHtml);
				}
				if (N.Nodes.Count > 0) WebsiteWriteTreeNodes(contents, N.Nodes, baseDirectory);
				if (N.Tag != null) contents.Append("</a>");
				contents.Append("</li>");
			}
			contents.Append("</ul>");
		}

		public void ExportLatenite1Xml(string lateniteDirectory) {
			Dictionary<Assembly, XmlWriter> Writers = new Dictionary<Assembly, XmlWriter>();
			List<IPlugin> Exported = new List<IPlugin>();
			Latenite1XmlWriteTreeNodes(this.Contents.Nodes, Writers, lateniteDirectory, Exported);
			foreach (KeyValuePair<Assembly, XmlWriter> KVP in Writers) {
				KVP.Value.WriteEndElement();
				KVP.Value.Flush();
				KVP.Value.Close();
			}
		}

		private void Latenite1XmlWriteTreeNodes(TreeNodeCollection nodes, Dictionary<Assembly, XmlWriter> writers, string baseDirectory, List<IPlugin> exported) {

			bool forExporting = true;

			foreach (TreeNode N in nodes) {
				IPlugin Plugin;
				if (N.Tag != null && (Plugin = N.Tag as IPlugin) != null && !exported.Contains(Plugin)) {
					exported.Add(Plugin);
					XmlWriter Writer;
					if (!writers.TryGetValue(Plugin.GetType().Assembly, out Writer)) {
						Writer = XmlWriter.Create(Path.Combine(baseDirectory, "Brass3." + Path.GetFileNameWithoutExtension(Plugin.GetType().Assembly.Location) + ".xml"));
						writers.Add(Plugin.GetType().Assembly, Writer);
						Writer.WriteStartElement("helpfile");

						string CollectionName = Plugin.GetType().Assembly.GetName().Name;
						object[] CollectionTitle = Plugin.GetType().Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
						if (CollectionTitle.Length > 0) CollectionName = (CollectionTitle[0] as AssemblyTitleAttribute).Title;

						Writer.WriteAttributeString("name", CollectionName);
					}

					Writer.WriteStartElement("item");

					string[] Names = Compiler.GetPluginNames(Plugin);

					Writer.WriteAttributeString("name", string.Join("/", Names));

					List<string> Highlights = new List<string>();

					if (Plugin as IFunction != null) {
						Highlights.AddRange(Names);
					}

					if (Plugin as IDirective != null) {
						foreach (string s in Names) {
							Highlights.Add("." + s);
							Highlights.Add("#" + s);
						}
					}

					if (Highlights.Count != 0) {
						Writer.WriteAttributeString("highlight", string.Join("/", Highlights.ToArray()));
						Writer.WriteAttributeString("colour", Plugin as IDirective == null ? "routine" : "directive");
					}

					DescriptionAttribute DA = HelpProvider.GetCustomAttribute<DescriptionAttribute>(Plugin);
					if (DA != null) Writer.WriteAttributeString("description", HelpProvider.DocumentationToHtml(DA.Description, true));

					SyntaxAttribute[] SA = HelpProvider.GetCustomAttributes<SyntaxAttribute>(Plugin);
					if (SA.Length > 0) {
						Writer.WriteAttributeString("syntax", string.Join(string.Format("{0}{1}{0}{1}", "<br />", Environment.NewLine), Array.ConvertAll<SyntaxAttribute, string>(SA, delegate(SyntaxAttribute S) {
							return HelpProvider.DocumentationToHtml(S.Syntax, forExporting);
						})));
					}

					foreach (WarningAttribute Warning in HelpProvider.GetCustomAttributes<WarningAttribute>(Plugin)) {
						Writer.WriteStartElement("note");
						Writer.WriteAttributeString("description", HelpProvider.DocumentationToHtml(Warning.Warning, forExporting));
						Writer.WriteEndElement();
					}

					foreach (RemarksAttribute Remarks in HelpProvider.GetCustomAttributes<RemarksAttribute>(Plugin)) {
						Writer.WriteStartElement("note");
						Writer.WriteAttributeString("description", HelpProvider.DocumentationToHtml(Remarks.Remarks, forExporting));
						Writer.WriteEndElement();
					}

					foreach (CodeExampleAttribute CEO in HelpProvider.GetCustomAttributes<CodeExampleAttribute>(Plugin)) {
						Writer.WriteStartElement("example");
						string Code = CEO.Example;
						Writer.WriteAttributeString("code", this.HelpProvider.ExpandTabs(HelpProvider.DocumentationToHtml(Code, false)));
						Writer.WriteEndElement();
					}

					Writer.WriteEndElement();


				}
				Latenite1XmlWriteTreeNodes(N.Nodes, writers, baseDirectory, exported);
			}

		}



		#endregion

	
	}
}
