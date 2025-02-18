using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.RebateProcessors
{
    /// <summary>
    /// Tests the ProcessRebate method in the <see cref="Services.FixedRateRebateProcessor"/>
    /// </summary>
    public class FixedRateRebateProcessor
    {
        /// <summary>
        /// Tests that processing a rebate on a product that does not support that incentive type will fail
        /// </summary>
        /// <param name="incentiveType">An invalid incentive type for the rebate being tested</param>
        [Theory]
        [InlineData(SupportedIncentiveType.FixedCashAmount)]
        [InlineData(SupportedIncentiveType.AmountPerUom)]
        public void ProcessRebate_InvalidSupportedIncentive_ReturnsNull(
            SupportedIncentiveType incentiveType)
        {
            // Arrange
            var processor = new Services.FixedRateRebateProcessor();
            var fakeProduct = new Product
            {
                SupportedIncentives = incentiveType,
                Price = 10,
            };

            var fakeRebate = new Rebate
            {
                Percentage = .1M
            };

            var fakeRequest = new CalculateRebateRequest
            {
                Volume = 10
            };

            // Act
            var result = processor.ProcessRebate(fakeRebate, fakeProduct, fakeRequest);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Tests that calculating the rebate with an invalid rebate percentage will fail
        /// </summary>
        [Fact]
        public void ProcessRebate_InvalidRebatePercentage_ReturnsNull()
        {
            // Arrange
            var processor = new Services.FixedRateRebateProcessor();
            var fakeProduct = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
                Price = 10,
            };

            var fakeRebate = new Rebate
            {
                Percentage = 0
            };

            var fakeRequest = new CalculateRebateRequest
            {
                Volume = 10
            };

            // Act
            var result = processor.ProcessRebate(fakeRebate, fakeProduct, fakeRequest);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Tests that calculating the rebate with an invalid product price will fail
        /// </summary>
        [Fact]
        public void ProcessRebate_InvalidProductPrice_ReturnsNull()
        {
            // Arrange
            var processor = new Services.FixedRateRebateProcessor();
            var fakeProduct = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
                Price = 0,
            };

            var fakeRebate = new Rebate
            {
                Percentage = .1M
            };

            var fakeRequest = new CalculateRebateRequest
            {
                Volume = 10
            };

            // Act
            var result = processor.ProcessRebate(fakeRebate, fakeProduct, fakeRequest);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Tests that calculating the rebate with an invalid request volume will fail
        /// </summary>
        [Fact]
        public void ProcessRebate_InvalidRequestVolume_ReturnsNull()
        {
            // Arrange
            var processor = new Services.FixedRateRebateProcessor();
            var fakeProduct = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
                Price = 10,
            };

            var fakeRebate = new Rebate
            {
                Percentage = .1M
            };

            var fakeRequest = new CalculateRebateRequest
            {
                Volume = 0
            };

            // Act
            var result = processor.ProcessRebate(fakeRebate, fakeProduct, fakeRequest);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Tests that passing in all valid inputs will output the correct rebate amount
        /// </summary>
        [Fact]
        public void ProcessRebate_ValidInputs_ReturnsCorrectRebateAmount()
        {
            // Arrange
            var processor = new Services.FixedRateRebateProcessor();
            var fakeProduct = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
                Price = 10,
            };

            var fakeRebate = new Rebate
            {
                Percentage = .1M
            };

            var fakeRequest = new CalculateRebateRequest
            {
                Volume = 10
            };

            var expected = fakeProduct.Price * fakeRebate.Percentage * fakeRequest.Volume;

            // Act
            var result = processor.ProcessRebate(fakeRebate, fakeProduct, fakeRequest);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
