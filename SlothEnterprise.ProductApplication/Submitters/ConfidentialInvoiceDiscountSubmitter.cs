using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Submitters
{
    public class ConfidentialInvoiceDiscountSubmitter : SubmitterBase<ConfidentialInvoiceDiscount>, IApplicationSubmitter
    {
        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;

        public ConfidentialInvoiceDiscountSubmitter(IConfidentialInvoiceService confidentialInvoiceWebService) =>
            _confidentialInvoiceWebService = confidentialInvoiceWebService;

        public bool CanSubmit(IProduct product) => product is ConfidentialInvoiceDiscount;

        public int Submit(SellerApplication application) =>
            Submit(application, (ConfidentialInvoiceDiscount) application.Product);

        protected override int Submit(SellerApplication application, ConfidentialInvoiceDiscount product)
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