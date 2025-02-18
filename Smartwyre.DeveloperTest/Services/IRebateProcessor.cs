using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    /// <summary>
    /// Defines a rebate processor
    /// </summary>
    public interface IRebateProcessor
    {
        /// <summary>
        /// Executes the strategy for a specific rebate
        /// </summary>
        /// <param name="rebate">The rebate to process</param>
        /// <param name="result">The result of the rebate processing request.</param>
        /// <param name="product">The product to the validity of the rebate against.</param>
        /// <returns>The monetary value of the rebate, if any.</returns>
        decimal ProcessRebate(
            Rebate rebate, CalculateRebateResult result, Product product);
    }
}
