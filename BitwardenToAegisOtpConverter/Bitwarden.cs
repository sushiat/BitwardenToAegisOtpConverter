namespace BitwardenToAegisOtpConverter
{
    using System.Collections.Generic;

    public class Bitwarden
    {
        public bool encrypted { get; set; }
        public List<Item> items { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string name { get; set; }

        public string folderId { get; set; }

        public string organizationId { get; set; }

        public string collectionIds { get; set; }

        public string notes { get; set; }

        public int type { get; set; }

        public Login login { get; set; }

        public bool favorite { get; set; }

    }

    public class Login
    {
        public string totp { get; set; }
    }
}
