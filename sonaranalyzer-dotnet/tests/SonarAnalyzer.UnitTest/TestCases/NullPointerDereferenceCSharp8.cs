using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAnalyzer.UnitTest.TestCases
{
    class NullPointerDereferenceCSharp8
    {
        public void SwitchExpression(string s)
        {
            switch (s)
            {
                case string x when x == null: x.ToLower(); // Noncompliant
                    break;

                default:
                    break;
            };

            var result = s switch
            {
                null => s.ToLower(), // Noncompliant
                _ => s.ToLower()
            };
        }
    }
}
