using Otreya.Common.CQRS.Implementation.Net;

namespace Otreya.Common.Cqrs.Tests;

public class Setup : Xunit.Di.Setup
{
	protected override void Configure()
	{
		ConfigureServices((context, services) =>
		{
			services.AddDefaultCqrs();
		});
	}
}