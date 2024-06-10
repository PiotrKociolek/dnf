using System;
using System.Collections.Generic;
using System.IO;

namespace dnfSln
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // sciezka do pliku wejściowego, podmienic na własną
            string fileName = "C:\\Users\\piotr\\Desktop\\dnf\\dnfSln\\dnfSln\\file.txt"; 

            try
            {
                List<string> data = LoadData(fileName);
                List<List<string>> dnf = GenerateDNF(data);
                DisplayDNF(dnf);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static List<string> LoadData(string fileName)
        {
            List<string> data = new List<string>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    data.Add(line);
                }
            }
            return data;
        }

        static List<List<string>> GenerateDNF(List<string> data)
        {
            List<List<string>> dnf = new List<List<string>>();
            foreach (string line in data)
            {
                string[] parts = line.Split(',');
                string classLabel = parts[parts.Length - 1];
                List<string> disjunction = new List<string>();
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    string attribute = parts[i];
                    if (attribute.Equals("1"))
                    {
                        disjunction.Add("A" + (i + 1));
                    }
                    else if (attribute.Equals("0"))
                    {
                        disjunction.Add("~A" + (i + 1));
                    }
                }
                disjunction.Add(classLabel);
                dnf.Add(disjunction);
            }
            return dnf;
        }

        static void DisplayDNF(List<List<string>> dnf)
        {
            foreach (List<string> disjunction in dnf)
            {
                for (int i = 0; i < disjunction.Count - 1; i++)
                {
                    Console.Write(disjunction[i] + " + ");
                }
                Console.WriteLine(disjunction[disjunction.Count - 1]);
            }
        }
    }
}
