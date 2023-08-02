﻿using AuthenticationService.Authentication;
using AuthenticationService.DTO;
using AuthenticationService.Entities;
using AuthenticationService.Repository;

namespace AuthenticationService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> ValidateUser(User user)
        {
            var userRegistered = await _userRepository.GetUser(new User
            {
                Username = user.Username
            });
            if (userRegistered == null) return false;
            return _passwordHasher.Validate(userRegistered.Password, user.Password);
        }

        public async Task<UserDTO> GetUserByUsername(string username)
        {
            var userRegistered = await _userRepository.GetUser(new User
            {
                Username = username
            });
            if (userRegistered != null)
            {
                return new UserDTO
                {
                    Username = userRegistered.Username,
                    Password = userRegistered.Password,
                };
            }
            return null;
        }

        public async Task AddUser(User user)
        {
            var hashedPassword = _passwordHasher.Hash(user.Password);
            await _userRepository.AddUser(new User
            {
                Username = user.Username,
                Password = hashedPassword,
                Role = user.Role
            });
        }
    }
}
