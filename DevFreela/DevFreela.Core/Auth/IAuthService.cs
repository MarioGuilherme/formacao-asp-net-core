﻿namespace DevFreela.Core.Auth;

public interface IAuthService {
    string GenerateJwtToken(string email, string role);
    string ComputeSha256Hash(string rawPassword);
}