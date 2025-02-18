using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    /// <summary>
    /// Code to process a fixed rate rebate
    /// </summary>
    public class FixedRateRebateProcessor : IRebateProcessor
    {
        /// <inheritdoc/>
        public decimal? ProcessRebate(
            Rebate rebate, Product product, CalculateRebateRequest request)
        {
            // the product must support fixed rate rebates
            if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
            {
                return null;
            }

            // all components necessary to calculate a fixed rate rebate must be present and non-zero
            if (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
            {
                return null;
            }

            // if all of the above validation passes, calculate and return the value of the rebate
            return product.Price * rebate.Percentage * request.Volume;
        }
    }
}
