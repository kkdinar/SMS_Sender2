using System;
using System.Collections.Generic;
using System.Text;

namespace SMS_Sender2.Models
{
    public class dictionaryColumns
    {
        public int id { get; set; }
        //Наименование справочника
        public string Title { get; set; }
        //Хранит DocColumns.id - к какому полю этот справочник привязан
        public int DocColumnsId { get; set; }  
        public IList<dicValue> dicValue { get; set; } //{} с полями справочника
        //Выбранное пользователем новое значение
        public string Value { get; set; }
    }

    public class dicValue
    {
        public int id { get; set; }
        //Элемент справочника
        public string Name { get; set; }
    }
}
