﻿namespace SchemaCompare;

public class RuleEngine
{
    private readonly List<IOutputTypeRule> _rules = new()
    {
        new NoMissingFields(),
        new OutputFieldIsNoLongerMandatory(),
        new OutputFieldTypeChanged(),
        new IllegalOperationInputChange(),
    };

    private readonly List<IInputTypeRule> _inputTypeRules = new()
    {
        new InputFieldIsNoLongerOptional(),
        new NoMissingInputField(),
    };

    public BreakingChange? ApplyAllRules(OutputFieldChange outputFieldChange) =>
        _rules.Select(r => r.ApplyRule(outputFieldChange))
            .FirstOrDefault(b => b != null);

    public BreakingChange? ApplyAllRulesToInputTypes(InputFieldChange fieldChange) =>
        _inputTypeRules.Select(r => r.ApplyRule(fieldChange))
            .FirstOrDefault(b => b != null);
}
