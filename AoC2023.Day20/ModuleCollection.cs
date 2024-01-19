using AoC.Common.Helpers;

namespace AoC2023.Day20;

/// <summary>
/// Represents a collection of modules.
/// </summary>
public class ModuleCollection
{
    private const string Broadcaster = "broadcaster";
    private readonly string _firstLayer;

    /// <summary>
    /// Gets the collection of modules.
    /// </summary>
    public IReadOnlyDictionary<string, Module> Modules { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="ModuleCollection"/>.
    /// </summary>
    /// <param name="modules">The collection of modules.</param>
    public ModuleCollection(IReadOnlyDictionary<string, Module> modules)
    {
        Modules = modules;
        _firstLayer = GetInputModules("rx").First();
    }

    /// <summary>
    /// Returns the product of high and low pulses.
    /// </summary>
    /// <param name="buttonTimes">The number of times to run the button module.</param>
    /// <returns>The product of high and low pulses.</returns>
    public long GetPulseProducts(int buttonTimes)
    {
        long low = 0, high = 0;
        for (var i = 0; i < buttonTimes; i++)
        {
            var (l, h) = CountPulses();
            low += l;
            high += h;
        }
        return low * high;
    }

    /// <summary>
    /// Returns the count for button presses when RX module is handled.
    /// </summary>
    /// <returns>The count for button presses when RX module is handled.</returns>
    /// <remarks>This method assumes first level and second level input modules to RX are conjunctions.</remarks>
    public long GetRxHandledCount()
    {
        long pressCount = 1;
        var counter = GetPart2Counter(_firstLayer);
        var (success, result) = SolvePart2(pressCount, counter);
        while (!success)
        {
            pressCount++;
            (success, result) = SolvePart2(pressCount, counter);
        }
        return result;
    }

    /// <summary>
    /// Returns the count of low and high pulses.
    /// </summary>
    /// <returns>The count of low and high pulses.</returns>
    private (long Low, long High) CountPulses()
    {
        long high = 0, low = 1;     // low = 1 because start button will give a low pulse
        var queue = new Queue<(Module Source, Pulse Input)>();
        queue.Enqueue((Modules[Broadcaster], Pulse.Low));

        while (queue.Count > 0)
        {
            var (source, input) = queue.Dequeue();
            if (input == Pulse.Low)
                low += source.Destinations.Count;
            else
                high += source.Destinations.Count;

            foreach (var destination in source.Destinations)
            {
                if (!Modules.ContainsKey(destination) || !Modules[destination].CanSendPulse(input))
                    continue;

                var output = Modules[destination].SendPulse(source.Name, input);
                queue.Enqueue((Modules[destination], output));
            }
        }
        return (low, high);
    }

    /// <summary>
    /// Solver for part 2.
    /// </summary>
    private (bool Found, long Result) SolvePart2(long pressCount, Dictionary<string, (long PressCount, bool Visited)> counterMap)
    {
        var queue = new Queue<(Module Source, Pulse Input)>();
        queue.Enqueue((Modules[Broadcaster], Pulse.Low));

        while (queue.Count > 0)
        {
            var (source, input) = queue.Dequeue();
            foreach (var destination in source.Destinations)
            {
                if (!Modules.ContainsKey(destination) || !Modules[destination].CanSendPulse(input))
                    continue;

                if (destination == _firstLayer && input == Pulse.High)
                {
                    var (_, visited) = counterMap[source.Name];
                    if (!visited)
                        counterMap[source.Name] = (pressCount, true);

                    if (counterMap.All(x => x.Value.Visited))
                    {
                        long result = 1;
                        foreach (var counter in counterMap.Values)
                            result *= (counter.PressCount + 1000) / MathHelper.GreatestCommonFactor(result, counter.PressCount);
                        return (true, result);
                    }
                }

                var output = Modules[destination].SendPulse(source.Name, input);
                queue.Enqueue((Modules[destination], output));
            }
        }
        return (false, 0);
    }

    /// <summary>
    /// Returns the input module containing the specified destination module.
    /// </summary>
    /// <param name="destination">The destination module.</param>
    /// <returns>The input module for the destination module.</returns>
    private IEnumerable<string> GetInputModules(string destination)
    {
        return Modules
            .Where(x => x.Value.Destinations.Contains(destination))
            .Select(x => x.Key);
    }

    /// <summary>
    /// Gets the conjunction modules counter for part 2.
    /// </summary>
    private Dictionary<string, (long, bool)> GetPart2Counter(string firstLayer)
    {
        var result = new Dictionary<string, (long, bool)>();
        foreach (var name in GetInputModules(firstLayer))
        {
            var module = Modules[name];
            if (module.Type == ModuleType.Conjunction)
                result[module.Name] = (0, false);
        }
        return result;
    }
}
