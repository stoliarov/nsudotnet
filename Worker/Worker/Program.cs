using System;
using Worker.context;
using Worker.dao;
using Worker.util;

namespace Worker
{
	class Program
	{
		static void Main(string[] args) {
			ApplicationContext context = new ApplicationContext();
			WorkerDao workerDao = new WorkerDao(context);
			ProjectDao projectDao = new ProjectDao(context);
			Printer printer = new Printer(context);

			//workerDao.AddWorker("Иванов", "Иван", "Иванович");
			//workerDao.AddWorker("Семенов", "Вадим", "Олегович");
			//workerDao.AddWorker("Жуковский", "Всеволод", "Анатольевич");

			//projectDao.AddProject("Посадить дерево", 10, 7);
			//projectDao.AddProject("Построить дом", 15, 8);
			//projectDao.AddProject("Вырастить сына", 25, 8);
			//projectDao.AddProject("Построить баню", 7, 9);

			printer.PrintWorkersWithProjectsOrderByTotalReward();
			Console.ReadLine();
		}
	}
}
