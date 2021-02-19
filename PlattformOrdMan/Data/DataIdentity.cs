using System;
using PlattformOrdMan.Database;

namespace PlattformOrdMan.Data
{
    public interface IDataIdentity : IDataIdentifier
    {
        Int32 GetId();
    }

    public abstract class DataIdentity : DataIdentifier, IDataIdentity
    {
        private Int32 MyId;

        public DataIdentity()
            : base()
        {
            MyId = PlattformOrdManData.NO_ID;
        }

        public DataIdentity(int id, string identifier)
            : base(identifier)
        {
            MyId = id;
        }

        public DataIdentity(DataReader dataReader)
            : base(dataReader)
        {
            MyId = dataReader.GetInt32(DataIdentityData.ID);
        }

        protected void SetTempId()
        {
            // temp id is unique for the session and negative
            MyId = GetNewTempId();
        }

        protected void SetId(int id)
        {
            MyId = id;
        }

        public Int32 GetId()
        {
            return MyId;
        }
    }

    public class DataIdentityList : DataIdentifierList
    {
        public IDataIdentity GetById(Int32 id)
        {
            foreach (IDataIdentity dataIdentity in this)
            {
                if (dataIdentity.GetId() == id)
                {
                    return dataIdentity;
                }
            }
            return null;
        }

        public override object Clone()
        {
            DataIdentityList diList = new DataIdentityList();
            foreach (IDataIdentity di in this)
            {
                diList.Add(di);
            }
            return diList;
        }

        public Int32 GetIndex(IDataIdentity dataIdentity)
        {
            Int32 index;

            for (index = 0; index < this.Count; index++)
            {
                if ((((IDataIdentity)(this[index])).GetDataType() == dataIdentity.GetDataType()) &&
                     (((IDataIdentity)(this[index])).GetId() == dataIdentity.GetId()))
                {
                    return index;
                }
            }
            return -1;
        }

        public Boolean IsMember(IDataIdentity dataIdentity)
        {
            return (this.GetIndex(dataIdentity) >= 0);
        }

        public Boolean IsNotMember(IDataIdentity dataIdentity)
        {
            return (this.GetIndex(dataIdentity) < 0);
        }

        public void Remove(IDataIdentity dataIdentity)
        {
            Int32 index;

            index = this.GetIndex(dataIdentity);
            if (index >= 0)
            {
                this.RemoveAt(index);
            }
        }
    }
}
