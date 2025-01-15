namespace BitwardenToAegisOtpConverter
{
    using System.Net;

    public class OtpAuthUrl
    {
        public OtpAuthUrl(string url)
        {
            if (url.StartsWith("otpauth://totp/"))
            {
                url = url.Substring("otpauth://totp/".Length);
                if (url.Contains("?"))
                {
                    var issuerAndUser = url.Split('?')[0];
                    url = url.Split('?')[1];

                    issuerAndUser = WebUtility.UrlDecode(issuerAndUser);
                    if (issuerAndUser.Contains(":"))
                    {
                        issuerAndUser = issuerAndUser.Split(":")[1];
                    }

                    this.user = issuerAndUser;

                    var parameters = url.Split('&');
                    foreach (var parameter in parameters)
                    {
                        if (parameter.Contains("="))
                        {
                            var key = parameter.Split("=")[0];
                            var value = parameter.Split("=")[1];
                            switch (key)
                            {
                                case "secret":
                                    this.secret = value;
                                    break;
                                case "algorithm":
                                    this.algorithm = value;
                                    break;
                                case "digits":
                                    this.digits = int.Parse(value);
                                    break;
                                case "period":
                                    this.period = int.Parse(value);
                                    break;
                                case "issuer":
                                    this.issuer = WebUtility.UrlDecode(value);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public string user { get; set; }

        public string secret { get; set; }

        public string algorithm { get; set; }

        public int digits { get; set; }

        public int period { get; set; }

        public string issuer { get; set; }
    }
}
