using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services;

public class CourseService : ICourseService
{
    private readonly IMongoCollection<Course> _courseCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CourseService(IMapper mapper,
        IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        
        _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

        _mapper = mapper;
    }

    public async Task<Response<List<CourseDto>>> GetAllAsync()
    {
        var courses = await _courseCollection.Find(course => true).ToListAsync();
        
        if (courses.Any())
        {
            foreach (var course in courses)
            {
                course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
            }
        }
        else
        {
            courses = new List<Course>();
        }
        
        return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
    }

    public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto input)
    {
        var course = _mapper.Map<Course>(input);
        course.CreatedTime = DateTime.Now;
        
        await _courseCollection.InsertOneAsync(course);

        return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
    }
    
    public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto input)
    {
        var course = _mapper.Map<Course>(input);
        
        var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == input.Id, course);
        
        if (result == null)
        {
            return Response<NoContent>.Fail("Course not found", 404);
        }
        
        return Response<NoContent>.Success(204);
    }
    
    public async Task<Response<NoContent>> DeleteAsync(string id)
    {
        var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);
        
        if (result.DeletedCount > 0)
        {
            return Response<NoContent>.Success(204);
        }
        else
        {
            return Response<NoContent>.Fail("Course not found", 404);
        }
    }
    
    public async Task<Response<CourseDto>> GetByIdAsync(string id)
    {
        var course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        if (course == null)
        {
            return Response<CourseDto>.Fail("Course not found", 404);
        }

        course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
        
        return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
    }

    public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
    {
        var courses = await _courseCollection.Find(x => x.UserId == userId).ToListAsync();

        if (courses.Any())
        {
            foreach (var course in courses)
            {
                course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
            }
        }
        else
        {
            courses = new List<Course>();
        }
        
        return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
    }
}