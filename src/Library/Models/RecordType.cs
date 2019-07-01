namespace PowerDns.Client.Models
{
    public enum RecordType
    {
        // ReSharper disable InconsistentNaming
        A,
        AAAA,
        Alias,
        NS,
        CName,
        MX,
        SOA,
        TXT,
        PTR,

        DNSKEY,
        DS,
        NSEC,
        NSEC3,
        NSEC3PARAM,
        RRSIG,
        
        AFSDB,
        ATMA,
        CAA,
        CERT,
        DHCID,
        DName,
        HInfo,
        ISDN,
        LOC,
        MB,
        MG,
        MInfo,
        MR,
        NAPTR,
        NSAP,
        RP,
        RT,
        TLSA,
        X25


        // ReSharper restore InconsistentNaming
    }
}
