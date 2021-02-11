using System;
using FluentAssertions;
using Moq;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Submitters;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Submitters
{
    public class BusinessLoansSubmitterTests
    {
        private readonly Mock<IBusinessLoansService> _businessLoansServiceMock;
        private readonly Mock<IApplicationResult> _applicationResultMock;
        private readonly IApplicationSubmitter _sut;

        public BusinessLoansSubmitterTests()
        {
            _applicationResultMock = new Mock<IApplicationResult>();
            _businessLoansServiceMock = new Mock<IBusinessLoansService>();
            _businessLoansServiceMock
                .Setup(x => x.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<LoansRequest>()))
                .Returns(_applicationResultMock.Object);

            _sut = new BusinessLoansSubmitter(_businessLoansServiceMock.Object);
        }

        [Fact]
        public void CanSubmit_BusinessLoansPayload_ReturnsTrue()
        {
            _sut.CanSubmit(new BusinessLoans()).Should().BeTrue();
        }

        [Fact]
        public void CanSubmit_ConfidentialInvoiceDiscountPayload_ReturnsFalse()
        {
            _sut.CanSubmit(new ConfidentialInvoiceDiscount()).Should().BeFalse();
        }

        [Fact]
        public void CanSubmit_SelectiveInvoiceDiscountPayload_ReturnsFalse()
        {
            _sut.CanSubmit(new SelectiveInvoiceDiscount()).Should().BeFalse();
        }

        [Theory]
        [InlineData(true, null, -1)]
        [InlineData(true, 2, 2)]
        [InlineData(false, null, -1)]
        public void Submit_ServiceReturnsApplicationId_ReturnsApplicationId(bool success, int? applicationId, int externalResult)
        {
            _applicationResultMock.SetupProperty(x => x.Success, success);
            _applicationResultMock.SetupProperty(x => x.ApplicationId, applicationId);

            var application = new SellerApplicationBuilder()
                .WithBusinessLoansProduct()
                .Build();

            var result = _sut.Submit(application);

            result.Should().Be(externalResult);

            _businessLoansServiceMock.Verify(
                x => x.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<LoansRequest>()), Times.Once);
        }

        [Fact]
        public void Submit_IncorrectProduct_ThrowsException()
        {
            var application = new SellerApplicationBuilder()
                .WithConfidentialInvoiceDiscountProduct()
                .Build();

            Assert.Throws<InvalidCastException>(() => _sut.Submit(application));
        }
    }
}