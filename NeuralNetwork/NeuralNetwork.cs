using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
  [Serializable]
  public class NeuralNetwork : INetwork
  {
    public enum LayerActivation
    {
      Sigmoid, Tanh, Relu, LeakyRelu,
    }

    private static Random random = new Random();

    public NeuralNetworkLayer[] Layers { get; }

    public int InputSize { get; }
    public int OutputSize { get; }

    public float LearningRate { get; set; } = 0.5f;

    public bool StopTraining { get; set; } = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sizes">First is the input size, others are the layers output sizes.</param>
    /// <param name="types">The type of each layer. Length of types must be one less then length of sizes.</param>
    public NeuralNetwork(int[] sizes, LayerActivation[] types = null)
    {
      if (types == null)
      {
        types = new LayerActivation[sizes.Length - 1];
        for (int i = 0; i < types.Length; i++)
          types[i] = LayerActivation.Relu;
      }
      else if (types.Length != sizes.Length - 1)
        throw new Exception("Length of types must be 1 less then length of sizes!");

      if (sizes.Length < 2)
        throw new Exception("There must be at least one layer");
      NeuralNetworkLayer previous = null;
      int inputSize = sizes[0];
      Layers = new NeuralNetworkLayer[sizes.Length - 1];
      for (int i = 1; i < sizes.Length; i++)
      {
        int outputSize = sizes[i];
        NeuralNetworkLayer layer = new NeuralNetworkLayer(inputSize, outputSize, types[i - 1], previous);
        Layers[i - 1] = layer;
        previous = layer;
        inputSize = outputSize;
      }
      
      InputSize = Layers[0].InputSize;
      OutputSize = Layers.Last().OutputSize;
    }

    public float[] Feed(float[] input)
    {
      return Layers[0].Feed(input);
    }

    public void Apply(IEnumerable<NeuralTrainSet> trainingSets, float learningRate = 0.05f)
    {
      NeuralTrainSet combination = null;
      foreach (NeuralTrainSet set in trainingSets)
      {
        if (combination == null)
          combination = set;
        else
          combination = new NeuralTrainSet(combination, set);
      }
      Apply(combination, learningRate);
    }

    public void Apply(NeuralTrainSet set, float learningRate = 0.05f)
    {
      for (int i = 0; i < Layers.Length; i++)
        Layers[i].Apply(set.Layers[i], learningRate);
    }

    public NeuralTrainSet GetNeuralTrainSet(float[] input, float[] target)
    {
      return new NeuralTrainSet(this, input, target); 
    }

    public NeuralTrainSet GetNeuralTrainSet(Sample portion)
    {
      return GetNeuralTrainSet(portion.input, portion.expectedOutput); 
    }

    public TestResult Test(Sample[] samples)
    {
      TestResult result = new TestResult(); 
      float cost = 0;
      int length = samples.Length;
      float successCount = 0; 
      bool parallel = true;
      if (parallel)
      {
        float[] costs = new float[length];
        bool[] successes = new bool[length];
        Parallel.For(0, length, i =>
         {
           Sample sample = samples[i];
           float[] output = Feed(sample.input);
           costs[i] = sample.Cost(output);
           successes[i] = sample.CheckSuccess(output); 
         }
        );
        for (int i = 0; i < length; i++)
        {
          cost += costs[i];
          if (successes[i])
            successCount++; 
        }
      }
      else 
      {
        float[] costs = new float[length];
        bool[] successes = new bool[length];
        for( int i = 0; i< length; i++)
        {
          Sample sample = samples[i];
          float[] output = Feed(sample.input);
          costs[i] = sample.Cost(output);
          successes[i] = sample.CheckSuccess(output);
        }
        for (int i = 0; i < length; i++)
        {
          cost += costs[i];
          if (successes[i])
            successCount++;
        }
      }
      result.cost = cost / length;
      result.successRate = successCount / length;
      return result; 
    }

    public void Train(Sample[] samples, int miniBatchCount = 16)
    {
      Train(samples, miniBatchCount, true, true); 
    }

    public void Train(Sample[] samples, int miniBatchCount, bool parallel, bool shuffle = true)
    {
      if (shuffle)
        samples = General.Shuffle(samples);

      int end = samples.Length;
      NeuralTrainSet[] miniBatch;

      for (int i = 0; i < end;)
      {
        if (StopTraining)
          return; 
        int localEnd = i + miniBatchCount;
        if (localEnd > end)
          localEnd = end;
        int size = localEnd - i;

        miniBatch = new NeuralTrainSet[size];
        if (parallel)
          Parallel.For(0, size, j => {
            miniBatch[j] = GetNeuralTrainSet(samples[j + i]);
          });
        else
          for (int j = 0; j < size; j++)
            miniBatch[j] = GetNeuralTrainSet(samples[j + i]);

        Apply(miniBatch, LearningRate);
        i = localEnd;
      }
    }

    public static float RandomRange(float min, float max)
    {
      if (min > max)
      {
        float swap = min;
        min = max;
        max = swap;
      }
      float width = max - min;
      float rand = (float)random.NextDouble();
      float result = rand * width + min;
      return result;
    }
  }

  public class TestResult
  {
    public float successRate;
    public float cost; 
  }
}
