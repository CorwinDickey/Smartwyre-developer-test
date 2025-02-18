using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    /// <summary>
    /// Code to process a fixed value rebate
    /// </summary>
    public class FixedCashRebateProcessor : IRebateProcessor
    {
        /// <inheritdoc/>
        public decimal ProcessRebate(
            Rebate rebate, CalculateRebateResult result, Product product)
        {
            // if there is no rebate, then the request fails to process and there is no monetary value, so return 0
            if (rebate == null)
            {
                return 0;
            }

            // if the product does not support fixed cash amount rebates then the request fails to process and there is no monetary value, so return 0
            if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
            {
                return 0;
            }

            // if the rebate exists but there is no value on it, then there was no successful rebate calculation and there is no monetary value, so return 0
            if (rebate.Amount == 0)
            {
                return 0;
            }

            // if all of the above validation passes, then it is a valid request so mark it as such and return the value of the rebate
            result.Success = true;
            return rebate.Amount;
        }
    }
}
