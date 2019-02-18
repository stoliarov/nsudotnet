using System;

namespace Task1 {
    class Calendar {
        static void Main(string[] args) {
            Console.WriteLine("Enter the date:");
			string date = Console.ReadLine();
			DateTime dateTime;
			if(DateTime.TryParse(date, out dateTime)) {
				int month = dateTime.Month;
				int year = dateTime.Year;
				int daysInMonth = DateTime.DaysInMonth(year, month);
				int dayOffCount = 0;
				DateTime.TryParse("1/" + month + "/" + year, out dateTime);

				Console.WriteLine("Mon Tue Wen Thu Fri Sat Sun");
				for(int i = 1; i < (int) dateTime.DayOfWeek; i++) {
					Console.Write("    ");
				}			
				while(dateTime.Month == month) {
					if (dateTime.Day == 10) {
						Console.Write(" ");
					}
					Console.Write(" " + dateTime.Day + " ");
					if(dateTime.Day < 9) {
						Console.Write(" ");
					}
					if(dateTime.DayOfWeek == DayOfWeek.Sunday) {
						Console.WriteLine();
					}
					if(dateTime.DayOfWeek == DayOfWeek.Sunday || dateTime.DayOfWeek == DayOfWeek.Saturday) {
						dayOffCount++;
					}
					dateTime = dateTime.AddDays(1);
				}
				Console.WriteLine();
				Console.WriteLine("Number of working days in this month: " + (daysInMonth - dayOffCount));
			} else {
				Console.WriteLine("Failed to parse the date");
			}
			Console.ReadLine();
		}
    }
}
