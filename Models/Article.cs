using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Razor9_identity.Models
{
    public class Article
    {
       
		[Key]
		public int Id {set;get;}

		[Required (ErrorMessage ="{0} phải nhập")]
		[StringLength(255,MinimumLength = 5,ErrorMessage ="{0} phải có độ dài từ {2} đến {1}")]
		[Column(TypeName ="nvarchar")]
		[DisplayName("Tiêu đề")]
		public string Title {set;get;}

		[Required (ErrorMessage ="{0} phải nhập")]
		[Column(TypeName ="text")]
		[StringLength(2558,MinimumLength = 5,ErrorMessage ="{0} phải có độ dài từ {0} đến {1}")]
		[DisplayName("Nội dung")]
		public string Content {set;get;}

		[DataType(DataType.DateTime)]
		[DisplayName("Ngày tạo")]
		[Required(ErrorMessage ="{0} phải chọn")]
		public DateTime Created {set;get;} 
    }
}