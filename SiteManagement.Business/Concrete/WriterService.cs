using SiteManagement.Business.Abstract;
using SiteManagement.DataAccess.Abstract;
using SiteManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Business.Concrete
{
	public class WriterService : IWriterService
	{
		private readonly IWriterDAL _writerDAL;
		public WriterService(IWriterDAL writerDAL)
		{
			_writerDAL = writerDAL;
		}
		public void TAdd(Writer t)
		{
			_writerDAL.Add(t);
		}

		public void TDelete(Writer t)
		{
			_writerDAL.Delete(t);
		}

		public Writer TGetByID(int id)
		{
			return _writerDAL.GetByID(id);
		}

		public List<Writer> TGetList()
		{
			return _writerDAL.GetList();
		}

		public void TUpdate(Writer t)
		{
			_writerDAL.Update(t);
		}
	}
}
