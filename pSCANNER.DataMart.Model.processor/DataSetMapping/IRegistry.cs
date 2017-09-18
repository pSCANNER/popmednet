using System.Collections.Generic;
using Lpp.Scanner.DataMart.Model.Processors.DataSetMapping.Configuration;

namespace Lpp.Scanner.DataMart.Model.Processors.DataSetMapping {

    /// <summary>
    /// </summary>
    public interface IRegistry {

        /// <summary>
        ///     Gets the data set connection element map.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, DataSetConnectionElement> GetDataSetConnectionElementMap();

        /// <summary>
        ///     Gets the r section.
        /// </summary>
        /// <returns></returns>
        string getRSection();
    }
}