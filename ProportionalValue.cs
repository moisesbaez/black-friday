using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ProportionValue<T>
{
    public double Proportion { get; set; }
    public T Value { get; set; }
}

public static class ProportionValue
{
    public static ProportionValue<T> Create<T>(double proportion, T value)
    {
        return new ProportionValue<T> { Proportion = proportion, Value = value };
    }

    public static T ChooseByRandom<T>(this IEnumerable<ProportionValue<T>> collection)
    {
        double rnd = UnityEngine.Random.Range(0.0f, 0.99f);
        foreach (var item in collection)
        {
            if (rnd < item.Proportion)
                return item.Value;
            rnd -= item.Proportion;
        }
        throw new InvalidOperationException(
            "The proportions in the collection do not add up to 1.");
    }
}

/* -----
 * Usage
 * -----
	var list = new[] {
    ProportionValue.Create(0.7, "a"),
    ProportionValue.Create(0.2, "b"),
    ProportionValue.Create(0.1, "c")
	};

	// Outputs "a" with probability 0.7, etc.
	Console.WriteLine(list.ChooseByRandom());
 *
 */
