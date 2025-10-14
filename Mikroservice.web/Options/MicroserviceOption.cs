namespace Mikroservice.web.Options
{
    public class MicroserviceOption
    {
        public required MikroserviceOptionItem Catalog { get; set; }


        public required MikroserviceOptionItem File { get; set; }



    }
    public class MikroserviceOptionItem
    {
        public required string BaseUrl { get; set; }
     
    }
}
