using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebateDataStore = new RebateDataStore();
        var productDataStore = new ProductDataStore();

        Rebate rebate = rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = productDataStore.GetProduct(request.ProductIdentifier);

        var result = new CalculateRebateResult();

        // if we were unable to retrieve either the rebate or the product,
        // then we can't calculate anything so skip the rest of this
        if (product != null && rebate != null)
        {
            IRebateProcessor processor = null;

            // identify the processor to use based on the incentive type
            // defined on the rebate
            switch (rebate.Incentive)
            {
                case IncentiveType.FixedCashAmount:
                    processor = new FixedCashRebateProcessor();
                    break;
                case IncentiveType.FixedRateRebate:
                    processor = new FixedRateRebateProcessor();
                    break;
                case IncentiveType.AmountPerUom:
                    processor = new AmountPerUomRebateProcessor();
                    break;
                default:
                    throw new NotImplementedException();
            }

            // process the rebate and return the calculated value
            var rebateAmount = processor.ProcessRebate(rebate, product, request);

            // if a value was returned after processing, set the result success value to true and store the calculated rebate value
            if (rebateAmount.HasValue)
            {
                result.Success = true;
                var storeRebateDataStore = new RebateDataStore();
                storeRebateDataStore.StoreCalculationResult(rebate, rebateAmount.Value);
            }
        }

        return result;
    }
}
