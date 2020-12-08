using System;

namespace Authorization
{
    public interface IUsersRepository
    {
        bool HasAccess(Guid userId);
    }
}