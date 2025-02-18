using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.RebateProcessors
{
    /// <summary>
    /// Tests the ProcessRebate method in the <see cref="Services.FixedCashRebateProcessor"/>
    /// </summary>
    public class FixedCashRebateProcessor
    {
        /// <summary>
        /// Tests that processing a rebate on a product that does not support that incentive type will fail
        /// </summary>
        /// <param name="incentiveType">An invalid incentive type for the rebate being tested</param>
        [Theory]
        [InlineData(SupportedIncentiveType.FixedRateRebate)]
        [InlineData(SupportedIncentiveType.AmountPerUom)]
        public void ProcessRebate_InvalidSupportedIncentive_ReturnsNull(
            SupportedIncentiveType incentiveType)
        {
            // Arrange
            var processor = new Services.FixedCashRebateProcessor();
            var fakeProduct = new Product
            {
                SupportedIncentives = incentiveType
            };

            var fakeRebate = new Rebate
            {
                Amount = 10
            };

            var stubRequest = new CalculateRebateRequest();

            // Act
            var result = processor.ProcessRebate(fakeRebate, fakeProduct, stubRequest);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Tests that processing a rebate with an invalid rebate amount will fail
        /// </summary>
        [Fact]
        public void ProcessRebate_InvalidRebateAmount_ReturnsNull()
        {
            // Arrange
            var processor = new Services.FixedCashRebateProcessor();
            var fakeProduct = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount
            };

            var fakeRebate = new Rebate
            {
                Amount = 0
            };

            var stubRequest = new CalculateRebateRequest();

            // Act
            var result = processor.ProcessRebate(fakeRebate, fakeProduct, stubRequest);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Tests that processing a rebate with all valid inputs will return the correct rebate amount
        /// </summary>
        [Fact]
        public void ProcessRebate_ValidProductAndRebate_ReturnsCorrectValue()
        {
            // Arrange
            var processor = new Services.FixedCashRebateProcessor();
            var fakeProduct = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount
            };

            var fakeRebate = new Rebate
            {
                Amount = 10
            };

            var stubRequest = new CalculateRebateRequest();

            // Act
            var result = processor.ProcessRebate(fakeRebate, fakeProduct, stubRequest);

            // Assert
            Assert.Equal(fakeRebate.Amount, result);
        }
    }
}
