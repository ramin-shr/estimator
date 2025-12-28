namespace QuoterPlan
{
	public partial class FormulaBuilderForm : global::QuoterPlan.BaseForm
	{
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::QuoterPlan.FormulaBuilderForm));
			this.btOk = new global::System.Windows.Forms.Button();
			this.btCancel = new global::System.Windows.Forms.Button();
			this.fctb = new global::FastColoredTextBoxNS.FastColoredTextBox();
			this.btDivide = new global::QuoterPlan.ButtonEx();
			this.btMultiply = new global::QuoterPlan.ButtonEx();
			this.btMinus = new global::QuoterPlan.ButtonEx();
			this.btPlus = new global::QuoterPlan.ButtonEx();
			this.btCeil = new global::QuoterPlan.ButtonEx();
			this.btRound = new global::QuoterPlan.ButtonEx();
			this.btFloor = new global::QuoterPlan.ButtonEx();
			this.btSqrt = new global::QuoterPlan.ButtonEx();
			this.btPower = new global::QuoterPlan.ButtonEx();
			this.labelEx3 = new global::QuoterPlan.LabelEx();
			this.treeResults = new global::DevExpress.XtraTreeList.TreeList();
			this.labelEx1 = new global::QuoterPlan.LabelEx();
			this.labelEx2 = new global::QuoterPlan.LabelEx();
			((global::System.ComponentModel.ISupportInitialize)this.fctb).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.treeResults).BeginInit();
			base.SuspendLayout();
			this.btOk.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.btOk.Location = new global::System.Drawing.Point(190, 553);
			this.btOk.Name = "btOk";
			this.btOk.Size = new global::System.Drawing.Size(87, 27);
			this.btOk.TabIndex = 11;
			this.btOk.Text = "OK";
			this.btOk.UseVisualStyleBackColor = true;
			this.btOk.Click += new global::System.EventHandler(this.btOK_Click);
			this.btCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.ImeMode = global::System.Windows.Forms.ImeMode.NoControl;
			this.btCancel.Location = new global::System.Drawing.Point(285, 553);
			this.btCancel.Name = "btCancel";
			this.btCancel.Size = new global::System.Drawing.Size(87, 27);
			this.btCancel.TabIndex = 12;
			this.btCancel.Text = "Cancel";
			this.btCancel.UseVisualStyleBackColor = true;
			this.btCancel.Click += new global::System.EventHandler(this.btCancel_Click);
			this.fctb.AutoCompleteBracketsList = new char[]
			{
				'(',
				')',
				'{',
				'}',
				'[',
				']',
				'"',
				'"',
				'\'',
				'\''
			};
			this.fctb.AutoIndent = false;
			this.fctb.AutoScrollMinSize = new global::System.Drawing.Size(0, 15);
			this.fctb.BackBrush = null;
			this.fctb.BackColor = global::System.Drawing.Color.LemonChiffon;
			this.fctb.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.fctb.CharHeight = 15;
			this.fctb.CharWidth = 7;
			this.fctb.Cursor = global::System.Windows.Forms.Cursors.IBeam;
			this.fctb.DelayedEventsInterval = 500;
			this.fctb.DelayedTextChangedInterval = 500;
			this.fctb.DisabledColor = global::System.Drawing.Color.FromArgb(100, 180, 180, 180);
			this.fctb.Font = new global::System.Drawing.Font("Consolas", 9.75f);
			this.fctb.Hotkeys = componentResourceManager.GetString("fctb.Hotkeys");
			this.fctb.IsReplaceMode = false;
			this.fctb.LeftBracket = '(';
			this.fctb.Location = new global::System.Drawing.Point(12, 46);
			this.fctb.Name = "fctb";
			this.fctb.Paddings = new global::System.Windows.Forms.Padding(3, 0, 0, 0);
			this.fctb.RightBracket = ')';
			this.fctb.SelectionColor = global::System.Drawing.Color.FromArgb(50, 0, 0, 255);
			this.fctb.ServiceColors = (global::FastColoredTextBoxNS.ServiceColors)componentResourceManager.GetObject("fctb.ServiceColors");
			this.fctb.ShowLineNumbers = false;
			this.fctb.Size = new global::System.Drawing.Size(360, 80);
			this.fctb.TabIndex = 0;
			this.fctb.WordWrap = true;
			this.fctb.Zoom = 100;
			this.fctb.TextChanged += new global::System.EventHandler<global::FastColoredTextBoxNS.TextChangedEventArgs>(this.fctb_TextChanged);
			this.fctb.KeyPressing += new global::System.Windows.Forms.KeyPressEventHandler(this.fctb_KeyPressing);
			this.btDivide.Location = new global::System.Drawing.Point(216, 137);
			this.btDivide.Name = "btDivide";
			this.btDivide.Size = new global::System.Drawing.Size(35, 35);
			this.btDivide.TabIndex = 4;
			this.btDivide.Text = "/";
			this.btDivide.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.btDivide.UseVisualStyleBackColor = true;
			this.btDivide.Click += new global::System.EventHandler(this.btDivide_Click);
			this.btMultiply.Location = new global::System.Drawing.Point(175, 137);
			this.btMultiply.Name = "btMultiply";
			this.btMultiply.Size = new global::System.Drawing.Size(35, 35);
			this.btMultiply.TabIndex = 3;
			this.btMultiply.Text = "*";
			this.btMultiply.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.btMultiply.UseVisualStyleBackColor = true;
			this.btMultiply.Click += new global::System.EventHandler(this.btMultiply_Click);
			this.btMinus.Location = new global::System.Drawing.Point(134, 137);
			this.btMinus.Name = "btMinus";
			this.btMinus.Size = new global::System.Drawing.Size(35, 35);
			this.btMinus.TabIndex = 2;
			this.btMinus.Text = "-";
			this.btMinus.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.btMinus.UseVisualStyleBackColor = true;
			this.btMinus.Click += new global::System.EventHandler(this.btMinus_Click);
			this.btPlus.Location = new global::System.Drawing.Point(93, 137);
			this.btPlus.Name = "btPlus";
			this.btPlus.Size = new global::System.Drawing.Size(35, 35);
			this.btPlus.TabIndex = 1;
			this.btPlus.Text = "+";
			this.btPlus.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.btPlus.UseVisualStyleBackColor = true;
			this.btPlus.Click += new global::System.EventHandler(this.btPlus_Click);
			this.btCeil.Location = new global::System.Drawing.Point(53, 177);
			this.btCeil.Name = "btCeil";
			this.btCeil.Size = new global::System.Drawing.Size(65, 35);
			this.btCeil.TabIndex = 6;
			this.btCeil.Text = "$ceil( )";
			this.btCeil.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.btCeil.UseVisualStyleBackColor = true;
			this.btCeil.Click += new global::System.EventHandler(this.btCeil_Click);
			this.btRound.Location = new global::System.Drawing.Point(195, 177);
			this.btRound.Name = "btRound";
			this.btRound.Size = new global::System.Drawing.Size(65, 35);
			this.btRound.TabIndex = 8;
			this.btRound.Text = "$round( )";
			this.btRound.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.btRound.UseVisualStyleBackColor = true;
			this.btRound.Click += new global::System.EventHandler(this.btRound_Click);
			this.btFloor.Location = new global::System.Drawing.Point(124, 177);
			this.btFloor.Name = "btFloor";
			this.btFloor.Size = new global::System.Drawing.Size(65, 35);
			this.btFloor.TabIndex = 7;
			this.btFloor.Text = "$floor( )";
			this.btFloor.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.btFloor.UseVisualStyleBackColor = true;
			this.btFloor.Click += new global::System.EventHandler(this.btFloor_Click);
			this.btSqrt.Location = new global::System.Drawing.Point(266, 177);
			this.btSqrt.Name = "btSqrt";
			this.btSqrt.Size = new global::System.Drawing.Size(65, 35);
			this.btSqrt.TabIndex = 9;
			this.btSqrt.Text = "$sqrt( )";
			this.btSqrt.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.btSqrt.UseVisualStyleBackColor = true;
			this.btSqrt.Click += new global::System.EventHandler(this.btSqrt_Click);
			this.btPower.Location = new global::System.Drawing.Point(257, 137);
			this.btPower.Name = "btPower";
			this.btPower.Size = new global::System.Drawing.Size(35, 35);
			this.btPower.TabIndex = 5;
			this.btPower.Text = "^";
			this.btPower.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.btPower.UseVisualStyleBackColor = true;
			this.btPower.Click += new global::System.EventHandler(this.btPower_Click);
			this.labelEx3.AutoSize = true;
			this.labelEx3.Location = new global::System.Drawing.Point(10, 222);
			this.labelEx3.Name = "labelEx3";
			this.labelEx3.Size = new global::System.Drawing.Size(189, 15);
			this.labelEx3.TabIndex = 55;
			this.labelEx3.Text = "Double-click to insert into formula";
			this.labelEx3.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.treeResults.Location = new global::System.Drawing.Point(12, 242);
			this.treeResults.LookAndFeel.SkinName = "Office 2010 Silver";
			this.treeResults.Name = "treeResults";
			this.treeResults.OptionsView.FocusRectStyle = global::DevExpress.XtraTreeList.DrawFocusRectStyle.RowFullFocus;
			this.treeResults.OptionsView.ShowHorzLines = false;
			this.treeResults.OptionsView.ShowIndicator = false;
			this.treeResults.OptionsView.ShowVertLines = false;
			this.treeResults.Size = new global::System.Drawing.Size(360, 303);
			this.treeResults.TabIndex = 10;
			this.treeResults.TreeLevelWidth = 12;
			this.labelEx1.AutoSize = true;
			this.labelEx1.Location = new global::System.Drawing.Point(10, 9);
			this.labelEx1.Name = "labelEx1";
			this.labelEx1.Size = new global::System.Drawing.Size(138, 15);
			this.labelEx1.TabIndex = 56;
			this.labelEx1.Text = "Formula associated with:";
			this.labelEx1.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			this.labelEx2.AutoSize = true;
			this.labelEx2.Location = new global::System.Drawing.Point(10, 24);
			this.labelEx2.Name = "labelEx2";
			this.labelEx2.Size = new global::System.Drawing.Size(66, 15);
			this.labelEx2.TabIndex = 57;
			this.labelEx2.Text = "Item Name";
			this.labelEx2.TextRenderingHint = global::System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			base.AcceptButton = this.btOk;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 15f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btCancel;
			base.ClientSize = new global::System.Drawing.Size(385, 591);
			base.Controls.Add(this.labelEx2);
			base.Controls.Add(this.labelEx1);
			base.Controls.Add(this.labelEx3);
			base.Controls.Add(this.btPower);
			base.Controls.Add(this.btSqrt);
			base.Controls.Add(this.btFloor);
			base.Controls.Add(this.btRound);
			base.Controls.Add(this.btCeil);
			base.Controls.Add(this.btDivide);
			base.Controls.Add(this.btMultiply);
			base.Controls.Add(this.btMinus);
			base.Controls.Add(this.btPlus);
			base.Controls.Add(this.fctb);
			base.Controls.Add(this.btOk);
			base.Controls.Add(this.btCancel);
			base.Controls.Add(this.treeResults);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormulaBuilderForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Formula Builder";
			((global::System.ComponentModel.ISupportInitialize)this.fctb).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.treeResults).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Button btOk;

		private global::System.Windows.Forms.Button btCancel;

		private global::FastColoredTextBoxNS.FastColoredTextBox fctb;

		private global::QuoterPlan.ButtonEx btDivide;

		private global::QuoterPlan.ButtonEx btMultiply;

		private global::QuoterPlan.ButtonEx btMinus;

		private global::QuoterPlan.ButtonEx btPlus;

		private global::QuoterPlan.ButtonEx btCeil;

		private global::QuoterPlan.ButtonEx btRound;

		private global::QuoterPlan.ButtonEx btFloor;

		private global::QuoterPlan.ButtonEx btSqrt;

		private global::QuoterPlan.ButtonEx btPower;

		private global::QuoterPlan.LabelEx labelEx3;

		private global::DevExpress.XtraTreeList.TreeList treeResults;

		private global::QuoterPlan.LabelEx labelEx1;

		private global::QuoterPlan.LabelEx labelEx2;
	}
}
