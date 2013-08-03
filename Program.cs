using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace X4B.Perm
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.WriteLine("One argument (path to permissions map) must be supplied");
                return;
            }

            var ps = new PermSync();
            var fs = File.OpenText(args[0]);
            while(!fs.EndOfStream)
            {
                var line = fs.ReadLine();
                var parts = line.Split(new char[] {'|'});

                if(parts.Length == 2)
                {
                    ps.Add(parts[0].Trim(), (int) PermSync.OctalToDecimal(parts[1].Trim()));
                }
            }

            ps.Sync();
        }
    }
}
