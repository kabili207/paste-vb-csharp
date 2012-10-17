using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasteAsCSharpVB
{
	interface IConvertCode
	{
		string Convert(string code);
		string ConverterName { get; }
	}
}
