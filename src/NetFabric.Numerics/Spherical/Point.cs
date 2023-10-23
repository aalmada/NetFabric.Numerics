namespace NetFabric.Numerics.Spherical;

/// <summary>
/// Represents a point as an immutable struct.
/// </summary>
/// <typeparam name="TAngleUnits">The units used for the angles.</typeparam>
/// <typeparam name="T">The type used for the coordinates.</typeparam>
[System.Diagnostics.DebuggerDisplay("Radius = {Radius}, Azimuth = {Azimuth}, Polar = {Polar}")]
[SkipLocalsInit]
public readonly struct Point<TAngleUnits, T>(T radius, Angle<TAngleUnits, T> azimuth, Angle<TAngleUnits, T> polar)
    : IPoint<Point<TAngleUnits, T>>
    where TAngleUnits : struct, IAngleUnits<TAngleUnits>
    where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Gets the radius component of the spherical coordinate point.
    /// </summary>
    /// <remarks>
    /// This property is read-only.
    /// </remarks>
    public T Radius
        => radius;

    /// <summary>
    /// Gets the azimuth angle component of the spherical coordinate point.
    /// </summary>
    /// <remarks>
    /// This property is read-only.
    /// </remarks>
    public Angle<TAngleUnits, T> Azimuth
        => azimuth;

    /// <summary>
    /// Gets the polar angle component of the spherical coordinate point.
    /// </summary>
    /// <remarks>
    /// This property is read-only.
    /// </remarks>
    public Angle<TAngleUnits, T> Polar
        => polar;

    #region constants

    public static readonly PointReduced<TAngleUnits, T> Zero = new(T.Zero, Angle<TAngleUnits, T>.Zero, Angle<TAngleUnits, T>.Zero);

    static Point<TAngleUnits, T> IGeometricBase<Point<TAngleUnits, T>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<TAngleUnits, T> MinValue = new(T.MinValue, Angle<TAngleUnits, T>.MinValue, Angle<TAngleUnits, T>.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<TAngleUnits, T> MaxValue = new(T.MaxValue, Angle<TAngleUnits, T>.MaxValue, Angle<TAngleUnits, T>.MaxValue);

    static Point<TAngleUnits, T> IMinMaxValue<Point<TAngleUnits, T>>.MinValue
        => MinValue;
    static Point<TAngleUnits, T> IMinMaxValue<Point<TAngleUnits, T>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<TAngleUnits, T> CoordinateSystem 
        => new();
    ICoordinateSystem IGeometricBase<Point<TAngleUnits, T>>.CoordinateSystem 
        => CoordinateSystem;

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// throwing an overflow exception for any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TAngleUnits, T}"/></param>
    /// <returns>An instance of <see cref="Point{TAngleUnits, T}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> or <typeparamref name="TOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TAngleUnits, T}"/>.</exception>
    public static Point<TAngleUnits, T> CreateChecked<TOther>(ref readonly Point<TAngleUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateChecked(point.Radius),
            Angle<TAngleUnits, T>.CreateChecked(point.Azimuth),
            Angle<TAngleUnits, T>.CreateChecked(point.Polar));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// saturating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TAngleUnits, T}"/></param>
    /// <returns>An instance of <see cref="Point{TAngleUnits, T}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> or <typeparamref name="TOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TAngleUnits, T}"/>.</exception>
    public static Point<TAngleUnits, T> CreateSaturating<TOther>(ref readonly Point<TAngleUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateSaturating(point.Radius),
            Angle<TAngleUnits, T>.CreateSaturating(point.Azimuth),
            Angle<TAngleUnits, T>.CreateSaturating(point.Polar));

    /// <summary>
    /// Creates an instance of the current type from a value, 
    /// truncating any values that fall outside the representable range of the current type.
    /// </summary>
    /// <typeparam name="TOther">The type of the components of <paramref name="point"/>.</typeparam>
    /// <param name="point">The value which is used to create the instance of <see cref="Point{TAngleUnits, T}"/></param>
    /// <returns>An instance of <see cref="Point{TAngleUnits, T}"/> created from <paramref name="point" />.</returns>
    /// <exception cref="NotSupportedException"><typeparamref name="TOther" /> or <typeparamref name="TOther"/> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <see cref="Point{TAngleUnits, T}"/>.</exception>
    public static Point<TAngleUnits, T> CreateTruncating<TOther>(ref readonly Point<TAngleUnits, TOther> point)
        where TOther : struct, IFloatingPoint<TOther>, IMinMaxValue<TOther>
        => new(
            T.CreateTruncating(point.Radius),
            Angle<TAngleUnits, T>.CreateTruncating(point.Azimuth),
            Angle<TAngleUnits, T>.CreateTruncating(point.Polar));

    object IGeometricBase<Point<TAngleUnits, T>>.this[int index] 
        => index switch
        {
            0 => Radius,
            1 => Azimuth,
            2 => Polar,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };

    #region equality

    /// <summary>
    /// Indicates whether two <see cref="Point{TAngleUnits, T}"/> instances are equal.
    /// </summary>
    /// <param name="left">The first point to compare.</param>
    /// <param name="right">The second point to compare.</param>
    /// <returns>true if the two points are equal, false otherwise.</returns>
    /// <remarks>
    /// The method compares the numerical values of the <paramref name="left"/> and <paramref name="right"/> points to determine their equality.
    /// </remarks>
    public static bool operator ==(Point<TAngleUnits, T> left, Point<TAngleUnits, T> right)
        => left.Equals(right);

    /// <summary>
    /// Indicates whether two <see cref="Point{TAngleUnits, T}"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first point to compare.</param>
    /// <param name="right">The second point to compare.</param>
    /// <returns>true if the two points are not equal, false otherwise.</returns>
    /// <remarks>
    /// The method compares the numerical values of the <paramref name="left"/> and <paramref name="right"/> points to determine their equality.
    /// </remarks>
    public static bool operator !=(Point<TAngleUnits, T> left, Point<TAngleUnits, T> right)
        => !left.Equals(right);

    /// <summary>
    /// Returns the hash code for the current <see cref="Point{TAngleUnits, T}"/> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
        => HashCode.Combine(Radius, Azimuth, Polar);

    /// <summary>
    /// Indicates whether the current <see cref="Point{TAngleUnits, T}"/> instance is equal to another <see cref="Point{TAngleUnits, T}"/> instance.
    /// </summary>
    /// <param name="other">A <see cref="Point{TAngleUnits, T}"/> value to compare to this instance.</param>
    /// <returns>true if <paramref name="other"/> has the same value as this instance; otherwise, false.</returns>
    public bool Equals(Point<TAngleUnits, T> other)
        => EqualityComparer<T>.Default.Equals(Radius, other.Radius) &&
            Azimuth.Equals(other.Azimuth) &&
            Polar.Equals(other.Polar);

    /// <summary>
    /// Indicates whether the current <see cref="Point{TAngleUnits, T}"/> instance is equal to another <see cref="PointReduced{TAngleUnits, T}"/> instance.
    /// </summary>
    /// <param name="other">A <see cref="PointReduced{TAngleUnits, T}"/> value to compare to this instance.</param>
    /// <returns>true if <paramref name="other"/> has the same value as this instance; otherwise, false.</returns>
    public bool Equals(PointReduced<TAngleUnits, T> other)
        => EqualityComparer<T>.Default.Equals(Radius, other.Radius) &&
            Azimuth.Equals(other.Azimuth) &&
            Polar.Equals(other.Polar);

    /// <summary>
    /// Indicates whether the current <see cref="Point{TAngleUnits, T}"/> instance is equal to another object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>
    /// true if <paramref name="obj"/> is an instance of a <see cref="Point{TAngleUnits, T}"/> or 
    /// <see cref="PointReduced{TAngleUnits, T}"/> and equals the value of this instance; otherwise, false.
    /// </returns>
    public override bool Equals(object? obj)
        => obj switch
        {
            Point<TAngleUnits, T> point => Equals(point),
            PointReduced<TAngleUnits, T> point => Equals(point),
            _ => false
        };

    #endregion
}

