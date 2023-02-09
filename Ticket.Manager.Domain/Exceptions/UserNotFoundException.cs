namespace Issues.Manager.Domain.Exceptions;

public class UserNotFoundException: Exception
{

    public UserNotFoundException(int userId)
        : base($"User with the Id: {userId} Can not be found") { }
}