namespace NetFabric.Numerics.Cartesian2;

public readonly record struct CoordinateSystem<T>
    : ICoordinateSystem
    where T: struct, INumber<T>
{
    static readonly ReadOnlyMemory<Coordinate> coordinates 
        = new(new[] {
            new Coordinate("X", typeof(T)), 
            new Coordinate("Y", typeof(T)),
        });

    public ReadOnlyMemory<Coordinate> Coordinates
        => coordinates;
}