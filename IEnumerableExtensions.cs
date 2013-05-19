using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public static class IEnumerableExtensions {

    public static T RandomElementByWeight<T>(this IEnumerable<T> sequence, Func<T, float> weightSelector)
    {
        float totalWeight = sequence.Sum(weightSelector);
        float itemWeightIndex =  UnityEngine.Random.Range (0.0f, 0.99f) * totalWeight;
        float currentWeightIndex = 0;

        foreach (var item in from weightedItem in sequence select new { Value = weightedItem, Weight = weightSelector(weightedItem) })
        {
            currentWeightIndex += item.Weight;

            if (currentWeightIndex > itemWeightIndex)
            {
                return item.Value;
            }

        }

        return default(T);
    }
}

/* ------
 * Usage (IEnumerableExtensions)
 * ------
    Dictionary<string, float> foo = new Dictionary<string, float>();
    foo.Add("Item 25% 1", 0.5f);
    foo.Add("Item 25% 2", 0.5f);
    foo.Add("Item 50%", 1f);

    var itemChosen foo.RandomElementByWeight(e => e.Value));
    //To get TKey or TValue, just append '.Key' or '.Value' to the above
 *
 */

