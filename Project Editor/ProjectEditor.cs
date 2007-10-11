using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Brass3;
using Brass3.Plugins;
using System.Xml;
using System.Reflection;
using System.Windows.Forms.VisualStyles;

namespace ProjectEditor {
	public partial class Editor : UserControl {

		private Dictionary<Assembly, List<IPlugin>> AvailablePlugins;
		private List<Type> ExcludedPlugins;

		private string projectFilename;
		public string ProjectFilename {
			get { return this.projectFilename; }
			set { this.projectFilename = value; }
		}

		private Project WorkingProject;

		public void Open(string filename) {
			this.projectFilename = filename;
			this.AvailablePlugins.Clear();
			this.ExcludedPlugins.Clear();

			WorkingProject = new Project();
			WorkingProject.Load(filename);

			foreach (Project.PluginCollectionInfo Collection in WorkingProject.Plugins) {
				this.LoadPluginsFromAssemblyName(Collection.Source, Collection.Exclusions.ToArray());
			}
			
			this.RefreshPluginList();
		}

		public void Save(string filename) {
			this.projectFilename = filename;
			this.Save();
		}

		public void Save() {
			this.WorkingProject.Plugins.Clear();
			foreach (KeyValuePair<Assembly, List<IPlugin>> KVP in this.AvailablePlugins) {
				bool FoundAnyPlugins = false;
				List<string> Exclusions = new List<string>();
				foreach (IPlugin P in KVP.Value) {
					if (this.ExcludedPlugins.Contains(P.GetType())) {
						Exclusions.Add(P.Name);
					} else {
						FoundAnyPlugins = true;
					}
				}
				if (FoundAnyPlugins) {
					this.WorkingProject.Plugins.Add(new Project.PluginCollectionInfo(KVP.Key.Location, Exclusions.ToArray()));
				}
			}
			this.WorkingProject.Save("proj.txt");
		}

		public Editor() {
			InitializeComponent();
			this.AvailablePlugins = new Dictionary<Assembly, List<IPlugin>>();
			this.ExcludedPlugins = new List<Type>();
			this.WorkingProject = new Project();
			this.IconImageList.Images.Add("PageCode", Properties.Resources.Icon_PageCode);
			this.IconImageList.Images.Add("Plugin", Properties.Resources.Icon_Plugin);
			this.IconImageList.Images.Add("PluginDisabled", Properties.Resources.Icon_PluginDisabled);
			this.IconImageList.Images.Add("PluginError", Properties.Resources.Icon_PluginError);
			this.ProjectTabsSource.ImageKey = "PageCode";
			this.ProjectTabsPlugins.ImageKey = "Plugin";

		}

		private void AddVisualStyleElementToImageList(VisualStyleElement element, ImageList imagelist) {
			VisualStyleRenderer VSR = new VisualStyleRenderer(element);
			Size S = VSR.GetPartSize(this.CreateGraphics(), ThemeSizeType.Draw);
			imagelist.ImageSize = S;
			Bitmap B = new Bitmap(S.Width, S.Height);
			using (Graphics G = Graphics.FromImage(B)) {
				VSR.DrawBackground(G, new Rectangle(0, 0, S.Width, S.Height));
				imagelist.Images.Add(B);
			}
		}

		private IPlugin[] LoadPluginsFromAssemblyName(string name, string[] exclusions) {
			List<string> Exclusions = new List<string>(Array.ConvertAll<string, string>(exclusions, delegate(string s) { return s.ToLowerInvariant(); }));
			List<IPlugin> ReturnValues = new List<IPlugin>();
			Assembly A = Assembly.LoadFrom(name);
			foreach (Type T in A.GetExportedTypes()) {
				if (T.IsClass && new List<Type>(T.GetInterfaces()).Contains(typeof(Brass3.Plugins.IPlugin))) {
					// Is a plugin!
					List<IPlugin> P;
					if (!this.AvailablePlugins.TryGetValue(A, out P)) {
						P = new List<IPlugin>();
						this.AvailablePlugins.Add(A, P);
					}
					ConstructorInfo CI = T.GetConstructor(new Type[] { typeof(Compiler) });
					IPlugin NewPlugin;
					if (CI != null) {
						NewPlugin = CI.Invoke(new object[] { new Compiler() }) as IPlugin;
					} else {
						NewPlugin = T.GetConstructor(Type.EmptyTypes).Invoke(null) as IPlugin;
					}

					if (NewPlugin != null) {
						if (P.TrueForAll(delegate(IPlugin p) { return p.GetType() != NewPlugin.GetType(); })) {
							ReturnValues.Add(NewPlugin);
							P.Add(NewPlugin);
							if (Exclusions.Contains(NewPlugin.Name.ToLowerInvariant())) {
								this.ExcludedPlugins.Add(NewPlugin.GetType());
							}
						}
					}
				}
			}
			return ReturnValues.ToArray();
		}

