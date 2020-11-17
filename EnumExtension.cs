using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;

namespace HomeWork_7
{
    /// <summary>
    /// На мой взгляд самое лучшее ррешение проблемы биндинга Enum.
    /// </summary>
    public class EnumExtension : MarkupExtension
    {
        public Type EnumType { get; private set; }  //Вспомогательное свойство.

        public EnumExtension(Type enumType)  //Конструктор класса
        {
            if (!enumType.IsEnum || enumType is null)  //проверка что не null и является Enum
                throw new Exception("Not Enum or is null");  //Выбрасывем ошибку.

            EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)  //Проброс значения.? надо детальней разобраться.
        {
            return Enum.GetValues(EnumType);                                   //возвращаем значение.
        }

    }

    namespace myName
    {
        enum MyEnum { one, two, three }
    }
}
