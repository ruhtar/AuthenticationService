﻿using AuthenticationService.Models;

namespace AuthenticationService.Repository
{
    public interface IUserRepository
    {
        Task AddUser(User user);
    }
}