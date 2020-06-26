using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
  [Serializable]
  public class NeuralNetworkLayer
  {
    public int InputSize { get; }
    public int OutputSize { get; }
    public int Size { get; }
    public float[] Bias { get; private set; }
    public float[] Weight { get; private set; }

    private NeuralNetworkLayer nextLayer;
    public NeuralNetwork.LayerActivation LayerActivation { get; }
    public Func<float, float> FlatFunction { get; }
    public Func<float, float> FlatDeriv { get; }

    public NeuralNetworkLayer(int inputSize, int outputSize, NeuralNetwork.LayerActivation layerType = NeuralNetwork.LayerActivation.Tanh, NeuralNetworkLayer previousLayer = null)
    {
      InputSize = inputSize;
      OutputSize = outputSize;
      Size = inputSize * outputSize;

      Bias = new float[outputSize];
      Weight = new float[Size];

      if (previousLayer != null)
        previousLayer.nextLayer = this;

      switch (layerType)
      {
        case NeuralNetwork.LayerActivation.Tanh:
          FlatFunction = Tanh;
          FlatDeriv = TanhD;
          break;
        case NeuralNetwork.LayerActivation.Sigmoid:
          FlatFunction = Sigmoid;
          FlatDeriv = SigmoidD;
          break;
        case NeuralNetwork.LayerActivation.Relu:
          FlatFunction = Relu;
          FlatDeriv = ReluD;
          break;
        case NeuralNetwork.LayerActivation.LeakyRelu:
          FlatFunction = LeakyRelu;
          FlatDeriv = LeakyReluD;
          break;
        default:
          FlatFunction = Relu;
          FlatDeriv = ReluD;
          break;
      }
      LayerActivation = layerType; 
      InitializeWeights();
      InitializeBiases();
    }

    internal float[] MultiplyIntoMatrix(float[] inputSide, float[] outputSide)
    {
      float[] result = new float[InputSize * OutputSize];

      for (int y = 0; y < OutputSize; y++)
      {
        int w = y * InputSize;
        float v = outputSide[y];
        for (int x = 0; x < InputSize; x++, w++)
          result[w] = inputSide[x]*v;
      }
      return result; 
    }

    private void InitializeBiases()
    {
      for (int i = 0; i < OutputSize; i++)
        Bias[i] = NeuralNetwork.RandomRange(-0.5f, 0.5f);
    }

    private void InitializeWeights()
    {
      for (int i = 0; i < Size; i++)
        Weight[i] = NeuralNetwork.RandomRange(-0.5f, 0.5f);
    }

    public static float Sigmoid(float x)
    {
      float k = (float)Math.Exp(x);
      if (float.IsInfinity(k))
        return 1; 
      float result =  k / (1.0f + k);
      if (float.IsNaN(result))
        throw new Exception("Sigmoid resulted in NaN!");
      return result; 
    }

    public static float Tanh(float x) => (float)Math.Tanh(x);
    public static float Relu(float x) => (0 >= x) ? 0 : x;
    public static float LeakyRelu(float x) => (0 >= x) ? 0.01f * x : x;

    public static float SigmoidD(float x) => x * (1 - x);
    public static float TanhD(float x) => 1 - (x * x);
    public static float ReluD(float x) => (0 >= x) ? 0 : 1;
    public static float LeakyReluD(float x) => (0 >= x) ? 0.01f : 1;
    public int GetWeightIndex(int inputIndex, int outputIndex) => InputSize*outputIndex+inputIndex; 

    internal void Apply(NeuralTrainLayer neuralTrainLayer, float learningRate)
    {
      if (neuralTrainLayer.TrainingCount == 0)
        return; 
      float multiplyer = learningRate / neuralTrainLayer.TrainingCount;
      float[] bShift = neuralTrainLayer.DCDB.Multiply(multiplyer);
      float[] wShift = neuralTrainLayer.DCDW.Multiply(multiplyer);

      Bias = Bias.Minus(bShift);
      Weight = Weight.Minus(wShift); 
    }

    public float[] Feed(float[] input, bool pushForward = true)
    {
      float[] output = new float[OutputSize]; 

      for (int y = 0; y < OutputSize; y++)
      {
        int start = y * InputSize;
        float z = Bias[y];
        int w = start;
        for (int x = 0; x < InputSize; x++, w++)
          z += Weight[w] * input[x];
        output[y] = FlatFunction(z);
      }

      if (pushForward && nextLayer != null)
        return nextLayer.Feed(output);
      else
        return output;
    }

    public void Override(float[] weight, float[] bias)
    {
      if (Weight.Length != weight.Length || bias.Length != Bias.Length)
        throw new Exception("Size mismatch.");
      Weight = weight;
      Bias = bias; 
    }

    public float[] FlatDerivArray(float[] a)
    {
      int length = a.Length; 
      float[] result = new float[length];

      for (int i = 0; i < length; i++)
      {
        float v = a[i]; 
        float r = FlatDeriv(v);
        if (float.IsNaN(v) || float.IsNaN(r))
          throw new Exception("NaN found!");
        result[i] = r;       
      }

      return result; 
    }
  }
}
