using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class BaseFileInfo : BaseInfo
    {
        public string FileName
        {
            get
            {
                return Utilities.GetFileName(this.FullFileName, false);
            }
        }

        public string FolderName
        {
            get
            {
                return Utilities.GetDirectoryName(this.FullFileName);
            }
        }

        public string FullFileName
        {
            get;
            set;
        }

        public BaseFileInfo()
        {
            this.FullFileName = string.Empty;
        }

        public override void Clear()
        {
            base.Clear();
            this.FullFileName = string.Empty;
        }

        public override void Dump()
        {
            base.Dump();
            Console.WriteLine(string.Concat("FullFileName = ", this.FullFileName));
        }
    }
}