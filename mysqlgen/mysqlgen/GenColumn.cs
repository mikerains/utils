using System;
using System.Collections.Generic;
using System.Text;

namespace mysqlgen
{
    public class GenColumn
    {
        public int id;
        public string colname;
        public string datatype;
        public Boolean isPK;
        public Boolean hasCodeLookup;
        public long Length;
        public bool isNullable;
    }
}
