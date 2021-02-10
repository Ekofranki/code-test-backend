using System;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Submitters;

namespace SlothEnterprise.ProductApplication.Dispatchers
{
    public class ApplicationDispatcher
    {
        private readonly ISelectInvoiceService _selectInvoiceService;
        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;
        private readonly IBusinessLoansService _businessLoansService;

        public ApplicationDispatcher(ISelectInvoiceService selectInvoiceService,
            IConfidentialInvoiceService confidentialInvoiceWebService,
            IBusinessLoansService businessLoansService)
        {
            _selectInvoiceService = selectInvoiceService;
            _confidentialInvoiceWebService = confidentialInvoiceWebService;
            _businessLoansService = businessLoansService;
        }

        public int Dispatch(SellerApplication application)
        {
            int result;

            try
            {
                switch (application.Product)
                {
                    case BusinessLoans businessLoans:
                        result = new BusinessLoansSubmitter(_businessLoansService)
                            .Submit(application, businessLoans);
                        break;
                    case ConfidentialInvoiceDiscount confidentialInvoiceDiscount:
                        result = new ConfidentialInvoiceDiscountSubmitter(_confidentialInvoiceWebService)
                            .Submit(application, confidentialInvoiceDiscount);
                        break;
                    case SelectiveInvoiceDiscount selectiveInvoiceDiscount:
                        result = new SelectiveInvoiceDiscountSubmitter(_selectInvoiceService)
                            .Submit(application, selectiveInvoiceDiscount);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
            catch (Exception e)
            {
                result = SubmitterBase.WrongApplicationId;
                //handling exception depends on infrastructure, this is silent way
                Console.WriteLine(e);
            }

            return result;
        }
    }
}