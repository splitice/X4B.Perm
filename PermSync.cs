using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mono.Unix;
using Mono.Unix.Native;
using FileMode = Mono.Posix.FileMode;

namespace X4B.Perm
{
    class PermSync
    {
        private readonly Dictionary<String, FileAccessPermissions> _files = new Dictionary<String, FileAccessPermissions>();

        /// <summary>
        /// Octals to decimal.
        /// </summary>
        /// <param name="octalNumber">The octal number.</param>
        /// <returns></returns>
        public static Int32 OctalToDecimal(string octalNumber)
        {
            return Convert.ToInt32(Convert.ToString(Convert.ToInt64(octalNumber, 8), 10));
        }

        public void Add(String file, int perm)
        {
            if (file.IndexOf('*') == -1)
            {
                //Shortcut
                _files.Add(file, (FileAccessPermissions)perm);
            }
            else
            {
                foreach(var g in Globber.Glob(file))
                {
                    _files.Add(g, (FileAccessPermissions)perm);
                }
            }
        }

        public void Sync()
        {
            foreach(var r in _files)
            {
                if (File.Exists(r.Key))
                {
                    var ufi = new UnixFileInfo(r.Key);

                    if (ufi.FileAccessPermissions != r.Value)
                    {
                        Console.WriteLine(String.Format("Permission {0} of path {1} does not match {2}",
                                                        ufi.FileAccessPermissions, r.Key, r.Value));


                        Syscall.chmod(r.Key, (FilePermissions) (int) r.Value);
                    }
                }
            }
        }
    }
}
