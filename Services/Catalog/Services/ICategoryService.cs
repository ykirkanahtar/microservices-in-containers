using FreeCourse.Catalog.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Catalog.Services;

public interface ICategoryService
{
    Task<Response<List<CategoryDto>>> GetAllAsync();
    Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto input);
    Task<Response<CategoryDto>> GetByIdAsync(string id);
}