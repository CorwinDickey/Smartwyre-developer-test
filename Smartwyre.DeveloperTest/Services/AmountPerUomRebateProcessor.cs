using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    /// <summary>
    /// Code to process an Amount Per UOM rebate
    /// </summary>
    public class AmountPerUomRebateProcessor : IRebateProcessor
    {
        /// <inheritdoc/>
        public decimal? ProcessRebate(
            Rebate rebate, Product product, CalculateRebateRequest request)
        {
            // the product must support Amount Per UOM rebates
            if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
            {
                return null;
            }

            // all components necessary to calculate an Amount Per UOM rebate must be present and non-zero
            if (rebate.Amount == 0 || request.Volume == 0)
            {
                return null;
            }

            // if all of the above validation passes, calculate and return the rebate
            return rebate.Amount * request.Volume;
        }
    }
}
