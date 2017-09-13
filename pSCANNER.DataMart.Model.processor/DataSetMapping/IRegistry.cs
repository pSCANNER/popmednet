using System.Collections.Generic;
using Lpp.Scanner.DataMart.Model.Processors.DataSetMapping.Configuration;


namespace Lpp.Scanner.DataMart.Model.Processors.DataSetMapping
{

    public interface IRegistry {

        Dictionary<string, DataSetConnectionElement> GetDataSetConnectionElementMap();

        string getRSection();

    }
}
