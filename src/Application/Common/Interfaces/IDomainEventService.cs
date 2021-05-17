using System.Threading.Tasks;
using TestCA.Domain.Common;

namespace TestCA.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
