    using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NumerosEnParalelo
{
    class Program
    {
        static void Main(string[] args)
        {
            var limit = 4_000_000;
            var numbers = Enumerable.Range(0, limit).ToList();

            var watch = Stopwatch.StartNew();
            var numeroPrimosForEach = ListaDeNumeroPrimos(numbers);
            watch.Stop();

            var watchParalelo = Stopwatch.StartNew();
            var numeroPrimosParaleloForEach = ListaDeNumeroPrimosParalelos(numbers);
            watchParalelo.Stop();


            Console.WriteLine($"Bucle ForEach clasico   |   Total de numeros primos: {numeroPrimosForEach.Count}    |   Tiempo de ejecucion: {watch.ElapsedMilliseconds} ms.");
            Console.WriteLine($"Bucle ForEach Paralelo  |   Total de numeros primos: {numeroPrimosParaleloForEach.Count}    |   Tiempo de ejecucion: {watchParalelo.ElapsedMilliseconds} ms.");
        }

        private static IList<int> ListaDeNumeroPrimos(IList<int> numbers) => numbers.Where(EsPrimo).ToList();


        private static IList<int> ListaDeNumeroPrimosParalelos(IList<int> numbers)
        {
            var primeNumbers = new ConcurrentBag<int>();

            Parallel.ForEach(numbers, number =>
            {
                if (EsPrimo(number))
                    primeNumbers.Add(number);
            });

            return primeNumbers.ToList();
        }


        private static bool EsPrimo(int number)
        {
            if(number <2)
                return false;

            for(var i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
    }
}