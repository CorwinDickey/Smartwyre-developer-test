using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.RebateService
{
	/// <summary>
	/// Tests the calculate method in the <see cref="Services.RebateService"/>
	/// </summary>
    public class Calculate
    {
		/// <summary>
		/// Tests that passing in an invalid
		/// <see cref="CalculateRebateRequest"/> returns a value of false for
		/// the success property.
		/// </summary>
		[Fact]
		public void Calculate_InvalidRequest_ReturnSuccessIsFalse()
		{
			// Arrange
			var mockRebateDataStore = new Mock<IRebateDataStore>();
			var mockProductDataStore = new Mock<IProductDataStore>();
			var service = new Services.RebateService(
				mockRebateDataStore.Object, mockProductDataStore.Object);
			var stubRequest = new CalculateRebateRequest();

			// Act
			var result = service.Calculate(stubRequest);

			// Assert
			Assert.False(result.Success);
		}

		/// <summary>
		/// Tests that the method will properly hit the FixedCashAmount
		/// strategy when the appropriate rebate/products are retrieved from
		/// the data store
		/// </summary>
		[Fact]
		public void Calculate_ValidFixedCashRequest_ReturnsSuccess()
		{
			// Arrange
			var mockRebateDataStore = new Mock<IRebateDataStore>();
            mockRebateDataStore
                .Setup(x => x.GetRebate(It.IsAny<string>()))
                .Returns(new Rebate { Amount = 10, Incentive = IncentiveType.FixedCashAmount });

            var mockProductDataStore = new Mock<IProductDataStore>();
            mockProductDataStore
                .Setup(x => x.GetProduct(It.IsAny<string>()))
                .Returns(new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount });

            var service = new Services.RebateService(
				mockRebateDataStore.Object, mockProductDataStore.Object);
			var stubRequest = new CalculateRebateRequest();

			// Act
			var result = service.Calculate(stubRequest);

			// Assert
			Assert.True(result.Success);
		}
	}
}
