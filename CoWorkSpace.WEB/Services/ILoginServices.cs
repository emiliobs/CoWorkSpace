namespace CoWorkSpace.WEB.Services;

public interface ILoginServices
{
    Task Login(string token);

    Task Logout();
}