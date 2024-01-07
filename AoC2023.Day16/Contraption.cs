using AoC.Common.Grid;

namespace AoC2023.Day16;

/// <summary>
/// Represents the mirror contraption.
/// </summary>
public class Contraption
{
    private const char VerticalSplit = '|';
    private const char HorizontalSplit = '-';
    private const char ForwardMirror = '/';
    private const char BackwardMirror = '\\';
    private readonly HashSet<Position> _visitedPositions = [];

    /// <summary>
    /// Gets the contraption layout.
    /// </summary>
    public string[] Layout { get; }

    /// <summary>
    /// Gets the layout boundary.
    /// </summary>
    public Boundary Bounds { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="Contraption"/>.
    /// </summary>
    /// <param name="layout">The layout of the contraption.</param>
    public Contraption(string[] layout)
    {
        Layout = layout;
        Bounds = new Boundary(layout.Length, layout[0].Length);
    }

    /// <summary>
    /// Counts the tiles that are energised.
    /// </summary>
    /// <param name="start">The start position.</param>
    /// <param name="direction">The direction of the starting beam.</param>
    /// <returns>The tiles that are energised.</returns>
    public int CountEnergisedTiles(Position start, Direction direction)
    {
        RunBeams(start, direction);
        return _visitedPositions.Count;
    }

    /// <summary>
    /// Runs the light beams.
    /// </summary>
    /// <param name="start">The start position.</param>
    /// <param name="direction">The direction of the starting beam.</param>
    private void RunBeams(Position start, Direction direction)
    {
        var beams = new List<LightBeam> { new(direction, start) };
        _visitedPositions.Clear();
        while (beams.Count != 0)
        {
            var beam = beams[^1];
            beams.RemoveAt(beams.Count - 1);
            beams.AddRange(RunBeam(beam));
        }
    }

    /// <summary>
    /// Runs a light beam.
    /// </summary>
    /// <param name="beam">The beam to run.</param>
    /// <returns>The new beams created when passing a splitter.</returns>
    private IEnumerable<LightBeam> RunBeam(LightBeam beam)
    {
        var result = new List<LightBeam>();
        while (Bounds.IsValid(beam.Position))
        {
            _visitedPositions.Add(beam.Position);
            var tile = Layout[beam.Position.Row32][beam.Position.Column32];
            if (tile == ForwardMirror)
                HandleForwardMirror(beam);
            else if (tile == BackwardMirror)
                HandleBackwardMirror(beam);
            else if (tile == VerticalSplit && HandleVerticalSplit(beam, result))
                return result;
            else if (tile == HorizontalSplit && HandleHorizontalSplit(beam, result))
                return result;

            beam.MoveToNextPosition();
        }
        return result;
    }

    /// <summary>
    /// Handles when a beam hits a horizontal splitter.
    /// </summary>
    /// <param name="beam">The beam that hit a horizontal splitter.</param>
    /// <param name="beams">The list of all light beams.</param>
    /// <returns>Whether the beam has ended.</returns>
    private static bool HandleHorizontalSplit(LightBeam beam, List<LightBeam> beams)
    {
        if (beam.Direction is Direction.Left or Direction.Right)
            return false;

        if (beam.AddSplitPoint(beam.Position))
        {
            beams.Add(beam.Clone(Direction.Left, beam.Position.Left));
            beams.Add(beam.Clone(Direction.Right, beam.Position.Right));
        }
        return true;
    }

    /// <summary>
    /// Handles when a beam hits a vertical splitter.
    /// </summary>
    /// <param name="beam">The beam that hit a vertical splitter.</param>
    /// <param name="beams">The list of all light beams.</param>
    /// <returns>Whether the beam has ended.</returns>
    private static bool HandleVerticalSplit(LightBeam beam, List<LightBeam> beams)
    {
        if (beam.Direction is Direction.Up or Direction.Down)
            return false;

        if (beam.AddSplitPoint(beam.Position))
        {
            beams.Add(beam.Clone(Direction.Up, beam.Position.Top));
            beams.Add(beam.Clone(Direction.Down, beam.Position.Bottom));
        }
        return true;
    }

    /// <summary>
    /// Handles when a beam hits a forward mirror.
    /// </summary>
    /// <param name="beam">The beam that hit a forward mirror.</param>
    private static void HandleForwardMirror(LightBeam beam)
    {
        beam.Direction = beam.Direction switch
        {
            Direction.Down => Direction.Left,
            Direction.Up => Direction.Right,
            Direction.Left => Direction.Down,
            _ => Direction.Up
        };
    }

    /// <summary>
    /// Handles when a beam hits a backward mirror.
    /// </summary>
    /// <param name="beam">The beam that hit a backward mirror.</param>
    private static void HandleBackwardMirror(LightBeam beam)
    {
        beam.Direction = beam.Direction switch
        {
            Direction.Down => Direction.Right,
            Direction.Up => Direction.Left,
            Direction.Left => Direction.Up,
            _ => Direction.Down
        };
    }
}
