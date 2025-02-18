using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    /// <summary>
    /// Code to process a fixed value rebate
    /// </summary>
    public class FixedCashRebateProcessor : IRebateProcessor
    {
        /// <inheritdoc/>
        public decimal? ProcessRebate(
            Rebate rebate, Product product, CalculateRebateRequest request)
        {
            // the product must support fixed cash amount rebates
            if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
            {
                return null;
            }

            // a fixed cash rebate with no amount on it is a nonsense answer, so it should fail to process
            if (rebate.Amount == 0)
            {
                return null;
            }

            // if all of the above validation passes, return the value of the rebate
            return rebate.Amount;
        }
    }
}
