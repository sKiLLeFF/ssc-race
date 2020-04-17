using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSC.Client.Data
{
    public interface IStaticAdapter<T>
    {
        T ToStaticData();
    }
}
