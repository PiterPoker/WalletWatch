using AutoMapper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Application.Mappings;

public class ColorProfile : Profile
{
    public ColorProfile()
    {
        CreateMap<Color?, string?>().ConvertUsing<ColorToStringConverter>();
        CreateMap<string, Color>().ConvertUsing<StringToColorConverter>();
    }
}

internal class ColorToStringConverter : ITypeConverter<Color?, string?>
{
    public string? Convert(Color? source, string destination, ResolutionContext context)
    {
        return source?.Name;
    }
}

internal class StringToColorConverter : ITypeConverter<string, Color>
{
    public Color Convert(string source, Color destination, ResolutionContext context)
    {
        return Color.FromName(source);
    }
}