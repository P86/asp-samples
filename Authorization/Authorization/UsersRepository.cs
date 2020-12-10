namespace Authorization
{
    public class UsersRepository : IUsersRepository
    {
        public bool HasAccess(int userId)
        {
            //user with id 506 don't have access
            return userId != 506;
        }
    }
}
