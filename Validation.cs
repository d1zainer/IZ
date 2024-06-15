using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IZ
{
    public static class Validation
    {
        public static bool ValidateInput(string input)
        {
            double temp;
            return Double.TryParse(input, out temp) && !Double.IsNaN(temp) && !Double.IsInfinity(temp);
        }
    }
}
