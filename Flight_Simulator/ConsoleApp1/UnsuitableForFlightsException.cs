using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{
    [Serializable]
    class UnsuitableForFlightsException: Exception
    {
        public UnsuitableForFlightsException( string message): base(message){}
    }
}
