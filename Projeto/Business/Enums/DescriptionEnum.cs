using System;

namespace Business.Enums
{
    public class DescriptionEnum : Attribute
    {
        public string Text;

        public DescriptionEnum(string text)
        {
            Text = text;
        }
    }
}
