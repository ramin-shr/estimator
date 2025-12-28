using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
	public class HelpUtilities : BaseFileInfo
	{
		public Hashtable ContextTable
		{
			[CompilerGenerated]
			get
			{
				return this.<ContextTable>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ContextTable>k__BackingField = value;
			}
		}

		public HelpUtilities(Control parent, string fileName)
		{
			this.parent = parent;
			base.FullFileName = fileName;
			this.ContextTable = new Hashtable();
		}

		public void ShowHelp(string contextString)
		{
			int num = (contextString != string.Empty) ? this.GetContextIDFromString(contextString) : -1;
			if (num != -1)
			{
				this.ShowContextID(num);
				return;
			}
			this.ShowContent();
		}

		public void ShowContent()
		{
			try
			{
				Help.ShowHelp(this.parent, base.FullFileName, HelpNavigator.TableOfContents, "");
			}
			catch
			{
			}
		}

		public void ShowIndex()
		{
			try
			{
				Help.ShowHelp(this.parent, base.FullFileName, HelpNavigator.Index, "");
			}
			catch
			{
			}
		}

		public void ShowContextID(int contextID)
		{
			try
			{
				Help.ShowHelp(this.parent, base.FullFileName, HelpNavigator.TopicId, contextID.ToString());
			}
			catch
			{
			}
		}

		public int GetContextIDFromString(string contextString)
		{
			int result;
			try
			{
				if (this.ContextTable.ContainsKey(contextString))
				{
					result = Utilities.ConvertToInt(this.ContextTable[contextString]);
				}
				else
				{
					result = -1;
				}
			}
			catch
			{
				result = -1;
			}
			return result;
		}

		private Control parent;

		[CompilerGenerated]
		private Hashtable <ContextTable>k__BackingField;
	}
}
