using AoC.Common.Input;

namespace AoC2023.Day20;

/// <summary>
/// Parser for <see cref="ModuleCollection"/>.
/// </summary>
public class ModuleCollectionParser : InputParser<ModuleCollection>
{
    private const char Conjunction = '&';
    private const char FlipFlop = '%';
    private const string Broadcaster = "broadcaster";

    /// <summary>
    /// Constructs a new instance of <see cref="ModuleCollectionParser"/>.
    /// </summary>
    /// <param name="filename">The input file name.</param>
    public ModuleCollectionParser(string filename) : base(filename) { }

    /// <inheritdoc cref="InputParser{T}.Parse"/>
    public override async Task<ModuleCollection> Parse(CancellationToken token = default)
    {
        var modules = new Dictionary<string, Module>();
        await foreach (var line in GetNonEmptyLines(token))
        {
            var module = ParseModule(line);
            modules[module.Name] = module;
        }

        UpdateConjunctionMemory(modules);
        return new ModuleCollection(modules);
    }

    /// <summary>
    /// Updates the initial memory for conjunction modules.
    /// </summary>
    /// <param name="modules">The module mappings.</param>
    private static void UpdateConjunctionMemory(Dictionary<string, Module> modules)
    {
        foreach (var (name, module) in modules)
        {
            foreach (var destination in module.Destinations)
            {
                if (modules.TryGetValue(destination, out var m) && m.Type == ModuleType.Conjunction)
                    m.InitialiseConjunctionMemory(name);
            }
        }
    }

    /// <summary>
    /// Parses a module and returns the parsed module.
    /// </summary>
    /// <param name="line">The line containing the module.</param>
    /// <returns>The parsed module.</returns>
    private static Module ParseModule(string line)
    {
        var arrowSplits = line.Split("->", SplitOpt);
        var destinations = arrowSplits[1]
            .Split(", ", SplitOpt)
            .Select(GetStrippedModuleName)
            .ToList();

        var name = GetStrippedModuleName(arrowSplits[0]);
        return new Module(name, destinations, GetModuleType(arrowSplits[0]));
    }

    /// <summary>
    /// Returns the module type.
    /// </summary>
    /// <param name="name">The name of the module.</param>
    /// <returns>The module type.</returns>
    private static ModuleType GetModuleType(string name)
    {
        return name[0] switch
        {
            FlipFlop => ModuleType.FlipFlop,
            Conjunction => ModuleType.Conjunction,
            _ => name == Broadcaster ? ModuleType.Broadcaster : ModuleType.Unnamed
        };
    }

    /// <summary>
    /// Returns the module name without any prefixes.
    /// </summary>
    /// <param name="name">The full name of the module.</param>
    /// <returns>The module name without any prefixes.</returns>
    private static string GetStrippedModuleName(string name)
    {
        return name[0] == FlipFlop || name[0] == Conjunction ? name[1..] : name;
    }
}
