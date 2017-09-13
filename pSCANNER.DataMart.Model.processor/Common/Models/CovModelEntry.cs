using System.Collections.Generic;
using LinqKit;

namespace Lpp.Scanner.DataMart.Model.Processors.Common.Models
{

    public class CovModelEntry
    {
        #region Fields

        readonly Dictionary<string, EntryList> _entries;

        #endregion

        #region Constructors

        public CovModelEntry(Dictionary<string, List<double>> dictionary)
        {
            _entries = new Dictionary<string, EntryList>();

            foreach (var key in dictionary.Keys)
            {
                var entryList = new EntryList();

                if (_entries.ContainsKey(key) == false)
                {

                    _entries.Add(key, entryList);
                }

                entryList = _entries[key];

                entryList.Set(dictionary[key]);
            }

        }

        #endregion

        #region Properties

        public Dictionary<string, EntryList>.KeyCollection Keys
        {
            get {
                return _entries.Keys;
            }
        }

        #endregion

        public Dictionary<string, EntryList> Get()
        {
            return _entries;
        }

        public List<double> Get(string name)
        {
            return _entries[name].ToList();
        }

        public void Set(string name, List<double> toList)
        {
            _entries[name] = new EntryList(toList);
        }

    }

}