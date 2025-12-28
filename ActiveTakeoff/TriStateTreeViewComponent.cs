using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace QuoterPlan
{
	public class TriStateTreeViewComponent : TreeView
	{
		[Category("Tri-State Tree View")]
		[Description("Style of the Tri-State Tree View")]
		[DisplayName("Style")]
		public TriStateTreeViewComponent.TriStateStyles TriStateStyleProperty
		{
			get
			{
				return this.TriStateStyle;
			}
			set
			{
				this.TriStateStyle = value;
			}
		}

		public TriStateTreeViewComponent()
		{
			base.StateImageList = new ImageList();
			int screenDpi = Utilities.GetScreenDpi();
			int y;
			if (screenDpi >= 144)
			{
				y = 0;
			}
			else if (screenDpi >= 120)
			{
				y = 0;
			}
			else
			{
				y = 1;
			}
			for (int i = 0; i < 3; i++)
			{
				Bitmap bitmap = new Bitmap(16, 16);
				Graphics g = Graphics.FromImage(bitmap);
				switch (i)
				{
				case 0:
					CheckBoxRenderer.DrawCheckBox(g, new Point(0, y), CheckBoxState.UncheckedNormal);
					break;
				case 1:
					CheckBoxRenderer.DrawCheckBox(g, new Point(0, y), CheckBoxState.CheckedNormal);
					break;
				case 2:
					CheckBoxRenderer.DrawCheckBox(g, new Point(0, y), CheckBoxState.MixedNormal);
					break;
				}
				base.StateImageList.Images.Add(bitmap);
			}
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			base.CheckBoxes = false;
			this.IgnoreClickAction++;
			this.UpdateChildState(base.Nodes, 0, false, true);
			this.IgnoreClickAction--;
		}

		protected override void OnAfterCheck(TreeViewEventArgs e)
		{
			base.OnAfterCheck(e);
			if (this.IgnoreClickAction > 0)
			{
				return;
			}
			this.IgnoreClickAction++;
			TreeNode node = e.Node;
			node.StateImageIndex = (node.Checked ? 1 : 0);
			this.UpdateChildState(e.Node.Nodes, e.Node.StateImageIndex, e.Node.Checked, false);
			this.UpdateParentState(e.Node.Parent);
			this.IgnoreClickAction--;
		}

		protected override void OnAfterExpand(TreeViewEventArgs e)
		{
			base.OnAfterExpand(e);
			this.IgnoreClickAction++;
			this.UpdateChildState(e.Node.Nodes, e.Node.StateImageIndex, e.Node.Checked, true);
			this.IgnoreClickAction--;
		}

		protected void UpdateChildState(TreeNodeCollection Nodes, int StateImageIndex, bool Checked, bool ChangeUninitialisedNodesOnly)
		{
			foreach (object obj in Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				if (!ChangeUninitialisedNodesOnly || treeNode.StateImageIndex == -1)
				{
					treeNode.StateImageIndex = StateImageIndex;
					treeNode.Checked = Checked;
					if (treeNode.Nodes.Count > 0)
					{
						this.UpdateChildState(treeNode.Nodes, StateImageIndex, Checked, ChangeUninitialisedNodesOnly);
					}
				}
			}
		}

		protected void UpdateParentState(TreeNode tn)
		{
			if (tn == null)
			{
				return;
			}
			int stateImageIndex = tn.StateImageIndex;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (object obj in tn.Nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				if (treeNode.StateImageIndex == 1)
				{
					num2++;
				}
				else
				{
					if (treeNode.StateImageIndex == 2)
					{
						num3++;
						break;
					}
					num++;
				}
			}
			if (this.TriStateStyle == TriStateTreeViewComponent.TriStateStyles.Installer && num3 == 0)
			{
				tn.Checked = (num == 0);
			}
			if (num3 > 0)
			{
				tn.StateImageIndex = 2;
			}
			else if (num2 > 0 && num == 0)
			{
				if (tn.Checked)
				{
					tn.StateImageIndex = 1;
				}
				else
				{
					tn.StateImageIndex = 2;
				}
			}
			else if (num2 > 0)
			{
				tn.StateImageIndex = 2;
			}
			else if (tn.Checked)
			{
				tn.StateImageIndex = 2;
			}
			else
			{
				tn.StateImageIndex = 0;
			}
			if (stateImageIndex != tn.StateImageIndex && tn.Parent != null)
			{
				this.UpdateParentState(tn.Parent);
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Space)
			{
				base.SelectedNode.Checked = !base.SelectedNode.Checked;
			}
		}

		protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
		{
			base.OnNodeMouseClick(e);
			TreeViewHitTestInfo treeViewHitTestInfo = base.HitTest(e.X, e.Y);
			if (treeViewHitTestInfo == null || treeViewHitTestInfo.Location != TreeViewHitTestLocations.StateImage)
			{
				return;
			}
			TreeNode node = e.Node;
			node.Checked = !node.Checked;
		}

		private int IgnoreClickAction;

		private TriStateTreeViewComponent.TriStateStyles TriStateStyle;

		public enum CheckedState
		{
			UnInitialised = -1,
			UnChecked,
			Checked,
			Mixed
		}

		public enum TriStateStyles
		{
			Standard,
			Installer
		}
	}
}
