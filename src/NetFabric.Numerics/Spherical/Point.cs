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
[System.Diagnostics.DebuggerDisplay("Radius = {Radius}, Azimuth = {Azimuth}, Zenith = {Zenith}")]
[SkipLocalsInit]
public readonly record struct Point<TAngleUnits, TAngle, TRadius>(TRadius Radius, Angle<TAngleUnits, TAngle> Azimuth, Angle<TAngleUnits, TAngle> Zenith)
    : IPoint<Point<TAngleUnits, TAngle, TRadius>>
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
    where TRadius : struct, IFloatingPoint<TRadius>, IMinMaxValue<TRadius>
{
    #region constants

    public static readonly PointReduced<TAngleUnits, TAngle, TRadius> Zero = new(TRadius.Zero, Angle<TAngleUnits, TAngle>.Zero, Angle<TAngleUnits, TAngle>.Zero);

    static Point<TAngleUnits, TAngle, TRadius> INumericBase<Point<TAngleUnits, TAngle, TRadius>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<TAngleUnits, TAngle, TRadius> MinValue = new(TRadius.MinValue, Angle<TAngleUnits, TAngle>.MinValue, Angle<TAngleUnits, TAngle>.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<TAngleUnits, TAngle, TRadius> MaxValue = new(TRadius.MaxValue, Angle<TAngleUnits, TAngle>.MaxValue, Angle<TAngleUnits, TAngle>.MaxValue);

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
            TRadius.CreateChecked(point.Radius)
,
            Angle<TAngleUnits, TAngle>.CreateChecked(point.Azimuth),
            Angle<TAngleUnits, TAngle>.CreateChecked(point.Zenith));

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
            TRadius.CreateSaturating(point.Radius)
,
            Angle<TAngleUnits, TAngle>.CreateSaturating(point.Azimuth),
            Angle<TAngleUnits, TAngle>.CreateSaturating(point.Zenith));

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
            TRadius.CreateTruncating(point.Radius)
,
            Angle<TAngleUnits, TAngle>.CreateTruncating(point.Azimuth),
            Angle<TAngleUnits, TAngle>.CreateTruncating(point.Zenith));

    object IPoint<Point<TAngleUnits, TAngle, TRadius>>.this[int index] 
        => index switch
        {
            0 => Radius,
            1 => Azimuth,
            2 => Zenith,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };
}

/// <summary>
/// Provides static methods for point operations.
/// </summary>
public static partial class Point
{
    public static Point<Degrees, T, T> ToDegrees<T>(Point<Degrees, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => point;

    public static Point<Radians, T, T> ToRadians<T>(Point<Degrees, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Zenith));

    public static Point<Gradians, T, T> ToGradians<T>(Point<Degrees, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Zenith));

    public static Point<Revolutions, T, T> ToRevolutions<T>(Point<Degrees, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Zenith));

    public static Point<Degrees, T, T> ToDegrees<T>(Point<Radians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Zenith));

    public static Point<Radians, T, T> ToRadians<T>(Point<Radians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => point;

    public static Point<Gradians, T, T> ToGradians<T>(Point<Radians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Zenith));

    public static Point<Revolutions, T, T> ToRevolutions<T>(Point<Radians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Zenith));

    public static Point<Degrees, T, T> ToDegrees<T>(Point<Gradians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Zenith));

    public static Point<Radians, T, T> ToRadians<T>(Point<Gradians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Zenith));

    public static Point<Gradians, T, T> ToGradians<T>(Point<Gradians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => point;

    public static Point<Revolutions, T, T> ToRevolutions<T>(Point<Gradians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Zenith));

    public static Point<Degrees, T, T> ToDegrees<T>(Point<Revolutions, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Zenith));

    public static Point<Radians, T, T> ToRadians<T>(Point<Revolutions, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Zenith));

    public static Point<Gradians, T, T> ToGradians<T>(Point<Revolutions, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Zenith));

    public static Point<Revolutions, T, T> ToRevolutions<T>(Point<Revolutions, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => point;

    public static PointReduced<TAngleUnits, TAngle, TRadius> Reduce<TAngleUnits, TAngle, TRadius>(Point<TAngleUnits, TAngle, TRadius> point)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where TAngle : struct, IFloatingPoint<TAngle>, IMinMaxValue<TAngle>
        where TRadius : struct, IFloatingPoint<TRadius>, IMinMaxValue<TRadius>
        => new(point.Radius, Angle.Reduce(point.Azimuth), Angle.Reduce(point.Zenith));

    /// <summary>
    /// Converts a point in spherical coordinates to cartesian 3D coordinates.
    /// </summary>
    /// <typeparam name="T">The type of the coordinates of the point.</typeparam>
    /// <param name="point">The point in spherical coordinates to convert.</param>
    /// <returns>The cartesian 3D coordinates representing the point.</returns>
    public static Cartesian3.Point<T> ToCartesian<T>(Point<Radians, T, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var sinAzimuth = Angle.Sin(point.Azimuth);
        var cosAzimuth = Angle.Cos(point.Azimuth);
        var sinZenith = Angle.Sin(point.Zenith);
        var cosZenith = Angle.Cos(point.Zenith);

        var x = T.CreateChecked(point.Radius * sinZenith * cosAzimuth);
        var y = T.CreateChecked(point.Radius * sinZenith * sinAzimuth);
        var z = T.CreateChecked(point.Radius * cosZenith);

        return new(x, y, z);
    }

    /// <summary>
    /// Converts a point in spherical coordinates to cartesian 3D coordinates.
    /// </summary>
    /// <typeparam name="TAngle">The type used by the angle of the azimuth and zenith coordinates of <paramref name="point"/>.</typeparam>
    /// <typeparam name="TRadius">The type of the radius coordinate of <paramref name="point"/>.</typeparam>
    /// <typeparam name="T">The type used by the resulting cartesian point coordinates.</typeparam>
    /// <param name="point">The point in spherical coordinates to convert.</param>
    /// <returns>The cartesian 3D coordinates representing the point.</returns>
    public static Cartesian3.Point<T> ToCartesian<TAngle, TRadius, T>(Point<Radians, TAngle, TRadius> point)
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