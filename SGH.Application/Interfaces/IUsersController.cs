﻿using SGH.Application.Models.Request;
using SGH.Application.Models.Responces;

namespace SGH.Application.Interfaces;

public interface IUsersController
{
    public Task<AuthResponse> Login(AuthRequest request, CancellationToken ct);
    public Task<RegistrationResponse> Registration(RegistrationRequest request, CancellationToken ct);
    public Task<GetUserInfoResponse> UpdateUser(UpdateUserRequest request, CancellationToken ct);
}