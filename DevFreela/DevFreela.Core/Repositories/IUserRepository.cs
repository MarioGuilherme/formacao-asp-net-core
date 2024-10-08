﻿using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface IUserRepository {
    Task<User> GetByIdAsync(int id);
    Task<int> CreateUserAsync(User user);
    Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash);
}