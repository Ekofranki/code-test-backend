using System;
using System.Collections.Generic;
using System.Linq;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Submitters;

namespace SlothEnterprise.ProductApplication.Dispatchers
{
    public class ApplicationDispatcher
    {
        private readonly IEnumerable<IApplicationSubmitter> _submitters;

        public ApplicationDispatcher(ISelectInvoiceService selectInvoiceService,
            IConfidentialInvoiceService confidentialInvoiceWebService,
            IBusinessLoansService businessLoansService)
        {
            _submitters = new List<IApplicationSubmitter>
            {
                new BusinessLoansSubmitter(businessLoansService),
                new ConfidentialInvoiceDiscountSubmitter(confidentialInvoiceWebService),
                new SelectiveInvoiceDiscountSubmitter(selectInvoiceService)
            };
        }

        public int Dispatch(SellerApplication application)
        {
            try
            {
                return _submitters.Single(s => s.CanSubmit(application.Product)).Submit(application);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }
        }
    }
}