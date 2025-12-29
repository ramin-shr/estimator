using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class HelpUtilities : BaseFileInfo
    {
        private Control parent;

        public Hashtable ContextTable
        {
            get;
            set;
        }

        public HelpUtilities(Control parent, string fileName)
        {
            this.parent = parent;
            base.FullFileName = fileName;
            this.ContextTable = new Hashtable();
        }

        public int GetContextIDFromString(string contextString)
        {
            int num;
            try
            {
                num = (!this.ContextTable.ContainsKey(contextString) ? -1 : Utilities.ConvertToInt(this.ContextTable[contextString]));
            }
            catch
            {
                num = -1;
            }
            return num;
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

        public void ShowHelp(string contextString)
        {
            int num = (contextString != string.Empty ? this.GetContextIDFromString(contextString) : -1);
            if (num == -1)
            {
                this.ShowContent();
                return;
            }
            this.ShowContextID(num);
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
    }
}