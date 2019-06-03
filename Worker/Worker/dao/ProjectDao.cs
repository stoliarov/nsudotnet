using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Worker.context;

namespace Worker.dao
{
	public class ProjectDao
	{
		private ApplicationContext context;

		public ProjectDao(ApplicationContext context) {
			this.context = context;
		}

		public void AddProject(String name, int reward, int workerId) {
			Worker worker = context.Workers.FirstOrDefault(w => w.Id == workerId);
			Project project = new Project(name, reward, worker);
			context.Projects.Add(project);

			context.SaveChanges();
		}

		public List<Project> GetProjects() {
			return context.Projects
				.Include(p => p.Worker)
				.ToList();
		}

		public Project GetProject(int id) {
			return context.Projects.FirstOrDefault(p => p.Id == id);
		}

		public void EditProject(int id, String name, int reward) {
			var project = context.Projects.FirstOrDefault(p => p.Id == id);

			if (project != null) {
				project.Name = name;
				project.Reward = reward;

				context.SaveChanges();
			}
		}

		public void EditProject(int id, String name, int reward, int workerId) {
			var project = context.Projects.FirstOrDefault(p => p.Id == id);

			Worker worker = context.Workers.FirstOrDefault(w => w.Id == workerId);

			if (project != null) {
				project.Name = name;
				project.Reward = reward;
				project.Worker = worker;

				context.SaveChanges();
			}
		}

		public void EditProject(int id, String name, int reward, Worker worker) {
			var project = context.Projects.FirstOrDefault(p => p.Id == id);

			if (project != null) {
				project.Name = name;
				project.Reward = reward;
				project.Worker = worker;

				context.SaveChanges();
			}
		}

		public void DeleteProject(int id) {
			var project = context.Projects.FirstOrDefault(p => p.Id == id);

			if (project != null) {
				context.Projects.Remove(project);

				context.SaveChanges();
			}
		}
	}
}
