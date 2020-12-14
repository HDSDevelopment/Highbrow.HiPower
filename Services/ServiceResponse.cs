
namespace Highbrow.HiPower.Services
{
    public class ServiceResponse<T>
    {        
        public T Data {get; set;}
        
        public ServiceResult Result { get; set; }

        public string Message { get; set; }        
    }    
}