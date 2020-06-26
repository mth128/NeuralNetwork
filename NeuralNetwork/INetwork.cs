using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
  public interface INetwork
  {
    bool StopTraining { get; set; }
    float[] Feed(float[] input);
    void Train(Sample[] samples, int miniBatchCount = 16);
    TestResult Test(Sample[] samples);
  }
}
