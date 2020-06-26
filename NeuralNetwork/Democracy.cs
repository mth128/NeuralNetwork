using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
  [Serializable]
  public class Democracy : INetwork
  {
    private bool stopTraining; 
    public bool StopTraining 
    { 
      set
      {
        if (value) 
          StopAll(); 
        else 
          StartAll(); 
      } 
      get
      {
        return stopTraining; 
      }
    }  
    public int InputSize { get; }
    public int OutputSize { get; }
    public NeuralNetwork[] Participants { get; }

    public NeuralNetwork Reviewer { get; }
    public int TrainMode { get; set; } = 0; 

    public Democracy(NeuralNetwork[] participants)
    {
      NeuralNetwork.LayerActivation type = participants[0].Layers.Last().LayerActivation;
      InputSize = participants[0].InputSize;
      OutputSize = participants[0].OutputSize;
      foreach (NeuralNetwork neuralNetwork in participants)
      {
        if (neuralNetwork.InputSize != InputSize || neuralNetwork.OutputSize != OutputSize)
          throw new Exception("Neural networks are not identical in input/output size!");
        if (neuralNetwork.Layers.Last().LayerActivation != type)
          throw new Exception("All participants must end with the same output activation!");
      }
      Participants = participants;

      Reviewer = new NeuralNetwork(new int[] { OutputSize * participants.Length, OutputSize * 2, OutputSize },
        new NeuralNetwork.LayerActivation[] { NeuralNetwork.LayerActivation.LeakyRelu, type });  ; 
    }

    public void StopAll()
    {
      foreach (NeuralNetwork neuralNetwork in Participants)
        neuralNetwork.StopTraining = true;
      Reviewer.StopTraining = true;
      stopTraining = true; 
    }

    public void StartAll()
    {
      foreach (NeuralNetwork neuralNetwork in Participants)
        neuralNetwork.StopTraining = false;
      Reviewer.StopTraining = false;
      stopTraining = false; 
    }

    public float[] Feed(float[] input)
    {
      float[] review = new float[OutputSize * Participants.Length];
      int i = 0; 
      foreach (NeuralNetwork participant in Participants)
      {
        float[] pOutput = participant.Feed(input);
        Array.Copy(pOutput, 0, review, i, OutputSize);
        i += OutputSize; 
      }
      return Reviewer.Feed(review); 
    }

    /// <summary>
    /// Training the participants and the reviewer. 
    /// </summary>
    /// <param name="samples">Samples to train with</param>
    /// <param name="miniBatchCount">Size of the minibatch</param>
    /// <param name="trainMode">0: train all, 1: only participants, 2: only reviewer.</param>
    public void Train(Sample[] samples, int miniBatchCount = 16)
    {
      samples = General.Shuffle(samples);

      Sample[] miniBatch; 
      int end = samples.Length;
      for (int i = 0; i < end;)
      {
        if (stopTraining)
          return;
        int localEnd = i + miniBatchCount;
        if (localEnd > end)
          localEnd = end;
        int size = localEnd - i;

        miniBatch = new Sample[size];
        Array.Copy(samples, i, miniBatch, 0, size);

        if (TrainMode == 0 || TrainMode == 1)
        Parallel.ForEach(Participants, participant =>
        {
          participant.Train(miniBatch, size, false); 
        });

        if (TrainMode == 0 || TrainMode == 2)
        {
          Sample[] reviewSamples = new Sample[size];

          int reviewInputSize = OutputSize * Participants.Length;
          for (int s = 0; s < size; s++)
          {
            Sample sample = new Sample();
            sample.value = samples[s].value;
            sample.expectedOutput = samples[s].expectedOutput;
            sample.input = new float[reviewInputSize];
            reviewSamples[s] = sample;
          }
          Parallel.For(0, Participants.Length, p =>
          {
            NeuralNetwork participant = Participants[p];
            int putPosition = p * OutputSize;
            for (int s = 0; s < size; s++)
            {
              float[] output = participant.Feed(samples[s].input);
              Array.Copy(output, 0, reviewSamples[s].input, putPosition, OutputSize);
            }
          });

          Reviewer.Train(reviewSamples, size, true, false);
        }
      }
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
        for (int i = 0; i < length; i++)
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

  }
}
