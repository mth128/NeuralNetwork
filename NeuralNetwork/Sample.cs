using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
  public class Sample
  {
    public float[] input;
    public float[] expectedOutput;
    public int value; 

    public float[] GetInvertedExpectedOutput()
    {
      float[] output = new float[expectedOutput.Length];
      for (int i = 0; i < expectedOutput.Length; i++)
        output[i] = 1 - expectedOutput[i];
      return output; 
    }

    /*public static Sample[] Shuffle(Sample[] samples)
    {
      Random random = new Random(); 
      List<KeyValuePair<double, Sample>> mix = new List<KeyValuePair<double, Sample>>();
      foreach (Sample sample in samples)
        mix.Add(new KeyValuePair<double, Sample>(random.NextDouble(), sample));

      mix = mix.OrderBy(t => t.Key).ToList();
      List<Sample> result = new List<Sample>();
      foreach (KeyValuePair<double, Sample> m in mix)
        result.Add(m.Value);
      return result.ToArray();
    }*/

    public int Max(float[] compare)
    {
      int max = 0;
      float highest = compare[0];
      for (int i = 1; i < compare.Length; i++)
      {
        if (compare[i] > highest)
        {
          highest = compare[i];
          max = i;
        }
      }
      return max; 
    }

    public bool CheckSuccess(float[] compare)
    {
      return Max(compare) == value; 
    }

    public float Cost(float[] compare)
    {
      float cost = 0;
      for (int i =0; i<compare.Length;i++)
      {
        float error = compare[i] - expectedOutput[i];
        cost += error * error; 
      }
      return cost; 
    }
  }
}
