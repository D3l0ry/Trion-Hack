namespace Trion.SDK.Structures.Externals
{
    internal struct Version
    {
        #region Properties
        public string ClientVersion { get;private set; }
        public string ServerVersion { get; private set; }
        public string PatchVersion { get; private set; }
        public string ProductName { get; private set; }
        public string appID { get; private set; }
        public string SourceRevision { get; private set; }
        public string VersionDate { get; private set; }
        public string VersionTime { get; private set; }
        #endregion
    }
}