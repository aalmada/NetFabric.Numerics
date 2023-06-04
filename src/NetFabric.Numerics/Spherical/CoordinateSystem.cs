namespace NetFabric.Numerics.Spherical;

public readonly record struct CoordinateSystem<TAngleUnits, TAngle, TRadius>
    : ICoordinateSystem
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
    where TRadius : struct, IFloatingPoint<TRadius>
{
    static readonly ReadOnlyMemory<Coordinate> coordinates 
        = new(new[] {
            new Coordinate("Azimuth", typeof(Angle<TAngleUnits, TAngle>)), 
            new Coordinate("Zenith", typeof(Angle<TAngleUnits, TAngle>)),
            new Coordinate("Radius", typeof(TRadius)),
        });

    public ReadOnlyMemory<Coordinate> Coordinates 
        => coordinates;
}