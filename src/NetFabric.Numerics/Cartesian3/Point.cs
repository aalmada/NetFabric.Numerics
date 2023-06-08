namespace NetFabric.Numerics.Cartesian3;

/// <summary>
/// Represents a point as an immutable struct.
/// </summary>
/// <typeparam name="T">The type of the point coordinates.</typeparam>
/// <param name="X">The X coordinate.</param>
/// <param name="Y">The X coordinate.</param>
/// <param name="Z">The X coordinate.</param>
[System.Diagnostics.DebuggerDisplay("X = {X}, Y = {Y}, Z = {Z}")]
public readonly record struct Point<T>(T X, T Y, T Z) 
    : IPoint<Point<T>>
    where T: struct, INumber<T>, IMinMaxValue<T>
{
    #region constants

    public static readonly Point<T> Zero = new(T.Zero, T.Zero, T.Zero);

    static Point<T> IPoint<Point<T>>.Zero
        => Zero;

    /// <summary>
    /// Represents the minimum value. This field is read-only.
    /// </summary>
    public static readonly Point<T> MinValue = new(T.MinValue, T.MinValue, T.MinValue);

    /// <summary>
    /// Represents the maximum value. This field is read-only.
    /// </summary>
    public static readonly Point<T> MaxValue = new(T.MaxValue, T.MaxValue, T.MaxValue);

    static Point<T> IMinMaxValue<Point<T>>.MinValue
        => MinValue;
    static Point<T> IMinMaxValue<Point<T>>.MaxValue
        => MaxValue;

    #endregion

    /// <summary>
    /// Gets the coordinate system.
    /// </summary>
    public CoordinateSystem<T> CoordinateSystem 
        => new();
    ICoordinateSystem IPoint<Point<T>>.CoordinateSystem 
        => CoordinateSystem;

    #region addition

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point<T> operator +(in Point<T> left, in Vector<T> right)
        => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    #endregion

    #region subtraction

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point<T> operator -(in Point<T> left, in Vector<T> right)
        => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> operator -(in Point<T> left, in Point<T> right)
        => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    #endregion

    object IPoint<Point<T>>.this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => Throw.ArgumentOutOfRangeException<object>(nameof(index), index, "index out of range")
        };
}

/// <summary>
/// Provides static methods for point operations.
/// </summary>
public static class Point
{

    /// <summary>
    /// Applies a quaternion to a 3-dimensional point.
    /// </summary>
    /// <typeparam name="T">The underlying numeric type of the quaternion and point coordinates.</typeparam>
    /// <param name="quaternion">The quaternion to apply.</param>
    /// <param name="point">The 3-dimensional point to transform.</param>
    /// <returns>The transformed 3-dimensional point.</returns>
    /// <remarks>
    /// <para>
    /// The <paramref name="quaternion"/> is not required to be a unit quaternion.
    /// </para>
    /// <para>
    /// The point coordinates type must be a floating point.
    /// </para>
    /// <para>
    /// The transformation is applied by multiplying the point with the quaternion,
    /// resulting in a new 3-dimensional point that represents the original point
    /// after being transformed by the quaternion.
    /// </para>
    /// </remarks>
    public static Point<T> Apply<T>(Quaternion<T> quaternion, Point<T> point)
        where T : struct, IFloatingPoint<T>, IMinMaxValue<T>
        => new (
            (quaternion.W * point.X) + (quaternion.Y * point.Z) - (quaternion.Z * point.Y),
            (quaternion.W * point.Y) + (quaternion.Z * point.X) - (quaternion.X * point.Z),
            (quaternion.W * point.Z) + (quaternion.X * point.Y) - (quaternion.Y * point.X)
        );

    /// <summary>
    /// Converts a <see cref="Point{TFrom}"/> to a <see cref="Point{TTo}"/>.
    /// </summary>
    /// <typeparam name="TFrom">The type of the components of the source point.</typeparam>
    /// <typeparam name="TTo">The type of the components of the target point.</typeparam>
    /// <param name="point">The source point to convert.</param>
    /// <exception cref="NotSupportedException"><typeparamref name="TTo" /> is not supported.</exception>
    /// <exception cref="OverflowException"><paramref name="point" /> is not representable by <typeparamref name="TFrom" />.</exception>
    /// <returns>The converted <see cref="Point{TTo}"/>.</returns>
    /// <remarks>
    /// This method performs a conversion from a <see cref="Point{TFrom}"/> to a <see cref="Point{TTo}"/>.
    /// It converts each component of the source point to the target type and constructs a new point with
    /// the converted components in the order x, y, z.
    /// </remarks>
    public static Point<TTo> Convert<TFrom, TTo>(in Point<TFrom> point)
        where TFrom : struct, IFloatingPoint<TFrom>, IMinMaxValue<TFrom>
        where TTo : struct, IFloatingPoint<TTo>, IMinMaxValue<TTo>
        => new(
            TTo.CreateChecked(point.X),
            TTo.CreateChecked(point.Y),
            TTo.CreateChecked(point.Z)
        );

    /// <summary>
    /// Calculates the distance between two points.
    /// </summary>
    /// <param name="from">The starting point.</param>
    /// <param name="to">The target point.</param>
    /// <returns>The distance between the two points.</returns>
    /// <remarks>
    /// <para>
    /// The <see cref="Distance"/> method calculates the distance between two points specified by the <paramref name="from"/> and <paramref name="to"/> parameters.
    /// </para>
    /// <para>
    /// The distance is calculated as the Euclidean distance in the 3D Cartesian coordinate system.
    /// </para>
    /// </remarks>
    public static double Distance<T>(in Point<T> from, in Point<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Math.Sqrt(double.CreateChecked(DistanceSquared(from, to)));

    /// <summary>
    /// Calculates the square of the distance between two points.
    /// </summary>
    /// <param name="from">The starting point.</param>
    /// <param name="to">The target point.</param>
    /// <returns>The square of the distance between the two points.</returns>
    /// <remarks>
    /// <para>
    /// The <see cref="DistanceSquared"/> method calculates the square of the distance between two points
    /// specified by the <paramref name="from"/> and <paramref name="to"/> parameters.
    /// </para>
    /// <para>
    /// The distance is calculated as the Euclidean distance in the 3D Cartesian coordinate system.
    /// </para>
    /// <para>
    /// Note that the square of the distance is returned instead of the actual distance to avoid the need for
    /// taking the square root, which can be a computationally expensive operation.
    /// </para>
    /// </remarks>
    public static T DistanceSquared<T>(in Point<T> from, in Point<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => Utils.Pow2(to.X - from.X) + Utils.Pow2(to.Y - from.Y) + Utils.Pow2(to.Z - from.Z);

    /// <summary>
    /// Gets the Manhattan distance between two points.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <remarks>
    /// <para>
    /// The term "Manhattan Distance" comes from the idea of measuring the distance a taxi 
    /// would have to travel along a grid of city blocks (which are typically arranged in 
    /// a rectangular or square grid pattern) to reach the destination point from the 
    /// starting point. 
    /// </para>
    /// <para>
    /// The Manhattan distance between two points, (x1, y1) and (x2, y2), is defined as the 
    /// sum of the absolute differences of their coordinates along each dimension.
    /// </para>
    /// </remarks>
    /// <returns>The Manhattan distance between two points.</returns>
    public static T ManhattanDistance<T>(in Point<T> from, in Point<T> to)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => T.Abs(to.X - from.X) + T.Abs(to.Y - from.Y) + T.Abs(to.Z - from.Z);

}
