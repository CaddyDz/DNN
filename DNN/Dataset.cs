﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNN
{
   class  Dataset
    {
        public List<double[]> LableDataset { get; }
        public List<double[]> InputDataset { get; }

        private int InputLength;
        private int LabletLength;

        public int Length {get { return LableDataset.Count; } }//input dataset have the same length as output dataset

        public Dataset(int input_length,int Lable_length)
        {
            InputLength = input_length;
            LabletLength = Lable_length;

            InputDataset = new List<double[]>();
            LableDataset = new List<double[]>();
           
        }
        public void LoadDataset(string train)
        {
            using (var reader = new System.IO.StreamReader(train))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();//read string line 
                    var values = line.Split(',').Select(x=> double.Parse(x));//store values and convert them to double 

                    LableDataset.Add(values.Take(LabletLength).ToArray());//store the input data array to the this list
                    InputDataset.Add(values.Skip(LabletLength).ToArray());//store the output data array to the this list
                }
            }
            for (int i = 0; i < InputDataset.Count; i++)
            {
                for (int j = 0; j < InputLength; j++)
                {
                    InputDataset[i][j] /= 255;
                }
            }
        }
        public void SaveDataset(string train)
        {
            using (var writer = new System.IO.StreamWriter(train))
            {
                for (int i = 0; i < LableDataset.Count; i++)
                {
                    for (int k = 0; k < 28*28; k++)
                    {
                        writer.Write(InputDataset[i][k]);

                        writer.Write(",");
                    }
                    for (int j = 0; j < 10; j++)
                    {
                        if (LableDataset[i][0] == j)
                            writer.Write("1");
                        else
                            writer.Write("0");

                        if (j == 10 - 1)
                            continue;
                    
                        writer.Write(",");

                    }
                    if (i == LableDataset.Count - 1)
                        continue;
                    writer.Write(Environment.NewLine);
                }
              
            }
            
        }
    }
}
