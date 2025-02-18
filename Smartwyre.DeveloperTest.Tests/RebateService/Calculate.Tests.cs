using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.RebateService
{
	/// <summary>
	/// Tests the calculate method in the <see cref="Services.RebateService"/>
	/// </summary>
    public class Calculate
    {
		/// <summary>
		/// Tests that passing in an invalid
		/// <see cref="CalculateRebateRequest"/> returns a value of false for
		/// the success property.
		/// </summary>
		[Fact]
		public void Calculate_InvalidRequest_ReturnSuccessIsFalse()
		{
			// Arrange
			var service = new Services.RebateService();
			var stubRequest = new CalculateRebateRequest();

			// Act
			var result = service.Calculate(stubRequest);

			// Assert
			Assert.False(result.Success);
		}
	}
}
