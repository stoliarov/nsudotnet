using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Worker.context;
using Worker.dao;

namespace Worker.util
{
	class Printer
	{
		private ApplicationContext context;

		public Printer(ApplicationContext context) {
			this.context = context;
		}

		public void PrintWorkersWithProjectsOrderByTotalReward() {

			var workersWithAverageReward = from worker in context.Workers
						select new {
							wks = worker,
							AverageReward = worker.Projects.Average(p => p.Reward)
						};

			var workersWithAverageRewardSorted = from pair in workersWithAverageReward
						 orderby pair.AverageReward descending
						 select pair;

			foreach (var w in workersWithAverageRewardSorted) {
				Console.WriteLine($"{w.wks.Id} {w.wks.FirstName} {w.wks.MiddleName} {w.AverageReward}");
			}
		}

		public void PrintProjectsOfWorker(int id) {
			var worker = context.Workers.FirstOrDefault(w => w.Id == id);
			
			if (worker != null) {
				String projectNames = ProjectListToString(worker.Projects);
				Console.WriteLine($"{worker.Id} {worker.FirstName} {worker.MiddleName} Проекты: {projectNames}");
			}
		}

		private String ProjectListToString(List<Project> projects) {
			StringBuilder builder = new StringBuilder();
			projects.ForEach(p => builder.Append(p.Name + ", "));
			String result = builder.ToString();
			return result.Substring(0, result.Length - 2);
		}
	}
}
