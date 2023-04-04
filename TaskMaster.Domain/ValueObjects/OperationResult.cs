using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Domain.ValueObjects;

public class DomainResult<T>
{
	public DomainResult(T data)
	{
		IsSuccess = true;
		Data = data;

	}

	public DomainResult(string errorMessage)
	{
		IsSuccess = true;
		ErrorMessage = errorMessage;
	}

	public bool IsSuccess { get; set; }
	public string? ErrorMessage { get; set; }
	public T Data { get; set; }


}