		private void AddPluginCollection_Click(object sender, EventArgs e) {
			if (this.OpenPluginDialog.ShowDialog(this) == DialogResult.OK) {
				foreach (string PluginFile in this.OpenPluginDialog.FileNames) {
					LoadPluginsFromAssemblyName(PluginFile, new string[] { });
				}
				this.RefreshPluginList();
			}

		}

		private void RefreshPluginList() {
			
			Comparison<TreeNode> TreeNodeSorter = delegate(TreeNode a, TreeNode b) { return a.Text.CompareTo(b.Text); };

			List<TreeNode> CollectionList = new List<TreeNode>();

			foreach (KeyValuePair<Assembly, List<IPlugin>> KVP in this.AvailablePlugins) {
				TreeNode T = new TreeNode(KVP.Key.GetName().Name);


				List<TreeNode> PluginEntries = new List<TreeNode>();
				foreach (IPlugin P in KVP.Value) {

					bool IsExcluded = this.ExcludedPlugins.Contains(P.GetType());

					IAliasedPlugin PAliased = P as IAliasedPlugin;
					TreeNode PT = new TreeNode(PAliased == null ? P.Name : string.Join("/", PAliased.Names));
					PT.Tag = P;
					PT.ImageIndex = IsExcluded ? 0 : 1;
					PT.SelectedImageIndex = IsExcluded ? 0 : 1;
					PluginEntries.Add(PT);
				}
				PluginEntries.Sort(TreeNodeSorter);
				T.Nodes.AddRange(PluginEntries.ToArray());
				T.ImageIndex = 0;
				T.SelectedImageIndex = 0;
				T.Expand();
				CollectionList.Add(T);
			}

			CollectionList.Sort(TreeNodeSorter);
			this.PluginCollectionList.Nodes.Clear();
			this.PluginCollectionList.Nodes.AddRange(CollectionList.ToArray());
		}


		private void ProjectEditor_Load(object sender, EventArgs e) {
			Application.DoEvents();
			this.AddVisualStyleElementToImageList(VisualStyleElement.Button.CheckBox.UncheckedNormal, this.CheckBoxImageList);
			this.AddVisualStyleElementToImageList(VisualStyleElement.Button.CheckBox.CheckedNormal, this.CheckBoxImageList);
			this.AddVisualStyleElementToImageList(VisualStyleElement.Button.CheckBox.MixedNormal, this.CheckBoxImageList);
		}

		private void PluginCollectionList_MouseUp(object sender, MouseEventArgs e) {
			if ((e.Button & MouseButtons.Left) == MouseButtons.None) return;
			TreeNode N = this.PluginCollectionList.GetNodeAt(e.Location);
			if (N != null) {
				Rectangle CheckBoxBounds = new Rectangle(N.Bounds.Location, this.CheckBoxImageList.ImageSize);
				CheckBoxBounds.Offset(-(3 + this.CheckBoxImageList.ImageSize.Width), (this.PluginCollectionList.ItemHeight - this.CheckBoxImageList.ImageSize.Height) / 2);
				if (CheckBoxBounds.Contains(e.Location)) {
					ToggleCheck(N);
				}
			}
		}

		private void ToggleCheck(TreeNode node) {

			if (node == null) return;

			switch (node.ImageIndex) {
				case 0:
					node.ImageIndex = 1;
					break;
				case 1:
					node.ImageIndex = 0;
					break;
				case 2:
					node.ImageIndex = 1;
					break;
			}
			node.SelectedImageIndex = node.ImageIndex;

			if (node.Tag == null) {
				foreach (TreeNode T in node.Nodes) {
					T.ImageIndex = node.ImageIndex;
					T.SelectedImageIndex = node.SelectedImageIndex;
				}
			} else {
				if (node.Parent != null) {

					this.ExcludedPlugins.Remove(node.Tag.GetType());
					if (node.ImageIndex == 0) this.ExcludedPlugins.Add(node.Tag.GetType());

					foreach (TreeNode T in node.Parent.Nodes) {
						if (T.ImageIndex != node.ImageIndex) {
							node.Parent.ImageIndex = 2;
							node.Parent.SelectedImageIndex = 2;
							return;
						}
					}
					node.Parent.ImageIndex = node.ImageIndex;
					node.Parent.SelectedImageIndex = node.SelectedImageIndex;
				}
			}

		}

		private void PluginCollectionList_BeforeCollapse(object sender, TreeViewCancelEventArgs e) {
			e.Cancel = true;
		}
	}
}
