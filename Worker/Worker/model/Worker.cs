using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Worker
{
	public class Worker
	{
		public Worker(string firstName, string middleName, string lastName) {
			FirstName = firstName;
			MiddleName = middleName;
			LastName = lastName;
			Projects = new List<Project>();
		}

		public Worker(int id, string firstName, string middleName, string lastName) {
			Id = id;
			FirstName = firstName;
			MiddleName = middleName;
			LastName = lastName;
			Projects = new List<Project>();
		}

		[Key]
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }

		public List<Project> Projects { get; set; }
	}

	public class Project
	{
		public Project(string name, int reward) {
			Name = name;
			Reward = reward;
		}

		public Project(int id, string name, int reward) {
			Id = id;
			Name = name;
			Reward = reward;
		}

		public Project(string name, int reward, Worker worker) {
			Name = name;
			Reward = reward;
			Worker = worker;
		}

		public Project(int id, string name, int reward, Worker worker) {
			Id = id;
			Name = name;
			Reward = reward;
			Worker = worker;
		}

		public Project(int id, string name, int reward, int workerId) {
			Id = id;
			Name = name;
			Reward = reward;
			WorkerId = workerId;
		}

		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public int Reward { get; set; }

		public int? WorkerId { get; set; }
		public Worker Worker { get; set; }
	}
}
