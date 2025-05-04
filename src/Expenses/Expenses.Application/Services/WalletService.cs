using AutoMapper;
using Expenses.Application.DTOs.Wallet;
using Expenses.Application.Exceptions.Wallet;
using Expenses.Application.Interfaces.Services;
using Expenses.Domain.Entities;
using Expenses.Domain.Interfaces.Repositories;
using Expenses.Domain.SeedWork;

namespace Expenses.Application.Services;

/// <summary>
/// Service for managing Wallet entities.
/// </summary>
public class WalletService : IWalletService
{
    private readonly IWalletRepository _walletRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="WalletService"/> class.
    /// </summary>
    /// <param name="walletRepository">The repository for accessing Wallet entities.</param>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    /// <param name="unitOfWork">The Unit of Work for transactional operations.</param>
    /// <exception cref="ArgumentNullException">Thrown when either <paramref name="walletRepository"/> or <paramref name="mapper"/> is null.</exception>
    public WalletService(IWalletRepository walletRepository, IMapper mapper)
    {
        _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Asynchronously creates a new Wallet.
    /// </summary>
    /// <param name="dto">The DTO containing the Wallet creation data.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, with the created <see cref="WalletDto"/>.</returns>
    public async Task<WalletDto?> CreateWalletAsync(CreateWalletDto dto)
    {
        var wallet = _mapper.Map<Wallet>(dto);
        await _walletRepository.AddAsync(wallet);
        await _walletRepository.UnitOfWork.SaveEntitiesAsync();
        return _mapper.Map<WalletDto>(wallet);
    }

    /// <summary>
    /// Asynchronously retrieves a Wallet by its ID.
    /// </summary>
    /// <param name="id">The ID of the Wallet to retrieve.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, with the retrieved <see cref="WalletDto"/>.</returns>
    /// <exception cref="WalletNotFoundException">Thrown when a Wallet with the specified ID is not found.</exception>
    public async Task<WalletDto?> GetWalletByIdAsync(Guid id)
    {
        var wallet = await _walletRepository.GetByIdAsync(id) ?? throw new WalletNotFoundException(id);
        return _mapper.Map<WalletDto>(wallet);
    }

    /// <summary>
    /// Asynchronously retrieves all Wallets.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a collection of <see cref="WalletDto"/>.</returns>
    public async Task<IEnumerable<WalletDto>> GetAllWalletsAsync()
    {
        var wallets = await _walletRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<WalletDto>>(wallets);
    }

    /// <summary>
    /// Asynchronously updates a Wallet.
    /// </summary>
    /// <param name="dto">The DTO containing the Wallet update data.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="WalletNotFoundException">Thrown when a Wallet with the specified ID is not found.</exception>
    /// <exception cref="WalletUpdateException">Thrown when an error occurs during the Wallet update.</exception>
    public async Task UpdateWalletAsync(Guid authorId, UpdateWalletDto dto)
    {
        try
        {
            var wallet = await _walletRepository.GetByIdAsync(authorId) ?? throw new WalletNotFoundException(authorId);
            _mapper.Map(dto, wallet);
            await _walletRepository.UpdateAsync(wallet);
            await _walletRepository.UnitOfWork.SaveEntitiesAsync();
        }
        catch (WalletNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new WalletUpdateException(authorId, "An error occurred while updating the wallet.", ex);
        }
    }

    /// <summary>
    /// Asynchronously deletes a Wallet by its ID.
    /// </summary>
    /// <param name="id">The ID of the Wallet to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="WalletNotFoundException">Thrown when a Wallet with the specified ID is not found.</exception>
    /// <exception cref="WalletDeleteException">Thrown when an error occurs during the Wallet deletion.</exception>
    public async Task DeleteWalletAsync(Guid id)
    {
        try
        {
            var wallet = await _walletRepository.GetByIdAsync(id) ?? throw new WalletNotFoundException(id);
            await _walletRepository.DeleteAsync(wallet);
            await _walletRepository.UnitOfWork.SaveEntitiesAsync();
        }
        catch (WalletNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new WalletDeleteException(id, "An error occurred while deleting the wallet.", ex);
        }
    }
}