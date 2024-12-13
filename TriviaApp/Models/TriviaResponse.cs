using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp.Models
{
    public class TriviaResponse
    {
        public int ResponseCode { get; set; }
        public List<TriviaQuestion> Questions { get; set; }
    }
}
