using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory.PrettyPrinter;

namespace PasteAsCSharpVB
{
	class NRefactoryConverter
	{
		
		public string ConvertCodeSnippet(string codeToConvert, bool csharpToVb)
		{
			// TODO: Will need to expand when other languages are added.

			SnippetParser parser = new SnippetParser((csharpToVb ? SupportedLanguage.CSharp : SupportedLanguage.VBNet));

			parser.Parse(codeToConvert);

			INode node = parser.Parse(codeToConvert);

			if ((parser.Errors.Count > 0))
			{
			    return parser.Errors.ErrorOutput;
			}

			// parser.Errors.ErrorOutput contains syntax errors, if any
			// parser.Specials is the list of comments, preprocessor directives etc.
			if (csharpToVb)
				PreprocessingDirective.CSharpToVB(parser.Specials);
			else
				PreprocessingDirective.VBToCSharp(parser.Specials);


			IOutputAstVisitor output = output = (csharpToVb ? (IOutputAstVisitor)new VBNetOutputVisitor() : (IOutputAstVisitor)new CSharpOutputVisitor());
			using (SpecialNodesInserter.Install(parser.Specials, output))
			{
				node.AcceptVisitor(output, null);
			}

			return output.Text;
		}

	}
}
