using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Submitters
{
    public interface IApplicationSubmitter
    {
        bool CanSubmit(IProduct product);
        int Submit(SellerApplication application);
    }
}