namespace PlattformOrdMan.Data
{
    public class OrderPostDto
    {
        public int PostId { get; set; } 
        public Enquiry Account { get; set; }

        public Enquiry Periodization { get; set; }
        public int OrderedUserId { get; set; }  

    }
}
