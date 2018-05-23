using System.Threading.Tasks;
using LevyFlight.Entities;

namespace LevyFlight.Domain.Modified.Rules
{
    public interface IRule<in TRuleArgument>
    {
        Task<Pollinator> ApplyRuleAsync(Pollinator pollinator, TRuleArgument ruleArgument);
    }
}
