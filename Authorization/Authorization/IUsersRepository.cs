using System;

namespace Authorization
{
    public interface IUsersRepository
    {
        bool HasAccess(int userId);
    }
}