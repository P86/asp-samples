using System;

namespace Authorization
{
    public class UsersRepository : IUsersRepository
    {
        public bool HasAccess(Guid userId)
        {
            return true;
        }
    }
}
