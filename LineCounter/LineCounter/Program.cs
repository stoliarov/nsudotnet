using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LineCounter
{
	class Program {
		private static int count { get; set; }

		static void Main(string[] args) {
			if (args.Length < 1) {
				Console.WriteLine("Expected file types as an arguments (e.g. *.cs). Press enter to exit");
				Console.ReadLine();
				return;
			}

			Console.WriteLine("\nLine count: " + ProcessDirectories("./", args));
		}

		private static int ProcessDirectories(String dir, String[] fileTypes) {
			int count = ProcessFilesInDirectory(dir, fileTypes);
			String[] dirs = Directory.GetDirectories(dir);
			foreach (String dirToProcess in dirs) {
				count += ProcessDirectories(dirToProcess, fileTypes);
			}

			return count;
		}

		private static int ProcessFilesInDirectory(String directory, String[] fileTypes) {
			String[] files = Directory.GetFiles(directory);
			files = FilterFiles(files, fileTypes);
			Console.WriteLine(directory);
			int count = 0;

			foreach (string file in files) {
				StreamReader reader = new StreamReader(file, System.Text.Encoding.Default);

				String line = reader.ReadLine();
				while (line != null) {
					line = SkipMultilineCommentIfNeeded(line, reader);

					if (!string.IsNullOrEmpty(line) && !string.IsNullOrWhiteSpace(line) && !IsOneLineComment(line)) {
						count++;
					}

					
					// todo вроде обрабатываем все файлы текущей директории, нужно сделать обход директорий

					line = reader.ReadLine();
				}

				reader.Close();
			}

			return count;
		}

		private static String[] FilterFiles(String[] files, String[] fileTypes) {
			List<String> resultFiles = new List<String>();

			foreach (String file in files) {
				int dotIndex = file.LastIndexOf('.');
				if (dotIndex == -1 || file.Length < dotIndex + 2) {
					continue;
				}

				String fileType = "*." + file.Substring(dotIndex + 1);
				if (fileTypes.Contains(fileType)) {
					resultFiles.Add(file);
				}
			}

			return resultFiles.ToArray();
		}

		private static bool IsOneLineComment(String line) {
			bool prevCharIsSlash = false;

			foreach (char character in line.ToCharArray()) {
			
				if (character.Equals('/')) {
					if (prevCharIsSlash) {
						return true;
					}
					prevCharIsSlash = true;
				} else if (prevCharIsSlash) {
					return false;
				} else {
					prevCharIsSlash = false;
				}
			}

			return false;
		}

		private static String SkipMultilineCommentIfNeeded(String line, StreamReader reader) {
			String firstChar = GetFirstSignificantChar(line);
			int firstCharIndex = line.IndexOf(firstChar);

			char[] lineChars = line.ToCharArray();
			if (lineChars.Length > firstCharIndex + 1) {
				String nextChar = line.ToCharArray()[firstCharIndex + 1].ToString();

				if (firstChar.Equals("/") && nextChar.Equals("*")) {
					String restOfLine = line.Substring(firstCharIndex + 2);

					if(!String.IsNullOrEmpty(restOfLine)) {
						int endCommentIndex = IndexOfCommentEnd(restOfLine);
						if (-1 != endCommentIndex) {
							return chooseLineToReturn(line, reader, restOfLine, endCommentIndex);
						}
					}

					while(true) {
						line = reader.ReadLine();
						if (line == null) {
							return line;
						}
						int endCommentIndex2 = IndexOfCommentEnd(line);

						if (-1 != endCommentIndex2) {
							return chooseLineToReturn(line, reader, line, endCommentIndex2);
						}
					}
				}
			}

			return line;
		}

		private static string chooseLineToReturn(string line, StreamReader reader, string restOfLine, int endCommentIndex) {
			if (restOfLine.Length <= endCommentIndex + 1) {
				return reader.ReadLine();
			}

			String afterCommentRestOfLine = restOfLine.Substring(endCommentIndex + 1);
			String significantChar = GetFirstSignificantChar(afterCommentRestOfLine);

			if (!significantChar.Equals("")) {
				return line;
			} else {
				return reader.ReadLine();
			}
		}

		/// <summary>
		/// Возвращает индекс конца многострочного коммента
		/// </summary>
		/// <param name="line"></param>
		/// <returns>индекс конца многострочного коммента - символа '/', или -1 если конец коммента не найден</returns>
		private static int IndexOfCommentEnd(String line) {
			int starIndex = line.IndexOf("*");
			if (starIndex != -1 && line.Length > starIndex + 1) {
				if (line.ToCharArray()[starIndex + 1].Equals('/')) {
					
					return starIndex + 1;
				}
			}
			
			return -1;
		}

		private static String GetFirstSignificantChar(String line) {
			char[] whiteSpaceChars = new[] { ' ', '\t', '\n', '\r' };

			foreach (char character in line.ToCharArray()) {
				if (!IsWhiteSpaceSymbol(character)) {
					return character.ToString();
				}
			}

			return "";
		}

		private static bool IsWhiteSpaceSymbol(char symbol) {
			char[] whiteSpaceChars = new[] { ' ', '\t', '\n', '\r' };
			return Array.Exists(whiteSpaceChars, element => element.Equals(symbol));
		}
	}
}
