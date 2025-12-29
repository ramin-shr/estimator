using System;
using System.Runtime.CompilerServices;

namespace QuoterPlan
{
    public class EEExchangeData
    {
        public string Description
        {
            get;
            set;
        }

        public string ItemID
        {
            get;
            set;
        }

        public string ItemType
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string PersonalKey
        {
            get;
            set;
        }

        public EEExchangeData()
        {
        }

        public void Clear()
        {
            this.ItemType = "";
            this.ItemID = "";
            this.Name = "";
            this.Key = "";
            this.PersonalKey = "";
            this.Description = "";
        }

        public void Clone(EEExchangeData source)
        {
            if (source == null)
            {
                this.Clear();
                return;
            }
            this.ItemType = source.ItemType;
            this.ItemID = source.ItemID;
            this.Name = source.Name;
            this.Key = source.Key;
            this.PersonalKey = source.PersonalKey;
            this.Description = source.Description;
        }

        public bool IsValid()
        {
            if (this.ItemType == null)
            {
                return false;
            }
            return this.ItemType != "";
        }
    }
}