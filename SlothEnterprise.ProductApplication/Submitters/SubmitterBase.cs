using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Submitters
{
    public abstract class SubmitterBase<TProduct>
        where TProduct : IProduct
    {
        public const int WrongApplicationId = -1;

        protected abstract int Submit(SellerApplication application, TProduct product);

        protected static int GetApplicationId(IApplicationResult result) =>
            result.Success ? result.ApplicationId ?? WrongApplicationId : WrongApplicationId;
    }
}