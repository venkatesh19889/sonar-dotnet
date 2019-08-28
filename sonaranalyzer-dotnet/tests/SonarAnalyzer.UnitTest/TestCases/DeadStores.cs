using System;

namespace Tests.Diagnostics
{
    public class DeadStores
    {
        void calculateRate2(int a, int b, int[] array)
        {
            var x = 0;
            x = array[^1]; // Noncompliant
            try
            {
                x = 11; // Noncompliant
                x = 12;
                Console.Write(x);
                x = 13; // Noncompliant
            }
            catch (Exception)
            {
                x = 21; // Noncompliant
                x = 22;
                Console.Write(x);
                x = 23; // Noncompliant
            }
            x = 31; // Noncompliant
        }
    }
}
