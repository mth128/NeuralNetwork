using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
  public class NeuralTrainSet
  {
    private float cost = float.NaN;
    public NeuralNetwork NeuralNetwork { get; }
    public NeuralTrainLayer[] Layers { get;  }

    private float[] output;
    private float[] target; 
    public float Cost => float.IsNaN(cost) ? cost = CalculateCost() : cost; 

    public NeuralTrainSet(NeuralNetwork neuralNetwork, float[] input, float[] target)
    {
      NeuralNetwork = neuralNetwork;
      Layers = new NeuralTrainLayer[neuralNetwork.Layers.Length];
      for (int i = 0; i < Layers.Length; i++)
        if (i==0) 
          Layers[i] = new NeuralTrainLayer(neuralNetwork.Layers[i], input);
        else
          Layers[i] = new NeuralTrainLayer(neuralNetwork.Layers[i], Layers[i-1].Output);

      output = Layers.Last().Output;
      this.target = target;
      bool firstLayer = true; 
      for (int i = Layers.Length - 1; i >= 0; i--)
      {
        NeuralTrainLayer layer = Layers[i]; 
        target = layer.CalculateBackpropagateValues(target,firstLayer);
        firstLayer = false; 
      }
    }

    public NeuralTrainSet(NeuralTrainSet a, NeuralTrainSet b)
    {
      if (a.NeuralNetwork != b.NeuralNetwork)
        throw new Exception("Cannot blend sets that belong to a different network.");
      NeuralNetwork = a.NeuralNetwork; 
      int layerCount = a.Layers.Length; 
      Layers = new NeuralTrainLayer[layerCount];
      for (int i = 0; i<layerCount; i++)
        Layers[i] = new NeuralTrainLayer(a.Layers[i], b.Layers[i]);
    }

    public float CalculateCost()
    {
      float cost = 0;
      for (int i = 0; i < target.Length; i++)
      {
        float error = target[i] - output[i];
        cost += error * error;
      }

      if (float.IsNaN(cost))
        throw new Exception("Cost is NaN!");
      return cost; 
    }
  }
}
