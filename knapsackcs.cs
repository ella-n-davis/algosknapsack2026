//compiled, run, tested using OnlineGDB:
//written in C# since that's what my internship is in this summer
/******************************************************************************

                            Online C# Compiler.
                Code, Compile, Run and Debug C# program online.
Write your code in this editor and press "Run" button to execute it.

*******************************************************************************/

//this solution uses a bottom-up dynamic programming approach to algorithmically and dynamically determine a maximum value
//within the knapsack and the items use to reach that value
//the solution processes each subproblem inside the nested forloop and saves it to the dp table
//it then goes through the table to determine which items were used 
//and finally reads the relevant cell in the table to gain the max value
using System;
using System.Collections;


class knapsackcs{

public static void Print2DArray<T>(T[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i,j] + "\t");
            }
            Console.WriteLine();
        }
    }

//function takes in values, weights, and knapsack capacity
static void knapsack(int[] valueUnfixed, int[] weightUnfixed, int capacity)
{
    //the algorithm below only works if each array starts with 0, due to bounds errors
    //and indexing math.
    //but to preserve the usability of the function, assuming it will be tested with many problems,
    //i've included code that will:
    //add '0' to the beginning of each array for you before any more code is run
    int[] value = new int[valueUnfixed.Length + 1];
    value[0] = 0;                                
    Array.Copy(valueUnfixed, 0, value, 1, valueUnfixed.Length);
    
    int[] weight = new int[weightUnfixed.Length + 1];
    weight[0] = 0;                                
    Array.Copy(weightUnfixed, 0, weight, 1, weightUnfixed.Length);
    //number of items = number of values
    int n = valueUnfixed.Length;
    
    //2d array represents the dp/knapsack table. it is one line larger to avoid out of bounds exceptions
    int[,] dp = new int[n+1, capacity+1];

    //outer loop, i, represents the rows or items of the table
    for(int i = 0; i <= n; i++)
    {
        //inner loop, c, represents the columns or increasing capacities of the table
        for(int c = 0; c <= capacity; c++)
        {
            
            //the first if statement handles the 0s:
            // first row + anywhere that capacity is 0 or item weighs too much
            if(i == 0 || c == 0)
            {
                
                //placing 0 into the table
                dp[i,c] = 0;
            }
            //check if the weight of the current item, fits within the current capacity
            //small error handling - i < n ensures no out of bounds
            else if(i < n && weight[i] <= c)
            {
                
                //need to decide what to place here since it fits
                //it needs to be the biggest value combination that fits
                //this math method chooses the max value of the two, either the item's
                //value plus the value of the item in the above row, at the column minus the weight of our item 
                //(basically, the biggest prior value which can fit in the remaining space)
                //OR the item directly above it
                dp[i,c] = Math.Max(value[i] + dp[i - 1, c - weight[i]], dp[i - 1, c]);
            }
            //otherwise, it doesn't fit, so we need to place the item above
            else
            {
                
                dp[i,c] = dp[i-1, c];
            }
        }
    
    }
    //find what items were used
    //essentially, if the item one row above the one considered is different, then the item must've been used (as it has higher value)
    //if they are the same, there was no change, and we didn't use that item
    
    ArrayList combo = new ArrayList();
    int comboCapacity = capacity;
    for(int i = n; i > 0; i--){
        if(dp[i,comboCapacity] != dp[i-1,comboCapacity]){
            //i should reflect the index of the item
            combo.Add(i);
            comboCapacity -= weight[i];
        }
    }
    
    int maxValue = dp[n, capacity];
    Console.WriteLine("Max value: "+ maxValue + "\nElements used: ");
    foreach(var item in combo){
            Console.WriteLine(item);
        }
    Console.WriteLine("Table:");
    Print2DArray(dp);
}

static void Main(string[] args)
{
    int[] value = {1600, 1000, 1800, 1200, 2000};
    int[] weight = {6, 4, 5, 3, 7};
    int capacity = 18;

    knapsack(value,weight,capacity);
}

}
