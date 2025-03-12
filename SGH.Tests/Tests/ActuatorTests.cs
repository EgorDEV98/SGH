using AppResponseExtension.Exceptions;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using SGH.Application.Common;
using SGH.Application.Interfaces;
using SGH.Application.Mappers;
using SGH.Application.Models.Params;
using SGH.Application.Services;
using SGH.Data;
using SGH.Data.Entities;
using SGH.Data.Enums;
using SGH.Tests.DbMock;

namespace SGH.Tests.Tests;

public class ActuatorTests
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly PostgresDbContext _dbContext;
    private readonly IActuatorsService _actuatorsService;
    
    
    public ActuatorTests()
    {
        _dateTimeProvider = new DateTimeProvider();
        _dbContext = PostgresMock.Create();
        _actuatorsService = new ActuatorsService(_dbContext, new ActuatorMapper(), _dateTimeProvider);

        Seed();
    }

    #region GetActuator

    [Fact]
    public async Task GetActuator_Success()
    {
        var result = await _actuatorsService.GetActuator(new GetActuatorParams()
        {
            Id = new Guid("80AB8942-87F8-4C9A-B16C-94D719AAA7D8")
        }, default);

        using (new AssertionScope())
        {
            result!.Id.Should().Be(new Guid("80AB8942-87F8-4C9A-B16C-94D719AAA7D8"));
            result!.Name.Should().Be("TEST_ACTUATOR");
            result!.SystemName.Should().Be("TEST_ACTUATOR_SYSTEM");
            result!.State.Should().Be(ActuatorState.Disabled);
        }
        
    }
    
    [Fact]
    public async Task GetActuator_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _actuatorsService.GetActuator(new GetActuatorParams()
            {
                Id = Guid.NewGuid()
            }, default);
        });
    }

    #endregion

    #region GetActuators

    [Fact]
    public async Task GetActuators_Success()
    {
        var result = await _actuatorsService.GetActuators(new GetActuatorsParams()
        {
            DeviceId = new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E")
        }, default);

        using (new AssertionScope())
        {
            result.Count().Should().Be(1);
        }
    }
    
    [Fact]
    public async Task GetActuators_EmptyList()
    {
        var result = await _actuatorsService.GetActuators(new GetActuatorsParams()
        {
            DeviceId = Guid.NewGuid()
        }, default);

        using (new AssertionScope())
        {
            result.Count().Should().Be(0);
        }
    }

    #endregion

    #region AddActuator

    [Fact]
    public async Task AddActuator_Success()
    {
        var result = await _actuatorsService.AddActuator(new AddActuatorParams()
        {
            DeviceId = new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E"),
            SystemName = "NEW_TEST_ACTUATOR_SYSTEM",
            Name = "NEW_TEST_ACTUATOR",
        }, default);

        using (new AssertionScope())
        {
            result.SystemName.Should().Be("NEW_TEST_ACTUATOR_SYSTEM");
            result.CreatedDate.Should().Be(result.LastUpdated);
            result.State.Should().Be(ActuatorState.Disabled);
            result.Name.Should().Be("NEW_TEST_ACTUATOR");
        }
    }
    
    [Fact]
    public async Task AddActuator_NotFoundDevice()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _actuatorsService.AddActuator(new AddActuatorParams()
            {
                DeviceId = Guid.NewGuid(),
                SystemName = "NEW_TEST_ACTUATOR_SYSTEM",
                Name = "NEW_TEST_ACTUATOR",
            }, default);
        });
    }

    #endregion

    #region UpdateActuator

    [Fact]
    public async Task UpdateActuator_Success()
    {
        var oldEntity = await _dbContext.Actuators
            .AsNoTracking()
            .Where(x => x.Id == new Guid("80AB8942-87F8-4C9A-B16C-94D719AAA7D8"))
            .FirstOrDefaultAsync();
        
        var result = await _actuatorsService.UpdateActuator(new UpdateActuatorParams
        {
            Id = new Guid("80AB8942-87F8-4C9A-B16C-94D719AAA7D8"),
            Name = "NEW_TEST_NAME_ACTUATOR",
            State = ActuatorState.Reverse
        }, default);

        using (new AssertionScope())
        {
            oldEntity!.Name.Should().Be("TEST_ACTUATOR");
            oldEntity!.State.Should().Be(ActuatorState.Disabled);
            
            result!.Name.Should().Be("NEW_TEST_NAME_ACTUATOR");
            result!.State.Should().Be(ActuatorState.Reverse);
        }
    }
    
    [Fact]
    public async Task UpdateActuator_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _actuatorsService.UpdateActuator(new UpdateActuatorParams
            {
                Id = Guid.NewGuid(),
                Name = "NEW_TEST_NAME_ACTUATOR",
                State = ActuatorState.Reverse
            }, default);
        });
    }

    #endregion

    #region DeleteActuator

    [Fact]
    public async Task DeleteActuator_Success()
    {
        var deviceActuatorsAfter = await _dbContext.Devices
            .AsNoTracking()
            .Include(x => x.Actuators)
            .Where(x => x.Id == new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E"))
            .SelectMany(x => x.Actuators)
            .ToArrayAsync();
        
        var result = await _actuatorsService.DeleteActuator(new DeleteActuatorParams()
        {
            Id = new Guid("80AB8942-87F8-4C9A-B16C-94D719AAA7D8")
        }, default);
        
        var deviceActuatorsBefore = await _dbContext.Devices
            .AsNoTracking()
            .Include(x => x.Actuators)
            .Where(x => x.Id == new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E"))
            .SelectMany(x => x.Actuators)
            .ToArrayAsync();

        using (new AssertionScope())
        {
            deviceActuatorsAfter.Length.Should().Be(1);
            result.Should().BeTrue();
            deviceActuatorsBefore.Length.Should().Be(0);
        }
    }
    
    [Fact]
    public async Task DeleteActuator_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _actuatorsService.DeleteActuator(new DeleteActuatorParams()
            {
                Id = Guid.NewGuid(),
            }, default);
        });
    }

    #endregion
    
    private void Seed()
    {
        var currentDateUserCreate = _dateTimeProvider.GetCurrent();
        var user = new User()
        {
            Id = new Guid("9B232608-249B-4F4A-BB9E-1E04AA2A9A8D"),
            Name = "TEST_USER",
            CreatedDate = currentDateUserCreate,
            LastUpdated = currentDateUserCreate,
            Login = "TEST_USER_LOGIN",
            Password = "TEST_USER_PASSWORD",
        };
        _dbContext.Users.Add(user);

        var currentDateDeviceCreate = _dateTimeProvider.GetCurrent();
        var device = new Device()
        {
            Id = new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E"),
            Name = "TEST_DEVICE",
            UserId = user.Id,
            User = user,
            IsDeleted = false,
            CreatedDate = currentDateDeviceCreate,
            LastUpdated = currentDateDeviceCreate,
            Sensors = new List<Sensor>()
            {
                new()
                {
                    Id = new Guid("E397F002-CB71-453A-A6CE-828CD3C92839"),
                    Name = "TEST_SENSOR",
                    SystemName = "TEST_SENSOR_SYSTEM",
                    CreatedDate = _dateTimeProvider.GetCurrent(),
                    LastUpdated = _dateTimeProvider.GetCurrent(),
                }
            },
            Actuators = new List<Actuator>()
            {
                new()
                {
                    Id = new Guid("80AB8942-87F8-4C9A-B16C-94D719AAA7D8"),
                    Name = "TEST_ACTUATOR",
                    SystemName = "TEST_ACTUATOR_SYSTEM",
                    State = ActuatorState.Disabled
                }
            }
        };
        _dbContext.Devices.Add(device);
        _dbContext.SaveChanges();
    }
}