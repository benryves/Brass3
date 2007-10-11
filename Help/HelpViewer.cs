using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Brass3;
using Brass3.Plugins;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Help {
	public partial class HelpViewer : UserControl {

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

				TreeNode Collections = new TreeNode("Collections");
				this.Contents.Nodes.Add(Collections);

				TreeNode Directives = GetHelpForPlugin<IDirective>(this.helpProvider.Compiler.Directives, "Directives", PluginCollections);
				if (Directives != null) this.Contents.Nodes.Add(Directives);

				TreeNode Functions = GetHelpForPlugin<IFunction>(this.helpProvider.Compiler.Functions, "Functions", PluginCollections);
				if (Functions != null) this.Contents.Nodes.Add(Functions);

				TreeNode Encoders = GetHelpForPlugin<IStringEncoder>(this.helpProvider.Compiler.StringEncoders, "String Encoding", PluginCollections);
				if (Encoders != null) this.Contents.Nodes.Add(Encoders);

				TreeNode NumberEncoders = GetHelpForPlugin<INumberEncoder>(this.helpProvider.Compiler.NumberEncoders, "Number Encoding", PluginCollections);
				if (NumberEncoders != null) this.Contents.Nodes.Add(NumberEncoders);

				TreeNode OutputWriters = GetHelpForPlugin<IOutputWriter>(this.helpProvider.Compiler.OutputWriters, "Output Writers", PluginCollections);
				if (OutputWriters != null) this.Contents.Nodes.Add(OutputWriters);

				TreeNode OutputModifiers = GetHelpForPlugin<IOutputModifier>(this.helpProvider.Compiler.OutputModifiers, "Output Modifiers", PluginCollections);
				if (OutputModifiers != null) this.Contents.Nodes.Add(OutputModifiers);

				TreeNode Assemblers = GetHelpForPlugin<IAssembler>(this.helpProvider.Compiler.Assemblers, "Assemblers", PluginCollections);
				if (Assemblers != null) this.Contents.Nodes.Add(Assemblers);

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
				

			}
		}

		private TreeNode GetHelpForPlugin<T>(IEnumerable<T> source, string sectionName, List<Assembly> assemblies) where T : IPlugin {

			Dictionary<string, List<TreeNode>> CategoryNodes = new Dictionary<string, List<TreeNode>>();

			Comparison<TreeNode> TreeNodeSorter = delegate(TreeNode a, TreeNode b) { return a.Text.CompareTo(b.Text); };

			
			foreach (T D in source) {

				Assembly A = D.GetType().Assembly;
				if (!assemblies.Contains(A)) assemblies.Add(A);

				string Category = "";
				object[] o = D.GetType().GetCustomAttributes(typeof(CategoryAttribute), false);
				if (o.Length == 1) {
					Category = (o[0] as CategoryAttribute).Category;
				}

				List<TreeNode> DirectiveNodes;
				if (!CategoryNodes.TryGetValue(Category, out DirectiveNodes)) {
					DirectiveNodes = new List<TreeNode>();
					CategoryNodes.Add(Category, DirectiveNodes);
				}

				string[] Names = new string[] { D.Name };
				IAliasedPlugin AliasedNames = D as IAliasedPlugin;
				if (AliasedNames != null) Names = AliasedNames.Names;

				foreach (string DirectiveName in Names) {
					TreeNode DirectiveNode = new TreeNode(DirectiveName);
					DirectiveNode.Tag = D;
					DirectiveNode.ImageIndex = 2;
					DirectiveNode.SelectedImageIndex = 2;
					DirectiveNodes.Add(DirectiveNode);
				}
			}

			if (CategoryNodes.Count > 0) {
				TreeNode Directives = new TreeNode(sectionName);
				
				Directives.ImageIndex = 0;
				Directives.SelectedImageIndex = 0;

				List<string> Categories = new List<string>(CategoryNodes.Keys);
				
				Categories.Sort(delegate(string a, string b) { // Custom sorter that sticks "" after everything.
					if (a == "" && b == "") {
						return 0;
					} else if (a =="") {
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

		public HelpViewer() {
			InitializeComponent();
			this.IconImages.Images.Add(Properties.Resources.Icon_BookClosed);
			this.IconImages.Images.Add(Properties.Resources.Icon_BookOpen);
			this.IconImages.Images.Add(Properties.Resources.Icon_PageBlue);
			this.History = new LinkedList<TreeNode>();
			History.AddFirst(null as TreeNode);
			this.CurrentHistoryPosition = History.First;
		}


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

		private void Contents_AfterSelect(object sender, TreeViewEventArgs e) {
			if (e.Node == null) return;
			if (e.Node.Tag == null) return;
			IPlugin P = e.Node.Tag as IPlugin;
			if (P != null) {
				this.SetHistory(e.Node);
				this.Viewer.DocumentText = this.HelpProvider.GetHelpHtml(P, false);
			}
			Assembly PluginCollection = e.Node.Tag as Assembly;
			if (PluginCollection != null) {
				this.SetHistory(e.Node);
				this.Viewer.DocumentText = this.HelpProvider.GetHelpHtml(PluginCollection, false);
			}

			this.Viewer.Document.MouseDown += new HtmlElementEventHandler(Document_MouseDown);
		}

		private HtmlElement LastSelectedElement;
		void Document_MouseDown(object sender, HtmlElementEventArgs e) {
			LastSelectedElement = this.Viewer.Document.GetElementFromPoint(e.MousePosition);
			while (LastSelectedElement != null && LastSelectedElement.TagName.ToLowerInvariant() != "pre") {
				LastSelectedElement = LastSelectedElement.Parent;
			}
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

		private void Viewer_Navigating(object sender, WebBrowserNavigatingEventArgs e) {
			if (e.Url.AbsolutePath.StartsWith("brass_help")) {
				string Action = e.Url.AbsolutePath.Substring(11).Split('=')[0];
				string Arguments = e.Url.AbsolutePath.Substring(12 + Action.Length);
				e.Cancel = true; // Cancel navigation.
				IPlugin Plugin = this.HelpProvider.Compiler.GetPluginInstanceFromGuid(new Guid(Arguments));
				if (Plugin != null) {
					this.SelectedPlugin = Plugin;
				}
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

		public void Print() {
			this.Viewer.Print();
		}

		public void ShowPrintPreviewDialog() {
			this.Viewer.ShowPrintPreviewDialog();
		}

		public void ShowPrintDialog() {
			this.Viewer.ShowPrintDialog();
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

		public void ExportWebsite(string indexFile) {
			string BasePath = Path.GetDirectoryName(indexFile);
			File.WriteAllText(indexFile, Properties.Resources.FramesetHtml);
			Directory.CreateDirectory(Path.Combine(BasePath, "help"));
			StringBuilder Contents = new StringBuilder(1024);
			Contents.Append(Properties.Resources.ContentsHtml);
			WriteTreeNodes(Contents, this.Contents.Nodes, (Path.Combine(BasePath, "help")));
			Contents.Append("</body></html>");
			File.WriteAllText(Path.Combine(BasePath, Path.Combine("help", "contents.html")), Contents.ToString());
			Properties.Resources.Icon_Error.Save(Path.Combine(BasePath, Path.Combine("help", "icon_error.png")));
			Properties.Resources.Icon_BookOpen.Save(Path.Combine(BasePath, Path.Combine("help", "icon_book_open.png")));
			Properties.Resources.Icon_BookClosed.Save(Path.Combine(BasePath, Path.Combine("help", "icon_book_closed.png")));
			Properties.Resources.Icon_PageBlue.Save(Path.Combine(BasePath, Path.Combine("help", "icon_page.png")));
		}

		public void WriteTreeNodes(StringBuilder contents, TreeNodeCollection nodes, string baseDirectory) {
			contents.Append("<ul>");
			foreach (TreeNode N in nodes) {
				contents.Append("<li class=\"");
				contents.Append(N.Tag == null ? "section" : "topic");
				contents.Append("\">");
				string Guid = N.Tag == null ? "" : N.Tag.GetType().GUID.ToString();
				if (N.Tag != null) {
					if (N.Tag as Assembly != null) {
						Guid = ((N.Tag as Assembly).GetCustomAttributes(typeof(GuidAttribute), false)[0] as GuidAttribute).Value;
					}
					contents.Append("<a href=\"" + Guid + ".html\" target=\"viewer\">");
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
				if (N.Nodes.Count > 0) WriteTreeNodes(contents, N.Nodes, baseDirectory);
				if (N.Tag != null) contents.Append("</a>");
				contents.Append("</li>");
			}
			contents.Append("</ul>");
		}

	}
}
