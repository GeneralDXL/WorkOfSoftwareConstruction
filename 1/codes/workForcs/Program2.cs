using System;   

namespace workForcs1;
class Program2
{
    public static void p2(string[] args)
    {
        int n1, n2;
        Console.WriteLine("Enter the first number:");
        string str = Console.ReadLine();
        string[] parts = str.Split(' ');
        n1= int.Parse(parts[0]);
        n2= int.Parse(parts[1]);
        int[] result = PrimeBetween(n1, n2);
        Console.WriteLine("Prime numbers between " + n1 + " and " + n2 + ":");
        for (int i = 0; i < result.Length; i++)
        {
            Console.Write(result[i] + " ");
        }
    }

    static int[] PrimeBetween(int n1, int n2)
    {
        int[] primes = new int[100]; 
        int count = 0;
        for (int i = n1; i <= n2; i++)
        {
            if (IsPrime(i))
            {
                primes[count] = i;
                count++;
            }
        }
        
        Array.Resize(ref primes, count);
        return primes;
    }

    static bool IsPrime(int num)
    {
        if (num <= 1) return false;
        for (int i = 2; i <= Math.Sqrt(num); i++)
        {
            if (num % i == 0) return false;
        }
        return true;
    }
}

