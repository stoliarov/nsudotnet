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
			Assert.Null(uut.FindByFamily("�������"));
			uut.AddWorker("�������", "������", "����������");
			Assert.NotNull(uut.FindByFamily("�������"));

			Assert.Null(uut.FindByFamily("������"));
			int id = uut.FindByFamily("�������").Id;
			uut.EditWorker(id, "������", "������", "����������");
			Assert.NotNull(uut.FindByFamily("������"));

			uut.DeleteWorker(id);
			Assert.Null(uut.FindByFamily("������"));
		}
	}
}
