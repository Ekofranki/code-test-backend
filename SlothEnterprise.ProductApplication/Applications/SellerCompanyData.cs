using System;
using SlothEnterprise.External;

namespace SlothEnterprise.ProductApplication.Applications
{
    public class SellerCompanyData
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string DirectorName { get; set; }
        public DateTime Founded { get; set; }

        internal CompanyDataRequest ToCompanyDataRequest() => new CompanyDataRequest
        {
            CompanyFounded = Founded,
            CompanyNumber = Number,
            CompanyName = Name,
            DirectorName = DirectorName
        };
    }
}