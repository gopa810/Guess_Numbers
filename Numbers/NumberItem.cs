using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numbers
{
    public class NumberItem
    {
        public bool mark = false;
        public ArrayList nums = new ArrayList();

        public void Set(string[] s)
        {
            int n;
            foreach (string h in s)
            {
                int.TryParse(h, out n);
                nums.Add(n);
            }
            Sort();
        }

        public void Sort()
        {
            nums.Sort();
        }

        public int CountOfLessThan(int e)
        {
            int i = 0;
            foreach (int g in nums)
            {
                if (g < e)
                    i++;
            }
            return i;
        }

        public int Count
        {
            get
            {
                return nums.Count;
            }
        }
        public bool HasMember(int num)
        {
            foreach (int a in nums)
            {
                if (a == num)
                    return true;
            }
            return false;
        }
        public int Number(int ord)
        {
            if (ord < 0 || ord >= nums.Count)
            {
                return -1;
            }
            return Convert.ToInt32(nums[ord]);
        }

        public int CountOfIntersect(NumberItem ni)
        {
            int c = 0;

            foreach (int a in nums)
            {
                if (ni.HasMember(a))
                    c++;
            }
            return c;
        }

        public string CountOfIntersect2(NumberItem ni)
        {
            int c1 = 0;
            int c2 = 0;
            int ca = 0;
            ca = 0;
            foreach (int a in nums)
            {
                ca++;
                if (ni.HasMember(a))
                {
                    if (ca >= 5 && ca <= 15)
                        c1++;
                    else
                        c2++;
                }
            }
            return string.Format("{0}:{1}", c1, c2) ;
        }

        public int TotalDistance(NumberItem ni)
        {
            int dist = 0;
            foreach (int a in nums)
            {
                int minDist = 1000;
                int minNum = 0;
                foreach (int b in ni.nums)
                {
                    if (Math.Abs(a - b) < minDist)
                    {
                        minNum = b;
                        minDist = Math.Abs(a - b);
                    }
                }
                dist += minDist;
            }
            return dist;
        }

        public int MaxDistance(NumberItem ni)
        {
            int dist = 0;
            foreach (int a in nums)
            {
                int minDist = 1000;
                int minNum = 0;
                foreach (int b in ni.nums)
                {
                    if (Math.Abs(a - b) < minDist)
                    {
                        minNum = b;
                        minDist = Math.Abs(a - b);
                    }
                }
                if (minDist > dist)
                    dist = minDist;
            }
            return dist;
        }

        public void Clear()
        {
            nums.Clear();
        }

        public void Add(int i)
        {
            nums.Add(i);
        }

        public NumberItem Copy()
        {
            NumberItem ni = new NumberItem();

            foreach (int a in nums)
            {
                ni.Add(a);
            }

            return ni;
        }

        public int Sum()
        {
            int sum = 0;
            foreach (int a in nums)
            {
                sum += a;
            }
            return sum;
        }

        public bool TestNumberInRange(int index, int start, int end)
        {
            return Number(index) >= start && Number(index) <= end;
        }

        public string CSV
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (int g in nums)
                {
                    if (sb.Length > 0)
                        sb.AppendFormat(",");
                    sb.AppendFormat("{0}", g);
                }
                return sb.ToString();
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (int g in nums)
            {
                sb.AppendFormat("{0,2} ", g);
            }
            return sb.ToString();
        }

        public NumberItem Derivation(int der)
        {
            NumberItem ni = new NumberItem();
            for (int i = der; i < nums.Count; i++)
            {
                ni.nums.Add(Number(i) - Number(i - der));
            }
            return ni;
        }
    }
}
