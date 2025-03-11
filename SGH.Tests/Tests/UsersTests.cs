using AppResponseExtension.Exceptions;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using SGH.Application.Common;
using SGH.Application.Interfaces;
using SGH.Application.Mappers;
using SGH.Application.Models.Params;
using SGH.Application.Services;
using SGH.Data.Entities;
using SGH.Tests.DbMock;

namespace SGH.Tests.Tests;

public class UsersTests
{
    private readonly PostgresDbContextMock _context;
    private readonly UsersService _usersService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;
    
    public UsersTests()
    {
        _context = PostgresMock.Create();
        _dateTimeProvider = new DateTimeProvider();
        _jwtProvider = new JwtProvider(_dateTimeProvider);
        _passwordHasher = new PasswordHasher();
        _usersService = new UsersService(_context, _dateTimeProvider, _jwtProvider, new UserMapper(), _passwordHasher);

       Seed();
    }

    #region PasswordHasher

    [Fact]
    public void PasswordHasher_Success()
    {
        var hash = _passwordHasher.HashPassword("TEST");
        using (new AssertionScope())
        {
            hash.Should().NotBeNullOrEmpty();
        }
    }
    
    [Fact]
    public void PasswordHasher_ThrowsArgumentEmptyException()
        => Assert.Throws<ArgumentNullException>(() => _passwordHasher.HashPassword(""));
    
    [Fact]
    public void PasswordHasher_ThrowsArgumentNullException()
        => Assert.Throws<ArgumentNullException>(() => _passwordHasher.HashPassword(null));
    

    #endregion
    
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
        var result = await _usersService.Login(new AuthParams()
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
            await _usersService.Login(new AuthParams()
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
            await _usersService.Login(new AuthParams()
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
            await _usersService.Login(new AuthParams()
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
        var result = await _usersService.Registration(new RegistrationParams
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
            await _usersService.Registration(new RegistrationParams
            {
                Login = "TEST_LOGIN",
                Password = "NEW_TEST_PASSWORD",
                Name = "TEST_USER"
            }, default);
        });
    }

    #endregion

    #region UpdateuUserInfo

    [Fact]
    public async Task UpdateUserInfo_Success()
    {
        // Получаем старый профиль
        var oldUserName = (await _context.Users.FirstOrDefaultAsync(x => x.Id == new Guid("E54C536B-4C75-4B0C-8927-8D866C86C4A6")))!.Name;
        using (new AssertionScope())
        {
            oldUserName.Should().Be("TEST_USER");
        }
        
        // Изменяем
        var newPassword = "NEW_TEST_PASSWORD";
        var result = await _usersService.UpdateUser(new UpdateUserParams
        {
            UserId = new Guid("E54C536B-4C75-4B0C-8927-8D866C86C4A6"),
            Name = "NEW_USER_NAME",
            Password = newPassword
        }, default);
        
        using (new AssertionScope())
        {
            var isValidPassword = _passwordHasher.VerifyPassword(newPassword, result.Password);
            
            result.Name.Should().Be("NEW_USER_NAME");
            result.Password.Length.Should().BeGreaterThan(0);
            isValidPassword.Should().BeTrue();
        }
    }

    [Fact]
    public async Task UpdateUserInfo_NotFoundUser()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _usersService.UpdateUser(new UpdateUserParams
            {
                UserId = Guid.NewGuid(),
                Name = "NEW_USER_NAME",
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
            Password = _passwordHasher.HashPassword("TEST_PASSWORD"),
            Login = "TEST_LOGIN",
            Name = "TEST_USER",
            CreatedDate = dateTimeNow,
            LastUpdated = dateTimeNow,
        });
        _context.SaveChanges();
    }
}