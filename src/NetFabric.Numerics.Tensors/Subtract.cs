using System.Runtime.InteropServices;

namespace NetFabric.Numerics;

public static partial class Tensor
{
    /// <summary>
    /// Subtracts a right from each element in the left span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The left span.</param>
    /// <param name="right">The right to subtract from each element.</param>
    /// <param name="destination">The destination span to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the left and destination spans have different lengths.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the type <typeparamref name="T"/> does not implement the <see cref="ISubtractionOperators{T, T, T}"/> interface.</exception>
    public static void Subtract<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Apply<T, SubtractOperator<T>>(left, right, destination);

    public static void Subtract<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Apply<T, SubtractOperator<T>>(left, right, destination);

    /// <summary>
    /// Subtracts corresponding elements in the left and right spans and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The left span.</param>
    /// <param name="right">The right span.</param>
    /// <param name="destination">The destination span to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the left, right, and destination spans have different lengths.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the type <typeparamref name="T"/> does not implement the <see cref="ISubtractionOperators{T, T, T}"/> interface.</exception>
    public static void Subtract<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Apply<T, SubtractOperator<T>>(left, right, destination);
}
