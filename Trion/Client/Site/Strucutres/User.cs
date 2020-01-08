using System;

namespace Trion.Client.Site.Strucutres
{
    internal struct User
    {
        public DateTime licenseDate { get; private set; }

        public uint status { get;private set; }

        public uint IsHwid { get; private set; }

        public string error { get; private set; }
    }
}