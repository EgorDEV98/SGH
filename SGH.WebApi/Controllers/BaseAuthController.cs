using Microsoft.AspNetCore.Mvc;

namespace SGH.WebApi.Controllers;

public class BaseAuthController : ControllerBase
{
    protected Guid UserId { get; }

    public BaseAuthController()
    {
        var claim = User.Claims.First(x => x.Type == nameof(Data.Entities.User.Id)).Value;
        UserId = Guid.Parse(claim);
    }
}