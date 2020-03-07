using System;

namespace DocumentRequestAPI.Models
{
	public class RequestModel
	{
		private string m_status;
		private string m_detail;

		public RequestModel()
		{
			created = LastModified = DateTime.UtcNow;
		}

		public int id { get; set; } = -1;

		public string body { get; set; }

		public string status
		{
			get { return m_status; }
			set
			{
				m_status = value;
				UpdateLastModified();
			}
		}

		public string detail
		{
			get { return m_detail; }
			set
			{
				m_detail = value;
				UpdateLastModified();
			}
		}

		public string callback { get; set; }

		public DateTime created { get; }

		public DateTime LastModified { get; private set; }

		private void UpdateLastModified()
		{
			LastModified = DateTime.UtcNow;
		}
	}
}