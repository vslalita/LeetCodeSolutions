/*
Given a string s, return the number of homogenous substrings of s. Since the answer may be too large, return it modulo 109 + 7.

A string is homogenous if all the characters of the string are the same.

A substring is a contiguous sequence of characters within a string.

 

Example 1:

Input: s = "abbcccaa"
Output: 13
Explanation: The homogenous substrings are listed as below:
"a"   appears 3 times.
"aa"  appears 1 time.
"b"   appears 2 times.
"bb"  appears 1 time.
"c"   appears 3 times.
"cc"  appears 2 times.
"ccc" appears 1 time.
3 + 1 + 2 + 1 + 3 + 2 + 1 = 13.
*/

public class Solution 
{
    public int CountHomogenous(string s) 
    {
        if(!String.IsNullOrEmpty(s))
        {          
            int previousCharIndex = 0;
            int homogenousStringCount = 0;
            char[] stringChar = s.ToCharArray();
            for(int currentCharIndex = 0; currentCharIndex < s.Length; previousCharIndex = currentCharIndex)
            {
               int currentStringLength = 1;
               while(currentCharIndex < s.Length && stringChar[currentCharIndex] == stringChar[previousCharIndex])
               {
                  currentCharIndex ++;
               }
               
               if(currentCharIndex > previousCharIndex)
               {
                 currentStringLength = currentCharIndex - previousCharIndex;
                 homogenousStringCount += GetCombinationCount(currentStringLength);
               }
               else
               {
                   homogenousStringCount += currentStringLength;
               }
           } 
          return homogenousStringCount;
        }
        
        return 0;
    }
    
    
    public int GetCombinationCount(int num)
    {
        int result = 1;
          // compute num choose 2
          if(num > 0)
          {
              double res = ((double)num / 2 )*((double)num-1);
              result = (int) ((Math.Round(res) + num) % 1000000007);

          }
        return result;
          
    }
}
