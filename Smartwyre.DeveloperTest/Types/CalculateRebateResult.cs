namespace Smartwyre.DeveloperTest.Types;

/// <summary>
/// Class containing the result of a rebate calculation request
/// </summary>
public class CalculateRebateResult
{
    /// <summary>
    /// Flag indicating whether the rebate calculation was successful or not.
    /// </summary>
    /// <remarks>
    /// Defaults to false to prevent false positives.
    /// </remarks>
    public bool Success { get; set; } = false;
}
