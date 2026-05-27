using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary
{




    /// <summary>
    /// https://github.com/yogiramchandani/Ambiguous_Sort
    /// </summary>
    public class StringSorter : IStringSorter
    {
        public class SortItem
        {
            public List<string> Chapters { get; set; } = [];
            public string OriginalValue { get; set; } = string.Empty;
        }

        public string[] Sort(string[] vals)
        {

            if (vals == null || vals.Length == 0) return Array.Empty<string>();

            var toSort = new SortItem[vals.Length];

            //looping each array item
            for (int i = 0; i < vals.Length; i++)
            {
                var sortItem = new SortItem() { OriginalValue = vals[i] };
                var chapters = ""; //bad place, creates allocation
                //looping all chars of the array item
                for (int j = 0; j < vals[i].Length; j++)
                {
                    //it does not care about spaces, so ignore
                    while (char.IsWhiteSpace(vals[i][j]) && j < vals[i].Length - 1)
                    {
                        j++;
                    }

                    // after spaces or it is a mumber or no number
                    // it will store for example 4.10.15
                    while ((char.IsNumber(vals[i][j]) || vals[i][j] == '.') && j < vals[i].Length - 1)
                    {
                        chapters = chapters + vals[i][j];
                        j++;
                    }

                    //adding the chapter
                    sortItem.Chapters.Add(string.IsNullOrEmpty(chapters) ? int.MaxValue.ToString() : chapters);

                    //clean the chapter 
                    chapters = "";

                    // since got here, have already extracted the chapter numbers
                    // let skips all till end of str
                    while (vals[i][j] != '|' && vals[i][j] != '>' && vals[i][j] != '»' && j < vals[i].Length - 1)
                    {
                        j++;
                    }

                    //edge situation
                    if (vals[i][j] == '>')
                    {
                        j++;
                    }

                    //add it to array to sort
                    if (j == vals[i].Length - 1)
                    {
                        toSort[i] = sortItem; //next loops creates a new sortItem
                    }
                }
            }

            //since I have my array strucure, just implement the comparer
            Array.Sort(toSort, (a, b) =>
            {
                var pa = string.Join(".", a.Chapters).Split('.').Select(int.Parse).ToArray();
                var pb = string.Join(".", b.Chapters).Split('.').Select(int.Parse).ToArray();

                int len = Math.Min(pa.Length, pb.Length);

                for (int i = 0; i < len; i++)
                {
                    int cmp = pa[i].CompareTo(pb[i]);

                    if (cmp != 0)
                        return cmp;
                }

                return pa.Length.CompareTo(pb.Length);
            });

            return toSort.Select(a=>a.OriginalValue).ToArray();
        }
    }
}
