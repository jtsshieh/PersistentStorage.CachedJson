using System;
using System.Collections.Generic;
using System.Text;

namespace PersistentStorage.JSON
{
    public class JSONProperties : IProperties
    {
        public string DataFilePath { get; set; }
        public string DataFile { get; set; }
    }
}
