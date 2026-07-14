using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class CommentReponse
{
    public string response { get; set; }
    public string message { get; set; }
    public List<CommentData> data { get; set; }


}
public partial class CommentData
{
    public string message { get; set; }
    public string createBy { get; set; }

}