using System;
using FluentAssertions;
using Moq;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Submitters;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Submitters
{
    public class SelectiveInvoiceDiscountSubmitterTests
    {
        private readonly Mock<ISelectInvoiceService> _selectInvoiceServiceMock;
        private readonly IApplicationSubmitter _sut;

        public SelectiveInvoiceDiscountSubmitterTests()
        {
            _selectInvoiceServiceMock = new Mock<ISelectInvoiceService>();

            _selectInvoiceServiceMock
                .Setup(x => x.SubmitApplicationFor(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(2);

            _sut = new SelectiveInvoiceDiscountSubmitter(_selectInvoiceServiceMock.Object);
        }

        [Fact]
        public void CanSubmit_BusinessLoansPayload_ReturnsFalse()
        {
            _sut.CanSubmit(new BusinessLoans()).Should().BeFalse();
        }

        [Fact]
        public void CanSubmit_ConfidentialInvoiceDiscountPayload_ReturnsFalse()
        {
            _sut.CanSubmit(new ConfidentialInvoiceDiscount()).Should().BeFalse();
        }

        [Fact]
        public void CanSubmit_SelectiveInvoiceDiscountPayload_ReturnsTrue()
        {
            _sut.CanSubmit(new SelectiveInvoiceDiscount()).Should().BeTrue();
        }

        [Fact]
        public void Submit_ServiceReturnsCorrectApplicationId_ReturnsApplicationId()
        {
            var application = new SellerApplicationBuilder()
                .WithSelectiveInvoiceDiscountProduct()
                .Build();

            var result = _sut.Submit(application);

            result.Should().Be(2);

            _selectInvoiceServiceMock.Verify(
                x => x.SubmitApplicationFor(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>()), Times.Once);
        }

        [Fact]
        public void Submit_IncorrectProduct_ThrowsException()
        {
            var application = new SellerApplicationBuilder()
                .WithBusinessLoansProduct()
                .Build();

            Assert.Throws<InvalidCastException>(() => _sut.Submit(application));
        }
    }
}