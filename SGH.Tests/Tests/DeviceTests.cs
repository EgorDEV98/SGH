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

public class DeviceTests
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly PostgresDbContext _dbContext;
    private readonly DeviceService _deviceService;
    
    public DeviceTests()
    {
        _dateTimeProvider = new DateTimeProvider();
        _dbContext = PostgresMock.Create();
        _deviceService = new DeviceService(_dbContext, new DeviceMapper(), _dateTimeProvider);

        Seed();
    }

    #region GetDevice

    [Fact]
    public async Task GetDevice_Success()
    {
        var result = await _deviceService.GetDevice(new GetDeviceParams
        {
            UserId = new Guid("9B232608-249B-4F4A-BB9E-1E04AA2A9A8D"),
            DeviceId = new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E"),
        }, default);

        using (new AssertionScope())
        {
            result.Id.Should().Be(new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E"));
            result.Name.Should().Be("TEST_DEVICE");
            result.Actuators.Count.Should().Be(1);
            result.Sensors.Count.Should().Be(1);
        }
    }
    
    [Fact]
    public async Task GetDevice_DeviceNotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _deviceService.GetDevice(new GetDeviceParams
            {
                UserId = new Guid("9B232608-249B-4F4A-BB9E-1E04AA2A9A8D"),
                DeviceId = Guid.NewGuid(),
            }, default);
        });
    }
    
    [Fact]
    public async Task GetDevice_DeviceUserNotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _deviceService.GetDevice(new GetDeviceParams
            {
                UserId = Guid.NewGuid(),
                DeviceId = new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E"),
            }, default);
        });
    }

    #endregion

    #region GetDevices

    [Fact]
    public async Task GetDevices_Success()
    {
        var result = await _deviceService.GetDevices(new GetDevicesParams()
        {
            UserId = new Guid("9B232608-249B-4F4A-BB9E-1E04AA2A9A8D")
        }, default);

        using (new AssertionScope())
        {
            result.Count.Should().Be(1);
        }
    }
    
    [Fact]
    public async Task GetDevices_NotFound()
    {
        var result = await _deviceService.GetDevices(new GetDevicesParams()
        {
            UserId = Guid.NewGuid()
        }, default);

        using (new AssertionScope())
        {
            result.Count.Should().Be(0);
        }
    }

    #endregion
    
    #region AddDevice

    [Fact]
    public async Task AddDevice_ToUserSuccess()
    {
        var result = await _deviceService.AddDevice(new AddDeviceParams
        {
            UserId = new Guid("9B232608-249B-4F4A-BB9E-1E04AA2A9A8D"),
            Name = "TEST_DEVICE_2",
        }, default);

        var allUserDevices = await _dbContext.Devices.CountAsync();

        using (new AssertionScope())
        {
            result.Name.Should().Be("TEST_DEVICE_2");
            allUserDevices.Should().BeGreaterThan(1);
            result.LastUpdated.Should().Be(result.CreatedDate);
        }
    }
    
    [Fact]
    public async Task AddDevice_WithoutNameSuccess()
    {
        var result = await _deviceService.AddDevice(new AddDeviceParams
        {
            UserId = new Guid("9B232608-249B-4F4A-BB9E-1E04AA2A9A8D"),
        }, default);

        var allUserDevices = await _dbContext.Devices.CountAsync();

        using (new AssertionScope())
        {
            result.Name.Should().Be("Новое устройство");
            allUserDevices.Should().BeGreaterThan(1);
        }
    }
    
    [Fact]
    public async Task AddDevice_UserNotExist()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _deviceService.AddDevice(new AddDeviceParams
            {
                UserId = Guid.NewGuid(),
                Name = "TEST_DEVICE_2",
            }, default);
        });
    }
    
    #endregion

    #region UpdateDevice

    [Fact]
    public async Task UpdateDevice_Success()
    {
        // Проверяем прошлое название 
        var entity = await _dbContext.Devices
            .Where(x => x.UserId == new Guid("9B232608-249B-4F4A-BB9E-1E04AA2A9A8D"))
            .Where(x => x.Id == new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E"))
            .FirstOrDefaultAsync();
        using (new AssertionScope())
        {
            entity!.Name.Should().Be("TEST_DEVICE");
        }
        
        // Изменяем и проверяем, что имя поменялось и дата последнего обновления тоже
        var result = await _deviceService.UpdateDevice(new UpdateDeviceParams
        {
            UserId = new Guid("9B232608-249B-4F4A-BB9E-1E04AA2A9A8D"),
            DeviceId = new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E"),
            Name = "NEW_DEVICE_NAME",
        }, default);

        using (new AssertionScope())
        {
            result!.Name.Should().Be("NEW_DEVICE_NAME");
            result.CreatedDate.Should().NotBe(result.LastUpdated);
        }
    }
    
    [Fact]
    public async Task UpdateDevice_DeviceNotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _deviceService.UpdateDevice(new UpdateDeviceParams
            {
                UserId = new Guid("9B232608-249B-4F4A-BB9E-1E04AA2A9A8D"),
                DeviceId = Guid.NewGuid(),
                Name = "NEW_DEVICE_NAME",
            }, default);
        });
    }
    
    [Fact]
    public async Task UpdateDevice_UserNotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _deviceService.UpdateDevice(new UpdateDeviceParams
            {
                UserId = Guid.NewGuid(),
                DeviceId = new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E"),
                Name = "NEW_DEVICE_NAME",
            }, default);
        });
    }

    #endregion

    #region DeleteDevice

    [Fact]
    public async Task DeleteDevice_Success()
    {
        // Удаляем устройство
        var result = await _deviceService.DeleteDevice(new DeleteDeviceParams
        {
            UserId = new Guid("9B232608-249B-4F4A-BB9E-1E04AA2A9A8D"),
            DeviceId = new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E")
        }, default);

        // Проверяем что у пользователя его действительно нет
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _deviceService.GetDevice(new GetDeviceParams()
            {
                UserId = new Guid("9B232608-249B-4F4A-BB9E-1E04AA2A9A8D"),
                DeviceId = new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E")
            }, default);
        });
        
        // Получаем эту зарядку из БД
        var userDeviceInDb = await _dbContext.Devices
            .IgnoreQueryFilters()
            .Where(x => x.UserId == new Guid("9B232608-249B-4F4A-BB9E-1E04AA2A9A8D"))
            .Where(x => x.Id == new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E"))
            .FirstOrDefaultAsync();

        using (new AssertionScope())
        {
            result.Should().Be(true);
            userDeviceInDb.Should().NotBeNull();
            userDeviceInDb.IsDeleted.Should().Be(true);
            userDeviceInDb.DeleteDate.Should().NotBe(null);
        }
    }

    [Fact]
    public async Task DeleteDevice_DeviceNotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _deviceService.DeleteDevice(new DeleteDeviceParams
            {
                UserId = new Guid("9B232608-249B-4F4A-BB9E-1E04AA2A9A8D"),
                DeviceId = Guid.NewGuid(),
            }, default);
        });
    }
    
    [Fact]
    public async Task DeleteDevice_UserNotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _deviceService.DeleteDevice(new DeleteDeviceParams
            {
                UserId = Guid.NewGuid(),
                DeviceId = new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E"),
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