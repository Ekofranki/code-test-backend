using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Submitters
{
    public class ConfidentialInvoiceDiscountSubmitter : SubmitterBase, IApplicationSubmitter<ConfidentialInvoiceDiscount>
    {
        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;

        public ConfidentialInvoiceDiscountSubmitter(IConfidentialInvoiceService confidentialInvoiceWebService) =>
            _confidentialInvoiceWebService = confidentialInvoiceWebService;

        public int Submit(SellerApplication application, ConfidentialInvoiceDiscount product)
        {
            var result = _confidentialInvoiceWebService.SubmitApplicationFor(
                application.CompanyData.ToCompanyDataRequest(),
                product.TotalLedgerNetworth,
                product.AdvancePercentage,
                product.VatRate);

            return GetApplicationId(result);
        }
    }
}