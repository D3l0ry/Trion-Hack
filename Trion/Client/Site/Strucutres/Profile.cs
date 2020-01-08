using System;

namespace Trion.Client.Site.Strucutres
{
    internal struct Profile
    {
        public string Name { get; private set; }

        public string Mail { get; private set; }

        public string Subscription { get; private set; }

        public string HWID { get; private set; }

        public DateTime LicenseDate { get; private set; }
    }
}