using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Submitters
{
    public class BusinessLoansSubmitter : SubmitterBase<BusinessLoans>, IApplicationSubmitter
    {
        private readonly IBusinessLoansService _service;

        public BusinessLoansSubmitter(IBusinessLoansService service) => _service = service;

        public bool CanSubmit(IProduct product) => product is BusinessLoans;

        public int Submit(SellerApplication application) =>
            Submit(application, (BusinessLoans) application.Product);

        protected override int Submit(SellerApplication application, BusinessLoans product)
        {
            var result = _service.SubmitApplicationFor(application.CompanyData.ToCompanyDataRequest(),
                product.ToLoansRequest());

            return GetApplicationId(result);
        }
    }
}