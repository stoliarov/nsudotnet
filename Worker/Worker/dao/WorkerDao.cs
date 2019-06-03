using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Worker.context;

namespace Worker.dao
{
	public class WorkerDao
	{
		public ApplicationContext context;

		public WorkerDao(ApplicationContext context) {
			this.context = context;
		}

		public void AddWorker(String firstName, String middleName, String lastName) {
			Worker worker = new Worker(firstName, middleName, lastName);
			context.Workers.Add(worker);

			context.SaveChanges();
		}

		public Worker FindByFamily(String secondName) {
			return context.Workers.FirstOrDefault(w => w.FirstName.Equals(secondName));
		} 

		public List<Worker> GetWorkers() {
			return context.Workers
				.Include(worker => worker.Projects)
				.ToList();
		}

		public Worker GetWorker(int id) {
			return context.Workers.FirstOrDefault(worker => worker.Id == id);
		}

		public void EditWorker(int id, String firstName, String middleName, String lastName) {
			var worker = context.Workers.FirstOrDefault(w => w.Id == id);

			if (worker != null) {
				worker.FirstName = firstName;
				worker.MiddleName = middleName;
				worker.LastName = lastName;

				context.SaveChanges();
			}
		}

		public void DeleteWorker(int id) {
			var worker = context.Workers.FirstOrDefault(w => w.Id == id);

			if (worker != null) {
				context.Workers.Remove(worker);

				context.SaveChanges();
			}
		}
	}
}
