using System;
using System.ComponentModel;
using System.Reflection;

namespace FI.AtividadeEntrevista.BLL.Util
{
    public static class EnumExtensions
    {
        public static string ObterDescricao(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            if (field == null)
                return value.ToString();

            DescriptionAttribute attribute =
                Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                as DescriptionAttribute;

            return attribute?.Description ?? value.ToString();
        }
    }
}
