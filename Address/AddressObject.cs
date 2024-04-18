using IO_list_automation_new.General;
using System.Collections.Generic;

namespace IO_list_automation_new.Address
{
    internal class AddressSingle
    {
        public string Area { get; private set; }
        public string AddressStart { get; private set; }
        public string AddressSize { get; private set; }
        public string ObjectVariableType { get; private set; }

        public bool Overlap;

        public AddressSingle(string area, string addressStart, string addressSize, string objectVariableType)
        {
            Area = area;
            AddressStart = addressStart;
            AddressSize = addressSize;
            ObjectVariableType = objectVariableType;
            Overlap = false;
        }

        public void Update(string area, string addressStart, string addressSize, string objectVariableType)
        {
            Area = area;
            AddressStart = addressStart;
            AddressSize = addressSize;
            ObjectVariableType = objectVariableType;
        }

        public bool CheckOverlapString(AddressSingle address)
        {
            if (address.Area != Area)
                return false;

            if (!int.TryParse(AddressStart, out int myAddress))
                return false;

            if (!int.TryParse(AddressSize, out int myAddressSize))
                return false;

            if (!int.TryParse(address.AddressStart, out int addressStart))
                return false;

            if (!int.TryParse(address.AddressSize, out int addressSize))
                return false;

            if (myAddress < addressStart + addressSize && addressStart < myAddress + myAddressSize)
            {
                Overlap = true;
                return true;
            }
            return false;
        }

        public bool CheckOverlap(string area, int addressStart, int addressSize)
        {
            if (area != Area)
                return false;

            if (!int.TryParse(AddressStart, out int myAddress))
                return false;

            if (!int.TryParse(AddressSize, out int myAddressSize))
                return false;

            if (myAddress < addressStart + addressSize && addressStart < myAddress + myAddressSize)
            {
                Overlap = true;
                return true;
            }
            return false;
        }
    }

    internal class AddressObject : GeneralSignal
    {
        public string ID { get; private set; }
        public string CPU { get; private set; }
        public string ObjectType { get; private set; }
        public string ObjectName { get; private set; }
        public string ObjectGeneralType { get; private set; }

        public List<AddressSingle> Addresses { get; }

        public string UniqueModuleName
        { get { return CPU + "_" + ObjectType + "_" + ObjectName; } }

        public AddressObject() : base()
        {
            Addresses = new List<AddressSingle>();
            ID = string.Empty;
            CPU = string.Empty;
            ObjectType = string.Empty;
            ObjectGeneralType = string.Empty;
            ObjectName = string.Empty;
        }

        /// <summary>
        /// Checks if design signal is valid
        /// </summary>
        /// <returns>true if minimum signal requirements are met</returns>
        public override bool ValidateSignal()
        {
            if (string.IsNullOrEmpty(ObjectType))
                return false;
            else if (string.IsNullOrEmpty(ObjectName))
                return false;
            else if (string.IsNullOrEmpty(ObjectGeneralType))
                return false;

            return true;
        }

        public int FindAddressIndex(string objectVariableType)
        {
            for (int i = 0; i < Addresses.Count; i++)
            {
                if (Addresses[i].ObjectVariableType == objectVariableType)
                    return i;
            }
            return -1;
        }

        public void Update(string id, string inCPU, string objectGeneralType, string objectType, string objectName)
        {
            ID = id;
            CPU = inCPU;
            ObjectType = objectType;
            ObjectName = objectName;
            ObjectGeneralType = objectGeneralType;
        }

        public List<string> GetColumns()
        {
            List<string> columns = new List<string>();
            foreach (AddressSingle addressSingle in Addresses)
                columns.Add(addressSingle.ObjectVariableType);

            return columns;
        }

        public bool CheckOverlap(AddressSingle address)
        {
            if (!int.TryParse(address.AddressStart, out int addressStart))
                return false;

            if (!int.TryParse(address.AddressSize, out int addressSize))
                return false;

            foreach (AddressSingle addressSingle in Addresses)
            {
                if (addressSingle.CheckOverlap(address.Area, addressStart, addressSize))
                    return true;
            }
            return false;
        }
    }
}
