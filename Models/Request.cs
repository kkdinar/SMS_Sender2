using System;
using System.Collections.Generic;
using System.Text;

namespace SMS_Sender2.Models
{
    public class Request
    {
        public string type { get; set; } //Тип окна docID
        public int docID { get; set; } //id таблицы Documents
        public int id { get; set; } //id таблицы
        public IList<docColumns> docColumns { get; set; } //{} с полями и их значениями
    }

   
}
