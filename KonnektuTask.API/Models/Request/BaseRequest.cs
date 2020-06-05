using System.ComponentModel.DataAnnotations;

namespace KonnektuTask.API.Models.Request
{
    public class BaseRequest<T>
    {
        public T Request { get; set; }
    }
}