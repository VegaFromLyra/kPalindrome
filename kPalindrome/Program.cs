using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//A k-palindrome is a string which transforms into a palindrome on removing at most k characters. 

//Given a string S, and an integer K, print "YES" if S is a k-palindrome; otherwise print "NO". 
//Constraints: 
//S has at most 20,000 characters. 
//0<=k<=30 

//Sample Test Case#1: 
//Input - abxa 1 
//Output - YES 
//Sample Test Case#2: 
//Input - abdxa 1 
//Output - No

// The idea is to use edit distance where only insertions and deletions are allowed
// to find the distance between S and its reverse. If the edit distance is <= 2k, then S is a k-palindrome
// Example, when S = abxa and k = 1, if we remove 1 character 'x', S = aba is a palindrome
// This is deduced by using edit distance where s1 = abxa and s2 = axba
// s1 can be transformed to s2 by 1 deletion  (removing 'x') and 1 insertion ('x'),
// so edit distance = 2. And the only differing character between S and its reverse is that one character
// so using 2k as a metric can be used to solve this problem
namespace kPalindrome
{
    class Program
    {
        static void Main(string[] args)
        {
            Test("abxa", 1);
            Test("abdxa", 1);
        }

        static void Test(string s, int k)
        {
            string result = IsKPalindrome(s, k) ? "Yes" : "No";
            Console.WriteLine(result);
        }

        static bool IsKPalindrome(string s, int k)
        {
            if (String.IsNullOrEmpty(s))
            {
                return true;
            }

            string s1 = s;

            string s2 = Reverse(s);

            int editDistance = GetModifiedDistance(s1, s2);

            return (editDistance <= 2 * k) ? true : false;
        }

        static string Reverse(string s)
        {
            StringBuilder sb = new StringBuilder(s);

            int start = 0;
            int end = sb.Length - 1;

            while (start < end)
            {
                char temp = sb[start];
                sb[start] = sb[end];
                sb[end] = temp;

                start++;
                end--;
            }

            return sb.ToString();
        }

        private static int InsertionCost = 1;

        private static int DeletionCost = 1;

        static int GetModifiedDistance(string s1, string s2)
        {
            if (String.IsNullOrEmpty(s1) && String.IsNullOrEmpty(s2))
            {
                return 0;
            }
            else if (String.IsNullOrEmpty(s1))
            {
                return InsertionCost * s2.Length;
            }
            else if (String.IsNullOrEmpty(s2))
            {
                return DeletionCost * s1.Length;
            }
            else
            {
                int i = s1.Length - 1;

                int j = s2.Length - 1;

                if (s1[i] == s2[j])
                {
                    return GetModifiedDistance(s1.Substring(0, s1.Length - 1), s2.Substring(0, s2.Length - 1));
                }

                int insertionCost = InsertionCost + GetModifiedDistance(s1, s2.Substring(0, s2.Length - 1));

                int deletionCost = DeletionCost + GetModifiedDistance(s1.Substring(0, s1.Length - 1), s2);

                return insertionCost < deletionCost ? insertionCost : deletionCost;
            }
        }
    }
}
