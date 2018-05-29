using System.Threading.Tasks;
using LevyFlight.Entities;

namespace LevyFlight.Domain.Rules
{
    public interface IRule<in TRuleArgument>
    {
        Task<Pollinator> ApplyRuleAsync(Pollinator pollinator, TRuleArgument ruleArgument);
    }
}
