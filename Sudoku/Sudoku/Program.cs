using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sudoku
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[][] goodSudoku1 = {
                new int[] {7,8,4,  1,5,9,  3,2,6},
                new int[] {5,3,9,  6,7,2,  8,4,1},
                new int[] {6,1,2,  4,3,8,  7,5,9},

                new int[] {9,2,8,  7,1,5,  4,6,3},
                new int[] {3,5,7,  8,4,6,  1,9,2},
                new int[] {4,6,1,  9,2,3,  5,8,7},

                new int[] {8,7,6,  3,9,4,  2,1,5},
                new int[] {2,4,3,  5,6,1,  9,7,8},
                new int[] {1,9,5,  2,8,7,  6,3,4}
            };


            int[][] goodSudoku2 = {
                new int[] {1,4, 2,3},
                new int[] {3,2, 4,1},

                new int[] {4,1, 3,2},
                new int[] {2,3, 1,4}
            };

            int[][] badSudoku1 =  {
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9}
            };

            int[][] badSudoku2 = {
                new int[] {1,2,3,4,5},
                new int[] {1,2,3,4},
                new int[] {1,2,3,4},
                new int[] {1}
            };

            int[][] zeroSudoku = { };

            Debug.Assert(ValidateSudoku(goodSudoku1), "This is supposed to validate! It's a good sudoku!");
            Debug.Assert(ValidateSudoku(goodSudoku2), "This is supposed to validate! It's a good sudoku!");
            Debug.Assert(!ValidateSudoku(badSudoku1), "This isn't supposed to validate! It's a bad sudoku!");
            Debug.Assert(!ValidateSudoku(badSudoku2), "This isn't supposed to validate! It's a bad sudoku!");

            bool isValid = ValidateSudoku(badSudoku1);
            Console.WriteLine(isValid);
            Console.Read();
        }

        static bool ValidateSudoku(int[][] puzzle)
        {
            //check rules of validation - structure, sizes, length
            int length = puzzle.GetLength(0);
            if (length == 0)
            {
                return false;
            }

            if (!CheckForPerfectSquare(length))
            {
                return false;
            }

            for (int row = 0; row < puzzle.GetLength(0) - 1; row++)
            {
                //check if rows count and cols count are equal
                if (puzzle[row].Length != puzzle[row + 1].Length)
                {
                    return false;
                }

                //check every array row in puzzle for digits from 1 to N where N is array length
                if (!CheckRowArrayDigits1ToN(puzzle[row]))
                {
                    return false;
                }
                
                //check every array row in puzzle for repeated digits
                if (!CheckRowArrayForRepeatedDigits(puzzle[row]))
                {
                    return false;
                }
            }

            //check every little squares
            if (!CheckLittleSquares(puzzle))
            {
                return false;
            }
            
            return true;
        }

        private static bool CheckForPerfectSquare(double input)
        {
            return (Math.Sqrt(input) % 1 == 0);
        }

        private static bool CheckRowArrayDigits1ToN(int[] arr)
        {
            var r = arr.OrderBy(d => d).ToArray();
            if (r[0] != 1 && r[arr.Length - 1] != arr.Length)
            {
                return false;
            }
            return true;
        }

        private static bool CheckRowArrayForRepeatedDigits(int[] arr)
        {
            if (arr.Distinct().ToArray().Length != arr.Length)
            {
                return false;
            }
            return true;
        }

        private static bool CheckLittleSquares(int[][] puzzle)
        {
            int puzzleSize = puzzle.Length;
            int squareSize = (int)Math.Sqrt(puzzleSize);
            var numbers = GetNumbers(puzzle);

            var grid = new int[puzzleSize, puzzleSize];
            {
                int i = 0;
                for (int row = 0; row < puzzleSize; row++)
                {
                    for (int col = 0; col < puzzleSize; col++)
                    {
                        grid[row, col] = numbers[i];
                        i++;
                    }
                }
            }

            for (int row = 0; row < puzzleSize; row += squareSize)
            {
                for (int col = 0; col < puzzleSize; col += squareSize)
                {
                    var tempArr = new int[puzzleSize];
                    int index = 0;
                    for (int i = 0; i < squareSize; i++)
                    {
                        for (int j = 0; j < squareSize; j++)
                        {
                            tempArr[index] = grid[i + col, j + row];
                            index++;
                        }
                    }
                    if (!CheckRowArrayDigits1ToN(tempArr) || !CheckRowArrayForRepeatedDigits(tempArr))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static List<int> GetNumbers(int[][] puzzle)
        {
            List<int> list = new List<int>();
            for (int row = 0; row < puzzle.GetLength(0); row++)
            {
                for (int col = 0; col < puzzle[row].Length; col++)
                {
                    list.Add(puzzle[row][col]);
                }
            }

            return list;
        }
    }
}
