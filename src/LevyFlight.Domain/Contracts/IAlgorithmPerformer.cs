using System.Threading.Tasks;
using LevyFlight.Entities;

namespace LevyFlight.Domain.Contracts
{
    internal interface IAlgorithmPerformer
    {
        Task<Pollinator> PolinateAsync();
    }
}
