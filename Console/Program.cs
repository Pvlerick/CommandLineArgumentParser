using ObjectMentor.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var arg = new Args("l,p#,d*", args);
                var logging = arg.GetBoolean('l');
                int port = arg.GetInt32('p');
                var directory = arg.GetString('d');

                System.Console.WriteLine($"Logging: {logging}; Port: {port}; Directory: {directory}");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
