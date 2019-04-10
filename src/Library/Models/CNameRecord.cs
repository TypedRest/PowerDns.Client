namespace PowerDns.Client.Models
{
    /// <summary>
    /// Represets a CName record.
    /// </summary>
    public class CNameRecord : Record
    {
        public CNameRecord(CanonicalName absoluteName)
            : base(absoluteName.ToString())
        {}
    }
}
