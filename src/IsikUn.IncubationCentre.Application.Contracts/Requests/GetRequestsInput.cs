using Volo.Abp.Application.Dtos;

namespace IsikUn.IncubationCentre.Requests
{
    public class GetRequestsInput : PagedAndSortedResultRequestDto
    {
        public string filter { get; set; }
        public string SenderUserName { get; set; }
        public string ReceiverUserName { get; set; }
        public string Title { get; set; }
        public string Explanation { get; set; }


        public GetRequestsInput()
        {

        }
    }
}
