/***
 * 
 * Author: Akhil Ramlakan
 * Stores text box
 * 
 ***/

using System;
using System.Text;

namespace Box_Writer_API
{
    class BoxWriter
    {
        private String word;
        private String box;

        //StringBuilder is used by multiple methods
        private StringBuilder boxBuilder;

        //puts a word in the center of the box
        private String label;

        //spacing indicates how many spaces are between letters on horizontal lines
        private int spacing;

        //offset adds x amount of spaces before each line is printed
        //when posting to some online sites, a small amount of offset
        //is needed for the box to display correctly
        private int offset;

        public BoxWriter(String word = null, String label = null)
        {
            this.word = word;
            this.label = label;
            boxBuilder = new StringBuilder();
            box = null;
            spacing = 2;
            offset = 10;
        }

        //creates the "text box"
        public void Boxify()
        {
            int length = word.Length - 1;

            do
            {
                Spaces(offset);
                Spaces(length);

                //outputs word in reverse on top of the box
                if ((length + 1) == word.Length)
                    SpaceWords(1);
                else
                {
                    //creates edges for the top half
                    boxBuilder.Append(JavaStyleSubstring(word, length, length + 1));
                    Spaces(word.Length * (spacing + 1) - (spacing + 2));
                    boxBuilder.Append(JavaStyleSubstring(word, (word.Length - (length + 1)), (word.Length - length)));
                    Spaces(word.Length - length - 2);
                    boxBuilder.Append(JavaStyleSubstring(word, (word.Length - (length + 1)), (word.Length - length)));
                }

                length--;

                if (length == 0)
                {
                    //prints word in normal order in the middle
                    boxBuilder.Append("\n");
                    Spaces(offset);
                    SpaceWords(0);
                    Spaces(word.Length - 2);
                    boxBuilder.Append(JavaStyleSubstring(word, (word.Length - (length + 1)), (word.Length - length)));
                }

                boxBuilder.Append("\n");
            }
            while (length > 0);

            length = word.Length - 1;

            do
            {
                //creates square box of words on the lower half
                Spaces(offset);
                boxBuilder.Append(JavaStyleSubstring(word, (word.Length - length), (word.Length - length + 1)));

                //label maker
                if ((label.Length > 1) && (length - 1 == word.Length / 2))
                {
                    Spaces((((word.Length * (spacing + 1)) - (spacing + 2)) / 2) - (label.Length / 2));
                    boxBuilder.Append(label);

                    //controls spaces for varying word lengths
                    if ((word.Length % 2) == 0)
                    {
                        if ((label.Length % 2) == 1)
                            Spaces((((word.Length * (spacing + 1)) - (spacing + 2)) / 2) - (label.Length / 2) - 1);
                        else
                            Spaces((((word.Length * (spacing + 1)) - (spacing + 2)) / 2) - (label.Length / 2));
                    }
                    else if ((word.Length % 2) == 1)
                    {
                        if ((label.Length % 2) == 0)
                            Spaces((((word.Length * (spacing + 1)) - (spacing + 2)) / 2) - (label.Length / 2) + 1);
                        else
                            Spaces((((word.Length * (spacing + 1)) - (spacing + 2)) / 2) - (label.Length / 2));
                    }
                }
                else
                    Spaces((word.Length * (spacing + 1)) - (spacing + 2));

                boxBuilder.Append(JavaStyleSubstring(word, length - 1, length));
                Spaces(word.Length - 2 - (word.Length - length));

                //adds right half edges
                if (length > 1)
                    boxBuilder.Append(JavaStyleSubstring(word, length - 1, length));

                length--;

                boxBuilder.Append("\n");
            }
            while (length > 1);

            Spaces(offset);
            SpaceWords(1);

            this.box = boxBuilder.ToString();
        }

        //creates n amount of spaces
        public void Spaces(int n)
        {
            for (int i = 0; i < n; i++)
                boxBuilder.Append(" ");
        }

        //prints a word with spacing value spaces in between
        //k = 0 prints words in normal order
        //k = 1 prints words in reverse
        public void SpaceWords(int k)
        {
            if (k == 0)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    boxBuilder.Append(JavaStyleSubstring(word, i, i + 1));

                    if (i < word.Length - 1)
                        Spaces(spacing);
                }
            }
            else if (k == 1)
            {
                for (int i = word.Length; i > 0; i--)
                {
                    boxBuilder.Append(JavaStyleSubstring(word, i - 1, i));

                    if (i > 1)
                        Spaces(spacing);
                }
            }
        }

        public int GetSpacing()
        {
            return spacing;
        }

        public int GetOffset()
        {
            return offset;
        }

        public void SetWord(String word) { this.word = word; }

        public void SetSpacing(int n) { spacing = n; }

        public void SetOffset(int n) { offset = n; }

        public override String ToString() { return box; }

        static string JavaStyleSubstring(string s, int beginIndex, int endIndex)
        {
            // simulates Java substring function
            int len = endIndex - beginIndex;
            return s.Substring(beginIndex, len);
        }
    }
}
