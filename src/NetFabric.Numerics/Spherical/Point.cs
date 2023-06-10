namespace NetFabric.Numerics.Spherical;

/// <summary>
/// Represents a point as an immutable struct.
/// </summary>
/// <typeparam name="TAngleUnits">The angle units used for the azimuth and zenith coordinates.</typeparam>
/// <typeparam name="TAngle">The type used by the angle of the azimuth and zenith coordinates.</typeparam>
/// <typeparam name="TRadius">The type of the radius coordinate.</typeparam>
/// <param name="Azimuth">The azimuth coordinate.</param>
/// <param name="Zenith">The zenith coordinate.</param>
/// <param name="Radius">The radius coordinate.</param>
[System.Diagnostics.DebuggerDisplay("Azimuth = {Azimuth}, Zenith = {Zenith}, Radius = {Radius}")]
public readonly record struct Point<TAngleUnits, TAngle, TRadius>(Angle<TAngleUnits, TAngle> Azimuth, Angle<TAngleUnits, TAngle> Zenith, TRadius Radius) 
    : IPoint<Point<TAngleUnits, TAngle, TRadius>>
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
    where TRadius : struct, IFloatingPoint<TRadius>, IMinMaxValue<TRadius>
{
    #region constants

    public static readonly Point<TAngleUnits, TAngle, TRadius> Zero = new(Angle<TAngleUnits, TAngle>.Zero, Angle<TAngleUnits, TAngle>.Zero, TRadius.Zero);

    static Point<TAngleUnits, TAngle, TRadius> IPoint<Point<TAngleUnits, TAngle, TRadius>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<TAngleUnits, TAngle, TRadius> MinValue = new(Angle<TAngleUnits, TAngle>.MinValue, Angle<TAngleUnits, TAngle>.MinValue, TRadius.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<TAngleUnits, TAngle, TRadius> MaxValue = new(Angle<TAngleUnits, TAngle>.MaxValue, Angle<TAngleUnits, TAngle>.MaxValue, TRadius.MaxValue);

    static Point<TAngleUnits, TAngle, TRadius> IMinMaxValue<Point<TAngleUnits, TAngle, TRadius>>.MinValue
        => MinValue;
    static Point<TAngleUnits, TAngle, TRadius> IMinMaxValue<Point<TAngleUnits, TAngle, TRadius>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<TAngleUnits, TAngle, TRadius> CoordinateSystem 
        => new();
    ICoordinateSystem IPoint<Point<TAngleUnits, TAngle, TRadius>>.CoordinateSystem 
        => CoordinateSystem;

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type used by the angle of the azimuth and zenith coordinates of <paramref name="point"/>.</typeparam>
    /// <typeparam name="TRadiusOther">The type of the radius coordinate of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TAngleUnits, TAngle, TRadius}"/></param>
    /// <returns>An instance of <see cref="Point{TAngleUnits, TAngle, TRadius}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> or <typeparamref name="TRadiusOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TAngleUnits, TAngle, TRadius}"/>.</exception>
    public static Point<TAngleUnits, TAngle, TRadius> CreateChecked<TAngleOther, TRadiusOther>(in Point<TAngleUnits, TAngleOther, TRadiusOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        where TRadiusOther : struct, IFloatingPoint<TRadiusOther>, IMinMaxValue<TRadiusOther>
        => new(
            Angle<TAngleUnits, TAngle>.CreateChecked(point.Azimuth),
            Angle<TAngleUnits, TAngle>.CreateChecked(point.Zenith),
            TRadius.CreateChecked(point.Radius)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type used by the angle of the azimuth and zenith coordinates of <paramref name="point"/>.</typeparam>
    /// <typeparam name="TRadiusOther">The type of the radius coordinate of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TAngleUnits, TAngle, TRadius}"/></param>
    /// <returns>An instance of <see cref="Point{TAngleUnits, TAngle, TRadius}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> or <typeparamref name="TRadiusOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TAngleUnits, TAngle, TRadius}"/>.</exception>
    public static Point<TAngleUnits, TAngle, TRadius> CreateSaturating<TAngleOther, TRadiusOther>(in Point<TAngleUnits, TAngleOther, TRadiusOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        where TRadiusOther : struct, IFloatingPoint<TRadiusOther>, IMinMaxValue<TRadiusOther>
        => new(
            Angle<TAngleUnits, TAngle>.CreateSaturating(point.Azimuth),
            Angle<TAngleUnits, TAngle>.CreateSaturating(point.Zenith),
            TRadius.CreateSaturating(point.Radius)
        );

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TAngleOther">The type used by the angle of the azimuth and zenith coordinates of <paramref name="point"/>.</typeparam>
    /// <typeparam name="TRadiusOther">The type of the radius coordinate of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TAngleUnits, TAngle, TRadius}"/></param>
    /// <returns>An instance of <see cref="Point{TAngleUnits, TAngle, TRadius}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TAngleOther" /> or <typeparamref name="TRadiusOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TAngleUnits, TAngle, TRadius}"/>.</exception>
    public static Point<TAngleUnits, TAngle, TRadius> CreateTruncating<TAngleOther, TRadiusOther>(in Point<TAngleUnits, TAngleOther, TRadiusOther> point)
        where TAngleOther : struct, IFloatingPoint<TAngleOther>, IMinMaxValue<TAngleOther>
        where TRadiusOther : struct, IFloatingPoint<TRadiusOther>, IMinMaxValue<TRadiusOther>
        => new(
            Angle<TAngleUnits, TAngle>.CreateTruncating(point.Azimuth),
            Angle<TAngleUnits, TAngle>.CreateTruncating(point.Zenith),
            TRadius.CreateTruncating(point.Radius)
        );

    object IPoint<Point<TAngleUnits, TAngle, TRadius>>.this[int index] 
        => index switch
        {
            0 => Azimuth,
            1 => Zenith,
            2 => Radius,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };
}

/// <summary>
/// Provides static methods for point operations.
/// </summary>
public static class Point
{
    /// <summary>
    /// Converts a point in spherical coordinates to cartesian 3D coordinates.
    /// </summary>
    /// <param name="point">The point in spherical coordinates to convert.</param>
    /// <returns>The cartesian 3D coordinates representing the point.</returns>
    public static Cartesian3.Point<T> ConvertToCartesian<TAngle, TRadius, T>(Point<Radians, TAngle, TRadius> point)
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>, ITrigonometricFunctions<TAngle>
        where TRadius : struct, IFloatingPoint<TRadius>, IMinMaxValue<TRadius>
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        var sinAzimuth = Angle.Sin(point.Azimuth);
        var cosAzimuth = Angle.Cos(point.Azimuth);
        var sinZenith = Angle.Sin(point.Zenith);
        var cosZenith = Angle.Cos(point.Zenith);

        var x = T.CreateChecked(point.Radius * TRadius.CreateChecked(sinZenith * cosAzimuth));
        var y = T.CreateChecked(point.Radius * TRadius.CreateChecked(sinZenith * sinAzimuth));
        var z = T.CreateChecked(point.Radius * TRadius.CreateChecked(cosZenith));

        return new(x, y, z);
    }

}