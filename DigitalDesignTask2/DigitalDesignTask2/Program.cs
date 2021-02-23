using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DigitalDesignTask2
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Пути к текстовым файлам */
            string readPath = @"text.txt";
            string writePath = @"result.txt";

            /* Массивы разделителей и спец. знаков и цифр */
            char[] separators = new char[] { ' ', ',', '.', ':', '-', ';', '\"', '(', ')', 
                '!', '?', '%', '/', '=', '+', '@', '#', '№', '$', '*', '^', '_', '<', '>', '[', ']', '{', '}' };
            char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            /* Массив объеденяющий в себе небуквенные знаки */
            char[] notWords = separators.Concat(numbers).ToArray();

            /* Словарь, в котром ключи - уникальные слова, а значения - 
             * количество раз, которое они встретились в тексте */
            Dictionary<string, int> uniqueWordsDict = new Dictionary<string, int>();

            try
            {
                using (StreamReader sr = new StreamReader(readPath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        /* Разделяем строку на массив слов, не учитывая пробелы, 
                         * скобки и знаки препинания, а так же числа */
                        string[] words = line.Split(notWords, StringSplitOptions.RemoveEmptyEntries);

                        /* Если слово не входит в словарь, добавляем его туда, 
                         * значение ставим = 1. Если слово входит в словарь, 
                         * увеличиваем зничение (количество повторений слова) на 1
                         * Перед проверкой и занесением в словарь всегда 
                         * приводим слово к нижнему регистру */
                        foreach (string word in words)
                        {
                            if (!uniqueWordsDict.Keys.Contains(word.ToLower()))
                                uniqueWordsDict.Add(word.ToLower(), 1);
                            else
                                uniqueWordsDict[word.ToLower()] += 1;
                        }
                    }
                }
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption);
            }

            /* Выводим все найденые слова и количесто раз, 
             * которое они встречаются в тексте */
            PrintWords(uniqueWordsDict, writePath);
            Console.ReadLine();
        }

        /* Метод вывода словаря */
        static void PrintWords(Dictionary<string, int> UniqueWordsDict, string WritePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(WritePath, false))
                {
                    foreach (KeyValuePair<string, int> keyValuePair in UniqueWordsDict.OrderByDescending(keyValuePair => keyValuePair.Value))
                    {
                        sw.WriteLine("{0,24}   |{1,5}", keyValuePair.Key, keyValuePair.Value);                   
                    }
                }
                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}