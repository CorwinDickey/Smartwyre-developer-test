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
        /// <param name="product">The product to the validity of the rebate against.</param>
        /// <param name="request">The rebate calculation request.</param>
        /// <returns>The monetary value of the rebate, if any. Returns null if the rebate fails to process.</returns>
        decimal? ProcessRebate(
            Rebate rebate, Product product, CalculateRebateRequest request);
    }
}
