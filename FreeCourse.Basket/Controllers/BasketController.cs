using FreeCourse.Basket.Dtos;
using FreeCourse.Basket.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Basket.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketController : CustomBaseController
{
    private readonly IBasketService _basketService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public BasketController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
    {
        _basketService = basketService;
        _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBasket()
    {
        var data = await _basketService.GetBasket(_sharedIdentityService.GetUserId);
        return CreateActionResultInstance(data);
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
    {
        var response = await _basketService.SaveOrUpdate(basketDto);
        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasket()
    {
        return CreateActionResultInstance(await _basketService.Delete(_sharedIdentityService.GetUserId));
    }
}