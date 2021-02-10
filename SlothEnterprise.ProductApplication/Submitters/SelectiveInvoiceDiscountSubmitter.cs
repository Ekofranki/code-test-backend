using System.Globalization;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Submitters
{
    public class SelectiveInvoiceDiscountSubmitter : SubmitterBase, IApplicationSubmitter<SelectiveInvoiceDiscount>
    {
        private readonly ISelectInvoiceService _selectInvoiceService;

        public SelectiveInvoiceDiscountSubmitter(ISelectInvoiceService selectInvoiceService) =>
            _selectInvoiceService = selectInvoiceService;

        public int Submit(SellerApplication application, SelectiveInvoiceDiscount product) =>
            _selectInvoiceService.SubmitApplicationFor(
                application.CompanyData.Number.ToString(CultureInfo.InvariantCulture),
                product.InvoiceAmount,
                product.AdvancePercentage);
    }
}