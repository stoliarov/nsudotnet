using System;
using Xunit;
using Worker.context;
using Worker.dao;

namespace Worker.Tests
{
	public class WorkerDaoTest
	{
		private WorkerDao uut;

		public WorkerDaoTest() {
			uut = new WorkerDao(new context.ApplicationContext());
		}

		[Fact]
		public void TestAddEditDelete() {
			Assert.Null(uut.FindByFamily("Андреев"));
			uut.AddWorker("Андреев", "Андрей", "Земфирович");
			Assert.NotNull(uut.FindByFamily("Андреев"));

			Assert.Null(uut.FindByFamily("Киреев"));
			int id = uut.FindByFamily("Андреев").Id;
			uut.EditWorker(id, "Киреев", "Андрей", "Земфирович");
			Assert.NotNull(uut.FindByFamily("Киреев"));

			uut.DeleteWorker(id);
			Assert.Null(uut.FindByFamily("Киреев"));
		}
	}
}
