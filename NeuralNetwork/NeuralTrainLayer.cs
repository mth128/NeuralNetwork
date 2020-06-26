using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
  public class NeuralTrainLayer
  {
    internal float[] Output { get; }

    //a is output
    //p is input
    //w is weight
    //b is bias
    //z is intermediate value
    //dxdy means "derivative of x divided by the derivative of y" 

    public float[] DCDW { get; private set; }
    public float[] DCDB { get; private set; }
    public float[] Z { get; }
    public float[] Input { get; }
    public int TrainingCount { get; private set; }
    public NeuralNetworkLayer Layer {get;}
    public NeuralTrainLayer(NeuralNetworkLayer layer, float[] input)
    {
      float[] result = layer.Feed(input,false);
      Input = input; 
      Output = result;

      Layer = layer;
      DCDB = new float[Output.Length];
      DCDW = new float[input.Length * Output.Length];
    }

    public NeuralTrainLayer(NeuralTrainLayer a, NeuralTrainLayer b)
    {
      DCDB = a.DCDB.Plus(b.DCDB);
      DCDW = a.DCDW.Plus(b.DCDW);
      TrainingCount = a.TrainingCount + b.TrainingCount; 
    }

    internal float[] CalculateBackpropagateValues(float[] target, bool firstLayer)
    {
      //https://blog.demofox.org/2017/03/09/how-to-train-neural-networks-with-backpropagation/
      //3 blue 1 brown video neural network 4: backpropagation. 
      float[] dcda = firstLayer ? Output.Minus(target).Multiply(2): target;
      float[] dadz = Layer.FlatDerivArray(Output);
      float[] dcdb = dcda.Multiply(dadz);
      float[] dcdw = Layer.MultiplyIntoMatrix(Input, dcdb);

      DCDB = DCDB.Plus(dcdb);
      DCDW = DCDW.Plus(dcdw);
      TrainingCount++;

      float[] backProp = new float[Layer.InputSize];
      for (int x = 0; x < Layer.InputSize; x++)
        for (int y = 0; y < Layer.OutputSize; y++)
        {
          int w = x + y*Layer.InputSize; 
          backProp[x] += dcdb[y] * Layer.Weight[w];
        }
      return backProp; 
    }
  }
}
