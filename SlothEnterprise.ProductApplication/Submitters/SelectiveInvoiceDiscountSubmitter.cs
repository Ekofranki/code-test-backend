using System.Globalization;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Submitters
{
    public class SelectiveInvoiceDiscountSubmitter : SubmitterBase<SelectiveInvoiceDiscount>, IApplicationSubmitter
    {
        private readonly ISelectInvoiceService _selectInvoiceService;

        public SelectiveInvoiceDiscountSubmitter(ISelectInvoiceService selectInvoiceService) =>
            _selectInvoiceService = selectInvoiceService;

        public bool CanSubmit(IProduct product) => product is SelectiveInvoiceDiscount;

        public int Submit(SellerApplication application) =>
            Submit(application, (SelectiveInvoiceDiscount) application.Product);

        protected override int Submit(SellerApplication application, SelectiveInvoiceDiscount product) =>
            _selectInvoiceService.SubmitApplicationFor(
                application.CompanyData.Number.ToString(CultureInfo.InvariantCulture),
                product.InvoiceAmount,
                product.AdvancePercentage);
    }
}