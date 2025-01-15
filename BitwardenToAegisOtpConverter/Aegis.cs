namespace BitwardenToAegisOtpConverter
{
    using System.Collections.Generic;

    public class Aegis
    {
        public DB db { get; set; }
    }

    public class DB
    {
        public List<Entry> entries { get; set; }
    }

    public class Entry
    {
        public string type { get; set; }

        public string uuid { get; set; }

        public string name { get; set; }

        public string issuer { get; set; }

        public string note { get; set; }

        public object icon { get; set; }

        public Info info { get; set; }
    }

    public class Info
    {
        public string secret { get; set; }

        public string algo { get; set; }

        public int digits { get; set; }

        public int period { get; set; }
    }
}
