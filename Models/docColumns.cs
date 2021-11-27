using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SMS_Sender2.Models
{
    [Table("docColumns")]
    public class docColumns
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int id { get; set; }
        //Внешний ключ к таблице Documents docID
        public int TableId { get; set; }
        //Порядковый номер документа id для journal для БД в мобилке
        public int Number { get; set; }
        public string DataTypes { get; set; }
        //Наименование поля
        public string Name { get; set; }
        //Описание поля
        public string Description { get; set; }
        //Значение поля
        public string Value { get; set; }
        //Дата создания записи в поле
        public string Date { get; set; }
        //Хранит DocColumns.id - какое поле из какой таблицы является справочником для данного столбца
        public int DictionaryId { get; set; }
    }
}
