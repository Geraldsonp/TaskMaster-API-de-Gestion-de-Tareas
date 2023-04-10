namespace TaskMaster.Domain.Exceptions;

public class NotFoundException : Exception
{

	public NotFoundException(string name, int id)
		: base($"{name} with the Id: {id} Can not be found") { }
}