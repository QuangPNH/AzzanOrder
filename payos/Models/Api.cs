namespace payos.Models
{
    public class Api
    {
        public class ApiRequest
        {
            public long accountNo { get; set; }
            public string accountName { get; set; }
            public int acqId { get; set; }
            public double amount { get; set; }
            public string addInfo { get; set; }
            public string format { get; set; }
            public string template { get; set; }
        }

        public class Data
        {
            public int acpId { get; set; }
            public string accountName { get; set; }
            public string qrCode { get; set; }
            public string qrDataURL { get; set; }
        }

        public class ApiResponse
        {
            public string code { get; set; }
            public string desc { get; set; }
            public Data data { get; set; }
        }
        public class Datum
        {
            public int id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public string bin { get; set; }
            public string shortName { get; set; }
            public string logo { get; set; }
            public int transferSupported { get; set; }
            public int lookupSupported { get; set; }
            public string short_name { get; set; }
            public int support { get; set; }
            public int isTransfer { get; set; }
            public string swift_code { get; set; }

            public string custom_name
            {
                get
                {
                    return $"({bin}) {shortName}";
                }
            }
        }

        public class Bank
        {
            public string code { get; set; }
            public string desc { get; set; }
            public IList<Datum> data { get; set; }
        }
    }
}
