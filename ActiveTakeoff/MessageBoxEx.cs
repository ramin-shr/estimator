using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using QuoterPlan.Properties;

namespace QuoterPlan
{
	public partial class MessageBoxEx : BaseForm
	{
		private void LoadResources()
		{
		}

		private void InitializeFonts()
		{
			this.lblTitle.Font = Utilities.GetDefaultFont(FontStyle.Bold);
			this.lblMessage.Font = Utilities.GetDefaultFont();
			this.txtMessage.Font = Utilities.GetDefaultFont();
		}

		private void InitializeWindow()
		{
			switch (this.messageType)
			{
			case MessageBoxEx.MessageTypeEnum.DisplayMessage:
				this.picIcon.Image = Resources.info_48x48;
				this.btOk.Visible = true;
				this.btOk.Left = this.btCancel.Left;
				break;
			case MessageBoxEx.MessageTypeEnum.DisplayWarning:
				this.picIcon.Image = Resources.warning_48x48;
				this.btOk.Visible = true;
				this.btOk.Left = this.btCancel.Left;
				break;
			case MessageBoxEx.MessageTypeEnum.DisplayQuestion:
				this.picIcon.Image = Resources.question_48x48;
				this.btYes.Visible = true;
				this.btYes.Left = this.btOk.Left;
				this.btNo.Visible = true;
				this.btNo.Left = this.btCancel.Left;
				break;
			case MessageBoxEx.MessageTypeEnum.DisplayWarningQuestion:
				this.picIcon.Image = Resources.warning_48x48;
				this.btYes.Visible = true;
				this.btYes.Left = this.btOk.Left;
				this.btNo.Visible = true;
				this.btNo.Left = this.btCancel.Left;
				break;
			case MessageBoxEx.MessageTypeEnum.DisplayWarningQuestionOkCancel:
				this.picIcon.Image = Resources.warning_48x48;
				this.btOk.Visible = true;
				this.btCancel.Visible = true;
				break;
			case MessageBoxEx.MessageTypeEnum.DisplayWarningQuestionYesNoCancel:
				this.picIcon.Image = Resources.warning_48x48;
				this.btYes.Visible = true;
				this.btNo.Visible = true;
				this.btCancel.Visible = true;
				break;
			case MessageBoxEx.MessageTypeEnum.DisplayError:
				this.picIcon.Image = Resources.error_48x48;
				this.btOk.Visible = true;
				this.btOk.Left = this.btCancel.Left;
				break;
			case MessageBoxEx.MessageTypeEnum.DisplayLogError:
				this.picIcon.Image = Resources.error_48x48;
				this.lblMessage.Visible = false;
				this.txtMessage.Visible = true;
				this.btOk.Visible = true;
				this.btOk.Left = this.btCancel.Left;
				break;
			case MessageBoxEx.MessageTypeEnum.DisplayDeleteConfirmation:
				this.picIcon.Image = Resources.delete_48x48;
				this.btDelete.Visible = true;
				this.btNo.Visible = true;
				this.btNo.Left = this.btCancel.Left;
				break;
			case MessageBoxEx.MessageTypeEnum.DisplayWarningQuestionCustom:
				this.picIcon.Image = Resources.warning_48x48;
				this.btCustomYes.Visible = true;
				this.btNo.Visible = true;
				this.btNo.Left = this.btCancel.Left;
				break;
			case MessageBoxEx.MessageTypeEnum.DisplayQuestionCustom:
				this.picIcon.Image = Resources.info_48x48;
				this.btCustomQuestion1.Visible = true;
				this.btCustomQuestion2.Visible = true;
				break;
			}
			this.picIcon.Invalidate();
		}

		public void SetCustomImage(Image customImage)
		{
			this.picIcon.Image = customImage;
		}

