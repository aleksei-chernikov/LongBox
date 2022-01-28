namespace Otreya.Common.CQRS.Implementation.Net.Settings;

public class CqrsOptions
{

	/// <summary>
	/// Проверка, что все команды имплементированы на старте приложения
	/// </summary>
	public bool CheckCommandQuery { get; set; } = false;
	
	
	
}