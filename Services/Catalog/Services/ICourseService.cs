using FreeCourse.Catalog.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Catalog.Services;

public interface ICourseService
{
    Task<Response<List<CourseDto>>> GetAllAsync();
    Task<Response<CourseDto>> CreateAsync(CourseCreateDto input);
    Task<Response<NoContent>> UpdateAsync(CourseUpdateDto input);
    Task<Response<NoContent>> DeleteAsync(string id);
    Task<Response<CourseDto>> GetByIdAsync(string id);
    Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId);
}