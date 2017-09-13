using System.Collections.Generic;


namespace Lpp.Scanner.DataMart.Model.Processors.Common.Models {

    public class EntryList
    {
        #region Fields

        private List<double> _entryList;

        #endregion

        #region Constructors

        public EntryList(List<double> toList)
        {
            _entryList = toList;
        }

        public EntryList()
        {

        }

        #endregion

        public override string ToString()
        {
            return string.Join(", ", _entryList);
        }

        public double Get(int index)
        {
            return _entryList[index];
        }

        public void Set(List<double> value)
        {
            _entryList = value;
        }

        public List<double> ToList()
        {
            return _entryList;
        }

    }

}