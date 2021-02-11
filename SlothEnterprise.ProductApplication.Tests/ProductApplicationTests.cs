using FluentAssertions;
using Moq;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationTests
    {
        private readonly IProductApplicationService _sut;

        private readonly Mock<IConfidentialInvoiceService> _confidentialInvoiceServiceMock =
            new Mock<IConfidentialInvoiceService>();

        private readonly SellerApplication _sellerApplication;
        private readonly Mock<IApplicationResult> _result = new Mock<IApplicationResult>();

        public ProductApplicationTests()
        {
            _result.SetupProperty(p => p.ApplicationId, 1);
            _result.SetupProperty(p => p.Success, true);
            var productApplicationService = new Mock<IProductApplicationService>();
            _sut = productApplicationService.Object;
            productApplicationService.Setup(m => m.SubmitApplicationFor(It.IsAny<SellerApplication>())).Returns(1);

            _sellerApplication = new SellerApplication
            {
                CompanyData = new SellerCompanyData(),
                Product = new ConfidentialInvoiceDiscount()
            };
        }

        [Fact]
        public void ProductApplicationService_SubmitApplicationFor_WhenCalledWithSelectiveInvoiceDiscount_ShouldReturnOne()
        {
            _confidentialInvoiceServiceMock
                .Setup(m => m.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(),
                    It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(_result.Object);

            var result = _sut.SubmitApplicationFor(_sellerApplication);

            result.Should().Be(1);
        }
    }
}