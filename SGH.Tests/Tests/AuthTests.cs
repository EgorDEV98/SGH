﻿using AppResponseExtension.Exceptions;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using SGH.Application.Common;
using SGH.Application.Interfaces;
using SGH.Application.Models.Params;
using SGH.Application.Services;
using SGH.Data.Entities;
using SGH.Tests.DbMock;

namespace SGH.Tests.Tests;

public class AuthTests
{
    private readonly PostgresDbContextMock _context;
    private readonly AuthService _authService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IJwtProvider _jwtProvider;
    
    public AuthTests()
    {
        _context = PostgresMock.Create();
        _dateTimeProvider = new DateTimeProvider();
        _jwtProvider = new JwtProvider(_dateTimeProvider);
        _authService = new AuthService(_context, _dateTimeProvider, _jwtProvider);

       Seed();
    }

    #region JwtProvider

    [Fact]
    public async Task GenerateJwt_Success()
    {
        var user = await _context.Users.FirstOrDefaultAsync();
        var result = _jwtProvider.GenerateJwtToken(user!);

        using (new AssertionScope())
        {
            result.Should().NotBeNull();
            result.Length.Should().BeGreaterThan(0);
        }
    }
    
    [Fact]
    public void GenerateJwt_EmptyUser()
    {
        Assert.Throws<ArgumentNullException>(() => _jwtProvider.GenerateJwtToken(null));
    }

    #endregion

    #region Login

    [Fact]
    public async Task Login_Success()
    {
        var result = await _authService.Login(new AuthParams()
        {
            Login = "TEST_LOGIN",
            Password = "TEST_PASSWORD"
        }, default);

        using (new AssertionScope())
        {
            result.Token.Should().NotBeNullOrEmpty();
            result.Token.Length.Should().BeGreaterThan(0);
        }
    }

    [Fact]
    public async Task Login_Incorrect_Login()
    {
        await Assert.ThrowsAsync<UnauthorizedException>(async () =>
        {
            await _authService.Login(new AuthParams()
            {
                Login = "TEST_INCORRECT_LOGIN",
                Password = "TEST_PASSWORD"
            }, default);
        });
    }
    
    [Fact]
    public async Task Login_NotFound()
    {
        await Assert.ThrowsAsync<UnauthorizedException>(async () =>
        {
            await _authService.Login(new AuthParams()
            {
                Login = "TEST_NOT_FOUND_LOGIN",
                Password = "TEST_NOT_FOUND_PASSWORD"
            }, default);
        });
    }
    
    [Fact]
    public async Task Login_Incorrect_Password()
    {
        await Assert.ThrowsAsync<UnauthorizedException>(async () =>
        {
            await _authService.Login(new AuthParams()
            {
                Login = "TEST_LOGIN",
                Password = "TEST_INCORRECT_PASSWORD"
            }, default);
        });
    }

    #endregion

    #region Registration

    [Fact]
    public async Task Registration_Success()
    {
        var result = await _authService.Registration(new RegistrationParams
        {
            Login = "NEW_TEST_LOGIN",
            Password = "NEW_TEST_PASSWORD",
            Name = "NEW_USER_NAME"
        }, default);

        using (new AssertionScope())
        {
            result.Token.Should().NotBeNullOrEmpty();
            result.Token.Length.Should().BeGreaterThan(0);
        }
    }
    
    [Fact]
    public async Task Registration_LoginAlreadyExist()
    {
        await Assert.ThrowsAsync<ConflictException>(async () =>
        {
            await _authService.Registration(new RegistrationParams
            {
                Login = "TEST_LOGIN",
                Password = "NEW_TEST_PASSWORD",
                Name = "TEST_USER"
            }, default);
        });
    }

    #endregion
    
    private void Seed()
    {
        var dateTimeNow = _dateTimeProvider.GetCurrent();
        _context.Users.Add(new User()
        {
            Id = new Guid("E54C536B-4C75-4B0C-8927-8D866C86C4A6"),
            Password = PasswordHasher.HashPassword("TEST_PASSWORD"),
            Login = "TEST_LOGIN",
            Name = "TEST_USER",
            CreatedDate = dateTimeNow,
            LastUpdated = dateTimeNow,
        });
        _context.SaveChanges();
    }
}