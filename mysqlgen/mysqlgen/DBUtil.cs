using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysqlgen
{
    public class DBUtil
    {
        public static Boolean hasRows(DataSet ds)
        {
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0) return true;
            return false;
        }
    }
}
