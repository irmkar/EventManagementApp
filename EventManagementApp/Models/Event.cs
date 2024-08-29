using System.ComponentModel.DataAnnotations;

namespace EventManagementApp.Models
{

    public class Event
    {
        public int Id { get; set; }

        [Required]

        public string Title { get; set; }          // Başlık

        [Required]

        public string? Location { get; set; }       // Yer

        [Required]

        public DateTime Date { get; set; }         // Zaman

        [Required]

        public bool IsPaid { get; set; }           // Ücretli/Ücretsiz
        public decimal? Price { get; set; }          // Ücret (Ücretli ise doldurulacak)
        public string? Description { get; set; }    // Açıklama

        //[Required]

        public string? ImageUrl { get; set; }       // Görsel URL
        public string? EventType { get; set; }      // Etkinlik tipi (Select box veya radio button ile seçilecek)
    }
}