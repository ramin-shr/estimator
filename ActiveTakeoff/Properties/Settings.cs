using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan.Properties
{
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
	[CompilerGenerated]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
		{
		}

		private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
		{
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		public Point WindowPosition
		{
			get
			{
				return (Point)this["WindowPosition"];
			}
			set
			{
				this["WindowPosition"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		public Size WindowSize
		{
			get
			{
				return (Size)this["WindowSize"];
			}
			set
			{
				this["WindowSize"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		public Color ThemeColor
		{
			get
			{
				return (Color)this["ThemeColor"];
			}
			set
			{
				this["ThemeColor"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		public char ImportDBPricesSeparator
		{
			get
			{
				return (char)this["ImportDBPricesSeparator"];
			}
			set
			{
				this["ImportDBPricesSeparator"] = value;
			}
		}
	}
}
