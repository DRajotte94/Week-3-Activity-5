using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Week_3_Activity_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void browse_Click(object sender, EventArgs e) // browse button click
        {
            openFile.InitialDirectory = @"C:\"; // Sets files search directory
            openFile.Title = "Browse Files"; // title of file search form

            // if user selects a file and clicks OK
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                fileNameText.Text = openFile.FileName; // diplay file name in textbox
                displayBox.Clear(); // if textbox already contains input from previous file, clear contents

                String line; // variable for each line within text file

                StreamReader inputFile;
                inputFile = File.OpenText(openFile.FileName);
                
                while (!inputFile.EndOfStream) // breaks at end of file
                {
                    line = inputFile.ReadLine(); // reads each line of text file and saves it to line variable

                    // credit to user1693593 on https://stackoverflow.com/questions/13318561/adding-new-line-of-data-to-textbox
                    displayBox.AppendText(line); // adds data to textbox
                    displayBox.AppendText(Environment.NewLine); // creates a new line for next loop
                }

                inputFile.Close(); // close the file

                String lower; // string variable for converting to lowercase
                lower = displayBox.Text.ToLower(); // saves textbox content from original file data to new variable and converts to lowercase
                lowerCase.Text = lower; // displays content in new textbox

                String[] words = lower.Split(new[] { " " }, StringSplitOptions.None); // splits text into an array

                // getting first and last alphabetically 
                Array.Sort(words); // sorts the array into alphabetical order
                // Takes the first and last words from the newly sorted array
                String first = words[0];
                String last = words[words.Length - 1];
                // displays results
                firstWord.Text = first;
                lastWord.Text = last;

                // getting the longest word
                String longest = ""; // variable being collected with for loop
                int count = 0; // integer used for comparisons
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Length > count) // uses count to hold length value of longest word, compares it against the length of currently indexed word
                    {
                        longest = words[i];
                        count = longest.Length;
                        longWord.Text = longest; // displays new data
                    }
                }

                // getting word with the most vowels
                // credit Reed Copsey from  https://stackoverflow.com/questions/18109890/c-sharp-count-vowels
                var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' }; // list of vowels for comparison
                int total = 0;
                for (int i = 0; i < words.Length; i++)
                {
                    int x = 0; // count for number of values in each word
                    String checkWord = words[i]; // saves current word to variale
                    for (int j = 0; j < checkWord.Length; j++)
                    {
                        if (vowels.Contains(checkWord[j])) // indexes each letter of the word and checks if it contains a value
                        {
                            x++; // increments the counter each time a letter is a vowel
                        }
                        if (x > total) // if number of vowels in current word exceeds previous highest total
                        {
                            total = x; // new highest total
                            mostVowels.Text = words[i]; // displays the word
                        }
                    } 
                }
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            saveFile.InitialDirectory = @"C:\"; // sets files save directory
            saveFile.Title = "Save File"; // creates title for file window

            // if user inputs a name and selects a file save location then presses ok
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter outputFile;
                outputFile = File.CreateText(saveFile.FileName);

                // write data to new text file
                outputFile.WriteLine("Original file contents: \n" + displayBox.Text);
                outputFile.WriteLine("Lowercase: \n" + lowerCase.Text);
                outputFile.WriteLine("Longest word: " + longWord.Text);
                outputFile.WriteLine("First alphabetically: " + firstWord.Text);
                outputFile.WriteLine("Last alphabetically: " + lastWord.Text);
                outputFile.WriteLine("Most vowels: " + mostVowels.Text);

                outputFile.Close(); // close file

            }
        }
    }
}
