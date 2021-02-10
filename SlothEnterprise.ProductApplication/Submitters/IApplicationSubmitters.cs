using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Submitters
{
    public interface IApplicationSubmitter<in TProduct>
        where TProduct : IProduct
    {
        int Submit(SellerApplication application, TProduct product);
    }
}