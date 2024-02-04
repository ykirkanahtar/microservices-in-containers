using FreeCourse.Basket.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Basket.Services;

public interface IBasketService
{
    Task<Response<BasketDto>> GetBasket(string userId);

    Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);

    Task<Response<bool>> Delete(string userId);
}