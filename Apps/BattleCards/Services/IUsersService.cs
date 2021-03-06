namespace BattleCard.Services
{
    public interface IUsersService
    {
        string GetUserId(string username, string password);

        void CreateUser(string username, string email, string password);

        bool IsUsernameAvailable(string username);

    }
}
