using AutoMapper;
using Expenses.Application.DTOs.Category;
using Expenses.Application.Exceptions.Author;
using Expenses.Application.Exceptions.Category;
using Expenses.Application.Interfaces.Services;
using Expenses.Domain.Interfaces.Factories;
using Expenses.Domain.Interfaces.Repositories;
using System.Drawing;

namespace Expenses.Application.Services;

/// <summary>
/// Service for managing Category entities.
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly ICategoryFactory _categoryFactory;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryService"/> class.
    /// </summary>
    /// <param name="categoryRepository">The repository for accessing Category entities.</param>
    /// <param name="authorRepository">The repository for accessing Author entities.</param>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
    public CategoryService(ICategoryFactory categoryFactory, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryFactory = categoryFactory ?? throw new ArgumentNullException(nameof(categoryFactory));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Asynchronously creates a new Category.
    /// </summary>
    /// <param name="dto">The DTO containing the Category creation data.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, with the created <see cref="CategoryDto"/>, or null if creation failed.</returns>
    /// <exception cref="AuthorNotFoundException">Thrown when the specified author is not found.</exception>
    public async Task<CategoryDto?> CreateCategoryAsync(CreateCategoryDto dto)
    {
        var colorDto = _mapper.Map<Color>(dto.Color);
        var category = await _categoryFactory.CreateCategoryAsync(dto.Name, dto.AuthorId, colorDto);

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.UnitOfWork.SaveEntitiesAsync();
        return _mapper.Map<CategoryDto>(category);
    }

    /// <summary>
    /// Asynchronously retrieves a Category by its ID.
    /// </summary>
    /// <param name="id">The ID of the Category to retrieve.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, with the retrieved <see cref="CategoryDto"/>, or null if not found.</returns>
    public async Task<CategoryDto?> GetCategoryByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        return category is null
            ? null
            : _mapper.Map<CategoryDto>(category);
    }

    /// <summary>
    /// Asynchronously retrieves all Categories.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a collection of <see cref="CategoryDto"/>.</returns>
    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    /// <summary>
    /// Asynchronously updates a Category.
    /// </summary>
    /// <param name="id">The ID of the Category to update.</param>
    /// <param name="dto">The DTO containing the updated Category data.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="CategoryNotFoundException">Thrown when the specified category is not found.</exception>
    /// <exception cref="CategoryUpdateException">Thrown when an error occurs during the Category update.</exception>
    public async Task UpdateCategoryAsync(Guid id, UpdateCategoryDto dto)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id) ?? throw new CategoryNotFoundException(id);
            var colorDto = _mapper.Map<Color>(dto.Color);
            category.Update(dto.Name, colorDto);

            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.UnitOfWork.SaveEntitiesAsync();
        }
        catch (CategoryNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new CategoryUpdateException(id, "An error occurred while updating the category.", ex);
        }
    }

    /// <summary>
    /// Asynchronously deletes a Category by its ID.
    /// </summary>
    /// <param name="id">The ID of the Category to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="CategoryNotFoundException">Thrown when the specified category is not found.</exception>
    /// <exception cref="CategoryDeleteException">Thrown when an error occurs during the Category deletion.</exception>
    public async Task DeleteCategoryAsync(Guid id)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id) ?? throw new CategoryNotFoundException(id);
            await _categoryRepository.DeleteAsync(category);
            await _categoryRepository.UnitOfWork.SaveEntitiesAsync();
        }
        catch (CategoryNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new CategoryDeleteException(id, "An error occurred while deleting the category.", ex);
        }
    }
}