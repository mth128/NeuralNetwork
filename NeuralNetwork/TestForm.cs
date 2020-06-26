using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NeuralNetwork
{
  public partial class TestForm : Form
  {

    private Bitmap bitmap; 
    private Graphics graphics;
    private Brush brush = new SolidBrush(Color.White);
    private Brush smoothBrush = new SolidBrush(Color.FromArgb(10, 255,255,255)); 
    private int radius = 4;
    private int radius2 = 10; 
    private int prevX = -1;
    private int prevY = -1;
    private Random random = new Random();
    Bitmap small;

    //NeuralNetwork neuralNetwork;
    Democracy democracy;
    INetwork network; 

    byte[] trainingImages;
    byte[] trainingValues;
    byte[] testImages;
    byte[] testValues;

    string trainImageFile = "train-images.idx3-ubyte";
    string trainValuesFile = "train-labels.idx1-ubyte"; 
    string testImageFile = "t10k-images.idx3-ubyte";
    string testValuesFile = "t10k-labels.idx1-ubyte";
    string webSource = "http://yann.lecun.com/exdb/mnist/";

    public int TrainImagesCount => trainingValues == null ? 0:trainingValues.Length - 8;
    public int TestImagesCount => testValues == null ? 0:testValues.Length - 8;

    public Sample[] trainSamples;
    public Sample[] testSamples;

    public bool training = false;  
    public BackgroundWorker Trainer = new BackgroundWorker();

    public Exception backgroundException; 

    public TestForm()
    {
      InitializeComponent();
      Clear();
      InitializeNeuralNetwork();
      InitializeTrainingSet();
      Trainer.DoWork += Training;
    }

    private void InitializeTrainingSet()
    {
      TryLoad(TestDataFolderBox.Text = Properties.Settings.Default.DataFolder);
    }

    private void InitializeNeuralNetwork()
    {
      NeuralNetwork[] networks = new NeuralNetwork[4]; 
      int[] layers = new int[] { 784, 16,16, 10 };
      NeuralNetwork.LayerActivation[] layerType = new NeuralNetwork.LayerActivation[]
      {  NeuralNetwork.LayerActivation.Relu, NeuralNetwork.LayerActivation.LeakyRelu,NeuralNetwork.LayerActivation.Sigmoid};

      //neuralNetwork = new NeuralNetwork(layers, layerType);

      networks[0] = new NeuralNetwork(layers, layerType);

      layers = new int[] { 784, 16, 16, 10 };
      layerType = new NeuralNetwork.LayerActivation[]
      {  NeuralNetwork.LayerActivation.Sigmoid, NeuralNetwork.LayerActivation.Sigmoid,NeuralNetwork.LayerActivation.Sigmoid};
      networks[1] = new NeuralNetwork(layers, layerType);

      layers = new int[] { 784, 20, 10 };
      layerType = new NeuralNetwork.LayerActivation[]
      {  NeuralNetwork.LayerActivation.Sigmoid,NeuralNetwork.LayerActivation.Sigmoid,};
      networks[2] = new NeuralNetwork(layers, layerType);

      layers = new int[] { 784, 16, 16, 16, 10 };
      layerType = new NeuralNetwork.LayerActivation[]
      {  NeuralNetwork.LayerActivation.LeakyRelu, NeuralNetwork.LayerActivation.LeakyRelu,NeuralNetwork.LayerActivation.LeakyRelu,NeuralNetwork.LayerActivation.Sigmoid,};
      networks[3] = new NeuralNetwork(layers, layerType);

      network = democracy = new Democracy(networks); 

    }

    public bool DrawOn { get; private set; }

    private void Clear()
    {
      if (bitmap != null)
        bitmap.Dispose();
      if (graphics != null)
        graphics.Dispose(); 
     
      bitmap = new Bitmap(256, 256);
      DrawPictureBox.Image = bitmap;
      graphics = Graphics.FromImage(bitmap);
      graphics.Clear(Color.Black);
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
    }

    private void ClearButton_Click(object sender, EventArgs e)
    {
      Clear();
    }

    private void DrawPictureBox_MouseDown(object sender, MouseEventArgs e)
    {
      DrawOn = true;
      DrawSpot(e.X, e.Y); 
    }

    private void DrawPictureBox_MouseUp(object sender, MouseEventArgs e)
    {
      DrawOn = false;
      prevX = -1;
      prevY = -1;
      if (small == null)
        small = new Bitmap(30, 30); 
      using (Graphics smallGraphics = Graphics.FromImage(small))
      {
        smallGraphics.SmoothingMode = SmoothingMode.HighQuality;

        smallGraphics.DrawImage(bitmap, new Rectangle(0,0,30,30));
        
      }
      SmallBox.Image = small;
      Guess(); 
    }

    private void PaintTo(int x, int y)
    {
      if (prevX != -1)
      {
        float dist = (float) Math.Sqrt(x * x + y * y);
        float xStep = (x-prevX) / dist;
        float yStep = (y-prevY) / dist;

        for (float i = 0; i < dist; i+=radius)
        {
          DrawSpot(x + xStep * i, y + yStep * y);   
        }
      }
      DrawSpot(x, y);
      prevX = x;
      prevY = y;
    }

    private void DrawSpot(float x, float y)
    {
      graphics.FillEllipse(brush, x - radius, y - radius, 2*radius, 2*radius);
      graphics.FillEllipse(smoothBrush, x - radius2, y - radius2, 2 * radius2, 2 * radius2);
      DrawPictureBox.Image = bitmap;
    }

    private void DrawPictureBox_MouseMove(object sender, MouseEventArgs e)
    {
      if (DrawOn)
      {
        PaintTo(e.X, e.Y);
      } 
    }

    private float[] GetImageValues(int r, byte[] imageArray)
    {
      float[] result = new float[784];
      int n = r * 784+16;

      for (int i = 0; i < 784; i++, n++)
        result[i] = imageArray[n]/255f;
      return result; 
    }
    private Bitmap GetImage(float[] values)
    {
      Bitmap result = new Bitmap(28, 28);
      int n = 0;
      for (int y = 0; y < 28; y++)
        for (int x = 0; x < 28; x++, n++)
        {
          byte v = (byte)(values[n]*255); 
          result.SetPixel(x, y, Color.FromArgb(v,v,v));
        }
      return result; 
    }

    private void RandomDigit_Click(object sender, EventArgs e)
    {

      Sample test = testSamples[random.Next(TestImagesCount)]; 
      if (small != null)
        small.Dispose();
      small = GetImage(test.input);
      SmallBox.Image = small;
      DigitBox.Text = test.value.ToString();
      Guess(); 

    }

    private void Guess()
    {
      if (small == null)
        return;
      float[] data = ImageFloats(small);

      float[] answers = network.Feed(data);
      //float[] answers = neuralNetwork.Feed(data);
      string answer = "";
      int value = 0;
      float highest = -1;
      for (int i = 0; i < 10; i++)
      {
        answer += "[" + i.ToString() + "]: " + answers[i].ToString() + "\r\n";
        if (answers[i] > highest)
        {
          highest = answers[i];
          value = i;
        }
      }
      AnswerBox.Text = answer;
      ResultLabel.Text = "It's a " + value.ToString();
    }

    public Sample GetPortion(int r, byte[] images, byte[] values)
    {
      Sample result = new Sample(); 
      result.input = GetImageValues(r, images);
      result.value = values[r + 8];
      result.expectedOutput = new float[10]; 
      for (int i=0; i<10;i++)
      {
        if (i == result.value)
          result.expectedOutput[i] = 1;
        else
          result.expectedOutput[i] = 0; 
      }
      return result; 
    }

    public Sample GetTestPortion(int r)
    {
      return GetPortion(r, testImages, testValues); 
    }

    public Sample GetTrainPortion(int r)
    {
      return GetPortion(r, trainingImages, trainingValues); 
    }

    public static float[] ImageFloats(Bitmap bitmap)
    {
      BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);
      IntPtr ptr = data.Scan0;
      int size = data.Stride * bitmap.Height; 
      byte[] bytes = new byte[size];
      Marshal.Copy(ptr, bytes,0, size);
      bitmap.UnlockBits(data);
      size /= 4; 
      float[] floats = new float[size];
      for (int i = 0; i < size; i++)
        floats[i] =  bytes[i*4]/255.0f;
      return floats; 
    }

    private void TrainButton_Click(object sender, EventArgs e)
    {
      if (training)
      {
        network.StopTraining = true; 
        //neuralNetwork.stopTraining = true;
        return;
      }

      network.StopTraining = false; 
      //neuralNetwork.stopTraining = false;
      training = true;
      TrainButton.Text = "Stop";
      MainBox.Enabled = false;
      Trainer.RunWorkerAsync();
    }

    private void Training(object sender, DoWorkEventArgs e)
    {
      try
      {
        //while (!neuralNetwork.stopTraining)
        //  neuralNetwork.Train(trainSamples);
        
        while (!network.StopTraining)
          network.Train(trainSamples, 16);       
      }
      catch (Exception ex)
      {
        backgroundException = ex; 
      }
      training = false;
      
    }

    private Sample[] Shuffle(Sample[] portions)
    {
      List<KeyValuePair<double, Sample>> mix = new List<KeyValuePair<double, Sample>>();
      foreach (Sample portion in portions)
        mix.Add(new KeyValuePair<double, Sample>(random.NextDouble(), portion));

      mix = mix.OrderBy(t => t.Key).ToList();
      List<Sample> result = new List<Sample>();
      foreach (KeyValuePair<double, Sample> m in mix)
        result.Add(m.Value);
      return result.ToArray(); 
    }

    private void TestButton_Click(object sender, EventArgs e)
    {
      //TestResult result = neuralNetwork.Test(testSamples);
      TestResult result = network.Test(testSamples);

      CostLabel.Text = "Cost: " + result.cost.ToString() + "\nSuccess Rate: " + result.successRate * 100 + "%"; 
    }

    private void BrowseButton_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog ofd = new OpenFileDialog())
      {
        ofd.FileName = TestDataFolderBox.Text;
        if (ofd.ShowDialog() != DialogResult.OK)
          return;
        TestDataFolderBox.Text = Path.GetDirectoryName(ofd.FileName);
        if (TryLoad(TestDataFolderBox.Text))
        {
          Properties.Settings.Default.DataFolder = TestDataFolderBox.Text;
          Properties.Settings.Default.Save();
        }
        else
        {
          MessageBox.Show("Failed to load at least one of the files:\n"+

                trainImageFile+"\n"+ trainValuesFile
                + "\n" + testImageFile + "\n" + testValuesFile + 
                "\n\nPlease download these files from:\n" + webSource,"Error"); 
            
        }
      }
    }

    private bool TryLoad(string folder)
    {
      try
      {
        trainingImages = File.ReadAllBytes(folder + "\\" + trainImageFile);
        trainingValues = File.ReadAllBytes(folder + "\\" + trainValuesFile);
        testImages = File.ReadAllBytes(folder + "\\" + testImageFile);
        testValues = File.ReadAllBytes(folder + "\\" + testValuesFile);
        int trainCount = TrainImagesCount;
        int testCount = TestImagesCount; 

        trainSamples = new Sample[trainCount];
        testSamples = new Sample[testCount];
        for (int i = 0; i < trainCount; i++)
          trainSamples[i] = GetTrainPortion(i);
        for (int i = 0; i < testCount; i++)
          testSamples[i] = GetTestPortion(i); 

        return true; 
      }
      catch
      {
      }
      return false; 
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog sfd = new SaveFileDialog())
      {
        if (sfd.ShowDialog() != DialogResult.OK)
          return;
        using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
        //using (BinaryWriter writer = new BinaryWriter(stream))
        {
          var formatter = new BinaryFormatter();
          //formatter.Serialize(stream, neuralNetwork);
          formatter.Serialize(stream, network);

        }

      }
    }

    private void LoadButton_Click(object sender, EventArgs e)
    {
      try
      {

        using (OpenFileDialog ofd = new OpenFileDialog())
        {
          if (ofd.ShowDialog() != DialogResult.OK)
            return;
          using (FileStream stream = new FileStream(ofd.FileName, FileMode.Open))
          {
            var formatter = new BinaryFormatter();
            //neuralNetwork = (NeuralNetwork) formatter.Deserialize(stream);
            network = (Democracy)formatter.Deserialize(stream);
          }

        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error");
      }
    }

    private void StopFix_Tick(object sender, EventArgs e)
    {
      if (training)
        return;

      if (MainBox.Enabled)
        return;

      MainBox.Enabled = true;
      TrainButton.Text = "Train"; 
    }
    private void SetTrainMode()
    {
      if (TrainReviewerBox.Checked)
        if (TrainParticipantsBox.Checked)
          democracy.TrainMode = 0;
        else
          democracy.TrainMode = 2;
      else if (TrainParticipantsBox.Checked)
        democracy.TrainMode = 1;
      else
        democracy.TrainMode = 0;
    }
    private void TrainReviewerBox_CheckedChanged(object sender, EventArgs e)
    {
      SetTrainMode(); 
    }

    private void TrainParticipantsBox_CheckedChanged(object sender, EventArgs e)
    {
      SetTrainMode();
    }

    private void DemocracyButton_CheckedChanged(object sender, EventArgs e)
    {
      if (DemocracyButton.Checked)
        network = democracy; 
    }

    private void Network1Button_CheckedChanged(object sender, EventArgs e)
    {
      if (Network1Button.Checked)
        network = democracy.Participants[0];
    }

    private void Network2Button_CheckedChanged(object sender, EventArgs e)
    {
      if (Network2Button.Checked)
        network = democracy.Participants[1];
    }

    private void Network3Button_CheckedChanged(object sender, EventArgs e)
    {
      if (Network3Button.Checked)
        network = democracy.Participants[2];
    }

    private void Network4Button_CheckedChanged(object sender, EventArgs e)
    {
      if (Network4Button.Checked)
        network = democracy.Participants[3];
    }
  }
}
