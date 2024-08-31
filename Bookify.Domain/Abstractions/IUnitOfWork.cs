using System.Threading;
using System.Threading.Tasks;

namespace Bookify.Domain.Abstractions;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}