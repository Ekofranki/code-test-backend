using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Dispatchers;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly ApplicationDispatcher _dispatcher;

        public ProductApplicationService(ISelectInvoiceService selectInvoiceService,
            IConfidentialInvoiceService confidentialInvoiceWebService,
            IBusinessLoansService businessLoansService)
        {
            _dispatcher = new ApplicationDispatcher(selectInvoiceService, confidentialInvoiceWebService,
                businessLoansService);
        }

        public int SubmitApplicationFor(SellerApplication application) => _dispatcher.Dispatch(application);
    }
}
