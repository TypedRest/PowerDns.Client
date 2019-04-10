namespace PowerDns.Client.Models
{
    /// <summary>
    /// Represents a single record in a <see cref="RecordSet"/>.
    /// </summary>
    public class Record
    {
        public Record()
        {}

        public Record(string content)
        {
            Content = content;
        }

        /// <summary>
        /// The content of this record.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Whether or not this record is disabled.
        /// </summary>
        /// <remarks>Default: false</remarks>
        public bool Disabled { get; set; }

        /// <summary>
        /// If set to true, the server will find the matching reverse zone and create a PTR there. 
        /// Existing PTR records are replaced. 
        /// If no matching reverse Zone, an error is thrown. Only valid in client bodies, only valid for A and AAAA types. Not returned by the server.
        /// </summary>
        /// <remarks>Default: false</remarks>
        public bool SetPtr { get; set; }
    }
}
