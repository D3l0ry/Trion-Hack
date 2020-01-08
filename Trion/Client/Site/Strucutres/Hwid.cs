using System;
using System.Collections.Generic;
using System.Text;

namespace Trion.Client.Site.Strucutres
{
    internal struct Hwid
    {
        public uint status { get; private set; }

        public string success { get; private set; }

        public string error { get; private set; }
    }
}