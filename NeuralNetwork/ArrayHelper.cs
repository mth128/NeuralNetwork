using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
  public static class General
  {    
    public static T[] Shuffle<T>(T[] array)
    {
      Random random = new Random();
      List<KeyValuePair<double, T>> mix = new List<KeyValuePair<double, T>>();
      foreach (T item in array)
        mix.Add(new KeyValuePair<double, T>(random.NextDouble(), item));

      mix = mix.OrderBy(t => t.Key).ToList();
      List<T> result = new List<T>();
      foreach (KeyValuePair<double, T> m in mix)
        result.Add(m.Value);
      return result.ToArray();
    }

    public static float[] Plus(this float[] a, float[] b)
    {
      int length = a.Length;
      if (length != b.Length)
        throw new Exception("Array size mismatch!");
      float[] c = new float[length];
      for (int i = 0; i < length; i++)
        c[i] = a[i] + b[i];
      return c;
    }

    public static float[] Minus(this float[] a, float[] b)
    {
      int length = a.Length;
      if (length != b.Length)
        throw new Exception("Array size mismatch!");
      float[] c = new float[length];
      for (int i = 0; i < length; i++)
        c[i] = a[i] - b[i];
      return c;
    }

    public static float[] Multiply(this float[] a, float[] b)
    {
      int length = a.Length;
      if (length != b.Length)
        throw new Exception("Array size mismatch!");
      float[] c = new float[length];
      for (int i = 0; i < length; i++)
        c[i] = a[i] * b[i];
      return c;
    }

    public static float[] Multiply(this float[] a, float b)
    {
      int length = a.Length;
      float[] c = new float[length];

      if (float.IsNaN(b) || float.IsInfinity(b))
        throw new Exception("Error in multiplyer (nan or infinity)");
      for (int i = 0; i < length; i++)
      {
        float v = a[i];
        if (float.IsNaN(v) || float.IsInfinity(v))
          throw new Exception("Error in multiplyer (nan or infinity)");

        c[i] = v * b;
      }
      return c;
    }

    public static float[] Divide(this float[] a, float[] b)
    {
      int length = a.Length;
      if (length != b.Length)
        throw new Exception("Array size mismatch!");
      float[] c = new float[length];
      for (int i = 0; i < length; i++)
        c[i] = a[i] / b[i];
      return c;
    }
  }
}
