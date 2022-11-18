using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Mammut.Web.Infrastructure;

public class LowercaseParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return value?.ToString()?.ToLowerInvariant();
    }
}

public class LowercasePageRoutingConvention : IPageRouteModelConvention
{
    readonly IReadOnlyList<string> _lowercasedTokenNames;

    public LowercasePageRoutingConvention(params string[] lowercasedTokenNames)
    {
        _lowercasedTokenNames = lowercasedTokenNames;
    }

    public void Apply(PageRouteModel model)
    {
        ApplyConstraintToAllInstancesOfToken(model.Selectors, _lowercasedTokenNames, "tolower");
    }

    static void ApplyConstraintToAllInstancesOfToken(IEnumerable<SelectorModel> selectors, IEnumerable<string> tokenNames, string constraint)
    {
        foreach (var selector in selectors)
        {
            if (selector.AttributeRouteModel is { Template: { Length: > 0 } template })
            {
                foreach (var tokenName in tokenNames)
                {
                    template = template
                        .Replace($"{{{tokenName}}}", $"{{{tokenName}:{constraint}}}", StringComparison.OrdinalIgnoreCase)
                        .Replace($"{{{tokenName}?}}", $"{{{tokenName}:{constraint}?}}", StringComparison.OrdinalIgnoreCase);
                }
                selector.AttributeRouteModel.Template = template;
            }
        }
    }
}
