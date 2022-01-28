using Microsoft.AspNetCore.Mvc;

namespace LongBox.WebApp.Controllers;

[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
	
	/// <summary>
	/// Hellow guis
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	[Route("hello-world")]
	public async Task<string> Test()
	{
		return "Hello World";
	} 
	
	
}