		public void SetCustomCaption1(string customCaption)
		{
			switch (this.messageType)
			{
			case MessageBoxEx.MessageTypeEnum.DisplayMessage:
			{
				int width = this.btOk.Width;
				this.btOk.Text = customCaption;
				this.btOk.Width = Utilities.MeasureTextWidth(customCaption, this.btOk.Font) + 20;
				this.btOk.Left -= this.btOk.Width - width;
				return;
			}
			case MessageBoxEx.MessageTypeEnum.DisplayWarning:
				this.btOk.Text = customCaption;
				return;
			case MessageBoxEx.MessageTypeEnum.DisplayQuestion:
				this.btYes.Text = customCaption;
				return;
			case MessageBoxEx.MessageTypeEnum.DisplayWarningQuestion:
				this.btYes.Text = customCaption;
				return;
			case MessageBoxEx.MessageTypeEnum.DisplayWarningQuestionOkCancel:
				this.btOk.Text = customCaption;
				return;
			case MessageBoxEx.MessageTypeEnum.DisplayWarningQuestionYesNoCancel:
				this.btYes.Text = customCaption;
				return;
			case MessageBoxEx.MessageTypeEnum.DisplayError:
				this.btOk.Text = customCaption;
				return;
			case MessageBoxEx.MessageTypeEnum.DisplayLogError:
				this.btOk.Text = customCaption;
				return;
			case MessageBoxEx.MessageTypeEnum.DisplayDeleteConfirmation:
				this.btDelete.Text = customCaption;
				return;
			case MessageBoxEx.MessageTypeEnum.DisplayWarningQuestionCustom:
				this.btCustomYes.Text = customCaption;
				return;
			case MessageBoxEx.MessageTypeEnum.DisplayQuestionCustom:
				this.btCustomQuestion1.Text = customCaption;
				return;
			default:
				return;
			}
		}

		public void SetCustomCaption2(string customCaption)
		{
			switch (this.messageType)
			{
			case MessageBoxEx.MessageTypeEnum.DisplayMessage:
			case MessageBoxEx.MessageTypeEnum.DisplayWarning:
			case MessageBoxEx.MessageTypeEnum.DisplayQuestion:
			case MessageBoxEx.MessageTypeEnum.DisplayWarningQuestion:
			case MessageBoxEx.MessageTypeEnum.DisplayWarningQuestionOkCancel:
			case MessageBoxEx.MessageTypeEnum.DisplayWarningQuestionYesNoCancel:
			case MessageBoxEx.MessageTypeEnum.DisplayError:
			case MessageBoxEx.MessageTypeEnum.DisplayLogError:
			case MessageBoxEx.MessageTypeEnum.DisplayDeleteConfirmation:
			case MessageBoxEx.MessageTypeEnum.DisplayWarningQuestionCustom:
				break;
			case MessageBoxEx.MessageTypeEnum.DisplayQuestionCustom:
				this.btCustomQuestion2.Text = customCaption;
				break;
			default:
				return;
			}
		}

		public MessageBoxEx(string title, string message, MessageBoxEx.MessageTypeEnum messageType, DialogResult defaultResult)
		{
			this.InitializeComponent();
			this.LoadResources();
			this.InitializeFonts();
			this.Text = Utilities.ApplicationName;
			this.lblTitle.Text = title;
			this.messageType = messageType;
			this.defaultResult = defaultResult;
			using (Graphics graphics = base.CreateGraphics())
			{
				Control control = (messageType != MessageBoxEx.MessageTypeEnum.DisplayLogError) ? this.lblMessage : this.txtMessage;
				int num = (int)Math.Ceiling((double)graphics.MeasureString(message, control.Font, control.Width).Height);
				if (!Utilities.IsSegoeUIInstalled())
				{
					num = (int)((float)num * 1.1f);
				}
				num = ((num > 200) ? 200 : num);
				if (num > control.Height)
				{
					int num2 = num - control.Height;
					control.Height = num;
					base.Height += num2;
				}
				if (messageType != MessageBoxEx.MessageTypeEnum.DisplayLogError)
				{
					this.lblMessage.Text = message;
				}
				else
				{
					this.txtMessage.Text = message;
				}
			}
			this.InitializeWindow();
		}

		private void btButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void MessageBoxEx_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == DialogResult.Cancel && this.defaultResult == DialogResult.No)
			{
				base.DialogResult = DialogResult.No;
			}
		}

		private MessageBoxEx.MessageTypeEnum messageType;

		private DialogResult defaultResult;

		public enum MessageTypeEnum
		{
			DisplayMessage,
			DisplayWarning,
			DisplayQuestion,
			DisplayWarningQuestion,
			DisplayWarningQuestionOkCancel,
			DisplayWarningQuestionYesNoCancel,
			DisplayError,
			DisplayLogError,
			DisplayDeleteConfirmation,
			DisplayWarningQuestionCustom,
			DisplayQuestionCustom
		}
	}
}
