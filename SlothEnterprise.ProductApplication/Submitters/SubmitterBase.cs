using SlothEnterprise.External;

namespace SlothEnterprise.ProductApplication.Submitters
{
    public abstract class SubmitterBase
    {
        public const int WrongApplicationId = -1;

        protected static int GetApplicationId(IApplicationResult result) =>
            result.Success ? result.ApplicationId ?? WrongApplicationId : WrongApplicationId;
    }
}