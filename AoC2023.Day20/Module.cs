namespace AoC2023.Day20;

/// <summary>
/// Represents a module.
/// </summary>
public class Module
{
    private readonly Dictionary<string, Pulse> _previousPulses = new();
    private bool _turnedOn;

    /// <summary>
    /// Gets the module name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the list of destination modules.
    /// </summary>
    public IReadOnlyCollection<string> Destinations { get; }

    /// <summary>
    /// Gets the module type.
    /// </summary>
    public ModuleType Type { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Module"/>.
    /// </summary>
    /// <param name="name">The module name.</param>
    /// <param name="destinations">The list of destination modules.</param>
    /// <param name="type">The module type.</param>
    public Module(string name, IReadOnlyCollection<string> destinations, ModuleType type)
    {
        Name = name;
        Destinations = destinations;
        Type = type;
    }

    /// <summary>
    /// Determines whether the pulse can be sent into this module.
    /// </summary>
    /// <param name="pulse">The pulse to send.</param>
    /// <returns>Whether a pulse can be sent into this module.</returns>
    public bool CanSendPulse(Pulse pulse)
    {
        return Type != ModuleType.FlipFlop || pulse != Pulse.High;
    }

    /// <summary>
    /// Sends a pulse into this module.
    /// </summary>
    /// <param name="input">The input module name.</param>
    /// <param name="pulse">The pulse to send.</param>
    /// <returns>Whether the pulse is successfully sent and the output pulse.</returns>
    public Pulse SendPulse(string input, Pulse pulse)
    {
        return Type switch
        {
            ModuleType.Broadcaster => HandlePulseForBroadcaster(),
            ModuleType.Conjunction => HandlePulseForConjunction(input, pulse),
            ModuleType.FlipFlop => HandlePulseForFlipFlop(),
            _ => pulse
        };
    }

    /// <summary>
    /// Initialises the memory for a conjunction module.
    /// </summary>
    /// <param name="module">The input module to initialise.</param>
    public void InitialiseConjunctionMemory(string module)
    {
        _previousPulses[module] = Pulse.Low;
    }

    /// <summary>
    /// Handles the pulse for broadcaster module.
    /// </summary>
    /// <returns>The output pulse.</returns>
    private static Pulse HandlePulseForBroadcaster()
    {
        return Pulse.Low;
    }

    /// <summary>
    /// Handles the pulse for conjunction module.
    /// </summary>
    /// <param name="input">The input module name.</param>
    /// <param name="pulse">The input pulse.</param>
    /// <returns>The output pulse.</returns>
    private Pulse HandlePulseForConjunction(string input, Pulse pulse)
    {
        _previousPulses[input] = pulse;
        return _previousPulses.Values.All(x => x == Pulse.High) ? Pulse.Low : Pulse.High;
    }

    /// <summary>
    /// Handles the pulse for flip-flop module.
    /// Assumes low pulse is sent.
    /// </summary>
    /// <returns>The output pulse.</returns>
    private Pulse HandlePulseForFlipFlop()
    {
        _turnedOn = !_turnedOn;
        return _turnedOn ? Pulse.High : Pulse.Low;
    }
}