/// <summary>
/// Provides static methods for point operations.
/// </summary>
public static partial class Point
{
    /// <summary>
    /// Converts a point from degrees to radians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point ref readonly degrees.</param>
    /// <returns>The converted point ref readonly radians.</returns>    
    public static Point<Radians, T> ToRadians<T>(Point<Degrees, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Polar));

    /// <summary>
    /// Convert a point from degrees to gradians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point ref readonly degrees.</param>
    /// <returns>The converted point ref readonly gradians.</returns>
    public static Point<Gradians, T> ToGradians<T>(Point<Degrees, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Polar));

    /// <summary>
    /// Convert a point from degrees to revolutions.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point ref readonly degrees.</param>
    /// <returns>The converted point ref readonly revolutions.</returns>
    public static Point<Revolutions, T> ToRevolutions<T>(Point<Degrees, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Polar));

    /// <summary>
    /// Convert a point from radians to degrees.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point ref readonly radians.</param>
    /// <returns>The converted point ref readonly degrees.</returns>
    public static Point<Degrees, T> ToDegrees<T>(Point<Radians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Polar));

    /// <summary>
    /// Convert a point from radians to gradians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point ref readonly radians.</param>
    /// <returns>The converted point ref readonly gradians.</returns>
    public static Point<Gradians, T> ToGradians<T>(Point<Radians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Polar));

    /// <summary>
    /// Convert a point from radians to revolutions.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point ref readonly radians.</param>
    /// <returns>The converted point ref readonly revolutions.</returns>
    public static Point<Revolutions, T> ToRevolutions<T>(Point<Radians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Polar));

    /// <summary>
    /// Convert a point from gradians to degrees.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point ref readonly gradians.</param>
    /// <returns>The converted point ref readonly degrees.</returns>
    public static Point<Degrees, T> ToDegrees<T>(Point<Gradians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Polar));

    /// <summary>
    /// Convert a point from gradians to radians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point ref readonly gradians.</param>
    /// <returns>The converted point ref readonly radians.</returns>
    public static Point<Radians, T> ToRadians<T>(Point<Gradians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Polar));

    /// <summary>
    /// Convert a point from gradians to revolutions.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point ref readonly gradians.</param>
    /// <returns>The converted point ref readonly revolutions.</returns>
    public static Point<Revolutions, T> ToRevolutions<T>(Point<Gradians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRevolutions(point.Azimuth), Angle.ToRevolutions(point.Polar));

    /// <summary>
    /// Convert a point from revolutions to degrees.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point ref readonly revolutions.</param>
    /// <returns>The converted point ref readonly degrees.</returns>
    public static Point<Degrees, T> ToDegrees<T>(Point<Revolutions, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToDegrees(point.Azimuth), Angle.ToDegrees(point.Polar));

    /// <summary>
    /// Convert a point from revolutions to radians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point ref readonly revolutions.</param>
    /// <returns>The converted point ref readonly radians.</returns>
    public static Point<Radians, T> ToRadians<T>(Point<Revolutions, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToRadians(point.Azimuth), Angle.ToRadians(point.Polar));

    /// <summary>
    /// Convert a point from revolutions to gradians.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point ref readonly revolutions.</param>
    /// <returns>The converted point ref readonly gradians.</returns>
    public static Point<Gradians, T> ToGradians<T>(Point<Revolutions, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new(point.Radius, Angle.ToGradians(point.Azimuth), Angle.ToGradians(point.Polar));

    /// <summary>
    /// Reduces a point ref readonly spherical coordinates by applying reduction functions to its components.
    /// </summary>
    /// <typeparam name="TAngleUnits">The type representing angle units.</typeparam>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The input point ref readonly spherical coordinates to be reduced.</param>
    /// <returns>
    /// A new <see cref="PointReduced{TAngleUnits, T}"/> object with a reduced
    /// azimuthal angle and polar angle, while keeping the original radius.
    /// </returns>
    /// <remarks>
    /// The reduction process involves applying reduction functions to the azimuthal and polar angles
    /// of the input spherical point. The radius component remains unchanged.
    /// </remarks>
    public static PointReduced<TAngleUnits, T> Reduce<TAngleUnits, T>(Point<TAngleUnits, T> point)
        where TAngleUnits : struct, IAngleUnits<TAngleUnits>
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
    {
        var azimuth = Angle.Reduce(point.Azimuth);
        var polar = Angle.Reduce(point.Polar);
        if (polar > Angle<TAngleUnits, T>.Straight)
        {
            polar = new(Angle<TAngleUnits, T>.Full.Value - polar.Value);
        }
        return new(point.Radius, azimuth, polar);
    }

    /// <summary>
    /// Converts a point ref readonly spherical coordinates to rectangular 3D coordinates.
    /// </summary>
    /// <typeparam name="T">The type used for the coordinates.</typeparam>
    /// <param name="point">The point ref readonly spherical coordinates to convert.</param>
    /// <returns>The rectangular 3D coordinates representing the point.</returns>
    /// <remarks>
    /// If the type of point to convert doesn't meet the constraints, please convert it first to a suitable type using one of the conversion methods: 
    /// - <see cref="Point{TAngleUnits, T}.CreateChecked{TOther}(ref readonly Point{TAngleUnits, TOther})"/>
    /// - <see cref="Point{TAngleUnits, T}.CreateSaturating{TOther}(ref readonly Point{TAngleUnits, TOther})"/>
    /// - <see cref="Point{TAngleUnits, T}.CreateTruncating{TOther}(ref readonly Point{TAngleUnits, TOther})"/>
    /// </remarks>
    public static Rectangular3D.Point<T> ToRectangular<T>(Point<Radians, T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>, ITrigonometricFunctions<T>
    {
        var (sinAzimuth, cosAzimuth) = Angle.SinCos(point.Azimuth);
        var (sinPolar, cosPolar) = Angle.SinCos(point.Polar);

        var x = point.Radius * sinPolar * cosAzimuth;
        var y = point.Radius * sinPolar * sinAzimuth;
        var z = point.Radius * cosPolar;

        return new(x, y, z);
    }
}