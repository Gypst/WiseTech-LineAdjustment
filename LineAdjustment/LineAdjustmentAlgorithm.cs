using System;
using System.Collections.Generic;

namespace LineAdjustment
{
    public class LineAdjustmentAlgorithm
    {
        public string Transform(string inputText, int maxLineWidth)
        {
            if (string.IsNullOrEmpty(inputText) || maxLineWidth <= 0)
            {
                return string.Empty;
            }

            var wordsInInput = inputText.Split([' '], StringSplitOptions.RemoveEmptyEntries);
            var justifiedLines = new List<string>();
            var currentLineWords = new List<string>();
            var currentLineLength = 0;

            foreach (var word in wordsInInput)
            {
                if (word.Length > maxLineWidth)
                {
                    var brokenWordLines = BreakLongWord(word, maxLineWidth);
                    if (currentLineWords.Count > 0)
                    {
                        justifiedLines.Add(string.Join(" ", currentLineWords).PadRight(maxLineWidth));
                        currentLineWords.Clear();
                        currentLineLength = 0;
                    }
                    justifiedLines.AddRange(brokenWordLines);
                    continue;
                }

                if (currentLineLength + currentLineWords.Count + word.Length > maxLineWidth)
                {
                    justifiedLines.Add(GenerateJustifiedLine(currentLineWords, currentLineLength, maxLineWidth));
                    currentLineWords.Clear();
                    currentLineLength = 0;
                }

                currentLineWords.Add(word);
                currentLineLength += word.Length;
            }

            if (currentLineWords.Count > 0)
            {
                justifiedLines.Add(string.Join(" ", currentLineWords).PadRight(maxLineWidth));
            }

            return string.Join("\n", justifiedLines);
        }

        private string GenerateJustifiedLine(List<string> wordsInLine, int totalWordLength, int maxLineWidth)
        {
            if (wordsInLine.Count == 1)
            {
                return wordsInLine[0].PadRight(maxLineWidth);
            }

            var totalPaddingSpaces = maxLineWidth - totalWordLength;
            var evenSpacesBetweenWords = totalPaddingSpaces / (wordsInLine.Count - 1);
            var extraSpacesToDistribute = totalPaddingSpaces % (wordsInLine.Count - 1);

            var justifiedTextLine = string.Empty;

            for (int i = 0; i < wordsInLine.Count - 1; i++)
            {
                justifiedTextLine += wordsInLine[i];
                justifiedTextLine += new string(' ', evenSpacesBetweenWords + (i < extraSpacesToDistribute ? 1 : 0));
            }

            justifiedTextLine += wordsInLine[^1];

            return justifiedTextLine;
        }

        private List<string> BreakLongWord(string word, int maxLineWidth)
        {
            var brokenLines = new List<string>();

            for (int i = 0; i < word.Length; i += maxLineWidth)
            {
                var chunk = word.Substring(i, Math.Min(maxLineWidth, word.Length - i));
                if (chunk.Length < maxLineWidth)
                {
                    chunk = chunk.PadRight(maxLineWidth);
                }
                brokenLines.Add(chunk);
            }

            return brokenLines;
        }
    }
}
