namespace FreeCourse.Services.Basket.Dtos;

public class BasketDto
{
    public BasketDto()
    {
        BasketItems = new List<BasketItemDto>();
    }

    public string UserId { get; set; }
    public string DiscountCode { get; set; }
    public List<BasketItemDto> BasketItems { get; set; }

    public decimal TotalPrice => BasketItems.Sum(p => p.Price * p.Quantity);
}