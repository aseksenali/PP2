using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter = 0; //number of primes
            int n = int.Parse(Console.ReadLine()); //reading number of numbers
            int[] a = new int[n];//array of numbers
            bool[] prime = new bool[n];//bool array, which indicates that a[i] is prime
            string[] s = Console.ReadLine().Split();//input
            for (int i = 0; i < n; i++)
            {
                a[i] = int.Parse(s[i]);//reading a[i]
                counter++;
                if(a[i] != 1)//1 is not a prime, so this is a special case
                    prime[i] = true;
                else
                {
                    counter--;
                }
                for (int j = 2; j < a[i]; j++)
                {
                    if (a[i] % j == 0)//if a[i] has divider, except of itself or 1, it is not prime
                    {
                        prime[i] = false;
                        counter--;
                        break;
                    }
                }
            }

            Console.WriteLine(counter);//output of number of primes
            for (int i = 0; i < n; i++)
            {
                if (prime[i])
                {
                    Console.Write(a[i] + " ");//output of primes
                }
            }
        }
    }
}
