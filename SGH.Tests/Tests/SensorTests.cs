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

public class SensorTests
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly PostgresDbContext _dbContext;
    private readonly SensorsService _sensorsService;
    
    public SensorTests()
    {
        _dateTimeProvider = new DateTimeProvider();
        _dbContext = PostgresMock.Create();
        _sensorsService = new SensorsService(_dbContext, new SensorMapper(), _dateTimeProvider);

        Seed();
    }

    #region GetSensor

    [Fact]
    public async Task GetSensor_Success()
    {
        var result = await _sensorsService.GetSensor(new GetSensorParams
        {
            Id = new Guid("E397F002-CB71-453A-A6CE-828CD3C92839"),
        }, default);

        using (new AssertionScope())
        {
            result.Id.Should().Be(new Guid("E397F002-CB71-453A-A6CE-828CD3C92839"));
            result.Name.Should().Be("TEST_SENSOR");
        }
    }
    
    [Fact]
    public async Task GetSensor_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _sensorsService.GetSensor(new GetSensorParams
            {
                Id = Guid.NewGuid(),
            }, default);
        });
    }

    #endregion

    #region GetSensors

    [Fact]
    public async Task GetSensors_Success()
    {
        var result = await _sensorsService.GetSensors(new GetSensorsParams()
        {
            DeviceId = new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E")
        }, default);

        using (new AssertionScope())
        {
            result.Count.Should().Be(1);
            result.First().Id.Should().Be(new Guid("E397F002-CB71-453A-A6CE-828CD3C92839"));
        }
    }
    
    [Fact]
    public async Task GetSensors_EmptyArray()
    {
        var result = await _sensorsService.GetSensors(new GetSensorsParams()
        {
            DeviceId = new Guid("AEEAE32D-ADC6-40B9-BE37-CE8625CA7526")
        }, default);

        using (new AssertionScope())
        {
            result.Count.Should().Be(0);
        }
    }
    
    [Fact]
    public async Task GetSensors_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _sensorsService.GetSensors(new GetSensorsParams()
            {
                DeviceId = Guid.NewGuid()
            }, default);
        });
    }

    #endregion
    
    #region AddSensor

    [Fact]
    public async Task AddSensor_Success()
    {
        var countSensorsBeforeAdd = await _dbContext.Sensors
            .AsNoTracking()
            .Where(x => x.DeviceId == new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E"))
            .CountAsync();
        
        var result = await _sensorsService.AddSensor(new AddSensorParams()
        {
            Name = "TEST_SENSOR",
            DeviceId = new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E"),
            SystemName = "TEST_SENSOR_SYSTEM",
        }, default);
        
        var countSensorsAfterAdd = await _dbContext.Sensors
            .AsNoTracking()
            .Where(x => x.DeviceId == new Guid("9716786A-7A14-4A7D-BCD0-7F27BDCC886E"))
            .CountAsync();

        using (new AssertionScope())
        {
            result.CreatedDate.Should().Be(result.LastUpdated);
            result.SystemName.Should().Be("TEST_SENSOR_SYSTEM");
            result.Name.Should().Be("TEST_SENSOR");
            countSensorsAfterAdd.Should().BeGreaterThan(countSensorsBeforeAdd);
        }
    }
    
    [Fact]
    public async Task AddSensor_DeviceNotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _sensorsService.AddSensor(new AddSensorParams()
            {
                Name = "TEST_SENSOR",
                DeviceId = Guid.NewGuid(),
                SystemName = "TEST_SENSOR_SYSTEM",
            }, default);
        });
    }
    
    #endregion

    #region UpdateSensor

    [Fact]
    public async Task UpdateSensor_Success()
    {
        var entityBeforeUpdate = await _dbContext.Sensors
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == new Guid("E397F002-CB71-453A-A6CE-828CD3C92839"));

        var result = await _sensorsService.UpdateSensor(new UpdateSensorParams()
        {
            Id = new Guid("E397F002-CB71-453A-A6CE-828CD3C92839"),
            Name = "NEW_TEST_NAME"
        }, default);
        
        var entityAfterUpdate = await _dbContext.Sensors
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == new Guid("E397F002-CB71-453A-A6CE-828CD3C92839"));

        using (new AssertionScope())
        {
            entityBeforeUpdate!.Name.Should().Be("TEST_SENSOR");
            result.Name.Should().Be("NEW_TEST_NAME");
            result.SystemName.Should().Be("TEST_SENSOR_SYSTEM");
            entityAfterUpdate!.Name.Should().Be("NEW_TEST_NAME");
            entityAfterUpdate.LastUpdated.Should().BeAfter(entityBeforeUpdate.CreatedDate);
        }
    }
    
    [Fact]
    public async Task UpdateSensor_SensorNotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _sensorsService.UpdateSensor(new UpdateSensorParams()
            {
                Id = Guid.NewGuid(),
                Name = "NEW_TEST_NAME"
            }, default);
        });
    }

    #endregion
    
    #region DeleteSensor

    [Fact]
    public async Task DeleteSensor_Success()
    {
        var result = await _sensorsService.DeleteSensor(new DeleteSensorParams()
        {
            Id = new Guid("E397F002-CB71-453A-A6CE-828CD3C92839")
        }, default);

        var sensorInDb = await _dbContext.Sensors
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == new Guid("E397F002-CB71-453A-A6CE-828CD3C92839"));

        using (new AssertionScope())
        {
            result.Should().BeTrue();
            sensorInDb.Should().NotBeNull();
            sensorInDb.Id.Should().Be(new Guid("E397F002-CB71-453A-A6CE-828CD3C92839"));
            sensorInDb.IsDeleted.Should().BeTrue();
            sensorInDb.DeleteDate.Should().NotBe(null);
            sensorInDb.DeleteDate.Should().Be(sensorInDb.LastUpdated);
        }
    }
    
    [Fact]
    public async Task DeleteSensor_SensorNotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _sensorsService.DeleteSensor(new DeleteSensorParams()
            {
                Id = Guid.NewGuid(),
            }, default);
        });
    }
    
    #endregion
    
    #region GetSensors

    [Fact]
    public async Task GetSensorValues_Success()
    {
        var result = await _sensorsService.GetSensorValues(new GetSensorValuesParams()
        {
            Id = new Guid("E397F002-CB71-453A-A6CE-828CD3C92839")
        }, default);

        using (new AssertionScope())
        {
            result.Count.Should().Be(3);
        }
    }
    
    [Fact]
    public async Task GetSensorValues_Success_WithDateParams()
    {
        var result = await _sensorsService.GetSensorValues(new GetSensorValuesParams()
        {
            Id = new Guid("E397F002-CB71-453A-A6CE-828CD3C92839"),
            From = new DateTime(2025, 1 , 1),
            To = new DateTime(2025, 1 , 2)
        }, default);

        using (new AssertionScope())
        {
            result.Count.Should().Be(2);
        }
    }
    
    [Fact]
    public async Task GetSensorValues_Success_WithLimits()
    {
        var result = await _sensorsService.GetSensorValues(new GetSensorValuesParams()
        {
            Id = new Guid("E397F002-CB71-453A-A6CE-828CD3C92839"),
            From = new DateTime(2025, 1 , 1),
            To = new DateTime(2025, 1 , 2),
            Offset = 0,
            Limit = 1
        }, default);

        using (new AssertionScope())
        {
            result.Count.Should().Be(1);
        }
    }
    
    [Fact]
    public async Task GetSensorValues_SensorNotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _sensorsService.GetSensorValues(new GetSensorValuesParams()
            {
                Id = Guid.NewGuid(),
            }, default);
        });
    }
    
     
    [Fact]
    public async Task GetSensorValues_EmptyArray()
    {
        var result = await _sensorsService.GetSensorValues(new GetSensorValuesParams()
        {
            Id = new Guid("3CE1648C-3A37-4F11-A59F-CD10E27CD1BB"),
            From = new DateTime(2025, 1 , 1),
            To = new DateTime(2025, 1 , 2),
            Offset = 0,
            Limit = 1
        }, default);

        using (new AssertionScope())
        {
            result.Count.Should().Be(0);
        }
    }
    
    
    #endregion

    #region AddSensorValue

    [Fact]
    public async Task AddSensorValue_Success()
    {
        var countSensorBeforeValues = await _dbContext.SensorValues
            .Where(x => x.SensorId == new Guid("E397F002-CB71-453A-A6CE-828CD3C92839"))
            .CountAsync();
        
        var result = await _sensorsService.AddSensorValue(new AddSensorValueParams()
        {
            Id = new Guid("E397F002-CB71-453A-A6CE-828CD3C92839"),
            Value = 22.2F,
            MeasurementDate = DateTime.Now,
        }, default );

        var countSensorAfterValues = await _dbContext.SensorValues
            .Where(x => x.SensorId == new Guid("E397F002-CB71-453A-A6CE-828CD3C92839"))
            .CountAsync();

        using (new AssertionScope())
        {
            result.Should().BeTrue();
            countSensorAfterValues.Should().BeGreaterThan(countSensorBeforeValues);
        }
    }
    
    [Fact]
    public async Task AddSensorValue_SensorNotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _sensorsService.AddSensorValue(new AddSensorValueParams()
            {
                Id = Guid.NewGuid(),
                Value = 22.2F,
                MeasurementDate = DateTime.Now,
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
                    Values = new List<SensorValue>()
                    {
                        new SensorValue()
                        {
                            Id = new Guid("5499B1DD-B75D-495B-B5F9-78C473200595"),
                            Value = 1.11F,
                            MeasurementDate = new DateTime(2025, 1 , 1)
                        },
                        new SensorValue()
                        {
                            Id = new Guid("C6A2B465-0CE2-4891-97FA-BE8DD9C4A1B6"),
                            Value = 2.22F,
                            MeasurementDate = new DateTime(2025, 1 ,2)
                        },
                        new SensorValue()
                        {
                            Id = new Guid("81E39BCE-89C4-438F-8647-F92C44B10621"),
                            Value = 3.33F,
                            MeasurementDate = new DateTime(2025, 1 , 3)
                        },
                    }
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
        
        var device2 = new Device()
        {
            Id = new Guid("AEEAE32D-ADC6-40B9-BE37-CE8625CA7526"),
            Name = "TEST_DEVICE",
            UserId = user.Id,
            User = user,
            IsDeleted = false,
            CreatedDate = currentDateDeviceCreate,
            LastUpdated = currentDateDeviceCreate,
            Sensors = new List<Sensor>(),
            Actuators = new List<Actuator>()
        };
        
        var device3 = new Device()
        {
            Id = new Guid("5C63EDAB-BB19-4ACF-9FE9-92CEB6EC3E6E"),
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
                    Id = new Guid("3CE1648C-3A37-4F11-A59F-CD10E27CD1BB"),
                    Name = "TEST_SENSOR",
                    SystemName = "TEST_SENSOR_SYSTEM",
                    CreatedDate = _dateTimeProvider.GetCurrent(),
                    LastUpdated = _dateTimeProvider.GetCurrent(),
                    Values = new List<SensorValue>()
                }
            },
            Actuators = new List<Actuator>()
        };
        _dbContext.Devices.AddRange(device, device2, device3);
        _dbContext.SaveChanges();
    }
}