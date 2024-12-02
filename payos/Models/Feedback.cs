using System;
using System.Collections.Generic;

namespace payos.Models
{
	public partial class Feedback
	{
		public int FeedbackId { get; set; }
		public string? Content { get; set; }
		public int? MemberId { get; set; }

		public virtual Member? Member { get; set; }
	}
}
