using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace LLAb2
{
    public partial class MainWindow : Window
    {
        private List<int[]> _arrays = new List<int[]>();
        private DataTable _table = new DataTable();
        private DataTable _normalizedTable = new DataTable();
        private Window _processingMark;
        private int _cpu = 0;
        private int _memory = 0;
        private int _frames = 0;
        private string _name = "My";

        public MainWindow()
        {
            InitializeComponent();
            InitializeDataGrid();
            Hide();
            _processingMark = new ProcessingMarkWindow();
            _processingMark.Show();
            CPU();
            Memory();
            var gpu = new GPU();
            gpu.Ended += OnGpuTestEnded;
            gpu.ShowDialog();

            _processingMark.Close();
            

            _table.Rows.Add(_name, _cpu, _frames, _memory, (_cpu + _frames + _memory) / 3);

            int maxCPU = 0;
            int maxGPU = 0;
            int maxMemory = 0;
            int maxOverall = 0;

            for (int i = 0; i < _table.Rows.Count - 1; i++)
            {
                if (int.Parse(_table.Rows[i][1].ToString()) > maxCPU)
                    maxCPU = int.Parse(_table.Rows[i][1].ToString());

                if (int.Parse(_table.Rows[i][2].ToString()) > maxGPU)
                    maxGPU = int.Parse(_table.Rows[i][2].ToString());

                if (int.Parse(_table.Rows[i][3].ToString()) > maxMemory)
                    maxMemory = int.Parse(_table.Rows[i][3].ToString());

                if (int.Parse(_table.Rows[i][4].ToString()) > maxOverall)
                    maxOverall = int.Parse(_table.Rows[i][4].ToString());
            }

            for (int i = 0; i < _table.Rows.Count - 1; i++)
            {
                float normalizedCPU = (float)int.Parse(_table.Rows[i][1].ToString()) / (float)maxCPU;
                float normalizedGPU = (float)int.Parse(_table.Rows[i][2].ToString()) / (float)maxGPU;
                float normalizedMemory = (float)int.Parse(_table.Rows[i][3].ToString()) / (float)maxMemory;
                float normalizedOverall = (float)int.Parse(_table.Rows[i][4].ToString()) / (float)maxOverall;

                _normalizedTable.Rows.Add($"PC{i + 1}", normalizedCPU, normalizedGPU, normalizedMemory, normalizedOverall);
            }

            float normalizedCPUThis = (float)int.Parse(_table.Rows[_table.Rows.Count - 1][1].ToString()) / (float)maxCPU;
            float normalizedGPUThis = (float)int.Parse(_table.Rows[_table.Rows.Count - 1][2].ToString()) / (float)maxGPU;
            float normalizedMemoryThis = (float)int.Parse(_table.Rows[_table.Rows.Count - 1][3].ToString()) / (float)maxMemory;
            float normalizedOverallThis = (float)int.Parse(_table.Rows[_table.Rows.Count - 1][4].ToString()) / (float)maxOverall;

            _normalizedTable.Rows.Add(_name, normalizedCPUThis, normalizedGPUThis, normalizedMemoryThis, normalizedOverallThis);

            Show();
        }

        private void InitializeDataGrid()
        {

            _table.Columns.Add("Name");
            _table.Columns.Add("CPU Time");
            _table.Columns.Add("GPU Time");
            _table.Columns.Add("Memory Time");
            _table.Columns.Add("Overall");
            _resultData.ItemsSource = _table.DefaultView;

            _table.Rows.Add("PC1", 50583, 54341, 33096, 46006);
            _table.Rows.Add("PC2", 74339, 111495, 68412, 84748);
            _table.Rows.Add("PC3", 28855, 40142, 23295, 30764);

            _normalizedTable.Columns.Add("Name");
            _normalizedTable.Columns.Add("CPU Time");
            _normalizedTable.Columns.Add("GPU Time");
            _normalizedTable.Columns.Add("Memory Time");
            _normalizedTable.Columns.Add("Overall");

            _normalizedResultData.ItemsSource = _normalizedTable.DefaultView;
        }

        public void CPU()
        {
            Random random = new Random();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int n = 0; n < 10000; n++)
            {
                for (int i = 1; i < 300; i++)
                {
                    Factorial(i);
                }
            }

            for (int i = 0; i < 10000; i++)
            {
                int[] array = new int[16384];

                for (int j = 0; j < 16384; j++)
                {
                    array[j] = random.Next(-16384, 16384);
                }

                Brush(array);
                _arrays.Add(array);
            }

            stopwatch.Stop();
            _cpu = stopwatch.Elapsed.Seconds * 1000 + stopwatch.Elapsed.Milliseconds;
        }

        public void Memory()
        {
            string filePath = @"C:\BenchMark\Test.txt";
            string directory = @"C:\BenchMark";
            Directory.CreateDirectory(directory);

            TimeSpan testDuration = new TimeSpan(0, 0, 5);
            Stopwatch stopwatch = new Stopwatch();

            try
            {
                for (int i = 0; i < 10000; i++)
                {
                    using (StreamWriter streamWriter = new StreamWriter(filePath))
                    {
                        stopwatch.Start();

                        foreach (var array in _arrays)
                        {
                            for (int j = 0; j < array.Length; j++)
                            {
                                streamWriter.WriteLine(array[j].ToString());
                            }
                        }
                    }

                    using (StreamReader streamReader = new StreamReader(filePath))
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.Append(streamReader.ReadToEnd());
                    }
                }
            }
            catch { }

            stopwatch.Stop();
            _memory = stopwatch.Elapsed.Seconds * 1000 + stopwatch.Elapsed.Milliseconds;

            File.Delete(filePath);
            Directory.Delete(directory);
        }

        public void Brush(int[] array)
        {
            double factor = 1.247330950103979;
            int step = array.Length - 1;

            while (step >= 1)
            {
                for (int i = 0; i + step < array.Length; i++)
                {
                    if (array[i] > array[i + step])
                    {
                        int temp = array[i];
                        array[i] = array[i + step];
                        array[i + step] = temp;
                    }
                }

                step = (int)((double)step / factor);
            }
        }

        public int Factorial(int n)
        {
            if (n == 1)
                return 1;

            return n * Factorial(n - 1);
        }

        private void OnGpuTestEnded(int frames)
        {
            _frames = frames;
            _frames *= 300;
        }
    }
}
