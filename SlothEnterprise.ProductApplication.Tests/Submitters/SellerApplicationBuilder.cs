using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Tests.Submitters
{
    public class SellerApplicationBuilder
    {
        private readonly SellerApplication _sellerApplication;

        public SellerApplicationBuilder()
        {
            _sellerApplication = new SellerApplication
            {
                CompanyData = new SellerCompanyData()
            };
        }

        public SellerApplicationBuilder WithBusinessLoansProduct()
        {
            _sellerApplication.Product = new BusinessLoans();

            return this;
        }

        public SellerApplicationBuilder WithConfidentialInvoiceDiscountProduct()
        {
            _sellerApplication.Product = new ConfidentialInvoiceDiscount();

            return this;
        }

        public SellerApplicationBuilder WithSelectiveInvoiceDiscountProduct()
        {
            _sellerApplication.Product = new SelectiveInvoiceDiscount();

            return this;
        }

        public SellerApplication Build() => _sellerApplication;
    }
}